using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using SagaLib;
using SagaDB.Actor;

namespace SagaDB.Item
{
    public class ItemFactory : Singleton<ItemFactory>
    {
        Dictionary<uint, Item.ItemData> items = new Dictionary<uint, Item.ItemData>();

        public ItemFactory()
        {
            
        }

        public void Init(string path,System.Text.Encoding encoding)
        {
            System.IO.StreamReader sr = new System.IO.StreamReader(path, encoding);
            Logger.ShowInfo("Loading item database...");
            Console.ForegroundColor = ConsoleColor.Green;
            int count = 0;
            bool print = true;
            while (!sr.EndOfStream)
            {
                string line;
                line = sr.ReadLine();                    
                try
                {
                    string[] paras;
                    Item.ItemData item;
                    if (line.Substring(0, 1) == "#")
                        continue;
                    paras = line.Split(',');

                    for (int i = 0; i < paras.Length; i++)
                    {
                        if (paras[i] == "")
                            paras[i] = "0";
                    }
                    item = new Item.ItemData();
                    item.id = uint.Parse(paras[0]);
                    item.name = paras[3];
                    item.itemType = (ItemType)Enum.Parse(typeof(ItemType), paras[4]);
                    item.price = uint.Parse(paras[5]);
                    item.weight = (ushort)float.Parse(paras[6]);
                    item.volume = (ushort)float.Parse(paras[7]);
                    item.equipVolume = ushort.Parse(paras[8]);
                    item.possessionWeight = ushort.Parse(paras[9]);
                    item.repairItem = (uint)int.Parse(paras[10]);
                    item.enhancementItem = (uint)int.Parse(paras[11]);
                    item.events = uint.Parse(paras[13]);
                    item.receipt = toBool(paras[14]);
                    item.dye = toBool(paras[15]);
                    item.stock = toBool(paras[16]);
                    item.doubleHand = toBool(paras[17]);
                    item.usable = toBool(paras[18]);
                    item.color = byte.Parse(paras[19]);
                    item.durability = ushort.Parse(paras[20]);
                    item.eventID = uint.Parse(paras[22]);
                    item.effectID = uint.Parse(paras[23]);
                    item.activateSkill = ushort.Parse(paras[24]);
                    item.possibleSkill = ushort.Parse(paras[25]);
                    item.passiveSkill = ushort.Parse(paras[26]);
                    item.possessionSkill = ushort.Parse(paras[27]);
                    item.possessionPassiveSkill = ushort.Parse(paras[28]);
                    item.target = (TargetType)Enum.Parse(typeof(TargetType), paras[29]);
                    item.activeType = (ActiveType)Enum.Parse(typeof(ActiveType), paras[30]);
                    item.range = (byte)int.Parse(paras[31]);
                    item.duration = uint.Parse(paras[32]);
                    item.effectRange = byte.Parse(paras[33]);
                    item.cast = uint.Parse(paras[35]);
                    item.delay = uint.Parse(paras[36]);
                    item.hp = short.Parse(paras[37]);
                    item.mp = short.Parse(paras[38]);
                    item.sp = short.Parse(paras[39]);
                    item.weightUp = short.Parse(paras[40]);
                    item.volumeUp = short.Parse(paras[41]);
                    item.speedUp = short.Parse(paras[42]);
                    item.str = short.Parse(paras[43]);
                    item.mag = short.Parse(paras[44]);
                    item.vit = short.Parse(paras[45]);
                    item.dex = short.Parse(paras[46]);
                    item.agi = short.Parse(paras[47]);
                    item.intel = short.Parse(paras[48]);
                    item.luk = short.Parse(paras[49]);
                    item.cha = short.Parse(paras[50]);
                    item.atk1 = short.Parse(paras[51]);
                    item.atk2 = short.Parse(paras[52]);
                    item.atk3 = short.Parse(paras[53]);
                    item.matk = short.Parse(paras[54]);
                    item.def = short.Parse(paras[55]);
                    item.mdef = short.Parse(paras[56]);
                    item.hitMelee = short.Parse(paras[57]);
                    item.hitRanged = short.Parse(paras[58]);
                    item.hitMagic = short.Parse(paras[59]);
                    item.avoidMelee = short.Parse(paras[60]);
                    item.avoidRanged = short.Parse(paras[61]);
                    item.avoidMagic = short.Parse(paras[62]);
                    item.hitCritical = short.Parse(paras[63]);
                    item.avoidCritical = short.Parse(paras[64]);
                    item.hpRecover = short.Parse(paras[65]);
                    item.mpRecover = short.Parse(paras[66]);
                    for (int i = 0; i < 7; i++)
                    {
                        item.element.Add((Elements)i, short.Parse(paras[67 + i]));
                    }
                    for (int i = 0; i < 9; i++)
                    {
                        item.abnormalStatus.Add((AbnormalStatus)i, short.Parse(paras[74 + i]));
                    }
                    for (int i = 0; i < 3; i++)
                    {
                        item.possibleRace.Add((SagaDB.Actor.PC_RACE)i, toBool(paras[83 + i]));
                    }
                    for (int i = 0; i < 2; i++)
                    {
                        item.possibleGender.Add((SagaDB.Actor.PC_GENDER)i, toBool(paras[86 + i]));
                    }
                    item.possibleLv = byte.Parse(paras[88]);
                    item.possibleStr = ushort.Parse(paras[89]);
                    item.possibleMag = ushort.Parse(paras[90]);
                    item.possibleVit = ushort.Parse(paras[91]);
                    item.possibleDex = ushort.Parse(paras[92]);
                    item.possibleAgi = ushort.Parse(paras[93]);
                    item.possibleInt = ushort.Parse(paras[94]);
                    item.possibleLuk = ushort.Parse(paras[95]);
                    item.possibleCha = ushort.Parse(paras[96]);
                    string[] jobs = Enum.GetNames(typeof(PC_JOB));
                    for (int i = 0; i < 37; i++)
                    {
                        item.possibleJob.Add((PC_JOB)Enum.Parse(typeof(PC_JOB), jobs[i]), toBool(paras[97 + i]));
                    }
                    for (int i = 0; i < 4; i++)
                    {
                        item.possibleCountry.Add((Country)i, toBool(paras[134 + i]));
                    }

                    if (!items.ContainsKey(item.id)) items.Add(item.id, item);

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
                    Logger.ShowError("Error on parsing item db!\r\nat line:" + line);
                    Logger.ShowError(ex);
                }
            }
            Console.WriteLine();
            Console.ResetColor();
            Logger.ShowInfo(count + " items loaded.");
        }

        private bool toBool(string input)
        {
            if (input == "1") return true; else return false;
        }

        public Item GetItem(uint id)
        {
            return GetItem(id, true);
        }

        public Item GetItem(uint id, bool identified)
        {
            if (items.ContainsKey(id))
            {
                Item item = new Item();
                item.BaseData = items[id];
                item.stack = 1;
                item.durability = item.BaseData.durability;
                item.Identified = identified;
                return item;
            }
            else
            {
                Logger.ShowDebug("Item:" + id.ToString() + "not found!", Logger.CurrentLogger);
                return null;
            }
        }
    }
}
