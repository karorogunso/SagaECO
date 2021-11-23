using System;
using System.Collections.Generic;
using System.Text;

using SagaDB.Actor;
using SagaMap.Scripting;

namespace SagaScript
{
    public class 帐篷 : EventActor
    {
        int hitCount = 0;

        public 帐篷()
        {
            this.EventID = 0xF0001233;
        }

        public override void OnEvent(ActorPC pc)
        {
            string input;

            if (pc == this.Actor.Caster)
            {
                switch (Select(pc, string.Format("{0}的 帐篷 Hit : {1}", this.Actor.Caster.Name, hitCount), "", "进入帐篷", "更改名字", "收拾帐篷", "什么也不做"))
                {
                    case 1:
                        break;

                    case 2:
                        input = InputBox(pc, "请输入招牌內容", InputType.PetRename);

                        if (input != "")
                            this.Actor.Title = input;
                        break;
                    case 3:

                        break;
                    case 4:
                        break;
                }
            }
            else
            {
                switch (Select(pc, string.Format("{0}的 飞空艇 Hit : {1}", this.Actor.Caster.Name, hitCount), "", "进入帐篷", "什么也不做"))
                {
                    case 1:
                        break;

                    case 2:
                        break;
                }
            }

            hitCount++;
        }

        public override EventActor Clone()
        {
            return new 帐篷();
        }
    }
}