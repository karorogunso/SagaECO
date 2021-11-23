using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using SagaLib;

namespace SagaProxy
{
    public partial class ScriptBuilder : Singleton<ScriptBuilder>
    {
        void FormatNPCID(Packet p)
        {
            this.npcID = p.GetUInt(2);
            this.contentbuf.AppendLine($"//NPC ID: {this.npcID} , Coordinate {this.x},{this.y}");
        }

        void FormatEventID(Packet p)
        {
            this.eventID = p.GetUInt(2);
            this.x = p.GetByte(6);
            this.y = p.GetByte(7);
        }

        void FormatEventStart(Packet p)
        {
            this.contentbuf.AppendLine("//Event started");
        }

        void FormatEventEnd(Packet p)
        {
            this.contentbuf.AppendLine("//Event ended");
        }

        void FormatNPCMessageStart(Packet p)
        {
            //this.contentbuf.AppendLine("//Message started");
        }

        void FormatNPCMessageEnd(Packet p)
        {
            //this.contentbuf.AppendLine("//Message ended");
        }

        void FormatNPCHidePlayers(Packet p)
        {
            if (p.GetByte(10)==0x01)
                 this.contentbuf.AppendLine("HidePlayer(pc);");
        }

        void FormatNPCMessage(Packet p)
        {
            string message = string.Empty, title=string.Empty;
            byte size;
            uint NPCid= p.GetUInt(2);
            int count = p.GetByte(8);
            ushort offset = 9;
            for (int i=0;i<count;i++)
            {
                size = p.GetByte(offset);
                if (i > 0)
                    message += " + " +Environment.NewLine;
                message += '\"';
                message += Global.Unicode.GetString(p.GetBytes(size,++offset));
                message += '\"';
                offset += size;
            }
            ushort motion = p.GetUShort(offset);
            offset += 2;
            size = p.GetByte(offset);
            title = Global.Unicode.GetString(p.GetBytes((ushort)(size-1), ++offset));

            this.contentbuf.AppendLine($"Say(pc, {NPCid}, {motion},{message}" + ',' + '\"' + $"{title}"+'\"' + @");");
        }

        void FormatNPCMove(Packet p)
        {
            uint npcID = p.GetUInt(2);
            byte x = p.GetByte(6);
            byte y = p.GetByte(7);
            ushort speed = p.GetUShort(8);
            byte dir = p.GetByte(10);
            ushort showtype = p.GetUShort(11);
            ushort motion = p.GetUShort(13);
            ushort motionspeed = p.GetUShort(15);
            byte type = p.GetByte(17);
            this.contentbuf.AppendLine($"NPCMove(pc,{npcID},{x},{y},{speed},{dir},{showtype},{motion},{motionspeed},{type})"+@";");
        }

        void FormatNPCSelect(Packet p)
        {
            byte size = p.GetByte(2);
            string title = Global.Unicode.GetString(p.GetBytes(size, 3));
            ushort offset = (ushort)(3 + size +1);
            byte optionsCount = p.GetByte(offset);
            offset++;
            List<string> options = new List<string>();
            for (int i=0;i<optionsCount;i++)
            {
                size = p.GetByte(offset);
                options.Add(Global.Unicode.GetString(p.GetBytes(size, ++offset)).Replace("\0", ""));
                offset += size;
            }
            size = p.GetByte(offset);
            string confirm=Global.Unicode.GetString(p.GetBytes(size,++offset));
            offset += size;
            bool cancancel =false;
            if (p.GetByte(offset) == 1)
                cancancel = true;
            this.contentbuf.AppendLine($"switch (Select(pc," + '\"' + $"{title}" + '\"' + "," + '\"' + $"{confirm}" + '\"' + $",{cancancel.ToString().ToLower()},");
            for (int k = 0;k<options.Count;k++)
            {
                this.contentbuf.Append('\"' + $"{options[k]}" + '\"' + ",");
                if (k != options.Count - 1)
                    this.contentbuf.Append(",");
            }
            this.contentbuf.AppendLine(")" + Environment.NewLine+ "{");
            for (int h = 0; h < options.Count; h++)
            {
                this.contentbuf.AppendLine($"Case{h + 1}:");
                this.contentbuf.AppendLine( "break;");
            }
            this.contentbuf.AppendLine( "}");
        }

        void FormatSelectResult(Packet p)
        {
            byte result = p.GetByte(2);
            this.contentbuf.AppendLine($"//Choosed {result}, case{result+1}" + @";");
        }

