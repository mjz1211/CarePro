using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;


namespace FileUploadsInAspNetMvc.Models
{
    public class CareKidUser
    {
        [Key]
        public int _id { get; set; }


        public string PSId { get; set; }

        [Display(Name = "地址")]
        public string Address { get; set; }

        [Display(Name = "懷孕日期")]
        public DateTime Pregnancyday { get; set; }

        [Display(Name = "寶寶出生日期")]
        public DateTime BabyBirthday { get; set; }


        public double Lat { get; set; }

        public double Lng { get; set; }
    }
}