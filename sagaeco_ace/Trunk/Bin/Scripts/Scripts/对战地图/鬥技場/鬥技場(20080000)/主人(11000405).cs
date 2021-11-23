using System;
using System.Collections.Generic;
using System.Text;

using SagaDB.Actor;
using SagaMap.Scripting;

using SagaMap;

using SagaLib;
//所在地圖:鬥技場(20080000) NPC基本信息:主人(11000405) X:25 Y:8
namespace SagaScript.M20080000
{
    public class S11000405 : Event
    {
        public S11000405()
        {
            this.EventID = 11000405;
        }

        public override void OnEvent(ActorPC pc)
        {
            byte x, y;

            if (pc.Mode == PlayerMode.COLISEUM_MODE)
            {
                取消對戰模式(pc);
                return;
            }

            Say(pc, 11000405, 131, "欢迎来到斗技场哦!!$R;" +
                                   "$R这里是有着多种对战方式，$R;" +
                                   "供冒险者之间切磋的战斗空间喔。$R;" +
                                   "$P但必须得先向我提出申请，$R;" +
                                   "不然是无法参战的呀。$R;" +
                                   "$P决斗方式有，$R;" +
                                   "把参赛者分成$R;" +
                                   "东、南、西、北骑士团的「骑士团练习」。$R;" +
                                   "$R还有把参赛者全体变身成皮露露，$R;" +
                                   "分成两组竞争的「梦之狂想战」。$R;" +
                                   "$P怎么样呢?$R;" +
                                   "$R想要参加吗?$R;", "主人");

            switch (Select(pc, "'想要参加哪个竞技活动?", "", "参加「斗技场」", "参加「骑士团练习」", "参加「梦之狂想战」", "从斗技场出去", "什么也不做"))
            {
                case 1:
                    鬥技場模式切換(pc);
                    break;

                case 2:
                    骑士团演习(pc);
                    break;

                case 3:
                   // GOTO EVT11000405202;
                    break;

                case 4:
                    x = (byte)Global.Random.Next(134, 135);
                    y = (byte)Global.Random.Next(47, 50);

                    Warp(pc, 10024000, x, y);
                    break;

                case 5:
                    break;
            }
        }

