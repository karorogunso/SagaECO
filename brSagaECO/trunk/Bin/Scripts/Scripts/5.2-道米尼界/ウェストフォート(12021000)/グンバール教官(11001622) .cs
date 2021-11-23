using System;
using System.Collections.Generic;
using System.Text;
using SagaLib;
using SagaDB.Actor;
using SagaMap.Scripting;

using SagaScript.Chinese.Enums;
namespace SagaScript.M12021000
{
    public class S11001622 : Event
    {
        public S11001622()
        {
            this.EventID = 11001622;
        }

        public override void OnEvent(ActorPC pc)
        {
            Say(pc, 131, "當把朋友重要的東西$R;" +
            "弄壞了的時候。$R;" +
            "$R當把隔壁大叔珍惜的$R;" +
            "盆栽弄壞的時候。$R;" +
            "$R……以及，妻子在怒吼的時候。$R;" +
            "$P能用在這種場面的特技就是$R;" +
            "正坐。$R;" +
            "$P正坐也有能用在$R;" +
            "戰場的地方。$R;" +
            "$R能讓敵人猶豫，來贏取一瞬間的空隙。$R;" +
            "看起來很無情，但是對于生存來說$R;" +
            "是很有效的特技。$R;", "グンバール教官");

            Say(pc, 131, "爲了讓妳們這些皮露露們$R;" +
            "能在這次訓練中少許成長一些，$R;" +
            "現在進行正坐的訓練！$R;" +
            "$P這次就例外地讓我來$R;" +
            "讓妳們見識一下正坐地真本領！$R;" +
            "$P要給我好——好地盯著看！$R;", "グンバール教官");
            ShowEffect(pc, 11001622, 5211);

            Say(pc, 153, "グンバール式・正坐！！$R;", "グンバール教官");

            Say(pc, 11001623, 131, "……好、好厲害。$R;" +
            "$R明明沒做什麽壞事，$R;" +
            "卻覺得有罪惡感……。$R;", "ドミナス訓練生");

            Say(pc, 11001625, 131, "還不止這些……。$R;" +
            "$R感覺不到嗎感じませんか？$R;" +
            "做了正坐后地$R;" +
            "グンバール軍曹……怎麽說呢$R;" +
            "還能覺得讓人憐愛……。$R;", "モルグ訓練生");

            Say(pc, 131, "……呼。$R;" +
            "怎麽樣，這就是正坐。$R;" +
            "$R你們這些人給我反復地$R;" +
            "做正坐！$R;", "グンバール教官");
        }

    }

}
            
            
        
     
    