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

            Say(pc, 11000973, 131, "從那邊的傳送點過去，$R;" +
                                   "就可以到達城市裡了。$R;" +
                                   "$R祝您戰鬥順利喔!!$R;", "提多");
        }

        void 提多給予初心者緞帶和埃米爾介紹信(ActorPC pc)
        {
            BitMask<Beginner_01> Beginner_01_mask = new BitMask<Beginner_01>(pc.CMask["Beginner_01"]);

            Beginner_01_mask.SetValue(Beginner_01.埃米爾給予埃米爾介紹書, true);
            Beginner_01_mask.SetValue(Beginner_01.貝利爾給予初心者緞帶, true);

            Say(pc, 11000973, 131, "您好!$R;" +
                                   "$R您基本的概念，$R;" +
                                   "都知道了吧?", "提多");

            PlaySound(pc, 2040, false, 100, 50);
            GiveItem(pc, 50053600, 1);
            GiveItem(pc, 10043081, 1);
            Say(pc, 0, 0, "得到『初心者緞帶』$R;" +
                          "和『埃米爾介紹信』!$R;", " ");

            Say(pc, 11000973, 131, "這是初心者使用的裝備。$R;" +
                                   "在您尋找同伴的時候，$R;" +
                                   "希望能給您一點幫助。$R;" +
                                   "$P啊! 還有這個!$R;" +
                                   "這個介紹信啊!$R;" +
                                   "埃米爾好像忘記了。$R;" +
                                   "$R…真不知道魂魄都在哪裡了?$R;" +
                                   "連藥都想吃…$R;" +
                                   "$P噢! 離題了，$R;" +
                                   "$R把介紹信交給$R;" +
                                   "「阿高普路斯市」的「咖啡館老闆」$R;" +
                                   "或「咖啡館分店店員」吧!$R;" +
                                   "他們會給您介紹工作唷!$R;" +
                                   "$P「咖啡館老闆」$R;" +
                                   "在「阿高普路斯市」的「下城」的東邊。$R;" +
                                   "「咖啡館分店店員」$R;" +
                                   "在「阿高普路斯市」的東、南、西、北平原的中央。$R;" +
                                   "$P還有…$R;" +
                                   "熟悉ECO世界的話，$R;" +
                                   "去一趟西邊的「蜂之巢穴」吧!$R;" +
                                   "聽說他們會幫初心者，$R;" +
                                   "介紹適合的工作呢!$R;" +
                                   "$R說不定是尋找同伴的好地方唷!$R;" +
                                   "$P啊! 是的!$R;" +
                                   "前面的橋上有個叫「復活戰士」的人。$R;" +
                                   "$P去他那裡找他說話吧!$R;" +
                                   "他曾說過他有方便的道具唷!$R;" +
                                   "從那邊的傳送點過去，$R;" +
                                   "就可以到達城市裡了。$R;" +
                                   "$R祝您戰鬥順利喔!!$R;", "提多");
        }

        void 提多給予埃米爾介紹信(ActorPC pc)
        {
            BitMask<Beginner_01> Beginner_01_mask = new BitMask<Beginner_01>(pc.CMask["Beginner_01"]);

            Beginner_01_mask.SetValue(Beginner_01.埃米爾給予埃米爾介紹書, true);

            Say(pc, 11000973, 131, "您好!$R;" +
                                   "$R我是提多。$R;" +
                                   "$P…這個給您吧!$R;" +
                                   "這是『埃米爾介紹信』。$R;", "提多");

            PlaySound(pc, 2040, false, 100, 50);
            GiveItem(pc, 10043081, 1);
            Say(pc, 0, 0, "得到『埃米爾介紹信』!$R;", " ");

            Say(pc, 11000973, 131, "埃米爾好像忘記了。$R;" +
                                   "$R…真不知道魂魄都在哪裡了?$R;" +
                                   "連藥都想吃…$R;" +
                                   "$P噢! 離題了，$R;" +
                                   "$R把介紹信交給$R;" +
                                   "「阿高普路斯市」的「咖啡館老闆」$R;" +
                                   "或「咖啡館分店店員」吧!$R;" +
                                   "他們會給您介紹工作唷!$R;" +
                                   "$P「咖啡館老闆」$R;" +
                                   "在「阿高普路斯市」的「下城」的東邊。$R;" +
                                   "「咖啡館分店店員」$R;" +
                                   "在「阿高普路斯市」的東、南、西、北平原的中央。$R;" +
                                   "$P還有…$R;" +
                                   "熟悉ECO世界的話，$R;" +
                                   "去一趟西邊的「蜂之巢穴」吧!$R;" +
                                   "聽說他們會幫初心者，$R;" +
                                   "介紹適合的工作呢!$R;" +
                                   "$R說不定是尋找同伴的好地方唷!$R;" +
                                   "$P啊! 是的!$R;" +
                                   "前面的橋上有個叫「復活戰士」的人。$R;" +
                                   "$P去他那裡找他說話吧!$R;" +
                                   "他曾說過他有方便的道具唷!$R;" +
                                   "從那邊的傳送點過去，$R;" +
                                   "就可以到達城市裡了。$R;" +
                                   "$R祝您戰鬥順利喔!!$R;", "提多");
        }
    }
}
