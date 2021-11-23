using System;
using System.Collections.Generic;
using System.Text;

using SagaDB.Actor;
using SagaMap.Scripting;

using SagaLib;
using SagaScript.Chinese.Enums;
//所在地圖:奧克魯尼亞東部平原(10025001) NPC基本信息:提多(11000973) X:14 Y:125
namespace SagaScript.M10025001
{
    public class S11000973 : Event
    {
        public S11000973()
        {
            this.EventID = 11000973;
        }

        public override void OnEvent(ActorPC pc)
        {
            BitMask<Beginner_01> Beginner_01_mask = new BitMask<Beginner_01>(pc.CMask["Beginner_01"]);

            if (!Beginner_01_mask.Test(Beginner_01.埃米爾給予埃米爾介紹書) &&
                !Beginner_01_mask.Test(Beginner_01.貝利爾給予初心者緞帶))
            {
                提多給予初心者緞帶和埃米爾介紹信(pc);
                return;
            }

            if (!Beginner_01_mask.Test(Beginner_01.埃米爾給予埃米爾介紹書))
            {
                提多給予埃米爾介紹信(pc);
                return;
            }

            Say(pc, 11000973, 131, "从那边的传送点过去，$R;" +
                                   "就可以到达城市里了。$R;" +
                                   "$R祝您战斗顺利!!$R;", "泰塔斯");
        }

        void 提多給予初心者緞帶和埃米爾介紹信(ActorPC pc)
        {
            BitMask<Beginner_01> Beginner_01_mask = new BitMask<Beginner_01>(pc.CMask["Beginner_01"]);

            Beginner_01_mask.SetValue(Beginner_01.埃米爾給予埃米爾介紹書, true);
            Beginner_01_mask.SetValue(Beginner_01.貝利爾給予初心者緞帶, true);

            Say(pc, 11000973, 131, "您好!$R;" +
                                   "$R您基本的概念，$R;" +
                                   "都知道了吧?", "泰塔斯");

            PlaySound(pc, 2040, false, 100, 50);
            GiveItem(pc, 50053600, 1);
            GiveItem(pc, 10043081, 1);
            Say(pc, 0, 0, "得到『初心者缎带』$R;" +
                          "和『埃米尔介绍信』!$R;", " ");

            Say(pc, 11000973, 131, "这是初心者使用的装备。$R;" +
                                   "在您寻找同伴的时候，$R;" +
                                   "希望能给您一点帮助。$R;" +
                                   "$P啊! 还有这个!$R;" +
                                   "这个介绍信啊!$R;" +
                                   "埃米尔好像忘记了。$R;" +
                                   "$R…真不知道魂魄都在哪里?$R;" +
                                   "没吃药吗…$R;" +
                                   "$P噢! 离题了，$R;" +
                                   "$R把介绍信交给$R;" +
                                   "「阿克罗波利斯」的「酒馆老板」$R;" +
                                   "或「酒馆分店店员」吧!$R;" +
                                   "他们会给你介绍工作呢!$R;" +
                                   "$P「酒馆老板」$R;" +
                                   "在「阿克罗波利斯」的「下城」的东边。$R;" +
                                   "「酒馆分店店员」$R;" +
                                   "在「阿克罗波利斯」的东、南、西、北平原的中央。$R;" +
                                   "$P还有…$R;" +
                                   "等熟悉这个世界的话，$R;" +
                                   "去一趟西边的「蜜蜂巢穴」吧!$R;" +
                                   "听说他们会帮初心者，$R;" +
                                   "介绍适合的工作呢!$R;" +
                                   "$R说不定是寻找同伴的好地方哦!$R;" +
                                   "$P啊! 是的!$R;" +
                                   "前面的桥上有个叫「复活战士」的人。$R;" +
                                   "$P去他那里找他说话吧!$R;" +
                                   "他曾说过他有方便的道具!$R;" +
                                   "从那边的传送点过去，$R;" +
                                   "就可以到达城市里了。$R;" +
                                   "$R祝你好运!!$R;", "泰塔斯");
        }

        void 提多給予埃米爾介紹信(ActorPC pc)
        {
            BitMask<Beginner_01> Beginner_01_mask = new BitMask<Beginner_01>(pc.CMask["Beginner_01"]);

            Beginner_01_mask.SetValue(Beginner_01.埃米爾給予埃米爾介紹書, true);

            Say(pc, 11000973, 131, "您好!$R;" +
                                   "$R我是泰塔斯。$R;" +
                                   "$P…这个给您吧!$R;" +
                                   "这是『埃米尔介绍信』。$R;", "泰塔斯");

            PlaySound(pc, 2040, false, 100, 50);
            GiveItem(pc, 10043081, 1);
            Say(pc, 0, 0, "得到『埃米尔介绍信』!$R;", " ");

            Say(pc, 11000973, 131, "埃米尔好像忘记了。$R;" +
                                   "$R…真不知道魂魄都丢在哪里了?$R;" +
                                   "没吃药吗…$R;" +
                                   "$P噢! 离题了，$R;" +
                                   "$R把介绍信交给$R;" +
                                   "「阿克罗波利斯」的「酒馆老板」$R;" +
                                   "或「酒馆分店店员」吧!$R;" +
                                   "他们会给你介绍工作呢!$R;" +
                                   "$P「酒馆老板」$R;" +
                                   "在「阿克罗波利斯」的「下城」的东边。$R;" +
                                   "「酒馆分店店员」$R;" +
                                   "在「阿克罗波利斯」的东、南、西、北平原的中央。$R;" +
                                   "$P还有…$R;" +
                                   "熟悉ECO世界的话，$R;" +
                                   "去一趟西边的「蜜蜂巢穴」吧!$R;" +
                                   "听说他们会帮初心者，$R;" +
                                   "介绍适合的工作呢!$R;" +
                                   "$R说不定是寻找同伴的好地方哦!$R;" +
                                   "$P啊! 是的!$R;" +
                                   "前面的桥上有个叫「复活战士」的人。$R;" +
                                   "$P去他那里找他说话吧!$R;" +
                                   "他曾说过他有方便的道具!$R;" +
                                   "从那边的传送点过去，$R;" +
                                   "就可以到达城市里了。$R;" +
                                   "$R祝你好运!!$R;", "泰塔斯");
        }
    }
}
