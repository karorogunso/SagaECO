using System;
using System.Collections.Generic;
using System.Text;
using SagaLib;
using SagaDB.Actor;
using SagaMap.Scripting;

using SagaScript.Chinese.Enums;
namespace SagaScript.M32003001
{
    public class S11001718 : Event
    {
        public S11001718()
        {
            this.EventID = 11001718;
        }

        public override void OnEvent(ActorPC pc)
        {
            Say(pc, 131, "なんだったんだ、今の爆発は……。$R;" +
            "$Rん？ここは危険だ！$R;" +
            "今すぐここから逃げ……$R;" +
            "$Rなんだと？若を探している？$R;" +
            "$P若ならこの奥だが$R;" +
            "なんだか様子がおかしい……。$R;" +
            "$R様子を見に行きたい所だが$R;" +
            "今、私はここを離れることができない。$R;" +
            "$R……すまないが$R;" +
            "若の様子を見てきてくれないか？$R;", "WF防衛最高責任者");
            switch (Select(pc, "どうする？", "", "やめる", "頼みを聞く"))
            {
                case 2:
                    Warp(pc, 32003000, 7, 38);
                    break;
            }
        }

    }

}
            
            
        
     
    