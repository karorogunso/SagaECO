using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using SagaLib;
using SagaDB.Actor;
using SagaMap.Scripting;
using SagaScript.Chinese.Enums;
using SagaDB.Mob;
using SagaMap;

namespace SagaScript.Scripts
{
    public class test : Event
    {
        public test()
        {
            this.EventID = 8500000;
        }
        public override void OnEvent(ActorPC pc)
        {
            Say(pc, 131, "欢迎使用奇怪的战斗测试机");
            string mobid = InputBox(pc, "请输入怪物ID", InputType.Bank);
            if (!MobFactory.Instance.Mobs.ContainsKey(uint.Parse(mobid)))
            {
                Say(pc, 131, "不存在这个魔物");
                return;
            }
            string num = InputBox(pc, "请输入数量", InputType.Bank);
            MobData md = MobFactory.Instance.GetMobData(uint.Parse(mobid));
            int mobnum = int.Parse(num);

            Dictionary<string, int> drops = new Dictionary<string, int>();
            MobData.DropData dd;
            SagaMap.Network.Client.MapClient.FromActorPC(pc).SendSystemMessage("测试对象:" + md.name + " 数量: " + num);
            SagaMap.Network.Client.MapClient.FromActorPC(pc).SendSystemMessage("-----------------------------基本掉落---------------------------");
            int outputindex = 0;
            if (md.dropItems.Count == 0)
            {
                SagaMap.Network.Client.MapClient.FromActorPC(pc).SendSystemMessage("无掉落");
            }
            else
            {
                foreach (var item in md.dropItems)
                {
                    if (md.dropItems[outputindex].TreasureGroup != "")
                        SagaMap.Network.Client.MapClient.FromActorPC(pc).SendSystemMessage("掉落" + (outputindex + 1) + ": " + md.dropItems[outputindex].TreasureGroup + " 机率: " + md.dropItems[outputindex].Rate);
                    else
                        SagaMap.Network.Client.MapClient.FromActorPC(pc).SendSystemMessage("掉落" + (outputindex + 1) + ": " + (md.dropItems[outputindex].ItemID != 10000000? SagaDB.Item.ItemFactory.Instance.Items[md.dropItems[outputindex].ItemID].name:"什么都不掉") + " 机率: " + md.dropItems[outputindex].Rate);

                    outputindex++;
                }
            }

            SagaMap.Network.Client.MapClient.FromActorPC(pc).SendSystemMessage("-----------------------------战斗测试---------------------------");
            for (int i = 1; i <= mobnum; i++)
            {
                dd = null;

                int lastdropline = 0, nowdropline = 0;
                if (md.dropItems.Count > 0)
                {
                    bool oneshotdrop = false;
                    int dropline = Global.Random.Next(1, md.dropItems.Sum(x => x.Rate));
                    foreach (SagaDB.Mob.MobData.DropData mobd in md.dropItems)
                    {
                        if (oneshotdrop)
                            continue;

                        nowdropline = lastdropline + (int)(mobd.Rate * 1);
                        if (dropline > lastdropline && dropline <= nowdropline)
                        {
                            dd = mobd;
                            oneshotdrop = true;
                        }
                        lastdropline = nowdropline;
                    }

                    if (dd != null)
                    {
                        if (dd.TreasureGroup != "")
                        {
                            if (drops.ContainsKey(dd.TreasureGroup))
                                drops[dd.TreasureGroup] += 1;
                            else
                                drops.Add(dd.TreasureGroup, 1);
                        }
                        else
                        {
                            if (drops.ContainsKey(dd.ItemID.ToString()))
                                drops[dd.ItemID.ToString()] += 1;
                            else
                                drops.Add(dd.ItemID.ToString(), 1);
                        }
                    }
                }
            }
            outputindex = 0;
            uint id;
            SagaMap.Network.Client.MapClient.FromActorPC(pc).SendSystemMessage("-----------------------------基本掉落结果---------------------------");
            if (drops.Count > 0)
                foreach (var item in drops)
                {
                    id = 0;
                    if (uint.TryParse(item.Key, out id))
                        SagaMap.Network.Client.MapClient.FromActorPC(pc).SendSystemMessage("掉落" + (++outputindex) + ":" + (id != 10000000? SagaDB.Item.ItemFactory.Instance.Items[id].name: "什么都没掉") + " 数量:" + item.Value);
                    else
                        SagaMap.Network.Client.MapClient.FromActorPC(pc).SendSystemMessage("掉落" + (++outputindex) + ":" + item.Key + " 数量:" + item.Value);
                }
            else
                SagaMap.Network.Client.MapClient.FromActorPC(pc).SendSystemMessage("未掉落任何物品");

        }
    }
}

