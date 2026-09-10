using ElectroMart.API.Models;
using Microsoft.EntityFrameworkCore;

namespace ElectroMart.API.Data;

public static class DbSeeder
{
    public static async Task SeedAsync(ApplicationDbContext db)
    {
        await db.Database.EnsureCreatedAsync();
        if (!await db.Users.AnyAsync()) db.Users.Add(new User { FirstName = "Electro", LastName = "Admin", Email = "admin@electromart.com", PasswordHash = BCrypt.Net.BCrypt.HashPassword("Admin@123"), Role = UserRole.Admin });
        if (!await db.Categories.AnyAsync())
        {
            var names = new[] { "Smartphones", "Laptops", "Tablets", "Headphones", "Smartwatches", "Cameras", "Gaming", "Accessories", "Televisions" };
            db.Categories.AddRange(names.Select(name => new Category { Name = name, Description = $"Explore our {name.ToLowerInvariant()} collection." }));
            await db.SaveChangesAsync();
        }
        if (!await db.Products.AnyAsync())
        {
            var products = new (string n, string b, int c, decimal p, decimal? d, int s)[] {
                ("iPhone 15 Pro", "Apple",1,999,null,25),("Galaxy S24 Ultra","Samsung",1,1199,1099,18),("Pixel 8 Pro","Google",1,899,799,20),
                ("MacBook Air M3","Apple",2,1099,null,12),("XPS 15","Dell",2,1599,1399,8),("Pavilion 15","HP",2,749,649,16),("ThinkPad X1 Carbon","Lenovo",2,1399,null,10),
                ("iPad Air","Apple",3,599,549,22),("Galaxy Tab S9","Samsung",3,799,699,14),
                ("WH-1000XM5","Sony",4,399,349,30),("QuietComfort Ultra","Bose",4,429,null,12),("Tune 770NC","JBL",4,129,99,40),
                ("Apple Watch Series 9","Apple",5,399,349,19),("Galaxy Watch 6","Samsung",5,299,249,24),
                ("EOS R6 Mark II","Canon",6,2499,2299,5),("Alpha a7 IV","Sony",6,2498,null,6),
                ("PlayStation 5 Slim","Sony",7,499,null,9),("Xbox Wireless Controller","Microsoft",7,59,49,50),
                ("MX Master 3S","Logitech",8,99,79,35),("K380 Keyboard","Logitech",8,39,29,45),("50-inch 4K OLED TV","Sony",9,1299,999,7),("65-inch QLED TV","Samsung",9,1499,1199,6)
            };
            db.Products.AddRange(products.Select((x, i) => new Product { Name=x.n, Brand=x.b, CategoryId=x.c, Price=x.p, DiscountPrice=x.d, StockQuantity=x.s, IsFeatured=i < 8, Description=$"Premium {x.n} from {x.b}. Built for modern connected living with reliable performance and exceptional quality.", ImageUrl=$"https://placehold.co/800x600/101828/ffffff?text={Uri.EscapeDataString(x.n)}", Specifications="{\"Warranty\":\"1 year\",\"Color\":\"Available in multiple colors\"}" }));
        }
        await db.SaveChangesAsync();
    }
}
