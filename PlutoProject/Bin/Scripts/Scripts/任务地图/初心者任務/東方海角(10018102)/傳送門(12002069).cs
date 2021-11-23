using System;
using System.Collections.Generic;
using System.Text;

using SagaDB.Actor;
using SagaMap.Scripting;
//所在地圖:東方海角(10018102) NPC基本信息:傳送門(12002069)
namespace SagaScript.M10018102
{
    public class S12002069 : Event
    {
        public S12002069()
        {
            this.EventID = 12002069;
        }

        public override void OnEvent(ActorPC pc)
        {
            Say(pc, 0, 0, "现在还不能进去。$R;" +
                          "$R要有通行许可证才可以!$R;", "法伊斯特国境警备员");
        }
    }
}
