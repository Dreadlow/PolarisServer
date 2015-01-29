﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using PolarisServer.Packets;

namespace PolarisServer.Models
{
    public class PSOObject
    {
        public struct PSOObjectThing
        {
            public UInt32 data;
        }

        public EntityHeader Header { get; set; }
        public MysteryPositions Position { get; set; }
        public string Name { get; set; }
        public UInt32 ThingFlag { get; set; }
        public PSOObjectThing[] things { get; set; }

        public byte[] GenerateSpawnBlob()
        {
            PacketWriter writer = new PacketWriter();
            writer.WriteStruct(Header);
            writer.Write(Position);
            writer.Seek(2, SeekOrigin.Current); // Padding I guess...
            writer.WriteFixedLengthASCII(Name, 0x34);
            writer.Write(ThingFlag);
            writer.Write(things.Length);
            foreach (PSOObjectThing thing in things)
            {
                writer.WriteStruct(thing);
            }

            return writer.ToArray();

        }
    }
}