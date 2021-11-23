using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using RebuildSSP;

namespace SkillDB
{
    class Utility
    {
        static string directory { get; set; }
        public struct Datapack
        {
            public List<string> SkillID { get; set; } //技能資料:SKILL ID

            public List<string> SubSkillID{ get; set; } //技能資料:替代用SKILL ID

            public List<string> SkillName { get; set; } //技能資料:名稱
            public List<string> Description { get; set; } //技能資料:描述
            public List<string> SkillType { get; set; } //技能資料:主動/被動
            public List<string> MaxLV { get; set; } //技能資料:最大LV
            public List<string> LV { get; set; } //技能資料:技能LV
            public List<string> NoData { get; set; } //技能資料:NoData
            public List<string> MP { get; set; } //技能資料:消耗MP
            public List<string> SP { get; set; } //技能資料:消耗SP
            public List<string> EP { get; set; } //技能資料:消耗EP
            public List<string> Range { get; set; } //技能資料:射程
            public List<string> TargetType { get; set; } //技能資料:目標類別
            public List<string> AreaType { get; set; } //技能資料:範圍類別
            public List<string> RangeArea { get; set; } //技能資料:範圍類別
            public List<string> Equip { get; set; } //技能資料:所需裝備
            public List<string> Unknown2 { get; set; }//技能資料:不明
            public List<string> SkillFlag { get; set; }//技能資料:技能標籤
            public List<string> Unknown4 { get; set; }//技能資料:不明
            public List<string> Unknown5 { get; set; }//技能資料:不明
            public List<string> Unknown6 { get; set; }//技能資料:不明
            public List<string> Unknown7 { get; set; }//技能資料:不明
            public List<string> Effect1 { get; set; }//技能資料:效果1
            public List<string> Effect2 { get; set; }//技能資料:效果2
            public List<string> Effect3 { get; set; }//技能資料:效果3
            public List<string> Unknown8 { get; set; }//技能資料:不明
            public List<string> Effect4 { get; set; }//技能資料:效果4
            public List<string> Effect5 { get; set; }//技能資料:效果5
            public List<string> Effect6 { get; set; }//技能資料:效果6
            public List<string> Effect7 { get; set; }//技能資料:效果7
            public List<string> Effect8 { get; set; }//技能資料:效果8
            public List<string> Animation1 { get; set; }//技能資料:動畫1
            public List<string> Animation2 { get; set; }//技能資料:動畫2
            public List<string> Animation3 { get; set; }//技能資料:動畫3

        }
        public struct Param
        {
            public static int Length { get; set; } //技能資料長度
            public static int MaxSkillCount { get; set; } //最大存放技能數目
            public static int SkillCount { get; set; } //存放技能數目
        }

