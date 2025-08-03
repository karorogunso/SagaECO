using System;
using System.Collections.Generic;
using System.Text;

using SagaDB.Actor;
using SagaMap.Scripting;

using SagaDB.Item;
namespace SagaScript.M10059100
{
    public class S11000069 : Event
    {
        public S11000069()
        {
            this.EventID = 11000069;
        }

        public override void OnEvent(ActorPC pc)
        {
            switch (Select(pc, "想要做什么事情呢?", "", "买东西", "卖东西", "到银行存款", "到银行取款", "使用仓库（" + 100 + "）", "什么也不做"))
            {
                case 1:
                    OpenShopBuy(pc, 1003);
                    break;

                case 2:
                    OpenShopSell(pc, 1003);
                    break;

                case 3:
                    BankDeposit(pc);
                    break;

                case 4:
                    BankWithdraw(pc);
                    break;

                case 5:
                    if (pc.Gold >= 100)
                    {
                        pc.Gold -= 100;

                        OpenWareHouse(pc, WarehousePlace.MaimaiCamp);
                    }
                    else
                    {
                        Say(pc, 131, "资金不足哦!$R;", "行会商人");
                    }
                    break;
                case 6: 
                    break;
            }
        }
    }
        


    
    //public class S11000069 : 行會商人
    //{
    //    public S11000069()
    //    {
    //       // Init(11000069, 218, 500, WarehousePlace.MaimaiCamp);
           
    //    }
    //}
}
