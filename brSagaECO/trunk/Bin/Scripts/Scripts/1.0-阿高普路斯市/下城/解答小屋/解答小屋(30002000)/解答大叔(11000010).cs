using System;
using System.Collections.Generic;
using System.Text;

using SagaDB.Actor;
using SagaMap.Scripting;

using SagaLib;
using SagaScript.Chinese.Enums;

using SagaDB.Quests;
//所在地圖:解答小屋(30002000) NPC基本信息:解答大叔(11000010) X:2 Y:1
namespace SagaScript.M30002000
{
    public class S11000010 : Event
    {
        public S11000010()
        {
            this.EventID = 11000010;

            //任務服務台相關設定
            this.leastQuestPoint = 1;

            this.alreadyHasQuest = "任務進行的還順利嗎?$R;";

            this.gotNormalQuest = "不要辜負我對你的期待啊!$R;";

            this.questCompleted = "辛苦了…$R;" +
                                  "$R恭喜你，任務完成了。$R;" +
                                  "$P來! 收下報酬吧!$R;";

            this.questCanceled = "……$R;" +
                                 "$P下次好好思考以後，$R;" +
                                 "再決定要不要接受任務會比較好。$R;";

            this.questFailed = "…看來是失敗了$R;" +
                               "都寫臉上了呢…$R;" +
                               "$R只要失去過一次信賴，$R;" +
                               "以後要挽回就是非常難的。$R;";

            this.notEnoughQuestPoint = "現在沒有要拜託的。$R;";

            this.questTooEasy = "對你來說這個任務太簡單了。$R;" +
                                "$R如果不擔心別人的評價，$R;" +
                                "承接這件委託怎麼樣啊?$R;";

            this.questTooHard = "這是個非常麻煩的任務…$R;" +
                                "$R小鬼呀，你確定能做到嗎?$R;"; 
        }

        public override void OnEvent(ActorPC pc)
        {                                 //任務：加入騎士團
            BitMask<Acropolisut_Pass_03> Acropolisut_Pass_03_mask = new BitMask<Acropolisut_Pass_03>(pc.CMask["Acropolisut_Pass_03"]);                                  //任務：取得偽造通行證

            BitMask<Knights> Knights_mask = pc.CMask["Knights"]; 
            BitMask<Neko_03> Neko_03_amask = pc.AMask["Neko_03"];
            BitMask<Neko_03> Neko_03_cmask = pc.CMask["Neko_03"];

            if (Neko_03_amask.Test(Neko_03.堇子任務開始) &&
                !Neko_03_amask.Test(Neko_03.堇子任務完成) &&
                Neko_03_cmask.Test(Neko_03.與鬼斬破多加對話) &&
                !Neko_03_cmask.Test(Neko_03.帶理路離開))
            {
                if (Neko_03_cmask.Test(Neko_03.找到理路))//_7A37)
                {
                    Say(pc, 11000010, 131, "去南軍地下倉庫？$R;");
                }
                else
                {
                    Say(pc, 11000010, 131, "？？$R;" +
                        "$R臉色不太好啊$R有什麼事情嗎？$R;");
                    Say(pc, 0, 131, "把到現在為止的事情都說了$R;");
                    Say(pc, 11000010, 131, "…嘿嘿！這樣啊$R;" +
                        "$R知道是什麼事情了$R很久以前就認識加多的$R;" +
                        "$P如果是有懷疑的地方的話…$R南軍地下倉庫有點奇怪…$R告訴你到那個地方的秘道吧$R;" +
                        "$R馬上就要去嗎？$R;");
                }
                switch (Select(pc, "馬上去嗎？", "", "去！", "現在不去！"))
                {
                    case 1:
                        if (pc.PossesionedActors.Count != 0)
                        {
                            Say(pc, 11000010, 131, "真是！$R好像有人在憑依？$R;" +
                                "$R這通路只能讓一個人通過！$R;");
                            return;
                        }
                        Say(pc, 11000010, 131, "好！$R;" +
                            "$R小心啊！$R;");
                        pc.CInt["Neko_03_Map2"] = CreateMapInstance(50007000, 30002000, 4, 2);

                        Warp(pc, (uint)pc.CInt["Neko_03_Map2"], 8, 28);
                        //EVENTMAP_IN 7 1 8 26 4
                        /*
                        if (a//ME.WORK0 = -1
                        )
                        {
                            Say(pc, 11000010, 131, "哎呀…現在有點困難啊？$R;" +
                                "$R軍隊的監視感應器開啓了…$R稍微等一會，再跟我說話吧$R;");
                            return;
                        }//*/
                        break;
                    case 2:
                        Say(pc, 11000010, 131, "是嗎？$R;" +
                            "$R準備好了就告訴我吧$R;");
                        break;
                }

                return;
            }

            if (Knights_mask.Test(Knights.已經加入騎士團))
            {
                Say(pc, 11000010, 131, "小鬼你看!$R;" +
                                       "我已經下決心當軍人了。$R;" +
                                       "$R不做生意了，趕快給我出去!!$R;", "解答大叔");

                Warp(pc, 10024000, 58, 87);
                return;
            }

            if (!Acropolisut_Pass_03_mask.Test(Acropolisut_Pass_03.解答大叔給予上城的偽造通行證))
            {
                取得偽造通行證(pc);
                return;
            }

            if (!Acropolisut_Pass_03_mask.Test(Acropolisut_Pass_03.給予解答大叔美味的咖哩))
            {
                解答大叔第一階段販賣物品(pc);
                return;
            }

            if (pc.Level < 30)
            {
                解答大叔第二階段販賣物品(pc);
                return;
            }

            if (Acropolisut_Pass_03_mask.Test(Acropolisut_Pass_03.解答大叔認可玩家的實力))
            {
                Acropolisut_Pass_03_mask.SetValue(Acropolisut_Pass_03.解答大叔認可玩家的實力, true);

                Say(pc, 11000010, 131, "變得很強了啊……$R;" +
                                       "$R如果是你的話，$R;" +
                                       "那就沒問題了。$R;", "解答大叔");
            }

            if (pc.Job == PC_JOB.SCOUT)
            {
                解答大叔第三階段盜賊專用販賣物品(pc);
            }
            else
            {
                解答大叔第三階段一般販賣物品(pc);
            }
        }

