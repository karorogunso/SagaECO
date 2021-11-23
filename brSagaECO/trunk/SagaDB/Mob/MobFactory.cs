using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using SagaLib;
using SagaLib.VirtualFileSystem;
using SagaDB.Actor;

namespace SagaDB.Mob
{
    public class MobFactory : Singleton<MobFactory>
    {
        Dictionary<uint, MobData> items = new Dictionary<uint, MobData>();
        Dictionary<uint, MobData> pets = new Dictionary<uint, MobData>();
        Dictionary<uint, MobData> petLimits = new Dictionary<uint, MobData>();

        public MobFactory()
        {

        }

        public MobData GetMobData(uint id)
        {
            return this.items[id];
        }

        public MobData GetPetData(uint id)
        {
            return this.pets[id];
        }

        public MobData GetPetLimit(uint id)
        {
            if (petLimits.ContainsKey(id))
                return this.petLimits[id];
            else
                return this.petLimits[10010003];
        }


        public Dictionary<uint, MobData> Mobs { get { return this.items; } }

        public void Init(string path, System.Text.Encoding encoding)
        {

            System.IO.StreamReader sr = new System.IO.StreamReader(VirtualFileSystemManager.Instance.FileSystem.OpenFile(path), encoding);
            int count = 0;
#if !Web
            string label = "Loading mob database";
            Logger.ProgressBarShow(0, (uint)sr.BaseStream.Length, label);
#endif
            DateTime time = DateTime.Now;
            string[] paras;
            while (!sr.EndOfStream)
            {
                string line;
                line = sr.ReadLine();
                try
                {
                    MobData mob;
                    if (line == "") continue;
                    if (line.Substring(0, 1) == "#")
                        continue;
                    paras = line.Split(',');

                    for (int i = 0; i < paras.Length; i++)
                    {
                        if (paras[i] == "" || paras[i].ToLower() == "null")
                            paras[i] = "0";
                    }
                    mob = new Mob.MobData();
                    mob.id = uint.Parse(paras[0]);
                    mob.name = paras[1];
                    mob.pictid = uint.Parse(paras[2]);
                    mob.mobType = (MobType)Enum.Parse(typeof(MobType), paras[3].Replace(" ", "_"));
                    mob.mobSize = float.Parse(paras[4]);
                    if (paras[7] == "1")
                        mob.fly = true;
                    if (paras[8] == "1")
                        mob.undead = true;
                    mob.level = byte.Parse(paras[10]);
                    mob.str = ushort.Parse(paras[11]);
                    mob.mag = ushort.Parse(paras[12]);
                    mob.vit = ushort.Parse(paras[13]);
                    mob.dex = ushort.Parse(paras[14]);
                    mob.agi = ushort.Parse(paras[15]);
                    mob.intel = ushort.Parse(paras[16]);
                    mob.hp = uint.Parse(paras[19]);
                    mob.mp = uint.Parse(paras[20]);
                    mob.sp = uint.Parse(paras[21]);
                    mob.speed = ushort.Parse(paras[22]);
                    mob.atk_min = ushort.Parse(paras[23]);
                    mob.atk_max = ushort.Parse(paras[24]);
                    mob.attackType = (ATTACK_TYPE)Enum.Parse(typeof(ATTACK_TYPE), paras[25]);
                    mob.matk_min = ushort.Parse(paras[26]);
                    mob.matk_max = ushort.Parse(paras[27]);
                    mob.def_add = ushort.Parse(paras[28]);
                    mob.def = ushort.Parse(paras[29]);
                    mob.mdef_add = ushort.Parse(paras[30]);
                    mob.mdef = ushort.Parse(paras[31]);
                    mob.physicreduce = (float.Parse(paras[32]) / 100.0f);
                    mob.magicreduce = (float.Parse(paras[33]) / 100.0f);
                    mob.hit_melee = ushort.Parse(paras[34]);
                    mob.hit_ranged = ushort.Parse(paras[35]);
                    mob.hit_magic = ushort.Parse(paras[36]);
                    mob.avoid_melee = ushort.Parse(paras[37]);
                    mob.avoid_ranged = ushort.Parse(paras[38]);
                    mob.avoid_magic = ushort.Parse(paras[39]);
                    mob.cri = ushort.Parse(paras[40]);
                    mob.criavd = ushort.Parse(paras[41]);
                    mob.resilience = short.Parse(paras[42]);//新加
                    mob.aspd = short.Parse(paras[44]);
                    mob.cspd = short.Parse(paras[45]);
                    mob.range = float.Parse(paras[46]);//新加

                    for (int i = 0; i < 7; i++)
                    {
                        mob.elements.Add((Elements)i, int.Parse(paras[47 + i]));
                    }
                    for (int i = 0; i < 9; i++)
                    {
                        mob.abnormalStatus.Add((AbnormalStatus)i, short.Parse(paras[54 + i]));
                    }
                    //mob.aiMode = int.Parse(paras[63]);
                    //mob.baseExp = (uint)(Math.Round(float.Parse(paras[108]), MidpointRounding.AwayFromZero));
                    //mob.jobExp = (uint)(Math.Round(float.Parse(paras[109]), MidpointRounding.AwayFromZero));
                    //mob.baseExp = 0;
                    //mob.jobExp = 0;
                    mob.baseExp = uint.Parse(paras[108]);
                    mob.jobExp = uint.Parse(paras[109]);

                    mob.dropRate = Mob.DropGroupFactory.Instance.GetDropRate(ushort.Parse(paras[112]));

                    for (int i = 0; i < 7; i++)
                    {
                        if (paras[113 + i] != "0")
                        {
                            try
                            {
                                //修复怪物掉落的加载
                                // 一共有7个掉落, 前5个为普通掉落只要击杀都有可能掉落, 后2个为特殊掉落需要怪物知识?.
                                // 掉落设置方式为 :
                                // 团队分享[P_道具ID]
                                // 公共分享[E_道具ID]
                                // 如果掉落设置为[宝箱组Name]
                                // 正常设置为 [P_/E_宝箱组Name] ex: [P_BOMB]
                                MobData.DropData newDrop = new MobData.DropData();
                                string args = paras[113 + i];
                                newDrop.Public = args.StartsWith("E_");
                                newDrop.Party = args.StartsWith("P_");
                                if (newDrop.Public)
                                    args = args.Substring(2);

                                if (newDrop.Party)
                                    args = args.Substring(2);

                                if (!uint.TryParse(args, out newDrop.ItemID))
                                {
                                    newDrop.TreasureGroup = args;
#if !Web
                                    if (!Treasure.TreasureFactory.Instance.Items.ContainsKey(newDrop.TreasureGroup))
                                    {
                                        Logger.ShowWarning("Can't find Drop Group: " + newDrop.TreasureGroup + " for Monster: " + mob.id);
                                        continue;
                                    }
#endif
                                }
                                newDrop.Rate = int.Parse(mob.dropRate[i]);

                                //如果不存在这个物品ID,并且没设置宝箱组,就跳过
                                if (newDrop.ItemID == 0 && newDrop.TreasureGroup == "")
                                    continue;

                                if (i < 5)
                                    mob.dropItems.Add(newDrop);
                                else
                                    mob.dropItemsSpecial.Add(newDrop);
                            }
                            catch (Exception) { }
                        }
                    }
                    if (paras[120] != "0")
                    {
                        MobData.DropData newDrop = new MobData.DropData();
                        newDrop.ItemID = uint.Parse(paras[120]);
                        //开启印章掉落可是件很危险的事...
                        //newDrop.Rate = int.Parse(paras[121]);
                        newDrop.Rate = int.Parse(mob.dropRate[7]);
                        mob.stampDrop = newDrop;
                    }
                    if (items.ContainsKey(mob.id))
                    {
                        Logger.ShowError("the monster id:" + mob.id.ToString() + ",[" + items[mob.id].name + "]is exist." + ",monster[" + mob.name + "]doesn't loaded.");
                    }
                    else
                        items.Add(mob.id, mob);
#if !Web
                    if ((DateTime.Now - time).TotalMilliseconds > 40)
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
                    Logger.ShowError("Error on parsing mob db!\r\nat line:" + line);
                    Logger.ShowError(ex);
#endif
                }
            }
#if !Web
            Logger.ProgressBarHide(count + " mobs loaded.");
#endif
            sr.Close();
        }

