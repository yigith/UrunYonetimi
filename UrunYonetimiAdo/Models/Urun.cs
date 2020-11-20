using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UrunYonetimiAdo.Models
{
    public class Urun
    {
        public int Id { get; set; }
        public string UrunAd { get; set; }
        public decimal BirimFiyat { get; set; }
    }
}
