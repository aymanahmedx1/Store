using System.Text.Json;

using Microsoft.Extensions.Logging;

using Store.data.Context;
using Store.data.Entity;

namespace Store.Repository
{
    public class SeedData
    {
        public static async Task SeedDataAsync(ApplicationDbContext context , ILoggerFactory ilogger) {
			try
			{
                if (context.productBrands!=null && !context.productBrands.Any())
                {
                    var brandData = File.ReadAllText("../Store.Repository/SeedData/brands.json");
                    var brandsList = JsonSerializer.Deserialize<List<ProductBrand>>(brandData);
                    if (brandsList is not null)
                    {
                        await context.productBrands.AddRangeAsync(brandsList);
                    }
                }
                if (context.ProductTypes != null && !context.ProductTypes.Any())
                {
                    var typesData = File.ReadAllText("../Store.Repository/SeedData/types.json");
                    var typesList = JsonSerializer.Deserialize<List<ProductType>>(typesData);
                    if (typesList is not null)
                    {
                        await context.ProductTypes.AddRangeAsync(typesList);
                    }
                }
                if (context.Products != null && !context.Products.Any())
                {
                    var productsData = File.ReadAllText("../Store.Repository/SeedData/products.json");
                    var productsList = JsonSerializer.Deserialize<List<Product>>(productsData);
                    if (productsList is not null)
                    {
                      await context.Products.AddRangeAsync(productsList);
                    }
                }
                if (context.DeliveryMethods != null && !context.DeliveryMethods.Any())
                {
                    var deliveryMethodsData = File.ReadAllText("../Store.Repository/SeedData/delivery.json");
                    var deliveryMethods = JsonSerializer.Deserialize<List<DeliveryMethod>>(deliveryMethodsData);
                    if (deliveryMethods is not null)
                    {
                        await context.DeliveryMethods.AddRangeAsync(deliveryMethods);
                    }
                }
                await context.SaveChangesAsync();

            }
            catch (Exception ex )
			{
                var logger = ilogger.CreateLogger<SeedData>();
                logger.LogError(ex.Message);
			}
        
        }
    }
}
