using System.ComponentModel.DataAnnotations;

namespace ExpenseTrackingSystem.Models
{
    public class Account
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string CreatedBy { get; set; }
        public int IsDeleted { get; set; }
    }
}
