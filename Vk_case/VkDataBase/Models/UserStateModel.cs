using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace VkDataBase.Models
{
    public enum UserStateCode
    {
        Active,
        Blocked
    }
    public class UserState
    {
        [Key]
        [ForeignKey("User")]
        public int Id { get; set; }
        public UserStateCode Code { get; set; }
        public string Description { get; set; }
        public User User { get; set; }
    }
}
