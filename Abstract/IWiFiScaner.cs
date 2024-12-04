using System.Collections.Generic;
using System.Threading.Tasks;

namespace Abstract
{
    public interface IWiFiScaner
    {
        public Task<IEnumerable<INetData>> ScanAsync();
    }
}