        public void InitPet(string path, System.Text.Encoding encoding)
        {
            System.IO.StreamReader sr = new System.IO.StreamReader(VirtualFileSystemManager.Instance.FileSystem.OpenFile(path), encoding);
            int count = 0;
#if !Web
            string label = "Loading pet database";
            Logger.ProgressBarShow(0, (uint)sr.BaseStream.Length, label);
#endif
            DateTime time = DateTime.Now;
            string[] paras;
            while (!sr.EndOfStream)
            {
                string line;
                line = sr.ReadLine();
                try
                {
                    MobData mob;
                    if (line == "") continue;
                    if (line.Substring(0, 1) == "#")
                        continue;
                    paras = line.Split(',');

                    for (int i = 0; i < paras.Length; i++)
                    {
                        if (paras[i] == "" || paras[i].ToLower() == "null")
                            paras[i] = "0";
                    }
                    mob = new Mob.MobData();
                    mob.id = uint.Parse(paras[0]);
                    mob.name = paras[1];
                    mob.pictid = uint.Parse(paras[2]);
                    try
                    {
                        mob.mobType = (MobType)Enum.Parse(typeof(MobType), paras[3]);
                    }
                    catch (Exception)
                    {
                        while (paras[3].Substring(paras[3].Length - 1) != "_")
                        {
                            paras[3] = paras[3].Substring(0, paras[3].Length - 1);
                        }
                        paras[3] = paras[3].Substring(0, paras[3].Length - 1);
                        mob.mobType = (MobType)Enum.Parse(typeof(MobType), paras[3]);
                    }
                    mob.mobSize = float.Parse(paras[4]);
                    mob.level = byte.Parse(paras[10]);
                    mob.str = ushort.Parse(paras[11]);
                    mob.mag = ushort.Parse(paras[12]);
                    mob.vit = ushort.Parse(paras[13]);
                    mob.dex = ushort.Parse(paras[14]);
                    mob.agi = ushort.Parse(paras[15]);
                    mob.intel = ushort.Parse(paras[16]);
                    mob.hp = uint.Parse(paras[19]);
                    mob.mp = uint.Parse(paras[20]);
                    mob.sp = uint.Parse(paras[21]);
                    mob.speed = ushort.Parse(paras[22]);
                    mob.atk_min = ushort.Parse(paras[23]);
                    mob.atk_max = ushort.Parse(paras[24]);
                    mob.attackType = (ATTACK_TYPE)Enum.Parse(typeof(ATTACK_TYPE), paras[25]);
                    mob.matk_min = ushort.Parse(paras[26]);
                    mob.matk_max = ushort.Parse(paras[27]);
                    mob.def_add = ushort.Parse(paras[28]);
                    mob.def = ushort.Parse(paras[29]);
                    mob.mdef_add = ushort.Parse(paras[30]);
                    mob.mdef = ushort.Parse(paras[31]);
                    mob.hit_melee = ushort.Parse(paras[32]);
                    mob.hit_ranged = ushort.Parse(paras[33]);
                    mob.hit_magic = ushort.Parse(paras[34]);
                    mob.avoid_melee = ushort.Parse(paras[35]);
                    mob.avoid_ranged = ushort.Parse(paras[36]);
                    mob.avoid_magic = ushort.Parse(paras[37]);

                    mob.cri = ushort.Parse(paras[38]);
                    mob.criavd = ushort.Parse(paras[39]);
                    mob.aspd = short.Parse(paras[42]);
                    mob.cspd = short.Parse(paras[43]);
                    mob.aiMode = int.Parse(paras[61]);
                    mob.baseExp = uint.Parse(paras[101]);
                    mob.jobExp = uint.Parse(paras[102]);


                    for (int i = 0; i < 7; i++)
                    {
                        if (paras[104 + i] != "0")
                        {
                            try
                            {
                                MobData.DropData newDrop = new MobData.DropData();
                                string[] args = paras[100 + i].Split('|');
                                args[0] = args[0].Replace("P_", "");
                                newDrop.ItemID = uint.Parse(args[0]);
                                if (args.Length == 2)
                                    newDrop.Rate = int.Parse(args[1]);
                                else
                                    newDrop.Rate = 100;
                                mob.dropItems.Add(newDrop);
                            }
                            catch (Exception) { }
                        }
                    }

                    pets.Add(mob.id, mob);
#if !Web
                    if ((DateTime.Now - time).TotalMilliseconds > 40)
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
                    Logger.ShowError("Error on parsing pet db!\r\nat line:" + line);
                    Logger.ShowError(ex);
#endif
                }
            }
#if !Web
            Logger.ProgressBarHide(count + " pets loaded.");
#endif
            sr.Close();
        }

