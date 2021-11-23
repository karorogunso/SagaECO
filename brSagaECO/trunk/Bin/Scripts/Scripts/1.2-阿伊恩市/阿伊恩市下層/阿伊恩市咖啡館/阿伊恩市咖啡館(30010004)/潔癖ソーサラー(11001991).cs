using System;
using System.Collections.Generic;
using System.Text;

using SagaDB.Actor;
using SagaMap.Scripting;
//所在地圖:阿伊恩市咖啡館(30010004) NPC基本信息:潔癖ソーサラー(11001991) X:6 Y:1
namespace SagaScript.M30010004
{
    public class S11001991 : Event
    {
        public S11001991()
        {
            this.EventID = 11001991;
        }

        public override void OnEvent(ActorPC pc)
        {

        }
    }
}