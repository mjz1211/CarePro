using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace FileUploadsInAspNetMvc.Models
{
    public class CaregiverDay
    {
        [Key]
        public int Id { get; set; }
        [Display(Name = "照護主題")]
        public string CareTitle { get; set; }
        [Display(Name = "照護說明")]

        public string CareText { get; set; }
        public string CareImageUrl { get; set; }
        [Display(Name = "照護時間")]

        public string CareDateTime { get; set; }

        public string CareUrl { get; set; }
    }
}