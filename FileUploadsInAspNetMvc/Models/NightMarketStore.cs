using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace FileUploadsInAspNetMvc.Models
{
    public class NightMarketStore
    {
        [Key]
        public int Id { get; set; }
        [Display (Name ="夜市名稱")]
        public string NightMarketName { get; set; }
        [Display(Name = "商家名稱")]
        public string Name { get; set; }
        [Display(Name = "地址")]
        public string Address { get; set; }
        [Display(Name = "說明")]
        public string Info { get; set; }
        [Display(Name = "獲獎")]
        public string Award { get; set; }
        public string Memo { get; set; }
        [Display(Name = "圖片")]
        public string ImageUrl { get; set; }
        [Display(Name = "優惠網址")]
        public string DiscountUrl { get; set; }
        [Display(Name = "分類")]
        public string Tag { get; set; }
        [Display(Name = "經度")]
        public string Lat { get; set; }
        [Display(Name = "緯度")]
        public string Lng { get; set; }
    }
}