using System;
using System.Collections.Generic;
using System.Text;

using SagaDB.Actor;
using SagaMap.Scripting;

using SagaLib;
using SagaScript.Chinese.Enums;

namespace SagaScript.M30098001
{
    public class S18000062 : Event
    {
        public S18000062()
        {
            this.EventID = 18000062;
        }

        public override void OnEvent(ActorPC pc)
        {
            Say(pc, 0, 0, "破れかけた絵だ……。$R;" +
            "もとはどんな絵$R;" +
            "だったのだろうか？$R;", "");

            Say(pc, 0, 0, "その破れた部分が、$R;" +
            "だんだんと人の顔のように$R;" +
            "みえてくる……。$R;", "");
            PlaySound(pc, 2234, false, 30, 100);

            Say(pc, 0, 0, "い、いや$R;" +
            "確かに人の顔だ！$R;" +
            "$P思わず確かめようと、$R;" +
            "そこにさわった。$R;", "");
            PlaySound(pc, 2101, false, 30, 100);

            Say(pc, 0, 0, "どこかで何かが$R;" +
            "開く音がした……。$R;", "");
        }
    }
}
