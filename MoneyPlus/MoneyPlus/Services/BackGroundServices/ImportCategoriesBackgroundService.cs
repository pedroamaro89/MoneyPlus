
using MoneyPlus.Data;
using System.Diagnostics;
using System.ComponentModel;
using YamlDotNet.Serialization;
using System.IO;
using Microsoft.AspNetCore;
using MoneyPlus.Services.Models;
using static MoneyPlus.Services.BackGroundServices.ImportCategoriesBackgroundService;

namespace MoneyPlus.Services.BackGroundServices;

public class ImportCategoriesBackgroundService : BackgroundService
{

	// Configurações
	TimeSpan IntervalBetweenJobs = TimeSpan.FromMinutes(5);
	//TimeSpan IntervalBetweenJobs = TimeSpan.FromSeconds(30);
	public IServiceProvider _serviceProvider { get; }

	public ImportCategoriesBackgroundService(IServiceProvider serviceProvider)
	{
		_serviceProvider = serviceProvider;
	}

	protected override async Task ExecuteAsync(CancellationToken ct)
	{
		using var scope = _serviceProvider.CreateScope();
		var ctx = scope.ServiceProvider.GetRequiredService<MoneyPlusContext>();

		while (!ct.IsCancellationRequested)
		{
			await DoWorkAsync();

			// Debug.WriteLine(ctx.Cities.First().Name);

			await Task.Delay(IntervalBetweenJobs);
		}
	}

	private async Task DoWorkAsync()
	{
		try
		{
			string fileLocation = "c:\\temp\\categories.yaml";

			bool fileExists = File.Exists("c:\\temp\\categories.yaml");
			if (!fileExists)
			{
				SerializeCategoriesYaml();
				DeserializeCategoriesYaml(fileLocation);
			}
			else
			{
				var categories = DeserializeCategoriesYaml(fileLocation);
				ImportCategories(categories);
			}
		}
		catch (Exception e)
		{
			Logger.WriteLog("Error importing yaml");
		}
	}


	private void SerializeCategoriesYaml()
	{
		using var scope = _serviceProvider.CreateScope();
		var context = scope.ServiceProvider.GetRequiredService<MoneyPlusContext>();

		var yamlFile = "c:\\temp\\categories.yaml";



		bool folderExists = Directory.Exists("c:\\temp\\");
		if (!folderExists)
			Directory.CreateDirectory("c:\\temp\\");

		var category = new Category()
		{
			Name = "University"
		};

		var subCategory = new SubCategory()
		{
			Name = "Books",
			Category = category
		};

		var subCategory1 = new SubCategory()
		{
			Name = "Traveling",
			Category = category
		};

		var subList = new List<SubCategory>();
		subList.Add(subCategory);
		subList.Add(subCategory1);


		var categorySub = new CategoriesSub()
		{
			category = category,
			subCategories = subList
		};

		var categoriesSub = new List<CategoriesSub>();
		categoriesSub.Add(categorySub);

		var serializer = new SerializerBuilder().Build();
		var serializeYaml = serializer.Serialize(categoriesSub);
		//Escrever para ficheiro
		File.WriteAllText(yamlFile, serializeYaml);
	}

	public List<CategoriesSub> DeserializeCategoriesYaml(string location)
	{
		var yamlFile = File.ReadAllText(location);
		var deserializer = new DeserializerBuilder().Build();
		var categories = deserializer.Deserialize<List<CategoriesSub>>(yamlFile);

		return categories;
	}


	public async void ImportCategories(List<CategoriesSub> categories)
	{
		using var scope = _serviceProvider.CreateScope();
		var context = scope.ServiceProvider.GetRequiredService<MoneyPlusContext>();

		Category cat;
		SubCategory subCat;

		bool changes = false;

		foreach (var item in categories)
		{
			var catBD = context.Category.Where(c => c.Name == item.category.Name).FirstOrDefault();

			if (catBD == null)
			{
				cat = new Category();
				cat.Name = item.category.Name;

				context.Category.Add(cat);
				changes = true;
			}
			else
			{
				cat = catBD;
			}

			if (item.subCategories != null)
			{

				foreach (var sub in item.subCategories)
				{
					var subCatBD = context.SubCategory.Where(c => c.Name == sub.Name && c.CategoryId == cat.ID).FirstOrDefault(); ;

					if (subCatBD == null)
					{
						subCat = new SubCategory();
						subCat.Name = sub.Name;
						subCat.Category = cat;

						context.SubCategory.Add(subCat);
						changes = true;
					}
					else
					{
						subCat = subCatBD;
					}
				}
			}
		}

		if (changes)
		{
			await context.SaveChangesAsync();
			Logger.WriteLog("New categories imported from yaml.");
		}
	}
}
