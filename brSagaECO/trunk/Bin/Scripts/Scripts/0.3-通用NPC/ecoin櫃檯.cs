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
            switch (Select(pc, "有什麽要諮詢的么！", "", "沒必要", "『ecoin』購買", "到銀行存錢", "到銀行取錢"))
            {
                case 2:
                    Say(pc, 131, "$CR " + pc.Name + " $CD您現在有$R;" +
                    "$CR" + pc.ECoin + " $CD枚ecoin。$R;", "ecoin櫃檯");

                    Say(pc, 131, "ecoin交換不能超過999999$R;" +
                    "否則不能交換。$R;" +
                    "$R$CR1$CD枚ecoin需要$CR100$CDgold哦$R;" +
                    "需要購入多少枚呢？$R;", "ecoin櫃檯");
                    string temp = InputBox(pc, "輸入要購買的枚數", InputType.Bank);
                    if (temp != "")
                    {
                        uint ecop = uint.Parse(temp);
                        Say(pc, 131, "購買" + temp + "枚ecoin$R;" +
                       "需要$CR" + temp + "00$CDgold哦。$R;", "ecoin櫃檯");
                        if (Select(pc, "怎麼辦？", "", "購買", "不購買") == 1)
                        {
                            if (pc.Gold >= (ecop * 100))
                            {
                                pc.Gold -= (int)(ecop * 100);
                                pc.ECoin += ecop;
                                Say(pc, 131, "購買了$R;" +
                                "$CR" + temp + "$CD枚ecoin。$R;", "ecoin櫃檯");
                            }
                            else
                            {
                                Say(pc, 131, "钱不够...$R;", "ecoin櫃檯");
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
            Say(pc, 11001724, 131, "那麼請繼續$R;" +
            "享受ＥＣＯ城吧！$R;", "ecoin櫃檯");
        }
    }
}