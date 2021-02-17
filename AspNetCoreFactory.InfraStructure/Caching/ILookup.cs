using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Text;


namespace AspNetCoreFactory.InfraStructure.Caching
{
    public interface ILookup
    {
        List<SelectListItem> CustomerItems { get; }
        List<SelectListItem> ProductItems { get; }
    }
}
