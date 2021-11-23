using System;
using System.Collections.Generic;
using System.Text;

using SagaDB.Actor;
using SagaMap.Scripting;
//所在地圖:奧克魯尼亞南部平原(10031000) NPC基本信息:普麗緹小姐(11000170) X:169 Y:220
namespace SagaScript.M10031000
{
    public class S11000170 : Event
    {
        public S11000170()
        {
            this.EventID = 11000170;
        }

        public override void OnEvent(ActorPC pc)
        {

        }
    }
}
