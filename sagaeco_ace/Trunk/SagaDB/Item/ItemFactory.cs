using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using SagaLib;
using SagaDB.Actor;

namespace SagaDB.Item
{
    public class ItemFactory : Factory<ItemFactory, Item.ItemData>
    {
        bool readDesc = false;

        public bool ReadDesc { set { readDesc = value; } }
        public string path = "";
        public ItemFactory()
        {
            this.loadingTab = "Loading item database";
            this.loadedTab = " items loaded.";
            this.databaseName = "Item";
            this.FactoryType = FactoryType.CSV;
        }

        protected override void ParseXML(System.Xml.XmlElement root, System.Xml.XmlElement current, Item.ItemData item)
        {
            throw new NotImplementedException();
        }

        protected override uint GetKey(Item.ItemData item)
        {
            return item.id;
        }

        protected override void ParseCSV(Item.ItemData item, string[] paras)
        {
            item.id = uint.Parse(paras[0]);
            item.imageID = uint.Parse(paras[1]);
            item.iconID = uint.Parse(paras[2]);
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
            if (paras[21] != "0")
                item.jointJob = (PC_JOB)(int.Parse(paras[21]) + 1000);
            else
                item.jointJob = PC_JOB.NONE;
            item.currentSlot = byte.Parse(paras[22]);
            item.maxSlot = byte.Parse(paras[23]);
            item.eventID = uint.Parse(paras[24]);
            item.effectID = uint.Parse(paras[25]);
            item.activateSkill = ushort.Parse(paras[26]);
            item.possibleSkill = ushort.Parse(paras[27]);
            item.passiveSkill = ushort.Parse(paras[28]);
            item.possessionSkill = ushort.Parse(paras[29]);
            item.possessionPassiveSkill = ushort.Parse(paras[30]);
            item.target = (TargetType)Enum.Parse(typeof(TargetType), paras[31]);
            item.activeType = (ActiveType)Enum.Parse(typeof(ActiveType), paras[32]);
            item.range = (byte)int.Parse(paras[33]);
            item.duration = uint.Parse(paras[34]);
            item.effectRange = byte.Parse(paras[35]);
            item.isRate = toBool(paras[36]);
            item.cast = uint.Parse(paras[37]);
            item.delay = uint.Parse(paras[38]);
            item.hp = short.Parse(paras[39]);
            item.mp = short.Parse(paras[40]);
            item.sp = short.Parse(paras[41]);
            item.weightUp = short.Parse(paras[42]);
            item.volumeUp = short.Parse(paras[43]);
            item.speedUp = short.Parse(paras[44]);
            item.str = short.Parse(paras[45]);
            item.mag = short.Parse(paras[46]);
            item.vit = short.Parse(paras[47]);
            item.dex = short.Parse(paras[48]);
            item.agi = short.Parse(paras[49]);
            item.intel = short.Parse(paras[50]);
            item.luk = short.Parse(paras[51]);
            item.cha = short.Parse(paras[52]);
            item.atk1 = short.Parse(paras[53]);
            item.atk2 = short.Parse(paras[54]);
            item.atk3 = short.Parse(paras[55]);
            item.matk = short.Parse(paras[56]);
            item.def = short.Parse(paras[57]);
            item.mdef = short.Parse(paras[58]);
            item.hitMelee = short.Parse(paras[59]);
            item.hitRanged = short.Parse(paras[60]);
            item.hitMagic = short.Parse(paras[61]);
            item.avoidMelee = short.Parse(paras[62]);
            item.avoidRanged = short.Parse(paras[63]);
            if (paras[64] != ".")
                item.avoidMagic = short.Parse(paras[64]);
            item.hitCritical = short.Parse(paras[65]);
            item.avoidCritical = short.Parse(paras[66]);
            item.hpRecover = short.Parse(paras[67]);
            item.mpRecover = short.Parse(paras[68]);
            for (int i = 0; i < 7; i++)
            {
                item.element.Add((Elements)i, short.Parse(paras[69 + i]));
            }
            for (int i = 0; i < 9; i++)
            {
                item.abnormalStatus.Add((AbnormalStatus)i, short.Parse(paras[76 + i]));
            }
            for (int i = 0; i < 4; i++)
            {
                item.possibleRace.Add((SagaDB.Actor.PC_RACE)i, toBool(paras[85 + i]));
            }
            for (int i = 0; i < 2; i++)
            {
                item.possibleGender.Add((SagaDB.Actor.PC_GENDER)i, toBool(paras[89 + i]));
            }
            item.possibleLv = byte.Parse(paras[91]);
            //转生
            item.possibleRebirth = toBool(paras[92]);
            item.possibleStr = ushort.Parse(paras[93]);
            item.possibleMag = ushort.Parse(paras[94]);
            item.possibleVit = ushort.Parse(paras[95]);
            item.possibleDex = ushort.Parse(paras[96]);
            item.possibleAgi = ushort.Parse(paras[97]);
            item.possibleInt = ushort.Parse(paras[98]);
            item.possibleLuk = ushort.Parse(paras[99]);
            item.possibleCha = ushort.Parse(paras[100]);
            //string[] jobs = Enum.GetNames(typeof(PC_JOB));
            //追加13个
            for (int i = 0; i < 36; i++)
            {
                item.possibleJob.Add((PC_JOB)(i / 3 * 10 + (i % 3 * 2) + 1), toBool(paras[102 + i]));
                if (toBool(paras[102 + i]))
                {
                    item.possibleJob[(PC_JOB)(i / 3 * 10 + 1)] = toBool(paras[102 + i]);
                }

            }
            for (int i = 0; i < 12; i++)
            {
                item.possibleJob.Add((PC_JOB)(i * 10 + 7), toBool(paras[138 + i]));
            }
            for (int i = 0; i < 4; i++)
            {
                item.possibleCountry.Add((Country)i, toBool(paras[151 + i]));
            }
            item.possibleJob.Add(PC_JOB.NOVICE, toBool(paras[101]));
            item.possibleJob.Add(PC_JOB.JOKER, toBool(paras[150]));
            item.possibleJob.Add(PC_JOB.BREEDER, toBool(paras[159]));
            item.possibleJob.Add(PC_JOB.GARDNER, toBool(paras[160]));

            item.marionetteID = uint.Parse(paras[166]);
            item.petID = uint.Parse(paras[167]);
            item.handMotion = byte.Parse(paras[168]);
            item.handMotion2 = byte.Parse(paras[169]);
            if (readDesc)
                item.desc = paras[174];
            item.noTrade = int.Parse(paras[176]) > 0;
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
                Item item = new Item(items[id]);
                item.Stack = 1;
                item.Durability = item.BaseData.durability;
                item.Identified = identified;
                return item;
            }
            else
            {
                Logger.ShowWarning("Item:" + id.ToString() + " not found! Creating dummy Item.");
                Item item = new Item(items[10000000]);
                item.Stack = 1;
                item.Durability = item.BaseData.durability;
                item.Identified = identified;
                return item;
            }
        }
    }
}
