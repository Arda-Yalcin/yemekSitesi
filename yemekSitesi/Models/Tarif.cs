using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace yemekSitesi.Models
{
    public class Tarif
    {
        public int Id { get; set; }      
        public string? Ad { get; set; }      
        public string? Malzemeler { get; set; }
        public string? Yonerge { get; set; }
        public string? Foto { get; set; }

    }
}