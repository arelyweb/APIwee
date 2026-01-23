using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace API_wee.Models
{
    [Table("user", Schema = "dbo")]
    public class User
    {
        [Key]
        public int Id_user { get; set; }
        
        public string? NameUser { get; set; }
        public string? LastNameUser { get; set; }
        public short Id_rol { get; set; }
        public string? PassUser { get; set; }

    }
}
