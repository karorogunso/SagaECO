using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;

using SagaLib;
using SagaLib.VirtualFileSystem;
using SagaDB.Actor;
using System.IO;

namespace SagaDB.Skill
{
    public class SkillFactory : Singleton<SkillFactory>
    {
        public Dictionary<uint, Dictionary<byte, SkillData>> items = new Dictionary<uint, Dictionary<byte, SkillData>>();
        Dictionary<PC_JOB, Dictionary<uint, byte>> skills = new Dictionary<PC_JOB, Dictionary<uint, byte>>();
        Dictionary<PC_JOB, Dictionary<uint, PreconditionSkill>> skills2 = new Dictionary<PC_JOB, Dictionary<uint, PreconditionSkill>>();

        string sklstpath = "";
        string skdbpath = "";

        public SkillFactory()
        {

        }

        public enum SkillPaga
        {
            p1,
            p21,
            p22,
            p3,
            none,
        }
        class PreconditionSkill
        {
            //public uint LearnSkillID;
            public SkillPaga paga;
            public uint LearnSkillJobLv;
            public Dictionary<uint, uint> PreconditionSkillInfo = new Dictionary<uint, uint>();
        }

        class sspHeader
        {
            public uint offset;
            public ushort size;
        }

        public void ReloadSkillDB()
        {
            ClientManager.noCheckDeadLock = true;
            try
            {
                skills.Clear();
                items.Clear();
                LoadSkillList(sklstpath);
                InitSSP(skdbpath, Encoding.Default);
            }
            catch { }
            ClientManager.noCheckDeadLock = false;
        }
        public Skill GetSkill(uint id, byte level)
        {
            try
            {
                if (!this.items.ContainsKey(id))
                {
                    Logger.ShowDebug("Cannot find skill:" + id.ToString(), null);
                    return null;
                }
                if (level != 0)
                {
                    SkillData data = this.items[id][level];
                    Skill skill = new Skill();
                    skill.BaseData = data;
                    skill.Level = level;
                    return skill;
                }
                else
                {
                    SkillData data = this.items[id][1];
                    Skill skill = new Skill();
                    skill.BaseData = data;
                    skill.Level = level;
                    return skill;
                }
            }
            catch
            {
                //Logger.ShowDebug("Cannot find skill:" + id.ToString() + " with level:" + level, null);
                return null;
            }
        }

        public Dictionary<uint, byte> CheckSkillList(ActorPC pc, SkillPaga paga)
        {
            return CheckSkillList(pc, paga, false);
        }
        public Dictionary<uint, byte> CheckSkillList(ActorPC pc, SkillPaga paga, bool isread)
        {
            Dictionary<uint, byte> result = new Dictionary<uint, byte>();
            if (!skills2.ContainsKey(pc.Job3)) return SkillList(pc.Job);

            foreach (KeyValuePair<uint, PreconditionSkill> skill in skills2[pc.Job3])
            {
                if (skill.Value.paga != paga) continue;
                if (isread)
                    result.Add(skill.Key, (byte)skill.Value.LearnSkillJobLv);
                else
                {
                    byte count = 0;
                    foreach (KeyValuePair<uint, uint> ps in skill.Value.PreconditionSkillInfo)
                    {
                        if (!pc.Skills.ContainsKey(ps.Key) && !pc.Skills2_1.ContainsKey(ps.Key) && !pc.Skills2_2.ContainsKey(ps.Key) && !pc.Skills3.ContainsKey(ps.Key)) continue;
                        if (pc.Skills.ContainsKey(ps.Key)) if (pc.Skills[ps.Key].Level < ps.Value) continue;
                        if (pc.Skills2_1.ContainsKey(ps.Key)) if (pc.Skills2_1[ps.Key].Level < ps.Value) continue;
                        if (pc.Skills2_2.ContainsKey(ps.Key)) if (pc.Skills2_2[ps.Key].Level < ps.Value) continue;
                        if (pc.Skills3.ContainsKey(ps.Key)) if (pc.Skills3[ps.Key].Level < ps.Value) continue;
                        count++;
                    }
                    if (count == skill.Value.PreconditionSkillInfo.Count && skill.Value.LearnSkillJobLv <= pc.JobLevel3)
                        result.Add(skill.Key, (byte)skill.Value.LearnSkillJobLv);
                }
            }
            return result;
        }

