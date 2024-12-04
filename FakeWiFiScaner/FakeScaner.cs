using System.Collections.Generic;
using System.Threading.Tasks;
using Abstract;

namespace FakeWiFiScaner
{
    public class FakeScaner : IWiFiScaner
    {
        public async Task<IEnumerable<INetData>> ScanAsync()
        {
            List<NetData> nets = new List<NetData>()
            {
                new NetData("Net01", 1),
                new NetData("Net02", 2)
            };
            await Task.Delay(2000);
            return nets;
        }
    }
}
