using System;
using System.Collections.Generic;
using System.Text;

using SagaDB.Actor;
using SagaMap.Scripting;

using SagaLib;
using SagaScript.Chinese.Enums;
namespace SagaScript.M30130002
{
    public class S11001368 : Event
    {
        public S11001368()
        {
            this.EventID = 11001368;
        }

        public override void OnEvent(ActorPC pc)
        {
            Say(pc, 131, "２人ともようやく、試験を$R;" +
            "受ける気になったのか？$R;", "フラム");

            Say(pc, 11001369, 131, "まぁね、そろそろちゃんと$R;" +
            "しないといけないと思ったし。$R;", "イオン");

            Say(pc, 11001370, 133, "憧れのフラム姐さんが試験官だなんて$R;" +
            "がぜんやる気出てきたぁ！$R;", "アーク");
        }
    }
}