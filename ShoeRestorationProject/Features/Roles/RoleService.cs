using MapsterMapper;
using ShoeRestorationProject.Exceptions;
using ShoeRestorationProject.Helpers;
using ShoeRestorationProject.Models;
using ShoeRestorationProject.Repositories;

namespace ShoeRestorationProject.Features.Roles;

public class RoleService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IRepository<Role, int> _roleRepository;
    private readonly IMapper _mapper;

    public RoleService(IUnitOfWork unitOfWork, IRepository<Role, int> roleRepository,
        IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _roleRepository = roleRepository;
        _mapper = mapper;
    }
    
    public async Task<RoleResponse> GetByIdAsync(int id)
    {
        var role = await _roleRepository.GetByPropertyAsync(x => x.Id == id) ??
                   throw new NotFoundException("Role not found");
        return _mapper.Map<RoleResponse>(role);
    }
    
    public async Task<RoleResponse> GetByNameAsync(string name)
    {
        var role = await _roleRepository.GetByPropertyAsync(x => x.Name == name)
            ?? throw new NotFoundException("Role is not found");
        
        return _mapper.Map<RoleResponse>(role);
    }
    
    public async Task<IList<RoleResponse>> GetAllAsync()
    {
        return [.. (await _roleRepository.GetAllAsync()).Select(x => _mapper.Map<RoleResponse>(x))];
    }

    public async Task<RoleResponse> AddAsync(RoleRequest roleRequest)
    {
        if (await _roleRepository.ExistsByPropertyAsync(x => x.Name == roleRequest.Name))
            throw new UniqueObjectException("Role already exists");
        
        var role = await _unitOfWork.ExecuteAsync(() =>
            _roleRepository.AddAsync(_mapper.Map<Role>(roleRequest)));
        
        return await GetByIdAsync(role.Id);
    }

    public async Task<RoleResponse> UpdateAsync(int id, RoleRequest roleRequest)
    {
        var roleToUpdate = await _roleRepository.GetByPropertyAsync(x=> x.Id == id) ??
            throw new NotFoundException("Role not found");

        var response = await _unitOfWork.ExecuteAsync(() =>
            _roleRepository.Update(roleToUpdate));

        return _mapper.Map<RoleResponse>(response);
    }

    public async Task<RoleResponse> DeleteAsync(int id)
    {
        var roleToDelete = await _roleRepository.GetByPropertyAsync(x => x.Id == id) ??
            throw new NotFoundException("Role not found");
        
        var response = await _unitOfWork.ExecuteAsync(() => 
            _roleRepository.Delete(roleToDelete));

        return _mapper.Map<RoleResponse>(response);
    }
}