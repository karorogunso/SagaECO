
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
    public class S910000046 : Event
    {
        public S910000046()
        {
            this.EventID = 910000046;
        }

        public override void OnEvent(ActorPC pc)
        {
            if (CountItem(pc, 910000046) > 0)
            {
                int rate = Global.Random.Next(0, 100);
                int Coinrate = Global.Random.Next(0, 100);
                int Srate = Global.Random.Next(0, 1000);
                PlaySound(pc, 2040, false, 100, 50);

                for (int i = 0; i < 200; i++)
                {
                    GiveItem(pc, (uint)Global.Random.Next(910000016, 910000019), 1);
                }
                GiveItem(pc, 940000002, 3);//+9~+11强化保护石
                GiveItem(pc, 950000001, 1);//脸币
                GiveItem(pc, 950000000, 1);//发型币
                GiveItem(pc, 950000002, 7);//扭曲石头
                pc.Gold += Global.Random.Next(2500000, 5000000);
                GiveItem(pc, 100000000, 999);//KUJI币
                GiveItem(pc, 950000005, 400);//特质KUJI币
                GiveItem(pc, 950000003, 999);//武器碎片

                GiveItem(pc, 910000040, 16);//CP5000

                TakeItem(pc, 910000046, 1);
            }
        }
    }
}

