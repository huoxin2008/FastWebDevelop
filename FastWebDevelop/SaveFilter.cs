using Hprose.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace FastWebDevelop
{
    public class SaveFilter : IHproseFilter
    {
        public MemoryStream InputFilter(MemoryStream inStream, HproseContext context)
        {
            throw new NotImplementedException();
        }

        public MemoryStream OutputFilter(MemoryStream outStream, HproseContext context)
        {
            throw new NotImplementedException();
        }
    }
}
