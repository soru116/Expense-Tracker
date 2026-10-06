using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Globalization; // ★ 1. 這裡一定要有，不然會報錯

namespace expense_tracker.Models
{
    public class Budget
    {
        [Key]
        public int BudgetId { get; set; }

        public int CategoryId { get; set; }
        public Category? Category { get; set; }

        public int Amount { get; set; }

        public DateTime BudgetMonth { get; set; }

        public string? UserId { get; set; }

        // --- ★★★ 2. 這是新增的，一定要加！ ★★★ ---

        [NotMapped]
        public string? CategoryTitleWithIcon
        {
            get
            {
                return Category == null ? "" : Category.Icon + " " + Category.Title;
            }
        }

        [NotMapped]
        public string? BudgetMonthGroup
        {
            get
            {
                // 這會產生 "Dec 2025" 這樣的英文格式
                return BudgetMonth.ToString("MMM yyyy", new CultureInfo("en-US"));
            }
        }
    }
}