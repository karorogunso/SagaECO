using System;
using System.Collections.Generic;
using System.Text;

using SagaDB.Actor;
using SagaDB.Item;
using SagaMap.Scripting;

using SagaLib;
using SagaScript.Chinese.Enums;
namespace SagaScript.M50007000
{
    public class S11001083 : Event
    {
        public S11001083()
        {
            this.EventID = 11001083;
        }

        public override void OnEvent(ActorPC pc)
        {
            BitMask<Neko_03> Neko_03_amask = pc.AMask["Neko_03"];
            BitMask<Neko_03> Neko_03_cmask = pc.CMask["Neko_03"];

            if (Neko_03_amask.Test(Neko_03.堇子任務開始) &&
                !Neko_03_amask.Test(Neko_03.堇子任務完成) &&
                 Neko_03_cmask.Test(Neko_03.找到理路) &&
                 !Neko_03_cmask.Test(Neko_03.使用了電晶體))
            {
                Say(pc, 11001083, 131, "$P这裡的机器人他们…$R都一样进去了$R我偷偷进入的飞空庭里呀$R;" +
                    "$R而且还有很多其他的喔$R;" +
                    "$P那些大叔$R好像做了什么实验似的…$R;");
            }
            else
            {
                Say(pc, 11001083, 131, "……！$R;" +
                    "$R……是谁呀?$R;");
                Say(pc, 0, 131, "这孩子是「理路」吗…?$R;");
                if (pc.Inventory.Equipments.ContainsKey(EnumEquipSlot.PET))
                {
                    if (pc.Inventory.Equipments[EnumEquipSlot.PET].ItemID == 10017905)
                    {
                        對話(pc);
                    }
                }
                else if (CountItem(pc, 10017905) >= 1)
                {
                    對話(pc);
                }
                else if (pc.Inventory.Equipments.ContainsKey(EnumEquipSlot.PET))
                {
                    if (pc.Inventory.Equipments[EnumEquipSlot.PET].ItemID == 10017900)
                    {
                        對話(pc);
                    }
                }
                else if (CountItem(pc, 10017900) >= 1)
                {
                    對話(pc);
                }
                else if (pc.Inventory.Equipments.ContainsKey(EnumEquipSlot.PET))
                {
                    if (pc.Inventory.Equipments[EnumEquipSlot.PET].ItemID == 10017902)
                    {
                        對話(pc);
                    }
                }
                else if (CountItem(pc, 10017902) >= 1)
                {
                    對話(pc);
                }
                else if (pc.Inventory.Equipments.ContainsKey(EnumEquipSlot.PET))
                {
                    if (pc.Inventory.Equipments[EnumEquipSlot.PET].ItemID == 10017903)
                    {
                        對話(pc);
                    }
                }
                else if (CountItem(pc, 10017903) >= 1)
                {
                    對話(pc);
                }
                Say(pc, 0, 131, "告诉理路，$R到现在为止发生的事情了$R;");
                Neko_03_cmask.SetValue(Neko_03.找到理路, true);
                Say(pc, 11001083, 131, "…原来如此呀……嗯$R;" +
                    "$R本来在飞空庭里等玛莎姐姐的$R后来突然几个恐怖的大叔$R把我抓到这里来$R;" +
                    "$P这里的机器人他们…$R都一样进去了$R我偷偷进入的飞空庭里呀$R;" +
                    "$R而且还有很多其他的喔$R;" +
                    "$P那些大叔$R好像做了什么实验似的…$R;");
                Say(pc, 11001083, 131, "哦?$R……您说是爸爸??$R;" +
                    "$P……不可能的$R妈妈说我还很小的时候$R爸爸就已经去世了$R;" +
                    "$R…东边国家那裡有亲戚…$R难道…$R…就是说爸爸吗？$R;");
            }

            PlaySound(pc, 2437, false, 100, 50);
            Wait(pc, 1000);
            PlaySound(pc, 2431, false, 100, 50);
            Wait(pc, 1000);
            PlaySound(pc, 2122, false, 100, 50);
            ShowEffect(pc, 10, 9, 5612);
            Wait(pc, 2000);
            PlaySound(pc, 2122, false, 100, 50);
            ShowEffect(pc, 6, 7, 5612);
            Wait(pc, 1000);
            PlaySound(pc, 2122, false, 100, 50);
            ShowEffect(pc, 8, 6, 5612);
            Wait(pc, 1000);
            PlaySound(pc, 2231, false, 100, 50);
            Say(pc, 0, 131, "……！！$R…真是！$R;" +
                "$R哎！麦加机器人…！$R;");
            pc.CInt["Neko_03_Map3"] = CreateMapInstance(50008000, 30002000, 4, 2);

            LoadSpawnFile(pc.CInt["Neko_03_Map3"], "DB/Spawns/50008000.xml");

            Warp(pc, (uint)pc.CInt["Neko_03_Map3"], 8, 5);
            //EVENTMAP_IN 8 1 8 4 0
            /*
            //ME.WORK0 = -1 EVT1100108312
            //EVENTEND
            //EVT1100108312
            _7A39 = false;
            Say(pc, 0, 131, "這裡的麥加機器人…$R好像還活著，還在動耶…$R;" +
                "$R要帶著年輕的理路逃出去！$R;");
            //EVENTEND*/
        }

        void 對話(ActorPC pc)
        {
            
            if (pc.Inventory.Equipments.ContainsKey(EnumEquipSlot.PET))
            {
                if (pc.Inventory.Equipments[EnumEquipSlot.PET].ItemID == 10017905)
                {
                    Say(pc, 0, 111, "喵喵？$R;", "猫灵（山吹）");
                }
            }
            else if (CountItem(pc, 10017905) >= 1)
            {
                Say(pc, 0, 111, "喵喵？$R;", "猫灵（山吹）");
            }
            //EVT1100108303
            if (pc.Inventory.Equipments.ContainsKey(EnumEquipSlot.PET))
            {
                if (pc.Inventory.Equipments[EnumEquipSlot.PET].ItemID == 10017900)
                {
                    Say(pc, 0, 111, "喵！$R;", "猫灵（桃子）");
                }
            }
            else if (CountItem(pc, 10017900) >= 1)
            {
                Say(pc, 0, 111, "喵！$R;", "猫灵（桃子）");
            }
            //EVT1100108305
            if (pc.Inventory.Equipments.ContainsKey(EnumEquipSlot.PET))
            {
                if (pc.Inventory.Equipments[EnumEquipSlot.PET].ItemID == 10017902)
                {
                    Say(pc, 0, 111, "喵喵嗷……$R;", "猫灵（緑）");
                    return;
                }
            }
            else if (CountItem(pc, 10017902) >= 1)
            {
                Say(pc, 0, 111, "喵喵嗷……$R;", "猫灵（緑）");
                return;
            }
            //EVT1100108307
            if (pc.Inventory.Equipments.ContainsKey(EnumEquipSlot.PET))
            {
                if (pc.Inventory.Equipments[EnumEquipSlot.PET].ItemID == 10017903)
                {
                    Say(pc, 0, 111, "…喵$R;", "猫灵（蓝子）");
                }
            }
            else if (CountItem(pc, 10017903) >= 1)
            {
                Say(pc, 0, 111, "…喵$R;", "猫灵（蓝子）");
            }
            Say(pc, 0, 111, "……咦?$R;" +
                "$R好像听到猫的叫声呢……$R;");
        }
    }
}