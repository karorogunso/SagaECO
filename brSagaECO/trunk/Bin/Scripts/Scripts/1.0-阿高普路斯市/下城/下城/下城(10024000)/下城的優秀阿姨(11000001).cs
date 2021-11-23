using System;
using System.Collections.Generic;
using System.Text;

using SagaDB.Actor;
using SagaMap.Scripting;

using SagaLib;
using SagaScript.Chinese.Enums;

using SagaDB.Item;
using SagaDB.Quests;
//所在地圖:下城(10024000) NPC基本信息:下城的優秀阿姨(11000001) X:131 Y:151
namespace SagaScript.M10024000
{
    public class S11000001 : Event
    {
        public S11000001()
        {
            this.EventID = 11000001;

            //任務服務台相關設定
            this.leastQuestPoint = 2;

            this.gotNormalQuest = "把垃圾扔進垃圾桶的話，$R;" +
                                  "數量就會自動計算。$R;" +
                                  "$R如果拿不動的話，就分幾次扔吧!$R;" +
                                  "那拜託您了!!$R;";

            this.questCompleted = "真是辛苦了，$R;" +
                                  "$R過來拿報酬吧!$R;";

            this.questCanceled = "真的是令我感到很失望啊!$R;";

            this.questFailed = "任務失敗了嗎?$R;";

            this.notEnoughQuestPoint = "看來現在不能接受任務，$R;" +
                                       "$R下次再來吧!$R;";
        }

        public override void OnEvent(ActorPC pc)
        {
            BitMask<Halloween_00> Halloween_00_mask = pc.CMask["Halloween_00"];                                                                                         //活動：萬聖節
            BitMask<Acropolisut_01> Acropolisut_01_mask = pc.CMask["Acropolisut_01"];                                                                                   //一般：阿高普路斯市
            BitMask<Acropolisut_Pass_02> Acropolisut_Pass_02_mask = pc.CMask["Acropolisut_Pass_02"];                                                                    //任務：取得上城通行證
            BitMask<Acropolisut_Pass_03> Acropolisut_Pass_03_mask = pc.CMask["Acropolisut_Pass_03"];                                                                    //任務：取得偽造通行證
            BitMask<Neko_03> Neko_03_amask = pc.AMask["Neko_03"];
            BitMask<Neko_03> Neko_03_cmask = pc.CMask["Neko_03"];                                                                                                        //任務：凱堤(堇子)
            BitMask<Knights> Knights_mask = pc.CMask["Knights"];
            BitMask<Neko_05> Neko_05_amask = pc.AMask["Neko_05"];                                                                                                      //任務：凱堤(茜子)
            BitMask<Neko_05> Neko_05_cmask = pc.CMask["Neko_05"];

            int selection;

            ////#3姉妹イベント
            //if(_2b62 && !_2b63 )
            //{
            //    Say(pc, 131, "嗯？什麼事啊？$R;" +
            //        "$R想了解古魯杜先生？$R;");
            //    //SWITCH START
            //    //ISTRANCEHOST EVT1000109901,
            //    //SWITCH END
            //    Say(pc, 131, "噓~！不能在這裡說，跟我來吧？$R;");
            //    //EVENTMAP_IN 26 1 3 6 4
            //    //SWITCH START
            //    //ME.WORK0 = -1 EVT1100000140a
            //    //SWITCH END
            //}

            ////EVT1100000140a
            //Say(pc, 131, "啊…可以等我一下嗎？$R;" +
            //    "現在人太多了，別人看到就麻煩了$R;" +
            //    "$R周圍沒人的時候，再來找我可以嗎？$R;");

            Halloween_00_mask.SetValue(Halloween_00.萬聖節活動期間, false);                                                                                             //萬聖節活動期間                開/關

            if (Halloween_00_mask.Test(Halloween_00.萬聖節活動期間))
            {
                萬聖節(pc);
            }

            if (!Acropolisut_01_mask.Test(Acropolisut_01.已經與下城的優秀阿姨進行第一次對話))
            {
                初次與下城的優秀阿姨進行對話(pc);
                return;
            }

            if (Acropolisut_01_mask.Test(Acropolisut_01.向萬物博士詢問進去上城的方法) &&
                !Knights_mask.Test(Knights.已經加入騎士團) &&
                !Knights_mask.Test(Knights.取得上城通行證))
            {
                取得上城通行證(pc);
                return;
            }

            if (CountItem(pc, 10048011) > 0)
            {
                行會評議會的禮物(pc);
                return;
            }

            if (!Neko_03_amask.Test(Neko_03.堇子任務開始) && 
                !Neko_03_amask.Test(Neko_03.堇子任務完成) &&
                !Neko_03_cmask.Test(Neko_03.接受下城的優秀阿姨給予的任務) &&
                pc.Level >= 30 &&
                pc.Fame >= 20)
            {
                堇子任務(pc);
                return;
            }

            if (Neko_05_amask.Test(Neko_05.茜子任务开始) &&
                Neko_05_cmask.Test(Neko_05.尋找瑪莎的蹤跡) &&
                !Neko_05_cmask.Test(Neko_05.向瑪莎詢問飛空庭引擎的相關情報))
            {
                茜子任務(pc);
                return;
            }

            if (pc.Fame > 0)
            {
                下城的優秀阿姨給予特別獎勵(pc);
            }

            Say(pc, 11000001, 131, "又是你啊?$R;" +
                                   "$R找我有什麼事情嗎?$R;", "下城的優秀阿姨");

            selection = Select(pc, "想要做什麼呢?", "", "清潔計劃", "什麼也不做");

            while (selection != 2)
            {
                if (!Acropolisut_01_mask.Test(Acropolisut_01.已經接受過清潔任務))
                {
                    Acropolisut_01_mask.SetValue(Acropolisut_01.已經接受過清潔任務, true);

                    Say(pc, 11000001, 131, "想要參加「清潔計劃」嘛?$R;" +
                                           "真的是很感謝啊!!$R;" +
                                           "$P這個計劃是把路邊的垃圾，$R;" +
                                           "主動的撿起來扔進垃圾桶裡的活動。$R;" +
                                           "$R目的是要把世界弄乾淨。$R;" +
                                           "$P那魔就開始撿垃圾吧!!$R;", "下城的優秀阿姨");
                }
                else
                {
                    Say(pc, 11000001, 131, "這個任務需要任務點數『2』。$R;", "下城的優秀阿姨");

                    HandleQuest(pc, 46);
                    return;
                }

                selection = Select(pc, "想要做什麼呢?", "", "清潔計劃", "什麼也不做");
            }
        }

