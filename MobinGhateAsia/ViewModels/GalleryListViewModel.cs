﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Models.ViewModels
{
    public class GalleryListViewModel:_LayoutViewModel
    {
        public List<Gallery> Galleries { get; set; }
    }
}