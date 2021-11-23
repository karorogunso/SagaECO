using System;
using System.Collections.Generic;
using System.Text;

using SagaDB.Actor;
using SagaMap.Scripting;

using SagaLib;
using SagaScript.Chinese.Enums;
//所在地圖:奧克魯尼亞東部平原(10025001) NPC基本信息:北軍騎士團長官(11000970) X:66 Y:127
namespace SagaScript.M10025001
{
    public class S11000970 : Event
    {
        public S11000970()
        {
            this.EventID = 11000970;
        }

        public override void OnEvent(ActorPC pc)
        {
            BitMask<Beginner_02> Beginner_02_mask = new BitMask<Beginner_02>(pc.CMask["Beginner_02"]);

            if (!Beginner_02_mask.Test(Beginner_02.已經與騎士團長官們進行第一次對話))
            {
                與騎士團長官們進行第一次對話(pc);
                return;
            }

            Say(pc, 11000970, 131, "对我们北军有兴趣吗?$R;", "北军骑士团长官");

            switch (Select(pc, "要问吗?", "", "诺森岛是?", "… 不用了"))
            {
                case 1:
                    Say(pc, 11000970, 131, "是位于阿克罗尼亚大陆北边，$R;" +
                                           "被冰山围绕的岛屿。$R;" +
                                           "$P魔法非常发达，$R;" +
                                           "城市里的浪漫风景一定要看啊!$R;" +
                                           "$P听说在诺森，有很多独自冒险的人。$R;" +
                                           "$P虽然跟同伴一起很不错，$R;" +
                                           "但有时也希望能独自行动…$R;" +
                                           "总有那样的时候嘛!$R;", "北军骑士团长官");
                    break;

                case 2:
                    Say(pc, 11000970, 131, "这样呀…$R;" +
                                           "$R如果想加入北军的话，$R;" +
                                           "请随时来到城市北边，$R;" +
                                           "北军骑士团的长官室吧!$R;" +
                                           "$R在城市北边左右侧，$R;" +
                                           "华丽的蓝色建筑物就是了。$R;", "北军骑士团长官");
                    break;
            }
        }

        void 與騎士團長官們進行第一次對話(ActorPC pc)
        {
            BitMask<Beginner_02> Beginner_02_mask = new BitMask<Beginner_02>(pc.CMask["Beginner_02"]);

            Beginner_02_mask.SetValue(Beginner_02.已經與騎士團長官們進行第一次對話, true);

            Say(pc, 11000970, 131, "欢迎来到阿克罗波利斯!$R;", "北军骑士团长官");

            Say(pc, 11000967, 131, "哦哦! 是从东方海角来的吗??$R;" +
                                   "$R加入我们东军的话，$R;" +
                                   "马上就可以进法伊斯特! 怎么样!?$R;", "东军骑士团长官");

            Say(pc, 11000970, 131, "喂，说是法伊斯特，$R;" +
                                   "其实是只有树和山坡的国家呀!$R;" +
                                   "$P加入我们北军的话，$R;" +
                                   "就可以自由出入诺森!$R;" +
                                   "怎么样? 我们北军…$R;", "北军骑士团长官");

            Say(pc, 11000969, 131, "诺森不是只有寒冷的旷野吗!?$R;" +
                                   "$R来加入到我们南军吧!$R;" +
                                   "艾恩萨乌斯是战斗者的天国!$R;" +
                                   "有很多强大的敌人，$R;" +
                                   "是发挥实力的好地方喔!$R;", "南军骑士团长官");

            Say(pc, 11000968, 131, "艾恩萨乌斯只是像蒸笼$R;" +
                                   "一样热而已啊!$R;" +
                                   "$R而且在地牢里发生意外的报导，$R;" +
                                   "络绎不绝呢!$R;" +
                                   "$P…怎么样?$R;" +
                                   "$R加入我们西军的话，$R;" +
                                   "可以随意进出西边的摩戈岛哦！$R;" +
                                   "在炭矿也可以挣钱呀。$R;" +
                                   "在光之塔里还可以找到$R;" +
                                   "机械文明的古迹喔!$R;", "西军骑士团长官");

            Say(pc, 11000967, 131, "摩戈岛，一句话可以形容，$R;" +
                                   "不是到处都是泥土吗!$R;", "东军骑士团长官");

            Say(pc, 11000968, 131, "唔…$R;", "西军骑士团长官");

            Say(pc, 0, 0, "虽然知道关系不好…$R;" +
                          "$R但关于军队的消息不一个一个问，$R;" +
                          "不行啊?$R;", " ");
        } 
    }
}
