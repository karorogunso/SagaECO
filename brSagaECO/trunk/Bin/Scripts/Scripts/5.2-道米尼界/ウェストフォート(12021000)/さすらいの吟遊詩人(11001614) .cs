using System;
using System.Collections.Generic;
using System.Text;
using SagaLib;
using SagaDB.Actor;
using SagaMap.Scripting;

using SagaScript.Chinese.Enums;
namespace SagaScript.M12021000
{
    public class S11001614 : Event
    {
        public S11001614()
        {
            this.EventID = 11001614;
        }

        public override void OnEvent(ActorPC pc)
        {
            Say(pc, 435, "啦啦啦～～♪$R;" +
            "$R我是流浪的吟遊詩人。$R;" +
            "$R爲了能讓這場戰爭流傳後世$R;" +
            "我在世界各地流浪著～～♪$R;" +
            "$P（鏘♪）$R;" +
            "$P……很多人看著歌唱的我$R;" +
            "說我不謹慎。$R;" +
            "$R但是爲了讓戰爭的殘酷を$R;" +
            "流傳後世$R;" +
            "我認為是非常重要的事情。$R;" +
            "$P妳不這么認爲嗎？$R;", "さすらいの吟遊詩人");
        }

    }

}
            
            
        
     
    