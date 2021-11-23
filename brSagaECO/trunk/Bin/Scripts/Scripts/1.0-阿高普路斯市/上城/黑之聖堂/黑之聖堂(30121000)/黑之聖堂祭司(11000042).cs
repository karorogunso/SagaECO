using System;
using System.Collections.Generic;
using System.Text;

using SagaDB.Actor;
using SagaMap.Scripting;

using SagaLib;
using SagaScript.Chinese.Enums;
//所在地圖:黑之聖堂(30121000) NPC基本信息:黑之聖堂祭司(11000042) X:8 Y:7
namespace SagaScript.M30121000
{
    public class S11000042 : Event
    {
        public S11000042()
        {
            this.EventID = 11000042;
        }

        public override void OnEvent(ActorPC pc)
        {
            BitMask<Acropolisut_01> Acropolisut_01_mask = new BitMask<Acropolisut_01>(pc.CMask["Acropolisut_01"]);
            BitMask<JobBasic_08> JobBasic_08_mask = new BitMask<JobBasic_08>(pc.CMask["JobBasic_08"]);
            BitMask<Puppet_01> Puppet_01_mask = pc.CMask["Puppet_01"];  
            BitMask<Neko_09> Neko_09_mask = new BitMask<Neko_09>(pc.CMask["Neko_09"]);
            if (CountItem(pc, 10002002) > 0)
            {
                if (Neko_09_mask.Test(Neko_09.黑暗圣杯满了))
                {
                    return;
                }
                if (Neko_09_mask.Test(Neko_09.获得灵魂碎片_01) &&
                    Neko_09_mask.Test(Neko_09.获得灵魂碎片_02) &&
                    Neko_09_mask.Test(Neko_09.获得灵魂碎片_03) &&
                    Neko_09_mask.Test(Neko_09.获得灵魂碎片_04))
                {
                    黑暗圣杯满了(pc);
                    return;
                }
            }
            if (Neko_09_mask.Test(Neko_09.綠色三角巾入手) &&
                !Neko_09_mask.Test(Neko_09.黑暗圣杯入手))
            {
            	黑暗圣杯(pc);
            	return;
            }


            Say(pc, 11000042, 131, "……$R;" +
                                   "$P這裡是黑之聖堂，$R;" +
                                   "是魔攻師行會的本部。$R;", "黑之聖堂祭司");

            if (CountItem(pc, 10047201) >= 1)
            {
                Say(pc, 131, "……$R;" +
                    "$P想給『空空的心』裡注入心??$R;" +
                    "$R呵呵呵…$R;" +
                    "要支付代價吧?$R;");
                if (Select(pc, "要付出什麼呢?", "", "攻擊１", "魔法１", "靈活１", "放棄") != 4)
                {
                    Puppet_01_mask.SetValue(Puppet_01.聖堂祭司給予洋鐵的心, true);
                    //_3A24 = true;
                    TakeItem(pc, 10047201, 1);
                    GiveItem(pc, 10047200, 1);
                    Say(pc, 131, "嗯…$R;" +
                        "既然您已經做好了準備，那就收下吧$R;");
                    PlaySound(pc, 4006, false, 100, 50);
                    Say(pc, 131, "得到了『洋鐵的心』！$R;");
                    Say(pc, 131, "我很喜歡全力以赴努力不懈的態度$R;" +
                        "所以沒有額外的要求$R;" +
                        "$R這次就算是我半買半送的吧$R;");
                }
                return;
            }

            if (JobBasic_08_mask.Test(JobBasic_08.魔攻師轉職成功) &&
                !JobBasic_08_mask.Test(JobBasic_08.已經轉職為魔攻師))
            {
                魔攻師轉職完成(pc);
                return;
            }

            if (pc.Job == PC_JOB.NOVICE && pc.Race != PC_RACE.DEM)
            {
                if (JobBasic_08_mask.Test(JobBasic_08.選擇轉職為魔攻師) &&
                    !JobBasic_08_mask.Test(JobBasic_08.已經轉職為魔攻師))
                {
                    魔攻師轉職任務(pc);
                    return;
                }
                else
                {
                    魔攻師簡介(pc);
                    return;
                }
            }

            if (pc.JobBasic == PC_JOB.WARLOCK)
            {
                Say(pc, 11000042, 131, pc.Name + "……$R;" +
                                       "……什麼事情啊?$R;", "黑之聖堂祭司");
                switch (Select(pc, "做什麼呢?", "", "任務服務台", "諾頓入國許可證販賣", "轉職", "什麼都不做"))
                {
                    case 1:
                        if (Acropolisut_01_mask.Test(Acropolisut_01.已經與黑之聖堂祭司詢問過任務服務台))
                        {
                            Acropolisut_01_mask.SetValue(Acropolisut_01.已經與黑之聖堂祭司詢問過任務服務台, true);

                            Say(pc, 11000042, 131, "……$R;" +
                                                   "$P給屬於行會的人們，$R;" +
                                                   "介紹各種的事情…$R;" +
                                                   "$P失敗後知道會怎麼樣吧?$R;" +
                                                   "$R他實在過於自負。$R;", "黑之聖堂祭司");
                        }
                        else
                        {
                            Say(pc, 11000042, 131, "……$R;" +
                                                   "已經好了…$R;", "黑之聖堂祭司");

                            HandleQuest(pc, 12);
                        }
                        break;
                    case 2:
                        Say(pc, 131, "到諾頓去？$R;");
                        break;
                    case 3:
                        進階轉職(pc);
                        OpenShopBuy(pc, 82);
                        break;
                    case 4:
                        break;
                }
                return;
            }
            Say(pc, 131, "……已經晚了……一切都太遲了…$R;");
        }
        void 黑暗圣杯满了(ActorPC pc)
        {
            BitMask<Neko_09> Neko_09_mask = new BitMask<Neko_09>(pc.CMask["Neko_09"]);
            Say(pc, 131, "……。$R;" +
            "$Pここは黒の聖堂。$R;" +
            "ウォーロックギルドの本拠地だ。$R;", "黒い聖堂司祭");

            Say(pc, 131, "……集めてきたようだな。$R;", "黒い聖堂司祭");

            Say(pc, 0, 131, "にゃん！$R;", "ネコマタ");

            Say(pc, 131, "……１つ忠告しておくことがある。$R;", "黒い聖堂司祭");

            Say(pc, 0, 131, "にゃ？$R;", "ネコマタ");

            Say(pc, 131, "……こぼれ落ちた水を$R;" +
            "完全に戻すことが出来ぬように$R;" +
            "散らばってしまった魂も$R;" +
            "完全に集めることは、……出来ぬ。$R;", "黒い聖堂司祭");

            Say(pc, 0, 131, "？？？$R;", "ネコマタ");

            Say(pc, 131, "ネコマタを呼び戻したとしても$R;" +
            "何か、を失っている可能性が高い。$R;", "黒い聖堂司祭");

            Say(pc, 0, 131, "にゃーー！？$R;", "ネコマタ");

            Say(pc, 131, "何を失っているかは$R;" +
            "私にもわからぬ。$R;" +
            "$R……ただ……。$R;", "黒い聖堂司祭");

            Say(pc, 0, 131, "にゃにゃ？$R;", "ネコマタ");

            Say(pc, 131, "……それでも、そのネコマタを$R;" +
            "大切にすると誓えるか？$R;" +
            "$P「黒の聖杯」の力で甦った魂が$R;" +
            "愛されず、悲しみに囚われれば$R;" +
            "その魂は闇と化す。$R;" +
            "$P闇となった魂は$R;" +
            "二度と風にもなれず……。$R;" +
            "$R永遠の暗闇をさまようであろう……。$R;", "黒い聖堂司祭");

            Say(pc, 0, 131, "にゃ、にゃぁ……。$R;", "ネコマタ");

            Say(pc, 131, "いいか、一度しか聞かぬ。$R;" +
            "$P何かが、欠けているネコマタでも$R;" +
            "お前は大切にすると誓えるか？$R;", "黒い聖堂司祭");
            if (Select(pc, "どうする？", "", "もちろん、大切にする！", "……できない。") == 1)
            {
                Say(pc, 131, "……その誓い、忘れるな。$R;", "黒い聖堂司祭");
                ShowEffect(pc, 5199);
                Wait(pc, 2970);
                PlaySound(pc, 2040, false, 100, 50);

                Say(pc, 0, 131, "「黒の聖杯」に闇の力が宿った！$R;", " ");

                Say(pc, 131, "闇の力が最も強まる夜。$R;" +
                "$R……その魂水を$R;" +
                "魂の砕け散った場所に注ぐのだ。$R;", "黒い聖堂司祭");

                Say(pc, 0, 131, "にゃん！$R;", "ネコマタ");

                Say(pc, 131, "さすれば、ネコマタ魂は$R;" +
                "この世に再び舞い降りよう……。$R;", "黒い聖堂司祭");
                Neko_09_mask.SetValue(Neko_09.黑暗圣杯满了, true);
            }
        }

