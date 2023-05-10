using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace VkDataBase.Models
{
    public enum UserGroupCode
    {
        User,
        Admin
    }
    public class UserGroup
    {
        [Key]
        [ForeignKey("User")]
        public int Id { get; set; }
        public UserGroupCode Code { get; set; }
        public string Description { get; set; }
        public User User { get; set; }
    }
}
