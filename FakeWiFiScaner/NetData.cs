﻿using Abstract;

namespace FakeWiFiScaner
{
    public class NetData : INetData
    {
        public NetData(string name, int strength)
        {
            Name = name;
            Strength = strength;
        }

        public string Name { get; }

        public int Strength { get; }
    }
}
