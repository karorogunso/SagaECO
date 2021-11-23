
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
    public class S910000021 : Event
    {
        public S910000021()
        {
            this.EventID = 910000021;
        }

        public override void OnEvent(ActorPC pc)
        {
            if (CountItem(pc, 910000021) > 0)
            {
                int rate = Global.Random.Next(0, 100);
                int Coinrate = Global.Random.Next(0, 100);
                int Srate = Global.Random.Next(0, 1000);
                PlaySound(pc, 2040, false, 100, 50);

                for (int i = 0; i < 20; i++)
                {
                    GiveItem(pc, (uint)Global.Random.Next(910000016, 910000019), 1);
                }
                GiveItem(pc, 950000000, 2);//发型币
                GiveItem(pc, 950000002, 1);//扭曲石头
                pc.Gold += Global.Random.Next(100000, 300000);
                GiveItem(pc, 100000000, 150);//KUJI币
                GiveItem(pc, 950000005, 20);//特质KUJI币
                GiveItem(pc, 950000003, 80);//武器碎片

                TakeItem(pc, 910000021, 1);
            }
        }
    }
}

