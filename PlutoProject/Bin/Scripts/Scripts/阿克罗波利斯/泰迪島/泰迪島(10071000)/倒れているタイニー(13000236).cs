using System;
using System.Collections.Generic;
using System.Text;

using SagaDB.Actor;
using SagaMap.Scripting;

using SagaLib;
using SagaScript.Chinese.Enums;
namespace SagaScript.M10071000
{
    public class S13000236 : Event
    {
        public S13000236()
        {
            this.EventID = 13000236;
        }

        public override void OnEvent(ActorPC pc)
        {
            BitMask<hanabi1> hanabi1_mask = new BitMask<hanabi1>(pc.CMask["hanabi1"]);
            //int selection;
            if (hanabi1_mask.Test(hanabi1.第二次对话后))
            {
                Say(pc, 363, "スキンクがぁ…$R;" +
                "うう…気持ち悪い…$R;" +
                "$Pそういえばあの女の子、$R;" +
                "花火が沢山欲しいって$R;" +
                "言ってたなぁ…$R;" +
                "$R悪戯に使うのかなぁ…$R;", "倒れているタイニー");
                return;
            }
            Say(pc, 363, "き…きつ……騙さ……ガクッ！$R;", "倒れているタイニー");
            if (hanabi1_mask.Test(hanabi1.第一次对话后))
            {
                Say(pc, 0, 363, "返事が無い$R;" +
                "何か複雑な事情があったようだ$R;", " ");
                hanabi1_mask.SetValue(hanabi1.泰迪对话后, true);
            }


        }
    }
}