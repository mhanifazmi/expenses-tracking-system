using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using ExpenseTrackingSystem.Models;

namespace ExpenseTrackingSystem.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        public DbSet<ExpenseTrackingSystem.Models.Category>? Category { get; set; }
        public DbSet<ExpenseTrackingSystem.Models.Account>? Account { get; set; }
        public DbSet<ExpenseTrackingSystem.Models.Expense>? Expense { get; set; }
    }
}