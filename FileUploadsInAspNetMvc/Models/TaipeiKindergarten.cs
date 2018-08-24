using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace FileUploadsInAspNetMvc.Models
{
    public class TaipeiKindergarten
    {
        [Key]
        public string _id { get; set; }

        public string Name { get; set; }

        public string PublicOrPrivate { get; set; }

        public string City { get; set; }

        public string Region { get; set; }

        public string Address { get; set; }
        
        public string Tel { get; set; }

        public double Lat { get; set; }

        public double Lng { get; set; }

        [Display(Name = "學費/註冊費")]
        public int TuitionFee { get; set; }

        [Display(Name = "雜費")]
        public int MiscFee { get; set; }

        [Display(Name = "月費")]
        public int MonthFee { get; set; }

        [NotMapped]
        public double Distance { get; set; }
    }
}