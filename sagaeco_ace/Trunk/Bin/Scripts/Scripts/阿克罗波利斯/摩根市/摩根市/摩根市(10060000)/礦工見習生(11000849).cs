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
    public class S11000849 : Event
    {
        public S11000849()
        {
            this.EventID = 11000849;

        }

        public override void OnEvent(ActorPC pc)
        {

            Say(pc, 255, "嗯…$R;" +
                "$R给我100金币$R;" +
                "就让您坐升降机。$R;");

            switch (Select(pc, "付100金币吗？", "", "支付", "不支付"))
            {
                case 1:
                    if (pc.Gold > 99)
                    {
                        PlaySound(pc, 2060, false, 100, 50);
                        pc.Gold -= 100;
                        Say(pc, 255, "谢谢…$R;");
                        Say(pc, 255, "来！下一位客人！$R;");
                        Warp(pc, 10060000, 145, 144);
                        return;
                    }
                    PlaySound(pc, 2041, false, 100, 50);
                    Say(pc, 255, "钱不够是吗?$R;");
                    break;
                case 2:
                    break;
            }
        }
    }
}