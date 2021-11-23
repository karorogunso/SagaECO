using System;
using System.Collections.Generic;
using System.Text;
using SagaLib;
using SagaDB.Actor;
using SagaMap.Scripting;
using SagaScript.Chinese.Enums;
namespace SagaScript.M30165000
{
    public class S11000387 : Event
    {
        public S11000387()
        {
            this.EventID = 11000387;
        }

        public override void OnEvent(ActorPC pc)
        {
            Say(pc, 131, "米粒阿裡阿先生能到我們國家來一趟$R;" +
                "在那裡慢慢聊吧$R;");
            Say(pc, 11000386, 131, "什麼？$R;" +
                "米粒阿裡阿先生$R;" +
                "得先到我們國家來阿$R;");
            Say(pc, 11000387, 131, "什麼？$R;");
            Say(pc, 11000385, 131, "兩位請安靜點吧！$R;");
        }
    }
}