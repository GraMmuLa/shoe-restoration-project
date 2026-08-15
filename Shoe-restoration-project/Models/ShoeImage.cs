using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Shoe_restoration_project.Models;

[Table("ShoeImages")]
public partial class ShoeImage
{
    [Key]
    [Column("Id")]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public Guid Id { get; set; }

    [Required]
    [Column("Name", TypeName = "nvarchar(255)")]
    public string Name { get; set; } = null!;

    [Column("ImageData", TypeName = "varbinary(max)")]
    public byte[]? ImageData { get; set; }

    [Column("ShoeId", TypeName = "int")]
    public int ShoeId { get; set; }

    [ForeignKey("ShoeId")]
    public Shoe Shoe { get; set; } = null!;
}
