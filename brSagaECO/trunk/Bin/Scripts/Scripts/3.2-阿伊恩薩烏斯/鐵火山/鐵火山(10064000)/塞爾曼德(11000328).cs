using System;
using System.Collections.Generic;
using System.Text;
using SagaLib;
using SagaDB.Actor;
using SagaMap.Scripting;
using SagaScript.Chinese.Enums;
namespace SagaScript.M10064000
{
    public class S11000328 : Event
    {
        public S11000328()
        {
            this.EventID = 11000328;
        }

        public override void OnEvent(ActorPC pc)
        {
            BitMask<Job2X_09> Job2X_09_mask = pc.CMask["Job2X_09"];
            
            if (Job2X_09_mask.Test(Job2X_09.獲得塞爾曼德的心臟))//_3a43)
            {
                Say(pc, 131, "那個孩子消失了$R;" +
                    "$R現在開始進入到您的內心，$R;" +
                    "守護著您了。$R;");
                return;
            }
            
            Say(pc, 131, "…$R;");
        }
    }
}
