using System;
using System.Collections.Generic;
using System.Text;

using SagaDB.Actor;
using SagaMap.Scripting;

using SagaLib;
namespace SagaScript.M21193000
{
    public class S11001815 : Event
    {
        public S11001815()
        {
            this.EventID = 11001815;
        }

        public override void OnEvent(ActorPC pc)
        {

            Say(pc, 0, "呼ー…。$R;" +
            "$R呀ー、真是美味！$R;" +
            "$P現在是吃飯後樂趣之一、$R;" +
            "正在品味咖啡。$R;" +
            "$P欸？$R;" +
            "說來這裡在哪？$R;" +
            "$P不是如此決定了嗎。$R;" +
            "$R家庭餐館吶。$R;", "吃飯後的男子");

            //
            /*
            Say(pc, 0, "ふー…。$R;" +
            "$Rいやー、美味かった！$R;" +
            "$P今は食後の楽しみの一つ、$R;" +
            "コーヒーを味わっているところさ。$R;" +
            "$Pえ？$R;" +
            "ところでここはどこだって？$R;" +
            "$Pそんなの決まってるじゃないか。$R;" +
            "$Rファミレスさ。$R;", "食後の男");
            */
        }
    }
}
 