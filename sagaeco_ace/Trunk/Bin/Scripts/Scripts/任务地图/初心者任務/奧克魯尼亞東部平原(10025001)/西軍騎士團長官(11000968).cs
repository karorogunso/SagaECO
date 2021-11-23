using System;
using System.Collections.Generic;
using System.Text;

using SagaDB.Actor;
using SagaMap.Scripting;

using SagaLib;
using SagaScript.Chinese.Enums;
//所在地圖:奧克魯尼亞東部平原(10025001) NPC基本信息:西軍騎士團長官(11000968) X:66 Y:129
namespace SagaScript.M10025001
{
    public class S11000968 : Event
    {
        public S11000968()
        {
            this.EventID = 11000968;
        }

        public override void OnEvent(ActorPC pc)
        {
            BitMask<Beginner_02> Beginner_02_mask = new BitMask<Beginner_02>(pc.CMask["Beginner_02"]);

            if (!Beginner_02_mask.Test(Beginner_02.已經與騎士團長官們進行第一次對話))
            {
                與騎士團長官們進行第一次對話(pc);
                return;
            }

            Say(pc, 11000968, 131, "对我们西军有兴趣吗?$R;", "西军骑士团长官");

            switch (Select(pc, "要问吗?", "", "摩戈岛是?", "… 不用了"))
            {
                case 1:
                    Say(pc, 11000968, 131, "是在阿克罗尼亚大陆西南边，$R;" +
                                           "盛产摩戈炭的山地地带呀!$R;" +
                                           "$R定期运行的飞空庭，$R;" +
                                           "可以在阿克罗尼亚大陆西边的$R;" +
                                           "「军舰岛」上乘坐，$R;" +
                                           "搭乘飞空庭就可以越过大海了哦!$R;" +
                                           "$P在摩戈岛里乘坐飞空庭，$R;" +
                                           "再往西南边走的话，$R;" +
                                           "可以去到叫「光之塔」的$R;" +
                                           "机械时代遗迹呀!$R;" +
                                           "$R听说冒险者现在都在那里探索呢!$R;", "西军骑士团长官");
                    break;

                case 2:
                    Say(pc, 11000968, 131, "这样呀…$R;" +
                                           "$R如果想加入西军的话，$R;" +
                                           "请随时来到城市西边，$R;" +
                                           "西军骑士团的长官室吧!$R;" +
                                           "$R在城市西边左右侧，$R;" +
                                           "华丽的黄色建筑物就是了。$R;", "西军骑士团长官");
                    break;
            }
        }

        void 與騎士團長官們進行第一次對話(ActorPC pc)
        {
            BitMask<Beginner_02> Beginner_02_mask = new BitMask<Beginner_02>(pc.CMask["Beginner_02"]);

            Beginner_02_mask.SetValue(Beginner_02.已經與騎士團長官們進行第一次對話, true);

            Say(pc, 11000970, 131, "欢迎来到阿克罗波利斯!$R;", "西军骑士团长官");

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
