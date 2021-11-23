using System;
using System.Collections.Generic;
using System.Text;

using SagaDB.Actor;
using SagaMap.Scripting;

using SagaLib;
namespace SagaScript.M21003000
{
    public class S11001494 : Event
    {
        public S11001494()
        {
            this.EventID = 11001494;
        }

        public override void OnEvent(ActorPC pc)
        {
            Say(pc, 131, "知道么？埃米爾的世界$R;" +
            "許多種族和平的生活著$R;" +
            "看得到么。$R;", "美人魚");

            Say(pc, 11001495, 132, "和平？$R;" +
            "那真的是和平么？$R;", "美人魚");

            Say(pc, 131, "那樣的話、很期待到處看看呢。$R;", "美人魚");

        }


    }
}


