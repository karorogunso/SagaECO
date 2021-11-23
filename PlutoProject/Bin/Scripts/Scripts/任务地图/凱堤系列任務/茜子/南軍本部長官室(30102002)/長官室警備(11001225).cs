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
            Say(pc, 11001225, 131, "啊啊…来吧地下犯$R给例实…馆长…$R;" +
                "$P听不懂是些什么乱七八糟的吧？$R;" +
                "$P最近才…解读成功的语言$R;" +
                "$P虽然被称为机械类语言…$R;" +
                "$R为了判断出敌人的语言$R骑士团团员不可以不学$R;" +
                "$R可我就是不喜欢语言学…唉…$R;");
        }
    }
}