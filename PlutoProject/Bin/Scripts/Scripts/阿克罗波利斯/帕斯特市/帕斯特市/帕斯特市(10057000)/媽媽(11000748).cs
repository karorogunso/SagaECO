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
                    "忘了带便当了！$R;" +
                    "$R在这里叫了也是无济于事啊$R;" +
                    "$P嗯?等等!$R;" +
                    "拜托！能不能把便当转交给我老公？$R;" +
                    "$R老公在观察水的人的小破屋前$R;" +
                    "观察水的人的小破屋在东南边！$R;");
                return;
            }
            if (mask.Test(PSTFlags.獲得便當) && 
                !mask.Test(PSTFlags.交出便當) && 
                CountItem (pc, 10043304) == 0)//_5a17 && !_5a18 
            {
                if (CheckInventory(pc, 10043304, 1))
                {
                    GiveItem(pc, 10043304, 1);
                    Say(pc, 131, "那就拜托您了！$R;");
                    return;
                }
                Say(pc, 131, "要给您便当$R;" +
                    "可以把行李减少吗?$R;");
                return;
            }
            Say(pc, 131, "那么要再睡一觉？$R;");
        }
    }
}