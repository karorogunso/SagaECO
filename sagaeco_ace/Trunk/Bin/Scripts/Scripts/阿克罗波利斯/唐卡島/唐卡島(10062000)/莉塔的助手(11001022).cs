using System;
using System.Collections.Generic;
using System.Text;
using SagaLib;
using SagaDB.Actor;
using SagaMap.Scripting;
using SagaScript.Chinese.Enums;
namespace SagaScript.M10062000
{
    public class S11001022 : Event
    {
        public S11001022()
        {
            this.EventID = 11001022;
        }

        public override void OnEvent(ActorPC pc)
        {
            if (pc.Marionette != null)
            {
                Say(pc, 131, "我们老师太马虎了$R;");
                Say(pc, 11001056, 131, "莉塔老师也是吗？$R;" +
                    "$R利迪阿的房间很脏$R;" +
                    "$R真不知该怎么办。$R;");
                Say(pc, 131, "活动木偶研究固然重要$R;" +
                    "$R但看完书就扔掉，$R;" +
                    "文件掉了也不捡，$R;" +
                    "这样，再漂亮也嫁不出去的呀$R;");
                Say(pc, 11001056, 131, "对阿对阿$R;" +
                    "真是让人操心呀$R;");
                return;
            }
            Say(pc, 131, "我们在这个私人工作室，$R;" +
                "做老师的助手$R;" +
                "$R我们的老师叫莉塔，$R;" +
                "虽然很年轻，但实力超强的哦$R;" +
                "活动木偶工匠$R;" +
                "$P特别是制作矿石精灵的$R;" +
                "杰出人物。$R;" +
                "$R您一定要见识一下呀！$R;");
        }
    }
}