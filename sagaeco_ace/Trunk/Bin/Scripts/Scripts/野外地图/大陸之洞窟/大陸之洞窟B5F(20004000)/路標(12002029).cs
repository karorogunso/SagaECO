using System;
using System.Collections.Generic;
using System.Text;
using SagaLib;
using SagaDB.Actor;
using SagaMap.Scripting;
using SagaScript.Chinese.Enums;
namespace SagaScript.M20004000
{
    public class S12002029 : Event
    {
        public S12002029()
        {
            this.EventID = 12002029;
        }

        public override void OnEvent(ActorPC pc)
        {
            Say(pc, 0, 131, "呵呵呵$R;" +
                "$R这里面是鱼的世界！$R;" +
                "是秘密…秘密…$R;");
            switch (Select(pc, "后面写了些什么…", "", "看看吧！", "不看！"))
            {
                case 1:
                    Say(pc, 0, 131, "组队的时候！！！$R;" +
                        "吃这道菜就最适合不过$R;" +
                        "$P把各种喜欢的材料$R;" +
                        "放在锅里煮$R;" +
                        "$P“肉包子”“煮鸡蛋”$R;" +
                        "“苹果干”“奇怪的磨菇”等等$R;" +
                        "$P成功的话会很好吃的$R;" +
                        "失败的话……$R;");
                    break;
                case 2:
                    break;
            }
        }
    }
}