using System;
using System.Collections.Generic;
using System.Text;

using SagaDB.Actor;
using SagaDB.Item;
using SagaMap.Scripting;

using SagaLib;
using SagaScript.Chinese.Enums;
namespace SagaScript.M20146001
{
    public class S11001232 : Event
    {
        public S11001232()
        {
            this.EventID = 11001232;
        }

        public override void OnEvent(ActorPC pc)
        {
            Say(pc, 0, 131, "「客人」！「客人」！$R;" +
                "$R來吧!快點找出『電腦唯讀記憶體』$R就回唐卡吧♪$R;", "行李裡的哈爾列爾利");
        }
    }
}