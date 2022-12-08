namespace MoneyPlus.Services.Models
{
    public class SubCategory: Category  
    {
        public int SubID { get; set; }
        public string SubName { get; set; }
        public Category Category { get; set; }
        public int CategoryId { get; set; } 
    }
}
