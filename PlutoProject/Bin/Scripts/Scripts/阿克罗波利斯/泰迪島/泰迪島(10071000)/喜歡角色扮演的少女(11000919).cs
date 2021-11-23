using System;
using System.Collections.Generic;
using System.Text;

using SagaDB.Actor;
using SagaMap.Scripting;
//所在地圖:泰迪島(10071000) NPC基本信息:喜歡角色扮演的少女(11000919) X:28 Y:185
namespace SagaScript.M10071000
{
    public class S11000919 : Event
    {
        public S11000919()
        {
            this.EventID = 11000919;
        }

        public override void OnEvent(ActorPC pc)
        {
            Say(pc, 11000919, 131, "雏菊啊~?$R;" +
                                   "$P你是来看我的吗?$R;" +
                                   "$R嗯~ 好害羞喔~$R;" +
                                   "啦啦啦啦!$R;", "喜欢角色扮演的少女");
        }
    }
}




