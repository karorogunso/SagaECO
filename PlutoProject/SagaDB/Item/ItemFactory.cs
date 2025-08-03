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
            uint offset = 0;
            item.id = uint.Parse(paras[0 + offset]);
            item.imageID = uint.Parse(paras[1 + offset]);
            item.iconID = uint.Parse(paras[2 + offset]);
            item.name = paras[3 + offset];
            offset += 1;//跳过 振り仮名
            item.itemType = (ItemType)Enum.Parse(typeof(ItemType), paras[4 + offset]);
            item.price = uint.Parse(paras[5 + offset]);
            item.weight = (uint)float.Parse(paras[6 + offset]);
            item.volume = (uint)float.Parse(paras[7 + offset]);
            item.equipVolume = uint.Parse(paras[8 + offset]);
            if (paras[9 + offset] != "-1")
                item.possessionWeight = uint.Parse(paras[9 + offset]);
            item.repairItem = (uint)int.Parse(paras[10 + offset]);
            item.enhancementItem = (uint)int.Parse(paras[11 + offset]);
            item.events = uint.Parse(paras[13 + offset]);
            item.receipt = toBool(paras[14 + offset]);
            item.dye = toBool(paras[15 + offset]);
            item.stock = toBool(paras[16 + offset]);
            item.doubleHand = toBool(paras[17 + offset]);
            item.usable = toBool(paras[18 + offset]);
            item.color = byte.Parse(paras[19 + offset]);
            item.durability = ushort.Parse(paras[20 + offset]);
            if (paras[21 + offset] != "0")
            {
                item.jointJob = (PC_JOB)(int.Parse(paras[21 + offset]) + 1000);
            }
            else
                item.jointJob = PC_JOB.NONE;
            item.currentSlot = byte.Parse(paras[22 + offset]);
            item.maxSlot = byte.Parse(paras[23 + offset]);
            uint.TryParse(paras[24 + offset], out item.eventID);
            uint.TryParse(paras[25 + offset], out item.effectID);
            item.activateSkill = ushort.Parse(paras[26 + offset]);
            item.possibleSkill = ushort.Parse(paras[27 + offset]);
            item.passiveSkill = ushort.Parse(paras[28 + offset]);
            ushort.TryParse(paras[29 + offset], out item.possessionSkill);
            ushort.TryParse(paras[30 + offset], out item.possessionPassiveSkill);
            item.target = (TargetType)Enum.Parse(typeof(TargetType), paras[31 + offset]);
            item.activeType = (ActiveType)Enum.Parse(typeof(ActiveType), paras[32 + offset]);
            item.range = (byte)int.Parse(paras[33 + offset]);
            item.duration = uint.Parse(paras[34 + offset]);
            item.effectRange = byte.Parse(paras[35 + offset]);
            item.isRate = toBool(paras[36 + offset]);
            item.cast = uint.Parse(paras[37 + offset]);
            item.delay = uint.Parse(paras[38 + offset]);
            item.hp = short.Parse(paras[39 + offset]);
            item.mp = short.Parse(paras[40 + offset]);
            item.sp = short.Parse(paras[41 + offset]);
            item.weightUp = short.Parse(paras[42 + offset]);
            item.volumeUp = short.Parse(paras[43 + offset]);
            item.speedUp = short.Parse(paras[44 + offset]);
            item.str = short.Parse(paras[45 + offset]);
            item.mag = short.Parse(paras[46 + offset]);
            item.vit = short.Parse(paras[47 + offset]);
            item.dex = short.Parse(paras[48 + offset]);
            item.agi = short.Parse(paras[49 + offset]);
            item.intel = short.Parse(paras[50 + offset]);
            item.luk = short.Parse(paras[51 + offset]);
            item.cha = short.Parse(paras[52 + offset]);
            item.atk1 = short.Parse(paras[53 + offset]);
            item.atk2 = short.Parse(paras[54 + offset]);
            item.atk3 = short.Parse(paras[55 + offset]);
            item.matk = short.Parse(paras[56 + offset]);
            item.def = short.Parse(paras[57 + offset]);
            item.mdef = short.Parse(paras[58 + offset]);
            item.hitMelee = short.Parse(paras[59 + offset]);
            item.hitRanged = short.Parse(paras[60 + offset]);
            item.hitMagic = short.Parse(paras[61 + offset]);
            item.avoidMelee = short.Parse(paras[62 + offset]);
            item.avoidRanged = short.Parse(paras[63 + offset]);
            if (paras[64 + offset] != ".")
                item.avoidMagic = short.Parse(paras[64 + offset]);
            item.hitCritical = short.Parse(paras[65 + offset]);
            item.avoidCritical = short.Parse(paras[66 + offset]);
            item.hpRecover = short.Parse(paras[67 + offset]);
            item.mpRecover = short.Parse(paras[68 + offset]);
            item.spRecover = 0;
            for (int i = 0; i < 7; i++)
            {
                item.element.Add((Elements)i, short.Parse(paras[69 + i + offset]));
            }
            for (int i = 0; i < 9; i++)
            {
                item.abnormalStatus.Add((AbnormalStatus)i, short.Parse(paras[76 + i + offset]));
            }
            for (int i = 0; i < 4; i++)
            {
                item.possibleRace.Add((SagaDB.Actor.PC_RACE)i, toBool(paras[85 + i + offset]));
            }
            for (int i = 0; i < 2; i++)
            {
                item.possibleGender.Add((SagaDB.Actor.PC_GENDER)i, toBool(paras[89 + i + offset]));
            }
            item.possibleLv = byte.Parse(paras[91 + offset]);
            //转生
            item.possibleRebirth = toBool(paras[92 + offset]);
            item.possibleStr = ushort.Parse(paras[93 + offset]);
            item.possibleMag = ushort.Parse(paras[94 + offset]);
            item.possibleVit = ushort.Parse(paras[95 + offset]);
            item.possibleDex = ushort.Parse(paras[96 + offset]);
            item.possibleAgi = ushort.Parse(paras[97 + offset]);
            item.possibleInt = ushort.Parse(paras[98 + offset]);
            item.possibleLuk = ushort.Parse(paras[99 + offset]);
            item.possibleCha = ushort.Parse(paras[100 + offset]);
            //string[+ offset] jobs = Enum.GetNames(typeof(PC_JOB));
            //追加13个
            for (int i = 0; i < 36; i++)
            {

                item.possibleJob.Add((PC_JOB)(i / 3 * 10 + (i % 3 * 2) + 1), toBool(paras[102 + i + offset]));
                if (toBool(paras[102 + i + offset]))
                {
                    item.possibleJob[(PC_JOB)(i / 3 * 10 + 1)] = toBool(paras[102 + i + offset]);
                }

            }
            for (int i = 0; i < 12; i++)
            {
                item.possibleJob.Add((PC_JOB)(i * 10 + 7), toBool(paras[138 + i + offset]));
            }
            for (int i = 0; i < 4; i++)
            {
                item.possibleCountry.Add((Country)i, toBool(paras[151 + i + offset]));
            }
            item.possibleJob.Add(PC_JOB.NOVICE, toBool(paras[101 + offset]));
            item.possibleJob.Add(PC_JOB.JOKER, toBool(paras[150 + offset]));
            item.possibleJob.Add(PC_JOB.BREEDER, toBool(paras[159 + offset]));
            item.possibleJob.Add(PC_JOB.GARDNER, toBool(paras[160 + offset]));

            item.marionetteID = uint.Parse(paras[166 + offset]);
            item.petID = uint.Parse(paras[167 + offset]);
            item.handMotion = ushort.Parse(paras[168 + offset]);
            item.handMotion2 = byte.Parse(paras[169 + offset]);

            if (readDesc)
                item.desc = paras[174 + offset];
            item.noTrade = int.Parse(paras[176 + offset]) > 0;
            var itemAddition = ItemAdditionFactory.Instance.GetItemAddition(item.id);
            if (itemAddition != null)
                item.ItemAddition = itemAddition;
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
        public void CalcRefineBouns(Item Equip)
        {
            if (Equip.Refine == 0) return;
            if (Equip.EquipSlot == null) return;
            if (Equip.EquipSlot[0] == EnumEquipSlot.CHEST_ACCE)
            {
                Equip.atk_refine = (short)(Equip.Refine_Sharp * 1);
                Equip.matk_refine = (short)(Equip.Refine_Enchanted * 1);
                Equip.hp_refine = (short)(Equip.Refine_Vitality * 15);
                Equip.hit_refine = (short)(Equip.Refine_Hit * 1); ;
                Equip.mhit_refine = (short)(Equip.Refine_Mhit * 1); ;
                Equip.recover_refine = (short)(Equip.Refine_Regeneration * 8);
                Equip.cri_refine = (short)(Equip.Refine_Lucky * 1);
                Equip.spd_refine = (short)(Equip.Refine_Dexterity * 2);
                Equip.atkrate_refine = (short)(Equip.Refine_ATKrate * 2);
                Equip.matkrate_refine = (short)(Equip.Refine_MATKrate * 2);
                Equip.defrate_refine = (short)(Equip.Refine_Def * 1);
                Equip.mdefrate_refine = (short)(Equip.Refine_Mdef * 2);
            }
            else if (Equip.EquipSlot[0] == EnumEquipSlot.LEFT_HAND)
            {
                Equip.atk_refine = (short)(Equip.Refine_Sharp * 2);
                Equip.matk_refine = (short)(Equip.Refine_Enchanted * 2);
                Equip.hp_refine = (short)(Equip.Refine_Vitality * 10);
                Equip.hit_refine = (short)(Equip.Refine_Hit * 1); ;
                Equip.mhit_refine = (short)(Equip.Refine_Mhit * 1); ;
                Equip.recover_refine = (short)(Equip.Refine_Regeneration * 5);
                Equip.cri_refine = (short)(Equip.Refine_Lucky * 2);
                Equip.spd_refine = (short)(Equip.Refine_Dexterity * 3);
                Equip.atkrate_refine = (short)(Equip.Refine_ATKrate * 4);
                Equip.matkrate_refine = (short)(Equip.Refine_MATKrate * 4);
                Equip.defrate_refine = (short)(Equip.Refine_Def * 1);
                Equip.mdefrate_refine = (short)(Equip.Refine_Mdef * 1);
            }
            else if (Equip.EquipSlot[0] == EnumEquipSlot.RIGHT_HAND)
            {
                Equip.atk_refine = (short)(Equip.Refine_Sharp * 2);
                Equip.matk_refine = (short)(Equip.Refine_Enchanted * 2);
                Equip.hp_refine = (short)(Equip.Refine_Vitality * 10);
                Equip.hit_refine = (short)(Equip.Refine_Hit * 1); ;
                Equip.mhit_refine = (short)(Equip.Refine_Mhit * 1); ;
                Equip.recover_refine = (short)(Equip.Refine_Regeneration * 5);
                Equip.cri_refine = (short)(Equip.Refine_Lucky * 2);
                Equip.spd_refine = (short)(Equip.Refine_Dexterity * 3);
                Equip.atkrate_refine = (short)(Equip.Refine_ATKrate * 4);
                Equip.matkrate_refine = (short)(Equip.Refine_MATKrate * 4);
                Equip.defrate_refine = (short)(Equip.Refine_Def * 1);
                Equip.mdefrate_refine = (short)(Equip.Refine_Mdef * 1);
            }
            else if (Equip.EquipSlot[0] == EnumEquipSlot.UPPER_BODY)
            {
                Equip.atk_refine = (short)(Equip.Refine_Sharp * 1);
                Equip.matk_refine = (short)(Equip.Refine_Enchanted * 1);
                Equip.hp_refine = (short)(Equip.Refine_Vitality * 20);
                Equip.hit_refine = (short)(Equip.Refine_Hit * 1); ;
                Equip.mhit_refine = (short)(Equip.Refine_Mhit * 1); ;
                Equip.recover_refine = (short)(Equip.Refine_Regeneration * 10);
                Equip.cri_refine = (short)(Equip.Refine_Lucky * 1);
                Equip.spd_refine = (short)(Equip.Refine_Dexterity * 2);
                Equip.atkrate_refine = (short)(Equip.Refine_ATKrate * 2);
                Equip.matkrate_refine = (short)(Equip.Refine_MATKrate * 2);
                Equip.defrate_refine = (short)(Equip.Refine_Def * 2);
                Equip.mdefrate_refine = (short)(Equip.Refine_Mdef * 1);
            }
        }
    }
}
