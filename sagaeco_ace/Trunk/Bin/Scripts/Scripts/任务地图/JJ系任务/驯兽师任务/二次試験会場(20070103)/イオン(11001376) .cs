using System;
using System.Collections.Generic;
using System.Text;

using SagaDB.Actor;
using SagaMap.Scripting;

using SagaLib;
using SagaScript.Chinese.Enums;
namespace SagaScript.M20070103
{
    public class S11001376 : Event
    {
        public S11001376()
        {
            this.EventID = 11001376;
        }

        public override void OnEvent(ActorPC pc)
        {
            Say(pc, 131, "お、君は確か一緒に面接を$R;" +
            "受けてたよな？$R;" +
            "$R俺はイオンって言うんだ、よろしく。$R;" +
            "$Pふぅ、何とかゴールまで$R;" +
            "たどり着けそうだな。$R;" +
            "$Rお互い最後までがんばろうな。$R;", "イオン");

        }
    }
}