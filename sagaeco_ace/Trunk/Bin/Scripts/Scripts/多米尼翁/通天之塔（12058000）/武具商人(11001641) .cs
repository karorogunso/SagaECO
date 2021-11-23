using System;
using System.Collections.Generic;
using System.Text;
using SagaLib;
using SagaDB.Actor;
using SagaMap.Scripting;

using SagaScript.Chinese.Enums;
namespace SagaScript.M12002101
{
    public class S11001641 : Event
    {
        public S11001641()
        {
            this.EventID = 11001641;
        }

        public override void OnEvent(ActorPC pc)
        {
            Say(pc, 131, "痛痛……。$R;" +
            "突然袭击之类的$R;" +
            "太过分……。$R;" +
            "$R啊、有什么可以帮助你的？$R;", "武具商人");
            switch (Select(pc, "有什么可以帮助你的？", "", "看武器和防具", "卖东西", "到银行存款", "到银行取款", "使用仓库（500G）", "什么都不做"))
            {
                case 1:
                    switch (Select(pc, "有什么可以帮助你的？", "", "买武器", "买防具", "买其他武器", "什么也不做"))
                    {
                        case 1:
                            OpenShopBuy(pc, 266);
                            break;
                        case 2:
                            OpenShopBuy(pc, 267);
                            break;
                        case 3:
                            OpenShopBuy(pc, 268);
                            break;
                    }
                    break;
                case 2:
                    OpenShopSell(pc, 266);
                    break;
                case 3:
                    BankDeposit(pc);
                    break;
                case 4:
                    BankWithdraw(pc);
                    break;
                case 5:
                    Say(pc, 131, "還沒裝額$R;", "よろず商人");
                    break;
            }

        }

    }

}
            
            
        
     
    