using DutchTreat.Data.Entities;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace DutchTreat.Data
{
    public class DutchSeeder
    {
        private readonly DutchContext ctx;
        private readonly IHostingEnvironment hosting;
        private readonly UserManager<StoreUser> userManager;

        public DutchSeeder(DutchContext ctx, IHostingEnvironment hosting, UserManager<StoreUser> userManager)
        {
            this.ctx = ctx;
            this.hosting = hosting;
            this.userManager = userManager;
        }

        public async Task SeedAsync()
        {
            ctx.Database.EnsureCreated();

            StoreUser user = await userManager.FindByEmailAsync("nasty_potapova@mail.ru");
            if (user == null)
            {
                user = new StoreUser()
                {
                    FirstName = "Nastya",
                    LastName = "Potapova",
                    Email = "nasty_potapova@mail.ru",
                    UserName = "nasty_potapova@mail.ru"
                };

                var result = await userManager.CreateAsync(user, "P@ssw0rd!");

                if (result != IdentityResult.Success)
                {
                    throw new InvalidOperationException("Could not create new user in seeder.");
                }
            }

            if (!ctx.Products.Any())
            {
                // Need to create sample data
                var filePath = Path.Combine(hosting.ContentRootPath, "Data/art.json");
                var json = File.ReadAllText(filePath);
                var products = JsonConvert.DeserializeObject<IEnumerable<Product>>(json);
                ctx.Products.AddRange(products);
                
                var order = ctx.Orders.Where(o => o.Id == 1).FirstOrDefault();
                if (order != null)
                {
                    order.User = user;
                    order.Items = new List<OrderItem>()
                    {
                        new OrderItem()
                        {
                            Product = ctx.Products.Local.First(),
                            Quantity = 5,
                            UnitPrice = ctx.Products.Local.First().Price
                        }
                    };
                }

                ctx.SaveChanges();
            }
        }
    }
}