        void 黑暗圣杯(ActorPC pc)
        {
        	BitMask<Neko_09> Neko_09_mask = new BitMask<Neko_09>(pc.CMask["Neko_09"]);
			Say(pc, 131, "……。$R;" + 
			"$Pここは黒の聖堂。$R;" + 
			"ウォーロックギルドの本拠地だ。$R;", "黒い聖堂司祭");
			
			Say(pc, 131, "……何の用だ？$R;", "黒い聖堂司祭");
			
			Say(pc, 0, 131, "にゃん、にゃん、にゃ～！$R;", "ネコマタ");
			Wait(pc, 990);
			
			Say(pc, 0, 131, "司祭に、天まで続く塔での$R;" + 
			"出来事を説明した。$R;", " ");
			
			Say(pc, 131, "……。$R;" + 
			"$P……理解した。$R;" + 
			"$R……おそらく、まだ、ネコマタ魂は$R;" + 
			"完全には消滅していないだろう。$R;" + 
			"$P……魂は$R;" + 
			"風に乗って、世界中に散らばり$R;" + 
			"少しずつ、大地や海に溶けて$R;" + 
			"個としての存在を失っていく……。$R;", "黒い聖堂司祭");
			
			Say(pc, 0, 131, "にゃ？？？$R;", "ネコマタ");
			
			Say(pc, 131, "完全に個を失ったそのとき……$R;" + 
			"魂はすべてになるのだ。$R;", "黒い聖堂司祭");
			
			Say(pc, 0, 131, "にゃ～？？？$R;", "ネコマタ");
			
			Say(pc, 131, "……いや、ネコには難しかったな。$R;", "黒い聖堂司祭");
			
			Say(pc, 0, 131, "にゃっー！！！$R;", "ネコマタ");
			
			Say(pc, 131, "……ネクロマンサーの秘術に$R;" + 
			"「黒の聖杯」の儀式というものがある。$R;" + 
			"$P「黒の聖杯」を使い$R;" + 
			"散らばった魂を集め$R;" + 
			"強制的にもとの肉体に戻し$R;" + 
			"ゾンビやスケルトンなどを作り出す$R;" + 
			"秘術なのだが……。$R;", "黒い聖堂司祭");
			
			Say(pc, 0, 131, "にゃあ！！$R;", "ネコマタ");
			
			Say(pc, 131, "……ネコマタは$R;" + 
			"もともと肉体がないからな。$R;" + 
			"$R……ゾンビになることもあるまい。$R;" + 
			"$P「黒い聖杯」儀式を行えば$R;" + 
			"甦ることが可能であろう。$R;", "黒い聖堂司祭");
			
			Say(pc, 0, 131, "にゃあ～～～♪$R;", "ネコマタ");
			
			Say(pc, 131, "黒の聖杯をそなたに授けよう。$R;" + 
			"$P「黒の聖杯」を天に掲げ祈るのだ。$R;" + 
			"$R近くに、ネコマタ魂がいれば$R;" + 
			"聖杯に魂水が注がれよう……。$R;" + 
			"$P聖杯が、魂水で$R;" + 
			"溢れそうになるまで溜まったら$R;" + 
			"また、私のもとにくるがよい……$R;", "黒い聖堂司祭");
			PlaySound(pc, 2040, false, 100, 50);
			GiveItem(pc, 10002002, 1);
			Say(pc, 0, 131, "『黒の聖杯』を入手した。$R;", " ");
			Neko_09_mask.SetValue(Neko_09.綠色三角巾入手, false);
			Neko_09_mask.SetValue(Neko_09.黑暗圣杯入手, true);
            int[] MAPID = { 10050000, 10001000, 10002000, 10063000, 10046000, 10064000, 20023000, 10062000, 10059000, 10057000, 10018000, 10019000, 10054000, 10060000, 12035000, 10028000, 10019100, 10030000, 20146000, 10058000, 11053000, 10071000, 12021000 };
            int map1 = MAPID[SagaLib.Global.Random.Next(0, MAPID.Length - 1) - 1];
            int map2 = MAPID[SagaLib.Global.Random.Next(0, MAPID.Length - 1) - 1];
            while (map1 == map2)
            {
                map2 = MAPID[SagaLib.Global.Random.Next(0, MAPID.Length - 1) - 1];
            }
            pc.CInt["NEKO_09_MAPs_01"] = map1;
            pc.CInt["NEKO_09_MAPs_02"] = map2;

        }
        
