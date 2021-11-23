using System;
using System.Collections.Generic;
using System.Text;

using SagaDB.Actor;
using SagaMap.Scripting;

using SagaLib;
namespace SagaScript.M21193000
{
    public class S11001815 : Event
    {
        public S11001815()
        {
            this.EventID = 11001815;
        }

        public override void OnEvent(ActorPC pc)
        {

            Say(pc, 0, "ふー…。$R;" +
            "$Rいやー、美味かった！$R;" +
            "$P今は食後の楽しみの一つ、$R;" +
            "コーヒーを味わっているところさ。$R;" +
            "$Pえ？$R;" +
            "ところでここはどこだって？$R;" +
            "$Pそんなの決まってるじゃないか。$R;" +
            "$Rファミレスさ。$R;", "食後の男");
        }
    }
}