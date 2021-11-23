using System;
using System.Collections.Generic;
using System.Text;
using SagaLib;
using SagaDB.Actor;
using SagaMap.Scripting;

using SagaScript.Chinese.Enums;
namespace SagaScript.M20004002
{
    public class S12002045 : Event
    {
        public S12002045()
        {
            this.EventID = 12002045;
        }

        public override void OnEvent(ActorPC pc)
        {
            Say(pc, 131, "昆蟲中，工蜂誕生的一瞬間$R;" +
                "看過沒有？$R;" +
                "沒看過的話就幸運多了$R;" +
                "$P怎樣出生？$R;" +
                "見過草蜢出生的人$R;" +
                "很容易想像得到吧……啐！！$R;" +
                "$P我不會再到這裡來的！$R;" +
                "                   冒險者嘉嘉$R;");
        }
    }
}
