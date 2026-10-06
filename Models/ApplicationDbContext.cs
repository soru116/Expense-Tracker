using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using expense_tracker.Models;

namespace expense_tracker.Models
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        // 原有的表格
        public DbSet<Transaction> Transactions { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Budget> Budgets { get; set; }

        // ★★★ 請補上這兩行，程式才找得到 Accounts ★★★
        public DbSet<Account> Accounts { get; set; }
        public DbSet<Transfer> Transfers { get; set; }

        // ★★★ 還有這個設定，防止資料庫報錯 (循環刪除問題) ★★★
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // 當刪除帳戶時，如果該帳戶有轉帳紀錄，禁止刪除 (保護資料)
            // 這是為了避免 SQL Server 的 "Multiple Cascade Paths" 錯誤
            
            modelBuilder.Entity<Transfer>()
                .HasOne(t => t.SourceAccount)
                .WithMany()
                .HasForeignKey(t => t.SourceAccountId)
                .OnDelete(DeleteBehavior.Restrict); 

            modelBuilder.Entity<Transfer>()
                .HasOne(t => t.TargetAccount)
                .WithMany()
                .HasForeignKey(t => t.TargetAccountId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}