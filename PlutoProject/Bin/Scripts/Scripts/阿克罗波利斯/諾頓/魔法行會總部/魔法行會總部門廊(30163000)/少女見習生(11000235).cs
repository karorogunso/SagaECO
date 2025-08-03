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
            Say(pc, 131, "嘘！不要乱跑$R;" +
                "这里是魔法行会总部大厅$R;" +
                "$R两个要求，第一肃静，第二也是肃静$R;");
        }
    }
}