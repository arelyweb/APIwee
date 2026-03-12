using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace API_wee.Models
{
    [Table("client")]
    public class Client
    {
            [Key]
            [Column("id_client")]
            public int IdClient { get; set; }

            [Column("nameClient")]
            public string name { get; set; } = string.Empty;

            [Column("lastNameClient")]
            public string lastName { get; set; } = null!;

            [Column("lastNameClientBis")]
            public string LastNameM { get; set; } = null!;

            [Column("ageClient")]
            public string age { get; set; } = null!;

            [Column("countryClient")]
            public string country { get; set; } = null!;

            [Column("genreClient")]
            public string genre { get; set; } = null!;

            [Column("emailClient")]
            public string email { get; set; } = null!;

            [Column("phoneClient")]
            public string phone { get; set; } = null!;

        
    }
}
