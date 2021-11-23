using System;
using System.Collections.Generic;
using System.Text;
using SagaLib;
using SagaDB.Actor;
using SagaMap.Scripting;

using SagaScript.Chinese.Enums;
namespace SagaScript.M11058000
{
    public class S11001484 : Event
    {
        public S11001484()
        {
            this.EventID = 11001484;
        }

        public override void OnEvent(ActorPC pc)
        {
            Say(pc, 131, "這扇門開啟的時候$R;" +
            "會出現的是惡魔還是怪物。$R;" +
            "$R…… 我有這樣聽說過，但是 $R;" +
            "顯現出的、居然是塔尼亞$R;" +
            "沒預想過是這樣的吶。$R;", "看守美人魚");
            // 02/09/2015 by hoshinokanade
            /*
            Say(pc, 131, "この扉が開いたときに$R;" +
            "現れるのは悪魔か化け物か。$R;" +
            "$R……そう聞いていたのだが$R;" +
            "まさか、タイタニアが現れるとは$R;" +
            "予想だにしなかったな。$R;", "見張りのマーメイド");
            */
        }
    }
}