        void 取得偽造通行證(ActorPC pc)
        {
            BitMask<Acropolisut_01> Acropolisut_01_mask = new BitMask<Acropolisut_01>(pc.CMask["Acropolisut_01"]);                                                      //一般：阿高普路斯市

            if (pc.Level < 2)
            {
                Say(pc, 11000010, 131, "……$R;" +
                                       "$P從「佩頓」那小子那裡$R;" +
                                       "聽到有關這裡的事嗎?$R;" +
                                       "$P那小子還真是不可靠啊!$R;" +
                                       "總是說一些不該說的話。$R;" +
                                       "$P這裡不是你這種初心者來的地方，$R;" +
                                       "快點回去吧!$R;" +
                                       "$R等你等級高一點以後，$R;" +
                                       "再過來吧!!", "解答大叔");
                return;
            }
            else
            {
                if (!Acropolisut_01_mask.Test(Acropolisut_01.已經與解答大叔進行第一次對話))
                {
                    初次與解答大叔進行對話(pc);
                }

                Say(pc, 11000010, 131, "小鬼想幹嘛?$R;", "解答大叔");

                switch (Select(pc, "想做什麼呢?", "", "買東西", "賣東西", "委託製作『阿高普路斯市偽造通行證』", "什麼也不做"))
                {
                    case 1:
                        OpenShopBuy(pc, 11);

                        Say(pc, 11000010, 131, "以後不要再來這樣的地方了。$R;", "解答大叔");
                        break;

                    case 2:
                        OpenShopSell(pc, 11);

                        Say(pc, 11000010, 131, "以後不要再來這樣的地方了。$R;", "解答大叔");
                        break;

                    case 3:
                        委託製作上城的偽造通行證(pc);
                        break;

                    case 4:
                        break;
                }           
            }
        }

