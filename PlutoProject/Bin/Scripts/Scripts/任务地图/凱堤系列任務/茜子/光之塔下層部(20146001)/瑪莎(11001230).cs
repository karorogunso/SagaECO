using System;
using System.Collections.Generic;
using System.Text;

using SagaDB.Actor;
using SagaDB.Item;
using SagaMap.Scripting;

using SagaLib;
using SagaScript.Chinese.Enums;
namespace SagaScript.M20146001
{
    public class S11001230 : Event
    {
        public S11001230()
        {
            this.EventID = 11001230;
        }

        public override void OnEvent(ActorPC pc)
        {
            BitMask<Neko_05> Neko_05_amask = pc.AMask["Neko_05"];
            BitMask<Neko_05> Neko_05_cmask = pc.CMask["Neko_05"];

            if (Neko_05_amask.Test(Neko_05.茜子任务开始) &&
                Neko_05_cmask.Test(Neko_05.了解到需要记忆体) &&
                !Neko_05_cmask.Test(Neko_05.理解记忆体的出处))
            {
                Neko_05_cmask.SetValue(Neko_05.理解记忆体的出处, true);
                Say(pc, 11001230, 131, "『电脑只读记忆体』可以在光之塔的$R「机件」中挖掘出来$R;" +
                    "$P但…负责挖掘的行会成员…$R拿走了很多之后，才说发现了机件$R所以具体在哪裡，我也不知道$R;");
                Neko_05_cmask.SetValue(Neko_05.进入光塔, true);
                Say(pc, 11001231, 131, "光之塔…竟然…自己一个进去!!$R;" +
                    "$R需要我的帮忙吗?$R;");
                Say(pc, 11001230, 131, "埃米尔…没有忘了些什么吗?$R;");
                Say(pc, 11001231, 131, "啊！对不起！$R;" +
                    "$R(真是对不起…今天约好跟玛莎$R一起去唐卡进货的…)$R;");
                Say(pc, 11001230, 131, "好吧！我们在唐卡有事要做！$R那我们先走了！$R;" +
                    "$R回来的时候，$R坐商人行会的飞空庭就可以了！$R;");
                Say(pc, 0, 131, "谢谢您送我回来♪$R;" +
                    "$R去唐卡吧?有时间一定要到$R「莉塔活动木偶工作室」看看喔！$R;" +
                    "$R妈妈会做很好吃的饼干招待您的！$R;", "行李里的哈利路亚");
                Say(pc, 11001230, 131, "嗯！我一定会去看看的♪$R;" +
                    "$R我也会转告她说，哈利路亚你很健康$R我想莉塔可能很担心呢$R;");
                Say(pc, 0, 131, "玛莎！埃米尔！真的谢谢！$R;" +
                    "「客人」！「客人」！$R;" +
                    "$R又变成道具给「客人」添麻烦了$R;" +
                    "$R来吧!快点找出『电脑只读记忆体』$R就回唐卡吧♪$R;", "行李里的哈利路亚");
                return;
            }
            if ((Neko_05_amask.Test(Neko_05.茜子任务开始) &&
                Neko_05_cmask.Test(Neko_05.理解记忆体的出处) &&
                !Neko_05_cmask.Test(Neko_05.进入光塔)) ||
                (Neko_05_amask.Test(Neko_05.茜子任务开始) &&
                Neko_05_cmask.Test(Neko_05.进入光塔) && 
                !Neko_05_cmask.Test(Neko_05.要求与跟瑪莎打招呼)))
            {
                Say(pc, 11001230, 131, "『电脑只读记忆体』可以在光之塔$R「机件」中挖掘出来$R;" +
                    "$R但是光之塔内有非常强的机器魔物$R要小心啊$R;" +
                    "$R不要勉强啊！$R;");
                return;
            }
            Say(pc, 11001230, 131, "这就是光之塔……$R;" +
                "$R每次来都觉得这里真是凄凉的地方…$R;");
            Say(pc, 11001231, 131, "嗯……$R;");

        }
    }
}