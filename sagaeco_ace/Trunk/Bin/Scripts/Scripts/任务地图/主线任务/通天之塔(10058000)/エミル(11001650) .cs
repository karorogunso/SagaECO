using System;
using System.Collections.Generic;
using System.Text;
using SagaLib;
using SagaDB.Actor;
using SagaMap.Scripting;

using SagaScript.Chinese.Enums;
namespace SagaScript.M10058000
{
    public class S11001650 : Event
    {
        public S11001650()
        {
            this.EventID = 11001650;
        }

        public override void OnEvent(ActorPC pc)
        {
            Say(pc, 131, "ドミニオンの世界がどうやら$R;" +
            "大変なことになっているらしいんだ。$R;" +
            "$R僕は彼の……$R;" +
            "ベリアルの力になってあげたい。$R;", "エミル");

        }
    }
}
        
     
    