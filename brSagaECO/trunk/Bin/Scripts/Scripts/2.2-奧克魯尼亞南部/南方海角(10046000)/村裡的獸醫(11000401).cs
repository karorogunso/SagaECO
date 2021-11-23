using System;
using System.Collections.Generic;
using System.Text;

using SagaDB.Actor;
using SagaMap.Scripting;
namespace SagaScript.M10046000
{
    public class S11000401 : Event
    {
        public S11000401()
        {
            this.EventID = 11000401;
        }

        public override void OnEvent(ActorPC pc)
        {
            if (pc.Fame < 10)
            {
                Say(pc, 131, "乖，真是一個乖孩子阿$R;");
                Say(pc, 11000429, 361, "哞…哞哞！$R;" +
                    "哞哞哞~！哞！！哞哞~！$R;");
                return;
            }
            Say(pc, 131, "對，真不錯吧？$R;");
            Say(pc, 11000429, 361, "哞…哞哞！$R;" +
                "哞哞哞~！哞！！哞哞~！$R;");
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
