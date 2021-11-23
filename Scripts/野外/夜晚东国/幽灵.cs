
using System;
using System.Collections.Generic;
using System.Text;
using SagaLib;
using SagaDB.Actor;
using SagaDB.Item;
using SagaMap;
using SagaMap.Skill;
using SagaMap.Scripting;
using SagaScript.Chinese.Enums;
namespace SagaScript.M30210000
{
    public class S80000701 : Event
    {
        public S80000701()
        {
            this.EventID = 80000701;
        }

        public override void OnEvent(ActorPC pc)
        {
            if (pc.CInt["灾祸的见证者任务"] == 3)
            {
                Say(pc, 0, "（再向少女追问$R似乎也问不出什么线索的样子。）", pc.Name);
                Say(pc, 0, "（关于她的记忆，$R去问问精通魔法现象的『$CR那个人$CD』吧）", pc.Name);
                switch (Select(pc, " ", "", "……我是来交任务的！", "回去找『那个人』"))
{case 1:
HandleQuest(pc, 5);
break;
case2:
return;
}
return;
            }
                if (pc.CInt["灾祸的见证者任务"] == 2)
                switch (Select(pc, " ", "", "接受任务委托", "你知道东之国曾经发生的事吗？"))
                {
                    case 1:
                        HandleQuest(pc, 5);
                        return;
                    case 2:
                        Say(pc, 0, "……", "幽灵少女");
                        Wait(pc, 600);
                        Say(pc, 527, "啊……？你…是在问我吗？", "幽灵少女");
                        Say(pc, 0, "抱歉…我不知道你在说什么", "幽灵少女");
                        Select(pc, " ", "", "你…难道不是这里的居民吗？");
                        Say(pc, 529, "我不知道…我的记忆很模糊。", "幽灵少女");
                        Say(pc, 529, "我从来没考虑过，为什么我要在这里……做这些事…", "幽灵少女");
                        Wait(pc, 1000);
                        Say(pc, 0, "（少女似乎想不起来自己为什么会在这里$R$R会不会有什么可能认识她的人呢…）", pc.Name);
                        Say(pc, 0, "（对了，$R去问问精通魔法现象的『$CR那个人$CD』吧）", pc.Name);
                        pc.CInt["灾祸的见证者任务"] = 3;
                        return;
                }
            else
                HandleQuest(pc, 5);

        }
    }
}

