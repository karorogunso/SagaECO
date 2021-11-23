using System;
using System.Collections.Generic;
using System.Text;

using SagaDB.Actor;
using SagaMap.Scripting;
//所在地圖:奧克魯尼亞西部平原(10022000) NPC基本信息:路標(12002018) X:62 Y:253
namespace SagaScript.M10022000
{
    public class S12002018 : Event
    {
        public S12002018()
        {
            this.EventID = 12002018;
        }

        public override void OnEvent(ActorPC pc)
        {           
            Say(pc, 0, 0, "南邊「果樹林」。$R;");

            switch (Select(pc, "聽一聽買賣的方法?", "", "看看吧", "不看"))
            {
                case 1:
                    Say(pc, 0, 0, "用火烤的肉非常好吃!$R;", " ");
                    break;

                case 2:
                    break;
            }
        }
    }
}
