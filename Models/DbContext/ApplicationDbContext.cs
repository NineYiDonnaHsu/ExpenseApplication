using ExpenseApplication.Models;
using Microsoft.EntityFrameworkCore;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
    {
    }

    public DbSet<Expense> Expenses { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Expense>().HasData(
            new Expense { Id = 1, Title = "豆干", Amount = 30.0M, Date = new DateTime(2024, 07, 13).ToUniversalTime(), Category = "食" },
            new Expense { Id = 2, Title = "燙青菜", Amount = 30.0M, Date = new DateTime(2023, 07, 15).ToUniversalTime(), Category = "食" },
            new Expense { Id = 3, Title = "紅燒肉", Amount = 100.0M, Date = new DateTime(2024, 07, 13).ToUniversalTime(), Category = "食" },
            new Expense { Id = 4, Title = "米色小外套", Amount = 999.0M, Date = new DateTime(2024, 07, 14).ToUniversalTime(), Category = "衣" },
            new Expense { Id = 5, Title = "節瓜", Amount = 199.0M, Date = new DateTime(2024, 07, 13).ToUniversalTime(), Category = "食" },
            new Expense { Id = 6, Title = "IRent租車", Amount = 120.0M, Date = new DateTime(2024, 07, 14).ToUniversalTime(), Category = "行" },
            new Expense { Id = 7, Title = "房租", Amount = 12000.0M, Date = new DateTime(2024, 07, 14).ToUniversalTime(), Category = "住" },
            new Expense { Id = 8, Title = "電費", Amount = 1200.0M, Date = new DateTime(2024, 07, 14).ToUniversalTime(), Category = "住" },
            new Expense { Id = 9, Title = "水費", Amount = 205.0M, Date = new DateTime(2024, 07, 15).ToUniversalTime(), Category = "住" },
            new Expense { Id = 10, Title = "瓦斯費", Amount = 300.0M, Date = new DateTime(2024, 07, 15).ToUniversalTime(), Category = "住" },
            new Expense { Id = 11, Title = "網路費", Amount = 500.0M, Date = new DateTime(2024, 07, 15).ToUniversalTime(), Category = "住" }
        );
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseNpgsql("Host=localhost:5433;Database=expense_test;Username=tester;Password=123456");
    }
}
