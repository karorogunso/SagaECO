using System;
using System.Collections.Generic;
using System.Text;
using SagaLib;
using SagaDB.Actor;
using SagaMap.Scripting;
using SagaScript.Chinese.Enums;
namespace SagaScript.M10017000
{
    public class S11000151 : Event
    {
        public S11000151()
        {
            this.EventID = 11000151;
        }

        public override void OnEvent(ActorPC pc)
        {
            BitMask<Puppet_02> Puppet_02_mask = pc.CMask["Puppet_02"];
            if (CountItem(pc, 10019700) >= 1 &&
                Puppet_02_mask.Test(Puppet_02.要求製作泰迪))
            {
                ShowEffect(pc, 11000150, 4112);
                PlaySound(pc, 3140, false, 100, 50);
                Wait(pc, 3000);
                TakeItem(pc, 10019700, 1);
                GiveItem(pc, 10019702, 1);
                Say(pc, 131, "『针』变成了『白天的针』!$R;");
                return;
            }
            Say(pc, 131, "……$R;");
        }
    }
}
