namespace backend.Entities;

public class OrderInfo
{
    public int? OrderId { get; set; }
    public string ProductType { get; set; } = String.Empty;
    public int ProductId { get; set; }
}