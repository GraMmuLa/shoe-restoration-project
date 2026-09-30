using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ShoeRestorationProject.Models;

[Table("Shoes")]
public class Shoe
{
    [Key]
    [Column("Id")]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }

    [Column("Title", TypeName = "nvarchar(64)")]
    public string Title { get; set; } = null!;

    [Column("Description", TypeName = "nvarchar(5048)")]
    public string Description { get; set; } = null!;

    [Column("BrandId", TypeName = "int")]
    public int BrandId { get; set; }

    [Column("ShoeTypeId", TypeName = "int")]
    public int ShoeTypeId { get; set; }

    [Column("ColorId", TypeName = "int")]
    public int ColorId { get; set; }

    [Column("ConditionId", TypeName = "int")]
    public int ConditionId { get; set; }

    [Column("SizeId", TypeName = "int")]
    public int SizeId { get; set; }

    [Column("SkinTypeId", TypeName = "int")]
    public int SkinTypeId { get; set; }

    [ForeignKey("BrandId")]
    public Brand Brand { get; set; } = null!;

    [ForeignKey("ColorId")]
    public Color Color { get; set; } = null!;

    [ForeignKey("ConditionId")]
    public Condition Condition { get; set; } = null!;

    public ICollection<ShoeImage> ShoeImages { get; set; } = new List<ShoeImage>();

    public ICollection<ShoeMeasurement> ShoeMeasurements { get; set; } = new List<ShoeMeasurement>();

    [ForeignKey("ShoeTypeId")]
    public ShoeType ShoeType { get; set; } = null!;

    [ForeignKey("SizeId")]
    public Size Size { get; set; } = null!;

    [ForeignKey("SkinTypeId")]
    public SkinType SkinType { get; set; } = null!;
}
