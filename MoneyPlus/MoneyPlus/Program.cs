using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using MoneyPlus.Areas.Identity.Data;
using MoneyPlus.Data;
using MoneyPlus.Repositories;
using MoneyPlus.Services;
using MoneyPlus.Services.Interfaces;
using MoneyPlus.Services.Models;

var builder = WebApplication.CreateBuilder(args);
var connectionString = builder.Configuration.GetConnectionString("MoneyPlusContextConnection") ?? throw new InvalidOperationException("Connection string 'MoneyPlusContextConnection' not found.");

builder.Services.AddDbContext<MoneyPlusContext>(options => options.UseSqlServer(connectionString));

/*
builder.Services.AddDefaultIdentity<MoneyPlusUser>(options => options.SignIn.RequireConfirmedAccount = true).AddEntityFrameworkStores<MoneyPlusContext>();
.AddRoles<IdentityRole>();*/


builder.Services.AddDefaultIdentity<MoneyPlusUser>(
    options => options.SignIn.RequireConfirmedAccount = false)
    .AddRoles<IdentityRole>()
    .AddEntityFrameworkStores<MoneyPlusContext>();

// Add services to the container.
builder.Services.AddRazorPages();


builder.Services.AddScoped<IWallet, Wallet>();
builder.Services.AddScoped<TransactionRepository>();
builder.Services.AddHostedService<EmailBackgroundService>();



var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapRazorPages();



using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;

    var context = services.GetRequiredService<MoneyPlusContext>();

    context.Database.Migrate();

    var roleMgr = services.GetRequiredService<RoleManager<IdentityRole>>();

    CreateRoles.Initialize(context, roleMgr).Wait();
}

app.Run();
