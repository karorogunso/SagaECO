
using System;
using System.Collections.Generic;
using System.Text;
using SagaLib;
using SagaDB.Actor;
using SagaDB.Item;
using SagaMap.Scripting;
using SagaScript.Chinese.Enums;
using WeeklyExploration;
using System.Globalization;
namespace SagaScript.M30210000
{
    public class S50000000 : Event
    {
        public S50000000()
        {
            this.EventID = 50000000;
        }

        public override void OnEvent(ActorPC pc)
        {
            //if (pc.Account.GMLevel > 200)
            {
                ChangeMessageBox(pc);
                if (pc.Account.AccountID <= 247 && pc.CInt["数据转移"] == 1)
                {
                    if (pc.CInt["转移后第一次移动"] != 1)
                    {
                        Say(pc, 0, "您已经经历过数据转移，我送你去过去。", "娜娜依");
                        pc.CInt["转移后第一次移动"] = 1;
                        SagaMap.Manager.ExperienceManager.Instance.ApplyExp(pc, 1, 1, 1f);
                        Warp(pc, 20081000, 12, 16);
                        return;
                    }
                    if (pc.Job == PC_JOB.GLADIATOR && pc.JobLevel3 < 10)
                    {
                        pc.JEXP = 9128226;
                    }
                    Say(pc, 0, "您已经经历过数据转移，我送你去鱼缸岛。", "娜娜依");
                    Warp(pc, 10054000, 197, 165);
                    return;
                }
                if ((pc.Account.AccountID <= 247 || pc.Name == "羽川柠")&& pc.CInt["数据转移"]!= 1)
                {
                    Say(pc, 0, "不好意思，数据转移由于存在大量BUG，暂时关闭维修中。", "娜娜依");
return;
                    Say(pc, 131, "你好呀，欢迎您回归。$R目前通往新版本的传送门已经打开了哦，$R但似乎只能舍弃一些东西后才能通过呢。", "娜娜依");
                    Say(pc, 0, "数据转移内容包括：$R$R清除道具栏及仓库、$R冻结所有当前金钱、$R洗掉身上所有强化");
                    Say(pc, 0, "数据转移将保留：$R当前身上装备的所有物品$R（不包括宠物和特效装备）、$R等级直升新版本的60/25、$R获得3个25强化的机会。$R$R※状态异常的物品将无法被转移！（比如图标变成了问号的）");
                    Say(pc, 0, "当一个角色进行转移后，$R账号的其他角色将无法再使用仓库。$R因此请在转移前，$R将其他角色需要转移的装备从仓库中整理好。");
                    Say(pc, 0, "转移后，可能会产生部分BUG。$R如有遇到，请报给番茄，番茄会尽快修理。");

                    Say(pc, 0, "转移成功后，您会被下线。$R再次上线和我说话就可以啦！", "娜娜依");
                    if (Select(pc, "是否进行数据转移？", "", "进行数据转移", "让我再想想") == 1)
                    {
                        string name = InputBox(pc, "请为女儿起一个好听的名字吧", InputType.PetRename);
                        pc.Name = name;
                        if (SagaMap.MapServer.charDB.CharExists(name))
                        {
                            Say(pc, 0, "名字已存在");
                            return;
                        }

                        pc.CInt["数据转移"] = 1;
                        pc.CInt["免费25强化次数"] = 3;
                        ChangePlayerJob(pc, PC_JOB.GLADIATOR);

                        if (pc.Skills != null)
                        {
                            List<uint> ids = new List<uint>();
                            foreach (var item in pc.Skills.Values)
                                ids.Add(item.ID);
                            foreach (var item in ids)
                                pc.Skills.Remove(item);
                        }
                        if (pc.Skills2 != null)
                        {
                            List<uint> ids = new List<uint>();
                            foreach (var item in pc.Skills2.Values)
                                ids.Add(item.ID);
                            foreach (var item in ids)
                                pc.Skills2.Remove(item);
                        }
                        if (pc.Skills2_1 != null)
                        {
                            List<uint> ids = new List<uint>();
                            foreach (var item in pc.Skills2_1.Values)
                                ids.Add(item.ID);
                            foreach (var item in ids)
                                pc.Skills2_1.Remove(item);
                        }
                        if (pc.Skills2_2 != null)
                        {
                            List<uint> ids = new List<uint>();
                            foreach (var item in pc.Skills2_2.Values)
                                ids.Add(item.ID);
                            foreach (var item in ids)
                                pc.Skills2_2.Remove(item);
                        }
                        if (pc.Skills3 != null)
                        {
                            List<uint> ids = new List<uint>();
                            foreach (var item in pc.Skills3.Values)
                                ids.Add(item.ID);
                            foreach (var item in ids)
                                pc.Skills3.Remove(item);
                        }

                        pc.Account.Bank += (uint)pc.Gold;
                        pc.Gold = 30000;
                        List<uint> ids2 = new List<uint>();
                        foreach (var item in pc.Inventory.Equipments)
                        {
                            if(item.Value != null)
                               if(item.Key != EnumEquipSlot.PET && item.Key != EnumEquipSlot.EFFECT && !ids2.Contains(item.Value.ItemID))
                                ids2.Add(item.Value.ItemID);
                        }
                        if (pc.AInt["第一次转"] != 1)
                        {
                            foreach (var item in pc.Inventory.WareHouse.Keys)
                            {
                                if (pc.Inventory.WareHouse.ContainsKey(item))
                                    pc.Inventory.WareHouse[item].Clear();
                            }
                        }

                        pc.Level = 1;
                        pc.JobLevel3 = 1;
                        pc.CEXP = 30904494;
                        pc.JEXP = 9128226;

                        pc.AInt["第一次转"] = 1;
                        pc.Inventory.Items.Clear();
                        pc.Inventory = new Inventory(pc);
                        for (int i = 0; i < ids2.Count; i++)
                        {
                            if (SagaDB.Item.ItemFactory.Instance.Items.ContainsKey(ids2[i]))
                                GiveItem(pc, ids2[i], 1);
                        }
                        foreach (var item in pc.AnotherPapers)
                            pc.AnotherPapers[item.Key] = new AnotherDetail();
                        SagaMap.MapServer.charDB.SaveChar(pc, true);
                        SagaMap.Network.Client.MapClient.FromActorPC(pc).netIO.Disconnect();
                    }
                }
            }
            /*
            Say(pc, 131, "你好呀，老玩家。$R$R飞行器目前坏掉了呢，$R哪儿都去不了了。", "娜娜依");
            Say(pc, 131, "由于改版后数据改动较大，$R所有“老账号”数据已不适用于新版内容。 ", "系统内容");
            Say(pc, 131, "所有老账号的数据，$R将会在未来进一步考虑数据转移问题，$R$R届时将会按一定程序兑换成$R可以适用于新版的内容，$R因此没有作删档处理。 ", "系统内容");
            Say(pc, 131, "请您耐心等待，$R并先建立新账号进行游戏，$R$R谢谢您的支持哦！$R$R如果您有任何疑问，$R请在群里或者论坛中提出。", "系统内容");
            */
        }
    }
}