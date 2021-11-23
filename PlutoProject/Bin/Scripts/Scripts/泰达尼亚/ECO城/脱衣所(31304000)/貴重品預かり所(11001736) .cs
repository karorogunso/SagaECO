using System;
using System.Collections.Generic;
using System.Text;

using SagaDB.Actor;
using SagaMap.Scripting;

using SagaLib;
namespace SagaScript.M31304000
{
    public class S11001736 : Event
    {
        public S11001736()
        {
            this.EventID = 11001736;
        }

        public override void OnEvent(ActorPC pc)
        {
            Say(pc, 131, "貴重品をお預かりいたします。$R;", "貴重品預かり所");
            Select(pc, "いかがなされますか？", "", "何もしない", "荷物を預ける（３００ゴールド）", "貸し倉庫に荷物を預ける");
        }
    }
}


