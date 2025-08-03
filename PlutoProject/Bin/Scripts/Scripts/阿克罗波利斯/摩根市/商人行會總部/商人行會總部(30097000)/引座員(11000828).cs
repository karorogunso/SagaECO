using System;
using System.Collections.Generic;
using System.Text;
using SagaLib;
using SagaDB.Item;
using SagaDB.Actor;
using SagaMap.Scripting;
using SagaScript.Chinese.Enums;
namespace SagaScript.M30097000
{
    public class S11000828 : Event
    {
        public S11000828()
        {
            this.EventID = 11000828;

        }

        public override void OnEvent(ActorPC pc)
        {
            BitMask<MorgFlags> mask = pc.CMask["MorgFlags"];

            Say(pc, 159, "欢迎光临~$R;");
            if (mask.Test(MorgFlags.接受基本職業) ||
                mask.Test(MorgFlags.接受專門職業) ||
                mask.Test(MorgFlags.接受技術職業) ||
                mask.Test(MorgFlags.給予護髮劑))
            {
                Say(pc, 131, "$R哥哥觉得改变一下发型$R;" +
                    "就可以赢我吗？$R;");
                return;
            }
        }
    }
}