        void 魔攻師簡介(ActorPC pc)
        {
            BitMask<JobBasic_08> JobBasic_08_mask = new BitMask<JobBasic_08>(pc.CMask["JobBasic_08"]);

            int selection;

            Say(pc, 11000042, 131, "我是…$R;" +
                                   "$R管理魔攻師行會的魔攻師總管。$R;" +
                                   "$P您是初心者吧?$R;", "黑之聖堂祭司");

            selection = Select(pc, "想做什麼?", "", "我想成為『魔攻師』!", "『魔攻師』是什麼樣的職業?", "任務服務台", "什麼也不做");

            while (selection != 4)
            {

                switch (selection)
                {
                    case 1:
                        JobBasic_08_mask.SetValue(JobBasic_08.選擇轉職為魔攻師, true);
                        //廢除一次職轉職任務
                        JobBasic_08_mask.SetValue(JobBasic_08.已經從闇之精靈那裡把心染為黑暗, true);
                        JobBasic_08_mask.SetValue(JobBasic_08.已經從黑佰特那裡聽取有關黑暗魔法的知識, true);
                        JobBasic_08_mask.SetValue(JobBasic_08.魔攻師轉職任務完成, true);
                        申請轉職為魔攻師(pc);
                        /*
                        if (pc.Race == PC_RACE.TITANIA)
                        {
                            if (!JobBasic_08_mask.Test(JobBasic_08.已經從闇之精靈那裡把心染為黑暗))
                            {
                                Say(pc, 11000042, 131, "想成為合格的魔攻師，$R;" +
                                                       "必須在內心裡蘊藏著「闇屬性」。$R;" +
                                                       "$P去求見存在這世界上，$R;" +
                                                       "某個角落的「闇之精靈」，$R;" +
                                                       "賜予您「闇屬性」之後，$R;" +
                                                       "再來找我吧…$R;", "黑之聖堂祭司");
                            }
                            else
                            {
                                Say(pc, 11000042, 131, "……$R;" +
                                                       "$P您明白了嗎?$R;", "黑之聖堂祭司");

                                Say(pc, 11000042, 131, "這個城市的「下城」裡，$R;" +
                                                       "有個熟悉「黑暗魔法」的人。$R;" +
                                                       "$P他的名字叫「黑佰特」。$R;" +
                                                       "$R先去見過那個傢伙後，$R;" +
                                                       "再回來找我吧!$R;", "黑之聖堂祭司");
                            }
                        }
                        else
                        {
                            Say(pc, 11000042, 131, "這個城市的「下城」裡，$R;" +
                                                   "有個熟悉「黑暗魔法」的人。$R;" +
                                                   "$P他的名字叫「黑佰特」。$R;" +
                                                   "$R先去見過那個傢伙後，$R;" +
                                                   "再回來找我吧!$R;", "黑之聖堂祭司");                      
                        }
                        return;
                        */
                        break;

                    case 2:
                        Say(pc, 11000042, 131, "……$R;" +
                                               "$P我們是屬於黑暗的魔法師…$R;" +
                                               "$R魔攻師比較適合塔妮亞或道米尼，$R;" +
                                               "這種魔力較高的種族。$R;" +
                                               "$P特別是道米尼種族，最佔優勢!$R;" +
                                               "因為他們先天上就具有「闇屬性」…$R;" +
                                               "$R所以魔攻師中道米尼種族比較多…$R;" +
                                               "$P相反的，道米尼以外的種族，$R;" +
                                               "要成為魔攻師，可是吃了不少苦呢!$R;" +
                                               "$R您明白了嗎?$R;", "黑之聖堂祭司");

                        switch (Select(pc, "還有疑問嗎?", "", "能再詳細的說明一次嗎?", "我知道了"))
                        {
                            case 1:
                                Say(pc, 11000042, 131, "『魔攻師』是使用「黑暗魔法」的職業。$R;" +
                                                       "$P在「黑暗魔法」中，攻擊性的魔法比較多。$R;" +
                                                       "$R在遠距離使用「黑暗魔法」的話，$R;" +
                                                       "您可以在不受傷的情況下，$R;" +
                                                       "輕鬆的取得勝利…$R;" +
                                                       "$P同時魔攻師也是靈活多變的職業…$R;" +
                                                       "$R只要您喜歡，也可以拿起手中的武器$R;" +
                                                       "與敵人近距離對戰來個親密接觸…$R;" +
                                                       "$P如果魔攻師選擇提升自己的力量，$R;" +
                                                       "即使是不死系的魔物們都要退避三舍。$R;", "黑之聖堂祭司");
                                break;
                                
                            case 2:
                                break;
                        }
                        break;

                    case 3:
                        Say(pc, 11000042, 131, "……$R;" +
                                               "$P這裡只替『魔攻師』介紹任務…$R;", "黑之聖堂祭司");
                        break;
                }

                selection = Select(pc, "想做什麼?", "", "我想成為『魔攻師』!", "『魔攻師』是什麼樣的職業?", "任務服務台", "什麼也不做");
            } 
        }

