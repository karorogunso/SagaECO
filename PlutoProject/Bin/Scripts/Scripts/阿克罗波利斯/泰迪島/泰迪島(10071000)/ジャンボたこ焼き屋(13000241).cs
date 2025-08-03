using System;
using System.Collections.Generic;
using System.Text;

using SagaDB.Actor;
using SagaMap.Scripting;
namespace SagaScript.M10071000
{
    public class S13000241 : Event
    {
        public S13000241()
        {
            this.EventID = 13000241;
        }

        public override void OnEvent(ActorPC pc)
        {
            Say(pc, 131, "ジャンボでクールなたこ焼きだよ！$R;" +
            "もちろん、味も天下一！$R;" +
            "冷めているのが玉に瑕！$R;", "ジャンボたこ焼き屋");
            OpenShopBuy(pc, 252);

        }
    }
}