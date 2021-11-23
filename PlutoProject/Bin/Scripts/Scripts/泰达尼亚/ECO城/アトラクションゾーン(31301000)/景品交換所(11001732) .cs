using System;
using System.Collections.Generic;
using System.Text;

using SagaDB.Actor;
using SagaMap.Scripting;

using SagaLib;
namespace SagaScript.M31301000
{
    public class S11001732 : Event
    {
        public S11001732()
        {
            this.EventID = 11001732;
        }

        public override void OnEvent(ActorPC pc)
        {
            Say(pc, 131, "這裡的ecoin$R;" +
                "可以用來交換$R;" +
                "很不錯的獎品哦。$R;", "獎品交換所");
            Say(pc, 131, "$CR " + pc.Name + " $CD您現在有$R;" +
                "$CR" + pc.ECoin + " $CD枚ecoin$R;" +
                "想交換獎品么？$R;", "獎品交換所");
            if (Select(pc, "想交換獎品么？", "", "不","是")==2)
        	{
                if(Select(pc, "選那類獎品呢？", "", "定期","新作")==1)
        		{
                    switch (DateTime.Now.Month)
                    {
                        case 1:
                            OpenShopBuy(pc, 401);
                            EXITJPJHS(pc);
                            break;
                        case 2:
                            OpenShopBuy(pc, 402);
                            EXITJPJHS(pc);
                            break;
                        case 3:
                            OpenShopBuy(pc, 403);
                            EXITJPJHS(pc);
                            break;
                        case 4:
                            OpenShopBuy(pc, 404);
                            EXITJPJHS(pc);
                            break;
                        case 5:
                            OpenShopBuy(pc, 405);
                            EXITJPJHS(pc);
                            break;
                        case 6:
                            OpenShopBuy(pc, 400);
                            EXITJPJHS(pc);
                            break;
                        case 7:
                            OpenShopBuy(pc, 401);
                            EXITJPJHS(pc);
                            break;
                        case 8:
                            OpenShopBuy(pc, 402);
                            EXITJPJHS(pc);
                            break;
                        case 9:
                            OpenShopBuy(pc, 403);
                            EXITJPJHS(pc);
                            break;
                        case 10:
                            OpenShopBuy(pc, 404);
                            EXITJPJHS(pc);
                            break;
                        case 11:
                            OpenShopBuy(pc, 405);
                            EXITJPJHS(pc);
                            break;
                        case 12:
                            OpenShopBuy(pc, 400);
                            EXITJPJHS(pc);
                            break;
                    }
                    return;
                }
                OpenShopBuy(pc, 406);
            }
        }

        void EXITJPJHS(ActorPC pc)
        {
            Say(pc, 131, "毎月1日的0時00左右$R;" +
                "獎品的販賣陣容會變化$R;" +
                "敬請期待吧♪$R;", "獎品交換所");
            return;
        }
    }
}