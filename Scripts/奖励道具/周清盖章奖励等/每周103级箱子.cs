
using System;
using System.Collections.Generic;
using System.Text;
using SagaLib;
using SagaDB.Actor;
using SagaDB.Item;
using SagaMap.Scripting;
using SagaScript.Chinese.Enums;
namespace SagaScript.M30210000
{
    public class S910000045 : Event
    {
        public S910000045()
        {
            this.EventID = 910000045;
        }

        public override void OnEvent(ActorPC pc)
        {
            if (CountItem(pc, 910000045) > 0)
            {
                int rate = Global.Random.Next(0, 100);
                int Coinrate = Global.Random.Next(0, 100);
                int Srate = Global.Random.Next(0, 1000);
                PlaySound(pc, 2040, false, 100, 50);

                for (int i = 0; i < 120; i++)
                {
                    GiveItem(pc, (uint)Global.Random.Next(910000016, 910000019), 1);
                }
                GiveItem(pc, 940000002, 1);//+9~+11强化保护石
                GiveItem(pc, 950000001, 1);//脸币
                GiveItem(pc, 950000000, 1);//发型币
                GiveItem(pc, 950000002, 3);//扭曲石头
                pc.Gold += Global.Random.Next(1000000, 2500000);
                GiveItem(pc, 100000000, 800);//KUJI币
                GiveItem(pc, 950000005, 160);//特质KUJI币
                GiveItem(pc, 950000003, 450);//武器碎片

                GiveItem(pc, 910000040, 8);//CP5000

                TakeItem(pc, 910000045, 1);
            }
        }
    }
}

