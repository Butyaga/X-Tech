using System.Collections.Generic;

namespace Abstract
{
    public interface IPDFWriter
    {
        public void Write(string title, IEnumerable<IRowData> tabledata);
    }
}
