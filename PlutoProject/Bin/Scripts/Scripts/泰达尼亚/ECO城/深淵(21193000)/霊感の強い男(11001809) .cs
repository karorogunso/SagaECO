using System;
using System.Collections.Generic;
using System.Text;

using SagaDB.Actor;
using SagaMap.Scripting;

using SagaLib;
namespace SagaScript.M21193000
{
    public class S11001809 : Event
    {
        public S11001809()
        {
            this.EventID = 11001809;
        }

        public override void OnEvent(ActorPC pc)
        {
            Say(pc, 0, "み、見えるぞ！$R;" +
            "俺には見える！$R;" +
            "$Rこの土地には幾多の魂が渦巻いている！$R;" +
            "$Pお前も早く逃げた方がいいぞ！$R;" +
            "$Rここにいては危険だ！$R;" +
            "$P…俺？$R;" +
            "$R俺は危ないからこのテントに$R;" +
            "逃げ込んでいるわけさ。$R;", "霊感の強い男");
        }
    }
}