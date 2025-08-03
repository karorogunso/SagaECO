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
                    Say(pc, 131, "这是传授给我们一族的秘笈$R;" +
                        "$R我正在找适合传授秘笈的对象哦$R;" +
                        "$R没有能够匹敌草眼睛的冒险者$R;" +
                        "这件事真是太可疑了$R;");
                    Select(pc, "怎么办？", "", "我呀！我呀！我最适合不过啦！", "我呀！我呀！我最适合不过啦！", "我没兴趣呢");
                    LV75_Clothes_mask.SetValue(LV75_Clothes.千姬第一次對話, true);
                    //_0c85 = true;
                    Say(pc, 131, "……$R;" +
                        "的话，接受草员的挑战吧！$R;");
                    if (CountItem(pc, 10022780) > 0)
                    {
                        LV75_Clothes_mask.SetValue(LV75_Clothes.獲得鑰匙, true);
                        //_0c86 = true;
                        Say(pc, 131, "便$R;" +
                            "拥有那条钥匙$R;" +
                            "就是完成草员挑战的证据呢！$R;" +
                            "$R的话，接受下一个挑战吧！$R;");
                        //GOTO EVT1100026013
                        return;
                    }
                    Say(pc, 131, "我也这样认为，请再等一下吧！$R;" +
                        "$P真的有吧？可是挑战草员，$R;" +
                        "不仅漫长还很艰辛唷$R;" +
                        "$R一定要坚持到底，$R;" +
                        "把现在接的任务完成喔$R;");
                    return;
                }

                if (!LV75_Clothes_mask.Test(LV75_Clothes.獲得鑰匙))
                {
                    //999魔物挑战
                    if (pc.Quest == null)
                    {
                        Say(pc, 131, "无论什么魔物都可以喔$R;" +
                            "首先打倒999只魔物吧！$R;" +
                            "$R先在朴鲁打倒1只吧！$R;" +
                            "然后在娜诺奥也打倒1隻吧！$R;" +
                            "$P要一个人去打倒999只魔物很难受吧！$R;" +
                            "$R草员的语言，即使人类也能听懂的喔$R;" +
                            "可不可以跟队友一起完成任务呢？$R;" +
                            "$R嗯，即使跟队友一起完成，$R要求也是一样喔！$R;" +
                            "只要承接了任务，讨伐魔物的数量$R;" +
                            "就可以共同计算唷！$R;");
                        switch (Select(pc, "想要做什么呢？", "", "好像会很烦呢", "好的！挑战吧！"))
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
                                Say(pc, 131, "打倒了999只魔物时，$R记住要告诉我喔！$R;" +
                                    "这可千万不能马虎呢！$R;");
                            }

                            if (pc.Quest.Status == SagaDB.Quests.QuestStatus.FAILED)
                            {
                                Say(pc, 131, "这样！$R;");
                            }

                            HandleQuest(pc, 30);
                            return;
                        }
                    }

                    Say(pc, 131, "我也这样认为，请再等一下吧！$R;" +
                        "$P真的有吧？可是挑战草员，$R;" +
                        "不仅漫长还很艰辛唷$R;" +
                        "$R一定要坚持到底，$R;" +
                        "把现在接的任务完成喔$R;");
                    return;
                }
                if (!LV75_Clothes_mask.Test(LV75_Clothes.任務完成))
                {
                    //最后的挑战
                    if (pc.Quest != null)
                    {
                        if (pc.Quest.ID == 10031701 && pc.Quest.Status == SagaDB.Quests.QuestStatus.OPEN)
                        {
                            Say(pc, 131, "好的！要现在去吗？$R;");
                            switch (Select(pc, "想要做什么呢？", "", "再等一下吧", "好！我要挑战！"))
                            {
                                case 1:
                                    break;

                                case 2:
                                    if (pc.PossesionedActors.Count != 0)
                                    {
                                        Say(pc, 131, "依赖草员的同僚，是不被允许的$R;" +
                                            "$R解除凭依后走吧！$R;");
                                        return;
                                    }

                                    if (!LV75_Clothes_mask.Test(LV75_Clothes.給予鑰匙))
                                    {
                                        Say(pc, 131, "把钥匙还给草鳄鱼吧$R;");
                                        if (CountItem(pc, 10022780) == 0)
                                        {
                                            Say(pc, 131, "……?$R;" +
                                                "$P棉和钥匙不见了？$R;" +
                                                "$R没有办法的冒险者传记$R;" +
                                                "怎么样都好$R;" +
                                                "这就是为这个时候准备的后备钥匙阿！$R;");
                                        }
                                        else
                                        {
                                            TakeItem(pc, 10022780, 1);
                                            Say(pc, 131, "把「仙姬美的飞空庭钥匙」还她了！$R;");
                                        }
                                    }

                                    Say(pc, 131, "贴着走吧！$R;");
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
                                        Say(pc, 131, "嗯……草员的飞空庭在哪里呢?$R;" +
                                            "$R在阿克罗波利斯有很多飞空庭喔$R;" +
                                            "要先找找，请稍等一下吧$R;");
                                        return;
                                    }*/
                                    break;
                            }
                        }
                        else
                        {
                            Say(pc, 131, "我也这样认为，再等一下吧！$R;" +
                                "$P一点也不后悔$R;" +
                                "又接受任务？$R;" +
                                "$R快点把现在接了的任务完成喔！$R;"); 
                        }
                    }
                    else
                    {
                        if (pc.QuestRemaining < 1)
                        {
                            Say(pc, 131, "現在不能進行挑戰，以後再來吧$R;");
                            return;
                        }
                        Say(pc, 131, "会让您跟守护草员的$R;" +
                            "魔鸟火凤凰决斗喔！$R;" +
                            "$P虽然好像是什么大型的任务$R;" +
                            "其实只是架子而己$R;" +
                            "$R只是想用您来试验而已喔?$R;" +
                            "$P这次是同僚的委託$R;" +
                            "$R只要您赢了魔鸟$R;" +
                            "就可以向您传授秘笈哦！$R;" +
                            "$P魔鸟会在草员的飞空庭等您的！$R;" +
                            "$R如果準备好的话$R;" +
                            "就去跟草鳄鱼说说话吧！$R;");
                        switch (Select(pc, "想要做什么呢？", "", "好像会很烦呢！", "好的，挑战吧！"))
                        {
                            case 1:
                                break;
                            case 2:
                                HandleQuest(pc, 31);
                                break;
                        }
                    }
                    /*
                    Say(pc, 131, "这是传授给我们一族的秘笈$R;" +
                        "$R我正在找适合传授秘笈的对象唷$R;" +
                        "$R没有能够匹敌草眼睛的冒险者$R;" +
                        "这件事真是太可疑了阿$R;");
                    */
                    return;
                }

                Say(pc, 131, "我正在等著您喔！$R;" +
                    "这是给我们一族使用的防具哦！$R;");

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
            switch (Select(pc, "想要做什么呢？", "", "累了，休息吧", "接受吧！西军！"))
            {
                case 1:
                    Say(pc, 131, "都准备好的话$R;" +
                        "去跟草鳄鱼说说话也好！$R;");
                    break;
                case 2:
                    if (pc.QuestRemaining < 1)
                    {
                        Say(pc, 131, "现在不能进行挑战，以后再来吧$R;");
                        return;
                    }
                    Say(pc, 131, "我也这样认为，再等一下吧！$R;" +
                        "$P一点也不后悔$R;" +
                        "又接受任务？$R;" +
                        "$R快点把现在接了的任务完成喔！$R;");
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
                    Say(pc, 131, "要送您一件好东西，来答谢您喔！$R;");
                    switch (Select(pc, "喜欢哪一样呢？", "", "南瓜头", "南瓜纹中统袜（女）", "南瓜花圃"))
                    {
                        case 1:
                            _0b16 = true;
                            GiveItem(pc, 50024358, 1);
                            Say(pc, 131, "好！就给您这个吧！$R;" +
                                "草，这是我们做的「南瓜头」$R;" +
                                "$R本来想用这个做伪装，$R;" +
                                "躲过那些鬼怪的$R;" +
                                "但现在已经用不到了$R;");
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
                Say(pc, 131, "之前在那边的公园里有鬼喔！$R;" +
                    "$R非常可怕啊……$R;");
                switch (Select(pc, "想要做什么呢？", "", "聊天", "给他『秘密糖果』"))
                {
                    case 1:
                        GOTO EVT1100026018;
                        break;
                    case 2:
                        _0b14 = true;
                        TakeItem(pc, 10001080, 1);
                        Say(pc, 131, "这…这是『秘密糖果』！！$R;" +
                            "吃这个的话，鬼怪就不能侵犯您$R;" +
                            "的神秘糖果呀！$R;" +
                            "$R真是感谢，现在我不害怕鬼怪了！$R;");
                        if (CheckInventory(pc, 50011250, 1))
                        {
                            Say(pc, 131, "要送您一件好东西，来答谢您喔！$R;");
                            switch (Select(pc, "喜欢哪一样呢？", "", "南瓜头", "南瓜纹中统袜（女）", "南瓜花圃"))
                            {
                                case 1:
                                    _0b16 = true;
                                    GiveItem(pc, 50024358, 1);
                                    Say(pc, 131, "好！就给您这个吧！$R;" +
                                        "草，这是我们做的「南瓜头」$R;" +
                                        "$R本来想用这个做伪装，$R;" +
                                        "躲过那些鬼怪的$R;" +
                                        "但现在已经用不到了$R;");
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
