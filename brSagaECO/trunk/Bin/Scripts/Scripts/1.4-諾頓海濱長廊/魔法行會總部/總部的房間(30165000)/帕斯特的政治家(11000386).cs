using System;
using System.Collections.Generic;
using System.Text;
using SagaLib;
using SagaDB.Actor;
using SagaMap.Scripting;
using SagaScript.Chinese.Enums;
namespace SagaScript.M30165000
{
    public class S11000386 : Event
    {
        public S11000386()
        {
            this.EventID = 11000386;
        }

        public override void OnEvent(ActorPC pc)
        {
            Say(pc, 131, "米粒阿裡阿先生$R;" +
                "聽我說好嗎$R;");
            Say(pc, 11000387, 131, "看，我先來的$R;" +
                "$R先聽我的才對$R;");
            Say(pc, 11000386, 131, "什麼？$R;");
            Say(pc, 11000385, 131, "別再打了！$R;");
        }
    }
}