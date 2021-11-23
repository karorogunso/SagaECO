using System;
using System.Collections.Generic;
using System.Text;

using SagaDB.Actor;
using SagaMap.Scripting;

using SagaLib;
namespace SagaScript.M21193000
{
    public class S11001798 : Event
    {
        public S11001798()
        {
            this.EventID = 11001798;
        }

        public override void OnEvent(ActorPC pc)
        {
            Say(pc, 0, "要加入咖啡牛奶、$R;" +
            "$R抑或是加入水果牛奶好呢…。$R;" +
            "$R或者、還有純牛奶…。$R;" +
            "$P喜出望外的、在焦糖Cappuccino$R;" +
            "很難取捨。$R;" +
            "$P不過因為經已辛苦來到海邊、$R;" +
            "所以即使不是浴後的牛奶選擇$R;" +
            "感覺也不錯吶…。$R;", "悩んでいる男");
            //
            /*
             Say(pc, 0, "コーヒー牛乳にするか、$R;" +
            "$Rそれともフルーツ牛乳にするか…。$R;" +
            "$Rはたまた、シンプルに牛乳もありか…。$R;" +
            "$P意表を付いて、キャラメルカプチーノって$R;" +
            "選択も捨てがたい。$R;" +
            "$Pでもせっかく海に来てるわけだから、$R;" +
            "風呂上りみたいな選択をしなくても$R;" +
            "良い気もするな…。$R;", "悩んでいる男");
            */
        }
    }
}
 