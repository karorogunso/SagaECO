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
            //item.name = paras[4];
            item.itemType = (ItemType)Enum.Parse(typeof(ItemType), paras[5]);
            item.price = uint.Parse(paras[6]);
            item.weight = (ushort)float.Parse(paras[7]);
            item.volume = (ushort)float.Parse(paras[8]);
            item.equipVolume = ushort.Parse(paras[9]);
            item.possessionWeight = ushort.Parse(paras[10]);
            item.repairItem = (uint)int.Parse(paras[11]);
            item.enhancementItem = (uint)int.Parse(paras[12]);
            item.events = uint.Parse(paras[14]);
            item.receipt = toBool(paras[15]);
            item.dye = toBool(paras[16]);
            item.stock = toBool(paras[17]);
            item.doubleHand = toBool(paras[18]);
            item.usable = toBool(paras[19]);
            item.color = byte.Parse(paras[20]);
            item.durability = ushort.Parse(paras[21]);
            if (paras[22] != "0")
                item.jointJob = (PC_JOB)(int.Parse(paras[22]) + 1000);
            else
                item.jointJob = PC_JOB.NONE;
            item.currentSlot = byte.Parse(paras[23]);
            item.maxSlot = byte.Parse(paras[24]);
            item.eventID = uint.Parse(paras[25]);
            item.effectID = uint.Parse(paras[26]);
            item.activateSkill = ushort.Parse(paras[27]);
            item.possibleSkill = ushort.Parse(paras[28]);
            item.passiveSkill = ushort.Parse(paras[29]);
            item.possessionSkill = ushort.Parse(paras[30]);
            item.possessionPassiveSkill = ushort.Parse(paras[31]);
            item.target = (TargetType)Enum.Parse(typeof(TargetType), paras[32]);
            item.activeType = (ActiveType)Enum.Parse(typeof(ActiveType), paras[33]);
            item.range = (byte)int.Parse(paras[34]);
            item.duration = uint.Parse(paras[35]);
            item.effectRange = byte.Parse(paras[36]);
            item.isRate = toBool(paras[37]);
            item.cast = uint.Parse(paras[38]);
            item.delay = uint.Parse(paras[39]);
            item.hp = short.Parse(paras[40]);
            item.mp = short.Parse(paras[41]);
            item.sp = short.Parse(paras[42]);
            item.weightUp = short.Parse(paras[43]);
            item.volumeUp = short.Parse(paras[44]);
            item.speedUp = short.Parse(paras[45]);
            item.str = short.Parse(paras[46]);
            item.mag = short.Parse(paras[47]);
            item.vit = short.Parse(paras[48]);
            item.dex = short.Parse(paras[49]);
            item.agi = short.Parse(paras[50]);
            item.intel = short.Parse(paras[51]);
            item.luk = short.Parse(paras[52]);
            item.cha = short.Parse(paras[53]);
            item.atk1 = short.Parse(paras[54]);
            item.atk2 = short.Parse(paras[55]);
            item.atk3 = short.Parse(paras[56]);
            item.matk = short.Parse(paras[57]);
            item.def = short.Parse(paras[58]);
            item.mdef = short.Parse(paras[59]);
            item.hitMelee = short.Parse(paras[60]);
            item.hitRanged = short.Parse(paras[61]);
            item.hitMagic = short.Parse(paras[62]);
            item.avoidMelee = short.Parse(paras[63]);
            item.avoidRanged = short.Parse(paras[64]);
            if (paras[64] != ".")
                item.avoidMagic = short.Parse(paras[65]);
            item.hitCritical = short.Parse(paras[66]);
            item.avoidCritical = short.Parse(paras[67]);
            item.hpRecover = short.Parse(paras[68]);
            item.mpRecover = short.Parse(paras[69]);
            for (int i = 0; i < 7; i++)
            {
                item.element.Add((Elements)i, short.Parse(paras[70 + i]));
            }
            for (int i = 0; i < 9; i++)
            {
                item.abnormalStatus.Add((AbnormalStatus)i, short.Parse(paras[77 + i]));
            }
            for (int i = 0; i < 4; i++)
            {
                item.possibleRace.Add((SagaDB.Actor.PC_RACE)i, toBool(paras[86 + i]));
            }
            for (int i = 0; i < 2; i++)
            {
                item.possibleGender.Add((SagaDB.Actor.PC_GENDER)i, toBool(paras[90 + i]));
            }
            item.possibleLv = byte.Parse(paras[92]);
            //转生
            item.possibleRebirth = toBool(paras[93]);
            item.possibleStr = ushort.Parse(paras[94]);
            item.possibleMag = ushort.Parse(paras[95]);
            item.possibleVit = ushort.Parse(paras[96]);
            item.possibleDex = ushort.Parse(paras[97]);
            item.possibleAgi = ushort.Parse(paras[98]);
            item.possibleInt = ushort.Parse(paras[99]);
            item.possibleLuk = ushort.Parse(paras[100]);
            item.possibleCha = ushort.Parse(paras[101]);
            //string[] jobs = Enum.GetNames(typeof(PC_JOB));
            //追加13个
            item.possibleJob.Add(PC_JOB.NOVICE, toBool(paras[102]));
            for (int i = 0; i < 36; i++)
            {
                item.possibleJob.Add((PC_JOB)(i / 3 * 10 + (i % 3 * 2) + 1), toBool(paras[103 + i]));
                if (toBool(paras[103 + i]))
                {
                    item.possibleJob[(PC_JOB)(i / 3 * 10 + 1)] = toBool(paras[103 + i]);
                }

            }
            for (int i = 0; i < 12; i++)
            {
                item.possibleJob.Add((PC_JOB)(i * 10 + 7), toBool(paras[139 + i]));
            }
            for (int i = 0; i < 4; i++)
            {
                item.possibleCountry.Add((Country)i, toBool(paras[152 + i]));
            }
            item.possibleJob.Add(PC_JOB.JOKER, toBool(paras[151]));
            item.possibleJob.Add(PC_JOB.BREEDER, toBool(paras[160]));
            item.possibleJob.Add(PC_JOB.GARDNER, toBool(paras[161]));

            item.marionetteID = uint.Parse(paras[167]);
            item.petID = uint.Parse(paras[168]);
            byte.TryParse(paras[169], out item.handMotion);
            byte.TryParse(paras[170], out item.handMotion2);
            if (readDesc)
                item.desc = paras[175];
            item.noTrade = int.Parse(paras[177]) > 0;
        }

        private bool toBool(string input) =>  input == "1";

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
