using MapsterMapper;
using ShoeRestorationProject.Exceptions;
using ShoeRestorationProject.Features.Authentication.DTO;
using ShoeRestorationProject.Features.Jwt;
using ShoeRestorationProject.Features.Jwt.DTO;
using ShoeRestorationProject.Features.Roles;
using ShoeRestorationProject.Helpers;
using ShoeRestorationProject.Models;
using ShoeRestorationProject.Repositories;

namespace ShoeRestorationProject.Features.Authentication;

public class AuthenticationService
{
    private readonly IRepository<User, int> _userRepository;
    private readonly IRepository<Role, int> _roleRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IJwtTokenService _jwtTokenService;
    private readonly IMapper _mapper;

    public AuthenticationService(IRepository<User, int> userRepository,
        IRepository<Role, int> roleRepository, IUnitOfWork unitOfWork,
        IJwtTokenService jwtTokenService, IMapper mapper)
    {
        _userRepository = userRepository;
        _roleRepository = roleRepository;
        _unitOfWork = unitOfWork;
        _jwtTokenService = jwtTokenService;
        _mapper = mapper;
    }
    
    public async Task<JwtResponseDto> LoginAsync(LoginRequestDto loginRequestDto)
    {
        var user = await _userRepository.GetByPropertyAsync(x => x.Username == loginRequestDto.Username, 
                       x=>x.Role) ??
                   throw new AuthenticationException();

        return !BCrypt.Net.BCrypt.Verify(loginRequestDto.Password, user.PasswordHash) ?
            throw new AuthenticationException() :
            _jwtTokenService.GenerateToken(_mapper.Map<JwtUserDto>(user));
    }

    public async Task<RegisterResponseDto> RegisterAsync(RegisterRequestDto registerRequestDto)
    {
        if (await _userRepository.ExistsByPropertyAsync(x => x.Username == registerRequestDto.Username) ||
            await _userRepository.ExistsByPropertyAsync(x => x.Email == registerRequestDto.Email))
            throw new UniqueObjectException("User already exists");
        
        var userToRegister = _mapper.Map<User>(registerRequestDto);
        
        userToRegister.PasswordHash = BCrypt.Net.BCrypt.HashPassword(registerRequestDto.Password);

        var userToRegisterRole = await _roleRepository.GetByPropertyAsync(r=>r.Name == AppRoles.User)
            ?? throw new NotFoundException($"Role {AppRoles.User} is not found");

        userToRegister.RoleId = userToRegisterRole.Id;

        var result = await _unitOfWork.ExecuteAsync(() =>
            _userRepository.AddAsync(userToRegister));

        var jwtResponseDto = _jwtTokenService
            .GenerateToken(_mapper.Map<JwtUserDto>(result));


        return new RegisterResponseDto(result.Id, jwtResponseDto);
    }
}