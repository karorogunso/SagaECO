using System;
using System.Collections.Generic;
using System.Text;

using SagaDB.Actor;
using SagaMap.Scripting;

using SagaLib;
namespace SagaScript.M31304000
{
    public class S11001862 : Event
    {
        public S11001862()
        {
            this.EventID = 11001862;
        }

        public override void OnEvent(ActorPC pc)
        {
            Say(pc, 0, "ねえねえっ！$R;" +
            "$R女子脱衣室に現れる$R;" +
            "謎の影の正体は$R;" +
            "青いヌルヌルの身体をした$R;" +
            "おじいさんなんだって！$R;", "口軽のクレー");

            Say(pc, 11001861, 0, "クレー！$R;" +
            "$Rそれ……、なんか違う……。$R;", "噂好きのエル");

            Say(pc, 0, "あれっ？$R;", "口軽のクレー");
        }


    }
}


