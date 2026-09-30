using ShoeRestorationProject.Models;

namespace ShoeRestorationProject.Repositories
{
    /// <summary>
    /// Repository for Shoe entity. Inherits all base CRUD operations from generic repository.
    /// Can be extended with Shoe-specific methods if needed.
    /// </summary>
    public interface IShoesRepository : IRepository<Shoe, int>
    {
        // Shoe-specific methods can be added here
    }
}