        public class SetUtility
        {
            public void OpenSetting(bool Writessp)
            {
                var s = new Setting();
                if (Writessp==false)
                {
                    s.radioButton3.Visible = false;
                    s.radioButton4.Visible = false;
                }
                s.ShowDialog();
            }
        }
        public class WriteUtility
        {
            public BinaryWriter bw = new BinaryWriter(File.OpenWrite(directory), Encoding.Unicode);
            public void WriteNumber(string s, int bytes)
            {
                var c = new ConvertUtility();
                // 寫入前處理: 轉為16進制字串
                int IntIntermediate = Convert.ToInt32(s);
                string StringIntermediate = Convert.ToString(IntIntermediate, 16);
                // 補至指定bytes
                for (int j = StringIntermediate.Length; j < bytes * 2; j++)
                {
                    StringIntermediate = "0" + StringIntermediate;
                }
                byte[] temp = c.StringToByteArray(StringIntermediate);
                bw.Write(temp, 0, temp.Length);
                bw.Flush();
            }
            public void WriteWords(string s, int bytes)
            {
                var c = new ConvertUtility();
                byte[] padding = c.StringToByteArray("00");
                // SkillName 寫入前轉為16進制字串
                // 寫入SkillName
                byte[] temp = Encoding.Unicode.GetBytes(s);
                bw.Write(temp, 0, temp.Length);
                for (int j = temp.Length; j < bytes; j++)//寫一次1byte
                {
                    bw.Write(padding);
                }
                bw.Flush();
            }
            public void WritePadding(int bytes)
            {
                var c = new ConvertUtility();
                byte[] padding = c.StringToByteArray("00");
                for (int i = 0; i < bytes; i++)//寫一次1byte
                {
                    bw.Write(padding);
                }
                bw.Flush();
            }
        }
        public class ConvertUtility
        {
            public string UTF8toUTF16(string words)
            {
                words = Encoding.Unicode.GetString(Encoding.Convert(Encoding.UTF8, Encoding.Unicode, Encoding.UTF8.GetBytes(words)));
                return words;
            }
            public string UTF16toUTF8(string words)
            {
                words = Encoding.UTF8.GetString(Encoding.Convert(Encoding.Unicode, Encoding.UTF8, Encoding.Unicode.GetBytes(words)));
                return words;
            }
            public byte[] StringToByteArray(string hex)
            {
                // 單數長度要在調用前補0至雙數
                if (hex.Length % 2 != 0) hex = "0" + hex;
                byte[] temp = Enumerable.Range(0, hex.Length / 2).Select(x => Convert.ToByte(hex.Substring(x * 2, 2), 16)).ToArray();
                // 作寫入前反轉
                Array.Reverse(temp);
                return temp;
            }
        }
        public int ReadSkillDB(out Datapack raw)
        {
            raw = new Datapack();
            Param.SkillCount = 0;
            var dialog = new System.Windows.Forms.OpenFileDialog();
            dialog.Title = "Select csv";
            dialog.DefaultExt = ".csv";
            dialog.InitialDirectory = System.Environment.CurrentDirectory;
            if (dialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                directory = dialog.FileName;
                var reader = new StreamReader(File.OpenRead(directory), Encoding.UTF8);
                raw.SkillID = new List<string>();
                raw.SkillName = new List<string>();
                raw.Description = new List<string>();
                raw.SkillType = new List<string>();
                raw.MaxLV = new List<string>();
                raw.LV = new List<string>();
                raw.NoData = new List<string>();
                raw.MP = new List<string>();
                raw.SP = new List<string>();
                raw.EP = new List<string>();
                raw.Range = new List<string>();
                raw.TargetType = new List<string>();
                raw.AreaType = new List<string>();
                raw.RangeArea = new List<string>();
                raw.Equip = new List<string>();
                raw.Unknown2 = new List<string>();
                raw.SkillFlag = new List<string>();

                raw.Unknown4 = new List<string>();
                raw.Unknown5 = new List<string>();
                raw.Unknown6 = new List<string>();
                raw.Unknown7 = new List<string>();
                raw.Effect1 = new List<string>();
                raw.Effect2 = new List<string>();
                raw.Effect3 = new List<string>();
                raw.Unknown8 = new List<string>();
                raw.Effect4 = new List<string>();
                raw.Effect5 = new List<string>();
                raw.Effect6 = new List<string>();
                raw.Effect7 = new List<string>();
                raw.Effect8 = new List<string>();
                raw.Animation1 = new List<string>();
                raw.Animation2 = new List<string>();
                raw.Animation3 = new List<string>();
                while (!reader.EndOfStream)
                {
                    Param.SkillCount++;
                    string line = reader.ReadLine();
                    string[] TempValues = line.Split(',');
                    var c = new ConvertUtility();

                    raw.SkillID.Add(c.UTF8toUTF16(TempValues[0]));
                    raw.SkillName.Add(c.UTF8toUTF16(TempValues[1]));
                    raw.Description.Add(c.UTF8toUTF16(TempValues[2]));

                    raw.SkillType.Add(c.UTF8toUTF16(TempValues[3]));
                    raw.MaxLV.Add(c.UTF8toUTF16(TempValues[4]));
                    raw.LV.Add(c.UTF8toUTF16(TempValues[5]));
                    raw.NoData.Add(c.UTF8toUTF16(TempValues[6]));

                    raw.MP.Add(c.UTF8toUTF16(TempValues[7]));
                    raw.SP.Add(c.UTF8toUTF16(TempValues[8]));
                    raw.EP.Add(c.UTF8toUTF16(TempValues[9]));

                    raw.Range.Add(c.UTF8toUTF16(TempValues[10]));
                    raw.TargetType.Add(c.UTF8toUTF16(TempValues[11]));
                    raw.AreaType.Add(c.UTF8toUTF16(TempValues[12]));
                    raw.RangeArea.Add(c.UTF8toUTF16(TempValues[13]));

                    raw.Equip.Add(c.UTF8toUTF16(TempValues[14]));
                    raw.Unknown2.Add(c.UTF8toUTF16(TempValues[15]));
                    raw.SkillFlag.Add(c.UTF8toUTF16(TempValues[16]));
                    raw.Unknown4.Add(c.UTF8toUTF16(TempValues[17]));

                    raw.Unknown5.Add(c.UTF8toUTF16(TempValues[18]));
                    raw.Unknown6.Add(c.UTF8toUTF16(TempValues[19]));
                    raw.Unknown7.Add(c.UTF8toUTF16(TempValues[20]));
                    raw.Effect1.Add(c.UTF8toUTF16(TempValues[21]));

                    raw.Effect2.Add(c.UTF8toUTF16(TempValues[22]));
                    raw.Effect3.Add(c.UTF8toUTF16(TempValues[23]));
                    raw.Unknown8.Add(c.UTF8toUTF16(TempValues[24]));
                    raw.Effect4.Add(c.UTF8toUTF16(TempValues[25]));

                    raw.Effect5.Add(c.UTF8toUTF16(TempValues[26]));
                    raw.Effect6.Add(c.UTF8toUTF16(TempValues[27]));
                    raw.Effect7.Add(c.UTF8toUTF16(TempValues[28]));
                    raw.Effect8.Add(c.UTF8toUTF16(TempValues[29]));

                    raw.Animation1.Add(c.UTF8toUTF16(TempValues[30]));
                    raw.Animation2.Add(c.UTF8toUTF16(TempValues[31]));
                    raw.Animation3.Add(c.UTF8toUTF16(TempValues[32]));
                }
                reader.Close();
                return 0;
            }
            else
            {
                return -1;
            }
        }
        public int Writessp(Datapack data)
        {
            var s = new Setting();
            s.ShowDialog();
            var dialog = new System.Windows.Forms.SaveFileDialog();
            dialog.Title = "Select ssp";
            dialog.DefaultExt = ".ssp";
            dialog.InitialDirectory = System.Environment.CurrentDirectory;
            if (dialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                directory = dialog.FileName;
                WriteUtility w = new WriteUtility();
                // 寫入header
                // 寫入address
                // 先寫入有資料的技能起始位置
                for (int i = 0; i < Param.SkillCount; i++)
                {
                    w.WriteNumber(Convert.ToString(Param.MaxSkillCount * 6 + Param.Length * i), 4);
                }
                //餘下的每組空白由4bytes的padding填補
                for (int i = 0; i < Param.MaxSkillCount - Param.SkillCount; i++)
                {
                    w.WritePadding(4);
                }
                // 寫入size
                for (int i = 0; i < Param.SkillCount; i++)
                {
                    w.WriteNumber(Convert.ToString(Param.Length), 2);
                }
                //餘下的每組空白由2bytes的padding填補
                for (int i = 0; i < Param.MaxSkillCount - Param.SkillCount; i++)
                {
                    w.WritePadding(2);
                }

                // 寫入content
                for (int i = 0; i < Param.SkillCount; i++)
                {
                    if (Param.Length == 726)
                    {
                        w.WriteNumber(data.SkillID[i], 2);
                    }
                    else if (Param.Length == 728)
                    {
                        w.WriteNumber(data.SkillID[i], 4);
                    }
                    w.WriteWords(data.SkillName[i], 128);
                    w.WriteWords(data.Description[i], 512);


                    w.WriteNumber(data.SkillType[i], 1);
                    w.WriteNumber(data.MaxLV[i], 1);
                    w.WriteNumber(data.LV[i], 1);
                    w.WriteNumber(data.NoData[i], 1);

                    w.WriteNumber(data.MP[i], 2);
                    w.WriteNumber(data.SP[i], 2);
                    w.WriteNumber(data.EP[i], 2);

                    w.WriteNumber(data.Range[i], 1);
                    w.WriteNumber(data.TargetType[i], 1);
                    w.WriteNumber(data.AreaType[i], 1);
                    w.WriteNumber(data.RangeArea[i], 1);

                    w.WriteNumber(data.Equip[i], 4);
                    w.WriteNumber(data.Unknown2[i], 4);
                    w.WriteNumber(data.SkillFlag[i], 4);
                    w.WriteNumber(data.Unknown4[i], 4);

                    w.WriteNumber(data.Unknown5[i], 4);
                    w.WriteNumber(data.Unknown6[i], 4);
                    w.WriteNumber(data.Unknown7[i], 4);
                    w.WriteNumber(data.Effect1[i], 4);

                    w.WriteNumber(data.Effect2[i], 4);
                    w.WriteNumber(data.Effect3[i], 4);
                    w.WriteNumber(data.Unknown8[i], 4);
                    w.WriteNumber(data.Effect4[i], 4);

                    w.WriteNumber(data.Effect5[i], 4);
                    w.WriteNumber(data.Effect6[i], 4);
                    w.WriteNumber(data.Effect7[i], 4);
                    w.WriteNumber(data.Effect8[i], 4);

                    w.WriteNumber(data.Animation1[i], 2);
                    w.WriteNumber(data.Animation2[i], 2);
                    w.WriteNumber(data.Animation3[i], 2);
                }
                w.bw.Close();
                return 0;
            }
            else
            {
                return -1;
            }
        }