        void 初次與解答大叔進行對話(ActorPC pc)
        {
            BitMask<Acropolisut_01> Acropolisut_01_mask = new BitMask<Acropolisut_01>(pc.CMask["Acropolisut_01"]);                                                      //一般：阿高普路斯市

            Acropolisut_01_mask.SetValue(Acropolisut_01.已經與解答大叔進行第一次對話, true);

            if (pc.Job == PC_JOB.SCOUT)
            {
                Say(pc, 11000010, 131, "你是『盜賊』嗎?$R;" +
                                       "$R嘿…那傢伙竟然批准了?$R;" +
                                       "看來是很喜歡您啊…$R;" +
                                       "$P經常得到「盜賊們」的幫助啊!$R;" +
                                       "現在換我來幫你吧!$R;" +
                                       "$P這裡是「闇黑商店」。$R;" +
                                       "#R只要是危險的事什麼都做…$R;" +
                                       "咯咯…$R;" +
                                       "$P我不想把單純的人牽扯進來，$R;" +
                                       "不可以跟別人說喔。$R;", "解答大叔");
            }
            else
            {
                Say(pc, 11000010, 131, "……$R;" +
                                       "$P從「佩頓」那小子那裡$R;" +
                                       "聽到有關這裡的情報，$R;" +
                                       "所以才來知道這裡嗎?$R;" +
                                       "$P最近總是來一些初心者啊!$R;" +
                                       "$R「下城」也變的冷清了。$R;" +
                                       "$P你知道這裡是什麼地方嗎?$R;" +
                                       "$R呵~ 無論如何，$R;" +
                                       "總得停一下聽聽吧?$R;", "解答大叔");
            }
        }

        void 委託製作上城的偽造通行證(ActorPC pc)
        {
            BitMask<Acropolisut_Pass_03> Acropolisut_Pass_03_mask = new BitMask<Acropolisut_Pass_03>(pc.CMask["Acropolisut_Pass_03"]);                                  //任務：取得偽造通行證

            if (Acropolisut_Pass_03_mask.Test(Acropolisut_Pass_03.向萬物博士詢問解答大叔的弱點))
            {
                Acropolisut_Pass_03_mask.SetValue(Acropolisut_Pass_03.解答大叔給予上城的偽造通行證, true);

                Say(pc, 11000010, 131, "那傢伙…$R;" +
                                       "連我喜歡的都說了…$R;" +
                                       "真是囉唆…$R;" +
                                       "$P真是的!$R;" +
                                       "$R知道了! 知道了!$R;" +
                                       "幫你做不就行了嗎?!$R;" +
                                       "$P不過，你得把那盤『咖喱』$R;" +
                                       "給我交出來才行。$R;", "解答大叔");

                TakeItem(pc, 10008900, 1);
                GiveItem(pc, 10042801, 1);
                Say(pc, 0, 0, "得到『阿高普路斯市偽造通行證』!$R;", " ");
            }
            else
            {
                Say(pc, 11000010, 131, "什麼? 那是什麼?$R;" +
                                       "我不太知道啊?$R;", "解答大叔");            
            }
        }

        void 解答大叔第一階段販賣物品(ActorPC pc)
        {
            BitMask<Acropolisut_Pass_03> Acropolisut_Pass_03_mask = new BitMask<Acropolisut_Pass_03>(pc.CMask["Acropolisut_Pass_03"]);                                  //任務：取得偽造通行證

            if (Acropolisut_Pass_03_mask.Test(Acropolisut_Pass_03.解答大叔想吃美味的咖哩))
            {
                解答大叔想吃美味的咖哩(pc);
                return;
            }

            if (pc.Level >= 10)
            {
                switch (Select(pc, "想做什麼呢?", "", "買東西", "賣東西", "談論有關『咖喱』的事情", "什麼也不做"))
                {
                    case 1:
                        OpenShopBuy(pc, 11);

                        Say(pc, 11000010, 131, "以後不要再來這樣的地方了。$R;", "解答大叔");
                        break;

                    case 2:
                        OpenShopSell(pc, 11);

                        Say(pc, 11000010, 131, "以後不要再來這樣的地方了。$R;", "解答大叔");
                        break;

                    case 3:
                        Acropolisut_Pass_03_mask.SetValue(Acropolisut_Pass_03.解答大叔想吃美味的咖哩, true);

                        Say(pc, 11000010, 131, "你也喜歡咖喱啊?$R;" +
                                               "$R看來我們有共同的語言，$R;" +
                                               "我也很喜歡咖喱!!$R;" +
                                               "$P都到了天天吃也不膩的程度，$R;" +
                                               "一提到咖喱就欲罷不能了。$R;" +
                                               "$R聽說過『美味的咖喱』嗎?$R;" +
                                               "$P聽說過是一種入口即溶的咖哩。$R;" +
                                               "$R真是令人食指大動的美味咖喱啊!$R;" +
                                               "$P真想吃一次看看啊…$R;" +
                                               "$R你不想吃吃看嗎?$R;" +
                                               "$P啊啊…$R;" +
                                               "$R啊…突然特別想吃咖喱，$R;" +
                                               "想吃得都快發瘋了…$R;" +
                                               "$P你今天就先回去吧!$R;", "解答大叔");
                        break;

                    case 4:
                        break;
                }
            }
            else 
            {
                switch (Select(pc, "想做什麼呢?", "", "買東西", "賣東西", "什麼也不做"))
                {
                    case 1:
                        OpenShopBuy(pc, 11);

                        Say(pc, 11000010, 131, "以後不要再來這樣的地方了。$R;", "解答大叔");
                        break;

                    case 2:
                        OpenShopSell(pc, 11);

                        Say(pc, 11000010, 131, "以後不要再來這樣的地方了。$R;", "解答大叔");
                        break;

                    case 3:
                        break;
                }
            }

        }

