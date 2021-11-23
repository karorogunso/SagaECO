using System;
using System.Collections.Generic;
using System.Text;
using SagaLib;
using SagaDB.Actor;
using SagaMap.Scripting;
using SagaScript.Chinese.Enums;
namespace SagaScript.M30154000
{
    public class S11001134 : Event
    {
        public S11001134()
        {
            this.EventID = 11001134;
        }

        public override void OnEvent(ActorPC pc)
        {
            Say(pc, 131, "前輩請看看這個，$R;" +
                "這是瑪歐斯的魚苗！是魚苗呀！！$R;" +
                "$R這是世紀之大發現！！$R;");
            Say(pc, 11001135, 131, "哦？真的？$R;" +
                "我看著怎麼像蝌蚪阿。$R;");
            Say(pc, 131, "不是，好好看這裡$R;" +
                "腿出來了呀？$R;" +
                "$R這個長大了就會成為$R瑪歐斯的腿部，$R;" +
                "哇，真了不起。$R;");
        }
    }
}