        void 魔攻師轉職任務(ActorPC pc)
        {
            BitMask<JobBasic_08> JobBasic_08_mask = new BitMask<JobBasic_08>(pc.CMask["JobBasic_08"]);

            if (!JobBasic_08_mask.Test(JobBasic_08.魔攻師轉職任務完成))
            {
                黑暗魔法相關問題回答(pc);
            }

            if (JobBasic_08_mask.Test(JobBasic_08.魔攻師轉職任務完成) &&
                !JobBasic_08_mask.Test(JobBasic_08.魔攻師轉職成功))
            {
                申請轉職為魔攻師(pc);
                return;
            }
        }

        void 黑暗魔法相關問題回答(ActorPC pc)
        {
            BitMask<JobBasic_08> JobBasic_08_mask = new BitMask<JobBasic_08>(pc.CMask["JobBasic_08"]);

            if (JobBasic_08_mask.Test(JobBasic_08.已經從黑佰特那裡聽取有關黑暗魔法的知識))
            {
                問題01(pc);
            }
            else
            {
                Say(pc, 11000042, 131, "這個城市的「下城」裡，$R;" +
                                       "有個熟悉黑暗知識的人。$R;" +
                                       "$P他的名字叫「黑佰特」。$R;" +
                                       "$R先去見過那個傢伙後，$R;" +
                                       "再回來找我吧!$R;" +
                                       "$P不要讓我重複講好幾次…$R;", "黑之聖堂祭司");
            }
        }

