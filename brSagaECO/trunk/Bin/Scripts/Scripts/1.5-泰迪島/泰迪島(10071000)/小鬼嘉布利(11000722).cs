using System;
using System.Collections.Generic;
using System.Text;

using SagaDB.Actor;
using SagaMap.Scripting;
//所在地圖:泰迪島(10071000) NPC基本信息:小鬼嘉布利(11000722) X:187 Y:163
namespace SagaScript.M10071000
{
    public class S11000722 : Event
    {
        public S11000722()
        {
            this.EventID = 11000722;
        }

        public override void OnEvent(ActorPC pc)
        {
            Say(pc, 11000722, 131, "小狗好像很開心啊!$R;", "小鬼嘉布利");

            ShowEffect(pc, 10009, 4516);
            ShowEffect(pc, 10011, 4516);
        }
    }
}