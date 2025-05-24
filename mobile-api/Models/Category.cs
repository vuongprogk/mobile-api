using System.ComponentModel.DataAnnotations.Schema;

namespace mobile_api.Models;

public class Category
{
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; } = string.Empty;
    public ICollection<Tour> Tours { get; set; }
}