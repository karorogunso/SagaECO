using System;
using System.Collections.Generic;
using System.Text;
using SagaLib;
using SagaDB.Actor;
using SagaMap.Scripting;

using SagaScript.Chinese.Enums;
namespace SagaScript.M11001783
{
    public class S11001783 : Event
    {
        public S11001783()
        {
            this.EventID = 11001783;
        }

        public override void OnEvent(ActorPC pc)
        {
            if (Select(pc, "要去军舰岛？", "", "不去", "去") == 2)
            {
                Say(pc, 11001669, 65535, "那么、要去了。$R;" +
                "$坐上来$R;" +
                "要牢牢的抓住哟！$R;", "反抗军傭兵");
                Warp(pc, 12035000, 47, 159);
            }
        }
    }
}



