using System;
using System.Collections.Generic;
using System.Text;

using SagaDB.Actor;
using SagaMap.Scripting;
//所在地圖:上城南邊吊橋(10023300) NPC基本信息:道具箱
namespace SagaScript.M10023300
{
    public class S12001006 : Event
    {
        public S12001006()
        {
            this.EventID = 12001006;
        }

        public override void OnEvent(ActorPC pc)
        {
            PlaySound(pc, 2559, false, 100, 50);
            Say(pc, 0, 131, "会有什么厉害的道具呢?$R;" +
                "真是叫人期待的道具箱!$R;");
            /*if(//ME.ITEMSLOT_EMPTY < 1) 检查玩家身上是否有空间
            {
                 Call(EVT1200100402);
             return;
            }*/
            switch (Select(pc, "要放什么徽章?", "", "铜徽章", "埃米尔徽章", "没兴趣"))
            {
                case 1:
                    if (CountItem(pc, 10009500) >= 1)
                    {
                        PlaySound(pc, 2060, false, 100, 50);
                        switch (Select(pc, "要怎么样的把手?", "", "『可爱的』", "『帅气的』", "『实用的』"))
                        {
                            case 1:
                                PlaySound(pc, 2429, false, 100, 50);
                                Wait(pc, 1000);
                                GiveRandomTreasure(pc, "COPPER_C1");
                                TakeItem(pc, 10009500, 1);
                                Say(pc, 0, 131, "得到道具!$R;");
                                break;
                            case 2:
                                PlaySound(pc, 2429, false, 100, 50);
                                Wait(pc, 1000);
                                GiveRandomTreasure(pc, "COPPER_C2");
                                TakeItem(pc, 10009500, 1);
                                Say(pc, 0, 131, "得到道具!$R;");
                                break;
                            case 3:
                                PlaySound(pc, 2429, false, 100, 50);
                                Wait(pc, 1000);
                                GiveRandomTreasure(pc, "COPPER_C3");
                                TakeItem(pc, 10009500, 1);
                                Say(pc, 0, 131, "得到道具!$R;");
                                break;
                        }
                        return;
                    }
                    Say(pc, 0, 131, "没有铜徽章$R;");
                    break;
                case 2:
                    if (CountItem(pc, 10009550) >= 1)
                    {
                        PlaySound(pc, 2060, false, 100, 50);
                        switch (Select(pc, "要怎么样的把手?", "", "『可爱的』", "『帅气的』", "『实用的』"))
                        {
                            case 1:
                                PlaySound(pc, 2429, false, 100, 50);
                                Wait(pc, 1000);
                                GiveRandomTreasure(pc, "COPPER_C1");
                                TakeItem(pc, 10009550, 1);
                                Say(pc, 0, 131, "得到道具!$R;");
                                break;
                            case 2:
                                PlaySound(pc, 2429, false, 100, 50);
                                Wait(pc, 1000);
                                GiveRandomTreasure(pc, "COPPER_C2");
                                TakeItem(pc, 10009550, 1);
                                Say(pc, 0, 131, "得到道具!$R;");
                                break;
                            case 3:
                                PlaySound(pc, 2429, false, 100, 50);
                                Wait(pc, 1000);
                                GiveRandomTreasure(pc, "COPPER_C3");
                                TakeItem(pc, 10009550, 1);
                                Say(pc, 0, 131, "得到道具!$R;");
                                break;
                        }
                        return;
                    }
                    Say(pc, 0, 131, "没有埃米尔徽章$R;");
                    break;
                case 3:
                    break;
            }
        }
    }

    public class S12001007 : Event
    {
        public S12001007()
        {
            this.EventID = 12001007;
        }

        public override void OnEvent(ActorPC pc)
        {
            PlaySound(pc, 2559, false, 100, 50);
            Say(pc, 0, 131, "会有什么厉害的道具呢?$R;" +
                "真是叫人期待的道具箱!$R;");
            /*if(//ME.ITEMSLOT_EMPTY < 1) 检查玩家身上是否有空间
            {
                 Call(EVT1200100402);
             return;
            }*/
            switch (Select(pc, "要放什么徽章?", "", "铜徽章", "埃米尔徽章", "没兴趣"))
            {
                case 1:
                    if (CountItem(pc, 10009500) >= 1)
                    {
                        PlaySound(pc, 2060, false, 100, 50);
                        switch (Select(pc, "要怎么样的把手?", "", "『可爱的』", "『帅气的』", "『实用的』"))
                        {
                            case 1:
                                PlaySound(pc, 2429, false, 100, 50);
                                Wait(pc, 1000);
                                GiveRandomTreasure(pc, "COPPER_C1");
                                TakeItem(pc, 10009500, 1);
                                Say(pc, 0, 131, "得到道具!$R;");
                                break;
                            case 2:
                                PlaySound(pc, 2429, false, 100, 50);
                                Wait(pc, 1000);
                                GiveRandomTreasure(pc, "COPPER_C2");
                                TakeItem(pc, 10009500, 1);
                                Say(pc, 0, 131, "得到道具!$R;");
                                break;
                            case 3:
                                PlaySound(pc, 2429, false, 100, 50);
                                Wait(pc, 1000);
                                GiveRandomTreasure(pc, "COPPER_C3");
                                TakeItem(pc, 10009500, 1);
                                Say(pc, 0, 131, "得到道具!$R;");
                                break;
                        }
                        return;
                    }
                    Say(pc, 0, 131, "没有铜徽章$R;");
                    break;
                case 2:
                    if (CountItem(pc, 10009550) >= 1)
                    {
                        PlaySound(pc, 2060, false, 100, 50);
                        switch (Select(pc, "要怎么样的把手?", "", "『可爱的』", "『帅气的』", "『实用的』"))
                        {
                            case 1:
                                PlaySound(pc, 2429, false, 100, 50);
                                Wait(pc, 1000);
                                GiveRandomTreasure(pc, "COPPER_C1");
                                TakeItem(pc, 10009550, 1);
                                Say(pc, 0, 131, "得到道具!$R;");
                                break;
                            case 2:
                                PlaySound(pc, 2429, false, 100, 50);
                                Wait(pc, 1000);
                                GiveRandomTreasure(pc, "COPPER_C2");
                                TakeItem(pc, 10009550, 1);
                                Say(pc, 0, 131, "得到道具!$R;");
                                break;
                            case 3:
                                PlaySound(pc, 2429, false, 100, 50);
                                Wait(pc, 1000);
                                GiveRandomTreasure(pc, "COPPER_C3");
                                TakeItem(pc, 10009550, 1);
                                Say(pc, 0, 131, "得到道具!$R;");
                                break;
                        }
                        return;
                    }
                    Say(pc, 0, 131, "没有埃米尔徽章$R;");
                    break;
                case 3:
                    break;
            }
        }
    }
}
