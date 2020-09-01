using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Models.ViewModels
{
    public class GalleryImageListViewModel : _LayoutViewModel
    {
        public List<GalleryImage> GalleryImages { get; set; }
    }
}