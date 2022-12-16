using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using MoneyPlus.Areas.Identity.Data;
using MoneyPlus.Services;
using MoneyPlus.Services.Models;

namespace MoneyPlus.Data;

public class MoneyPlusContext : IdentityDbContext<MoneyPlusUser>
{
    public MoneyPlusContext(DbContextOptions<MoneyPlusContext> options)
        : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
        // Customize the ASP.NET Identity model and override the defaults if needed.
        // For example, you can rename the ASP.NET Identity table names and more.
        // Add your customizations after calling base.OnModelCreating(builder);

    }

    public DbSet<MoneyPlus.Services.Models.Wallet> Wallet { get; set; } = default!;

    public DbSet<MoneyPlus.Services.Models.Category> Category { get; set; } = default!;

    public DbSet<MoneyPlus.Services.Models.SubCategory> SubCategory { get; set; } = default!;

    public DbSet<MoneyPlus.Services.Models.Payee> Payee { get; set; } = default!;

    public DbSet<MoneyPlus.Services.Models.Transaction> Transaction { get; set; } = default!;

    public DbSet<MoneyPlus.Services.Models.Transfer> Transfer { get; set; } = default!;

    public DbSet<MoneyPlus.Services.Models.Asset> Asset { get; set; } = default!;
	public DbSet<MoneyPlus.Services.Models.EmailLogs> EmailLogs { get; set; } = default!;

	public DbSet<MoneyPlus.Repositories.TransactionRepository.MonthlyExpensesModel> MonthlyExpensesModel { get; set; } = default!;


}