        void 萬聖節(ActorPC pc)
        {
            if (pc.Inventory.Equipments.ContainsKey(EnumEquipSlot.HEAD))
            {
                if (pc.Inventory.Equipments[EnumEquipSlot.HEAD].ItemID == 50025800 ||
                    pc.Inventory.Equipments[EnumEquipSlot.HEAD].ItemID == 50024350 ||
                    pc.Inventory.Equipments[EnumEquipSlot.HEAD].ItemID == 50024351 ||
                    pc.Inventory.Equipments[EnumEquipSlot.HEAD].ItemID == 50024352 ||
                    pc.Inventory.Equipments[EnumEquipSlot.HEAD].ItemID == 50024353 ||
                    pc.Inventory.Equipments[EnumEquipSlot.HEAD].ItemID == 50024354 ||
                    pc.Inventory.Equipments[EnumEquipSlot.HEAD].ItemID == 50024355 ||
                    pc.Inventory.Equipments[EnumEquipSlot.HEAD].ItemID == 50024356 ||
                    pc.Inventory.Equipments[EnumEquipSlot.HEAD].ItemID == 50024357 ||
                    pc.Inventory.Equipments[EnumEquipSlot.HEAD].ItemID == 50024358 ||
                    pc.Inventory.Equipments[EnumEquipSlot.HEAD].ItemID == 50022500 ||
                    pc.Inventory.Equipments[EnumEquipSlot.HEAD].ItemID == 50022600 ||
                    pc.Inventory.Equipments[EnumEquipSlot.HEAD].ItemID == 50022700 ||
                    pc.Inventory.Equipments[EnumEquipSlot.HEAD].ItemID == 50022800)
                {
                    Say(pc, 11000001, 131, "哎呀! 嚇我一跳!!$R;" +
                                           "原來不是鬼怪啊。$R;", "下城的優秀阿姨");
                }
            }
        }

