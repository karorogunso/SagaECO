using System;
using System.Collections.Generic;
using System.Text;

using SagaDB.Actor;
using SagaDB.Item;
using SagaMap.Scripting;

using SagaLib;
using SagaScript.Chinese.Enums;
namespace SagaScript.M30102002
{
    public class S11001223 : Event
    {
        public S11001223()
        {
            this.EventID = 11001223;
        }

        public override void OnEvent(ActorPC pc)
        {
            Say(pc, 11001223, 131, "啊啊！$R;" +
                pc.Name + "?$R;" +
                "$R好久不見！！你過的好嗎??$R;" +
                "$R找瑪莎?$R;" +
                "$R啊…瑪莎跟長官正在談話可能會很久$R好像要等一段時間才可以…$R;");
        }
    }
}