using Core.Entities.Identity;
using Microsoft.AspNetCore.Identity;

namespace Infrastructure.Identity
{
    public class AppIdentityDbContextSeed
    {
        public static async Task SeedUsersAsync(UserManager<AppUser> userManager){
            if(!userManager.Users.Any()){
                var user = new AppUser {
                    DisplayName = "Kunal",
                    Email = "Kunal@gmail.com",
                    UserName ="kunal@gmail.com",
                    Address = new Address{
                        FirstName = "Kunal",
                        LastName = "Umrajwala",
                        Street = "111",
                        City = "Bharuch",
                        State = "Gujarat",
                        ZipCode = "392001"
                    }
                };

                await userManager.CreateAsync(user, "Pa$$w0rd");
            }
        } 
    }
}