using System;
using System.Collections.Generic;
using System.Text;

using SagaDB.Actor;
using SagaMap.Scripting;

using SagaLib;
using SagaScript.Chinese.Enums;
namespace SagaScript.M32003000
{
    public class S12001166 : Event
    {
        public S12001166()
        {
            this.EventID = 12001166;
        }

        public override void OnEvent(ActorPC pc)
        {
            BitMask<Emojie_01> Emojie_01_mask = new BitMask<Emojie_01>(pc.CMask["Emojie_01"]);
            //int selection;
            if (Emojie_01_mask.Test(Emojie_01.迷之少年第一次见面))
            {

            }

            Say(pc, 0, 0, "うぐっ……。$R;", "レジスタンスリーダー");

            Say(pc, 0, 0, "弱いな……。$R;" +
            "リーダーが、この程度なのか？$R;", "謎の少年");
            Wait(pc, 990);
            ShowEffect(pc, 11001569, 4501);
            Wait(pc, 990);

            Say(pc, 0, 0, "……見られたか。$R;", "謎の少年");

            Say(pc, 0, 0, "ううっ……。$R;" +
            "早く…逃げ…ろ…。$R;" +
            "$Rこいつは……ヒトじゃ…ない……。$R;", "レジスタンスリーダー");
            Emojie_01_mask.SetValue(Emojie_01.迷之少年第一次见面, true);

        }
    }
}
    