        void 解答大叔想吃美味的咖哩(ActorPC pc)
        {
            BitMask<Acropolisut_Pass_03> Acropolisut_Pass_03_mask = new BitMask<Acropolisut_Pass_03>(pc.CMask["Acropolisut_Pass_03"]);                                  //任務：取得偽造通行證

            if (Acropolisut_Pass_03_mask.Test(Acropolisut_Pass_03.向萬物博士詢問美味的咖哩的做法))
            {
                if (CountItem(pc, 10008950) > 0)
                {
                    給予解答大叔美味的咖哩(pc);
                    return;
                }

                Say(pc, 11000010, 131, "啊…是你啊…$R;" +
                                       "我沒有辦法了啊…$R;" +
                                       "$R『美味的咖喱』$R;" +
                                       "總是離不開我的腦子…$R;" +
                                       "$P讓我什麼都不能想…$R;" +
                                       "什麼事也不能做啊!!$R;" +
                                       "$R所以乾脆把事情都放在一邊了…$R;", "解答大叔");
            }
            else 
            {
                Say(pc, 11000010, 131, "…$R;", "解答大叔");
            }
        }

        void 給予解答大叔美味的咖哩(ActorPC pc)
        {
            BitMask<Acropolisut_Pass_03> Acropolisut_Pass_03_mask = new BitMask<Acropolisut_Pass_03>(pc.CMask["Acropolisut_Pass_03"]);                                  //任務：取得偽造通行證

            Acropolisut_Pass_03_mask.SetValue(Acropolisut_Pass_03.給予解答大叔美味的咖哩, true);

            Say(pc, 11000010, 131, "該不會是…?!$R;" +
                                   "$P難道…$R;" +
                                   "$P你帶來的那個難道是…$R;" +
                                   "$P怎麼會…根本不可能啊?$R;" +
                                   "$R難道我的眼睛出問題了?$R;" +
                                   "$P但是這個味道…$R;" +
                                   "$R真的是『美味的咖喱』啊!!$R;" +
                                   "$P嘎吱嘎吱…$R;", "解答大叔");

            TakeItem(pc, 10008950, 1);
            Say(pc, 0, 0, "解答大叔狼吞虎嚥的$R;" +
                          "把『美味的咖喱』吃掉…$R;");

            Say(pc, 11000010, 131, "嗚啊!太太好吃了!$R;" +
                                   "$R為了我把這個…實在太感謝了!$R;" +
                                   "對了，你叫什麼名字啊?$R;", "解答大叔");

            Say(pc, 11000010, 131, "我知道了!!$R;" +
                                   "$R嘿……$R;" +
                                   "$P" + pc.Name + "，$R;" +
                                   "我會牢牢的記住你的名字的。$R;" +
                                   "$R下次一定再來啊!!$R;", "解答大叔");
        }

        void 解答大叔第二階段販賣物品(ActorPC pc)
        {
            switch (Select(pc, "", "想做什麼呢?", "買東西", "賣東西", "販賣『偽造通行證』", "什麼也不做"))
            {
                case 1:
                    OpenShopBuy(pc, 13);

                    Say(pc, 11000010, 131, pc.Name + "下次再來玩呀!!$R;", "解答大叔");
                    break;

                case 2:
                    OpenShopSell(pc, 13);

                    Say(pc, 11000010, 131, pc.Name + "下次再來玩呀!!$R;", "解答大叔");
                    break;

                case 3:
                    OpenShopBuy(pc, 83);

                    Say(pc, 11000010, 131, "回去的路上要小心啊!$R;", "解答大叔");
                    break;

                case 4:
                    break;
            }
        }

