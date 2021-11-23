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
            Say(pc, 131, "剛剛的爆炸是?$R;" +
            "$R嗯？這兒十分危險的！$R;" +
            "快點離開這兒！$R;" +
            "$R什麼？你們在找若？$R;" +
            "$P若的話，就在這裏面，但$R;" +
            "但她的面色有點奇怪……。$R;" +
            "$R雖然很想看看她，$R;" +
            "但現在我不能離開這兒。$R;" +
            "$R……真的不好意思，但$R;" +
            "可以請你進去替我看看若怎樣嗎？$R;", "WF防衛最高責任者");
            switch (Select(pc, "怎麼辦？", "", "拒絕", "聽聽要做什麼吧~"))
            {
                case 2:
                    Warp(pc, 32003000, 7, 38);
                    break;
            }
        }

    }

}
//
/* Say(pc, 131, "なんだったんだ、今の爆発は……。$R;" +
           "$Rん？ここは危険だ！$R;" +
           "今すぐここから逃げ……$R;" +
           "$Rなんだと？若を探している？$R;" +
           "$P若ならこの奥だが$R;" +
           "なんだか様子がおかしい……。$R;" +
           "$R様子を見に行きたい所だが$R;" +
           "今、私はここを離れることができない。$R;" +
           "$R……すまないが$R;" +
           "若の様子を見てきてくれないか？$R;", "WF防衛最高責任者");
           switch (Select(pc, "どうする？", "", "やめる", "頼みを聞く"))*/
           //

    

