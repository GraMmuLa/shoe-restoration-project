using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ShoeRestorationProject.Models;

[Table("Countries")]
public class Country
{
    [Key]
    [Column("id")]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }

    [Column("Name", TypeName = "nvarchar(64)")]
    public string Name { get; set; } = null!;

    [Column("IsoCode", TypeName = "nchar(2)")]
    public string IsoCode { get; set; } = null!;

    public ICollection<Brand> Brands { get; set; } = new List<Brand>();
}
