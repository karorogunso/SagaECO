using System;
using System.Collections.Generic;
using System.Text;

using SagaDB.Actor;
using SagaMap.Scripting;
//所在地圖:奧克魯尼亞南部平原(10031000) NPC基本信息:維多利娜(11000137) X:119 Y:41
namespace SagaScript.M10031000
{
    public class S11000137 : Event
    {
        public S11000137()
        {
            this.EventID = 11000137;
        }

        public override void OnEvent(ActorPC pc)
        {

        }
    }
}
