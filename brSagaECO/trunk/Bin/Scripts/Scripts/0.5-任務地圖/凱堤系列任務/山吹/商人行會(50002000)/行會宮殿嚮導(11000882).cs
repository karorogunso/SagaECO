using System;
using System.Collections.Generic;
using System.Text;
using SagaDB.Actor;
using SagaMap.Scripting;
using SagaLib;
using SagaScript.Chinese.Enums;
namespace SagaScript.M50002000
{
    public class S11000882 : Event
    {
        public S11000882()
        {
            this.EventID = 11000882;
        }

        public override void OnEvent(ActorPC pc)
        {
            BitMask<Neko_04> Neko_04_amask = pc.AMask["Neko_04"];
            BitMask<Neko_04> Neko_04_cmask = pc.CMask["Neko_04"];
            if (Neko_04_amask.Test(Neko_04.任務開始) &&
                !Neko_04_amask.Test(Neko_04.任務結束) && 
                Neko_04_cmask.Test(Neko_04.被詢問犯人的事) &&
                !Neko_04_cmask.Test(Neko_04.被告知去找機器人))
            {
                Say(pc, 11000882, 134, "怎麼會發生這種事情…$R;");
                return;
            }
            if (!Neko_04_amask.Test(Neko_04.任務開始))
            {
                Say(pc, 11000882, 134, "怎麼會發生這種事情…$R;");
                return;
            }
            if (!Neko_04_cmask.Test(Neko_04.被告知襲擊))
            {
                Say(pc, 11000882, 134, "怎麼會發生這種事情…$R;");
                return;
            }
            if (Neko_04_amask.Test(Neko_04.任務開始) &&
                !Neko_04_amask.Test(Neko_04.任務結束) &&
                Neko_04_cmask.Test(Neko_04.與嚮導對話) &&
                !Neko_04_cmask.Test(Neko_04.被詢問犯人的事))
            {
                Say(pc, 11000882, 134, "嗚嗚…對不起…$R;");
                return;
            }
            if (!Neko_04_amask.Test(Neko_04.任務開始))
            {
                Say(pc, 11000882, 134, "怎麼會發生這種事情…$R;");
                return;
            }
            if (Neko_04_amask.Test(Neko_04.任務結束))
            {
                Say(pc, 11000882, 134, "怎麼會發生這種事情…$R;");
                return;
            }
            Neko_04_cmask.SetValue(Neko_04.與嚮導對話, true);
            Say(pc, 11000882, 134, "…對不起！！$R;" +
                "$R我一直跟往常一樣$R檢查進出入口之人的身份$R;" +
                "$P卻放走了那個危險的傢伙$R;" +
                "$R真不知說什麼好…$R真的非常抱歉!!$R;");
            Say(pc, 11000877, 131, "不是你的錯，不要再哭了$R;" +
                "$R在阿高普路斯市上城的$R東南西北門都在調查可疑的人$R;" +
                "$P和平的阿高普路斯市上城$R居然讓這樣的人潛入$R這是問題的嚴重所在$R;");
            Say(pc, 11000880, 131, "不可能!$R不可以這麼說！$R;" +
                "$R評議會覺得我們$R警備員閒著沒事幹嗎？$R;");
            Say(pc, 11000879, 131, "我們的檢查很完善！$R不要亂說話呀$R;");
            Say(pc, 11000878, 131, "真的沒這可能嗎？$R;" +
                "$R可能是評議會要讓混城騎士團$R陷入困境而做的一場戲阿…$R;");
            Say(pc, 11000877, 131, "什麼！$R;" +
                "$R這真是豈有此理！$R;");
            Say(pc, 0, 131, "…真煩$R;" +
                "$R大人們怎麼說話都那樣…$R把責任推來推去的$R;", "\"凱堤（山吹）\"");
            Say(pc, 0, 131, "嗯…就是吧…哎!…$R看見山吹了嗎？$R;" +
                "$R好像有可疑的孩子$R;", "\"凱堤（桃）\"");
            Say(pc, 0, 131, "看見了，挺可疑的$R;" +
                "$R主人被人頂撞$R我們不可能沒有覺察到呀$R;", "\"凱堤（山吹）\"");
        }
    }
}