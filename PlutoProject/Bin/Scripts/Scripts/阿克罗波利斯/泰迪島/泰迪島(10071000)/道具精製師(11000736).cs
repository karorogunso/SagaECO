using System;
using System.Collections.Generic;
using System.Text;

using SagaDB.Actor;
using SagaMap.Scripting;
//所在地圖:泰迪島(10071000) NPC基本信息:道具精製師(11000736) X:127 Y:218
namespace SagaScript.M10071000
{
    public class S11000736 : Event
    {
        public S11000736()
        {
            this.EventID = 11000736;
        }

        public override void OnEvent(ActorPC pc)
        {
            Say(pc, 11000736, 131, "原来有困难啊?$R;" +
                                   "叫您去借商人的飞空庭搭一下，$R;" +
                                   "干嘛搞成这样啊?$R;", "道具精制师");
        }
    }
}




