using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Abstract;
using ManagedNativeWifi;

namespace WiFiScaner
{
    public class Scaner : IWiFiScaner
    {
        public async Task<IEnumerable<INetData>> ScanAsync()
        {
            await NativeWifi.ScanNetworksAsync(TimeSpan.FromSeconds(4));
            IEnumerable<AvailableNetworkPack> listNet = NativeWifi.EnumerateAvailableNetworks();

            IEnumerable<NetData> listNetData = from AvailableNetworkPack net in listNet select
                              new NetData(net.Ssid.ToString(), net.SignalQuality);
            return listNetData;
        }
    }
}
