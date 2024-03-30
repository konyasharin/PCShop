namespace backend.Entities;

public class Filter
{
    public int? Id { get; set; }
    public string FilterName { get; set; }
    public string? ComponentType { get; set; }
    public string FilterValue { get; set; }
}