        void 問題01(ActorPC pc)
        {
            Say(pc, 11000042, 131, "…$R;" +
                                   "$R好像是聽到消息之後才來的啊…$R;" +
                                   "$R那麼給您進行一個小小的測驗吧!$R;" +
                                   "請您回答我所提出的問題。$R;" +
                                   "$P「闇屬性」的相對屬性是什麼?$R;", "黑之聖堂祭司");

            switch (Select(pc, "「闇屬性」的相對屬性是什麼?", "", "光屬性", "聖屬性", "沒有"))
            {
                case 1:
                    問題回答錯誤(pc);
                    return;
                    
                case 2:
                    問題回答錯誤(pc);
                    return;

                case 3:
                    問題02(pc);
                    break;
            }
        }

        void 問題02(ActorPC pc)
        {
            PlaySound(pc, 2040, false, 100, 50);

            Say(pc, 11000042, 131, "是啊…$R;" +
                                   "$R看起來所有屬性都像是有相對關係的，$R;" +
                                   "但是實際上不是那樣…,;" +
                                   "$P請您回答我所提出的問題。$R;" +
                                   "$R跟『闇靈之力』不相通的屬性是?$R;", "黑之聖堂祭司");

            switch (Select(pc, "跟『闇靈之力』不相通的屬性是?", "", "光屬性", "水屬性", "闇屬性"))
            {
                case 1:
                    問題03(pc);
                    break;
                    
                case 2:
                    問題回答錯誤(pc);
                    return;

                case 3:
                    問題回答錯誤(pc);
                    return;
            }
        }

