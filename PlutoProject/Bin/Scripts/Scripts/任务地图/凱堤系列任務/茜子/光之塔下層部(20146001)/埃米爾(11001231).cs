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
    public class S11001231 : Event
    {
        public S11001231()
        {
            this.EventID = 11001231;
        }

        public override void OnEvent(ActorPC pc)
        {
            BitMask<Neko_05> Neko_05_amask = pc.AMask["Neko_05"];
            BitMask<Neko_05> Neko_05_cmask = pc.CMask["Neko_05"];

            if (Neko_05_amask.Test(Neko_05.茜子任务开始) &&
                Neko_05_cmask.Test(Neko_05.理解记忆体的出处) &&
                !Neko_05_cmask.Test(Neko_05.进入光塔))
            {
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
            if (Neko_05_amask.Test(Neko_05.茜子任务开始) &&
                Neko_05_cmask.Test(Neko_05.进入光塔) &&
                !Neko_05_cmask.Test(Neko_05.把電腦唯讀記憶體給哈爾列爾利))
            {
                Say(pc, 11001231, 131, "(都说对不起了！$R只是稍微忘记了而已啊…)$R;");
                Say(pc, 11001230, 131, "(太过份了！！$R3个星期前就一直在说了阿…$R;" +
                    "$R只要有冒险的事情…$R我一定会…不！其他事情都忘记了！$R;");
                if (CountItem(pc, 10017900) >= 1 && CountItem(pc, 10017905) >= 1)
                {
                    Say(pc, 0, 131, "……$R;" +
                        "$R哇啊！埃米尔真的生气了！$R;", "“猫灵(山吹)”");
                    Say(pc, 0, 131, "嗯，埃米尔…还真有率直的一面$R;" +
                        "$R啊……?怎么了吗?干嘛那么惊讶啊?$R;", "“猫灵（桃子）”");
                    Say(pc, 0, 131, "桃子说别人坏话…还是第一次见啊…$R;", "“猫灵（山吹）”");
                    Say(pc, 0, 131, "啊！！…不是啦！！$R;" +
                        "$R这个不是坏话啊！$R;", "“猫灵（桃子）”");
                    Say(pc, 0, 131, "那样果然很率直$R;" +
                        "$R你是想说很像埃米尔对吧?$R;", "“猫灵（绿子）”");
                    Say(pc, 0, 131, "不是……那样的啦……$R;", "“猫灵（桃子）”");
                    return;
                }
                return;
            }
            Say(pc, 11001231, 131, "光之塔……$R;");
        }
    }
}