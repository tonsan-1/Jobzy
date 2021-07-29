namespace Jobzy.Data.Seeding
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;

    using Jobzy.Data.Models;

    internal class CategoriesSeeder : ISeeder
    {
        public async Task SeedAsync(ApplicationDbContext dbContext, IServiceProvider serviceProvider)
        {
            await SeedCategory(dbContext, "Accounting & Consulting");
            await SeedCategory(dbContext, "Data Science & Analytics");
            await SeedCategory(dbContext, "Design & Creative");
            await SeedCategory(dbContext, "Engineering & Architecture");
            await SeedCategory(dbContext, "IT & Networking");
            await SeedCategory(dbContext, "Sales & Marketing");
            await SeedCategory(dbContext, "Translation");
            await SeedCategory(dbContext, "Software Development");
            await SeedCategory(dbContext, "Writing");
        }

        private static async Task SeedCategory(ApplicationDbContext db, string categoryName)
        {
            var category = db.Categories.FirstOrDefault(x => x.Name == categoryName);

            if (category == null)
            {
                db.Categories.Add(new Category
                {
                    Name = categoryName,
                });

                await db.SaveChangesAsync();
            }
        }
    }
}