        void 初次與下城的優秀阿姨進行對話(ActorPC pc)
        {
            BitMask<Acropolisut_01> Acropolisut_01_mask = new BitMask<Acropolisut_01>(pc.CMask["Acropolisut_01"]);                                                      //一般：阿高普路斯市

            Acropolisut_01_mask.SetValue(Acropolisut_01.已經與下城的優秀阿姨進行第一次對話, true);

            Say(pc, 11000001, 131, "真是勇敢的孩子，$R;" +
                                   "路上小心喔!$R;", "下城的優秀阿姨");
        }

        void 取得上城通行證(ActorPC pc)
        {
            BitMask<Acropolisut_Pass_02> Acropolisut_Pass_02_mask = new BitMask<Acropolisut_Pass_02>(pc.CMask["Acropolisut_Pass_02"]);                                  //任務：取得上城通行證

            BitMask<Knights> Knights_mask = pc.CMask["Knights"];  

            Say(pc, 11000001, 131, "……$R;" +
                                   "$P在哪聽說過跟我有關的消息吧。$R;" +
                                   "$R我的名字是「路藍」。$R;" +
                                   "$P現在擔任治理「阿高普路斯市」的$R;" +
                                   "『行會評議會』主席一職。$R;" +
                                   "$R想也知道要進「上城」，$R;" +
                                   "是需要『通行證』的吧?$R;", "下城的優秀阿姨");

            if (pc.Fame > 0)
            {
                Acropolisut_Pass_02_mask.SetValue(Acropolisut_Pass_02.下城的優秀阿姨給予上城通行證, true);
                Knights_mask.SetValue(Knights.取得上城通行證, true);
                Say(pc, 11000001, 131, "……$R;" +
                                       "$P有關你的事情，$R;" +
                                       "我從咖啡館老闆$R;" +
                                       "「菲利普」那裡聽過了。$R;" +
                                       "$P可靠而且精神充沛。$R;" +
                                       "$R聽說是最近加入的新冒險者中，$R;" +
                                       "最有潛質的啊!$R;" +
                                       "$P得到城市的人信任是很重要的。$R;" +
                                       "$P「阿高普路斯市」$R;" +
                                       "如果沒有像你這樣的冒險者，$R;" +
                                       "就不能存在了。$R;" +
                                       "$R雖然給您『通行證』也不成問題…$R;" +
                                       "$P但是有一個條件，$R;" +
                                       "就是絕對不可以在那裡打架。$R;" +
                                       "$R這一點一定要做好，知道了吧?$R;" +
                                       "$R要保管好『阿高普路斯市通行證』唷$!R;", "下城的優秀阿姨");

                PlaySound(pc, 4006, false, 100, 50);
                GiveItem(pc, 10042800, 1);
                Say(pc, 0, 65535, "得到『阿高普路斯市通行證』!$R;", " ");
            }
            else
            {
                Say(pc, 11000001, 131, "……$R;" +
                                       "$P您是第一次來這個城市吧?$R;" +
                                       "$R「阿高普路斯市」需要像您$R;" +
                                       "這樣的冒險者才能存活的喔。$R;" +
                                       "$P…但是現在我對您還不太瞭解，$R;" +
                                       "所以還不能信賴您啊!!$R;", "下城的優秀阿姨");
            }
        }