        public void InitPetLimit(string path, System.Text.Encoding encoding)
        {
            System.IO.StreamReader sr = new System.IO.StreamReader(VirtualFileSystemManager.Instance.FileSystem.OpenFile(path), encoding);
            int count = 0;
#if !Web
            string label = "Loading pet limit database";
            Logger.ProgressBarShow(0, (uint)sr.BaseStream.Length, label);
#endif
            DateTime time = DateTime.Now;
            string[] paras;
            while (!sr.EndOfStream)
            {
                string line;
                line = sr.ReadLine();
                try
                {
                    MobData mob;
                    if (line == "") continue;
                    if (line.Substring(0, 1) == "#")
                        continue;
                    paras = line.Split(',');

                    for (int i = 0; i < paras.Length; i++)
                    {
                        if (paras[i] == "" || paras[i].ToLower() == "null")
                            paras[i] = "0";
                    }
                    mob = new Mob.MobData();
                    mob.id = uint.Parse(paras[0]);
                    mob.name = paras[1];
                    mob.str = ushort.Parse(paras[11]);
                    mob.mag = ushort.Parse(paras[12]);
                    mob.vit = ushort.Parse(paras[13]);
                    mob.dex = ushort.Parse(paras[14]);
                    mob.agi = ushort.Parse(paras[15]);
                    mob.intel = ushort.Parse(paras[16]);
                    mob.hp = uint.Parse(paras[19]);
                    mob.mp = uint.Parse(paras[20]);
                    mob.sp = uint.Parse(paras[21]);
                    mob.speed = ushort.Parse(paras[22]);
                    mob.atk_min = ushort.Parse(paras[23]);
                    mob.atk_max = ushort.Parse(paras[24]);
                    mob.attackType = (ATTACK_TYPE)Enum.Parse(typeof(ATTACK_TYPE), paras[25]);
                    mob.matk_min = ushort.Parse(paras[26]);
                    mob.matk_max = ushort.Parse(paras[27]);
                    mob.def_add = ushort.Parse(paras[28]);
                    mob.def = ushort.Parse(paras[29]);
                    mob.mdef_add = ushort.Parse(paras[30]);
                    mob.mdef = ushort.Parse(paras[31]);
                    mob.hit_melee = ushort.Parse(paras[32]);
                    mob.hit_ranged = ushort.Parse(paras[33]);
                    mob.hit_magic = ushort.Parse(paras[34]);
                    mob.avoid_melee = ushort.Parse(paras[35]);
                    mob.avoid_ranged = ushort.Parse(paras[36]);
                    mob.avoid_magic = ushort.Parse(paras[37]);
                    mob.cri = ushort.Parse(paras[38]);
                    mob.criavd = ushort.Parse(paras[39]);
                    mob.aspd = short.Parse(paras[42]);
                    mob.cspd = short.Parse(paras[43]);
                    mob.aiMode = int.Parse(paras[61]);
                    mob.baseExp = uint.Parse(paras[101]);
                    mob.jobExp = uint.Parse(paras[102]);


                    for (int i = 0; i < 7; i++)
                    {
                        if (paras[104 + i] != "0")
                        {
                            try
                            {
                                MobData.DropData newDrop = new MobData.DropData();
                                string[] args = paras[100 + i].Split('|');
                                args[0] = args[0].Replace("P_", "");
                                newDrop.ItemID = uint.Parse(args[0]);
                                if (args.Length == 2)
                                    newDrop.Rate = int.Parse(args[1]);
                                else
                                    newDrop.Rate = 100;
                                mob.dropItems.Add(newDrop);
                            }
                            catch (Exception) { }
                        }
                    }

                    petLimits.Add(mob.id, mob);
#if !Web
                    if ((DateTime.Now - time).TotalMilliseconds > 40)
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
                    Logger.ShowError("Error on parsing pet limit db!\r\nat line:" + line);
                    Logger.ShowError(ex);
#endif
                }
            }
#if !Web
            Logger.ProgressBarHide(count + " pet limits loaded.");
#endif
            sr.Close();
        }
    }
}

