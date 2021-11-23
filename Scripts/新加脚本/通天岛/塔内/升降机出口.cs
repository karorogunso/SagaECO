
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
    public class S12001162 : Event
    {
        public S12001162()
        {
            this.EventID = 12001162;
        }
        public override void OnEvent(ActorPC pc)
        {
            switch (Select(pc, "要怎么操作呢？", "", "前往上层", "前往中层", "前往下层（尚未开放）", "什么也不做"))
            {
                case 1:
                    ShowEffect(pc, 5121);
                    Wait(pc, 1000);
                    Warp(pc, 11058000, 127, 158);
                    break;
                case 2:
                    ShowEffect(pc, 5121);
                    Wait(pc, 1000);
                    Warp(pc, 10058000, 127, 158);
                    break;
            }
        }
    }
    public class S10001546 : Event
    {
        public S10001546()
        {
            this.EventID = 10001546;
        }

        public override void OnEvent(ActorPC pc)
        {
            switch (Select(pc, "要怎么操作呢？", "", "前往上层", "前往中层", "前往下层（尚未开放）", "什么也不做"))
            {
                case 1:
                    ShowEffect(pc, 5121);
                    Wait(pc, 1000);
                    Warp(pc, 11058000, 127, 158);
                    break;
                case 2:
                    ShowEffect(pc, 5121);
                    Wait(pc, 1000);
                    Warp(pc, 10058000, 127, 158);
                    break;
            }
        }
    }
}