using System;
using System.Collections.Generic;
using System.Text;

using SagaDB.Actor;
using SagaMap.Scripting;

namespace SagaScript
{
    public class S1110 : Event
    {
        public S1110()
        {
            this.EventID = 1110;
        }

        public override void OnEvent(ActorPC pc)
        {
            switch (Select(pc, "要抽哪一类发饰品？", "", "可爱的发簪", "爆炸头假发和头上小猫", "好看的头带", "学生戴的饰物", "战国武将的帽子", "好看的头巾", "サムライのリボン"))
            {
                case 1:
                if (CountItem(pc, 21050110) > 0)
                {
                GiveRandomTreasure(pc, "head1");
                TakeItem(pc, 21050110, 1);
                }
                    break;
                case 2:
                if (CountItem(pc, 21050110) > 0)
                {
                GiveRandomTreasure(pc, "head2");
                TakeItem(pc, 21050110, 1);
                }
                    break;
                case 3:
                if (CountItem(pc, 21050110) > 0)
                {
                GiveRandomTreasure(pc, "head3");
                TakeItem(pc, 21050110, 1);
                }
                    break;
                case 4:
                if (CountItem(pc, 21050110) > 0)
                {
                GiveRandomTreasure(pc, "head4");
                TakeItem(pc, 21050110, 1);
                }
                    break;
                case 5:
                if (CountItem(pc, 21050110) > 0)
                {
                GiveRandomTreasure(pc, "head5");
                TakeItem(pc, 21050110, 1);
                }
                    break;
                case 6:
                if (CountItem(pc, 21050110) > 0)
                {
                GiveRandomTreasure(pc, "head6");
                TakeItem(pc, 21050110, 1);
                }
                    break;
                case 7:
                if (CountItem(pc, 21050110) > 0)
                {
                GiveRandomTreasure(pc, "head7");
                TakeItem(pc, 21050110, 1);
                }               
                    break;
            }
        }
    }
}