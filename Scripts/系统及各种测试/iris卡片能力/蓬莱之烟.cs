
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
    public class S70182030: Event
    {
        public S70182030()
        {
            this.EventID = 70182030;
        }

        public override void OnEvent(ActorPC pc)
        {
            switch (Select(pc, "是否通过蓬莱之烟进行复活？", "", "是", "否"))
            {
                case 1:
                    Revive(pc,6);
                    pc.EP += pc.MaxHP;
                    break;
                case 2:
                    break;
            }
        }
    }
}