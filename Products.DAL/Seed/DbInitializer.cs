using System;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Products.DAL.Entity;

namespace Products.DAL.Seed
{
    public class DbInitializer : IDbInitializer
    {
        private readonly ProductsDbContext _dbContext;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly string _defaultPassword;

        public DbInitializer(ProductsDbContext dbContext,
            UserManager<IdentityUser> userManager,
            RoleManager<IdentityRole> roleManager)
        {
            _dbContext = dbContext;
            _userManager = userManager;
            _roleManager = roleManager;
            _defaultPassword = "SW6_SB_!23";
        }

        public void SeedDatabase()
        {
            if (_dbContext.Database.GetPendingMigrations().Count() > 0)
            {
                _dbContext.Database.Migrate();
            }


            #region users_and_roles
            if (!_roleManager.RoleExistsAsync("ADMIN").GetAwaiter().GetResult())
            {
                _roleManager.CreateAsync(new IdentityRole("ADMIN")).GetAwaiter().GetResult();

                _dbContext.SaveChanges();
            }

            if (_userManager.FindByEmailAsync("adam23stevens@gmail.com").GetAwaiter().GetResult() == null)
            {
                var newUser = new IdentityUser()
                {
                    UserName = "Adam.Stevens",
                    Email = "adam23stevens@gmail.com",
                    EmailConfirmed = true,
                    PhoneNumber = "07854385800"
                };

                var thing = _userManager.CreateAsync(newUser, _defaultPassword).GetAwaiter().GetResult();

                _dbContext.SaveChanges();
               
                _userManager.AddToRoleAsync(newUser, "ADMIN").GetAwaiter().GetResult();
            }
            #endregion

            #region Entities
            if (!_dbContext.Colours.Any())
            {
                _dbContext.Colours.Add(new Colour()
                {
                    Name = "Blue",
                    HexValue = "#0000FF"
                });
                _dbContext.Colours.Add(new Colour()
                {
                    Name = "Red",
                    HexValue = "#FF0000"
                });
                _dbContext.Colours.Add(new Colour()
                {
                    Name = "Green",
                    HexValue = "#00FF00"
                });

                _dbContext.SaveChanges();
            }

            if (!_dbContext.Products.Any())
            {
                _dbContext.Products.Add(new Product()
                {
                    Name = "Lamp",
                    Colour = _dbContext.Colours.First(x => x.Name == "Blue"),
                });
                _dbContext.Products.Add(new Product()
                {
                    Name = "Guitar",
                    Colour = _dbContext.Colours.First(x => x.Name == "Blue")
                });
                _dbContext.Products.Add(new Product()
                {
                    Name = "Cup",
                    Colour = _dbContext.Colours.First(x => x.Name == "Red")
                });
                _dbContext.Products.Add(new Product()
                {
                    Name = "Book",
                    Colour = _dbContext.Colours.First(x => x.Name == "Red")
                });
                _dbContext.Products.Add(new Product()
                {
                    Name = "Plant",
                    Colour = _dbContext.Colours.First(x => x.Name == "Green")
                });
                _dbContext.Products.Add(new Product()
                {
                    Name = "Sofa",
                    Colour = _dbContext.Colours.First(x => x.Name == "Green")
                });

                _dbContext.SaveChanges();
            }
            #endregion
        }
    }
}

