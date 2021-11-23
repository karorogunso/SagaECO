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
            Say(pc, 131, "這裡就是諾頓島唷$R;" +
                "$R沿著這條街往北走$R就是王國的首都諾頓市$R;" +
                "$P那裡的魔物比大陸的厲害得多$R您得有心理準備呀$R;");
        }
    }
}
