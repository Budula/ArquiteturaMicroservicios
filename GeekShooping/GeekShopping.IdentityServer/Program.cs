using GeekShopping.IdentityServer.Configuration;
using GeekShopping.IdentityServer.Initializer;
using GeekShopping.IdentityServer.Model;
using GeekShopping.IdentityServer.Model.Context;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

var connection = builder.Configuration.GetConnectionString("MySQlConnectionString");
builder.Services.AddDbContext<MySQLContext>(options => options.
                            UseMySql(connection, new MySqlServerVersion(new Version(8, 0, 33))));
builder.Services.AddIdentity<ApplicationUser, IdentityRole>()
    .AddEntityFrameworkStores<MySQLContext>()
    .AddDefaultTokenProviders();


var builderServices = builder.Services.AddIdentityServer(options =>
{
    options.Events.RaiseErrorEvents = true;
    options.Events.RaiseInformationEvents = true;
    options.Events.RaiseFailureEvents = true;
    options.Events.RaiseSuccessEvents = true;
    options.EmitStaticAudienceClaim = true;
}).AddInMemoryIdentityResources(
        IdentityConfiguration.IdentityResources)
        .AddInMemoryApiScopes(IdentityConfiguration.ApiScopes)
        .AddInMemoryClients(IdentityConfiguration.Clients)
        .AddAspNetIdentity<ApplicationUser>();


builder.Services.AddScoped<IDbInitializer, DbInitializer>();

builderServices.AddDeveloperSigningCredential();
// Add services to the container.
builder.Services.AddRazorPages();

var app = builder.Build();

var initializer = app.Services.CreateScope().ServiceProvider.GetRequiredService<IDbInitializer>();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
}
app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseIdentityServer();

app.UseAuthorization();
initializer.Initialize();
app.MapRazorPages();

app.Run();
