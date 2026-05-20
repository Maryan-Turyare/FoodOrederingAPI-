namespace FoodOrederingAPI.Models
{
    public class Order
    {
        public int Id { get; set; }
        public string CustomerName { get; set; } = "";
        public int FoodId { get; set; }
        public int Quantity { get; set; }
    }
}