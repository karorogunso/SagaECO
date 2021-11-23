using System;
using System.Collections.Generic;
using System.Text;

using SagaDB.Actor;
using SagaMap.Scripting;

using SagaLib;
using SagaScript.Chinese.Enums;
namespace SagaScript.M30130002
{
    public class S11001370 : Event
    {
        public S11001370()
        {
            this.EventID = 11001370;
        }

        public override void OnEvent(ActorPC pc)
        {
            Say(pc, 131, "かぁー早く俺もその制服着たいぜ！$R;", "アーク");

            Say(pc, 11001369, 131, "試験に受かれば支給$R;" +
            "されるんじゃないか？$R;" +
            "$R受かれば、の話だがな。$R;", "イオン");

            Say(pc, 131, "失敬な奴だな！$R;", "アーク");
        }
    }
}