using MapsterMapper;
using ShoeRestorationProject.Exceptions;
using ShoeRestorationProject.Features.Jwt;
using ShoeRestorationProject.Features.Roles;
using ShoeRestorationProject.Features.Users.DTO;
using ShoeRestorationProject.Helpers;
using ShoeRestorationProject.Models;
using ShoeRestorationProject.Repositories;

namespace ShoeRestorationProject.Features.Users;

public class UserService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IJwtTokenService _jwtTokenService;
    private readonly IRepository<User, int> _userRepository;
    private readonly IMapper _mapper;
    private readonly RoleService _roleService;

    public UserService(IUnitOfWork unitOfWork, IJwtTokenService jwtTokenService,
        IRepository<User, int> userRepository, IMapper mapper,
        RoleService roleService)
    {
        _unitOfWork = unitOfWork;
        _jwtTokenService = jwtTokenService;
        _userRepository = userRepository;
        _mapper = mapper;
        _roleService = roleService;
    }

    public async Task<IList<UserDto>> GetAllAsync()
    {
        var users = await _userRepository.GetAllAsync(x=>x.Role);
        return [.. users.Select(x => _mapper.Map<UserDto>(x))];
    }

    public async Task<UserDto> GetByIdAsync(int id)
    {
        var user = await _userRepository.GetByPropertyAsync(x=> x.Id == id, x=> x.Role) ??
            throw new NotFoundException("User not found");

        var userDto = _mapper.Map<UserDto>(user);
        
        return userDto;
    }

    // TODO
}