using Microsoft.AspNetCore.Mvc.ModelBinding;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rad.Model
{
    public class Review
    {
        [Key]
        public int ID { get; set; }
        [BindNever]

        [ForeignKey(nameof(AppUser))]
        public string UserId { get; set; }
        public AppUser? User { get; set; }

        [Required]
        public string Content { get; set; }

        [Required]
        public string Title { get; set; }

        [Required]
        [Range(1, 5)]
        public int Rating { get; set; }

        [ForeignKey(nameof(Accomodation))]
        public int AccomodationID { get; set; }
        public Accomodation? Accomodation { get; set; }
    }
}
