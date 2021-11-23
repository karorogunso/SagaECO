
using System;
using System.Collections.Generic;
using System.Text;
using SagaLib;
using SagaDB.Actor;
using SagaDB.Item;
using SagaMap.Scripting;
using SagaScript.Chinese.Enums;
using WeeklyExploration;
using System.Globalization;
namespace SagaScript.M30210000
{
    public partial class S100000100 : Event
    {
        public S100000100()
        {
            this.EventID = 100000100;
        }
        public override void OnEvent(ActorPC pc)
        {
            ChangeMessageBox(pc);
            NPCMove(pc, 60000006, 142, 139, 300, 6, 0xb, 121, 10, 0);
            Wait(pc, 2000);
            Say(pc, 0, "啊，是你。", "沙月");
            Say(pc, 0, "看起来你似乎又要开始新的冒险了啊", "沙月");
            Say(pc, 0, "嗯……算是吧？", pc.Name);
            Say(pc, 60000006, 2138, "衷心祝愿你有个愉快的旅途。", "沙月");
            NPCMove(pc, 60000006, 139, 138, 300, 3, 0xb, 121, 10, 0);
            Wait(pc, 1000);
            Say(pc, 0, "谢谢。$R$R啊……你知道东方地牢的魔狼一族吗？", pc.Name);
            Wait(pc, 2000);
            NPCMove(pc, 60000006, 139, 138, 300, 6, 0xb, 121, 10, 0);
            Say(pc, 60000006, 131, "嗯？东方地牢吗，恐怕我也不是很清楚。$R我也只是和你们一样的外来冒险者，$R在这之前我对东国的了解也只停留在道听途说的层面上。", "沙月");
            Say(pc, 60000006, 131, "东之国自从之前的灾难之后就已经成为一座死城，城中所有生物无一幸免。$R并且这股污染还有进一步扩散的趋势。$R事实上，为了避免污染的扩散，$R现在整个东国通向外界的道路都已经被封锁了", "沙月");
            Say(pc, 60000006, 131, "东方地牢作为东国的一部分，受到的影响要比周边区域还要大得多。$R我一直以为那里面也已经变成活死人的游乐园了。$R想不到居然还有顽强地存活着的生命留在那里。", "沙月");
            Say(pc, 60000006, 2133, "嗯……？", "沙月");
            Wait(pc, 1000);
            Say(pc, 60000006, 0, "……好奇怪啊，我来到这个岛屿的时间应该比你们要早很多，但是迄今为止我从来没听说过这个岛上还有医生。", "沙月");
            Say(pc, 0, "虽然不知道是不是我多心了…请千万小心，我总觉得事情恐怕没这么简单。", "沙月");
            Say(pc, 2133, "不过，东方地牢吗…$R真让人在意。$R还有传说中的魔狼一族也是……", "沙月");
            pc.CInt["东牢进入任务"] = 2;
        }
    }
}   