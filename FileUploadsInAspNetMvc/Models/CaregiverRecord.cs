using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace FileUploadsInAspNetMvc.Models
{
    public class CaregiverRecord
    {
        [Key]
        public int Id { get; set; }
        [Display(Name = "時間")]
        public string CareDateTime { get; set; }
        [Display(Name = "活動")]
        public string CareTitle { get; set; }
        public string CareText { get; set; }
        [Display(Name = "感覺")]
        public string CareFeedback { get; set; }


    }
}