using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using SagaLib;

using SagaMap.Network.Client;

namespace SagaMap.PacketLogger
{
    /// <summary>
    /// 用于出错，记录问题玩家的网络通讯，以便找出复制等bug
    /// </summary>
    public class PacketLogger
    {
        MapClient client;
        System.IO.FileStream fs;
        System.IO.BinaryWriter bw;

        public PacketLogger(MapClient client)
        {
            this.client = client;
            client.netIO.OnReceivePacket += OnReceivePacket;
            client.netIO.OnSendPacket += OnSendPacket;
            if (!System.IO.Directory.Exists("Log/PacketLog"))
                System.IO.Directory.CreateDirectory("Log/PacketLog");
            DateTime now = DateTime.Now;
            fs = new System.IO.FileStream(string.Format("Log/PacketLog/{0}({1})_{2}-{3}-{4} {5}-{6}-{7}.dat",
                client.Character.Name, client.Character.Account.Name, now.Year, now.Month, now.Day, now.Hour, now.Minute, now.Second),
                 System.IO.FileMode.Create);
            bw = new System.IO.BinaryWriter(fs);
        }

        public void Dispose()
        {
            client.netIO.OnReceivePacket -= OnReceivePacket;
            client.netIO.OnSendPacket -= OnSendPacket;
            fs.Flush();
            fs.Close();
        }

        void OnReceivePacket(Packet p)
        {
            lock (fs)
            {
                long ori = bw.BaseStream.Position;
                try
                {
                    if (p.ID == 0x11F8 || p.ID == 0x11F9 || p.ID == 0x0FA5 || p.ID == 0x0FA6)
                        return;
                    bw.Write((byte)0);//client message
                    bw.Write(DateTime.Now.ToBinary());
                    bw.Write(p.ID);
                    byte[] packetName = Encoding.UTF8.GetBytes(p.ToString().Replace("SagaMap.Packets.Client.", ""));
                    bw.Write((short)packetName.Length);
                    bw.Write(packetName);
                    System.Reflection.PropertyInfo[] properties = p.GetType().GetProperties();
                    bw.Write((short)(properties.Length - 1));
                    foreach (System.Reflection.PropertyInfo i in properties)
                    {
                        if (i.Name == "ID")
                            continue;
                        byte[] name = Encoding.UTF8.GetBytes(i.Name);
                        bw.Write((short)name.Length);
                        bw.Write(name);
                        byte[] value = Encoding.UTF8.GetBytes(i.GetGetMethod().Invoke(p, null).ToString());
                        bw.Write((short)value.Length);
                        bw.Write(value);
                    }
                    bool needInventory = ifNeedInventory(p);
                    bw.Write(needInventory);
                    if (needInventory)
                    {
                        byte[] inventory = client.Character.Inventory.ToBytes();
                        bw.Write((short)inventory.Length);
                        bw.Write(inventory);
                    }
                }
                catch
                {
                    bw.BaseStream.Position = ori;
                }
            }
        }

        void OnSendPacket(Packet p)
        {
            lock (fs)
            {
                long ori = bw.BaseStream.Position;
                try
                {
                    if (p.ID == 0x11F8 || p.ID == 0x11F9 || p.ID == 0x0FA5 || p.ID == 0x0FA6)
                        return;
                    bw.Write((byte)1);//client message
                    bw.Write(DateTime.Now.ToBinary());
                    byte[] packetName = Encoding.UTF8.GetBytes(p.ToString().Replace("SagaMap.Packets.Server.", ""));
                    bw.Write((short)packetName.Length);
                    bw.Write(packetName);
                    bw.Write((short)p.data.Length);
                    bw.Write(p.data);
                    bool needInventory = ifNeedInventory(p);
                    bw.Write(needInventory);
                    if (needInventory)
                    {
                        byte[] inventory = client.Character.Inventory.ToBytes();
                        bw.Write((short)inventory.Length);
                        bw.Write(inventory);
                    }
                }
                catch
                {
                    bw.BaseStream.Position = ori;
                }
            }
        }

        bool ifNeedInventory(Packet p)
        {
            switch (p.ID)
            {
                case 0x07D0:
                case 0x07E4:
                case 0x09C4:
                case 0x09E2:
                case 0x09E7:
                case 0x0A14:
                    return true;
                default:
                    return false;
            }
        }
    }
}
