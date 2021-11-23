using System;
using System.Collections.Generic;
using System.Text;

using SagaDB.Actor;
using SagaMap.Scripting;
//所在地图:沃特雷亚(11053000)NPC基本信息:11001513-古龟伍- X:72 Y:211
namespace SagaScript.M11053000
{
    public class S11001513 : Event
    {
    public S11001513()
        {
            this.EventID = 11001513;
        }


        public override void OnEvent(ActorPC pc)
        {
            Say(pc, 131, "压拉那一卡?$R;");
            switch(Select(pc,"该怎么做呢","","阿姨洗铁路","阿姨洗公路","阿姨洗土路","你在说什么?"))
            {
                case 1:
                    {
                        Say(pc, 0, "呼哧!呼哧! 呼呼!!!$R嘶!!!!!!!!!!!!!!!!$R;");
                        return;
                    }
                case 2:
                    {
                        Say(pc, 0, "呼嘶呼嘶$R;");
                        return;
                    }
                case 3:
                    {
                        Say(pc, 0, "呼哧!$R;");
                        return;
                    }
                case 4:
                    {
                        Say(pc, 0, "呼...............");
                        return;
                    }
            }
        }
    }
}
