using System;
using System.Collections.Generic;
using System.Text;

using SagaDB.Actor;
using SagaMap.Scripting;

using SagaLib;
namespace SagaScript.M21193000
{
    public class S11001793 : Event
    {
        public S11001793()
        {
            this.EventID = 11001793;
        }

        public override void OnEvent(ActorPC pc)
        {
            Say(pc, 0, "最近疲れてたから、凄く眠くて…$R;" +
            "$Pもう、ここで寝ちゃおうかな～$R;" +
            "どうしようかな～$R;" +
            "迷うな～$R;", "花見客");
        }
    }
}