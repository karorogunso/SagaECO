using System;
using System.Collections.Generic;
using System.Text;
using SagaLib;
using SagaDB.Actor;
using SagaMap.Scripting;
using SagaScript.Chinese.Enums;
namespace SagaScript.M30163000
{
    public class S11000235 : Event
    {
        public S11000235()
        {
            this.EventID = 11000235;
        }


        public override void OnEvent(ActorPC pc)
        {
            Say(pc, 131, "噓！不要亂跑$R;" +
                "這裡是魔法行會總部大廳$R;" +
                "$R兩個要求，第一肅靜，第二也是肅靜$R;");
        }
    }
}