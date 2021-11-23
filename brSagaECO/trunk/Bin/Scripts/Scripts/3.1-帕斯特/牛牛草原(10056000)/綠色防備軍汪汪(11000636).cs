using System;
using System.Collections.Generic;
using System.Text;
using SagaLib;
using SagaDB.Actor;
using SagaMap.Scripting;
using SagaScript.Chinese.Enums;
namespace SagaScript.M10056000
{
    public class S11000636 : Event
    {
        public S11000636()
        {
            this.EventID = 11000636;
        }

        public override void OnEvent(ActorPC pc)
        {
            Say(pc, 500, "吭!吭吭吭$R;");
            Say(pc, 11000635, 131, "這傢伙是守衛帕斯特$R;" +
                "綠色防備軍的狗部隊隷屬小狗$R;" +
                "$R可愛吧？$R;");
        }
    }
}