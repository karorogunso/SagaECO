
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
    public class S80000602: Event
    {
        public S80000602()
        {
            this.EventID = 80000602;
        }

        public override void OnEvent(ActorPC pc)
        {
            Say(pc, 0, "呐，$R你知道法伊斯特那边为何封锁起来了吗？$R是出什么事了吗？", "棕发双马尾的少女");
        }
    }
}