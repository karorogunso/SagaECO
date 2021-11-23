using System;
using System.Collections.Generic;
using System.Text;
using SagaLib;
using SagaDB.Actor;
using SagaMap.Scripting;

using SagaScript.Chinese.Enums;
namespace SagaScript.M12021000
{
    public class S12002093 : Event
    {
        public S12002093()
        {
            this.EventID = 12002093;
        }

        public override void OnEvent(ActorPC pc)
        {
            PlaySound(pc, 2559, false, 100, 50);

            Say(pc, 0, 131, "真期待裏面會出來什麽東西！$R;" +
            "心跳道具箱！$R;", " ");
            Select(pc, "要放入哪個徽章？", "", "金徽章", "沒興趣");
        }

    }

}
            
            
        
     
    