        public Dictionary<uint, byte> SkillList(PC_JOB job)
        {
            if (this.skills.ContainsKey(job))
                return this.skills[job];
            else
                return new Dictionary<uint, byte>();
        }

        public void LoadSkillList2(string path)
        {
            string[] file = SagaLib.VirtualFileSystem.VirtualFileSystemManager.Instance.FileSystem.SearchFile(path, "*.xml");
            int total = 0;
            foreach (string f in file)
            {
                total += LoadOne(f);
            }
            Logger.ShowInfo("Skill list for jobs loaded...");
        }
        public int LoadOne(string f)
        {
            int total = 0;
            XmlDocument xml = new XmlDocument();
            try
            {
                XmlElement root;
                XmlNodeList list;
                System.IO.Stream fs = SagaLib.VirtualFileSystem.VirtualFileSystemManager.Instance.FileSystem.OpenFile(f);
                xml.Load(fs);
                root = xml["SkillList"];
                list = root.ChildNodes;
                foreach (object j in list)
                {
                    XmlElement i;
                    if (j.GetType() != typeof(XmlElement)) continue;
                    i = (XmlElement)j;
                    switch (i.Name.ToLower())
                    {
                        case "skills":
                            Actor.PC_JOB job;
                            job = (PC_JOB)Enum.Parse(typeof(Actor.PC_JOB), i.Attributes["Job"].Value, true);
                            Dictionary<uint, PreconditionSkill> list2;
                            if (!this.skills2.ContainsKey(job))
                            {
                                list2 = new Dictionary<uint, PreconditionSkill>();
                                this.skills2.Add(job, list2);
                            }
                            else
                            {
                                list2 = this.skills2[job];
                            }
                            XmlNodeList skills = i.ChildNodes;
                            foreach (object l in skills)
                            {
                                XmlElement k;
                                if (l.GetType() != typeof(XmlElement)) continue;
                                k = (XmlElement)l;
                                switch (k.Name.ToLower())
                                {
                                    case "skill":
                                        PreconditionSkill ps = new PreconditionSkill();
                                        uint LearnSkillID = uint.Parse(k.Attributes["ID"].InnerText);
                                        ps.paga = (SkillPaga)Enum.Parse(typeof(SkillPaga), k.Attributes["Page"].InnerText);
                                        ps.LearnSkillJobLv = uint.Parse(k.Attributes["JobLV"].InnerText);
                                        list2.Add(LearnSkillID, ps);
                                        //uint JobLv_ = uint.Parse(k.Attributes["JobLV"].InnerText);

                                        XmlNodeList PreconditionSkills = k.ChildNodes;
                                        foreach (object l2 in PreconditionSkills)
                                        {
                                            XmlElement k2;
                                            if (l2.GetType() != typeof(XmlElement)) continue;
                                            k2 = (XmlElement)l2;
                                            switch (k2.Name)
                                            {
                                                case "PreconditionSkill":
                                                    uint PreconditionSkillLv = uint.Parse(k2.Attributes["SkillLv"].InnerText);
                                                    uint PreconditionSkillID = uint.Parse(k2.InnerText);
                                                    if (!list2[LearnSkillID].PreconditionSkillInfo.ContainsKey(PreconditionSkillID))
                                                        list2[LearnSkillID].PreconditionSkillInfo.Add(PreconditionSkillID, PreconditionSkillLv);
                                                    break;
                                            }
                                        }
                                        break;
                                }
                            }
                            break;
                    }
                }
            }
            catch (Exception ex) { SagaLib.Logger.ShowError(ex); }
            return total;
        }
        public void LoadSkillList(string path)
        {
            sklstpath = path;
            Logger.ShowInfo("Now Loading Skill Tree...");
            XmlDocument xml = new XmlDocument();
            try
            {
                XmlElement root;
                XmlNodeList list;
                xml.Load(VirtualFileSystemManager.Instance.FileSystem.OpenFile(path));
                root = xml["SkillList"];
                list = root.ChildNodes;
                foreach (object j in list)
                {
                    XmlElement i;
                    if (j.GetType() != typeof(XmlElement)) continue;
                    i = (XmlElement)j;
                    switch (i.Name.ToLower())
                    {
                        case "skills":
                            Actor.PC_JOB job;
                            job = (PC_JOB)Enum.Parse(typeof(Actor.PC_JOB), i.Attributes["Job"].Value, true);
                            Dictionary<uint, byte> list2;
                            if (!this.skills.ContainsKey(job))
                            {
                                list2 = new Dictionary<uint, byte>();
                                this.skills.Add(job, list2);
                            }
                            else
                            {
                                list2 = this.skills[job];
                            }
                            XmlNodeList skills = i.ChildNodes;
                            foreach (object l in skills)
                            {
                                XmlElement k;
                                if (l.GetType() != typeof(XmlElement)) continue;
                                k = (XmlElement)l;
                                switch (k.Name.ToLower())
                                {
                                    case "skillid":

                                        list2.Add(uint.Parse(k.InnerText), byte.Parse(k.Attributes["JobLV"].InnerText));
                                        break;
                                }
                            }
                            break;
                    }
                }
                Logger.ShowInfo("Done Loaded Skill Tree...");
            }
            catch (Exception ex)
            {
                Logger.ShowError(ex);
            }
        }

