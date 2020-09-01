using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Models.ViewModels
{
    public class CatalogListViewModel: _LayoutViewModel
    {
        public List<Catalog> Catalogs { get; set; }
    }
}