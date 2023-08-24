using System.ComponentModel.DataAnnotations;

namespace ExpenseTrackingSystem.Models
{
	public class Expense
	{
		public int Id { get; set; }
        [UIHint("CurrencyFormat")]
        public float Amount { get; set; }
		public int Type { get; set; }
		public int AccountId { get; set; }
		public int CategoryId { get; set; }
        [MaxLength(500)]
        public string Description { get; set; }
		public DateTime Date { get; set; }
        [MaxLength(200)]
        public string CreatedBy { get; set; }
        public int IsDeleted { get; set; }
    }
}
