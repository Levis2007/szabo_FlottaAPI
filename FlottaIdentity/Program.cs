using szabo_FlottaAPI.Models;
using FlottaIdentity;
using Microsoft.EntityFrameworkCore;

namespace FlottaIdentity
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            builder.Services.AddIdentityServer()
            .AddInMemoryIdentityResources(Config.IdentityResources)
            .AddInMemoryApiScopes(Config.ApiScopes)
            .AddInMemoryClients(Config.Clients)
            .AddResourceOwnerValidator<FlottaPasswordValidator>() // <--- Saját Argon2 ellenõrzés
            .AddProfileService<FlottaProfileService>()             // <--- Saját claim-ek betöltése
            .AddDeveloperSigningCredential();


            var app = builder.Build();

            app.UseIdentityServer();
            app.MapGet("/", () => "Hello World!");

            app.Run();
        }
    }
}
