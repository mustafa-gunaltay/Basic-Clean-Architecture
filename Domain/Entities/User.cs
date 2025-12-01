using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities
{
    public class User
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public string Username { get; set; } = string.Empty;

        public string Password { get; set; } = string.Empty;
        public string? Email { get; set; }

        // Navigation Property - One User can have Many TaskItems
        public ICollection<TaskItem> TaskItems { get; set; } = new List<TaskItem>();
    }
}