        void 解答大叔第三階段一般販賣物品(ActorPC pc)
        {
            switch (Select(pc, "", "想做什麼呢?", "買東西", "賣東西", "販賣『偽造通行證』", "販賣『職業證明道具』", "什麼也不做"))
            {
                case 1:
                    OpenShopBuy(pc, 13);

                    Say(pc, 11000010, 131, pc.Name + "下次再來玩呀!!$R;", "解答大叔");
                    break;

                case 2:
                    OpenShopSell(pc, 13);

                    Say(pc, 11000010, 131, pc.Name + "下次再來玩呀!!$R;", "解答大叔");
                    break;

                case 3:
                    OpenShopBuy(pc, 83);

                    Say(pc, 11000010, 131, "回去的路上要小心啊!$R;", "解答大叔");
                    break;

                case 4:
                    if (pc.Fame >= 100)
                    {
                        OpenShopBuy(pc, 15);

                        Say(pc, 11000010, 131, pc.Name + "下次再來玩呀!!$R;", "解答大叔");
                    }
                    else
                    {
                        Say(pc, 11000010, 131, "這是象徵職業標誌一般的道具…$R;" +
                                               "因此只憑我的判斷是不能販賣的。$R;" +
                                               "$R須要再多多努力幫忙人們，$R;" +
                                               "得到人們的信任。$R;", "解答大叔");
                    }
                    break;

                case 5:
                    break;
            }
        }

        void 解答大叔第三階段盜賊專用販賣物品(ActorPC pc)
        {
            BitMask<Acropolisut_01> Acropolisut_01_mask = new BitMask<Acropolisut_01>(pc.CMask["Acropolisut_01"]);                                                      //一般：阿高普路斯市

            int selection;

            selection = Select(pc, "", "想做什麼呢?", "買東西", "賣東西", "販賣『偽造通行證』", "販賣『職業證明道具』", "任務服務台", "什麼也不做");

            while (selection != 6)
            {
                switch (selection)
                {
                    case 1:
                        OpenShopBuy(pc, 14);

                        Say(pc, 11000010, 131, pc.Name + "下次再來玩呀!!$R;", "解答大叔");
                        return;

                    case 2:
                        OpenShopSell(pc, 14);

                        Say(pc, 11000010, 131, pc.Name + "下次再來玩呀!!$R;", "解答大叔");
                        return;

                    case 3:
                        OpenShopBuy(pc, 83);

                        Say(pc, 11000010, 131, "回去的路上要小心啊!$R;", "解答大叔");
                        return;

                    case 4:
                        if (pc.Fame >= 100)
                        {
                            OpenShopBuy(pc, 15);

                            Say(pc, 11000010, 131, pc.Name + "下次再來玩呀!!$R;", "解答大叔");
                        }
                        else
                        {
                            Say(pc, 11000010, 131, "這是象徵職業標誌一般的道具…$R;" +
                                                   "因此只憑我的判斷是不能販賣的。$R;" +
                                                   "$R須要再多多努力幫忙人們，$R;" +
                                                   "得到人們的信任。$R;", "解答大叔");
                        }
                        return;

                    case 5:
                        if (!Acropolisut_01_mask.Test(Acropolisut_01.已經與解答大叔詢問過任務服務台))
                        {
                            Acropolisut_01_mask.SetValue(Acropolisut_01.已經與解答大叔詢問過任務服務台, true);

                            Say(pc, 11000010, 131, pc.Name + "……$R;" +
                                                   "$R你在最近幾個月內，$R;" +
                                                   "變的很強了啊?$R;" +
                                                   "$P你還是初心者的時候，$R;" +
                                                   "我就開始在注意你了。$R;" +
                                                   "$R但是沒想到會變得這麼強…$R;" +
                                                   "真是厲害啊!$R;" +
                                                   "$P你有想過在『闇黑世界』裡做事嗎?$R;" +
                                                   "$P「搬運私貨」、「暗殺重要人物」、$R;" +
                                                   "「打獵食人魔物」等等…$R;" +
                                                   "$R雖然都是危險的事情，$R;" +
                                                   "但是報酬也相對多啊。$R;" +
                                                   "$P至於要不要做就由你自己決定吧…$R;", "解答大叔");
                        }
                        else
                        {
                            HandleQuest(pc, 56);
                            return;
                        }
                        break;
                }

                selection = Select(pc, "", "想做什麼呢?", "買東西", "賣東西", "販賣『偽造通行證』", "販賣『職業證明道具』", "任務服務台", "什麼也不做");
            }
        }
    }
}