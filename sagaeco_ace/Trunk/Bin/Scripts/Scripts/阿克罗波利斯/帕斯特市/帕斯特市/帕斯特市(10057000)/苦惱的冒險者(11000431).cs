using System;
using System.Collections.Generic;
using System.Text;
using SagaLib;
using SagaDB.Actor;
using SagaMap.Scripting;
using SagaScript.Chinese.Enums;
namespace SagaScript.M10057000
{
    public class S11000431 : Event
    {
        public S11000431()
        {
            this.EventID = 11000431;
        }

        public override void OnEvent(ActorPC pc)
        {
            Say(pc, 190, "想进入这个森林，可是魔物太强了…$R;" +
                "$P那是因为您太弱了！$R;");
            Say(pc, 11000432, 190, "那你就很强吗？$R;");
        }
    }
}