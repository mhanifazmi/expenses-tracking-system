using System.ComponentModel.DataAnnotations;

namespace ExpenseTrackingSystem.Models
{
    public class Category
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int IsLocked { get; set; }
        public string CreatedBy { get; set; }
        public int IsDeleted { get; set; }
    }
}
