using System;
using System.Collections.Generic;
using System.Text;
using SagaLib;
using SagaDB.Item;
using SagaDB.Actor;
using SagaMap.Scripting;
using SagaScript.Chinese.Enums;
namespace SagaScript.M10060000
{
    public class S11000848 : Event
    {
        public S11000848()
        {
            this.EventID = 11000848;

        }

        public override void OnEvent(ActorPC pc)
        {

            Say(pc, 255, "嗯…$R;" +
                "$R給我100金幣$R;" +
                "就讓您坐升降機。$R;");

            switch (Select(pc, "付100金幣嗎？", "", "支付", "不支付"))
            {
                case 1:
                    if (pc.Gold > 99)
                    {
                        PlaySound(pc, 2060, false, 100, 50);
                        pc.Gold -= 100;
                        Say(pc, 255, "謝謝…$R;");
                        Say(pc, 255, "來！下一位客人！$R;");
                        Warp(pc, 10060000, 89, 145);
                        return;
                    }
                    PlaySound(pc, 2041, false, 100, 50);
                    Say(pc, 255, "錢不夠是嗎?$R;");
                    break;

                case 2:
                    break;
            }
        }
    }
}