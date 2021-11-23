using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using SagaLib;
using SagaDB.Actor;
using SagaLib.VirtualFileSystem;
namespace SagaDB.Item
{
    public class AnotherFactory : Singleton<AnotherFactory>
    {
        Dictionary<uint, Dictionary<byte, Another>> another = new Dictionary<uint, Dictionary<byte, Another>>();
        public Dictionary<uint, Dictionary<byte, Another>> AnotherPapers { get { return another; } }
        public void Init(string path, System.Text.Encoding encoding)
        {
            System.IO.StreamReader sr = new System.IO.StreamReader(VirtualFileSystemManager.Instance.FileSystem.OpenFile(path), encoding);

            DateTime time = DateTime.Now;

            string[] paras;
            while (!sr.EndOfStream)
            {
                string line;
                line = sr.ReadLine();
                try
                {
                    if (line == "") continue;
                    if (line.Substring(0, 1) == "#")
                        continue;
                    paras = line.Split(',');
                    Another paper = new Another();
                    paper.id = uint.Parse(paras[0]);
                    paper.name = paras[1];
                    paper.type = byte.Parse(paras[2]);
                    paper.lv = byte.Parse(paras[3]);
                    for (int i = 0; i < 8; i++)
                    {
                        paper.paperItems1.Add(uint.Parse(paras[5 + i]));
                    }
                    for (int i = 0; i < 8; i++)
                    {
                        paper.paperItems2.Add(uint.Parse(paras[13 + i]));
                    }
                    paper.requestItem1 = uint.Parse(paras[21]);
                    paper.requestItem2 = uint.Parse(paras[22]);
                    paper.awakeSkillID = uint.Parse(paras[23]);
                    paper.awakeSkillMaxLV = byte.Parse(paras[24]);
                    for (int i = 0; i < 5; i++)
                    {
                        uint skillid = uint.Parse(paras[25 + i * 2]);
                        byte skillMaxLV = byte.Parse(paras[26 + i * 2]);
                        if (!paper.skills.ContainsKey(skillid))
                            paper.skills.Add(skillid, new List<byte>());
                        paper.skills[skillid].Add(skillMaxLV);
                    }
                    paper.str = ushort.Parse(paras[35]);
                    paper.mag = ushort.Parse(paras[36]);
                    paper.vit = ushort.Parse(paras[37]);
                    paper.dex = ushort.Parse(paras[38]);
                    paper.agi = ushort.Parse(paras[39]);
                    paper.ing = ushort.Parse(paras[40]);
                    paper.hp_add = short.Parse(paras[41]);
                    paper.mp_add = short.Parse(paras[42]);
                    paper.sp_add = short.Parse(paras[43]);
                    paper.min_atk_add = ushort.Parse(paras[44]);
                    paper.max_atk_add = ushort.Parse(paras[45]);
                    paper.min_matk_add = ushort.Parse(paras[46]);
                    paper.max_matk_add = ushort.Parse(paras[47]);
                    paper.def_add = ushort.Parse(paras[48]);
                    paper.mdef_add = ushort.Parse(paras[49]);
                    paper.hit_melee_add = ushort.Parse(paras[50]);
                    paper.hit_magic_add = ushort.Parse(paras[51]);
                    paper.avoid_melee_add = ushort.Parse(paras[52]);
                    paper.avoid_magic_add = ushort.Parse(paras[53]);
                    if (!another.ContainsKey(paper.id))
                        another.Add(paper.id, new Dictionary<byte, Another>());
                    another[paper.id].Add(paper.lv, paper);
                }
                catch (Exception ex)
                {
                    Logger.ShowError(ex);
                }
            }
            sr.Close();
        }
        public byte GetPaperLv(ulong value)
        {
            byte lv = 0;
            if (value >= 0xff) lv = 1;
            if (value >= 0xffff) lv = 2;
            if (value >= 0xffffff) lv = 3;
            if (value >= 0xffffffff) lv = 4;
            if (value >= 0xffffffffff) lv = 5;
            return lv;
        }
        public ulong GetPaperValue(byte paperID, byte lv, uint ItemID)
        {
            ulong value = 0;
            if (!AnotherPapers.ContainsKey(paperID)) return 0;
            if (!AnotherPapers[paperID].ContainsKey(lv)) return 0;
            if (!AnotherPapers[paperID][lv].paperItems1.Contains(ItemID)) return 0;
            int index = AnotherPapers[paperID][lv].paperItems1.IndexOf(ItemID);
            switch (index)
            {
                case 0:
                    value = 0x1;
                    break;
                case 1:
                    value = 0x2;
                    break;
                case 2:
                    value = 0x4;
                    break;
                case 3:
                    value = 0x8;
                    break;
                case 4:
                    value = 0x10;
                    break;
                case 5:
                    value = 0x20;
                    break;
                case 6:
                    value = 0x40;
                    break;
                case 7:
                    value = 0x80;
                    break;
            }
            if (lv > 1)
                value = value * (ulong)((lv - 1) * 0x100);
            return value;
        }
    }
}
