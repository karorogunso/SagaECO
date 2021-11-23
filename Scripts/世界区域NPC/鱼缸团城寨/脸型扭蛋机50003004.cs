
using System;
using System.Collections.Generic;
using System.Text;
using SagaLib;
using SagaDB.Actor;
using SagaDB.Item;
using System.Text;
using System.Diagnostics;
using SagaMap.Scripting;
using SagaScript.Chinese.Enums;
namespace SagaScript.FF
{
    public class S50003004 : Event
    {
        public S50003004()
        {
            this.EventID = 50003004;
        }


        public override void OnEvent(ActorPC pc)
        {
            PlaySound(pc, 2559, false, 100, 50);
            Say(pc, 0, "这里是脸型扭蛋机!", "扭蛋机");
            switch (Select(pc, "要怎么办呢？", "", "投入一枚[脸型代币]", "老梗按钮", "随机改变三转外观(10000G/次)", "离开吧"))
            {

                case 1:
                    if (CountItem(pc, 950000001) >= 1)
                    {
                        TakeItem(pc, 950000001, 1);
                        uint id = getrandomfaceid();
                        if (!ItemFactory.Instance.Items.ContainsKey(id))
                        {
                            GiveItem(pc, 950000001, 1);
                            Say(pc, 0, "“呸——”", "扭蛋机");
                            Say(pc, 0, "被退币了..似乎故障了！");
                            return;
                        }
                        GiveItem(pc, id, 1);
                        PlaySound(pc, 2040, false, 100, 50);
                    }
                    else
                    {
                        Say(pc, 0, "嗯...似乎没有代币呢");
                    }
                    break;
                case 2:
                    Say(pc, 0, "你好，我是王宝强，$R$R我现在急要钱，$R给我50000元，$R我到时候还你100倍！", "扭蛋机");
                    Say(pc, 0, "……");
                    Say(pc, 0, "什么鬼");
                    break;
                case 3:
                    Say(pc, 0, "由于UI封包太复杂了！$R所以只能随机了！$R$R          番茄❤$R$R(上面贴着这样的一张纸条。)", "扭蛋机");
                    SagaMap.Map map = SagaMap.Manager.MapManager.Instance.GetMap(pc.MapID);
                    switch (Select(pc, "是否要随机一个三转外观呢？(10000G/次)", "", "是的！(10000G)","我要还原成初始状态(5000G)","离开"))
                    {
                        case 1:
                            if(pc.Gold > 10000)
                            {
                                pc.Gold -= 10000;
                                if(pc.Race == PC_RACE.EMIL)
                                    pc.TailStyle = (byte)Global.Random.Next(1, 3);
                                if(pc.Race == PC_RACE.TITANIA)
                                {
                                    pc.TailStyle = (byte)Global.Random.Next(31, 32);
                                    pc.WingStyle = (byte)Global.Random.Next(35, 39);
                                    pc.WingColor = (byte)Global.Random.Next(45, 55);
                                }
                                if(pc.Race == PC_RACE.DOMINION)
                                {
                                    pc.TailStyle = (byte)Global.Random.Next(61, 63);
                                    pc.WingStyle = (byte)Global.Random.Next(65, 69);
                                    pc.WingColor = (byte)Global.Random.Next(75, 85);
                                }
                                map.SendEventToAllActorsWhoCanSeeActor(SagaMap.Map.EVENT_TYPE.CHAR_INFO_UPDATE, null, pc, true);
                            }
                            break;
                        case 2:
                            if (pc.Gold > 5000)
                            {
                                pc.Gold -= 5000;
                                pc.TailStyle = 0;
                                pc.WingStyle = 0;
                                pc.WingColor = 0;
                                map.SendEventToAllActorsWhoCanSeeActor(SagaMap.Map.EVENT_TYPE.CHAR_INFO_UPDATE, null, pc, true);
                            }

                            break;
                        case 3:
                            break;
                    }
                    break;
            }
        }
        uint getrandomfaceid()
        {
            Dictionary<int, uint> ids = new Dictionary<int, uint>();
            int count = 0;
            foreach (var item in FaceFactory.Instance.FaceItemIDList)
            {
                if (!ids.ContainsValue(item))
                {
                    ids.Add(count, item);
                    count++;
                }
            }
            return ids[SagaLib.Global.Random.Next(1, 17)];
        }
    }
}

