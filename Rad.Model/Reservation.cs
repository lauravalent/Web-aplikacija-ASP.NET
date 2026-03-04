using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rad.Model
{
    public class Reservation
    {
        [Key]
        public int ID { get; set; }

        [ForeignKey(nameof(AppUser))]
        public string UserId { get; set; }

        public AppUser? User { get; set; }

        [Required]
        public DateTime StartDate { get; set; }

        [Required]
        [Range(1, 100, ErrorMessage = "Broj gostiju mora biti barem 1.")]
        public int NumberOfGuests { get; set; }


        [Required]
        public DateTime EndDate { get; set; }

        [ForeignKey(nameof(Accomodation))]
        public int AccomodationID { get; set; }
        public Accomodation? Accomodation { get; set; }
    }
}
