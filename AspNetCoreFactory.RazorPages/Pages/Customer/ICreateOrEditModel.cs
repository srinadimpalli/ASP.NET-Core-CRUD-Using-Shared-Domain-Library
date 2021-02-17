using SharedDomain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AspNetCoreFactory.RazorPages
{
    public interface ICreateOrEditModel
    {
        Customer Customer { get; set; }
    }
}
