using System;
using System.Collections.Generic;
using System.Text;
using SagaLib;
using SagaDB.Actor;
using SagaMap.Scripting;
using SagaScript.Chinese.Enums;
namespace SagaScript.M10049000
{
    public class S11000196 : Event
    {
        public S11000196()
        {
            this.EventID = 11000196;
        }

        public override void OnEvent(ActorPC pc)
        {
            Say(pc, 131, "这里就是诺森岛$R;" +
                "$R沿着这条街往北走$R就是王国的首都诺森市$R;" +
                "$P那里的魔物比大陆的厉害得多$R您得有心理准备呀$R;");
        }
    }
}
