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

            Say(pc, 11000968, 131, "對我們西軍有興趣嗎?$R;", "西軍騎士團長官");

            switch (Select(pc, "要問嗎?", "", "摩根島是?", "… 不用了"))
            {
                case 1:
                    Say(pc, 11000968, 131, "是在奧克魯尼亞大陸西南邊，$R;" +
                                           "摩根炭的山地地帶呀!$R;" +
                                           "$R定期運行的飛空庭，$R;" +
                                           "可以在奧克魯尼亞大陸西邊的$R;" +
                                           "「軍艦島」上乘坐，$R;" +
                                           "搭乘飛空庭就可以越過大海了唷!$R;" +
                                           "$P在摩根島裡乘坐飛空庭，$R;" +
                                           "再往西南邊走的話，$R;" +
                                           "可以去到叫「光之塔」的$R;" +
                                           "機械時代遺跡呀!$R;" +
                                           "$R聽說冒險者現在都在那裡探索呢!$R;", "西軍騎士團長官");
                    break;
                    
                case 2:
                    Say(pc, 11000968, 131, "這樣呀…$R;" +
                                           "$R如果想加入西軍的話，$R;" +
                                           "請隨時來到城市西邊，$R;" +
                                           "西軍騎士團的長官室吧!$R;" +
                                           "$R在城市西邊左右側，$R;" +
                                           "華麗的黃色建築物就是了。$R;", "西軍騎士團長官");
                    break;
            }
        }

        void 與騎士團長官們進行第一次對話(ActorPC pc)
        {
            BitMask<Beginner_02> Beginner_02_mask = new BitMask<Beginner_02>(pc.CMask["Beginner_02"]);

            Beginner_02_mask.SetValue(Beginner_02.已經與騎士團長官們進行第一次對話, true);

            Say(pc, 11000968, 131, "歡迎來到阿高普路斯市唷!$R;", "西軍騎士團長官");

            Say(pc, 11000967, 131, "哦哦! 是從東方海角來的嗎??$R;" +
                                   "$R加入我們東軍的話，$R;" +
                                   "馬上就可以進帕斯特! 怎麼樣!?$R;", "東軍騎士團長官");

            Say(pc, 11000970, 131, "喂，說是帕斯特島，$R;" +
                                   "其實是只有樹和山坡的國家呀!$R;" +
                                   "$P加入我們北軍的話，$R;" +
                                   "就可以自由出入諾頓!$R;" +
                                   "怎麼樣? 我們北軍…$R;", "北軍騎士團長官");

            Say(pc, 11000969, 131, "諾頓島不是只有寒冷的曠野嗎!?$R;" +
                                   "$R來加入到我們南軍吧!$R;" +
                                   "阿伊恩薩烏斯是戰鬥者的天國!$R;" +
                                   "有很多強大的敵人，$R;" +
                                   "是發揮實力的好地方喔!$R;", "南軍騎士團長官");

            Say(pc, 11000968, 131, "阿伊恩薩烏斯島只是像蒸籠$R;" +
                                   "一樣熱而已啊!$R;" +
                                   "$R而且在地牢裡發生意外的報導，$R;" +
                                   "絡繹不絕呢!$R;" +
                                   "$P…怎麼樣?$R;" +
                                   "$R加入我們西軍的話，$R;" +
                                   "可以隨意進出西邊的摩根島唷！$R;" +
                                   "在炭礦也可以掙錢呀。$R;" +
                                   "在光之塔裡還可以找到$R;" +
                                   "機械文明的古蹟喔!$R;", "西軍騎士團長官");

            Say(pc, 11000967, 131, "摩根島，一句話可以形容，$R;" +
                                   "不是到處都是泥土嗎!$R;", "東軍騎士團長官");

            Say(pc, 11000968, 131, "嗯…$R;", "西軍騎士團長官");

            Say(pc, 0, 0, "雖然知道關係不好…$R;" +
                          "$R但關於軍隊的消息不一個一個問，$R;" +
                          "不行啊?$R;", " ");
        } 
    }
}
