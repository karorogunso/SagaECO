using System;
using System.Collections.Generic;
using System.Text;
using SagaLib;
using SagaDB.Actor;
using SagaDB.Item;
using SagaMap.Scripting;
using SagaScript.Chinese.Enums;
namespace SagaScript.M10062000
{
    public class S11001137 : Event
    {
        public S11001137()
        {
            this.EventID = 11001137;
        }

        public override void OnEvent(ActorPC pc)
        {
            Say(pc, 131, "这裡是活动木偶的总部。$R;" +
                "$R刚才有一个人鬼鬼祟祟地进去了，$R;" +
                "$R是不是小偷？$R;");
            /*
            if (!_7a71)
            {
                Say(pc, 131, "這裡是活動木偶的總部。$R;" +
                    "$R剛才有一個人鬼鬼祟祟地進去了，$R;" +
                    "$R是不是小偷？$R;");
                return;
            }
            Say(pc, 131, "這裡是活動木偶的總部。$R;");
            */
        }
    }
}