using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using SagaLib;
using System.Text.RegularExpressions;

namespace SagaProxy
{
    public partial class ScriptBuilder : Singleton<ScriptBuilder>
    {
        public List<Packet> packets;
        StreamWriter sw;
        string path;
        StringBuilder headerbuf, contentbuf, footerbuf;
        uint eventID  , npcID;
        byte x , y ;

        public ScriptBuilder()
        {
            packets = new List<Packet>();
            headerbuf = new StringBuilder();
            contentbuf = new StringBuilder();
            footerbuf = new StringBuilder();
        }
        public void Import(List<PacketInfo> Pi)
        {
            foreach (PacketInfo pi in Pi)
            {
                var p = new Packet();
                string buf = pi.Data.Replace(" ", "");
                buf = buf.Replace("\r", "");
                buf = buf.Replace("\r\n", "");
                p.data = SagaLib.Conversions.HexStr2Bytes(buf);
                this.packets.Add(p);
            }
        }

        public void Export()
        {
            this.eventID = 0;
            this.npcID = 0;
            this.x = 0;
            this.y = 0;
            contentbuf.Clear();
            FormatContent();
            FormatHeader();
            FormatFooter();
            if (this.eventID==0)

                sw = new StreamWriter($"S{this.npcID}" + DateTime.Now.ToString("yyMMdd-hhmmss") + ".cs");
            else
                sw = new StreamWriter($"S{this.eventID}" + DateTime.Now.ToString("yyMMdd-hhmmss") + ".cs");
            sw.Write(headerbuf);
            sw.Write(contentbuf);
            sw.Write(footerbuf);
            sw.Flush();
            sw.Close();
        }

        void FormatContent()
        {
            foreach(Packet p in packets)
            {
                switch (p.GetShort(0))
                {
                    case (0x03F7):
                        FormatNPCMessageStart(p);
                        break;
                    case (0x03F9):
                        FormatNPCMessage(p);
                        break;
                    case (0x03FA):
                        FormatNPCMessageEnd(p);
                        break;
                    case (0x05DC):
                        FormatEventStart(p);
                        break;
                    case (0x05DD):
                        FormatEventEnd(p);
                        break;
                    case (0x05E6):
                        FormatEventID(p);
                        break;
                    case (0x05E7):
                        FormatNPCID(p);
                        break;
                    case (0x05E4):
                        FormatNPCEvent(p);
                        break;
                    case (0x05E9):
                        FormatNPCMove(p);
                        break;
                    case (0x05EB):
                        FormatNPCWait(p);
                        break;
                    case (0x05F2):
                        FormatChangeBGM(p);
                        break;
                    case (0x05F4):
                        FormatInputBox(p);
                        break;
                    case (0x05F5):
                        FormatInputBoxResult(p);
                        break;
                    case (0x05F7):
                        FormatSelectResult(p);
                        break;
                    case (0x0600):
                        FormatNPCShowEffect(p);
                        break;
                    case (0x0615):
                        FormatNPCHidePlayers(p);
                        break;
                }
            }
        }
    }
}