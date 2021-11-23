using System;
using System.Collections.Generic;
using System.Text;
using SagaLib;
using SagaDB.Actor;
using SagaMap.Scripting;

using SagaScript.Chinese.Enums;
namespace SagaScript.M30020001
{
    public class S11000333 : Event
    {
        public S11000333()
        {
            this.EventID = 11000333;
        }

        public override void OnEvent(ActorPC pc)
        {
            Say(pc, 11000434, 131, "它的名字是圍棋$R;" +
                "很兇的，所以小心哦!$R;");
            Say(pc, 111, "嗚嚅嚅嚅嚅!!!$R;");
        }
    }
}