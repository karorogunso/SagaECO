using System;
using System.Collections.Generic;
using System.Text;
using SagaLib;
using SagaDB.Actor;
using SagaMap.Scripting;
using SagaScript.Chinese.Enums;
namespace SagaScript.M11058000
//所在地图:通天塔之岛(11058000)NPC基本信息:11001470-领航者- X:126 Y:250
{
    public class S11001470 : Event
    {
        public S11001470()
        {
            this.EventID = 11001470;
        }

        public override void OnEvent(ActorPC pc)
        {
            Say(pc, 131, "租赁我们的船吗?$R这样可以去我们的岛上.$R;");
            switch(Select(pc, "租船吗", "", "需要租船", "去泰达尼亚","取消"))
            {
                case 1:
                    Warp(pc, 11053000, 19, 230);
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