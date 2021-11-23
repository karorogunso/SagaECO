using System;
using System.Collections.Generic;
using System.Text;
using SagaLib;
using SagaDB.Actor;
using SagaMap.Scripting;
using SagaScript.Chinese.Enums;
namespace SagaScript.M30091000
{
    public class S11000699 : Event
    {
        public S11000699()
        {
            this.EventID = 11000699;
        }

        public override void OnEvent(ActorPC pc)
        {
            Say(pc, 160, "不可能的，我没有才能！$R;" +
                "也不像賢皮克前輩一樣聰明!$R;");
            Say(pc, 11000698, 131, "你這傢伙！在説什麽呀！$R;" +
                "你不是想回去諾頓總部嗎?$R;");
            Say(pc, 160, "但是！！真的一個都不知道啊！$R;");
        }
    }
}