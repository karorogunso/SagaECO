using System;
using System.Collections.Generic;
using System.Text;

using SagaDB.Actor;
using SagaMap.Scripting;

using SagaLib;
using SagaScript.Chinese.Enums;
//所在地圖:北部平原初心者學校(30091004) NPC基本信息:初階冒險者(11000997) X:8 Y:9
namespace SagaScript.M30091004
{
    public class S11000997 : Event
    {
        public S11000997()
        {
            this.EventID = 11000997;
        }

        public override void OnEvent(ActorPC pc)
        {
            BitMask<Beginner_02> Beginner_02_mask = new BitMask<Beginner_02>(pc.CMask["Beginner_02"]);

            if (!Beginner_02_mask.Test(Beginner_02.已經與男初階冒險者進行第一次對話))
            {
                初次與初階冒險者進行對話(pc);
                return;
            }

            Say(pc, 11000997, 0, "啊，您好!$R;", "初阶冒险者");
        }

        void 初次與初階冒險者進行對話(ActorPC pc)
        {
            BitMask<Beginner_02> Beginner_02_mask = new BitMask<Beginner_02>(pc.CMask["Beginner_02"]);

            Beginner_02_mask.SetValue(Beginner_02.已經與男初階冒險者進行第一次對話, true);

            Say(pc, 11000997, 0, "啊，您好!$R;" +
                                 "$R您也是第一次来「阿克罗波利斯」吗?$R;" +
                                 "$P我也是刚到没多久，$R;" +
                                 "所以在听老师说明呢!$R;" +
                                 "$R就像老师说的，$R;" +
                                 "我们这些来到「阿克罗波利斯」$R;" +
                                 "没多久的新手，$R;" +
                                 "最好向冒险者前辈们，$R;" +
                                 "请假一下冒险的方法。$R;" +
                                 "$P您如果有不知道的问题，$R;" +
                                 "就问问老师吧?$R;", "初阶冒险者");

            switch (Select(pc, "要继续聊天吗?", "", "您的衣服很好看啊!", "…好了"))
            {
                case 1:
                    Say(pc, 11000997, 0, "啊，是吗?$R;" +
                                         "谢谢称赞呀!$R;" +
                                         "$P这是在「泰迪岛」买的。$R;" +
                                         "$R嗯? …「泰迪岛」是什么?$R;" +
                                         "$P事实上…虽然无法相信$R;" +
                                         "但是在「上城」那$R;" +
                                         "有个泰迪娃娃跟我搭话。$R;" +
                                         "$R回答后，就变得精神恍惚，$R;" +
                                         "一醒来就发现自己在南边的岛上。$R;" +
                                         "$P是真的! 不是做梦啊!$R;" +
                                         "$R这衣服就是在那岛上的商人那买的!$R;" +
                                         "$P那时候虽然没什么钱。$R;" +
                                         "$R但我按照那商人说的做，$R;" +
                                         "把岛上遍布的岩石和草丛里，$R;" +
                                         "获得的道具卖给商人，$R;" +
                                         "赚到了一些钱。$R;" +
                                         "$P我就拿着那些钱回到城市里，$R;" +
                                         "就可以买到各种物品了。$R;", "初阶冒险者");
                    break;

                case 2:
                    break;
            }
        }
    }
}