        void 下城的優秀阿姨給予特別獎勵(ActorPC pc)
        {
            BitMask<Acropolisut_01> Acropolisut_01_mask = new BitMask<Acropolisut_01>(pc.CMask["Acropolisut_01"]);                                                      //一般：阿高普路斯市
            BitMask<LV35_Clothes_01> LV35_Clothes_01_mask = new BitMask<LV35_Clothes_01>(pc.CMask["LV35_Clothes_01"]);                                                  //任務：見習鐵匠阿魯斯的委託
            BitMask<LV35_Clothes_02> LV35_Clothes_02_mask = new BitMask<LV35_Clothes_02>(pc.CMask["LV35_Clothes_02"]);                                                  //任務：獨奏的委託
            BitMask<LV35_Clothes_03> LV35_Clothes_03_mask = new BitMask<LV35_Clothes_03>(pc.CMask["LV35_Clothes_03"]);                                                  //任務：撒帕涅的委託
            BitMask<LV35_Clothes_04> LV35_Clothes_04_mask = new BitMask<LV35_Clothes_04>(pc.CMask["LV35_Clothes_04"]);                                                  //任務：熊太郎的委託

            if (!Acropolisut_01_mask.Test(Acropolisut_01.再次跟下城的優秀阿姨問候) &&
                pc.Fame > 3)
            {
                Acropolisut_01_mask.SetValue(Acropolisut_01.再次跟下城的優秀阿姨問候, true);

                Say(pc, 11000001, 131, "噢噢! 是新的冒險者啊!$R;" +
                                       "對這個地方熟悉了嗎?$R;" +
                                       "$R偶爾來我的地方玩吧!!$R;", "下城的優秀阿姨");
                return;
            }

            if (!Acropolisut_01_mask.Test(Acropolisut_01.下城的優秀阿姨給予銅徽章) &&
                pc.Fame >= 10)
            {
                Acropolisut_01_mask.SetValue(Acropolisut_01.下城的優秀阿姨給予銅徽章, true);

                Say(pc, 11000001, 131, "咿呀…變得很大義凜然啊!$R;" +
                                       "$R現在對這個地方也完全熟悉了吧?$R;" +
                                       "$P這是我給您的禮物。$R;", "下城的優秀阿姨");

                PlaySound(pc, 4006, false, 100, 50);
                GiveItem(pc, 10009500, 1);
                Say(pc, 0, 65535, "得到『銅徽章』!$R;", " ");

                Say(pc, 11000001, 131, "這是因為您為城市的付出，$R;" +
                                       "所以送給您的禮物。$R;" +
                                       "$R以後也要經常過來啊!$R;", "下城的優秀阿姨");
                return;
            }

            if (LV35_Clothes_01_mask.Test(LV35_Clothes_01.見習鐵匠阿魯斯的委託完成) &&
                LV35_Clothes_02_mask.Test(LV35_Clothes_02.獨奏的委託完成) &&
                LV35_Clothes_03_mask.Test(LV35_Clothes_03.撒帕涅的委託完成) &&
                LV35_Clothes_04_mask.Test(LV35_Clothes_04.熊太郎的委託完成) &&
                !Acropolisut_01_mask.Test(Acropolisut_01.下城的優秀阿姨給予技能點))
            {
                Acropolisut_01_mask.SetValue(Acropolisut_01.下城的優秀阿姨給予技能點, true);

                Say(pc, 11000001, 131, "聽說人們有困難的時候，$R;" +
                                       "您幫助了他們?$R;" +
                                       "$R真是感謝啊!!$R;" +
                                       "$P這是我給您的禮物。$R;", "下城的優秀阿姨");

                技能點數上升(pc);
                return;
            }
        }

        void 技能點數上升(ActorPC pc)
        {
            Wait(pc, 1000);

            PlaySound(pc, 3087, false, 100, 50);
            ShowEffect(pc, 4131);
            Wait(pc, 1000);

            SkillPointBonus(pc, 1);

            Say(pc, 0, 65535, "技能點數上升『1』!$R;", " ");
        }

        void 行會評議會的禮物(ActorPC pc)
        {
            BitMask<Hearts_Gift> Hearts_Gift_mask = new BitMask<Hearts_Gift>(pc.CMask["Hearts_Gift"]);                                                                  //任務：行會評議會的禮物

            if (!Hearts_Gift_mask.Test(Hearts_Gift.向下城的優秀阿姨詢問心靈之票))
            {
                Hearts_Gift_mask.SetValue(Hearts_Gift.向下城的優秀阿姨詢問心靈之票, true);

                Say(pc, 11000001, 131, "你擁有『心靈之票』啊?$R;" +
                                       "$R看你擁有那個，應該是對初心者吧?$R;" +
                                       "…很親切吧?$R;" +
                                       "$P不用謙虛的啊!$R;" +
                                       "你的行為，真的很親切啊!!$R;" +
                                       "$P對啊……$R;" +
                                       "收集到5張『心靈之票』，$R;" +
                                       "拿到「行會評議會」去的話，$R;" +
                                       "他們會送你一份禮物來感謝您喔。$R;", "下城的優秀阿姨");
            }

            if (CountItem(pc, 10048011) >= 5)
            {
                悅換行會評議會的禮物(pc);
            }
        }