        void FormatNPCEvent(Packet p)
        {
            this.eventID = p.GetUInt(2);
        }

        void FormatNPCWait(Packet p)
        {
            uint duration = p.GetUInt(2);
            this.contentbuf.AppendLine($"Wait(pc,{duration})" + @";");
        }

        void FormatChangeBGM(Packet p)
        {
            uint soundID = p.GetUInt(2);
            bool loop;
            if (p.GetByte(6) == 0)
                loop = false;
            else
                loop = true;
            uint volume = p.GetUInt(8);
            byte balance = p.GetByte(12);
            this.contentbuf.AppendLine($"ChangeBGM(pc,{soundID},{loop.ToString().ToLower()},{volume},{balance})" + @";");
        }

        void FormatInputBox(Packet p)
        {
            byte size = p.GetByte(2);
            string title = Global.Unicode.GetString(p.GetBytes(size,3));
            string inputType = string.Empty;
            int type = p.GetInt((ushort)(3 + size));
            if (type == 2)
                inputType = "InputType.Bank";
            else if (type == 3)
                inputType = "InputType.ItemCode";
            else if (type == 4)
                inputType = "InputType.PetRename";
            else
                inputType = type.ToString();
            this.contentbuf.AppendLine($"InputBox(pc,{title},{inputType})" + @";");
        }

        void FormatInputBoxResult(Packet p)
        {
            byte size = p.GetByte(2);
            string result = Global.Unicode.GetString(p.GetBytes(size, 3)).Replace("\0", "");
            this.contentbuf.AppendLine($"//Choosed {result}, case{result + 1}" + @";");
        }

        void FormatNPCShowEffect(Packet p)
        {
            uint actorID = p.GetUInt(2);
            uint effectID = p.GetUInt(6);
            byte x = p.GetByte(14);
            byte y = p.GetByte(15);
            ushort height = p.GetUShort(16);
            bool isOneTime = false;
            if (p.GetByte(24) == 1)
                isOneTime = true;
            if (actorID!=0xFFFFFFFF)
                this.contentbuf.AppendLine($"ShowEffect(pc,{actorID},{height},{effectID},{isOneTime.ToString().ToLower()})" + @";");
            else
                this.contentbuf.AppendLine($"ShowEffect(pc,{x},{y},{height},{effectID},{isOneTime.ToString().ToString()})" + @";");
        }

        void FormatNPCMotion(Packet p)
        {
            uint actorID = p.GetUInt(2);
            ushort motion =p.GetUShort(6);
            byte loop = p.GetByte(8);
            uint speed = p.GetByte(9);
            byte unknown = p.GetByte(13);
            this.contentbuf.AppendLine($"NPCMotion(pc,{actorID},(MotionType){motion},{loop.ToString().ToLower()},{unknown})" + @";");
        }

        void FormatHeader()
        {
            headerbuf.Clear();
            headerbuf.AppendLine("using System;");
            headerbuf.AppendLine("using System.Collections.Generic;");
            headerbuf.AppendLine("using System.Text;");
            headerbuf.AppendLine("using SagaLib;");
            headerbuf.AppendLine("using SagaDB.Actor;");
            headerbuf.AppendLine("using SagaMap.Scripting;");
            headerbuf.AppendLine("using SagaScript.Chinese.Enums;");
            headerbuf.AppendLine("");
            headerbuf.AppendLine("namespace SagaScript");
            headerbuf.AppendLine("{");
            if (this.eventID == 0)
            {
                headerbuf.AppendLine($"  public class S{this.npcID}:Event");
                headerbuf.AppendLine("  {");
                headerbuf.AppendLine($"    public S{this.npcID}()");
                headerbuf.AppendLine("    {");
                headerbuf.AppendLine($"    this.EventID = {this.npcID}; ");
                headerbuf.AppendLine("    }");
            }
            else
            {
                headerbuf.AppendLine($"  public class S{this.eventID}:Event");
                headerbuf.AppendLine("  {");
                headerbuf.AppendLine($"    public S{this.eventID}()");
                headerbuf.AppendLine("    {");
                headerbuf.AppendLine($"    this.EventID = {this.eventID}; ");
                headerbuf.AppendLine("    }");
            }
            headerbuf.AppendLine($"     public override void OnEvent(ActorPC pc)");
            headerbuf.AppendLine("    {");
        }
    
        void FormatFooter()
        {
            footerbuf.Clear();
            footerbuf.AppendLine("    }");
            footerbuf.AppendLine("  }");
            footerbuf.AppendLine("}");
        }
    }
}