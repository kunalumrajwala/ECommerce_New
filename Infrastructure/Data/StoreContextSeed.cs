using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using Core.Entities;
using Core.Entities.OrderAggregate;

namespace Infrastructure.Data
{
    public class StoreContextSeed
    {
        public static async Task SeedAsync(StoreContext context){
            if(!context.ProductBrand.Any()){
                var brandsData =  File.ReadAllText("../Infrastructure/Data/SeedData/brands.json");
                var brand = JsonSerializer.Deserialize<List<ProductBrand>>(brandsData);
                context.ProductBrand.AddRange(brand);
            }

            if(!context.ProductType.Any()){
                var ProductTypeData =  File.ReadAllText("../Infrastructure/Data/SeedData/types.json");
                var ProductTypes = JsonSerializer.Deserialize<List<ProductType>>(ProductTypeData);
                context.ProductType.AddRange(ProductTypes);
            }

            if(!context.Products.Any()){
                var ProductsData =  File.ReadAllText("../Infrastructure/Data/SeedData/Products.json");
                var Products = JsonSerializer.Deserialize<List<Product>>(ProductsData);
                context.Products.AddRange(Products);
            }            

            if(!context.DeliveryMethods.Any())
            {
                var deliveryData = File.ReadAllText("../Infrastructure/Data/SeedData/delivery.json");
                var methods = JsonSerializer.Deserialize<List<DeliveryMethod>>(deliveryData);
                context.DeliveryMethods.AddRange(methods);
            }

            if(context.ChangeTracker.HasChanges()) await context.SaveChangesAsync();
        }
    }
}