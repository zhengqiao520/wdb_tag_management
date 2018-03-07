using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Entity
{
    public class BookTopicalCheckItem : BooktopicalMappings
    {
        public bool isChecked { get; set; } = false;
    }
}
