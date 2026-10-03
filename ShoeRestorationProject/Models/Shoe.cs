using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ShoeRestorationProject.Models;

[Table("Shoes")]
public class Shoe
{
    public int Id { get; set; }

    public string Title { get; set; } = null!;

    public string Description { get; set; } = null!;

    public int BrandId { get; set; }
    public Brand Brand { get; set; } = null!;

    public int ShoeTypeId { get; set; }
    
    public ShoeType ShoeType { get; set; } = null!;

    public int ColorId { get; set; }
    public Color Color { get; set; } = null!;
    
    public int ConditionId { get; set; }
    public Condition Condition { get; set; } = null!;

    public int SkinTypeId { get; set; }
    public SkinType SkinType { get; set; } = null!;

    public ICollection<ShoeImage> ShoeImages { get; set; } = new List<ShoeImage>();

    public ICollection<ShoeMeasurement> ShoeMeasurements { get; set; } = new List<ShoeMeasurement>();
    
    public Size Size { get; set; } = null!;
}
