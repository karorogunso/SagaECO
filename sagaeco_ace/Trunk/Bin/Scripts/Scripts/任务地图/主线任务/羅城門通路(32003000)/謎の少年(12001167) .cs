using System;
using System.Collections.Generic;
using System.Text;

using SagaDB.Actor;
using SagaMap.Scripting;

using SagaLib;
using SagaScript.Chinese.Enums;
namespace SagaScript.M32003000
{
    public class S12001167 : Event
    {
        public S12001167()
        {
            this.EventID = 12001167;
        }

        public override void OnEvent(ActorPC pc)
        {
            BitMask<Emojie_01> Emojie_01_mask = new BitMask<Emojie_01>(pc.CMask["Emojie_01"]);
            //int selection;
            if (Emojie_01_mask.Test(Emojie_01.迷之少年第一次见面))
            {
                Say(pc, 0, 0, "待て。$R;", "謎の少年");
                ShowEffect(pc, 6, 6, 4011);
                ShowEffect(pc, 7, 27, 4011);
                Wait(pc, 2970);
                NPCShow(pc, 11001646);
                NPCHide(pc, 11001569);

                Say(pc, 0, 0, "……ちょうど、いい。$R;" +
                "あれでは物足りないと$R;" +
                "思っていたところだ。$R;", "謎の少年");
                Select(pc, "どうする！！！", "", "逃げる", "戦う！");
                ShowEffect(pc, 5204);
                Wait(pc, 330);
                Wait(pc, 1980);
                Wait(pc, 990);
                Say(pc, 0, 0, "お前も、弱い……。$R;" +
                "つまらない…な……。$R;", "謎の少年");
                Say(pc, 0, 0, "（こっちだ！$R;" +
                "　若が中の様子を見に行った！$R;" +
                "　早く来てくれ！）$R;", "羅城門門兵");
                Say(pc, 0, 0, "……。$R;" +
                "$P……わが主よ。$R;" +
                "$Rすでに、目的は達されました。$R;" +
                "これ以上、騒ぎが大きくなるのは$R;" +
                "マムのお望みではないかと……。$R;", "謎のネコマタ");
                Say(pc, 0, 0, "母様が！？$R;" +
                "$Rそうだね、黒。$R;" +
                "すべては、母様の望みのままに……。$R;" +
                "一旦、退却しよう……。$R;", "謎の少年");
                Wait(pc, 330);
                ShowEffect(pc, 7, 27, 4011);
                Wait(pc, 1980);
                NPCHide(pc, 11001646);
                Say(pc, 0, 0, "緊張の糸が切れ$R;" +
                "意識が……薄れていった……。$R;", "");
                Wait(pc, 990);
                Warp(pc, 32053000, 3, 6);
            }
            else
            {


            }
        }
    }
}
            
            
        
     
    