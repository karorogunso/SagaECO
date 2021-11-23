using System;
using System.Collections.Generic;
using System.Text;

using SagaDB.Actor;
using SagaMap.Scripting;

using SagaLib;
using SagaScript.Chinese.Enums;

namespace SagaScript
{
    public abstract class ecoin櫃檯 : Event
    {
        public ecoin櫃檯()
        {

        }

        public override void OnEvent(ActorPC pc)
        {
            switch (Select(pc, "有什么要咨询的么！", "", "没必要", "『ecoin』购买", "到银行存钱", "到银行取钱"))
            {
                case 2:
                    Say(pc, 131, "$CR " + pc.Name + " $CD您现在有$R;" +
                    "$CR" + pc.ECoin + " $CD枚ecoin。$R;", "ecoin柜台");

                    Say(pc, 131, "ecoin交换不能超过999999$R;" +
                    "否则不能交换。$R;" +
                    "$R$CR1$CD枚ecoin需要$CR100$CDgold哦$R;" +
                    "需要购入多少枚呢？$R;", "ecoin柜台");
                    string temp = InputBox(pc, "输入要购买的枚数", InputType.Bank);
                    if (temp != "")
                    {
                        uint ecop = uint.Parse(temp);
                        Say(pc, 131, "购买" + temp + "枚ecoin$R;" +
                       "需要$CR" + temp + "00$CDgold哦。$R;", "ecoin柜台");
                        if (Select(pc, "怎么办？", "", "购买", "不购买") == 1)
                        {
                            if (pc.Gold >= (ecop * 100))
                            {
                                pc.Gold -= (int)(ecop * 100);
                                pc.ECoin += ecop;
                                Say(pc, 131, "购买了$R;" +
                                "$CR" + temp + "$CD枚ecoin。$R;", "ecoin柜台");
                            }
                            else
                            {
                                Say(pc, 131, "钱不够...$R;", "ecoin柜台");
                            }
                        }
                    }
                    break;
                case 3:
                    BankDeposit(pc);
                    break;

                case 4:
                    BankWithdraw(pc);
                    break;
            }
            Say(pc, 11001724, 131, "那么请继续$R;" +
            "享受ＥＣＯ城吧！$R;", "ecoin柜台");
        }
    }
}