        void 問題03(ActorPC pc)
        {
            PlaySound(pc, 2040, false, 100, 50);

            Say(pc, 11000042, 131, "答對了! 那是很常用的魔法…$R;" +
                                   "要好好的記下來喔!$R;" +
                                   "$P那下一個問題是…;" +
                                   "魔攻師可以使用的裝備是?$R;", "黑之聖堂祭司");

            switch (Select(pc, "魔攻師可以使用的裝備是?", "", "短劍", "斧頭", "槍"))
            {
                case 1:
                    問題回答正確(pc);
                    break;
                    
                case 2:
                    問題回答錯誤(pc);
                    return;

                case 3:
                    問題回答錯誤(pc);
                    return;
            }
        }

        void 問題回答正確(ActorPC pc)
        {
            BitMask<JobBasic_08> JobBasic_08_mask = new BitMask<JobBasic_08>(pc.CMask["JobBasic_08"]);

            PlaySound(pc, 2040, false, 100, 50);

            Say(pc, 11000042, 131, "正確答案…$R;" +
                                   "$P嗯…好像情報蒐集得還不錯。$R;" +
                                   "$P好吧! 就讓您加入魔攻師的行列…;" +
                                   "$R真的要成為『魔攻師』嗎?$R;", "黑之聖堂祭司");

            switch (Select(pc, "要轉職為『魔攻師』嗎?", "", "轉職為『魔攻師』", "還是算了吧"))
            {
                case 1:
                    JobBasic_08_mask.SetValue(JobBasic_08.魔攻師轉職任務完成, true);
                    break;
                    
                case 2:
                    break;
            }
        }

