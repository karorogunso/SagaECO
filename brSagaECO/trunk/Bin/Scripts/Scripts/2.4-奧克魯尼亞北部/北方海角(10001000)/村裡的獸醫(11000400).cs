using System;
using System.Collections.Generic;
using System.Text;
using SagaLib;
using SagaDB.Actor;
using SagaMap.Scripting;
using SagaScript.Chinese.Enums;
namespace SagaScript.M10001000
{
    public class S11000400 : Event
    {
        public S11000400()
        {
            this.EventID = 11000400;
        }

        public override void OnEvent(ActorPC pc)
        {
            if (pc.Fame < 10)
            {
                Say(pc, 131, "對，真不錯吧？$R;");
                Say(pc, 11000430, 361, "哞～！$R;" +
                    "哞！哞哞～！$R;", "雪姆貝爾");
                return;
            }
            Say(pc, 131, "對，真不錯吧$R;");
            Say(pc, 11000430, 361, "咕咕~！$R;" +
                "咕咕咕！咕咕~！$R;");
            Say(pc, 131, "啊，您也想治療寵物嗎？$R;");
            switch (Select(pc, "想怎麼做呢？", "", "治療寵物", "什麼也不做"))
            {
                case 1:
                    PetRecover(pc, 1);
                    break;
                case 2:
                    break;
            }
        }
    }
}
