using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ShoeRestorationProject.Models;

public class Brand
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public string? Description { get; set; }

    public int CountryId { get; set; }

    public Country Country { get; set; } = null!;

    public ICollection<Shoe> Shoes { get; set; } = new List<Shoe>();
}
