using System;
using System.Collections.Generic;
using System.Text;

using SagaDB.Actor;
using SagaMap.Scripting;

using SagaLib;
namespace SagaScript.M31302000
{
    public class S11001880 : Event
    {
        public S11001880()
        {
            this.EventID = 11001880;
        }

        public override void OnEvent(ActorPC pc)
        {
            Say(pc, 0, "アール！私たちはここで$R;" +
            "タイタニアンドリームを$R;" +
            "手に入れるのよッ！$R;", "意気込むエリュ");

            Say(pc, 11001881, 0, "いいから早く行こうぜっ！$R;", "意気込むアール");

            Say(pc, 0, "ちょ、ちょっと待って！$R;" +
            "ま、まだ心の準備が……。$R;", "意気込むエリュ");
}
}

        
    }


