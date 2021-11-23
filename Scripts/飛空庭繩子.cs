using System;
using System.Collections.Generic;
using System.Text;

using SagaDB.Actor;
using SagaMap.Scripting;

namespace SagaScript
{
    public class 飛空庭繩子 : EventActor
    {
        int hitCount = 0;

        public 飛空庭繩子()
        {
            this.EventID = 0xF0000100;
        }

        public override void OnEvent(ActorPC pc)
        {
            string input;

            if (pc == this.Actor.Caster)
            {
                switch (Select(pc, string.Format("{0}的 飛空庭 Hit : {1}", this.Actor.Caster.Name, hitCount), "", "到飛空庭", "整理繩子", "設定出入限制", "打出招牌", "什麼也不做"))
                {
                    case 1:
                        EnterFGarden(pc, this.Actor);
                        break;

                    case 2:
                        ReturnRope(pc);

                        Say(pc, 0, 0, "整理了繩子。$R;", "");
                        break;

                    case 3:

                        break;

                    case 4:
                        input = InputBox(pc, "請輸入招牌內容", InputType.PetRename);

                        if (input != "")
                            this.Actor.Title = input;
                        break;

                    case 5:
                        break;
                }
            }
            else
            {
                switch (Select(pc, string.Format("{0}的 飛空庭 Hit : {1}", this.Actor.Caster.Name, hitCount), "", "到飛空庭", "什麼也不做"))
                {
                    case 1:
                        EnterFGarden(pc, this.Actor);
                        break;

                    case 2:
                        break;
                }
            }

            hitCount++;
        }

        public override EventActor Clone()
        {
            return new 飛空庭繩子();
        }
    }
}