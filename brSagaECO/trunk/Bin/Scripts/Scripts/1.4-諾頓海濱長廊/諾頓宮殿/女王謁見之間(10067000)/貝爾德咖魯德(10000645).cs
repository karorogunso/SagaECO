using System;
using System.Collections.Generic;
using System.Text;
using SagaLib;
using SagaDB.Actor;
using SagaMap.Scripting;
using SagaScript.Chinese.Enums;
namespace SagaScript.M10067000
{
    public class S10000645 : Event
    {
        public S10000645()

        {
            this.EventID = 10000645
;
        }

        public override void OnEvent(ActorPC pc)
        {   
            //万圣节部分
            BitMask<wsj> wsj_mask = new BitMask<wsj>(pc.CMask["wsj"]);
            //int selection;
            if (wsj_mask.Test(wsj.开始))
            {
                Say(pc, 0, 0, "……。$R;" +
                "我が名はヴェルデガルド$R;" +
                "$Rノーザン王朝を統べる女王。$R;" +
                "$P……。$R;" +
                "$P……あなたの友達は、まだあの部屋に$R;" +
                "閉じ込められています。$R;" +
                "$Rさあ、あなたが選ぶ道は２つ。$R;" +
                "友を助けるか、見捨てるか。$R;" +
                "どちらを選んでも、かまいません。$R;", "");
                if (Select(pc, "好きなほうを選びなさい", "", "友を助ける", "友を見捨てる") == 1)
                {
                    Say(pc, 0, 0, "魔法ギルド総本山に向かいなさい。$R;" +
                    "そこで、きっと何か$R;" +
                    "手がかりをつかむことでしょう。$R;" +
                    "$R魔法ギルド総本山は蒼と金の色で$R;" +
                    "彩られた装飾豊かな建物です。$R;" +
                    "$P道に迷ったり、どうしていいか$R;" +
                    "わからないときは僧兵に聞きなさい。$R;" +
                    "$R彼らに案内させましょう……。$R;", "");
                    wsj_mask.SetValue(wsj.女王, true);
                    wsj_mask.SetValue(wsj.开始, false);
                    Warp(pc, 10065000, 52, 21);
                    
                    return;
                }
                Say(pc, 0, 0, "すべてを無にかえしましょう。$R;" +
                "あなたは、夢を見ていたのです。$R;", "");
                wsj_mask.SetValue(wsj.开始, false);
                Warp(pc, 10023000, 131, 139);
                return;
            }
            BitMask<NDFlags> mask = new BitMask<NDFlags>(pc.CMask["ND"]);
            //EVT11000538
            /*
            if (_Xa10 && !_Xa11)
            {
                if (!_0b18)
                {
                    Say(pc, 131, "…$R;" +
                        "我的名字叫貝爾德咖魯德$R;" +
                        "$R是諾頓女王$R;" +
                        "$P…$R;" +
                        "$P您的朋友$R;" +
                        "還被關在這間房子裡阿$R;" +
                        "$P現在您只有兩個選擇$R;" +
                        "救朋友還是裝做不知道呢$R;" +
                        "選擇哪個，是您的自由呀$R;");
                    switch (Select(pc, "選擇哪樣呢", "", "救朋友", "裝做不知道"))
                    {
                        case 1:
                            _0b18 = true;
                            Say(pc, 131, "您去魔法行會總部吧$R;" +
                                "在那裡會找到頭緒的$R;" +
                                "$R魔法行會總部是$R;" +
                                "藍色和金黃色的華麗建築唷，$R;" +
                                "不明白的時候$R;" +
                                "$R問武僧吧$R;" +
                                "$R已經跟他們說好了，會給您引路的$R;");
                            //#ノーザン宮殿ホールへ
                            Warp(pc, 10065000, 50, 21);
                            //WARP  629
                            break;
                        case 2:
                            Say(pc, 131, //#ハロウィン終了
                            "就當沒發生過好嗎$R;" +
                                "只當作一場您做的夢吧$R;");
                            _Xa10 = false;
                            //#アクロポリスへ
                            Warp(pc, 10023000, 130, 139);
                            //WARP  201
                            break;
                    }
                    return;
                }
                if (!_0b19)
                {
                    Say(pc, 131, "不明白的時候$R;" +
                        "問武僧吧$R;" +
                        "$R已經跟他們說好了，會給您指路的$R;");
                    return;
                }
            }
            */
            if (!mask.Test(NDFlags.貝爾德咖魯德第一次对话))
            {
                mask.SetValue(NDFlags.貝爾德咖魯德第一次对话, true);
                //_2A45 = true;
                SkillPointBonus(pc, 1);
                Say(pc, 131, "…$R;" +
                    "大陸來的冒險者，路上辛苦了$R;" +
                    "$P我的名字叫貝爾德咖魯德$R;" +
                    "$R是諾頓女王$R;" +
                    "$P…很驚訝是嗎？$R您看到是我的幻想全息圖$R;" +
                    "$P在您的腳下能看到塔內$R流出的光芒吧？$R;" +
                    "$R這就是這王國的真正首都諾頓市唷$R;" +
                    "$P很遺憾，您能進入的地方只能到這兒$R;" +
                    "$R從這裡開始是$R我們諾頓市民的神聖領地，$R別國人是看不見的$R;");
                Wait(pc, 2000);
                PlaySound(pc, 3087, false, 100, 50);
                ShowEffect(pc, 4131);
                Wait(pc, 2000);
                Say(pc, 131, "技能點數上升了1$R;");
                Say(pc, 131, "喚起了您潛在的力量$R;" +
                    "$P感謝您通過冰雪到我們這來，$R這是給您的小小禮物唷。$R;" +
                    "$P您只要經過磨練後，$R就會發現潛在的力量，$R通往神聖領域的門就會開啟的$R;" +
                    "$R現在回去吧$R;");
                return;
            }
            //#うさぎクエストへ
            if (pc.Level > 59 && !mask.Test(NDFlags.职业装任务))
            {
                Say(pc, 131, "…$R;" +
                    "$R我的名字叫貝爾德咖魯德$R;" +
                    "是諾頓女王$R;" +
                    "$P冒險者！$R;" +
                    "充滿力量的冒險者呀！$R;" +
                    "$R想不想為我做事呢？$R;" +
                    "$P只要發誓為我做事，$R;" +
                    "我會給您王宮的寶物唷$R;");
                switch (Select(pc, "為女王做事嗎？", "", "夠了", "為了女王做事"))
                {
                    case 1:
                        Say(pc, 131, "…$R;" +
                            "$R那麼您沒有理由待在這裡了$R;" +
                            "回去吧$R;");
                        break;
                    case 2:
                        mask.SetValue(NDFlags.职业装任务, true);
                       // _0c53 = true;
                        Say(pc, 131, "真是有勇氣的冒險者呀$R;" +
                            "聽到這句話太高興了$R;" +
                            "$R先把這個收下吧$R;");
                        GiveItem(pc, 10022600, 1);
                        Say(pc, 131, "拿到了『女王的鑰匙』$R;");
                        Say(pc, 131, "這是『寶物倉庫』的鑰匙$R;" +
                            "拿這個鑰匙打開您喜歡的箱子吧$R;" +
                            "$R這鑰匙是對您承諾的報酬$R;" +
                            "$P每次您為我做事$R;" +
                            "我會給您一把鑰匙唷$R;" +
                            "$P委託勤衛兵保管了$R;" +
                            "$R詳細的去問宮廷大堂的女衛兵吧$R;");
                        break;
                }
                return;
            }
            Say(pc, 131, "…$R;");
        }
    }
}