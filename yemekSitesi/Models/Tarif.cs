using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace yemekSitesi.Models
{
    public class Tarif
    {
        public int Id { get; set; }      
        [Required(ErrorMessage ="boş geçme")]
        [Display(Name ="Yemek Adı")]
        public string Ad { get; set; }      
        [Required(ErrorMessage ="boş geçme")]
        [Display(Name ="Malzeme")]
        public string Malzemeler { get; set; }
        [Required(ErrorMessage ="boş geçme")]
        [Display(Name ="Tarifi")]
        public string Yonerge { get; set; }
        [Display(Name ="Fotoğraf")]
        public string? Foto { get; set; }

    }
}