﻿using Line.Messaging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;


namespace FileUploadsInAspNetMvc.Models
{
    public class CarouselItem
    {
        public string Title { get; set; }
        public string ImageUrl { get; set; }
        public string Text { get; set; }
        public List<ITemplateAction> Actions { get; set; }
    }
}