        public int Readssp(out Datapack raw)
        {
            raw = new Datapack();
            var dialog = new System.Windows.Forms.OpenFileDialog();
            dialog.Title = "Select ssp";
            dialog.DefaultExt = ".ssp";
            dialog.InitialDirectory = System.Environment.CurrentDirectory;
            if (dialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                directory = dialog.FileName;
                var reader = new BinaryReader(File.OpenRead(directory), Encoding.UTF8);
                raw.SkillID = new List<string>();
                raw.SubSkillID = new List<string>();
                raw.SkillName = new List<string>();
                raw.Description = new List<string>();
                raw.SkillType = new List<string>();
                raw.MaxLV = new List<string>();
                raw.LV = new List<string>();
                raw.NoData = new List<string>();
                raw.MP = new List<string>();
                raw.SP = new List<string>();
                raw.EP = new List<string>();
                raw.Range = new List<string>();
                raw.TargetType = new List<string>();
                raw.AreaType = new List<string>();
                raw.RangeArea = new List<string>();
                raw.Equip = new List<string>();
                raw.Unknown2 = new List<string>();
                raw.SkillFlag = new List<string>();
                raw.Unknown4 = new List<string>();
                raw.Unknown5 = new List<string>();
                raw.Unknown6 = new List<string>();
                raw.Unknown7 = new List<string>();
                raw.Effect1 = new List<string>();
                raw.Effect2 = new List<string>();
                raw.Effect3 = new List<string>();
                raw.Unknown8 = new List<string>();
                raw.Effect4 = new List<string>();
                raw.Effect5 = new List<string>();
                raw.Effect6 = new List<string>();
                raw.Effect7 = new List<string>();
                raw.Effect8 = new List<string>();
                raw.Animation1 = new List<string>();
                raw.Animation2 = new List<string>();
                raw.Animation3 = new List<string>();


                //Read the header
                Encoding unicode = Encoding.Unicode;
                for (Param.SkillCount = 0; Param.SkillCount < Param.MaxSkillCount; Param.SkillCount++)
                {
                    if (reader.ReadInt32() == 0)
                    {
                        break;
                    }
                }
                reader.ReadBytes((Param.MaxSkillCount - Param.SkillCount) * 4 - 4);
                Param.Length = reader.ReadUInt16();
                reader.ReadBytes((Param.MaxSkillCount) * 2 - 2);
                for (int i = 0; i < Param.SkillCount; i++)
                {
                    if (Param.Length == 726)
                    {
                        raw.SkillID.Add(Convert.ToString(reader.ReadUInt16()));
                    }
                    else if (Param.Length == 728)
                    {
                        raw.SkillID.Add(Convert.ToString(reader.ReadUInt16()));
                        raw.SubSkillID.Add(Convert.ToString(reader.ReadUInt16()));
                    }
                    var c = new ConvertUtility();
                    string intermediate = c.UTF16toUTF8(unicode.GetString(reader.ReadBytes(128)));
                    raw.SkillName.Add(intermediate.Remove(intermediate.IndexOf('\0')));
                    intermediate = c.UTF16toUTF8(unicode.GetString(reader.ReadBytes(512)));
                    raw.Description.Add(intermediate.Remove(intermediate.IndexOf('\0')));

                    raw.SkillType.Add(Convert.ToString(reader.ReadByte()));
                    raw.MaxLV.Add(Convert.ToString(reader.ReadByte()));
                    raw.LV.Add(Convert.ToString(reader.ReadByte()));
                    raw.NoData.Add(Convert.ToString(reader.ReadByte()));

                    raw.MP.Add(Convert.ToString(reader.ReadUInt16()));
                    raw.SP.Add(Convert.ToString(reader.ReadUInt16()));
                    raw.EP.Add(Convert.ToString(reader.ReadUInt16()));

                    raw.Range.Add(Convert.ToString(reader.ReadByte()));
                    raw.TargetType.Add(Convert.ToString(reader.ReadByte()));
                    raw.AreaType.Add(Convert.ToString(reader.ReadByte()));
                    raw.RangeArea.Add(Convert.ToString(reader.ReadByte()));

                    raw.Equip.Add(Convert.ToString(reader.ReadInt32()));
                    raw.Unknown2.Add(Convert.ToString(reader.ReadInt32()));
                    raw.SkillFlag.Add(Convert.ToString(reader.ReadInt32()));
                    raw.Unknown4.Add(Convert.ToString(reader.ReadInt32()));

                    raw.Unknown5.Add(Convert.ToString(reader.ReadInt32()));
                    raw.Unknown6.Add(Convert.ToString(reader.ReadInt32()));
                    raw.Unknown7.Add(Convert.ToString(reader.ReadInt32()));
                    raw.Effect1.Add(Convert.ToString(reader.ReadInt32()));

                    raw.Effect2.Add(Convert.ToString(reader.ReadInt32()));
                    raw.Effect3.Add(Convert.ToString(reader.ReadInt32()));
                    raw.Unknown8.Add(Convert.ToString(reader.ReadInt32()));
                    raw.Effect4.Add(Convert.ToString(reader.ReadInt32()));

                    raw.Effect5.Add(Convert.ToString(reader.ReadInt32()));
                    raw.Effect6.Add(Convert.ToString(reader.ReadInt32()));
                    raw.Effect7.Add(Convert.ToString(reader.ReadInt32()));
                    raw.Effect8.Add(Convert.ToString(reader.ReadInt32()));

                    raw.Animation1.Add(Convert.ToString(reader.ReadUInt16()));
                    raw.Animation2.Add(Convert.ToString(reader.ReadUInt16()));
                    raw.Animation3.Add(Convert.ToString(reader.ReadUInt16()));
                }
                return 0;
            }
            else
            {
                return -1;
            }
        }
        public int WriteSkillDB(Datapack data)
        {
            var dialog = new System.Windows.Forms.SaveFileDialog();
            dialog.Title = "Select csv";
            dialog.DefaultExt = ".csv";
            dialog.InitialDirectory = System.Environment.CurrentDirectory;
            if (dialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                directory = dialog.FileName;
                StreamWriter writer = new StreamWriter(directory, false, Encoding.UTF8);
                if (Param.Length == 726)
                {
                    writer.WriteLine("#SkillID,Name,Description,SkillType,MaxLv,Lv,Nodata,MP,SP,EP,Range,TargetType,AreaType,RangeArea,Equip,Unknown2,SkillFlag,Unknown4,Unknown5,Unknown6,Unknown7,Effect1,Effect2,Effect3,Unknown8,Effect4,Effect5,Effect6,Effect7,Effect8,Ani1,Ani2,Ani3");
                    writer.WriteLine("#Total Skill found " + Param.SkillCount + ",Each of " + Param.Length + "bytes long");
                    for (int i = 0; i < Param.SkillCount; i++)
                    {
                        string temp = data.SkillID[i] + "," + data.SkillName[i] + "," + data.Description[i] + "," +
                            data.SkillType[i] + "," + data.MaxLV[i] + "," + data.LV[i] + "," + data.NoData[i] + "," +
                            data.MP[i] + "," + data.SP[i] + "," + data.EP[i] + "," +
                            data.Range[i] + "," + data.TargetType[i] + "," + data.AreaType[i] + "," + data.RangeArea[i] + "," +
                            data.Equip[i] + "," + data.Unknown2[i] + "," + data.SkillFlag[i] + "," + data.Unknown4[i] + "," +
                            data.Unknown5[i] + "," + data.Unknown6[i] + "," + data.Unknown7[i] + "," + data.Effect1[i] + "," +
                            data.Effect2[i] + "," + data.Effect3[i] + "," + data.Unknown8[i] + "," + data.Effect4[i] + "," +
                            data.Effect5[i] + "," + data.Effect6[i] + "," + data.Effect7[i] + "," + data.Effect8[i] + "," +
                            data.Animation1[i] + "," + data.Animation2[i] + "," + data.Animation3[i];
                        writer.WriteLine(temp);
                        writer.Flush();
                    }
                }
                if (Param.Length == 728)
                {
                    writer.WriteLine("#SkillID,SubSkillID,Name,Description,SkillType,MaxLv,Lv,Nodata,MP,SP,EP,Range,TargetType,AreaType,RangeArea,Equip,Unknown2,SkillFlag,Unknown4,Unknown5,Unknown6,Unknown7,Effect1,Effect2,Effect3,Unknown8,Effect4,Effect5,Effect6,Effect7,Effect8,Ani1,Ani2,Ani3");
                    writer.WriteLine("#Total Skill found " + Param.SkillCount + ",Each of " + Param.Length + "bytes long");
                    for (int i = 0; i < Param.SkillCount; i++)
                    {
                        string temp = data.SkillID[i] + "," + data.SubSkillID[i] + "," + data.SkillName[i] + "," + data.Description[i] + "," +
                            data.SkillType[i] + "," + data.MaxLV[i] + "," + data.LV[i] + "," + data.NoData[i] + "," +
                            data.MP[i] + "," + data.SP[i] + "," + data.EP[i] + "," +
                            data.Range[i] + "," + data.TargetType[i] + "," + data.AreaType[i] + "," + data.RangeArea[i] + "," +
                            data.Equip[i] + "," + data.Unknown2[i] + "," + data.SkillFlag[i] + "," + data.Unknown4[i] + "," +
                            data.Unknown5[i] + "," + data.Unknown6[i] + "," + data.Unknown7[i] + "," + data.Effect1[i] + "," +
                            data.Effect2[i] + "," + data.Effect3[i] + "," + data.Unknown8[i] + "," + data.Effect4[i] + "," +
                            data.Effect5[i] + "," + data.Effect6[i] + "," + data.Effect7[i] + "," + data.Effect8[i] + "," +
                            data.Animation1[i] + "," + data.Animation2[i] + "," + data.Animation3[i];
                        writer.WriteLine(temp);
                        writer.Flush();
                    }
                }
                writer.Close();
                return 0;
            }
            else
            {
                return -1;
            }
        }
    }
}
