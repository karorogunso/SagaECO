using System;
using System.Collections.Generic;
using System.Text;

using SagaDB.Actor;
using SagaMap.Scripting;

using SagaLib;
using SagaScript.Chinese.Enums;
//所在地圖:奧克魯尼亞東部平原(10025001) NPC基本信息:報告任務的女孩(11000982) X:103 Y:133
namespace SagaScript.M10025001
{
    public class S11000982 : Event
    {
        public S11000982()
        {
            this.EventID = 11000982;
        }

        public override void OnEvent(ActorPC pc)
        {
            BitMask<Beginner_02> Beginner_02_mask = new BitMask<Beginner_02>(pc.CMask["Beginner_02"]);

            if (!Beginner_02_mask.Test(Beginner_02.轉交感謝信任務完成))
            {
                轉交感謝信任務(pc);
                return;
            }

            Say(pc, 11000982, 131, "過了這路端的橋，$R;" +
                                   "就是在大陸中心的「阿高普路斯市」喔!$R;" +
                                   "$R那麼，加油唷~!!$R;", "報告任務的女孩");            
        }

        void 轉交感謝信任務(ActorPC pc)
        {
            BitMask<Beginner_02> Beginner_02_mask = new BitMask<Beginner_02>(pc.CMask["Beginner_02"]);

            if (!Beginner_02_mask.Test(Beginner_02.轉交感謝信任務開始))
            {
                Say(pc, 11000982, 131, "初心者 您好~!$R;" +
                                       "$R嗯? 我嗎?$R;" +
                                       "我是為了報告「任務」情況，$R;" +
                                       "來咖啡館分店的。$R;" +
                                       "$P「任務」是指，$R;" +
                                       "在 ECO世界裡發生的事件。$R;" +
                                       "$R詳細的去問咖啡館店員吧?$R;" +
                                       "指導結束後去問一下吧!$R;" +
                                       "$P哎，真是的。$R;" +
                                       "$R雖然不是「任務」…$R;" +
                                       "但可以拜託您一件事嗎?$R;" +
                                       "$P有東西想要交給，$R;" +
                                       "那邊的 「初心者嚮導」啊!$R;", "報告任務的女孩");

                switch (Select(pc, "怎麼做呢?", "", "好的", "因為有點忙…"))
                {
                    case 1:
                        Beginner_02_mask.SetValue(Beginner_02.轉交感謝信任務開始, true);

                        Say(pc, 11000982, 131, "真的?$R;" +
                                               "$R哇啊! 非常感謝!!$R;" +
                                               "$P這封信啊…$R;" +
                                               "$R一想到要給他就有點害羞呢!$R;" +
                                               "$P因為他幫了我很多，$R;" +
                                               "所以想給他感謝信。可是…$R;" +
                                               "$R哇! 臉又紅了!$R;" +
                                               "求求您! 把這個交給他吧!!$R;", "報告任務的女孩");

                        PlaySound(pc, 2040, false, 100, 50);
                        GiveItem(pc, 10043190, 1);
                        Say(pc, 0, 0, "收到了『感謝信』！$R;", " ");
                        return;
                        
                    case 2:
                        Say(pc, 11000982, 131, "是嗎? 那樣的話，就沒辦法了。$R;" +
                                               "$P過了這路端的橋，$R;" +
                                               "就是「阿高普路斯市」了。$R;" +
                                               "$R那麼，加油喔~!!$R;", "報告任務的女孩");
                        return;
                }
            }

            if (Beginner_02_mask.Test(Beginner_02.已經把信轉交給初心者嚮導))
            {
                Beginner_02_mask.SetValue(Beginner_02.轉交感謝信任務完成, true);

                Say(pc, 11000982, 131, "啊? 送給他了!?$R;", "報告任務的女孩");

                Say(pc, 0, 0, "要告訴她已經把信交給他了，$R;" +
                              "還有從「初心者嚮導」那帶來了口訊。$R;", " ");

                Say(pc, 11000982, 131, "他說很開心嗎? 那太好了!!$R;" +
                                       "$R真的非常感謝，$R;" +
                                       "啊…給您這個作為謝禮吧!$R;", "報告任務的女孩");

                PlaySound(pc, 2040, false, 100, 50);
                GiveItem(pc, 10043302, 1);
                Say(pc, 0, 0, "得到『艾格三明治』!$R;", " ");

                Say(pc, 11000982, 131, "本來想晚一點吃才帶來的，$R;" +
                                       "不知道為什麼，心總是卜卜地跳。$R;" +
                                       "$R…對不起、對不起!$R;" +
                                       "說這些很沒趣吧?$R;" +
                                       "$P我現在拜託您的事情，$R;" +
                                       "就像「任務」那樣。$R;" +
                                       "$R「任務」中$R;" +
                                       "有「擊退任務」、$R;" +
                                       "「採集任務」、$R;" +
                                       "「搬運任務」這三種唷!$R;" +
                                       "$P剛剛送信跟「搬運任務」比較相似。$R;" +
                                       "$R簡單來說，就是做 「跑腿」的。$R;" +
                                       "$P關於「任務」的詳細說明，$R;" +
                                       "到咖啡館店員那裡聽聽吧?$R;" +
                                       "$R我也聽過店員的說明，$R;" +
                                       "講的很簡單很容易明白唷!$R;" +
                                       "$P過了這路端的橋，$R;" +
                                       "就是「阿高普路斯市」了!$R;" +
                                       "$R那麼，加油喔~!!$R;", "報告任務的女孩");
            }
            else
            {
                Say(pc, 11000982, 131, "「初心者嚮導」$R;" +
                                       "就在後方學校的前面呀!$R;" +
                                       "$R把這信帶過去吧! 拜託了!$R;", "報告任務的女孩");
            }
        }
    }
}
