using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using SagaLib;
using SagaDB.Actor;
using SagaLib.VirtualFileSystem;
using System.Xml;
namespace SagaDB.Title
{
    public class TitleFactory : Singleton<TitleFactory>
    {
        public Dictionary<uint, Title> TitleList = new Dictionary<uint, Title>();

        public void Init(string path, System.Text.Encoding encoding)
        {
            System.IO.StreamReader sr = new System.IO.StreamReader(VirtualFileSystemManager.Instance.FileSystem.OpenFile(path), encoding);

            DateTime time = DateTime.Now;

            try
            {
                string[] paras;
                while (!sr.EndOfStream)
                {
                    string line;
                    line = sr.ReadLine();
                    try
                    {
                        if (line == "" || line.Substring(0, 1) == "#" || line.Substring(0, 6) == ",,,,,,")
                            continue;
                        paras = line.Split(',');
                        Title item = new Title();
                        item.ID = uint.Parse(paras[0]);
                        item.typename = paras[2];
                        item.rate = byte.Parse(paras[3]);
                        item.name = paras[4];
                        item.hp = uint.Parse(paras[63]);
                        item.mp = uint.Parse(paras[64]);
                        item.sp = uint.Parse(paras[65]);
                        item.atk_min = uint.Parse(paras[66]);
                        item.atk_max = uint.Parse(paras[67]);
                        item.matk_min = uint.Parse(paras[72]);
                        item.matk_max = uint.Parse(paras[73]);
                        item.def = uint.Parse(paras[74]);
                        item.mdef = uint.Parse(paras[75]);
                        item.hit_melee = uint.Parse(paras[76]);
                        item.hit_range = uint.Parse(paras[77]);
                        item.hit_magic = uint.Parse(paras[78]);
                        item.avoid_melee = uint.Parse(paras[79]);
                        item.avoid_range = uint.Parse(paras[80]);
                        item.avoid_magic = uint.Parse(paras[81]);
                        item.cri = uint.Parse(paras[82]);
                        item.cri_avoid = uint.Parse(paras[83]);
                        item.aspd = uint.Parse(paras[84]);
                        item.cspd = uint.Parse(paras[85]);
                        item.ConCount = uint.Parse(paras[88]);
                        if (!TitleList.ContainsKey(item.ID))
                            TitleList.Add(item.ID, item);
                    }
                    catch (Exception ex)
                    {
                        Logger.ShowError(ex);
                    }
                }
                sr.Close();
            }
            catch(Exception ex)
            {
                SagaLib.Logger.ShowError(ex);
            }
        }
        public void InitB(string path, System.Text.Encoding encoding)
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
                    Dictionary<uint, ushort> items = new Dictionary<uint, ushort>();
                    uint ID = uint.Parse(paras[0]);
                    byte count = byte.Parse(paras[1]);
                    for (int i = 1; i <= count; i++)
                    {
                        uint itemID = uint.Parse(paras[i * 2]);
                        ushort stack = ushort.Parse(paras[1 + i * 2]);
                        items.Add(itemID, stack);
                    }
                    if (TitleList.ContainsKey(ID))
                        TitleList[ID].Bouns = items;
                }
                catch (Exception ex)
                {
                    Logger.ShowError(ex);
                }
            }
            sr.Close();
        }
    }
}
