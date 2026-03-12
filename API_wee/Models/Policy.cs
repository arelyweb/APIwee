using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
namespace API_wee.Models
{
    [Table("policy", Schema = "dbo")]
    public class Policy
    {
        [Key]
        public int Id_policy { get; set; }
        public short Id_client { get; set; }
        public short Id_typePolicy { get; set; }
        public short id_statusPolicy { get; set; }
        public string numPolicy { get; set; } = string.Empty;
        public DateTime startDatePolicy { get; set; } 
        public DateTime endDatePolicy { get; set; }
        public double amountPolicy { get; set; }
    }
}
