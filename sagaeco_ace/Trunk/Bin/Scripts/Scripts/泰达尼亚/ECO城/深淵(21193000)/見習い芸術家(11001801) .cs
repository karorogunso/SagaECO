using System;
using System.Collections.Generic;
using System.Text;

using SagaDB.Actor;
using SagaMap.Scripting;

using SagaLib;
namespace SagaScript.M21193000
{
    public class S11001801 : Event
    {
        public S11001801()
        {
            this.EventID = 11001801;
        }

        public override void OnEvent(ActorPC pc)
        {
            Say(pc, 0, "何をモチーフに描こう…。$R;" +
            "$Rやっぱり、この秋の景色？$R;" +
            "それとも、人のデッサン？$R;" +
            "あるいは、私の心の中？$R;" +
            "$Pダメ！ そんなの在り来たりだわ！$R;" +
            "$Rアイディア勝負で乗り切るべきか、$R;" +
            "自分に素直になって描くべきか…。$R;" +
            "$R悩む！ 悩むわ！$R;", "見習い芸術家");

        }
    }
}