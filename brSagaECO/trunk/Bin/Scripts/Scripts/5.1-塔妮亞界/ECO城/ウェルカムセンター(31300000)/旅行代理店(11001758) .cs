using System;
using System.Collections.Generic;
using System.Text;

using SagaDB.Actor;
using SagaMap.Scripting;

using SagaLib;
namespace SagaScript.M31300000
{
    public class S11001758 : Event
    {
        public S11001758()
        {
            this.EventID = 11001758;
        }

        public override void OnEvent(ActorPC pc)
        {
            Say(pc, 131, "現地発信の旬な情報と$R;" +
            "ツアー、ホテル、スパなどの$R;" +
            "予約手配がボクの仕事さ！$R;", "旅行代理店");
        }


    }
}


