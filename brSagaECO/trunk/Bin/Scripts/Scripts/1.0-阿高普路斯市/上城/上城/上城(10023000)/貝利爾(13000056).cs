using System;
using System.Collections.Generic;
using System.Text;

using SagaDB.Actor;
using SagaMap.Scripting;
//所在地圖:上城(10023000) NPC基本信息:貝利爾(13000056) X:128 Y:117
namespace SagaScript.M10023000
{
    public class S13000056 : Event
    {
        public S13000056()
        {
            this.EventID = 13000056;
        }

        public override void OnEvent(ActorPC pc)
        {

        }
    }
}
