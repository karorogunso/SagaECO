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
                Say(pc, 11001231, 131, "光之塔…竟然…自己一個進去!!$R;" +
                    "$R需要我的幫忙嗎?$R;");
                Say(pc, 11001230, 131, "埃米爾…沒有忘了些什麼嗎?$R;");
                Say(pc, 11001231, 131, "啊！對不起！$R;" +
                    "$R(真是對不起…今天約好跟瑪莎$R一起去唐卡進貨的…)$R;");
                Say(pc, 11001230, 131, "好吧！我們在唐卡有事要做！$R那我們先走了！$R;" +
                    "$R回來的時候，$R坐商人行會的飛空庭就可以了！$R;");
                Say(pc, 0, 131, "謝謝您送我回來♪$R;" +
                    "$R去唐卡吧?有時間一定要到$R「莉塔活動木偶工作室」看看喔！$R;" +
                    "$R媽媽會做很好吃的餅乾招待您的！$R;", "行李裡的哈爾列爾利");
                Say(pc, 11001230, 131, "嗯！我一定會去看看的♪$R;" +
                    "$R我也會轉告她說，哈爾列爾利你很健康$R我想莉塔可能很擔心呢$R;");
                Say(pc, 0, 131, "瑪莎！埃米爾！真的謝謝！$R;" +
                    "「客人」！「客人」！$R;" +
                    "$R又變成道具給「客人」添麻煩了$R;" +
                    "$R來吧!快點找出『電腦唯讀記憶體』$R就回唐卡吧♪$R;", "行李中的哈爾列爾利");
                return;
            }
            if (Neko_05_amask.Test(Neko_05.茜子任务开始) &&
                Neko_05_cmask.Test(Neko_05.进入光塔) &&
                !Neko_05_cmask.Test(Neko_05.把電腦唯讀記憶體給哈爾列爾利))
            {
                Say(pc, 11001231, 131, "(都說對不起了！$R只是稍微忘記了而已啊…)$R;");
                Say(pc, 11001230, 131, "(太過份了！！$R3個星期前就一直在說了阿…$R;" +
                    "$R只要有冒險的事情…$R我一定會…不！其他事情都忘記了！$R;");
                if (CountItem(pc, 10017900) >= 1 && CountItem(pc, 10017905) >= 1)
                {
                    Say(pc, 0, 131, "……$R;" +
                        "$R哇啊！埃米爾真的生氣了！$R;", "“凱堤(山吹)”");
                    Say(pc, 0, 131, "嗯，埃米爾…還真有率直的一面$R;" +
                        "$R啊……?怎麼了嗎?幹嘛那麼驚訝啊?$R;", "“凱堤（桃）”");
                    Say(pc, 0, 131, "蘋果說別人壞話…還是第一次見啊…$R;", "“凱堤(山吹)”");
                    Say(pc, 0, 131, "啊！！…不是啦！！$R;" +
                        "$R這個不是壞話啊！$R;", "“凱堤（桃）”");
                    Say(pc, 0, 131, "那樣果然很率直$R;" +
                        "$R你是想說很像埃米爾對吧?$R;", "“凱堤(緑)”");
                    Say(pc, 0, 131, "不是……那樣的啦……$R;", "“凱堤（桃）”");
                    return;
                }
                return;
            }
            Say(pc, 11001231, 131, "光之塔……$R;");
        }
    }
}