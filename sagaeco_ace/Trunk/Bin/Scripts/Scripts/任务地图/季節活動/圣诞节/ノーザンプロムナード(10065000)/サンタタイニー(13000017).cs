using System;
using System.Collections.Generic;
using System.Text;
using SagaLib;
using SagaDB.Actor;
using SagaMap.Scripting;
using SagaScript.Chinese.Enums;
namespace SagaScript.M10065000
{
    public class S13000017 : Event
    {
        public S13000017()
        {
            this.EventID = 13000017;
        }

        public override void OnEvent(ActorPC pc)
        {
            if (CountItem(pc, 10049100) >= 1)
            {

                return;
            }

            Say(pc, 131, "メリークリスマス！$R;" +
            "$R今なら『クリスマスカード』３枚で$R;" +
            "クリスマスの素敵な帽子と交換しまふ！$R;", "サンタタイニー");
        }
    }
}