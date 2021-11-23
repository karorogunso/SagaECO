using System;
using System.Collections.Generic;
using System.Text;

using SagaDB.Actor;
using SagaMap.Scripting;

using SagaLib;
namespace SagaScript.M20070050
{
    public class S11003964 : Event
    {
        public S11003964()
        {
            this.EventID = 11003964;
        }

        public override void OnEvent(ActorPC pc)
        {
            /*開發中 進度
            基本資料O
            原文搬運O
            翻譯校對X
            細節修正X
            */
            Say(pc, 0, "宝箱を開ける瞬間のドキドキつて$R;" +
            "やみつきになると思わない？$R;" +
            "$Pもし、「幸運の宝箱」を見つけたら$R;" +
            "中身はいらないから$R;" +
            "あたしに開けさせてく寝ないかなつ！$R;", "箱開けが趣味の少女");

        }
    }


}
