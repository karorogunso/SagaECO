using System;
using System.Collections.Generic;
using System.Text;
using SagaLib;
using SagaDB.Actor;
using SagaMap.Scripting;
using SagaScript.Chinese.Enums;
namespace SagaScript.M30141003
{
    public class S11000780 : Event
    {
        public S11000780()
        {
            this.EventID = 11000780;
        }

        public override void OnEvent(ActorPC pc)
        {
            BitMask<Crusade_Pluto> Crusade_Pluto_mask = pc.CMask["Crusade_Pluto"];

            if (Crusade_Pluto_mask.Test(Crusade_Pluto.拒絕討伐))
            {
                Say(pc, 11000780, 131, "$R呐！从那个传送点$R回到您原来的地方吧！$R;");
                return;
            }
            if (Crusade_Pluto_mask.Test(Crusade_Pluto.幫助討伐))
            {
                Say(pc, 11000780, 131, "拜託，如果发现冥王的话$R帮我消灭它$R;");
                return;
            }
            Say(pc, 11000780, 131, "您到底…？！$R从哪里来？$R;" +
                "$P啊是那样…又开始了$R;" +
                "$P这里危险!快回到原来的地方吧!$R;" +
                "$R来!从那个传送点$R回到您原来的地方!$R;");

            switch (Select(pc, "怎么办?", "", "不知道说什么，还是回去吧", "…这是什么地方？"))
            {
                case 1:
                    Crusade_Pluto_mask.SetValue(Crusade_Pluto.拒絕討伐, true);
                    break;
                case 2:
                    Say(pc, 11000780, 131, "…这裡是封印地狱怪兽「冥王」的$R地方…$R;" +
                        "$P我叫普罗莎娜$R是多米尼翁的神天使$R;" +
                        "$R负责监狱的监视工作$R;" +
                        "$P从您那里能感受到$R「赤铜宝珠」散发出来的黑暗波动$R;" +
                        "$R就是那个波动引来了魔物阿$R;");
                    switch (Select(pc, "怎么办?", "", "不知道説什么，但是回去吧", "…冥王是什么?"))
                    {
                        case 1:
                            Crusade_Pluto_mask.SetValue(Crusade_Pluto.拒絕討伐, true);
                            break;
                        case 2:
                            Say(pc, 11000780, 131, "冥王是这个世界里的地狱怪兽$R;" +
                                "$P宗族处罚我要监视着$R封印冥王的监狱$R;" +
                                "$P这个监狱建在次元之间$R和任何一个世界都不相通$R;" +
                                "$P就在次元的裂缝中，冥王逃跑了$R就像断线的风筝一样，不知去向$R;" +
                                "$R…可能去了埃米尔世界吧$R;" +
                                "$P您拥有只有我们家族的背德者$R才拥有的「赤铜宝珠」力量$R;" +
                                "$P能不能代替我出去$R找找冥王的行踪呢？$R;");
                            switch (Select(pc, "怎么办?", "", "帮他", "算了"))
                            {
                                case 1:
                                    Say(pc, 11000780, 131, "…谢谢您接受我的委托$R;" +
                                        "$R冥王喜欢又黑又湿的洞窟$R;");

                                    Say(pc, 11000780, 131, "委托，如果发现冥王的话$R帮我消灭它$R;");
                                    Crusade_Pluto_mask.SetValue(Crusade_Pluto.拒絕討伐, false);
                                    Crusade_Pluto_mask.SetValue(Crusade_Pluto.幫助討伐, true);
                                    break;
                                case 2:
                                    Say(pc, 11000780, 131, "…是吗？$R很抱歉，向您提出这么无理的要求$R;" +
                                        "$R来!从那个传送点$R回到您原来的地方吧!$R;");
                                    Crusade_Pluto_mask.SetValue(Crusade_Pluto.拒絕討伐, true);
                                    break;
                            }
                            break;
                    }
                    break;
            }
        }
    }
}