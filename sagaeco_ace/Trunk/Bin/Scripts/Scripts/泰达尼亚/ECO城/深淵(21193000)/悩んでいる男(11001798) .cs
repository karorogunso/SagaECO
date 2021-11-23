using System;
using System.Collections.Generic;
using System.Text;

using SagaDB.Actor;
using SagaMap.Scripting;

using SagaLib;
namespace SagaScript.M21193000
{
    public class S11001798 : Event
    {
        public S11001798()
        {
            this.EventID = 11001798;
        }

        public override void OnEvent(ActorPC pc)
        {
            Say(pc, 0, "コーヒー牛乳にするか、$R;" +
            "$Rそれともフルーツ牛乳にするか…。$R;" +
            "$Rはたまた、シンプルに牛乳もありか…。$R;" +
            "$P意表を付いて、キャラメルカプチーノって$R;" +
            "選択も捨てがたい。$R;" +
            "$Pでもせっかく海に来てるわけだから、$R;" +
            "風呂上りみたいな選択をしなくても$R;" +
            "良い気もするな…。$R;", "悩んでいる男");
        }
    }
}