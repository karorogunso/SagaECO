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
    public class S11001225 : Event
    {
        public S11001225()
        {
            this.EventID = 11001225;
        }

        public override void OnEvent(ActorPC pc)
        {
            Say(pc, 11001225, 131, "啊啊…來吧地下犯$R給例實…館長…$R;" +
                "$P最近才…解讀成功的語言$R;" +
                "$P雖然被稱為機械類語言…$R;" +
                "$R為了判斷出敵人的語言$R騎士團團員不可以不學$R;" +
                "$R可我就是不喜歡語言學…唉…$R;");
        }
    }
}