
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
    public class S80000500 : Event
    {
        public S80000500()
        {
            this.EventID = 80000500;
        }

        public override void OnEvent(ActorPC pc)
        {
            Say(pc, 80000500, 131, "咦？你是新玩家吗？", "天宫希");
            Say(pc, 80000500, 131, "想要开始游戏的话~$R就走到旁边的那个光圈里就好啦！", "天宫希");
        }
    }
}