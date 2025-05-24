namespace mobile_api.Models;

public class Tag
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public ICollection<Tour> Tours { get; set; } = new List<Tour>();
}