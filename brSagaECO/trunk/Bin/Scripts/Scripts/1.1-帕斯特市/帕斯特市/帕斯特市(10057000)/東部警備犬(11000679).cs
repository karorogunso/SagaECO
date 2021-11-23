using System;
using System.Collections.Generic;
using System.Text;
using SagaLib;
using SagaDB.Actor;
using SagaMap.Scripting;
using SagaScript.Chinese.Enums;
namespace SagaScript.M10057000
{
    public class S11000679 : Event
    {
        public S11000679()
        {
            this.EventID = 11000679;
        }

        public override void OnEvent(ActorPC pc)
        {
            BitMask<PSTFlags> mask = pc.CMask["PST"];
            if (pc.Level > 44 && !mask.Test(PSTFlags.獲得翡翠) && CheckInventory(pc, 10013002, 1))//_Xa58
            {
                mask.SetValue(PSTFlags.獲得翡翠, true);
                //_Xa58 = true;
                GiveItem(pc, 10013002, 1);
                Say(pc, 131, "吭吭吭...$R;");
                Say(pc, 0, 131, "嘴裡好像叼著什麽…$R;" +
                    "原來是發綠光的漂亮石頭$R;");
                PlaySound(pc, 2040, false, 100, 50);
                Say(pc, 0, 131, "得到了『翡翠』!$R;");
                Say(pc, 131, "汪汪！$R;");
                return;
            }
            Say(pc, 131, "汪!汪汪!$R;");
        }
    }
}