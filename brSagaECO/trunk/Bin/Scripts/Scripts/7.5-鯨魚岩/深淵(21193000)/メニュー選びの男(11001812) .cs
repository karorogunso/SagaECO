using System;
using System.Collections.Generic;
using System.Text;

using SagaDB.Actor;
using SagaMap.Scripting;
using SagaScript.Chinese.Enums;
using SagaLib;
namespace SagaScript.M21193000
{
    public class S11001812 : Event
    {
        public S11001812()
        {
            this.EventID = 11001812;
        }

        public override void OnEvent(ActorPC pc)
        {
            Say(pc, 0, "這店的菜單並沒有正確的選擇。$R;" +
            "$R一天只有其中之一的樂趣！$R;" +
            "$R那麼、今天要點甚麼呢？$R;", "在選菜的男孩");

            //
            /*
             Say(pc, 0, "この店のメニューはどれも正解なんだ。$R;" +
            "$R一日の楽しみの一つさ！$R;" +
            "$Rさーて、今日は何を頼もうかな？$R;", "メニュー選びの男");
            */
        }
    }
}
 