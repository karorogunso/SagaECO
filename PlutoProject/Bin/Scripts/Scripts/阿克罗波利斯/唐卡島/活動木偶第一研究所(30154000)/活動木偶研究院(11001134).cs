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
            Say(pc, 131, "前辈请看看这个，$R;" +
                "这是鱼人的鱼苗！是鱼苗呀！！$R;" +
                "$R这是世纪之大发现！！$R;");
            Say(pc, 11001135, 131, "哦？真的？$R;" +
                "我看着怎么像蝌蚪阿。$R;");
            Say(pc, 131, "不是，好好看这里$R;" +
                "腿出来了呀？$R;" +
                "$R这个长大了就会成为$R鱼人的腿部，$R;" +
                "哇，真了不起。$R;");
        }
    }
}