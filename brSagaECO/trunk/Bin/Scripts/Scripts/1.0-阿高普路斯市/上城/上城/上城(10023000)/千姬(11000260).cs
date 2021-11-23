using System;
using System.Collections.Generic;
using System.Text;

using SagaDB.Actor;
using SagaMap.Scripting;

using SagaLib;
using SagaScript.Chinese.Enums;
//所在地圖:上城(10023000) NPC基本信息:千姬(11000260) X:106 Y:122
namespace SagaScript.M10023000
{
    public class S11000260 : Event
    {
        public S11000260()
        {
            this.EventID = 11000260;
        }

        public override void OnEvent(ActorPC pc)
        {
            BitMask<LV75_Clothes> LV75_Clothes_mask = pc.CMask["LV75_Clothes"];

            /*
            if (!_0b16)
            {
                Call(EVT1100026018);
                return;
            }
            */
            //EVT1100026018
            if (pc.JobLevel2T > 29)
            {
                if (!LV75_Clothes_mask.Test(LV75_Clothes.千姬第一次對話))
                {
                    Say(pc, 131, "這是傳授給我們一族的秘笈$R;" +
                        "$R我正在找適合傳授秘笈的對象唷$R;" +
                        "$R沒有能夠匹敵草眼睛的冒險者$R;" +
                        "這件事真是太可疑了$R;");
                    Select(pc, "どうする？", "", "我呀！我呀！我最適合不過啦！", "我呀！我呀！我最適合不過啦！", "我沒興趣呢");
                    LV75_Clothes_mask.SetValue(LV75_Clothes.千姬第一次對話, true);
                    //_0c85 = true;
                    Say(pc, 131, "……$R;" +
                        "的話，接受草員的挑戰吧！$R;");
                    if (CountItem(pc, 10022780) > 0)
                    {
                        LV75_Clothes_mask.SetValue(LV75_Clothes.獲得鑰匙, true);
                        //_0c86 = true;
                        Say(pc, 131, "便$R;" +
                            "擁有那條鑰匙$R;" +
                            "就是完成草員挑戰的證據唷！$R;" +
                            "$R的話，接受下一個挑戰吧！$R;");
                        //GOTO EVT1100026013
                        return;
                    }
                    Say(pc, 131, "我也這樣認為，請再等一下吧！$R;" +
                        "$P真的有吧？可是挑戰草員，$R;" +
                        "不僅漫長還很艱辛唷$R;" +
                        "$R一定要堅持到底，$R;" +
                        "把現在接的任務完成喔$R;");
                    return;
                }

                if (!LV75_Clothes_mask.Test(LV75_Clothes.獲得鑰匙))
                {
                    //999魔物挑战
                    if (pc.Quest == null)
                    {
                        Say(pc, 131, "無論什麼魔物都可以喔$R;" +
                            "首先打倒999隻魔物吧！$R;" +
                            "$R先在樸魯打倒1隻吧！$R;" +
                            "然後在娜諾奧也打倒1隻吧！$R;" +
                            "$P要一個人去打倒999隻魔物很難受吧！$R;" +
                            "$R草員的語言，即使人類也能聽懂的喔$R;" +
                            "可不可以跟隊友一起完成任務呢？$R;" +
                            "$R嗯，即使跟隊友一起完成，$R要求也是一樣喔！$R;" +
                            "只要承接了任務，討伐魔物的數量$R;" +
                            "就可以共同計算唷！$R;");
                        switch (Select(pc, "想要做什麼呢？", "", "好像會很煩呢", "好的！挑戰吧！"))
                        {
                            case 1:
                                break;
                            case 2:
                                HandleQuest(pc, 30);
                                break;
                        }
                        return;

                    }

                    else
                    {
                        if (pc.Quest.ID == 10031700)
                        {
                            if (pc.Quest.Status == SagaDB.Quests.QuestStatus.COMPLETED)
                            {
                                LV75_Clothes_mask.SetValue(LV75_Clothes.獲得鑰匙, true);
                                Say(pc, 131, "打倒了999隻魔物時，$R記住要告訴我喔！$R;" +
                                    "這可千萬不能馬虎呢！$R;");
                            }

                            if (pc.Quest.Status == SagaDB.Quests.QuestStatus.FAILED)
                            {
                                Say(pc, 131, "這樣！$R;");
                            }

                            HandleQuest(pc, 30);
                            return;
                        }
                    }

                    Say(pc, 131, "我也這樣認為，請再等一下吧！$R;" +
                        "$P真的有吧？可是挑戰草員，$R;" +
                        "不僅漫長還很艱辛唷$R;" +
                        "$R一定要堅持到底，$R;" +
                        "把現在接的任務完成喔$R;");
                    return;
                }
                if (!LV75_Clothes_mask.Test(LV75_Clothes.任務完成))
                {
                    //最后的挑战
                    if (pc.Quest != null)
                    {
                        if (pc.Quest.ID == 10031701 && pc.Quest.Status == SagaDB.Quests.QuestStatus.OPEN)
                        {
                            Say(pc, 131, "好的！要現在去嗎？$R;");
                            switch (Select(pc, "想要做什麼呢？", "", "再等一下吧", "好！我要挑戰！"))
                            {
                                case 1:
                                    break;

                                case 2:
                                    if (pc.PossesionedActors.Count != 0)
                                    {
                                        Say(pc, 131, "依賴草員的同僚，是不被允許的$R;" +
                                            "$R解除憑依後走吧！$R;");
                                        return;
                                    }

                                    if (!LV75_Clothes_mask.Test(LV75_Clothes.給予鑰匙))
                                    {
                                        Say(pc, 131, "把鑰匙還給草鱷魚吧$R;");
                                        if (CountItem(pc, 10022780) == 0)
                                        {
                                            Say(pc, 131, "……?$R;" +
                                                "$P棉和鑰匙不見了？$R;" +
                                                "$R沒有辦法的冒險者傳記$R;" +
                                                "怎麽樣都好$R;" +
                                                "這就是為這個時候準備的後備鑰匙阿！$R;");
                                        }
                                        else
                                        {
                                            TakeItem(pc, 10022780, 1);
                                            Say(pc, 131, "把「仙姬美的飛空庭鑰匙」還她了！$R;");
                                        }
                                    }

                                    Say(pc, 131, "貼著走吧！$R;");
                                    LV75_Clothes_mask.SetValue(LV75_Clothes.給予鑰匙, true);
                                    //_0c87 = true;
                                    //EVENTMAP_IN 11 1 7 12 4
                                    //SWITCH START

                                    pc.CInt["LV75_Clothes_Map_01"] = CreateMapInstance(50011000, 10023000, 95, 165);

                                    LoadSpawnFile(pc.CInt["LV75_Clothes_Map_01"], "DB/Spawns/50011000.xml");

                                    Warp(pc, (uint)pc.CInt["LV75_Clothes_Map_01"], 7, 11);

                                    /*
                                    if (pc.QuestRemaining < 1)
                                    {
                                        Say(pc, 131, "嗯……草員的飛空庭在哪裡呢?$R;" +
                                            "$R在阿高普路斯有很多飛空庭喔$R;" +
                                            "要先找找，請稍等一下吧$R;");
                                        return;
                                    }*/
                                    break;
                            }
                        }
                        else
                        {
                            Say(pc, 131, "我也這樣認為，再等一下吧！$R;" +
                                "$P一點也不後悔$R;" +
                                "又接受任務？$R;" +
                                "$R快點把現在接了的任務完成喔！$R;"); 
                        }
                    }
                    else
                    {
                        if (pc.QuestRemaining < 1)
                        {
                            Say(pc, 131, "現在不能進行挑戰，以後再來吧$R;");
                            return;
                        }
                        Say(pc, 131, "會讓您跟守護草員的$R;" +
                            "魔鳥火鳳凰决鬥喔！$R;" +
                            "$P雖然好像是什麼大型的任務$R;" +
                            "其實只是架子而己$R;" +
                            "$R只是想用您來試驗而已喔?$R;" +
                            "$P這次是同僚的委託$R;" +
                            "$R只要您贏了魔鳥$R;" +
                            "就可以向您傳授秘笈唷！$R;" +
                            "$P魔鳥會在草員的飛空庭等您的！$R;" +
                            "$R如果準備好的話$R;" +
                            "就去跟草鱷魚說說話吧！$R;");
                        switch (Select(pc, "想要做什麼呢？", "", "好像會很煩呢！", "好的，挑戰吧！"))
                        {
                            case 1:
                                break;
                            case 2:
                                HandleQuest(pc, 31);
                                break;
                        }
                    }
                    /*
                    Say(pc, 131, "這是傳授給我們一族的秘笈$R;" +
                        "$R我正在找適合傳授秘笈的對象唷$R;" +
                        "$R沒有能夠匹敵草眼睛的冒險者$R;" +
                        "這件事真是太可疑了阿$R;");
                    */
                    return;
                }

                Say(pc, 131, "我正在等著您喔！$R;" +
                    "這是給我們一族使用的防具唷！$R;");

                if (pc.JobBasic == PC_JOB.RANGER ||
                    pc.JobBasic == PC_JOB.MERCHANT)
                {
                    OpenShopBuy(pc, 206);
                    return;
                }

                if (pc.JobBasic == PC_JOB.TATARABE ||
                    pc.JobBasic == PC_JOB.FARMASIST)
                {
                    OpenShopBuy(pc, 205);
                    return;
                }

                if (pc.JobBasic == PC_JOB.VATES ||
                    pc.JobBasic == PC_JOB.WARLOCK)
                {
                    OpenShopBuy(pc, 204);
                    return;
                }

                if (pc.JobBasic == PC_JOB.WIZARD ||
                    pc.JobBasic == PC_JOB.SHAMAN)
                {
                    OpenShopBuy(pc, 203);
                    return;
                }

                if (pc.JobBasic == PC_JOB.SCOUT ||
                    pc.JobBasic == PC_JOB.ARCHER)
                {
                    OpenShopBuy(pc, 202);
                    return;
                }

                if (pc.JobBasic == PC_JOB.FENCER ||
                    pc.JobBasic == PC_JOB.SWORDMAN)
                {
                    OpenShopBuy(pc, 201);
                    return;
                }
                return;
            }
            Say(pc, 131, "呵呵呵呵…$R;");
            //EVT11000260end
            //EVENTEND

            /*
            //EVT1100026013
            switch (Select(pc, "想要做什麼呢？", "", "累了，休息吧", "接受吧！西軍！"))
            {
                case 1:
                    Say(pc, 131, "都準備好的話$R;" +
                        "去跟草鱷魚說說話也好！$R;");
                    break;
                case 2:
                    if (pc.QuestRemaining < 1)
                    {
                        Say(pc, 131, "現在不能進行挑戰，以後再來吧$R;");
                        return;
                    }
                    Say(pc, 131, "我也這樣認為，再等一下吧！$R;" +
                        "$P一點也不後悔$R;" +
                        "又接受任務？$R;" +
                        "$R快點把現在接了的任務完成喔！$R;");
                    break;
            }
            //EVENTEND
            */
        }

