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
            Say(pc, 131, "到機械文明時代$R尋找寶物來的$R;" +
                "不過行李太多了！$R;" +
                "太重了，怎麼都搬不動阿$R;" +
                "$R算是幫我的忙$R;" +
                "隨便買一件好不好呢？$R;");
            switch (Select(pc, "隨便買一個吧？", "", "買", "不買"))
            {
                case 1:
                    OpenShopBuy(pc, 177);
                    Say(pc, 131, "哎呀~還是搬不動呀！$R;");
                    break;
                case 2:
                    break;
            }

        }
    }
}