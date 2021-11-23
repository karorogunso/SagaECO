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
            Say(pc, 0, "看、看見了！$R;" +
            "被老子看見了！$R;" +
            "$R這塊土地上 この土地には幾多の魂が渦巻いている！$R;" +
            "$P你也最好早早逃離這裡！$R;" +
            "$R留在這裡很危險！$R;" +
            "$P…老子？$R;" +
            "$R老子也危險因此逃入$R;" +
            "這個帳篷裡避難。$R;", "霊感の強い男");
            //
            /*
            Say(pc, 0, "み、見えるぞ！$R;" +
            "俺には見える！$R;" +
            "$Rこの土地には幾多の魂が渦巻いている！$R;" +
            "$Pお前も早く逃げた方がいいぞ！$R;" +
            "$Rここにいては危険だ！$R;" +
            "$P…俺？$R;" +
            "$R俺は危ないからこのテントに$R;" +
            "逃げ込んでいるわけさ。$R;", "霊感の強い男");
            */
        }
    }
}
 