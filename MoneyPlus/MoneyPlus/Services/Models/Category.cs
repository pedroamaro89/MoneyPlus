namespace MoneyPlus.Services.Models
{
    public class Category
    {
        public int ID { get; set; }
        public string Name { get; set; } 
        
    }

	public class CategoriesSub
	{
		public Category category { get; set; }
		public List<SubCategory> subCategories { get; set; }
	}
}