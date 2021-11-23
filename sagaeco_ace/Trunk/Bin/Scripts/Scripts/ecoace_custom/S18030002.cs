using System;
using System.Collections.Generic;
using System.Text;
using SagaLib;
using SagaDB.Item;
using SagaDB.Map;
using SagaDB.Actor;
using SagaMap.Scripting;
using SagaScript.Chinese.Enums;
namespace SagaScript
{
    public class S18030002 : Event
    {
        public S18030002()
        {
            this.EventID = 18030002;
        }

        public override void OnEvent(ActorPC pc)
        {
            Say(pc, 0, "来吧, 和我做个交易吧!", "爱心泰迪");//90000212 e
            Say(pc, 0, "用你的ECO之心$R$R兑换800个商城点数如何?", "爱心泰迪");
            switch (Select(pc, "要兑换吗?", "", "兑换1个", "兑换10个", "兑换100个","泰迪扭蛋" ,"不要"))
            {
                case 1:
                    {
                        if (CountItem(pc, 22000103) >= 1)
                        {
                            pc.Fame += 1;
                            pc.VShopPoints += 800;
                            TakeItem(pc, 22000103, 1);
                            PlaySound(pc, 2040, false, 100, 50);
                            Wait(pc, 1000);
                            Say(pc, 0, "增加了商城点券800点", "爱心泰迪");
                        }
                        else
                        {
                            Say(pc, 0, "不够啊.....算了吧....", "爱心泰迪");
                        }
                        break;
                    }
                case 2:
                    {
                        if (CountItem(pc, 22000103) >= 10)
                        {
                            pc.Fame += 1;
                            pc.VShopPoints += 8000;
                            TakeItem(pc, 22000103, 10);
                            PlaySound(pc, 2040, false, 100, 50);
                            Wait(pc, 1000);
                            Say(pc, 0, "增加了商城点券8000点", "爱心泰迪");
                            
                        }
                        else
                        {
                            Say(pc, 0, "不够啊.....算了吧....", "爱心泰迪");
                        }
                        break;
                    }
                case 3:
                    {
                        if (CountItem(pc, 22000103) >= 10)
                        {
                            pc.Fame += 10;
                            pc.VShopPoints += 80000;
                            TakeItem(pc, 22000103, 100);
                            PlaySound(pc, 2040, false, 100, 50);
                            Wait(pc, 1000);
                            Say(pc, 0, "增加了商城点券80000点", "爱心泰迪");
                            Wait(pc, 2000);
                            PlaySound(pc, 2040, false, 100, 50);
                            pc.VShopPoints += 10000;
                            Say(pc, 0, "咦? 有优惠? 多给了10000点? $R$R$R商城点券又追加了10000点", "爱心泰迪");
                        }
                        else
                        {
                            Say(pc, 0, "不够啊.....算了吧....", "爱心泰迪");
                        }
                        break;
                    }
                case 4:
                    {
                        Say(pc, 0, "咪呦!$R$R泰迪忘记带礼物了QAQ", "爱心泰迪");
                        Say(pc, 0, "也就是说...暂时无法兑换哦!", "爱心泰迪");
                        Say(pc, 0, "咪呦咪呦!", "爱心泰迪");

                        /*
                        switch (Select(pc, "要兑换吗?", "", "1,000,000点数 兑换随机的99武器", "100,000点数 兑换随机的90武器", "15,000点数 兑换「？？？」", "不要"))
                        {
                            case 1:
                            {
                                if (pc.VShopPoints >= 1000000)
                                {
                                    Say(pc, 0, "好高兴哦!!!$R$R到时候F子可以跪舔你了!!", "爱心泰迪");
                                    pc.VShopPoints -= 1000000;
                                    Say(pc, 0, "扣去了1,000,000点ECOShop点数", "爱心泰迪");
                                    Wait(pc, 1000);
                                    PlaySound(pc, 2040, false, 100, 50);
                                    GiveRandomTreasure(pc, "SPECIAL_99WEAPON");
                                    Say(pc, 0, "获得了珍贵的道具", "爱心泰迪");
                                }
                                else
                                {
                                    Say(pc, 0, "不够啊.....算了吧....", "爱心泰迪");
                                }
                            break;
                            }
                            case 2:
                            {
                                if (pc.VShopPoints >= 100000)
                                {
                                    Say(pc, 0, "好高兴哦!!!$R$R到时候F子可以跪舔你了!!", "爱心泰迪");
                                    pc.VShopPoints -= 100000;
                                    Say(pc, 0, "扣去了100,000点ECOShop点数", "爱心泰迪");
                                    Wait(pc, 1000);
                                    PlaySound(pc, 2040, false, 100, 50);
                                    GiveRandomTreasure(pc, "SPECIAL_90WEAPON");
                                    Say(pc, 0, "获得了珍贵的道具", "爱心泰迪");
                                }
                                else
                                {
                                    Say(pc, 0, "不够啊.....算了吧....", "爱心泰迪");
                                }
                                break;
                            }
                            case 3:
                            {
                                if (pc.VShopPoints >= 15000)
                                {
                                    Say(pc, 0, "这一组...有点坑啊!", "爱心泰迪");
                                    pc.VShopPoints -= 15000;
                                    Say(pc, 0, "扣去了15,000点ECOShop点数", "爱心泰迪");
                                    Wait(pc, 1000);
                                    PlaySound(pc, 2040, false, 100, 50);
                                    GiveRandomTreasure(pc, "SPECIAL_WEAPON");
                                    Say(pc, 0, "获得了一般的道具", "爱心泰迪");
                                }
                                else
                                {
                                    Say(pc, 0, "不够啊.....算了吧....", "爱心泰迪");
                                }
                                break;
                            }
                            case 4:
                            {
                                Say(pc, 0, "呜呜呜呜....", "爱心泰迪");
                                break;
                            }
                        }
                         * */


                        break;
                    }
                case 5:
                    {
                        Say(pc, 0, "肯定有点亏了QAQ....$R$R不勉强哦!", "爱心泰迪");
                        return;
                    }
            }
        }
    }
}