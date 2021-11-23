using System;
using System.Collections.Generic;
using System.Text;

using SagaDB.Actor;
using SagaMap.Scripting;

using SagaLib;
using SagaScript.Chinese.Enums;

namespace SagaScript.M30098001
{
    public class S18000063 : Event
    {
        public S18000063()
        {
            this.EventID = 18000063;
        }

        public override void OnEvent(ActorPC pc)
        {
            BitMask<obake> obake_mask = new BitMask<obake>(pc.CMask["obake"]);
            //int selection;
            if (obake_mask.Test(obake.第二目))
            {
                PlaySound(pc, 2554, false, 100, 50);

                Say(pc, 0, 0, "この絵に二人の少女が$R;" +
                "描かれている。$R;" +
                "髪の長い女の子と$R;" +
                "髪の短い女の子だ。$R;" +
                "二人の女の子が、$R;" +
                "家の前で笑っている構図だ。$R;" +
                "$P……よくみると$R;" +
                "髪の長い女の子のほうは$R;" +
                "あの幽霊に似ているように$R;" +
                "見えた。$R;" +
                "$P二人の仲よさそうな姉妹。$R;" +
                "でもどこか違和感がある。$R;" +
                "どこだろう？$R;" +
                "$Pどこに違和感が$R;" +
                "あるんだろう？$R;" +
                "$P近づいて、$R;" +
                "よくみてみた。$R;" +
                "$P……。$R;", "");

                Say(pc, 0, 0, "二人とも$R;" +
                "目が全く笑っていない。$R;" +
                "$P……にも関わらず$R;" +
                "こちらをみて$R;" +
                "笑っているかのような$R;" +
                "表情をつくっている。$R;" +
                "$Pそれに、姉妹の背後に$R;" +
                "描かれている家……。$R;", "");
                PlaySound(pc, 2236, false, 100, 50);

                Say(pc, 0, 0, "その中に、誰かが$R;" +
                "立っているようにも見える！$R;" +
                "$P一見してみると$R;" +
                "とても平和な風景を$R;" +
                "描いているのにも関わらず。$R;" +
                "$Pとても不気味な$R;" +
                "絵であることに$R;" +
                "気付いてしまった……。$R;", "");
                obake_mask.SetValue(obake.開門, true);
                return;
            }
            Say(pc, 0, 0, "何かの絵だ……。$R;" +
            "古ぼけていて$R;" +
            "何が描かれているか$R;" +
            "よくわからない。$R;", "");
        }
    }
}