        void 南瓜(ActorPC pc)
        {
            /*
            //EVT1100026020
            if (_0b14)
            {
                if (CheckInventory(pc, 50011250, 1))
                {
                    Say(pc, 131, "要送您一件好東西，來答謝您喔！$R;");
                    switch (Select(pc, "喜歡哪一樣呢？", "", "南瓜頭", "南瓜紋中統襪（女）", "南瓜花圃"))
                    {
                        case 1:
                            _0b16 = true;
                            GiveItem(pc, 50024358, 1);
                            Say(pc, 131, "好！就給您這個吧！$R;" +
                                "草，這是我們做的「南瓜頭」$R;" +
                                "$R本來想用這個做偽裝，$R;" +
                                "躲過那些鬼怪的$R;" +
                                "但現在已經用不到了$R;");
                            break;
                        case 2:
                            _0b16 = true;
                            GiveItem(pc, 50011250, 1);
                            break;
                        case 3:
                            _0b16 = true;
                            GiveItem(pc, 31160200, 1);
                            break;
                    }
                    return;
                }
                return;
            }
            if (_0b13 && CountItem(pc, 10001080) >= 1)
            {
                Say(pc, 131, "之前在那邊的公園裡有鬼喔！$R;" +
                    "$R非常可怕啊……$R;");
                switch (Select(pc, "想要做什麼呢？", "", "聊天", "給他『秘密糖果』"))
                {
                    case 1:
                        GOTO EVT1100026018;
                        break;
                    case 2:
                        _0b14 = true;
                        TakeItem(pc, 10001080, 1);
                        Say(pc, 131, "這…這是『秘密糖果』！！$R;" +
                            "吃這個的話，鬼怪就不能侵犯您$R;" +
                            "的神秘糖果呀！$R;" +
                            "$R真是感謝，現在我不害怕鬼怪了！$R;");
                        if (CheckInventory(pc, 50011250, 1))
                        {
                            Say(pc, 131, "要送您一件好東西，來答謝您喔！$R;");
                            switch (Select(pc, "喜歡哪一樣呢？", "", "南瓜頭", "南瓜紋中統襪（女）", "南瓜花圃"))
                            {
                                case 1:
                                    _0b16 = true;
                                    GiveItem(pc, 50024358, 1);
                                    Say(pc, 131, "好！就給您這個吧！$R;" +
                                        "草，這是我們做的「南瓜頭」$R;" +
                                        "$R本來想用這個做偽裝，$R;" +
                                        "躲過那些鬼怪的$R;" +
                                        "但現在已經用不到了$R;");
                                    break;
                                case 2:
                                    _0b16 = true;
                                    GiveItem(pc, 50011250, 1);
                                    break;
                                case 3:
                                    _0b16 = true;
                                    GiveItem(pc, 31160200, 1);
                                    break;
                            }
                            return;
                        }
                        break;
                }
                return;
            }
            */
        }
    }
}
