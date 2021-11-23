using System;
using System.Collections.Generic;
using System.Text;
using SagaLib;
using SagaDB.Actor;
using SagaDB.Item;
using SagaMap.Scripting;
using SagaScript.Chinese.Enums;
namespace SagaScript.M10062000
{
    public class S11001056 : Event
    {
        public S11001056()
        {
            this.EventID = 11001056;
        }

        public override void OnEvent(ActorPC pc)
        {
            if (pc.Marionette != null)
            {
                Say(pc, 11001022, 131, "那么，一直在这里行吗？$R;");
                Say(pc, 131, "就是……$R;" +
                    "利迪阿在实验中失败…$R变得神经质了…$R;" +
                    "$R想从这里回避一段时间$R咇咇…$R;");
                Say(pc, 11001022, 131, "啊，原来如此。$R;" +
                    "最好避开一下吧！$R;");
                return;
            }
            Say(pc, 131, "我们是在营运这个$R;" +
                "个人工作室的工匠助手！$R;" +
                "$R这附近的工匠都用自己制作的$R;" +
                "活动木偶，作为助手哦。$R;");
        }
    }
}