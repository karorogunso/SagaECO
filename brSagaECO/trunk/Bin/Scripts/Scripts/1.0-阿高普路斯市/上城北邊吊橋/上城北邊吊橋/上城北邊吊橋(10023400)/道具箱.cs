using System;
using System.Collections.Generic;
using System.Text;

using SagaDB.Actor;
using SagaMap.Scripting;
//所在地圖:上城北邊吊橋(10023400) NPC基本信息:道具箱
namespace SagaScript.M10023400
{
    public class S12001002 : Event
    {
        public S12001002()
        {
            this.EventID = 12001002;
        }
        public override void OnEvent(ActorPC pc)
        {
            Say(pc, 0, 131, "真是叫人期待的道具箱!$R;" +
                "$R現在調整中$R;" +
                "請使用北邊或南邊$R;" +
                "『騎士團宮殿』前的機械$R;");
        }
    }

    public class S12001003 : Event
    {
        public S12001003()
        {
            this.EventID = 12001003;
        }
        public override void OnEvent(ActorPC pc)
        {
            Say(pc, 0, 131, "真是叫人期待的道具箱!$R;" +
                "$R現在調整中$R;" +
                "請使用北邊或南邊$R;" +
                "『騎士團宮殿』前的機械$R;");
        }
    }
}
