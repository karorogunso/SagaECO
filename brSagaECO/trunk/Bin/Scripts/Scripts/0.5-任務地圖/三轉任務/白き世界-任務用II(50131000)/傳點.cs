using System;
using System.Collections.Generic;
using System.Text;
using SagaLib;
using SagaDB.Actor;
using SagaMap.Scripting;

using SagaScript.Chinese.Enums;
//正在測試的開發內容
namespace SagaScript.M50131000 //白色世界 任務
{
    public class S11001862 : Event
    {
        public S11001862()
        {
            this.EventID = 11001862;
        }

        //其實這是個傳點
        public override void OnEvent(ActorPC pc)
        {
            Say(pc, 0, "稍微在繁忙的每天喘息一下喔。$R;" +
                "$R雖然很不好意思、但可不可以讓我獨自一人？$R;" +
                "只有在這個瞬間、不想回到現實。$R;", "放鬆的女子");
        }
    }
}
