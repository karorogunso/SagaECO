using System;
using System.Collections.Generic;
using System.Text;

using SagaDB.Actor;
using SagaMap.Scripting;

using SagaLib;
namespace SagaScript.M20070050
{
    public class S11003681 : Event
    {
        public S11003681()
        {
            this.EventID = 11003681;
        }

        public override void OnEvent(ActorPC pc)
        {
            /*開發中 進度
            基本資料O
            原文搬運O
            翻譯校對X
            細節修正X
            */
            Say(pc, 0, "てなわけで、新たに$R;" +
                "発見された、無限回廊の$R;" +
                "地下の階層まで来てみた$R;" +
                "わけだが、感想をどうぞ。$R;", "フレイズ");
            Say(pc, 11003682, "来たばっかで、感想もあるか。$R;" +
                "$Rでも、久々に$R;" +
                "お宝にあいつけそうな$R;" +
                "予感がする。$R;", "バーン");
            Say(pc, 11003683, "そうね、ようやく$R;" +
                "私達の力が発揮できそうな$R;" +
                "場所が現れたわ。$R;", "クローネ");
            Say(pc, 11003684, "ですね。$R;" +
                "今日のために装備を$R;" +
                "一新しました。$R;", "フォル");
            Say(pc, 0, "お、いいね～$R;" +
                "気合入っているね、みなさん。$R;"+
                "$Pオレもこの、エンシェントアークで$R;" +
                "手に入れた、装備があれば$R;" +
                "どんなところだって$R;" +
                "行ける気がするぜ！$R;", "アインス");
        }

        private void Say(ActorPC pc, int v1, string v2, string v3)
        {
            throw new NotImplementedException();
        }
    }


}
