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
    public class S50015000 : Event
    {
        public S50015000()
        {
            this.EventID = 50015000;
        }

        public override void OnEvent(ActorPC pc)
        {
            Say(pc, 131, "你好，要前往测试竞技场吗？", "竞技场管理员");
            switch(Select(pc,"选择","","前往竞技场","查看排名","离开"))
            {
                case 1:
                    pc.TInt["TempBGM"] = 1155;
                    Warp(pc, 80010000, 33, 13);
                    break;
            }
        }
    }
}

