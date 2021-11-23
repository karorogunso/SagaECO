using System;
using System.Collections.Generic;
using System.Text;
using SagaDB.Actor;
using SagaMap.Scripting;
//所在地图:沃特雷亚(11053000)NPC基本信息:11001471-领航者- X:20 Y:234
namespace SagaScript.M11053000
{
    public class S11001471 : Event
    {
    public S11001471()
        {
            this.EventID = 11001471;
        }


        public override void OnEvent(ActorPC pc)
        {
            Say(pc, 131, "租赁我们的船吗?$R这样可以回到通天塔.$R;");
            switch (Select(pc, "租船吗", "", "需要租船", "去泰达尼亚", "取消"))
            {
                case 1:
                        Warp(pc, 11001470, 126, 250);
                        break;
                case 2:
                        //Warp(pc, 11024000, 119, 218);
                        Warp(pc, 11075000, 17, 9);
                        break;
                case 3:
                        break;
            }
        }
    }
}
