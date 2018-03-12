using DutchTreat.Library2.Models.Persistent.Interfaces;

namespace DutchTreat.Library2.Models.Persistent
{
  public class OrderItem : IEntity
    {
    public int Id { get; set; }
    public Product Product { get; set; }
    public int Quantity { get; set; }
    public decimal UnitPrice { get; set; }
    public Order Order { get; set; }
  }
}