        void 骑士团演习(ActorPC pc)
        {
            Say(pc, 11000405, 131, "想要参加骑士团练习是吗？$R;" +
                "让我详细告诉您规则好吗？$R;");
            int sel2;
            do
            {
                sel2 = Select(pc, "听骑士团练习规则吗？", "", "听骑士团练习规则。", "骑士团练习报名方法", "还是放弃吧");
                switch (sel2)
                {
                    case 1:
                        Say(pc, 11000405, 131, "骑士团练习是分为东南西北组$R;" +
                            "在规定时间内争取得分的竞赛唷$R;" +
                            "$P得分的方法有好几种$R;" +
                            "「打倒对方玩家」2分$R;" +
                            "「破坏石榴石」1分$R;" +
                            "「破坏蓝玉」3分$R;" +
                            "「破坏祖母绿」5分$R;" +
                            "「破坏电气石」50分$R;" +
                            "只要完成上述的行为$R;" +
                            "就可以为自己所属团队取得分数唷$R;" +
                            "$P还想听更详细的吗？$R;");
                        int sel;
                        do
                        {
                            sel = Select(pc, "继续听吗？", "", "什么叫石榴石？", "为什么只有电气石可以得到50分呢？", "我被打倒会怎么样？", "有没有参加奖品？", "好了");
                            switch (sel)
                            {
                                case 1:
                                    Say(pc, 11000405, 131, "「石榴石」是战争中产生$R;" +
                                        "稀有的魔法物质喔$R;" +
                                        "$R除此以外，还有「蓝玉」、$R;" +
                                        "「祖母绿」、「电气石」等$R;" +
                                        "也是在战争中产生的$R;" +
                                        "$P破坏这些宝石$R;" +
                                        "就可以为自己所属团队取得分数呀$R;" +
                                        "$P且如果是持有知识技能的人$R;" +
                                        "破坏这些魔法物质的话$R;" +
                                        "还可以得到道具呢$R;" +
                                        "这些道具在骑士团练习完毕后$R;" +
                                        "是不会消失的唷$R;" +
                                        "所以生产系们，道具一定要$R;" +
                                        "保存到最后哦$R;");
                                    break;
                                case 2:
                                    Say(pc, 11000405, 131, "「电气石」是最高分的魔法物质呀，$R;" +
                                        "可以知道所有参赛者的位置喔$R;" +
                                        "$P而且破坏「电气石」的人$R;" +
                                        "参赛的时候，得分会增加两倍唷。$R;" +
                                        "$R但是有几个条件$R;" +
                                        "$P1）凭依中的人破坏电气石，$R;" +
                                        "主人可以得到双倍的分数$R;" +
                                        "$P2）如果得分2倍的人登出的话，$R;" +
                                        "其效果将转移到周围的参赛者呀。$R;" +
                                        "$P3）而若得分2倍的人死亡的话，$R;" +
                                        "其效果将转移到打败他的人身上。$R;");
                                    break;
                                case 3:
                                    Say(pc, 11000405, 131, "进行骑士团练习时，即使被打死了$R;" +
                                        "也可以得到同伴的帮助复活后$R;" +
                                        "返回储存点$R;" +
                                        "$P但是如果选择返回储存点，$R;" +
                                        "当您回到报名的大楼中庭$R;" +
                                        "$P要返回前线，必须先在那里等3分钟$R;" +
                                        "，然后再跟报名工作人员说话哦$R;" +
                                        "$P放心，骑士团练习没有死亡的惩罚$R;" +
                                        "$P并不会像格斗场一样，$R;" +
                                        "出现武器和防具$R降低耐久度的情况唷。$R;");
                                    break;
                                case 4:
                                    Say(pc, 11000405, 131, "骑士团练习结束后，$R;" +
                                        "还会给参赛全员分配基本经验值。$R;" +
                                        "$P分配标准根据以下条件来定$R;" +
                                        "1）骑士团练习参加人数$R;" +
                                        "2）自己组的顺序$R;" +
                                        "3）组裡的人数$R;" +
                                        "4）自己的得分$R;" +
                                        "5）有无拿取得各种奖赏$R;" +
                                        "$P想得到奖赏有几种条件喔，$R;" +
                                        "个人死亡的次数，$R;" +
                                        "持有瞬间最大攻击破坏$R;" +
                                        "个人所持的道具数目$R;" +
                                        "个人累积的破坏数量$R;");
                                    break;
                                case 5:
                                    Say(pc, 11000405, 131, "那么，要不要参加试试看呢？$R;");
                                    break;
                            }
                        }
                        while (sel != 5);
                        break;
                    case 2:
                        //KWAR_ENTRY 0
                        if (true)
                        {
                            Say(pc, 11000405, 131, "对不起，现在不是报名时间喔，$R;" +
                                "请等可以报名的时候，再来吧$R;");
                            return;
                        }
                        if (true)
                        {
                            Say(pc, 11000405, 131, "非常抱歉，下回的骑士团练习$R;" +
                                "有等级的限制$R;" +
                                "$R您的实力太强了，所以不能参加呀。$R;");
                            return;
                        }
                        Say(pc, 11000405, 131, "那么在这个大楼中庭报名吧。$R;");
                        switch (Global.Random.Next(1, 4))
                        {
                            case 1:
                                //WARP 752
                                break;
                            case 2:
                                //WARP 753
                                break;
                            case 3:
                                //WARP 754
                                break;
                            case 4:
                                //WARP 755
                                break;
                        }
                        break;
                }
            } while (sel2 == 1);
        }

        void 鬥技場模式切換(ActorPC pc)
        {
            if (pc.PossesionedActors.Count != 0)
            {
                Say(pc, 11000405, 131, "抱歉，$R;" +
                                       "请您解除凭依好吗?$R;" +
                                       "$R请申请参赛以后再凭依可以吗?$R;", "主人");
            }
            else
            {
                Say(pc, 11000405, 131, "知道了。$R;" +
                                       "现在给您15秒的时间做准备。$R;" +
                                       "$P好好准备一下吧!!$R;", "主人");

                pc.Mode = PlayerMode.COLISEUM_MODE;

                //SagaMap.Tasks.PC.PVPTime task = new SagaMap.Tasks.PC.PVPTime();
                //pc.Tasks.Add("PVPTime", task);
                //task.Activate();
            }
        }

        void 取消對戰模式(ActorPC pc)
        {
            Say(pc, 11000405, 131, "怎么了吗?$R;" +
                                   "$R不想要参加了呀?$R;", "主人");

            switch (Select(pc, "放弃参加吗?", "", "不放弃", "放弃参加"))
            {
                case 1:
                    break;

                case 2:
                    if (pc.PossesionedActors.Count != 0)
                    {
                        Say(pc, 11000405, 131, "抱歉，$R;" +
                                               "请您解除凭依好吗?$R;", "主人");
                    }
                    else
                    {
                        pc.Mode = PlayerMode.NORMAL;

                        Say(pc, 11000405, 131, "期待您下次的参加唷!!$R;", "主人");
                    }
                    break;
            }
        }


    }
}
