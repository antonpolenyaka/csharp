﻿using System.Runtime.InteropServices;

namespace Sally7.Protocol.Cotp
{
    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public struct Tsap
    {
        public Tsap(in byte channel, in byte position)
        {
            Channel = channel;
            Position = position;
        }

        public byte Channel;
        public byte Position;
    }
}