using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace expense_tracker.Models
{
    public class Transaction
    {
        [Key]
        public int TransactionId { get; set; }

        // --- 類別 (原本就有的) ---
        [Range(1, int.MaxValue, ErrorMessage = "Please select a category.")]
        public int CategoryId { get; set; }
        public Category? Category { get; set; }

        // --- ★★★ 關鍵修正：補上這兩行，程式才找得到 AccountId ★★★ ---
        [Range(1, int.MaxValue, ErrorMessage = "Please select an account.")]
        public int AccountId { get; set; }
        public Account? Account { get; set; }
        // -----------------------------------------------------------

        [Range(1, int.MaxValue, ErrorMessage = "Amount should be greater than 0.")]
        public int Amount { get; set; }

        [Column(TypeName = "nvarchar(75)")]
        public string? Note { get; set; }

        public DateTime Date { get; set; } = DateTime.Now;

        public string? UserId { get; set; }

        // --- 顯示用的輔助屬性 ---

        [NotMapped]
        public string? CategoryTitleWithIcon
        {
            get { return Category == null ? "" : Category.Icon + " " + Category.Title; }
        }
        // ★★★ 請新增這一段 (為了讓列表顯示帳戶圖示+名稱) ★★★
        [NotMapped]
        public string? AccountTitleWithIcon
        {
            get { return Account == null ? "" : Account.Icon + " " + Account.Title; }
        }
        // ★★★ 結束 ★★★
        [NotMapped]
        public string? FormattedAmount
        {
            get { return ((Category == null || Category.Type == "Expense") ? "- " : "+ ") + Amount.ToString("C0"); }
        }
        
        // ★ 補上這個，方便前端顯示帳戶名稱
        [NotMapped]
        public string? AccountName
        {
             get { return Account == null ? "" : Account.Title; }
        }
    }
}