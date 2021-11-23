
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
    public class S80000802: Event
    {
        public S80000802()
        {
            this.EventID = 80000802;
        }

        public override void OnEvent(ActorPC pc)
        {
            ChangeMessageBox(pc);
            Say(pc, 0, "这么冷的天，会着凉的，我们就在这里静静的看不好吗？", "兔麻麻");
        }
    }
}