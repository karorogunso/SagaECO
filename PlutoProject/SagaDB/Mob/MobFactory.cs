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
        Dictionary<uint, MobData> items = new Dictionary<uint,MobData>();
        Dictionary<uint, MobData> pets = new Dictionary<uint, MobData>();
        Dictionary<uint, MobData> petLimits = new Dictionary<uint, MobData>();
        public List<ActorMob> BossList = new List<ActorMob>();

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

        //        public void Init(string path,System.Text.Encoding encoding)
        //        {

        //            System.IO.StreamReader sr = new System.IO.StreamReader(VirtualFileSystemManager.Instance.FileSystem.OpenFile(path), encoding);
        //            System.IO.StreamWriter sw = new System.IO.StreamWriter("p.txt");
        //            List<string> sss =new List<string>();
        //            int count = 0;
        //#if !Web
        //            string label = "Loading mob database";
        //            Logger.ProgressBarShow(0, (uint)sr.BaseStream.Length, label);
        //#endif
        //            DateTime time = DateTime.Now;
        //            string[] paras;
        //            while (!sr.EndOfStream)
        //            {
        //                string line;
        //                line = sr.ReadLine();                    
        //                try
        //                {
        //                    MobData mob;
        //                    if (line == "") continue;
        //                    if (line.Substring(0, 1) == "#")
        //                        continue;
        //                    paras = line.Split(',');

        //                    for (int i = 0; i < paras.Length; i++)
        //                    {
        //                        if (paras[i] == "" || paras[i].ToLower() == "null")
        //                            paras[i] = "0";
        //                    }
        //                    mob = new Mob.MobData();
        //                    mob.id = uint.Parse(paras[0]);
        //                    mob.name = paras[1];
        //                    mob.pictid = uint.Parse(paras[2]);
        //                    try
        //                    {
        //                    mob.mobType = (MobType)Enum.Parse(typeof(MobType), paras[3].Replace(" ", "_"));
        //                    }
        //                    catch
        //                    {
        //                        if (!sss.Contains(paras[3].Replace(" ", "_")))
        //                        {
        //                            sss.Add(paras[3].Replace(" ", "_"));

        //                            sw.WriteLine(paras[3].Replace(" ", "_") + ",");
        //                        }
        //                        Logger.ShowInfo(string.Format("{0}",paras[3].Replace(" ", "_")));
        //                    }
        //                    mob.mobSize = float.Parse(paras[4]);
        //                    if (paras[7] == "1")
        //                        mob.fly = true;
        //                    if (paras[8] == "1")
        //                        mob.undead = true;
        //                    mob.level = byte.Parse(paras[10]);
        //                    mob.str = ushort.Parse(paras[11]);
        //                    mob.mag = ushort.Parse(paras[12]);                    
        //                    mob.vit = ushort.Parse(paras[13]);
        //                    mob.dex = ushort.Parse(paras[14]);
        //                    mob.agi = ushort.Parse(paras[15]);
        //                    mob.intel = ushort.Parse(paras[16]);
        //                    mob.hp = 15 * uint.Parse(paras[19]);
        //                    mob.mp = uint.Parse(paras[20]);
        //                    mob.sp = uint.Parse(paras[21]);
        //                    mob.speed = (ushort)(ushort.Parse(paras[22]) + 500);
        //                    mob.atk_min = (ushort)(5 * ushort.Parse(paras[23]));
        //                    mob.atk_max = (ushort)(5 * ushort.Parse(paras[24]));
        //                    mob.attackType = (ATTACK_TYPE)Enum.Parse(typeof(ATTACK_TYPE), paras[25]);
        //                    mob.matk_min = (ushort)(2 * ushort.Parse(paras[26]));
        //                    mob.matk_max = (ushort)(2 * ushort.Parse(paras[27]));
        //                    mob.def_add = ushort.Parse(paras[28]);
        //                    mob.def = ushort.Parse(paras[29]);
        //                    mob.mdef_add = ushort.Parse(paras[30]);
        //                    mob.mdef = ushort.Parse(paras[31]);
        //                    //mob.dex = ushort.Parse(paras[32]);
        //                    //mob.agi = ushort.Parse(paras[35]);
        //                    mob.hit_melee = ushort.Parse(paras[32]);
        //                    mob.hit_ranged = ushort.Parse(paras[33]);
        //                    mob.hit_magic = ushort.Parse(paras[34]);
        //                    mob.avoid_melee = ushort.Parse(paras[35]);
        //                    mob.avoid_ranged = ushort.Parse(paras[36]);
        //                    mob.avoid_magic = ushort.Parse(paras[37]);

        //                    mob.cri = ushort.Parse(paras[38]);
        //                    mob.criavd = ushort.Parse(paras[39]);

        //                    mob.resilience = short.Parse(paras[41]);//新加

        //                    mob.aspd = short.Parse(paras[42]);
        //                    mob.cspd = short.Parse(paras[43]);

        //                    mob.range = float.Parse(paras[44]);//新加
        //                    for (int i = 0; i < 7; i++)
        //                    {
        //                        mob.elements.Add((Elements)i, int.Parse(paras[45 + i]));
        //                    }
        //                    for (int i = 0; i < 9; i++)
        //                    {
        //                        mob.abnormalStatus.Add((AbnormalStatus)i, short.Parse(paras[53 + i]));
        //                    }
        //                    mob.baseExp = mob.hp/10;
        //                    mob.jobExp = mob.hp/10;
        //                    //mob.aiMode = int.Parse(paras[98]);

        //                    for (int i = 0; i < 7; i++)
        //                    {
        //                        if (paras[100 + i] != "0")
        //                        {
        //                            try
        //                            {
        //                                MobData.DropData newDrop = new MobData.DropData();
        //                                string[] args = paras[100 + i].Split('|');
        //                                newDrop.Public = args[0].StartsWith("E_");
        //                                newDrop.Party = args[0].StartsWith("P_");
        //                                args[0] = args[0].Replace("E_", "");
        //                                args[0] = args[0].Replace("P_", "");
        //                                if (!uint.TryParse(args[0], out newDrop.ItemID))
        //                                {
        //                                    newDrop.TreasureGroup = args[0];
        //#if !Web
        //                                    if (!Treasure.TreasureFactory.Instance.Items.ContainsKey(newDrop.TreasureGroup))
        //                                    {
        //                                        //Logger.ShowWarning("Cannot find treasure group:" + newDrop.TreasureGroup + "\r\n           Droped by:" +
        //                                            //mob.name + "(" + mob.id + ")");
        //                                    }
        //#endif
        //                                }
        //                                if (args.Length == 2)
        //                                    newDrop.Rate = int.Parse(args[1]);
        //                                else
        //                                    newDrop.Rate = 100;
        //                                //if ((SagaDB.Item.ItemFactory.Instance.GetItem(newDrop.ItemID)) != null)
        //                                if(SagaDB.Item.ItemFactory.Instance.Items.ContainsKey(newDrop.ItemID))
        //                                {
        //                                    if ((SagaDB.Item.ItemFactory.Instance.GetItem(newDrop.ItemID)).BaseData.itemType != Item.ItemType.POTION)
        //                                    {
        //                                        if (i < 5)
        //                                            mob.dropItems.Add(newDrop);
        //                                        else
        //                                            mob.dropItemsSpecial.Add(newDrop);
        //                                    }
        //                                }
        //                                else
        //                                {
        //                                    if (i < 5)
        //                                        mob.dropItems.Add(newDrop);
        //                                    else
        //                                        mob.dropItemsSpecial.Add(newDrop);
        //                                }
        //                            }
        //                            catch (Exception) { }
        //                        }
        //                    }
        //                    if (paras[107] != "0")
        //                    {
        //                        MobData.DropData newDrop = new MobData.DropData();
        //                        newDrop.ItemID = uint.Parse(paras[107]);
        //                        newDrop.Rate = int.Parse(paras[108]);
        //                        mob.stampDrop = newDrop;
        //                    }
        //                    if(items.ContainsKey(mob.id))
        //                    {
        //                        Logger.ShowError("重复的怪物ID:" + mob.id.ToString() + ",[" + items[mob.id].name + "]的已存在" + ",让[" + mob.name + "]没有被添加。");
        //                    }
        //                    else
        //                    items.Add(mob.id, mob);
        //#if !Web
        //                    if ((DateTime.Now - time).TotalMilliseconds > 40)
        //                    {
        //                        time = DateTime.Now;
        //                        Logger.ProgressBarShow((uint)sr.BaseStream.Position, (uint)sr.BaseStream.Length, label);
        //                    }
        //#endif                   
        //                    count++;
        //                }
        //                catch (Exception ex)
        //                {
        //#if !Web
        //                    Logger.ShowError("Error on parsing mob db!\r\nat line:" + line);
        //                    Logger.ShowError(ex);
        //#endif
        //                }
        //            }
        //#if !Web
        //            Logger.ProgressBarHide(count + " mobs loaded.");
        //#endif
        //            sw.Close();
        //            sr.Close();
        //        }


        private Race GetMobRace(string typestr)
        {
            Race race = Race.NONE;
            if (typestr.Contains("HUMAN"))
                race = Race.HUMAN;
            else if (typestr.Contains("PLANT"))
                race = Race.PLANT;
            else if (typestr.Contains("ELEMENT"))
                race = Race.ELEMENT;
            else if (typestr.Contains("ROCK"))
                race = Race.ROCK;
            else if (typestr.Contains("UNDEAD"))
                race = Race.UNDEAD;
            else if (typestr.Contains("BIRD"))
                race = Race.BIRD;
            else if (typestr.Contains("INSECT"))
                race = Race.INSECT;
            else if (typestr.Contains("MAGIC_CREATURE"))
                race = Race.MAGIC_CREATURE;
            else if (typestr.Contains("MACHINE"))
                race = Race.MACHINE;
            else if (typestr.Contains("WATER_ANIMAL"))
                race = Race.WATER_ANIMAL;
            else if (typestr.Contains("ANIMAL"))
                race = Race.ANIMAL;
            return race;
        }

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
                    mob.race = GetMobRace(paras[3]);
                    mob.mobSize = float.Parse(paras[4]);
                    if (paras[7] == "1")
                        mob.fly = true;
                    if (paras[8] == "1")
                        mob.undead = true;
                    if (paras[3].Contains("BOSS"))
                        mob.boss = true;
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
                        //这边把null 引入掉落列表了 并且设定掉落为头发
                        //if (paras[113 + i] != "0")
                        //{
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
                            if (args == "0")
                            {
                                newDrop.Public = false;
                                newDrop.Party = true;
                                args = "10000000";

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
                            else
                            {
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

                                mob.dropItems.Add(newDrop);

                                //这里取消了特殊掉落,所有物品都纳入正常掉落中.知识掉落待重写
                                //if (i < 5)
                                //    mob.dropItems.Add(newDrop);
                                //else
                                //    mob.dropItemsSpecial.Add(newDrop);
                            }
                        }
                        catch (Exception) { }
                        //}
                    }
                    if (paras[120] != "0")
                    {
                        MobData.DropData newDrop = new MobData.DropData();
                        newDrop.ItemID = uint.Parse(paras[120]);
                        //暂时开启印章掉落?!
                        newDrop.Rate = int.Parse(paras[121]);
                        newDrop.Rate = int.Parse(mob.dropRate[7]);
                        mob.stampDrop = newDrop;
                    }
                    mob.guideFlag = byte.Parse(paras[122]);
                    mob.guideID = short.Parse(paras[123]);
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
                    //骑宠的DB和一般的db 结构不再一致了 by 黑白照 2018.06.30
                    if (mob.mobType.ToString().Contains("RIDE"))
                    {
                        mob.mobSize = float.Parse(paras[4]);
                        mob.level = byte.Parse(paras[10]);
                        mob.str = ushort.Parse(paras[11]);
                        mob.mag = ushort.Parse(paras[12]);
                        mob.vit = ushort.Parse(paras[13]);
                        mob.dex = ushort.Parse(paras[14]);
                        mob.agi = ushort.Parse(paras[15]);
                        mob.intel = ushort.Parse(paras[16]);
                        mob.hp = uint.Parse(paras[17]);
                        mob.mp = uint.Parse(paras[18]);
                        mob.sp = uint.Parse(paras[19]);
                        mob.speed = ushort.Parse(paras[20]);
                        mob.atk_min = ushort.Parse(paras[21]);
                        mob.atk_max = ushort.Parse(paras[22]);
                        mob.attackType = (ATTACK_TYPE)Enum.Parse(typeof(ATTACK_TYPE), paras[23]);
                        mob.matk_min = ushort.Parse(paras[24]);
                        mob.matk_max = ushort.Parse(paras[25]);
                        mob.def_add = ushort.Parse(paras[26]);
                        mob.def = ushort.Parse(paras[27]);
                        mob.mdef_add = ushort.Parse(paras[28]);
                        mob.mdef = ushort.Parse(paras[29]);
                        mob.hit_melee = ushort.Parse(paras[30]);
                        mob.hit_ranged = ushort.Parse(paras[31]);
                        mob.hit_magic = ushort.Parse(paras[32]);
                        mob.avoid_melee = ushort.Parse(paras[33]);
                        mob.avoid_ranged = ushort.Parse(paras[34]);
                        mob.avoid_magic = ushort.Parse(paras[35]);

                        mob.cri = ushort.Parse(paras[38]);
                        mob.criavd = ushort.Parse(paras[39]);
                        mob.aspd = short.Parse(paras[42]);
                        mob.cspd = short.Parse(paras[43]);
                        //mob.baseExp = uint.Parse(paras[97]);
                        //mob.jobExp = uint.Parse(paras[98]);
                        //mob.aiMode = int.Parse(paras[99]);

                        //for (int i = 0; i < 7; i++)
                        //{
                        //    if (paras[100 + i] != "0")
                        //    {
                        //        try
                        //        {
                        //            MobData.DropData newDrop = new MobData.DropData();
                        //            string[] args = paras[100 + i].Split('|');
                        //            args[0] = args[0].Replace("P_", "");
                        //            newDrop.ItemID = uint.Parse(args[0]);
                        //            if (args.Length == 2)
                        //                newDrop.Rate = int.Parse(args[1]);
                        //            else
                        //                newDrop.Rate = 100;
                        //            mob.dropItems.Add(newDrop);
                        //        }
                        //        catch (Exception) { }
                        //    }
                        //}
                        //if (paras[107] != "0")
                        //{
                        //    MobData.DropData newDrop = new MobData.DropData();
                        //    newDrop.ItemID = uint.Parse(paras[107]);
                        //    newDrop.Rate = int.Parse(paras[108]);
                        //    mob.stampDrop = newDrop;
                        //}
                    }
                    else
                    {

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

        public void InitPartner(string path, System.Text.Encoding encoding)
        {
            System.IO.StreamReader sr = new System.IO.StreamReader(VirtualFileSystemManager.Instance.FileSystem.OpenFile(path), encoding);
            int count = 0;
#if !Web
            string label = "Loading partner database";
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
                    mob.mobSize = float.Parse(paras[3]);
                    try
                    {
                        mob.mobType = (MobType)Enum.Parse(typeof(MobType), paras[6]);
                    }
                    catch (Exception)
                    {
                        while (paras[6].Substring(paras[6].Length - 1) != "_")
                        {
                            paras[6] = paras[6].Substring(0, paras[6].Length - 1);
                        }
                        paras[6] = paras[6].Substring(0, paras[6].Length - 1);
                        mob.mobType = (MobType)Enum.Parse(typeof(MobType), paras[6]);
                    }
                    mob.speed = ushort.Parse(paras[9]);
                    mob.attackType = (ATTACK_TYPE)Enum.Parse(typeof(ATTACK_TYPE), paras[10]);
                    mob.level = byte.Parse(paras[15]);
                    mob.str = 0;
                    mob.mag = 0;
                    mob.vit = 0;
                    mob.dex = 0;
                    mob.agi = 0;
                    mob.intel = 0;
                    mob.hp = 2000;
                    mob.mp = 1000;
                    mob.sp = 1000;
                    //mob.atk_min = ushort.Parse(paras[14]);
                    //mob.atk_max = ushort.Parse(paras[14]);
                    //mob.matk_min = ushort.Parse(paras[14]);
                    //mob.matk_max = ushort.Parse(paras[14]);
                    mob.atk_min = 200;
                    mob.atk_max = 250;
                    mob.matk_min = 200;
                    mob.matk_max = 250;
                    mob.def_add = 100;
                    mob.def = 20;
                    mob.mdef_add = 100;
                    mob.mdef = 15;
                    mob.hit_melee = 200;
                    mob.hit_ranged = 200;
                    mob.hit_magic = 200;
                    mob.avoid_melee = 50;
                    mob.avoid_ranged = 50;
                    mob.avoid_magic = 50;

                    mob.cri = 200;
                    mob.criavd = 50;
                    mob.aspd = 500;
                    //mob.cspd = 500;
                    mob.baseExp = 0;
                    mob.jobExp = 0;
                    mob.aiMode = 0;

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
                    Logger.ShowError("Error on parsing partner db!\r\nat line:" + line);
                    Logger.ShowError(ex);
#endif
                }
            }
