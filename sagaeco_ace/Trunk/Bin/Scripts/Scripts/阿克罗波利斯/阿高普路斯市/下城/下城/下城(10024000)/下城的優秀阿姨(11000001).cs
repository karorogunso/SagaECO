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

            this.gotNormalQuest = "把垃圾扔进垃圾桶的话，$R;" +
                                  "数量就会自动计算。$R;" +
                                  "$R如果拿不动的话，就分几次扔吧!$R;" +
                                  "那拜托您了!!$R;";

            this.questCompleted = "真是辛苦了，$R;" +
                                  "$R过来拿报酬吧!$R;";

            this.questCanceled = "真的是令我感到很失望啊!$R;";

            this.questFailed = "任务失败了吗?$R;";

            this.notEnoughQuestPoint = "看来现在不能接受任务，$R;" +
                                       "$R下次再来吧!$R;";
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
                                   "$R找我有什么事情吗?$R;", "有名望的老妇人");

            selection = Select(pc, "想要做什么呢?", "", "清洁计划", "什么也不做");

            while (selection != 2)
            {
                if (!Acropolisut_01_mask.Test(Acropolisut_01.已經接受過清潔任務))
                {
                    Acropolisut_01_mask.SetValue(Acropolisut_01.已經接受過清潔任務, true);

                    Say(pc, 11000001, 131, "想要参加「清洁计划」嘛?$R;" +
                                           "真的是很感谢啊!!$R;" +
                                           "$P这个计划是把路边的垃圾，$R;" +
                                           "主动的捡起来扔进垃圾桶里的活动。$R;" +
                                           "$R目的是要把世界弄干净。$R;" +
                                           "$P那么就开始捡垃圾吧!!$R;", "有名望的老妇人");
                }
                else
                {
                    Say(pc, 11000001, 131, "这个任务需要任务点数『2』。$R;", "有名望的老妇人");

                    HandleQuest(pc, 46);
                    return;
                }

                selection = Select(pc, "想要做什么呢?", "", "清洁计划", "什么也不做");
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
                    Say(pc, 11000001, 131, "哎呀! 吓我一跳!!$R;" +
                                           "原来不是鬼怪啊。$R;", "有名望的老妇人");
                }
            }
        }

        void 初次與下城的優秀阿姨進行對話(ActorPC pc)
        {
            BitMask<Acropolisut_01> Acropolisut_01_mask = new BitMask<Acropolisut_01>(pc.CMask["Acropolisut_01"]);                                                      //一般：阿高普路斯市

            Acropolisut_01_mask.SetValue(Acropolisut_01.已經與下城的優秀阿姨進行第一次對話, true);

            Say(pc, 11000001, 131, "真是勇敢的孩子，$R;" +
                                   "路上小心喔!$R;", "有名望的老妇人");
        }

        void 取得上城通行證(ActorPC pc)
        {
            BitMask<Acropolisut_Pass_02> Acropolisut_Pass_02_mask = new BitMask<Acropolisut_Pass_02>(pc.CMask["Acropolisut_Pass_02"]);                                  //任務：取得上城通行證

            BitMask<Knights> Knights_mask = pc.CMask["Knights"];  

            Say(pc, 11000001, 131, "……$R;" +
                                   "$P在哪听说过跟我有关的消息吧。$R;" +
                                   "$R我的名字是「路蓝」。$R;" +
                                   "$P现在担任治理「阿克罗波利斯」的$R;" +
                                   "『行会评议会』主席一职。$R;" +
                                   "$R想也知道要进「上城」，$R;" +
                                   "是需要『通行证』的吧?$R;", "有名望的老妇人");

            if (pc.Fame > 0)
            {
                Acropolisut_Pass_02_mask.SetValue(Acropolisut_Pass_02.下城的優秀阿姨給予上城通行證, true);
                Knights_mask.SetValue(Knights.取得上城通行證, true);
                Say(pc, 11000001, 131, "……$R;" +
                                       "$P有关你的事情，$R;" +
                                       "我从酒馆老板$R;" +
                                       "「菲利普」那里听过了。$R;" +
                                       "$P可靠而且精神充沛。$R;" +
                                       "$R听说是最近加入的新冒险者中，$R;" +
                                       "最有潜质的啊!$R;" +
                                       "$P得到城市的人信任是很重要的。$R;" +
                                       "$P「阿克罗波利斯」$R;" +
                                       "如果没有像你这样的冒险者，$R;" +
                                       "就不能存在了。$R;" +
                                       "$R虽然给您『通行证』也不成问题…$R;" +
                                       "$P但是有一个条件，$R;" +
                                       "就是绝对不可以在那里打架。$R;" +
                                       "$R这一点一定要做好，知道了吧?$R;" +
                                       "$R要保管好『阿克罗波利斯通行证』唷$!R;", "有名望的老妇人");

                PlaySound(pc, 4006, false, 100, 50);
                GiveItem(pc, 10042800, 1);
                Say(pc, 0, 65535, "得到『阿克罗波利斯通行证』!$R;", " ");
            }
            else
            {
                Say(pc, 11000001, 131, "……$R;" +
                                       "$P您是第一次来这个城市吧?$R;" +
                                       "$R「阿克罗波利斯」需要像您$R;" +
                                       "这样的冒险者才能存活的喔。$R;" +
                                       "$P…但是现在我对您还不太了解，$R;" +
                                       "所以还不能信赖您啊!!$R;", "有名望的老妇人");
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

                Say(pc, 11000001, 131, "噢噢! 是新的冒险者啊!$R;" +
                                       "对这个地方熟悉了吗?$R;" +
                                       "$R偶尔来我的地方玩吧!!$R;", "有名望的老妇人");
                return;
            }

            if (!Acropolisut_01_mask.Test(Acropolisut_01.下城的優秀阿姨給予銅徽章) &&
                pc.Fame >= 10)
            {
                Acropolisut_01_mask.SetValue(Acropolisut_01.下城的優秀阿姨給予銅徽章, true);

                Say(pc, 11000001, 131, "咿呀…变得很大义凛然啊!$R;" +
                                       "$R现在对这个地方也完全熟悉了吧?$R;" +
                                       "$P这是我给您的礼物。$R;", "有名望的老妇人");

                PlaySound(pc, 4006, false, 100, 50);
                GiveItem(pc, 10009500, 1);
                Say(pc, 0, 65535, "得到『铜徽章』!$R;", " ");

                Say(pc, 11000001, 131, "这是因为您为城市的付出，$R;" +
                                       "所以送给您的礼物。$R;" +
                                       "$R以后也要经常过来啊!$R;", "有名望的老妇人");
                return;
            }

            if (LV35_Clothes_01_mask.Test(LV35_Clothes_01.見習鐵匠阿魯斯的委託完成) &&
                LV35_Clothes_02_mask.Test(LV35_Clothes_02.獨奏的委託完成) &&
                LV35_Clothes_03_mask.Test(LV35_Clothes_03.撒帕涅的委託完成) &&
                LV35_Clothes_04_mask.Test(LV35_Clothes_04.熊太郎的委託完成) &&
                !Acropolisut_01_mask.Test(Acropolisut_01.下城的優秀阿姨給予技能點))
            {
                Acropolisut_01_mask.SetValue(Acropolisut_01.下城的優秀阿姨給予技能點, true);

                Say(pc, 11000001, 131, "听说人们有困难的时候，$R;" +
                                       "您帮助了他们?$R;" +
                                       "$R真是感谢啊!!$R;" +
                                       "$P这是我给您的礼物。$R;", "有名望的老妇人");

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

            Say(pc, 0, 65535, "技能点数上升『1』!$R;", " ");
        }

        void 行會評議會的禮物(ActorPC pc)
        {
            BitMask<Hearts_Gift> Hearts_Gift_mask = new BitMask<Hearts_Gift>(pc.CMask["Hearts_Gift"]);                                                                  //任務：行會評議會的禮物

            if (!Hearts_Gift_mask.Test(Hearts_Gift.向下城的優秀阿姨詢問心靈之票))
            {
                Hearts_Gift_mask.SetValue(Hearts_Gift.向下城的優秀阿姨詢問心靈之票, true);

                Say(pc, 11000001, 131, "你拥有『衷心的感谢礼券』啊?$R;" +
                                       "$R看你拥有那个，应该是对初心者吧?$R;" +
                                       "…很亲切吧?$R;" +
                                       "$P不用谦虚的啊!$R;" +
                                       "你的行为，真的很亲切啊!!$R;" +
                                       "$P对啊……$R;" +
                                       "收集到5张『衷心的感谢礼券』，$R;" +
                                       "拿到「行会评议会」去的话，$R;" +
                                       "他们会送你一份礼物来感谢您喔。$R;", "有名望的老妇人");
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
                                       "$P已经有5张『衷心的感谢礼券』了啊?$R;" +
                                       "那么收下这个礼物吧!$R;", "有名望的老妇人");

                PlaySound(pc, 2040, false, 100, 50);
                GiveItem(pc, 10049055, 1);
                Say(pc, 0, 65535, "得到『行会评议会送来的礼物』!$R;", "有名望的老妇人");

                Say(pc, 11000001, 131, "以后您也是冒险者前辈，$R;" +
                                       "记得要帮助初心者喔!$R;", "有名望的老妇人");
            }
            else
            {
                Say(pc, 11000001, 131, "…$R;" +
                                       "$P已经有5张『衷心的感谢礼券』了吗?$R;" +
                                       "$R马上给你礼物，$R;" +
                                       "那可以请您减轻行李重量吗?$R;", "有名望的老妇人");
            }
        }

        void 堇子任務(ActorPC pc)
        {
            BitMask<Neko_03> Neko_03_amask = pc.AMask["Neko_03"];
            BitMask<Neko_03> Neko_03_cmask = pc.CMask["Neko_03"];                                                                                  //任務：凱堤(堇子)

            Say(pc, 11000001, 131, "您来的正好啊!$R;" +
                                   "$R有事想拜托您，可以帮我一个忙吗?$R;" +
                                   "$P其实是想请您帮我找一个人。$R;" +
                                   "$R他是坐著从「摩戈岛」到「阿克罗波利斯」的$R;" +
                                   "货物艇的偷渡者…$R;" +
                                   "$R『混成骑士团』好像在追捕他?$R;" +
                                   "$P当然用货物艇偷渡是犯法的，$R;" +
                                   "得支付罚款和运费才行…$R;" +
                                   "$P但是正规军这样坚韧不拔的追捕，$R;" +
                                   "这可是第一次呢!!$R;" +
                                   "$P『评议会』觉得那个偷渡者在货物艇里，$R;" +
                                   "看到了『混成骑士团』想要隐藏的一些东西。$R;" +
                                   "$P对了，偷渡者是一名小男孩，$R;" +
                                   "听说往「阿克罗尼亚东部平原」的方向逃走了。$R;" +
                                   "$P在那孩子被『混成骑士团』抓到之前，$R;" +
                                   "去把他找出来，带到这边可以吗?$R;", "有名望的老妇人");

            switch (Select(pc, "怎么办呢?", "", "找找看", "不找"))
            {
                case 1:
                    Neko_03_amask.SetValue(Neko_03.堇子任務開始, true);
                    Neko_03_cmask.SetValue(Neko_03.接受下城的優秀阿姨給予的任務, true);

                    Say(pc, 11000001, 131, "谢谢!!$R;" +
                                           "$R那就拜託您了。$R;", "有名望的老妇人");
                    break;

                case 2:
                    Say(pc, 11000001, 131, "我也不能说什么。$R;" +
                                           "$R拜托您做这样的事，$R;" +
                                           "真是不好意思啊。$R;", "有名望的老妇人");
                    break;
            }
        }

        void 茜子任務(ActorPC pc)
        {
            BitMask<Neko_05> Neko_05_cmask = pc.CMask["Neko_05"];                                                                                 //任務：凱堤(茜子)

            Neko_05_cmask.SetValue(Neko_05.向下城的優秀阿姨詢問瑪莎的蹤跡,true );

            Say(pc, 11000001, 131, "想要找「玛莎」?$R;" +
                                   "$R嗯…$R;" +
                                   "她可能在混成骑士团南军长官那边。$R;" +
                                   "$P不久之前发生个小的骚动。$R;" +
                                   "$R之后她就刻意去亲近南军长官，$R;" +
                                   "好像是这样…$R;" +
                                   "$P今天好像也是为了听长官的话，$R;" +
                                   "跑去那里的样子。$R;" +
                                   "$R如果你去那里说别的故事，$R;" +
                                   "那孩子也会开心的。$R;", "有名望的老妇人");
        }
    }
}