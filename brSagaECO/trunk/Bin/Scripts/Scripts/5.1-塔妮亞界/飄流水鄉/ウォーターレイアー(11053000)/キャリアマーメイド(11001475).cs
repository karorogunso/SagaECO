using System;
using System.Collections.Generic;
using System.Text;

using SagaDB.Actor;
using SagaMap.Scripting;

using SagaLib;
namespace SagaScript.M11053000
{
    public class S11001475 : Event
    {
        public S11001475()
        {
            this.EventID = 11001475;
        }

        public override void OnEvent(ActorPC pc)
        {
            Say(pc, 131, "哦？是客人么？$R;" +
            "$R這裡真是很清闲呢$R;" +
            "我是酒店的店員。$R;" +
            "真是、人們爲什麽不來了$R;" +
            "隨時都做好了準備地說！$R;" +
            "$P明明還能接任務$R;" +
            "这里还能买东西哦！$R;", "キャリアマーメイド");
            Say(pc, 131, "我现在很闲，不知道有什么能帮忙的吗？$R;", "キャリアマーメイド");
            switch (Select(pc, "我能幫你什麽", "", ",沒什麼事情", "任務柜台ー", "買東西", "賣東西"))
            {

                case 2:
                    HandleQuest(pc, 6);
                    break;
                case 3:
                    OpenShopBuy(pc, 4);
                    break;
                case 4:
                    OpenShopSell(pc, 4);
                    break;
                    
            }
            
        }


    }
}

