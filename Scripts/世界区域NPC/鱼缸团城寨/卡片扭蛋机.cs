
using System;
using System.Collections.Generic;
using System.Text;
using SagaLib;
using SagaDB.Actor;
using SagaDB.Item;
using System.Text;
using System.Diagnostics;
using SagaMap.Scripting;
using SagaScript.Chinese.Enums;
namespace SagaScript.FF
{
    public class S80000003 : Event
    {
        public S80000003()
        {
            this.EventID = 80000003;
        }

        public override void OnEvent(ActorPC pc)
        {
            Say(pc, 0, "陈列着很多卡片包的机器，$R投入封印卡片的话，$R就会出来漂亮的卡片。$R$R机器下方写着：禁止摇晃、踢打。", "卡片扭蛋机");
            PlaySound(pc, 2559, false, 100, 50);
            OpenIrisGacha(pc);
        }
    }
}