
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
    public class S80000700: Event
    {
        public S80000700()
        {
            this.EventID = 80000700;
        }

        public override void OnEvent(ActorPC pc)
        {
            if (pc.CInt["魔导师转职任务"] == 2)//此段为NPC禁断之书使用
            {
                Say(pc, 0, "这就是夏影说的禁断之书吧。", pc.Name);
                Say(pc, 0, "（你把书拿起来收进背包里）", " ");
                GiveItem(pc, 140000000, 1);//禁断之书
                pc.CInt["魔导师转职任务"] = 3;
                return;
            }
            else if(pc.CInt["魔导师转职任务"] == 3)
            {
                Say(pc, 0, "把它带回去给夏影吧。");
                return;
            }
                Say(pc, 0, "不道是谁遗落在这里的书。", " ");
                Say(pc, 0, "还是不要乱动吧，说不定会被诅咒的。", " ");
        }
    }
}