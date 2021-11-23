using System;
using System.Collections.Generic;
using System.Text;
using SagaLib;
using SagaDB.Actor;
using SagaMap.Scripting;
using SagaScript.Chinese.Enums;
namespace SagaScript.M10057000
{
    public class S11000748 : Event
    {
        public S11000748()
        {
            this.EventID = 11000748;
        }

        public override void OnEvent(ActorPC pc)
        {
            BitMask<PSTFlags> mask = pc.CMask["PST"];
            if (!mask.Test(PSTFlags.獲得便當))//_5a17)
            {
                mask.SetValue(PSTFlags.獲得便當, true);
                //_5a17 = true;
                GiveItem(pc, 10043304, 1);
                Say(pc, 131, "喂！！喂！！！$R;" +
                    "忘了帶便當了！$R;" +
                    "$R在這裡叫了也是無濟於事啊$R;" +
                    "$P嗯?等等!$R;" +
                    "拜託！能不能把便當轉交給我老公？$R;" +
                    "$R老公在觀察水的人的小破屋前$R;" +
                    "觀察水的人的小破屋在東南邊！$R;");
                return;
            }
            if (mask.Test(PSTFlags.獲得便當) && 
                !mask.Test(PSTFlags.交出便當) && 
                CountItem (pc, 10043304) == 0)//_5a17 && !_5a18 
            {
                if (CheckInventory(pc, 10043304, 1))
                {
                    GiveItem(pc, 10043304, 1);
                    Say(pc, 131, "那就拜託您了！$R;");
                    return;
                }
                Say(pc, 131, "要給您便當$R;" +
                    "可以把行李减少嗎?$R;");
                return;
            }
            Say(pc, 131, "那麽要再睡一覺？$R;");
        }
    }
}