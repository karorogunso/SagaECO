using System;
using System.Collections.Generic;
using System.Text;

using SagaDB.Actor;
using SagaMap.Scripting;

using SagaLib;
using SagaScript.Chinese.Enums;
//所在地圖:北部平原初心者學校(30091004) NPC基本信息:初階冒險者(11000996) X:4 Y:6
namespace SagaScript.M30091004
{
    public class S11000996 : Event
    {
        public S11000996()
        {
            this.EventID = 11000996;
        }

        public override void OnEvent(ActorPC pc)
        {
            BitMask<Beginner_02> Beginner_02_mask = new BitMask<Beginner_02>(pc.CMask["Beginner_02"]);

            if (!Beginner_02_mask.Test(Beginner_02.已經與女初階冒險者進行第一次對話))
            {
                初次與初階冒險者進行對話(pc);
                return;
            }

            Say(pc, 11000996, 0, "哎呀，您好!$R;" +
                                 "$R現在對「阿高普路斯市」熟悉了嗎?$R;", "初階冒險者");
        }

        void 初次與初階冒險者進行對話(ActorPC pc)
        {
            BitMask<Beginner_02> Beginner_02_mask = new BitMask<Beginner_02>(pc.CMask["Beginner_02"]);

            Beginner_02_mask.SetValue(Beginner_02.已經與女初階冒險者進行第一次對話, true);

            int selection;

            Say(pc, 11000996, 0, "哎呀，您好!$R;" +
                                 "$R您第一次來「阿高普路斯市」嗎?$R;" +
                                 "$P我只比您稍微早一點$R;" +
                                 "來到「阿高普路斯市」而已。$R;" +
                                 "$R雖然可能不是遇到初心者。$R;" +
                                 "$P但在這裡，可以很容易找到$R;" +
                                 "跟自己等級差不多的人組成隊伍。$R;" +
                                 "所以我也不知不覺走到這來了。$R;", "初階冒險者");

            selection = Select(pc, "要繼續聊天嗎?", "", "衣服很可愛啊!", "您的頭髮是怎麼弄的?", "不用了");

            while (selection != 3)
            {
                switch (selection)
                {
                    case 1:
                        Say(pc, 11000996, 0, "真的? 謝謝呀!$R;" +
                                             "$P這個T恤是在「阿高普路斯市」的「下城」的$R;" +
                                             "「染色店阿姨」幫我染的。$R;" +
                                             "$R帶『罩衫(男)』或『罩衫(女)』去的話，$R;" +
                                             "她會給您免費染色的。$R;" +
                                             "$P好像是…有點…多變的阿姨呀!$R;" +
                                             "要染什麼顏色，看他那時的心情吧?$R;" +
                                             "$R到了「下城」有空的話，去看看也不錯唷!$R;" +
                                             "$P要是不知道阿姨在哪裡?$R;" +
                                             "看看小地圖吧，紅點就是NPC的位置，$R;" +
                                             "參考一下吧!$R;", "初階冒險者");
                        break;

                    case 2:
                        Say(pc, 11000996, 0, "這個頭髮是在「阿高普路斯市」的「上城」的$R;" +
                                             "「尼貝隆肯髮型屋」得到的唷!$R;" +
                                             "$P那裡有個語氣奇怪的髮型師，$R;" +
                                             "他會剪頭髮和設計造型喔!$R;" +
                                             "$P啊! 這個頭髮有店特別吧!$R;" +
                                             "是使用『髮型屋介紹書』的呀!$R;" +
                                             "$R是只有拿著介紹書$R;" +
                                             "才幫您設計特別的髮型。$R;" +
                                             "$P嗯? 『髮型屋介紹書』是怎麼得到的?$R;" +
                                             "$P我把平原的木箱打破得到『木箱』，$R;" +
                                             "然後請「道具精製師」幫我打開後。$R;" +
                                             "才發現裡面有介紹書的。$R;" +
                                             "$R除此之外，還有其他的方法喔!$R;", "初階冒險者");
                        break;
                }

                selection = Select(pc, "要繼續聊天嗎?", "", "衣服很可愛啊", "您的頭髮是怎麼弄的?", "不用了");
            }
        }
    }
}
