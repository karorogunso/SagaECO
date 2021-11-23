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

            this.alreadyHasQuest = "任务进行的还顺利吗?$R;";

            this.gotNormalQuest = "不要辜负我对你的期待啊!$R;";

            this.questCompleted = "辛苦了…$R;" +
                                  "$R恭喜你，任务完成了。$R;" +
                                  "$P来! 收下报酬吧!$R;";

            this.questCanceled = "……$R;" +
                                 "$P下次好好思考以后，$R;" +
                                 "再决定要不要接受任务会比较好。$R;";

            this.questFailed = "…看来是失败了$R;" +
                               "都写脸上了呢…$R;" +
                               "$R只要失去过一次信赖，$R;" +
                               "以后要挽回就是非常难的。$R;";

            this.notEnoughQuestPoint = "现在没有要拜托的。$R;";

            this.questTooEasy = "对你来说这个任务太简单了。$R;" +
                                "$R如果不担心别人的评价，$R;" +
                                "承接这件委托怎么样啊?$R;";

            this.questTooHard = "这是个非常麻烦的任务…$R;" +
                                "$R小鬼呀，你确定能做到吗?$R;"; 
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
                    Say(pc, 11000010, 131, "去南军地下仓库？$R;");
                }
                else
                {
                    Say(pc, 11000010, 131, "？？$R;" +
                        "$R脸色不太好啊$R有什么事情吗？$R;");
                    Say(pc, 0, 131, "把到现在为止的事情都说了$R;");
                    Say(pc, 11000010, 131, "…嘿嘿！这样啊$R;" +
                        "$R知道是什么事情了$R很久以前就认识加多的$R;" +
                        "$P如果是有怀疑的地方的话…$R南军地下仓库有点奇怪…$R告诉你到那个地方的秘道吧$R;" +
                        "$R马上就要去吗？$R;");
                }
                switch (Select(pc, "馬上去嗎？", "", "去！", "現在不去！"))
                {
                    case 1:
                        if (pc.PossesionedActors.Count != 0)
                        {
                            Say(pc, 11000010, 131, "真是！$R好像有人在凭依？$R;" +
                                "$R这通路只能让一个人通过！$R;");
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
                            Say(pc, 11000010, 131, "哎呀…现在有点困难啊？$R;" +
                                "$R军队的监视感应器开启了…$R稍微等一会，再跟我说话吧$R;");
                            return;
                        }//*/
                        break;
                    case 2:
                        Say(pc, 11000010, 131, "是吗？$R;" +
                            "$R准备好了就告诉我吧$R;");
                        break;
                }

                return;
            }

            if (Knights_mask.Test(Knights.已經加入騎士團))
            {
                Say(pc, 11000010, 131, "小鬼你看!$R;" +
                                       "我已经下决心当军人了。$R;" +
                                       "$R不做生意了，赶快给我出去!!$R;", "解答大叔");

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

                Say(pc, 11000010, 131, "变得很强了啊……$R;" +
                                       "$R如果是你的话，$R;" +
                                       "那就没问题了。$R;", "解答大叔");
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
                                       "$P从「佩顿」那小子那里$R;" +
                                       "听到有关这里的事吗?$R;" +
                                       "$P那小子还真是不可靠啊!$R;" +
                                       "总是说一些不该说的话。$R;" +
                                       "$P这里不是你这种初心者来的地方，$R;" +
                                       "快点回去吧!$R;" +
                                       "$R等你等级高一点以后，$R;" +
                                       "再过来吧!!", "解答大叔");
                return;
            }
            else
            {
                if (!Acropolisut_01_mask.Test(Acropolisut_01.已經與解答大叔進行第一次對話))
                {
                    初次與解答大叔進行對話(pc);
                }

                Say(pc, 11000010, 131, "小鬼想干嘛?$R;", "解答大叔");

                switch (Select(pc, "想做什么呢?", "", "买东西", "卖东西", "委托制作『阿克罗波利斯伪造通行证』", "什么也不做"))
                {
                    case 1:
                        OpenShopBuy(pc, 11);

                        Say(pc, 11000010, 131, "以后不要再来这样的地方了。$R;", "解答大叔");
                        break;

                    case 2:
                        OpenShopSell(pc, 11);

                        Say(pc, 11000010, 131, "以后不要再来这样的地方了。$R;", "解答大叔");
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
                Say(pc, 11000010, 131, "你是『盗贼』吗?$R;" +
                                       "$R嘿…那家伙竟然批准了?$R;" +
                                       "看来是很喜欢您啊…$R;" +
                                       "$P经常得到「盗贼们」的帮助啊!$R;" +
                                       "现在换我来帮你吧!$R;" +
                                       "$P这里是「暗黑商店」。$R;" +
                                       "#R只要是危险的事什么都做…$R;" +
                                       "咯咯…$R;" +
                                       "$P我不想把单纯的人牵扯进来，$R;" +
                                       "不可以跟别人说喔。$R;", "解答大叔");
            }
            else
            {
                Say(pc, 11000010, 131, "……$R;" +
                                       "$P从「佩顿」那小子那里$R;" +
                                       "听到有关这裡的情报，$R;" +
                                       "所以才来知道这裡吗?$R;" +
                                       "$P最近总是来一些初心者啊!$R;" +
                                       "$R「下城」也变的冷清了。$R;" +
                                       "$P你知道这里是什么地方吗?$R;" +
                                       "$R呵~ 无论如何，$R;" +
                                       "总得停一下听听吧?$R;", "解答大叔");
            }
        }

        void 委託製作上城的偽造通行證(ActorPC pc)
        {
            BitMask<Acropolisut_Pass_03> Acropolisut_Pass_03_mask = new BitMask<Acropolisut_Pass_03>(pc.CMask["Acropolisut_Pass_03"]);                                  //任務：取得偽造通行證

            if (Acropolisut_Pass_03_mask.Test(Acropolisut_Pass_03.向萬物博士詢問解答大叔的弱點))
            {
                Acropolisut_Pass_03_mask.SetValue(Acropolisut_Pass_03.解答大叔給予上城的偽造通行證, true);

                Say(pc, 11000010, 131, "那傢伙…$R;" +
                                       "连我喜欢的都说了…$R;" +
                                       "真是啰唆…$R;" +
                                       "$P真是的!$R;" +
                                       "$R知道了! 知道了!$R;" +
                                       "帮你做不就行了吗?!$R;" +
                                       "$P不过，你得把那盘『咖喱』$R;" +
                                       "给我交出来才行。$R;", "解答大叔");

                TakeItem(pc, 10008900, 1);
                GiveItem(pc, 10042801, 1);
                Say(pc, 0, 0, "得到『阿克罗波利斯伪造通行证』!$R;", " ");
            }
            else
            {
                Say(pc, 11000010, 131, "什么? 那是什么?$R;" +
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
                switch (Select(pc, "想做什么呢?", "", "买东西", "卖东西", "谈论有关『咖喱』的事情", "什么也不做"))
                {
                    case 1:
                        OpenShopBuy(pc, 11);

                        Say(pc, 11000010, 131, "以后不要再来这样的地方了。$R;", "解答大叔");
                        break;

                    case 2:
                        OpenShopSell(pc, 11);

                        Say(pc, 11000010, 131, "以后不要再来这样的地方了。$R;", "解答大叔");
                        break;

                    case 3:
                        Acropolisut_Pass_03_mask.SetValue(Acropolisut_Pass_03.解答大叔想吃美味的咖哩, true);

                        Say(pc, 11000010, 131, "你也喜欢咖喱啊?$R;" +
                                               "$R看来我们有共同的语言，$R;" +
                                               "我也很喜欢咖喱!!$R;" +
                                               "$P都到了天天吃也不腻的程度，$R;" +
                                               "一提到咖喱就欲罢不能了。$R;" +
                                               "$R听说过『美味的咖喱』吗?$R;" +
                                               "$P听说过是一种入口即溶的咖哩。$R;" +
                                               "$R真是令人食指大动的美味咖喱啊!$R;" +
                                               "$P真想吃一次看看啊…$R;" +
                                               "$R你不想吃吃看吗?$R;" +
                                               "$P啊啊…$R;" +
                                               "$R啊…突然特别想吃咖喱，$R;" +
                                               "想吃得都快发疯了…$R;" +
                                               "$P你今天就先回去吧!$R;", "解答大叔");
                        break;

                    case 4:
                        break;
                }
            }
            else 
            {
                switch (Select(pc, "想做什么呢?", "", "买东西", "卖东西", "什么也不做"))
                {
                    case 1:
                        OpenShopBuy(pc, 11);

                        Say(pc, 11000010, 131, "以后不要再来这样的地方了。$R;", "解答大叔");
                        break;

                    case 2:
                        OpenShopSell(pc, 11);

                        Say(pc, 11000010, 131, "以后不要再来这样的地方了。$R;", "解答大叔");
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
                                       "我没有办法了啊…$R;" +
                                       "$R『美味的咖喱』$R;" +
                                       "总是离不开我的脑子…$R;" +
                                       "$P让我什么都不能想…$R;" +
                                       "什么事也不能做啊!!$R;" +
                                       "$R所以干脆把事情都放在一边了…$R;", "解答大叔");
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

            Say(pc, 11000010, 131, "该不会是…?!$R;" +
                                   "$P难道…$R;" +
                                   "$P你带来的那个难道是…$R;" +
                                   "$P怎么会…根本不可能啊?$R;" +
                                   "$R难道我的眼睛出问题了?$R;" +
                                   "$P但是这个味道…$R;" +
                                   "$R真的是『美味的咖喱』啊!!$R;" +
                                   "$P嘎吱嘎吱…$R;", "解答大叔");

            TakeItem(pc, 10008950, 1);
            Say(pc, 0, 0, "解答大叔狼吞虎咽的$R;" +
                          "把『美味的咖喱』吃掉…$R;");

            Say(pc, 11000010, 131, "呜啊!太太好吃了!$R;" +
                                   "$R为了我把这个…实在太感谢了!$R;" +
                                   "对了，你叫什么名字啊?$R;", "解答大叔");

            Say(pc, 11000010, 131, "我知道了!!$R;" +
                                   "$R嘿……$R;" +
                                   "$P" + pc.Name + "，$R;" +
                                   "我会牢牢的记住你的名字的。$R;" +
                                   "$R下次一定再来啊!!$R;", "解答大叔");
        }

        void 解答大叔第二階段販賣物品(ActorPC pc)
        {
            switch (Select(pc, "", "想做什么呢?", "买东西", "卖东西", "贩卖『伪造通行证』", "什么也不做"))
            {
                case 1:
                    OpenShopBuy(pc, 13);

                    Say(pc, 11000010, 131, pc.Name + "下次再来玩呀!!$R;", "解答大叔");
                    break;

                case 2:
                    OpenShopSell(pc, 13);

                    Say(pc, 11000010, 131, pc.Name + "下次再来玩呀!!$R;", "解答大叔");
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
            switch (Select(pc, "", "想做什么呢?", "买东西", "卖东西", "贩卖『伪造通行证』", "贩卖『职业证明道具』", "什么也不做"))
            {
                case 1:
                    OpenShopBuy(pc, 13);

                    Say(pc, 11000010, 131, pc.Name + "下次再来玩呀!!$R;", "解答大叔");
                    break;

                case 2:
                    OpenShopSell(pc, 13);

                    Say(pc, 11000010, 131, pc.Name + "下次再来玩呀!!$R;", "解答大叔");
                    break;

                case 3:
                    OpenShopBuy(pc, 83);

                    Say(pc, 11000010, 131, "回去的路上要小心啊!$R;", "解答大叔");
                    break;

                case 4:
                    if (pc.Fame >= 100)
                    {
                        OpenShopBuy(pc, 15);

                        Say(pc, 11000010, 131, pc.Name + "下次再来玩呀!!$R;", "解答大叔");
                    }
                    else
                    {
                        Say(pc, 11000010, 131, "这是象征职业标志一般的道具…$R;" +
                                               "因此只凭我的判断是不能贩卖的。$R;" +
                                               "$R必须再多多努力帮助人们，$R;" +
                                               "得到人们的信任。$R;", "解答大叔");
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

            selection = Select(pc, "", "想做什么呢?", "买东西", "卖东西", "贩卖『伪造通行证』", "贩卖『职业证明道具』", "任务服务台", "什么也不做");

            while (selection != 6)
            {
                switch (selection)
                {
                    case 1:
                        OpenShopBuy(pc, 14);

                        Say(pc, 11000010, 131, pc.Name + "下次再来玩呀!!$R;", "解答大叔");
                        return;

                    case 2:
                        OpenShopSell(pc, 14);

                        Say(pc, 11000010, 131, pc.Name + "下次再来玩呀!!$R;", "解答大叔");
                        return;

                    case 3:
                        OpenShopBuy(pc, 83);

                        Say(pc, 11000010, 131, "回去的路上要小心啊!$R;", "解答大叔");
                        return;

                    case 4:
                        if (pc.Fame >= 100)
                        {
                            OpenShopBuy(pc, 15);

                            Say(pc, 11000010, 131, pc.Name + "下次再来玩呀!!$R;", "解答大叔");
                        }
                        else
                        {
                            Say(pc, 11000010, 131, "这是象征职业标志一般的道具…$R;" +
                                                   "因此只凭我的判断是不能贩卖的。$R;" +
                                                   "$R必须再多多努力帮助人们，$R;" +
                                                   "得到人们的信任。$R;", "解答大叔");
                        }
                        return;

                    case 5:
                        if (!Acropolisut_01_mask.Test(Acropolisut_01.已經與解答大叔詢問過任務服務台))
                        {
                            Acropolisut_01_mask.SetValue(Acropolisut_01.已經與解答大叔詢問過任務服務台, true);

                            Say(pc, 11000010, 131, pc.Name + "……$R;" +
                                                   "$R你在最近几个月内，$R;" +
                                                   "变的很强了啊?$R;" +
                                                   "$P你还是初心者的时候，$R;" +
                                                   "我就开始在注意你了。$R;" +
                                                   "$R但是没想到会变得这么强…$R;" +
                                                   "真是厉害啊!$R;" +
                                                   "$P你有想过在『暗黑世界』裡做事吗?$R;" +
                                                   "$P「搬运私货」、「暗杀重要人物」、$R;" +
                                                   "「打猎食人魔物」等等…$R;" +
                                                   "$R虽然都是危险的事情，$R;" +
                                                   "但是报酬也相对多啊。$R;" +
                                                   "$P至于要不要做就由你自己决定吧…$R;", "解答大叔");
                        }
                        else
                        {
                            HandleQuest(pc, 56);
                            return;
                        }
                        break;
                }

                selection = Select(pc, "", "想做什么呢?", "买东西", "卖东西", "贩卖『伪造通行证』", "贩卖『职业证明道具』", "任务服务台", "什么也不做");
            }
        }
    }
}