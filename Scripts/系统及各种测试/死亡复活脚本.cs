using System;
using System.Collections.Generic;
using System.Text;

using SagaDB.Actor;
using SagaMap.Scripting;

namespace SagaScript
{
    public class 死亡复活 : Event
    {
        public 死亡复活()
        {
            this.EventID = 0xF1000000;
        }

        public override void OnEvent(ActorPC pc)
        {
            switch (Select(pc, "玩家 "+ pc.TStr["复活者"] +" 正在使你复活", "", "复活", "不复活"))
            {
                case 1:
                    Revive(pc, (byte)pc.TInt["Revive"]);
                    break;
                    
                case 2:
                    break;
            }
        }
    }
}
