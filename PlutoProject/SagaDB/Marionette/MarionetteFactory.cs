using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using SagaLib;
using SagaLib.VirtualFileSystem;
using SagaDB.Mob;

namespace SagaDB.Marionette
{
    public class MarionetteFactory : Singleton<MarionetteFactory>
    {
        public Dictionary<uint, Marionette> Items = new Dictionary<uint, Marionette>();
        public MarionetteFactory()
        {
        }

        public Marionette this[uint index]
        {
            get
            {
                if (Items.ContainsKey(index))
                    return Items[index];
                else
                    return null;
            }
        }

        public void Init(string path, System.Text.Encoding encoding)
        {
            System.IO.StreamReader sr = new System.IO.StreamReader(VirtualFileSystemManager.Instance.FileSystem.OpenFile(path), encoding);
            Logger.ShowInfo("Loading marionette database...");
            Console.ForegroundColor = ConsoleColor.Green;
            int count = 0;
            string[] paras;
            while (!sr.EndOfStream)
            {
                string line;
                line = sr.ReadLine();
                try
                {
                    Marionette mob;
                    if (line == "") continue;
                    if (line.Substring(0, 1) == "#")
                        continue;
                    paras = line.Split(',');

                    for (int i = 0; i < paras.Length; i++)
                    {
                        if (paras[i] == "" || paras[i].ToLower() == "null")
                            paras[i] = "0";
                    }
                    mob = new Marionette();
                    mob.ID = uint.Parse(paras[0]);
                    mob.Name = paras[1];
                    mob.PictID = uint.Parse(paras[2]);
                    mob.MobType = (MobType)Enum.Parse(typeof(MobType), paras[3]);
                    mob.Duration = int.Parse(paras[4]);
                    mob.Delay = int.Parse(paras[5]);
                    mob.str = short.Parse(paras[24]);
                    mob.mag = short.Parse(paras[25]);
                    mob.vit = short.Parse(paras[26]);
                    mob.dex = short.Parse(paras[27]);
                    mob.agi = short.Parse(paras[28]);
                    mob.intel = short.Parse(paras[29]);
                    mob.hp = short.Parse(paras[32]);
                    mob.sp = short.Parse(paras[33]);
                    mob.mp = short.Parse(paras[34]);
                    mob.move_speed = short.Parse(paras[35]);
                    mob.min_atk1 = short.Parse(paras[36]);
                    mob.max_atk1 = short.Parse(paras[37]);
                    mob.min_atk2 = short.Parse(paras[38]);
                    mob.max_atk2 = short.Parse(paras[39]);
                    mob.min_atk3 = short.Parse(paras[40]);
                    mob.max_atk3 = short.Parse(paras[41]);
                    mob.min_matk = short.Parse(paras[42]);
                    mob.max_matk = short.Parse(paras[43]);
                    mob.def_add = short.Parse(paras[44]);
                    mob.def = short.Parse(paras[45]);
                    mob.mdef_add = short.Parse(paras[46]);
                    mob.mdef = short.Parse(paras[47]);
                    mob.hit_melee = short.Parse(paras[48]);
                    mob.hit_ranged = short.Parse(paras[49]);
                    mob.hit_magic = short.Parse(paras[50]);
                    mob.avoid_melee = short.Parse(paras[51]);
                    mob.avoid_ranged = short.Parse(paras[52]);
                    mob.avoid_magic = short.Parse(paras[53]);
                    mob.hit_cri = short.Parse(paras[54]);
                    mob.avoid_cri = short.Parse(paras[55]);
                    mob.hp_recover = short.Parse(paras[56]);
                    mob.mp_recover = short.Parse(paras[57]);
                    mob.aspd = short.Parse(paras[58]);
                    mob.cspd = short.Parse(paras[59]);
                    for (int i = 0; i < 6; i++)
                    {
                        if (paras[77 + i] != "0")
                            mob.skills.Add(ushort.Parse(paras[77 + i]));
                    }
                    for (int i = 0; i < 8; i++)
                    {
                        if (paras[83 + i] == "1")
                            mob.gather.Add((GatherType)i, true);
                        else
                            mob.gather.Add((GatherType)i, false);
                    }

                    Items.Add(mob.ID, mob);                   
                    count++;
                }
                catch (Exception ex)
                {
                    Logger.ShowError("Error on parsing marionette db!\r\nat line:" + line);
                    Logger.ShowError(ex);
                }
            }
            Console.ResetColor();
            Logger.ShowInfo(count + " marionette loaded.");
            sr.Close();
        }
    }
}
