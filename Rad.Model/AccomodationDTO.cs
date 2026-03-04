using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vjezba.Model;

namespace Rad.Model
{
    public class AccomodationDTO
    {
        public int ID { get; set; }
        public string Name { get; set; } = "";
        public int Capacity { get; set; }
        public int Size { get; set; }
        public int? CityID { get; set; }
        public string? CityName { get; set; }
        public decimal PricePerNight { get; set; }


    }
}