#if !Web
            Logger.ProgressBarHide(count + " partners loaded.");
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
                    mob.str = ushort.Parse(paras[10]);
                    mob.mag = ushort.Parse(paras[11]);
                    mob.vit = ushort.Parse(paras[12]);
                    mob.dex = ushort.Parse(paras[13]);
                    mob.agi = ushort.Parse(paras[14]);
                    mob.intel = ushort.Parse(paras[15]);
                    mob.hp = uint.Parse(paras[18]);
                    mob.mp = uint.Parse(paras[19]);
                    mob.sp = uint.Parse(paras[20]);
                    mob.speed = ushort.Parse(paras[21]);
                    mob.atk_min = ushort.Parse(paras[22]);
                    mob.atk_max = ushort.Parse(paras[23]);
                    mob.attackType = (ATTACK_TYPE)Enum.Parse(typeof(ATTACK_TYPE), paras[24]);
                    if(paras[25] != "BLOW" && paras[25] != "SLASH" && paras[25] != "STAB")//临时格式改动
                    mob.matk_min = ushort.Parse(paras[25]);
                    mob.matk_max = ushort.Parse(paras[26]);
                    mob.def_add = ushort.Parse(paras[27]);
                    mob.def = ushort.Parse(paras[28]);
                    mob.mdef_add = ushort.Parse(paras[29]);
                    mob.mdef = ushort.Parse(paras[30]);
                    mob.hit_melee = ushort.Parse(paras[31]);
                    mob.hit_ranged = ushort.Parse(paras[32]);
                    mob.hit_magic = ushort.Parse(paras[33]);
                    mob.avoid_melee = ushort.Parse(paras[34]);
                    mob.avoid_ranged = ushort.Parse(paras[35]);
                    mob.avoid_magic = ushort.Parse(paras[36]);

                    mob.cri = ushort.Parse(paras[37]);
                    mob.criavd = ushort.Parse(paras[38]);
                    mob.aspd = short.Parse(paras[41]);
                    mob.cspd = short.Parse(paras[42]);
                    mob.baseExp = uint.Parse(paras[96]);
                    mob.jobExp = uint.Parse(paras[97]);
                    mob.aiMode = int.Parse(paras[98]);

                    for (int i = 0; i < 7; i++)
                    {
                        if (paras[99 + i] != "0")
                        {
                            try
                            {
                                MobData.DropData newDrop = new MobData.DropData();
                                string[] args = paras[99 + i].Split('|');
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
                    if (paras[106] != "0")
                    {
                        MobData.DropData newDrop = new MobData.DropData();
                        newDrop.ItemID = uint.Parse(paras[106]);
                        newDrop.Rate = int.Parse(paras[107]);
                        mob.stampDrop = newDrop;
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
