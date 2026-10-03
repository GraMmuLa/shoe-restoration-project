using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ShoeRestorationProject.Models;

public class Condition
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public ICollection<Shoe> Shoes { get; set; } = new List<Shoe>();
}
