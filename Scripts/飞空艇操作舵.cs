using System;
using System.Collections.Generic;
using System.Text;
using SagaLib;
using SagaDB.Actor;
using SagaMap.Scripting;
using SagaScript.Chinese.Enums;

namespace SagaScript
{
    public class 飛空庭輪舵 : Event
    {
        public 飛空庭輪舵()
        {
            this.EventID = 12001110;
        }

        public override void OnEvent(ActorPC pc)
        {
            string input;
            ActorPC owner = GetFGardenOwner(pc);

            if (pc == owner)
            {
                switch (Select(pc, "想要做什麼呢?", "",  "從飛空庭下來", "打出招牌", "什麼也不做"))
                {
                    case 1:
                        NPCMotion(pc, 12001111, 612);
                        Wait(pc, 500);

                        PlaySound(pc, 2231, false, 100, 50);
                        Wait(pc, 2000);

                        ExitFGarden(pc);
                        break;
                    case 2:
                        input = InputBox(pc, "請輸入招牌內容", InputType.PetRename);
                        if (input != "")
                            GetRopeActor(owner).Title = input;
                        break;
                    case 5:
                        break;
                }
            }
            else
            {
                switch (Select(pc, "想要做什麼呢?", "", "從飛空庭下來", "什麼也不做"))
                {
                    case 1:
                        NPCMotion(pc, 12001111, 612);
                        Wait(pc, 500);

                        PlaySound(pc, 2231, false, 100, 50);
                        Wait(pc, 2000);

                        ExitFGarden(pc);
                        break;

                    case 2:
                        break;
                }
            }
        }
    }
}