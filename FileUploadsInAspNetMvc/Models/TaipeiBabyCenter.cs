using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace FileUploadsInAspNetMvc.Models
{
    public class TaipeiBabyCenter
    {
        [Key]
        public int _id { get; set; }

        public string ORG_Name { get; set; }

        public string Division { get; set; }
        public string Person_In_Charge { get; set; }
        public string Contact_Name { get; set; }
        public string Zone_Code { get; set; }

        public string City { get; set; }

        public string Address { get; set; }

        public string Phone { get; set; }

        public string Registered { get; set; }

        public string Create_Date { get; set; }

        public string Bedno { get; set; }

        public double Lat { get; set; }

        public double Lon { get; set; }

        public string Post_Date { get; set; }

        [NotMapped]
        public double Distance { get; set; }

    }
}