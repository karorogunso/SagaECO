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
                Say(pc, 11000882, 134, "怎么会发生这种事情…$R;");
                return;
            }
            if (!Neko_04_amask.Test(Neko_04.任務開始))
            {
                Say(pc, 11000882, 134, "怎么会发生这种事情…$R;");
                return;
            }
            if (!Neko_04_cmask.Test(Neko_04.被告知襲擊))
            {
                Say(pc, 11000882, 134, "怎么会发生这种事情…$R;");
                return;
            }
            if (Neko_04_amask.Test(Neko_04.任務開始) &&
                !Neko_04_amask.Test(Neko_04.任務結束) &&
                Neko_04_cmask.Test(Neko_04.與嚮導對話) &&
                !Neko_04_cmask.Test(Neko_04.被詢問犯人的事))
            {
                Say(pc, 11000882, 134, "呜呜…对不起…$R;");
                return;
            }
            if (!Neko_04_amask.Test(Neko_04.任務開始))
            {
                Say(pc, 11000882, 134, "怎么会发生这种事情…$R;");
                return;
            }
            if (Neko_04_amask.Test(Neko_04.任務結束))
            {
                Say(pc, 11000882, 134, "怎么会发生这种事情…$R;");
                return;
            }
            Neko_04_cmask.SetValue(Neko_04.與嚮導對話, true);
            Say(pc, 11000882, 134, "…对不起！！$R;" +
                "$R我一直跟往常一样$R检查进出入口之人的身份$R;" +
                "$P却放走了那个危险的傢伙$R;" +
                "$R真不知说什么好…$R真的非常抱歉!!$R;");
            Say(pc, 11000877, 131, "不是你的错，不要再哭了$R;" +
                "$R在阿克罗波利斯上城的$R东南西北门都在调查可疑的人$R;" +
                "$P和平的阿克罗波利斯上城$R居然让这样的人潜入$R这是问题的严重所在$R;");
            Say(pc, 11000880, 131, "不可能!$R不可以这么说！$R;" +
                "$R评议会觉得我们$R警备员闲着没事干吗？$R;");
            Say(pc, 11000879, 131, "我们的检查很完善！$R不要乱说话呀$R;");
            Say(pc, 11000878, 131, "真的没这可能吗？$R;" +
                "$R可能是评议会要让混成骑士团$R陷入困境而做的一场戏阿…$R;");
            Say(pc, 11000877, 131, "什么！$R;" +
                "$R这真是岂有此理！$R;");
            Say(pc, 0, 131, "…真烦$R;" +
                "$R大人们怎么说话都那样…$R把责任推来推去的$R;", "\"猫灵（山吹）\"");
            Say(pc, 0, 131, "嗯…就是吧…哎!…$R看见了吗？$R;" +
                "$R好像有可疑的孩子$R;", "\"猫灵（桃子）\"");
            Say(pc, 0, 131, "看见了，挺可疑的$R;" +
                "$R主人被人顶撞$R我们不可能没有觉察到呀$R;", "\"猫灵（山吹）\"");
        }
    }
}