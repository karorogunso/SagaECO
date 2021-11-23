using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace EffectTranslateTool
{
    public partial class frmMain : Form
    {
        public frmMain()
        {
            InitializeComponent();
        }

        List<OfficialSSPHeader> OfficialHeader = new List<OfficialSSPHeader>();
        List<OfficialEffect> OfficialSkills = new List<OfficialEffect>();
        public string FilePath;

        private List<ACESSPHeader> ACEHeader = new List<ACESSPHeader>();
        private List<ACEEffect> ACESkills = new List<ACEEffect>();

        class OfficialSSPHeader
        {
            public uint offset;
            public ushort size;
        }
        class OfficialEffect
        {
            public string name, desc;
            public byte active, maxLv, lv, nodata, range, target, target2, effectRange;
            public ushort id, icon, mp, sp, ap, nHumei4, nHumei5, nHumei6, nHumei7, nAnim1, nAnim2, nAnim3;
            public int CastTime, Delay, SingleCD, equipFlag, skillFlag, effect1, effect2, effect3, nHumei8, effect4, effect5, effect6, effect7, effect8, nHumei9, nHumei10, nHumei2;
        }
        private class ACEEffect
        {
            public byte active;
            public int castTime, delay;
            public string name, desc;
            public int SingleCD, effect1, effect2, effect3, effect4, effect5, effect6, effect7, effect8, skillFlag, equipFlag, nHumei2, nHumei8;
            public ushort id, mp, sp, ep, nAnim1, nAnim2, nAnim3, nHumei4, nHumei5, nHumei6, nHumei7;
            public byte lv, maxLv, effectRange, nodata, range, target, target2;
        }
        private class ACESSPHeader
        {
            public uint offset;
            public ushort size;
        }

        private void btnOfficialssp2csv_Click(object sender, EventArgs e)
        {
            OpenSsp.Filter = "effect.ssp|effect.ssp";
            OpenSsp.FileName = "effect.ssp";
            OpenSsp.Title = "选择打开effect.ssp的位置";
            if (OpenSsp.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                this.OpenOfficialSSP();
                this.SaveOfficialCSV();
                this.OfficialHeader.Clear();
                this.OfficialSkills.Clear();
                MessageBox.Show("官方专属 ssp->csv转换完成");
            }
        }
        private void btnOfficialcsv2ssp_Click(object sender, EventArgs e)
        {
            OpenCsv.Filter = "skillDB.csv|skillDB.csv";
            OpenCsv.Title = "选择打开csv的位置";
            if (OpenCsv.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                this.OpenOfficialCSV();
                this.SaveOfficialSSP();
                this.OfficialHeader.Clear();
                this.OfficialSkills.Clear();
                MessageBox.Show("官方专属 csv->ssp转换完成");
            }
        }
        private void btnAcessp2csv_Click(object sender, EventArgs e)
        {
            this.OpenSsp.Filter = "effect.ssp|effect.ssp";
            this.OpenSsp.FileName = "effect.ssp";
            this.OpenSsp.Title = "选择打开effect.ssp的位置";
            if (this.OpenSsp.ShowDialog() == DialogResult.OK)
            {
                this.OpenACESSP();
                this.SaveACECSV();
                this.ACEHeader.Clear();
                this.ACESkills.Clear();
                MessageBox.Show("ACE专属 ssp->csv转换完成");
            }
        }
        private void btnAcecsv2ssp_Click(object sender, EventArgs e)
        {
            this.OpenCsv.Filter = "skillDB.csv|skillDB.csv";
            this.OpenCsv.Title = "选择打开csv的位置";
            if (this.OpenCsv.ShowDialog() == DialogResult.OK)
            {
                this.OpenACECSV();
                this.SaveACESSP();
                this.ACEHeader.Clear();
                this.ACESkills.Clear();
                MessageBox.Show("ACE专属 csv->ssp转换完成");
            }
        }

        private void OpenOfficialSSP()
        {
            Encoding unicode = Encoding.Unicode;
            this.OfficialHeader = new List<OfficialSSPHeader>();
            this.OfficialSkills = new List<OfficialEffect>();
            this.FilePath = this.OpenSsp.FileName;
            string str = Path.GetDirectoryName(this.FilePath) + @"\";
            string fileNameWithoutExtension = Path.GetFileNameWithoutExtension(this.FilePath);
            FileStream input = new FileStream(this.FilePath, FileMode.Open, FileAccess.ReadWrite);
            BinaryReader reader = new BinaryReader(input);
            for (int i = 0; i < 30000; i++)
            {
                OfficialSSPHeader item = new OfficialSSPHeader
                {
                    offset = reader.ReadUInt32(),
                    size = 0x02d8
                };
                if (item.offset == 0)
                    break;
                this.OfficialHeader.Add(item);
            }

            foreach (OfficialSSPHeader i in this.OfficialHeader)
            {
                input.Position = i.offset;
                OfficialEffect effice = new OfficialEffect
                {
                    id = reader.ReadUInt16()
                };
                if (effice.id == 0)
                    continue;
                effice.icon = reader.ReadUInt16();
                string str3 = unicode.GetString(reader.ReadBytes(116));
                effice.name = str3.Remove(str3.IndexOf('\0'));
                effice.CastTime = reader.ReadInt32();
                effice.Delay = reader.ReadInt32();
                effice.SingleCD = reader.ReadInt32();
                str3 = unicode.GetString(reader.ReadBytes(512));
                effice.desc = str3.Remove(str3.IndexOf('\0'));
                effice.active = reader.ReadByte();
                effice.maxLv = reader.ReadByte();
                effice.lv = reader.ReadByte();
                effice.nodata = reader.ReadByte();
                effice.mp = reader.ReadUInt16();
                effice.sp = reader.ReadUInt16();
                effice.ap = reader.ReadUInt16();
                effice.range = reader.ReadByte();
                effice.target = reader.ReadByte();
                effice.target2 = reader.ReadByte();
                effice.effectRange = reader.ReadByte();
                effice.equipFlag = reader.ReadInt32();
                effice.nHumei2 = reader.ReadInt32();
                effice.skillFlag = reader.ReadInt32();
                effice.nHumei4 = reader.ReadUInt16();
                effice.nHumei5 = reader.ReadUInt16();
                effice.nHumei6 = reader.ReadUInt16();
                effice.nHumei7 = reader.ReadUInt16();
                effice.nHumei9 = reader.ReadInt32();
                effice.nHumei10 = reader.ReadInt32();
                effice.effect1 = reader.ReadInt32();
                effice.effect2 = reader.ReadInt32();
                effice.effect3 = reader.ReadInt32();
                effice.nHumei8 = reader.ReadInt32();
                effice.effect4 = reader.ReadInt32();
                effice.effect5 = reader.ReadInt32();
                effice.effect6 = reader.ReadInt32();
                effice.effect7 = reader.ReadInt32();
                effice.effect8 = reader.ReadInt32();
                effice.nAnim1 = reader.ReadUInt16();
                effice.nAnim2 = reader.ReadUInt16();
                effice.nAnim3 = reader.ReadUInt16();
                if (effice.id != 0)
                {
                    this.OfficialSkills.Add(effice);
                }
            }
            input.Close();
        }
        private void SaveOfficialCSV()
        {
            this.SaveCsv.Filter = "skillDB.csv|skillDB.csv";
            this.SaveCsv.Title = "选择保存csv的位置";
            if (this.SaveCsv.ShowDialog() == DialogResult.OK)
            {
                StreamWriter writer = new StreamWriter(this.SaveCsv.FileName, false, Encoding.UTF8);
                writer.WriteLine("#ID,图标,Name,咏唱时间,公共延迟,独立冷却,描述,主动,最大Lv,技能Lv,Nodata,MP,SP,EP,射程,目标类型,范围类型,效果范围,所需装备,技能Flag,Humei2,技能标记,Humei4,Humei5,Humei7,effect1,effect2,effect3,Humei8,effect4,effect5,effect6,effect7,effect8,Animi1,Animi2,Animi3");
                foreach (OfficialEffect item in this.OfficialSkills)
                {
                    StringBuilder sb = new StringBuilder();
                    sb.Append(item.id + "," + item.icon + "," + item.name + "," + item.CastTime + "," + item.Delay + "," + item.SingleCD + "," + item.desc.Replace("\r\n", "$R") + "," + item.active + "," + item.maxLv + "," + item.lv + "," + item.nodata + ","
                        + item.mp + "," + item.sp + "," + item.ap + "," + item.range + "," + item.target + "," + item.target2 + "," + item.effectRange + "," + item.equipFlag + "," + item.nHumei2 + "," + item.skillFlag + "," + item.nHumei4
                        + "," + item.nHumei5 + "," + item.nHumei6 + "," + item.nHumei7 + "," + item.nHumei9 + "," + item.nHumei10 + "," + item.effect1 + "," + item.effect2 + "," + item.effect3 + "," + item.nHumei8 + "," + item.effect4
                        + "," + item.effect5 + "," + item.effect6 + "," + item.effect7 + "," + item.effect8 + "," + item.nAnim1 + "," + item.nAnim2 + "," + item.nAnim3);

                    writer.WriteLine(sb.ToString());
                }

                writer.Flush();
                writer.Close();
            }
        }

        private void OpenOfficialCSV()
        {
            try
            {
                StreamReader reader = new StreamReader(this.OpenCsv.FileName, Encoding.UTF8);
                this.OfficialSkills = new List<OfficialEffect>();
                while (!reader.EndOfStream)
                {
                    var str = reader.ReadLine();
                    if (str.StartsWith("#"))
                        continue;
                    string[] strArray = str.Split(new char[] { ',' });
                    OfficialEffect item = new OfficialEffect
                    {
                        id = ushort.Parse(strArray[0]),
                        icon = ushort.Parse(strArray[1]),
                        name = strArray[2]
                    };
                    item.CastTime = int.Parse(strArray[3]);
                    item.Delay = int.Parse(strArray[4]);
                    item.SingleCD = int.Parse(strArray[5]);
                    string str2 = strArray[6];
                    str2 = str2.Replace("$R", "\r\n");
                    item.desc = str2;
                    item.active = byte.Parse(strArray[7]);
                    item.maxLv = byte.Parse(strArray[8]);
                    item.lv = byte.Parse(strArray[9]);
                    item.nodata = byte.Parse(strArray[10]);
                    item.mp = ushort.Parse(strArray[11]);
                    item.sp = ushort.Parse(strArray[12]);
                    item.ap = ushort.Parse(strArray[13]);
                    item.range = byte.Parse(strArray[14]);
                    item.target = byte.Parse(strArray[15]);
                    item.target2 = byte.Parse(strArray[16]);
                    item.effectRange = byte.Parse(strArray[17]);
                    item.equipFlag = int.Parse(strArray[18]);
                    item.nHumei2 = int.Parse(strArray[19]);
                    item.skillFlag = int.Parse(strArray[20]);
                    item.nHumei4 = ushort.Parse(strArray[21]);
                    item.nHumei5 = ushort.Parse(strArray[22]);
                    item.nHumei6 = ushort.Parse(strArray[23]);
                    item.nHumei7 = ushort.Parse(strArray[24]);
                    item.nHumei9 = int.Parse(strArray[25]);
                    item.nHumei10 = int.Parse(strArray[26]);
                    item.effect1 = int.Parse(strArray[27]);
                    item.effect2 = int.Parse(strArray[28]);
                    item.effect3 = int.Parse(strArray[29]);
                    item.nHumei8 = int.Parse(strArray[30]);
                    item.effect4 = int.Parse(strArray[31]);
                    item.effect5 = int.Parse(strArray[32]);
                    item.effect6 = int.Parse(strArray[33]);
                    item.effect7 = int.Parse(strArray[34]);
                    item.effect8 = int.Parse(strArray[35]);
                    item.nAnim1 = ushort.Parse(strArray[36]);
                    item.nAnim2 = ushort.Parse(strArray[37]);
                    item.nAnim3 = ushort.Parse(strArray[38]);
                    if (item.id != 0)
                    {
                        this.OfficialSkills.Add(item);
                    }
                }
                reader.Close();
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.ToString());
            }
        }
        private void SaveOfficialSSP()
        {
            this.SaveSsp.Filter = "*.ssp|*.ssp";
            this.SaveSsp.FileName = "effect.ssp";
            this.SaveSsp.Title = "选择保存effect.ssp的位置";
            if (this.SaveSsp.ShowDialog() == DialogResult.OK)
            {
                FileStream output = new FileStream(this.SaveSsp.FileName, FileMode.Create);
                BinaryWriter writer = new BinaryWriter(output, Encoding.Unicode);
                //头文件部分直接自己计算就可以出来。完全没有存在的必要。
                for (int i = 0; i < this.OfficialSkills.Count; i++)
                    writer.Write((uint)(0x30000 + 0x02D8 * i));
                output.Position = 0x20000;
                for (int i = 0; i < this.OfficialSkills.Count; i++)
                    writer.Write((ushort)0x02D8);//每一个SkillData长度是固定的
                output.Position = 0x30000;

                foreach (OfficialEffect effice in this.OfficialSkills)
                {
                    writer.Write(effice.id);
                    writer.Write(effice.icon);
                    char[] cname = effice.name.ToCharArray();
                    Array.Resize<char>(ref cname, 58); //char类型占用2字节，因此116/2=58。此外，这里可以不声明<char>，直接使用Array.Resize(ref cname, 58);
                    writer.Write(cname);

                    writer.Write(effice.CastTime);
                    writer.Write(effice.Delay);
                    writer.Write(effice.SingleCD);

                    char[] cdes = effice.desc.Replace(Environment.NewLine, "$R").ToCharArray();
                    Array.Resize<char>(ref cdes, 256);
                    writer.Write(cdes);

                    writer.Write(effice.active);
                    writer.Write(effice.maxLv);
                    writer.Write(effice.lv);
                    writer.Write(effice.nodata);
                    writer.Write(effice.mp);
                    writer.Write(effice.sp);
                    writer.Write(effice.ap);
                    writer.Write(effice.range);
                    writer.Write(effice.target);
                    writer.Write(effice.target2);
                    writer.Write(effice.effectRange);
                    writer.Write(effice.equipFlag);
                    writer.Write(effice.nHumei2);
                    writer.Write(effice.skillFlag);
                    writer.Write(effice.nHumei4);
                    writer.Write(effice.nHumei5);
                    writer.Write(effice.nHumei6);
                    writer.Write(effice.nHumei7);
                    writer.Write(effice.nHumei9);
                    writer.Write(effice.nHumei10);
                    writer.Write(effice.effect1);
                    writer.Write(effice.effect2);
                    writer.Write(effice.effect3);
                    writer.Write(effice.nHumei8);
                    writer.Write(effice.effect4);
                    writer.Write(effice.effect5);
                    writer.Write(effice.effect6);
                    writer.Write(effice.effect7);
                    writer.Write(effice.effect8);
                    writer.Write(effice.nAnim1);
                    writer.Write(effice.nAnim2);
                    writer.Write(effice.nAnim3);
                }
                output.Close();
                writer.Close();
            }
        }

        private void OpenACECSV()
        {
            try
            {
                StreamReader reader = new StreamReader(this.OpenCsv.FileName, Encoding.UTF8);
                this.ACESkills = new List<ACEEffect>();
                while (!reader.EndOfStream)
                {
                    string str = reader.ReadLine();
                    if (!str.StartsWith("#"))
                    {
                        string[] strArray = str.Split(new char[] { ',' });
                        ACEEffect item = new ACEEffect
                        {
                            id = ushort.Parse(strArray[0]),
                            name = strArray[1]
                        };
                        string str2 = strArray[2];
                        str2 = str2.Replace("$R", "\r\n");
                        item.desc = str2;
                        item.active = byte.Parse(strArray[3]);
                        item.maxLv = byte.Parse(strArray[4]);
                        item.lv = byte.Parse(strArray[5]);
                        item.nodata = byte.Parse(strArray[6]);
                        item.castTime = int.Parse(strArray[7]);
                        item.delay = int.Parse(strArray[8]);
                        item.SingleCD = int.Parse(strArray[9]);
                        item.mp = ushort.Parse(strArray[10]);
                        item.sp = ushort.Parse(strArray[11]);
                        item.ep = ushort.Parse(strArray[12]);
                        item.range = byte.Parse(strArray[13]);
                        item.target = byte.Parse(strArray[14]);
                        item.target2 = byte.Parse(strArray[15]);
                        item.effectRange = byte.Parse(strArray[16]);
                        item.equipFlag = int.Parse(strArray[17]);
                        item.nHumei2 = int.Parse(strArray[18]);
                        item.skillFlag = int.Parse(strArray[19]);
                        item.nHumei4 = ushort.Parse(strArray[20]);
                        item.nHumei5 = ushort.Parse(strArray[21]);
                        item.nHumei6 = ushort.Parse(strArray[22]);
                        item.nHumei7 = ushort.Parse(strArray[23]);
                        item.effect1 = int.Parse(strArray[24]);
                        item.effect2 = int.Parse(strArray[25]);
                        item.effect3 = int.Parse(strArray[26]);
                        item.nHumei8 = int.Parse(strArray[27]);
                        item.effect4 = int.Parse(strArray[28]);
                        item.effect5 = int.Parse(strArray[29]);
                        item.effect6 = int.Parse(strArray[23]);
                        item.effect7 = int.Parse(strArray[31]);
                        item.effect8 = int.Parse(strArray[32]);
                        item.nAnim1 = ushort.Parse(strArray[33]);
                        item.nAnim2 = ushort.Parse(strArray[34]);
                        item.nAnim3 = ushort.Parse(strArray[35]);
                        if (item.id != 0)
                        {
                            this.ACESkills.Add(item);
                        }
                    }
                }
                reader.Close();
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.ToString());
            }
        }
        private void OpenACESSP()
        {
            int num;
            Encoding unicode = Encoding.Unicode;
            this.ACEHeader = new List<ACESSPHeader>();
            this.ACESkills = new List<ACEEffect>();
            this.FilePath = this.OpenSsp.FileName;
            string str = Path.GetDirectoryName(this.FilePath) + @"\";
            string fileNameWithoutExtension = Path.GetFileNameWithoutExtension(this.FilePath);
            FileStream input = new FileStream(this.FilePath, FileMode.Open, FileAccess.ReadWrite);
            BinaryReader reader = new BinaryReader(input);
            for (num = 0; num < 10000; num++)
            {
                ACESSPHeader item = new ACESSPHeader
                {
                    offset = reader.ReadUInt32()
                };
                if (item.offset != 0)
                {
                    this.ACEHeader.Add(item);
                }
            }
            for (num = 0; num < this.ACEHeader.Count; num++)
            {
                this.ACEHeader[num].size = 800;
            }
            foreach (ACESSPHeader header2 in this.ACEHeader)
            {
                input.Position = header2.offset;
                ACEEffect effect = new ACEEffect
                {
                    id = reader.ReadUInt16()
                };
                string str3 = unicode.GetString(reader.ReadBytes(128));
                effect.name = str3.Remove(str3.IndexOf('\0'));
                str3 = unicode.GetString(reader.ReadBytes(512));
                effect.desc = str3.Remove(str3.IndexOf('\0'));
                effect.active = reader.ReadByte();
                effect.maxLv = reader.ReadByte();
                effect.lv = reader.ReadByte();
                effect.nodata = reader.ReadByte();
                effect.castTime = reader.ReadInt32();
                effect.delay = reader.ReadInt32();
                effect.SingleCD = reader.ReadInt32();
                effect.mp = reader.ReadUInt16();
                effect.sp = reader.ReadUInt16();
                effect.ep = reader.ReadUInt16();
                effect.range = reader.ReadByte();
                effect.target = reader.ReadByte();
                effect.target2 = reader.ReadByte();
                effect.effectRange = reader.ReadByte();
                effect.equipFlag = reader.ReadInt32();
                effect.nHumei2 = reader.ReadInt32();
                effect.skillFlag = reader.ReadInt32();
                effect.nHumei4 = reader.ReadUInt16();
                effect.nHumei5 = reader.ReadUInt16();
                effect.nHumei6 = reader.ReadUInt16();
                effect.nHumei7 = reader.ReadUInt16();
                effect.effect1 = reader.ReadInt32();
                effect.effect2 = reader.ReadInt32();
                effect.effect3 = reader.ReadInt32();
                effect.nHumei8 = reader.ReadInt32();
                effect.effect4 = reader.ReadInt32();
                effect.effect5 = reader.ReadInt32();
                effect.effect6 = reader.ReadInt32();
                effect.effect7 = reader.ReadInt32();
                effect.effect8 = reader.ReadInt32();
                effect.nAnim1 = reader.ReadUInt16();
                effect.nAnim2 = reader.ReadUInt16();
                effect.nAnim3 = reader.ReadUInt16();
                if (effect.id != 0)
                {
                    this.ACESkills.Add(effect);
                }
            }
            input.Close();
        }
        private void SaveACECSV()
        {
            this.SaveCsv.Filter = "skillDB.csv|skillDB.csv";
            this.SaveCsv.Title = "选择保存csv的位置";
            if (this.SaveCsv.ShowDialog() == DialogResult.OK)
            {
                StreamWriter writer = new StreamWriter(this.SaveCsv.FileName, false, Encoding.UTF8);
                writer.WriteLine("#ID,Name,描述,主动,最大Lv,技能Lv,Nodata,CastTime,Delay,SingleCD,MP,SP,EP,射程,目标类型,范围类型,效果范围,所需装备,技能Flag,Humei2,技能标记,Humei4,Humei5,Humei7,effect1,effect2,effect3,Humei8,effect4,effect5,effect6,effect7,effect8,Animi1,Animi2,Animi3");
                foreach (ACEEffect i in this.ACESkills)
                {
                    string txt = i.id.ToString();
                    txt += ("," + i.name);
                    string desc = i.desc;
                    if (desc.Contains("\r\n"))
                        desc = desc.Replace("\r\n", "$R");
                    txt += ("," + desc);
                    txt += ("," + i.active.ToString());
                    txt += ("," + i.maxLv.ToString());
                    txt += ("," + i.lv.ToString());
                    txt += ("," + i.nodata.ToString());
                    txt += ("," + i.castTime.ToString());
                    txt += ("," + i.delay.ToString());
                    txt += ("," + i.SingleCD.ToString());
                    txt += ("," + i.mp.ToString());
                    txt += ("," + i.sp.ToString());
                    txt += ("," + i.ep.ToString());
                    txt += ("," + i.range.ToString());
                    txt += ("," + i.target.ToString());
                    txt += ("," + i.target2.ToString());
                    txt += ("," + i.effectRange.ToString());
                    txt += ("," + i.equipFlag.ToString());
                    txt += ("," + i.nHumei2.ToString());
                    txt += ("," + i.skillFlag.ToString());
                    txt += ("," + i.nHumei4.ToString());
                    txt += ("," + i.nHumei5.ToString());
                    txt += ("," + i.nHumei6.ToString());
                    txt += ("," + i.nHumei7.ToString());
                    txt += ("," + i.effect1.ToString());
                    txt += ("," + i.effect2.ToString());
                    txt += ("," + i.effect3.ToString());
                    txt += ("," + i.nHumei8.ToString());
                    txt += ("," + i.effect4.ToString());
                    txt += ("," + i.effect5.ToString());
                    txt += ("," + i.effect6.ToString());
                    txt += ("," + i.effect7.ToString());
                    txt += ("," + i.effect8.ToString());
                    txt += ("," + i.nAnim1.ToString());
                    txt += ("," + i.nAnim2.ToString());
                    txt += ("," + i.nAnim3.ToString());
                    writer.WriteLine(txt);
                }
                writer.Flush();
                writer.Close();
            }
        }
        private void SaveACESSP()
        {
            this.SaveSsp.Filter = "*.ssp|*.ssp";
            this.SaveSsp.FileName = "effect.ssp";
            this.SaveSsp.Title = "选择保存effect.ssp的位置";
            if (this.SaveSsp.ShowDialog() == DialogResult.OK)
            {
                int num;
                FileStream output = new FileStream(this.SaveSsp.FileName, FileMode.Create);
                BinaryWriter writer = new BinaryWriter(output);
                for (num = 0; num < this.ACESkills.Count; num++)
                {
                    writer.Write((int)(60000 + (800 * num)));
                }
                while (writer.BaseStream.Position < 60000)
                {
                    writer.Write(0);
                }
                for (num = 0; num < this.ACESkills.Count; num++)
                {
                    writer.Seek(60000 + (800 * num), SeekOrigin.Begin);
                    writer.Write(this.ACESkills[num].id);
                    byte[] bytes = new byte[128];
                    Encoding.Unicode.GetBytes(this.ACESkills[num].name, 0, this.ACESkills[num].name.Length, bytes, 0);
                    writer.Write(bytes);
                    bytes = new byte[512];
                    Encoding.Unicode.GetBytes(this.ACESkills[num].desc, 0, this.ACESkills[num].desc.Length, bytes, 0);
                    writer.Write(bytes);
                    writer.Write(this.ACESkills[num].active);
                    writer.Write(this.ACESkills[num].maxLv);
                    writer.Write(this.ACESkills[num].lv);
                    writer.Write(this.ACESkills[num].nodata);
                    writer.Write(this.ACESkills[num].castTime);
                    writer.Write(this.ACESkills[num].delay);
                    writer.Write(this.ACESkills[num].SingleCD);
                    writer.Write(this.ACESkills[num].mp);
                    writer.Write(this.ACESkills[num].sp);
                    writer.Write(this.ACESkills[num].ep);
                    writer.Write(this.ACESkills[num].range);
                    writer.Write(this.ACESkills[num].target);
                    writer.Write(this.ACESkills[num].target2);
                    writer.Write(this.ACESkills[num].effectRange);
                    writer.Write(this.ACESkills[num].equipFlag);
                    writer.Write(this.ACESkills[num].nHumei2);
                    writer.Write(this.ACESkills[num].skillFlag);
                    writer.Write(this.ACESkills[num].nHumei4);
                    writer.Write(this.ACESkills[num].nHumei5);
                    writer.Write(this.ACESkills[num].nHumei6);
                    writer.Write(this.ACESkills[num].nHumei7);
                    writer.Write(this.ACESkills[num].effect1);
                    writer.Write(this.ACESkills[num].effect2);
                    writer.Write(this.ACESkills[num].effect3);
                    writer.Write(this.ACESkills[num].nHumei8);
                    writer.Write(this.ACESkills[num].effect4);
                    writer.Write(this.ACESkills[num].effect5);
                    writer.Write(this.ACESkills[num].effect6);
                    writer.Write(this.ACESkills[num].effect7);
                    writer.Write(this.ACESkills[num].effect8);
                    writer.Write(this.ACESkills[num].nAnim1);
                    writer.Write(this.ACESkills[num].nAnim2);
                    writer.Write(this.ACESkills[num].nAnim3);
                }
                while (writer.BaseStream.Position < 8000000)
                {
                    writer.Write(0);
                }
                output.Close();
                writer.Close();
            }
        }

    }
}
