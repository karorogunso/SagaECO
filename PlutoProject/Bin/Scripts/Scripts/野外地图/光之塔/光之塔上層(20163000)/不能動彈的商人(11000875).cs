using System;
using System.Collections.Generic;
using System.Text;
using SagaLib;
using SagaDB.Item;
using SagaDB.Actor;
using SagaMap.Scripting;
using SagaScript.Chinese.Enums;
namespace SagaScript.M20163000
{
    public class S11000875 : Event
    {
        public S11000875()
        {
            this.EventID = 11000875;
        }

        public override void OnEvent(ActorPC pc)
        {
            Say(pc, 131, "到机械文明遗迹$R寻找宝物来的$R;" +
                "不过行李太多了！$R;" +
                "太重了，怎么都搬不动阿$R;" +
                "$R算是帮我的忙$R;" +
                "随便买一件好不好呢？$R;");
            switch (Select(pc, "随便买一个吧？", "", "买", "不买"))
            {
                case 1:
                    OpenShopBuy(pc, 177);
                    Say(pc, 131, "哎呀~还是搬不动呀！$R;");
                    break;
                case 2:
                    break;
            }

        }
    }
}