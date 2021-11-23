using System;
using System.Collections.Generic;
using System.Text;
using SagaLib;
using SagaDB.Actor;
using SagaMap.Scripting;
using SagaScript.Chinese.Enums;
namespace SagaScript.M30069002
{
    public class S20010011 : Event
    {
        public S20010011()
        {
            this.EventID =20010011;
        }

        public override void OnEvent(ActorPC pc)
        {
            Say(pc, 113, "汪汪~$R;" +
                "$R欢迎光临~$R;");
            switch (Select(pc, "“想做什么？”", "", "买吃的","买..“那个”", "什么都不做"))
            { 
                case 1:
                    OpenShopBuy(pc, 2009);
                    break;
                case 2:
                    OpenShopBuy(pc, 2010);
                    break;
                case 3:
                    break;
            }
            //Say(pc, 113, "汪汪~$R;" +
            //    "$R进不去吗？$R;");
            //switch (Select(pc, "“想买那个吗？”", "", "买", "不买"))
            //{
            //    case 1:
            //        OpenShopBuy(pc, 2010);
            //        //Say(pc, 135, "目前因为生产系并未实装，所以法伊斯特门票由我暂时销售~$R;");
            //        break;
            //    case 2:
            //        break;
            //}
        }
    }
}