using BookStore.DAL.Entities;
using BookStore.DAL.Enums;
using BookStore.DAL.Persistence;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;

namespace BookStore.WebApi.Extensions
{
    public static class SeedingLocalExtensions
    {
        public async static Task SeedDatabaseAsync(this WebApplication app)
        {
            using (var scope = app.Services.CreateScope())
            {      
                //TestDeletion(scope.ServiceProvider);    

                await SeedIdentity(scope.ServiceProvider);

                await SeedDataAsync(scope.ServiceProvider);
            }
        }

        private static void TestDeletion(IServiceProvider serviceProvider)
        {
            var db = serviceProvider.GetService<BookStoreDbContext>();

            var a = db.Authors.FirstOrDefault(x => x.Id == 1);

            db.Authors.Remove(a);

            db.SaveChanges();
        }

        private async static Task SeedDataAsync(IServiceProvider serviceProvider)
        {
            var db = serviceProvider.GetService<BookStoreDbContext>();

            if (!db.Generes.Any())
            {
                await db.Generes.AddAsync
                    (
                        new Genre()
                        { 
                            Name = "Scientific Literature"                            
                        }
                    );

                await db.SaveChangesAsync();
            }

            if (!db!.Authors.Any())
            {
                var genre = db.Generes.FirstOrDefault(x =>x.Name.Equals("Scientific Literature"));

                var Books = new List<Book>()
                {
                    new Book()
                        {
                            Name="CLR via C#",
                            PubYear=2013
                        },
                        new Book()
                        {
                            Name = "Windows Runtime via C#",
                            PubYear = 2014
                        },
                        new Book()
                        {
                            Name = "Windows via C/C++",
                            PubYear = 2009
                        }
                };

                var a = new Author()
                {
                    Name = "Jeffrey",
                    Surename = "Richter",
                    BirthDate = new DateOnly(1964, 7, 27)
                };

                foreach (var book in Books)
                {
                    book.Book_Genres.Add(new Book_Genre() { Book = book, Genre = genre });
                    
                    a.Book_Authors.Add(new Book_Author() { Author = a, Book = book });

                    await db.Authors.AddAsync(a);
                } 

                await db.SaveChangesAsync();
            }
        }

        private async static Task SeedIdentity(IServiceProvider serviceProvider)
        {
            // Constants for roles            
            const string adminRoleId = "563b4777-0615-4c3c-8a7d-8858412b6562";
            const string userRoleId = "12444183-0753-495b-a34f-c5a622d8fc6d";

            // Constants for Admin
            const string adminUserName = "SuperAdmin";
            const string adminUserEmail = "SuperAdmin@test.com";
            const string adminId = "4eb10d27-a950-45ef-9ebe-f730a07ce5e9";
            const string adminPass = "*Superuser18";
            BookStoreDbContext? context = serviceProvider.GetService<BookStoreDbContext>();
            
            if(context == null)
                throw new Exception("Fail to get DbContext!");

            RoleManager<IdentityRole<Guid>> rm = serviceProvider.GetService<RoleManager<IdentityRole<Guid>>>();

            UserManager<User> um = serviceProvider.GetService<UserManager<User>>();

            try
            {
                // Seed Roles
                if (!context.Roles.Any())
                {
                    await rm.CreateAsync(
                        new IdentityRole<Guid>(UserRole.Admin.ToString())
                        {
                            Id = Guid.Parse(adminRoleId)
                        });

                    await rm.CreateAsync(
                        new IdentityRole<Guid>(UserRole.User.ToString())
                        {
                            Id = Guid.Parse(userRoleId)
                        });
                }

                // Seed Admin
                if (!context.Users.Any())
                {
                    var r = await um.CreateAsync(
                        new User()
                        {
                            UserName = adminUserName,
                            Email = adminUserEmail,
                            Id = Guid.Parse(adminId),
                            EmailConfirmed = true,
                            Address = "Some address",
                            BirthDate = new DateOnly(2002, 9, 12)

                        }, adminPass);
                }

                if (!context.UserRoles.Any())
                {
                    await um.AddToRoleAsync(context.Users.First(u => u.Id.Equals(Guid.Parse(adminId))),
                        context.Roles.First(r => r.Id.Equals(Guid.Parse(adminRoleId))).Name);
                }
            }
            catch (Exception ex)
            {                
                throw;
            }
        }
    }
}