        public void Convert(string path, System.Text.Encoding encoding)
        {
            System.IO.StreamReader sr = new System.IO.StreamReader(path, encoding);
            System.IO.StreamWriter sw = new System.IO.StreamWriter(path + ".csv", false, encoding);
            sw.WriteLine("#ID,Name,主动,最大Lv,Lv,JobLv,MP,SP,吟唱时间,延迟,射程,目标,目标2,范围,技能释放射程");
            Logger.ShowInfo("Loading skill database...");
            Console.ForegroundColor = ConsoleColor.Green;
            int count = 0;
            bool print = true;
            string[] paras;
            while (!sr.EndOfStream)
            {
                string line;
                line = sr.ReadLine();
                try
                {
                    SkillData skill;
                    if (line == "") continue;
                    if (line.Substring(0, 1) == "#")
                        continue;
                    paras = line.Split(',');

                    for (int i = 0; i < paras.Length; i++)
                    {
                        if (paras[i] == "")
                            paras[i] = "0";
                    }
                    skill = new SkillData();
                    skill.id = uint.Parse(paras[0]);
                    skill.name = paras[1];
                    if (paras[3] == "1")
                        skill.active = true;
                    else
                        skill.active = false;
                    skill.maxLv = byte.Parse(paras[4]);
                    skill.lv = byte.Parse(paras[5]);
                    skill.mp = byte.Parse(paras[6]);
                    skill.sp = byte.Parse(paras[7]);
                    skill.range = sbyte.Parse(paras[9]);
                    skill.target = byte.Parse(paras[10]);
                    skill.target2 = byte.Parse(paras[11]);
                    skill.effectRange = byte.Parse(paras[12]);
                    skill.castRange = (byte)ushort.Parse(paras[17]);
                    //#ID,Name,主动,最大Lv,Lv,JobLv,MP,SP,吟唱时间,延迟,射程,目标,目标2,范围,技能释放射程

                    sw.WriteLine(string.Format("{0},{1},{2},{3},{4},{5},{6},{7},{8},{9},{10},{11},{12},{13},{14}", skill.id, skill.name, paras[3], skill.maxLv, skill.lv,
                        1, skill.mp, skill.sp, 10, 10, skill.range, skill.target, skill.target2, skill.effectRange, skill.castRange));
                    double perc = (double)sr.BaseStream.Position / sr.BaseStream.Length;
                    if ((int)(perc * 100) % 3 == 0)
                    {
                        if (print)
                        {
                            Console.Write("*");
                            print = false;
                        }
                    }
                    else
                    {
                        print = true;
                    }
                    count++;
                }
                catch (Exception ex)
                {
                    Logger.ShowError("Error on parsing skill db!\r\nat line:" + line);
                    Logger.ShowError(ex);
                }
            }
            Console.WriteLine();
            Console.ResetColor();
            Logger.ShowInfo(count + " skills loaded.");
            sw.Flush();
            sw.Close();
            sr.Close();
        }
        public void InitSSP(string path, System.Text.Encoding encoding)
        {
            skdbpath = path;
            Logger.ShowInfo("Now Loading Skill Data...");
            List<sspHeader> header = new List<sspHeader>();
            System.Text.Encoding encoder = encoding;
            string line = "";
            int count = 0;
            try
            {
                DateTime time = DateTime.Now;
                FileStream fs = new FileStream(path, FileMode.Open, FileAccess.ReadWrite);
                BinaryReader br = new BinaryReader(fs);
                for (int i = 0; i < 30000; i++) //技能最多可以有32767个，不是10000个（日服技能已经超过1w个）
                {
                    sspHeader newHead = new sspHeader()
                    {
                        offset = br.ReadUInt32(),
                        size = 0x02d8
                    };
                    if (newHead.offset == 0)
                        break;
                    header.Add(newHead);
                }
                foreach (sspHeader i in header)
                {
                    fs.Position = i.offset;
                    SkillData tskill = new SkillData() { id = br.ReadUInt16() };
                    if (tskill.id == 0)
                        continue;
                    tskill.icon = br.ReadUInt16();
                    //tskill.Name = br.ReadChars(116);
                    string buf = Encoding.Unicode.GetString(br.ReadBytes(116));//名称后12字节被设定为castTime、delay、singleCD
                    tskill.name = buf.Remove(buf.IndexOf('\0'));

                    tskill.castTime = br.ReadInt32();
                    tskill.delay = br.ReadInt32();
                    tskill.SingleCD = br.ReadInt32();

                    buf = Encoding.Unicode.GetString(br.ReadBytes(512));
                    tskill.description = buf.Remove(buf.IndexOf('\0'));//.Replace("$R", Environment.NewLine);

                    tskill.active = br.ReadByte() == 0x1 ? true : false;
                    tskill.maxLv = br.ReadByte();
                    tskill.lv = br.ReadByte();
                    tskill.joblv = br.ReadByte();
                    tskill.mp = br.ReadUInt16();
                    tskill.sp = br.ReadUInt16();
                    tskill.ep = br.ReadUInt16();
                    tskill.range = br.ReadSByte();
                    tskill.target = br.ReadByte();
                    tskill.target2 = br.ReadByte();
                    tskill.effectRange = br.ReadByte();
                    tskill.equipFlag.Value = br.ReadInt32();
                    tskill.nHumei2 = br.ReadInt32();
                    tskill.skillFlag = br.ReadInt32();

                    string b = string.Format("0x{0:X8}", tskill.skillFlag).Replace("0x", "");
                    tskill.flag.Value = (int)Conversions.HexStr2uint(b)[0];

                    tskill.eFlag1 = br.ReadInt32();
                    tskill.eFlag2 = br.ReadInt32();
                    tskill.eFlag3 = br.ReadInt16();

                    //tskill.nHumei4 = br.ReadUInt16();
                    tskill.nHumei5 = br.ReadUInt16();
                    tskill.nHumei6 = br.ReadUInt16();
                    tskill.nHumei7 = br.ReadUInt16();   //类型
                    //tskill.nHumei9 = br.ReadUInt16();    //射程
                    //tskill.nHumei10 = br.ReadUInt16();

                    tskill.effect1 = br.ReadInt32();
                    tskill.effect2 = br.ReadInt32();
                    tskill.effect3 = br.ReadInt32();

                    //tskill.nHumei8 = br.ReadInt32();

                    tskill.effect4 = br.ReadInt32();
                    tskill.effect5 = br.ReadInt32();
                    tskill.effect6 = br.ReadInt32();
                    tskill.effect7 = br.ReadInt32();
                    tskill.effect8 = br.ReadInt32();
                    tskill.effect9 = br.ReadInt32();

                    tskill.nAnim1 = br.ReadUInt16();
                    tskill.nAnim2 = br.ReadUInt16();
                    tskill.nAnim3 = br.ReadUInt16();

                    if (!items.ContainsKey(tskill.id))
                        items.Add(tskill.id, new Dictionary<byte, SkillData>());
                    if (!items[tskill.id].ContainsKey(tskill.lv))
                        items[tskill.id].Add(tskill.lv, tskill);
                    count++;
                }
                Logger.ProgressBarHide("Done Loaded " + count + " skills.  use time: " + (DateTime.Now - time).TotalMilliseconds.ToString() + "ms");
                fs.Close();
                br.Close();
            }
            catch (Exception ex)
            {
                Logger.ShowError("技能DB解析错误!\r\n行信息: " + line);
                Logger.ShowError(ex);
            }
        }
        public void Init(string path, System.Text.Encoding encoding)
        {
            System.IO.StreamReader sr = new System.IO.StreamReader(VirtualFileSystemManager.Instance.FileSystem.OpenFile(path), encoding);
#if !Web
            string label = "Loading skill database";
            Logger.ProgressBarShow(0, (uint)sr.BaseStream.Length, label);
#endif
            DateTime time = DateTime.Now;
            int count = 0;
            string[] paras;
            while (!sr.EndOfStream)
            {
                string line;
                line = sr.ReadLine();
                try
                {
                    SkillData skill;
                    if (line == "") continue;
                    if (line.Substring(0, 1) == "#")
                        continue;
                    paras = line.Split(',');

                    for (int i = 0; i < paras.Length; i++)
                    {
                        if (paras[i] == "")
                            paras[i] = "0";
                    }
                    skill = new SkillData();
                    skill.id = uint.Parse(paras[0]);
                    skill.name = paras[1];
                    if (paras[2] == "1")
                        skill.active = true;
                    else
                        skill.active = false;
                    skill.maxLv = byte.Parse(paras[3]);
                    skill.lv = byte.Parse(paras[4]);
                    //skill.joblv = byte.Parse(paras[5]);
                    skill.mp = ushort.Parse(paras[6]);
                    skill.sp = ushort.Parse(paras[7]);
                    skill.castTime = int.Parse(paras[8]);
                    skill.delay = int.Parse(paras[9]);
                    skill.range = sbyte.Parse(paras[10]);
                    skill.target = byte.Parse(paras[11]);
                    skill.target2 = byte.Parse(paras[12]);
                    skill.effectRange = byte.Parse(paras[13]);
                    skill.castRange = (byte)ushort.Parse(paras[14]);
                    skill.flag.Value = (int)Conversions.HexStr2uint(paras[16].Replace("0x", ""))[0];
                    skill.equipFlag.Value = (int)Conversions.HexStr2uint(paras[15].Replace("0x", ""))[0];
                    //skill.effect = uint.Parse(paras[15]);

                    if (!items.ContainsKey(skill.id))
                        items.Add(skill.id, new Dictionary<byte, SkillData>());
                    items[skill.id].Add(skill.lv, skill);
#if !Web
                    if ((DateTime.Now - time).TotalMilliseconds > 10)
                    {
                        time = DateTime.Now;
                        Logger.ProgressBarShow((uint)sr.BaseStream.Position, (uint)sr.BaseStream.Length, label);
                    }
#endif
                    count++;
                }
                catch (Exception ex)
                {
#if !Web
                    Logger.ShowError("Error on parsing skill db!\r\nat line:" + line);
                    Logger.ShowError(ex);
#endif
                }
            }
            Logger.ProgressBarHide(count + " skills loaded.");
            sr.Close();
        }
    }
}
