using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations.Schema;

namespace VkDataBase.Models
{
    public class User
    {
        public int Id { get; set; }

        [Required]
        public string Login { get; set; }
        public string Password { get; set; }

        [Column(TypeName = "Date")]
        public DateTime CreatedDate { get; set; }

        [Required]
        public UserGroup Group { get; set; }

        [Required]
        public UserState State { get; set; }

    }
}