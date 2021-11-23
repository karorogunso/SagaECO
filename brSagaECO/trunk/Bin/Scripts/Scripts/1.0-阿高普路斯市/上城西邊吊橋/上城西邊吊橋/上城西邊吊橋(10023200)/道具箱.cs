using System;
using System.Collections.Generic;
using System.Text;

using SagaDB.Actor;
using SagaMap.Scripting;

using SagaLib;
using SagaScript.Chinese.Enums;
//所在地圖:上城西邊吊橋(10023200) NPC基本信息:道具箱
namespace SagaScript.M10023200
{
    public class S12001000 : Event
    {
        public S12001000()
        {
            this.EventID = 12001000;
        }

        public override void OnEvent(ActorPC pc)
        {
            PlaySound(pc, 2559, false, 100, 50);
            Say(pc, 0, 131, "會有什麼厲害的道具呢?$R;" +
                "道具箱，真令人期待呢！$R;");
            /*if(//ME.ITEMSLOT_EMPTY < 1) 检查玩家身上是否有空间
            {
                 Call(EVT1200100402);
             return;
            }*/
            switch (Select(pc, "要放徽章嗎?", "", "金徽章1個", "沒興趣"))
            {
                case 1:
                    if (CountItem(pc, 10009700) >= 1)
                    {
                        PlaySound(pc, 2060, false, 100, 50);
                        switch (Select(pc, "要怎麼樣的把手?", "", "『可愛的』", "『帥氣的』", "『實用的』"))
                        {
                            case 1:
                                PlaySound(pc, 2429, false, 100, 50);
                                Wait(pc, 1000);
                                GiveRandomTreasure(pc, "GOLD_A1");
                                TakeItem(pc, 10009700, 1);
                                Say(pc, 0, 131, "得到道具!$R;");
                                break;
                            case 2:
                                PlaySound(pc, 2429, false, 100, 50);
                                Wait(pc, 1000);
                                GiveRandomTreasure(pc, "GOLD_A2");
                                TakeItem(pc, 10009700, 1);
                                Say(pc, 0, 131, "得到道具!$R;");
                                break;
                            case 3:
                                PlaySound(pc, 2429, false, 100, 50);
                                Wait(pc, 1000);
                                GiveRandomTreasure(pc, "GOLD_A1");
                                TakeItem(pc, 10009700, 1);
                                Say(pc, 0, 131, "得到道具!$R;");
                                break;
                        }
                        return;
                    }
                    Say(pc, 0, 131, "沒有金徽章$R;");
                    break;
                case 2:
                    break;
            }
        }
    }



    public class S12001001 : Event
    {
        public S12001001()
        {
            this.EventID = 12001001;
        }

        public override void OnEvent(ActorPC pc)
        {
            PlaySound(pc, 2559, false, 100, 50);
            Say(pc, 0, 131, "會有什麼厲害的道具呢?$R;" +
                "道具箱，真令人期待呢！$R;");
            /*if(//ME.ITEMSLOT_EMPTY < 1) 检查玩家身上是否有空间
            {
                 Call(EVT1200100402);
             return;
            }*/
            switch (Select(pc, "要放徽章嗎?", "", "金徽章1個", "沒興趣"))
            {
                case 1:
                    if (CountItem(pc, 10009700) >= 1)
                    {
                        PlaySound(pc, 2060, false, 100, 50);
                        switch (Select(pc, "要怎麼樣的把手?", "", "『可愛的』", "『帥氣的』", "『實用的』"))
                        {
                            case 1:
                                PlaySound(pc, 2429, false, 100, 50);
                                Wait(pc, 1000);
                                GiveRandomTreasure(pc, "GOLD_A1");
                                TakeItem(pc, 10009700, 1);
                                Say(pc, 0, 131, "得到道具!$R;");
                                break;
                            case 2:
                                PlaySound(pc, 2429, false, 100, 50);
                                Wait(pc, 1000);
                                GiveRandomTreasure(pc, "GOLD_A2");
                                TakeItem(pc, 10009700, 1);
                                Say(pc, 0, 131, "得到道具!$R;");
                                break;
                            case 3:
                                PlaySound(pc, 2429, false, 100, 50);
                                Wait(pc, 1000);
                                GiveRandomTreasure(pc, "GOLD_A1");
                                TakeItem(pc, 10009700, 1);
                                Say(pc, 0, 131, "得到道具!$R;");
                                break;
                        }
                        return;
                    }
                    Say(pc, 0, 131, "沒有金徽章$R;");
                    break;
                case 2:
                    break;
            }
        }
    }
}