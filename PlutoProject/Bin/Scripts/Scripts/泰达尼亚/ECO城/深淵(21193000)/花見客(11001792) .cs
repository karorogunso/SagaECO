using System;
using System.Collections.Generic;
using System.Text;

using SagaDB.Actor;
using SagaMap.Scripting;

using SagaLib;
namespace SagaScript.M21193000
{
    public class S11001792 : Event
    {
        public S11001792()
        {
            this.EventID = 11001792;
        }

        public override void OnEvent(ActorPC pc)
        {
            Say(pc, 0, "花より団子って言葉はあっても、$R;" +
            "団子より花ってのは無いのよね。$R;" +
            "不思議！$R;", "花見客");
        }
    }
}