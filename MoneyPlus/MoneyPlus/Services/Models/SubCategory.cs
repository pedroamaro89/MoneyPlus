namespace MoneyPlus.Services.Models
{
    public class SubCategory
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public Category Category { get; set; }
        public int CategoryId { get; set; } 
    }
}
