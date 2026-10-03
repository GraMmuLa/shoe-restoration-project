using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace ShoeRestorationProject.Models;

[Table("ShoeTypes")]
public class ShoeType
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public string? Description { get; set; }

    public ICollection<Shoe> Shoes { get; set; } = new List<Shoe>();
}
