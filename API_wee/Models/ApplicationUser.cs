using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace API_wee.Models
{
    [Table("user")]
    public class ApplicationUser
    {
        [Key]
        [Column("id_user")]
        public int IdUser { get; set; }

        [Column("id_rol")]
        public short RoleId { get; set; }

        [Column("nameuser")]
        public string UserName { get; set; } = null!;

        [Column("lastnameuser")]
        public string LastName { get; set; } = null!;

        [Column("passuser")]
        public string PasswordHash { get; set; } = null!; // aquí guardaremos el hash
    }
}