        void 問題回答錯誤(ActorPC pc)
        {
            BitMask<JobBasic_08> JobBasic_08_mask = new BitMask<JobBasic_08>(pc.CMask["JobBasic_08"]);

            JobBasic_08_mask.SetValue(JobBasic_08.已經從黑佰特那裡聽取有關黑暗魔法的知識, false);

            PlaySound(pc, 2041, false, 100, 50);

            Say(pc, 11000042, 131, "……$R;" +
                                   "$R再回去找「黑佰特」，$R;" +
                                   "更加瞭解後再回來吧!$R;", "黑之聖堂祭司");
        }

        void 申請轉職為魔攻師(ActorPC pc)
        {
            BitMask<JobBasic_08> JobBasic_08_mask = new BitMask<JobBasic_08>(pc.CMask["JobBasic_08"]);

            Say(pc, 11000042, 131, "這是象徵『魔攻師』的『魔攻師紋章』，$R;" +
                                   "請您好好保管!$R;", "黑之聖堂祭司");

            if (pc.Inventory.Equipments.Count == 0)
            {
                JobBasic_08_mask.SetValue(JobBasic_08.魔攻師轉職成功, true);

                PlaySound(pc, 3087, false, 100, 50);
                ShowEffect(pc, 4131);
                Wait(pc, 3960);

                Say(pc, 11000042, 131, "……$R;" +
                                       "$P……好了，$R;" +
                                       "您已經成為了『魔攻師』。$R;" +
                                       "呵呵呵…$R;", "黑之聖堂祭司");

                PlaySound(pc, 4012, false, 100, 50);
                ChangePlayerJob(pc, PC_JOB.WARLOCK);

                Say(pc, 0, 0, "您已經轉職為『魔攻師』了!$R;", " ");

                Say(pc, 11000042, 131, "$P給您成為黑暗夥伴的象徵。$R;" +
                                       "$R先穿衣服吧…$R;", "黑之聖堂祭司");
            }
            else
            {
                Say(pc, 11000042, 131, "紋章是烙印在皮膚上的，$R;" +
                                       "先把裝備脫掉吧。$R;", "黑之聖堂祭司");
            }
        }

        void 魔攻師轉職完成(ActorPC pc)
        {
            BitMask<JobBasic_08> JobBasic_08_mask = new BitMask<JobBasic_08>(pc.CMask["JobBasic_08"]);

            if (pc.Inventory.Equipments.Count != 0)
            {

                JobBasic_08_mask.SetValue(JobBasic_08.已經轉職為魔攻師, true);

                Say(pc, 11000042, 131, "給您成為黑暗夥伴的證據吧…$R;", "黑之聖堂祭司");

                PlaySound(pc, 2040, false, 100, 50);
                GiveItem(pc, 50050600, 1);
                Say(pc, 0, 0, "得到『黑暗胸針』!$R;", " ");

                LearnSkill(pc, 3083);
                Say(pc, 0, 0, "學到『闇靈之力』!R;", " ");

                Say(pc, 11000042, 131, "已經無法回頭了…$R;" +
                                       "$R呵呵呵…$R;", "黑之聖堂祭司");
            }
            else
            {
                Say(pc, 11000042, 131, "請先穿上衣服。$R;", "黑之聖堂祭司");
            }
        }

