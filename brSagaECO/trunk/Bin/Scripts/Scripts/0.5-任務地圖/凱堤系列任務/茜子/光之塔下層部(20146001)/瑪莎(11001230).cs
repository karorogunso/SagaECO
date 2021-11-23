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
                Say(pc, 11001230, 131, "『電腦唯讀記憶體』可以在光之塔的$R「機件」中挖掘出來$R;" +
                    "$P但…負責挖掘的行會成員…$R拿走了很多之後，才說發現了機件$R所以具體在哪裡，我也不知道$R;");
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
            if ((Neko_05_amask.Test(Neko_05.茜子任务开始) &&
                Neko_05_cmask.Test(Neko_05.理解记忆体的出处) &&
                !Neko_05_cmask.Test(Neko_05.进入光塔)) ||
                (Neko_05_amask.Test(Neko_05.茜子任务开始) &&
                Neko_05_cmask.Test(Neko_05.进入光塔) && 
                !Neko_05_cmask.Test(Neko_05.要求与跟瑪莎打招呼)))
            {
                Say(pc, 11001230, 131, "『電腦唯讀記憶體』可以在光之塔$R「機件」中挖掘出來$R;" +
                    "$R但是光之塔內有非常強的機器魔物$R要小心啊$R;" +
                    "$R不要勉強啊！$R;");
                return;
            }
            Say(pc, 11001230, 131, "這就是光之塔……$R;" +
                "$R每次來都覺得這裡真是淒涼的地方…$R;");
            Say(pc, 11001231, 131, "嗯……$R;");

        }
    }
}