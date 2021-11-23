using System;
using System.Collections.Generic;
using System.Text;

using SagaDB.Actor;
using SagaMap.Scripting;

using SagaLib;
namespace SagaScript.M11053000
{
    public class S11001516 : Event
    {
        public S11001516()
        {
            this.EventID = 11001516;
        }

        public override void OnEvent(ActorPC pc)
        {

            Say(pc, 0, "…咩咩咩.$R;", "カメ");
            if (pc.Gender > 0)
            {

                Say(pc, 0, "這個這個$R;" +
                "可愛的小姐$R;" +
                "$R如果不介意的話$R;" +
                "在那裡的龜背上坐下$R;" +
                "一起聊天如何呢？$R;", "カメ");
            }
            else
            {
                Say(pc, 0, "男的一边去.$R;", "カメ");
            }
        }
    }

}