        void 進階轉職(ActorPC pc)
        {
            BitMask<Job2X_08> mask = pc.CMask["Job2X_08"];
            if (mask.Test(Job2X_08.轉職結束))//_3A40)
            {
                if (pc.Inventory.Equipments.Count == 0)
                {
                    Say(pc, 131, "……$R;" +
                        "$P穿衣服吧…$R;");
                    return;
                }
                Say(pc, 131, "……$R;" +
                    "$P對您還是較困難$R;");
                return;
            }
            if (pc.Job == PC_JOB.WARLOCK && pc.JobLevel1 > 29)
            {
                if (mask.Test(Job2X_08.轉職開始))//_3A39)
                {
                    if (CountItem(pc, 10034000) >= 1 && CountItem(pc, 10018209) >= 1)
                    {
                        Say(pc, 131, "……$R;" +
                            "$P嗯…$R;" +
                            "$P好啊…$R;" +
                            "認定您為『暗黑神官』吧!$R;" +
                            "$R真的要成為暗黑神官嗎?$R;");
                        int a = 0;
                        while (a == 0)
                        {
                            switch (Select(pc, "要轉職嗎?", "", "要成為暗黑神官!", "轉職時的注意事項", "不要"))
                            {
                                case 1:
                                    Say(pc, 131, "給您象徵『暗黑神官』的$R;" +
                                        "『暗黑神官紋章』$R;");
                                    if (pc.Inventory.Equipments.Count == 0)
                                    {
                                        switch (Select(pc, "要轉職嗎?", "", "要成為暗黑神官!", "不要"))
                                        {
                                            case 1:
                                                TakeItem(pc, 10034000, 1);
                                                TakeItem(pc, 10018209, 1);
                                                pc.JEXP = 0;
                                                mask.SetValue(Job2X_08.轉職結束, true);
                                                //_3A40 = true;
                                                ChangePlayerJob(pc, PC_JOB.CABALIST);
                                                //PARAM ME.JOB = 73
                                                PlaySound(pc, 3087, false, 100, 50);
                                                ShowEffect(pc, 4131);
                                                Wait(pc, 4000);
                                                Say(pc, 131, "……$R;" +
                                                    "$P……好了$R;" +
                                                    "您已經是『暗黑神官』了!$R;" +
                                                    "$R呵呵呵…$R;");
                                                PlaySound(pc, 4012, false, 100, 50);
                                                Say(pc, 131, "轉職成『暗黑神官』!$R;");
                                                break;
                                            case 2:
                                                Say(pc, 131, "…嚇著了?$R呵呵呵…$R;");
                                                break;
                                        }
                                        return;
                                    }
                                    Say(pc, 131, "紋章會烙印在皮膚上的$R;" +
                                        "把裝備脫掉後再來吧$R;");
                                    return;
                                case 2:
                                    Say(pc, 131, "轉職到『暗黑神官』的話$R;" +
                                        "LV會成為1…$R;" +
                                        "$R但是轉職的之後擁有的$R;" +
                                        "技能和技能點數是不會變的$R;" +
                                        "$P而且…轉職之前沒有練熟的技能$R;" +
                                        "一旦轉職了就無法練下去$R;" +
                                        "$R比如說，職業LV30時轉職的話$R;" +
                                        "本來的職業LV30以上的技能$R;" +
                                        "轉職後就無法在練下去了…$R;" +
                                        "$P好好想想吧…$R;");
                                    break;
                                case 3:
                                    Say(pc, 131, "…嚇著了?$R呵呵呵…$R;");
                                    return;
                            }
                        }
                        return;
                    }
                    Say(pc, 131, "向黑暗奉獻的供品$R;" +
                        "『玉桂罐頭』和『黑暗羽毛翅膀』$R;" +
                        "都準備好了！$R;" +
                        "$P那樣的話，就把您認定為$R;" +
                        "『暗黑神官』吧…$R;");
                    return;
                }
                if (pc.Inventory.Equipments.Count == 0)
                {
                    Say(pc, 131, "……$R;" +
                        "$P穿衣服吧…$R;");
                    return;
                }
                Say(pc, 131, "……$R;" +
                    "$P您的黑暗也…相當的深了…$R;");
                Say(pc, 131, "向黑暗奉獻的供品$R;" +
                    "『玉桂罐頭』和『黑暗羽毛翅膀』$R;" +
                    "都準備好了！$R;" +
                    "$P那樣的話，就把您認定為$R;" +
                    "『暗黑神官』吧…$R;");
                mask.SetValue(Job2X_08.轉職開始, true);
                //_3A39 = true;
                return;
            }
            Say(pc, 131, "……$R;" +
                "$P對您還是較困難$R;");
        }
    }
}
