using System;
using System.Collections.Generic;
using System.Text;

using SagaDB.Actor;
using SagaMap.Scripting;
//所在地圖:下城(10024000) NPC基本信息:道具箱
namespace SagaScript.M10024000
{
    public class S12001008 : Event
    {
        public S12001008()
        {
            this.EventID = 12001008;
        }

        public override void OnEvent(ActorPC pc)
        {
            PlaySound(pc, 2559, false, 100, 50);
            Say(pc, 0, 131, "會有什麼厲害的道具呢?$R;" +
                "真是叫人期待的道具箱!$R;");
            /*if(//ME.ITEMSLOT_EMPTY < 1) 检查玩家身上是否有空间
            {
                 Call(EVT1200100402);
             return;
            }*/
            switch (Select(pc, "要放什麼徽章?", "", "銅徽章", "埃米爾徽章", "沒興趣"))
            {
                case 1:
                    if (CountItem(pc, 10009500) >= 1)
                    {
                        PlaySound(pc, 2060, false, 100, 50);
                        switch (Select(pc, "要怎麼樣的把手?", "", "『可愛的』", "『帥氣的』", "『實用的』"))
                        {
                            case 1:
                                PlaySound(pc, 2429, false, 100, 50);
                                Wait(pc, 1000);
                                GiveRandomTreasure(pc, "COPPER_D1");
                                TakeItem(pc, 10009500, 1);
                                Say(pc, 0, 131, "得到道具!$R;");
                                break;
                            case 2:
                                PlaySound(pc, 2429, false, 100, 50);
                                Wait(pc, 1000);
                                GiveRandomTreasure(pc, "COPPER_D2");
                                TakeItem(pc, 10009500, 1);
                                Say(pc, 0, 131, "得到道具!$R;");
                                break;
                            case 3:
                                PlaySound(pc, 2429, false, 100, 50);
                                Wait(pc, 1000);
                                GiveRandomTreasure(pc, "COPPER_D3");
                                TakeItem(pc, 10009500, 1);
                                Say(pc, 0, 131, "得到道具!$R;");
                                break;
                        }
                        return;
                    }
                    Say(pc, 0, 131, "沒有銅徽章$R;");
                    break;
                case 2:
                    if (CountItem(pc, 10009550) >= 1)
                    {
                        PlaySound(pc, 2060, false, 100, 50);
                        switch (Select(pc, "要怎麼樣的把手?", "", "『可愛的』", "『帥氣的』", "『實用的』"))
                        {
                            case 1:
                                PlaySound(pc, 2429, false, 100, 50);
                                Wait(pc, 1000);
                                GiveRandomTreasure(pc, "COPPER_D1");
                                TakeItem(pc, 10009550, 1);
                                Say(pc, 0, 131, "得到道具!$R;");
                                break;
                            case 2:
                                PlaySound(pc, 2429, false, 100, 50);
                                Wait(pc, 1000);
                                GiveRandomTreasure(pc, "COPPER_D2");
                                TakeItem(pc, 10009550, 1);
                                Say(pc, 0, 131, "得到道具!$R;");
                                break;
                            case 3:
                                PlaySound(pc, 2429, false, 100, 50);
                                Wait(pc, 1000);
                                GiveRandomTreasure(pc, "COPPER_D3");
                                TakeItem(pc, 10009550, 1);
                                Say(pc, 0, 131, "得到道具!$R;");
                                break;
                        }
                        return;
                    }
                    Say(pc, 0, 131, "沒有埃米爾徽章$R;");
                    break;
                case 3:
                    break;
            }
        }
    }

    public class S12001010 : 道具箱
    {
        public S12001010()
        {
            Init(12001010, 0, "", 1, "");
        }
    }

    public class S12001011 : 道具箱
    {
        public S12001011()
        {
            Init(12001011, 0, "", 1, "");
        }
    }
}
