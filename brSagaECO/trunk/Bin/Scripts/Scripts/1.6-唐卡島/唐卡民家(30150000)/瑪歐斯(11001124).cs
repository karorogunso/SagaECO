using System;
using System.Collections.Generic;
using System.Text;
using SagaLib;
using SagaDB.Actor;
using SagaMap.Scripting;
using SagaScript.Chinese.Enums;
namespace SagaScript.M30150000
{
    public class S11001124 : Event
    {
        public S11001124()
        {
            this.EventID = 11001124;
        }

        public override void OnEvent(ActorPC pc)
        {
            if (pc.Marionette != null)
            {
                Say(pc, 255, "這裡的阿姨菜做的不錯，$R;" +
                    "$R今天晚上吃什麼呢？$R;");
                return;
            }
            Say(pc, 255, "阿姨，飯還沒好嗎？$R;");
            Say(pc, 11001130, 131, "是，還沒有。$R;");
            Say(pc, 255, "是嗎，$R;" +
                "$R肚子有點餓…$R;" +
                "快上飯呀！$R;");
            Say(pc, 11001130, 131, "好的…$R;");
        }
    }
}