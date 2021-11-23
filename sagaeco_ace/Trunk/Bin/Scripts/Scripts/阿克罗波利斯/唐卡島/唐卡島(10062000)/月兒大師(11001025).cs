using System;
using System.Collections.Generic;
using System.Text;
using SagaLib;
using SagaDB.Actor;
using SagaMap.Scripting;
using SagaScript.Chinese.Enums;
namespace SagaScript.M10062000
{
    public class S11001025 : Event
    {
        public S11001025()
        {
            this.EventID = 11001025;
        }

        public override void OnEvent(ActorPC pc)
        {
            BitMask<Neko_05> Neko_05_amask = pc.AMask["Neko_05"];
            BitMask<Neko_05> Neko_05_cmask = pc.CMask["Neko_05"];
            BitMask<FGarden> fgarden = pc.AMask["FGarden"];

            if (Neko_05_amask.Test(Neko_05.茜子任务开始) &&
                Neko_05_cmask.Test(Neko_05.开始指导) &&
                !Neko_05_cmask.Test(Neko_05.告知需寻找工匠))
            {
                Neko_05_cmask.SetValue(Neko_05.告知需寻找工匠, true);
                Say(pc, 0, 131, "是他吗？$R;");
                Say(pc, 11001025, 131, "嗯？什么事情呢？$R;");
                Say(pc, 0, 131, "想问一下关于飞空庭引擎消息。$R;", "行李里的哈利路亚");
                Say(pc, 11001025, 131, "哇呀…$R;" +
                    "$R哦…还以为什么呢，$R原来是活动木偶石像呀$R;" +
                    "$R是在行李里头吧。$R;" +
                    "$P想知道关于飞空庭的引擎是吗？$R;" +
                    "$R嗯……$R;" +
                    "$P我认识一个精通飞空庭引擎的工匠，$R;" +
                    "$R不过他现在在阿克罗尼亚大陆喔$R;" +
                    "那是因为…自己的飞空庭发生故障时$R答应冒险者帮他们做飞空庭，$R就接受了他们的帮助。$R;" +
                    "所以他说，$R现在接到了很多飞空庭订单，$R一时是回不来的。$R;");
                if (fgarden.Test(FGarden.第一次和飛空庭匠人說話))
                {
                    Say(pc, 0, 131, "啊$R;");
                    Say(pc, 0, 131, "怎么了？$R;" +
                        "$R啊，是不是认识的人呀？$R;" +
                        "$P那么送过去吧。$R;", "行李里的哈利路亚");
                    Say(pc, 0, 131, "哎呀，怎么办..被发现了$R;" +
                        "$R法伊斯特街道…太远了$R;");
                    return;
                }
                Say(pc, 0, 131, "原来如此，不过都已经来到这里了…$R;", "行李里的哈利路亚");
                return;
            }
            Say(pc, 11001025, 131, "这飞空庭漂亮吧？$R;");
            if (Neko_05_amask.Test(Neko_05.茜子任务开始) &&
                Neko_05_cmask.Test(Neko_05.告知需寻找工匠) &&
                !Neko_05_cmask.Test(Neko_05.收到碎紙))
            {
                Say(pc, 0, 131, "　做什么呢，『客人』$R;" +
                    "$R快去看引擎吧。$R;", "行李里的哈利路亚");
                return;
            }
        }
    }
}