namespace WebAPI_PL.Controllers.Demo
{
    public class TestProduct
    {
        public TestProduct(string name, string description, int price, int remainingStock)
        {
            Name = name;
            Description = description;
            Price = price;
            RemainingStock = remainingStock;
        }

        public string Name { get; set; }
        public string Description { get; set; }
        public int Price { get; set; }
        public int RemainingStock { get; set; }
    }
}
