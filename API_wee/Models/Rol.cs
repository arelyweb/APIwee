using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace API_wee.Models
{
    [Table("rol", Schema = "dbo")]
    public class Rol
    {
        [Key]
        public int Id_rol { get; set; }

        [Column(TypeName = "VARCHAR(50)")]
        public string? Description{ get; set; }
    }
}
