using System;
using System.Collections.Generic;
using System.Text;
using SagaDB.Actor;
using SagaMap.Scripting;
using SagaLib;
using SagaScript.Chinese.Enums;
namespace SagaScript.M50002000
{
    public class S11000877 : Event
    {
        public S11000877()
        {
            this.EventID = 11000877;
        }

        public override void OnEvent(ActorPC pc)
        {
            BitMask<Neko_04> Neko_04_amask = pc.AMask["Neko_04"];
            BitMask<Neko_04> Neko_04_cmask = pc.CMask["Neko_04"];

            if (!Neko_04_cmask.Test(Neko_04.交出商人的傳達品))
            {
                Say(pc, 11000877, 131, "在和平的阿克罗波利斯上城$R居然发生这种事情呀$R;");
                return;
            }
            if (Neko_04_amask.Test(Neko_04.任務開始) &&
                !Neko_04_amask.Test(Neko_04.任務結束) &&
                Neko_04_cmask.Test(Neko_04.被告知襲擊) && 
                Neko_04_cmask.Test(Neko_04.與嚮導對話) &&
                !Neko_04_cmask.Test(Neko_04.被詢問犯人的事))
            {
                if (Neko_04_amask.Test(Neko_04.任務開始) &&
                !Neko_04_amask.Test(Neko_04.任務結束) &&
                Neko_04_cmask.Test(Neko_04.交出商人的傳達品) &&
                !Neko_04_cmask.Test(Neko_04.收到商人的傳達品))
                {
                    return;
                }
                if (Neko_04_amask.Test(Neko_04.任務開始) &&
                !Neko_04_amask.Test(Neko_04.任務結束) &&
                Neko_04_cmask.Test(Neko_04.收到商人的傳達品) &&
                !Neko_04_cmask.Test(Neko_04.被告知襲擊))
                {
                    return;
                }
                Neko_04_cmask.SetValue(Neko_04.被詢問犯人的事, true);
                Say(pc, 11000877, 131, "再问您一次$R您真的没见到犯人吗？$R;" +
                    "$R…$R知道了$R;" +
                    "$R恕我刚才无礼，现在放您走吧，您现在自由了$R;" +
                    "$P混成骑士团成员！$R;" +
                    "$R犯人可能$R还在阿克罗波利斯上城的某个地方$R建议暂时封锁上城的出入吧$R;");
                Say(pc, 11000879, 131, "什么？$R;" +
                    "那么那是谁的责任呢？$R;");
                Say(pc, 11000877, 131, "责任？$R;" +
                    "$R维持阿克罗波利斯的$R治安是我们的责任不是吗？！$R;");
                Say(pc, 0, 131, "喵！喵！！喵喵喵！$R;" +
                    "$R喵！喵！！喵喵喵！$R;");
                Say(pc, 0, 131, "…猫又哭了…$R;" +
                    "$R！！…是不是知道…猫灵就是犯人$R;");
                return;
            }
            if (Neko_04_amask.Test(Neko_04.任務開始) &&
                !Neko_04_amask.Test(Neko_04.任務結束) &&
                Neko_04_cmask.Test(Neko_04.被詢問犯人的事) &&
                !Neko_04_cmask.Test(Neko_04.被告知去找機器人))
            {
                Say(pc, 11000877, 131, "在和平的阿克罗波利斯上城$R居然发生这种事情呀$R;");
                return;
            }
            if (!Neko_04_amask.Test(Neko_04.任務開始))
            {
                Say(pc, 11000877, 131, "在和平的阿克罗波利斯上城$R居然发生这种事情呀$R;");
                return;
            }
            if (Neko_04_amask.Test(Neko_04.任務結束))
            {
                Say(pc, 11000877, 131, "在和平的阿克罗波利斯上城$R居然发生这种事情呀$R;");
                return;
            }
            Neko_04_cmask.SetValue(Neko_04.被告知襲擊, true);
            Say(pc, 11000877, 131, "回过神了$R;" +
                "$R不知被谁袭击，在这里昏过去了$R;" +
                "$R商人总管也一起晕过去了$R;" +
                "$P总之…幸好不是很严重啊$R;" +
                "$R虽然不知道为什么被袭击$R不知道有没有丢东西呀？$R;");
            Say(pc, 0, 131, "…!!$R;" +
                "陶瓷盒呢？$R;");
            Say(pc, 11000877, 131, "哎呀！看样子被偷了$R;" +
                "$R陶瓷盒？只有那个吗？$R里面有什么？$R;" +
                "$P不知道？…$R啊…是受人之托的吧$R;");
            Say(pc, 11000877, 131, "在这和平的阿克罗波利斯上城…$R;" +
                "$R真不敢相信在$R阿克罗波利斯中心的行会宫殿$R居然发生强盗事件呀$R;" +
                "$P丢了重要的东西，真可惜$R;" +
                "$R在调查现场之前，请先待在这里吧$R;");
        }
    }
}