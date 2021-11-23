using System;
using System.Collections.Generic;
using System.Text;

using SagaDB.Actor;
using SagaMap.Scripting;

using SagaLib;
namespace SagaScript.M31304000
{
    public class S11001871 : Event
    {
        public S11001871()
        {
            this.EventID = 11001871;
        }

        public override void OnEvent(ActorPC pc)
        {
            Say(pc, 0, "啊啊、我的西瑞安！$R;" +
            "$R為何你這麼$R;" +
            "優美如此……？$R;" +
            "$R沐浴在你吐出的神聖之水$R;" +
            "你真是幸福唷……。$R;", "西瑞安信奉者");
            
            /* Say(pc, 0, "ああっ、僕のキリオンッ！$R;" +
            "$Rなぜキミはこんなにも$R;" +
            "優美なんだい……？$R;" +
            "$Rキミが吐き出す聖なる水に浸かれて$R;" +
            "僕は本当に幸せだよ……。$R;", "キリオンマニア");
            */
        }


    }
}


