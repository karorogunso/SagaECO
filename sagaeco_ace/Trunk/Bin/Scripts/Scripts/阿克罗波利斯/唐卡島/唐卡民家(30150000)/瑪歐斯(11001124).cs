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
                Say(pc, 255, "这里的阿姨菜做的不错，$R;" +
                    "$R今天晚上吃什么呢？$R;");
                return;
            }
            Say(pc, 255, "阿姨，饭还没好吗？$R;");
            Say(pc, 11001130, 131, "是，还没有。$R;");
            Say(pc, 255, "是吗，$R;" +
                "$R肚子有点饿…$R;" +
                "快上饭呀！$R;");
            Say(pc, 11001130, 131, "好的…$R;");
        }
    }
}