        void 悅換行會評議會的禮物(ActorPC pc)
        {
            if (CheckInventory(pc, 10049055, 1))
            {
                TakeItem(pc, 10048011, 5);

                Say(pc, 11000001, 131, "…$R;" +
                                       "$P已經有5張『心靈之票』了啊?$R;" +
                                       "那麼收下這個禮物吧!$R;", "下城的優秀阿姨");

                PlaySound(pc, 2040, false, 100, 50);
                GiveItem(pc, 10049055, 1);
                Say(pc, 0, 65535, "得到『行會評議會送來的禮物』!$R;", "下城的優秀阿姨");

                Say(pc, 11000001, 131, "以後您也是冒險者前輩，$R;" +
                                       "記得要幫助初心者喔!$R;", "下城的優秀阿姨");
            }
            else
            {
                Say(pc, 11000001, 131, "…$R;" +
                                       "$P已經有5張『心靈之票』了嗎?$R;" +
                                       "$R馬上給你禮物，$R;" +
                                       "那可以請您減輕行李重量嗎?$R;", "下城的優秀阿姨");
            }
        }

        void 堇子任務(ActorPC pc)
        {
            BitMask<Neko_03> Neko_03_amask = pc.AMask["Neko_03"];
            BitMask<Neko_03> Neko_03_cmask = pc.CMask["Neko_03"];                                                                                  //任務：凱堤(堇子)

            Say(pc, 11000001, 131, "您來的正好啊!$R;" +
                                   "$R有事想拜託您，可以幫我一個忙嗎?$R;" +
                                   "$P其實是想請您幫我找一個人。$R;" +
                                   "$R他是坐著從「摩根島」到「阿高普路斯市」的$R;" +
                                   "貨物艇的偷渡者…$R;" +
                                   "$R『混城騎士團』好像在追捕他?$R;" +
                                   "$P當然用貨物艇偷渡是犯法的，$R;" +
                                   "得支付罰款和運費才行…$R;" +
                                   "$P但是正規軍這樣堅韌不拔的追捕，$R;" +
                                   "這可是第一次呢!!$R;" +
                                   "$P『評議會』覺得那個偷渡者在貨物艇裡，$R;" +
                                   "看到了『混城騎士團』想要隱藏的一些東西。$R;" +
                                   "$P對了，偷渡者是一名小男孩，$R;" +
                                   "聽說往「奧克魯尼亞東部平原」的方向逃走了。$R;" +
                                   "$P在那孩子被『混城騎士團』抓到之前，$R;" +
                                   "去把他找出來，帶到這邊可以嗎?$R;", "下城的優秀阿姨");

            switch (Select(pc, "怎麼辦呢?", "", "找找看", "不找"))
            {
                case 1:
                    Neko_03_amask.SetValue(Neko_03.堇子任務開始, true);
                    Neko_03_cmask.SetValue(Neko_03.接受下城的優秀阿姨給予的任務, true);

                    Say(pc, 11000001, 131, "謝謝!!$R;" +
                                           "$R那就拜託您了。$R;", "下城的優秀阿姨");
                    break;

                case 2:
                    Say(pc, 11000001, 131, "我也不能說什麼。$R;" +
                                           "$R拜託您做這樣的事，$R;" +
                                           "真是不好意思啊。$R;", "下城的優秀阿姨");
                    break;
            }
        }

        void 茜子任務(ActorPC pc)
        {
            BitMask<Neko_05> Neko_05_cmask = pc.CMask["Neko_05"];                                                                                 //任務：凱堤(茜子)

            Neko_05_cmask.SetValue(Neko_05.向下城的優秀阿姨詢問瑪莎的蹤跡,true );

            Say(pc, 11000001, 131, "想要找「瑪莎」?$R;" +
                                   "$R嗯…$R;" +
                                   "她可能在混城騎士團南軍長官那邊。$R;" +
                                   "$P不久之前發生個小的騷動。$R;" +
                                   "$R之後她就刻意去親近南軍長官，$R;" +
                                   "好像是這樣…$R;" +
                                   "$P今天好像也是為了聽長官的話，$R;" +
                                   "跑去那裡的樣子。$R;" +
                                   "$R如果你去那裡說別的故事，$R;" +
                                   "那孩子也會開心的。$R;", "下城的優秀阿姨");
        }
    }
}