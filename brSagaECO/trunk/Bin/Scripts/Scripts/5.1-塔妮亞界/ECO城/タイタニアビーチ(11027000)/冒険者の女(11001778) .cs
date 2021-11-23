using System;
using System.Collections.Generic;
using System.Text;

using SagaDB.Actor;
using SagaMap.Scripting;

using SagaLib;
namespace SagaScript.M11027000
{
    public class S11001778 : Event
    {
        public S11001778()
        {
            this.EventID = 11001778;
        }

        public override void OnEvent(ActorPC pc)
        {
            Say(pc, 131, "このロープを伝っていけば、$R;" +
            "上にあるクジラ岩の中に入れるぞ。$R;" +
            "$P一見するとこいつはただの岩だが、$R;" +
            "中には恐ろしく強い敵が徘徊している…$R;" +
            "$Pオマケに、岩の中だと思っていたら$R;" +
            "進むと急に変な空間に出てくる。$R;" +
            "$Rなんかもう、デタラメだ。$R;" +
            "$Pとはいえ、行くなら止めはしない。$R;" +
            "ロープを伝って中に入ると良い。$R;", "冒険者の女");
}
}

        
    }


