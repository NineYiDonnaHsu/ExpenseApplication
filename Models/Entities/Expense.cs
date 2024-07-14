using System.ComponentModel.DataAnnotations;

namespace ExpenseApplication.Models
{
    /// <summary>
    /// 支出模型
    /// </summary>
    /// <remarks>一筆支出要至少包含：標題、金額、發生日日期和時間、分類，需要在每個屬性前面加上中文註解</remarks>
    /// <remarks>金額不能夠為負數</remarks>
    /// <remarks>分類只能夠是【食、衣、住、行】</remarks
    /// <remarks>發生日日期不能夠晚於 1 年前</remarks>
    public class Expense
    {
        [Key]
        public long Id { get; set; }

        /// <summary>
        /// 支出標題
        /// </summary>
        [Required(ErrorMessage = "請輸入支出標題")]
        [Display(Name = "標題")]
        public string Title { get; set; }

        /// <summary>
        /// 支出金額
        /// </summary>
        [Required(ErrorMessage = "請輸入支出金額")]
        [Range(0, double.MaxValue, ErrorMessage = "金額不能為負數")]
        [Display(Name = "金額")]
        public decimal Amount { get; set; }

        /// <summary>
        /// 支出發生日期和時間
        /// </summary>
        [Required(ErrorMessage = "請選擇支出發生日期和時間")]
        [DataType(DataType.DateTime, ErrorMessage = "請輸入有效的日期和時間")]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd HH:mm:ss}", ApplyFormatInEditMode = true)]
        [ValidDate(ErrorMessage = "發生日日期不能夠晚於 1 年前")]
        [Display(Name = "日期")]
        public DateTime Date { get; set; }

        /// <summary>
        /// 支出分類
        /// </summary>
        [Required(ErrorMessage = "請選擇支出分類")]
        [RegularExpression("^(食|衣|住|行)$", ErrorMessage = "分類只能是【食、衣、住、行】")]
        [Display(Name = "分類")]
        public string Category { get; set; }
    }

    /// <summary>
    /// 自訂驗證屬性，用於檢查日期是否晚於 1 年前
    /// </summary>
    public class ValidDateAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            DateTime date = (DateTime)value;
            if (date > DateTime.Now.AddYears(-1))
            {
                return ValidationResult.Success;
            }
            else
            {
                return new ValidationResult(ErrorMessage);
            }
        }
    }
}
