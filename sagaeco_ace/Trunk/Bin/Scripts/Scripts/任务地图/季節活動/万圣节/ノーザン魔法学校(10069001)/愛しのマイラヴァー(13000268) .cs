using System;
using System.Collections.Generic;
using System.Text;

using SagaDB.Actor;
using SagaMap.Scripting;

using SagaLib;
using SagaScript.Chinese.Enums;
namespace SagaScript.M10069001
{
    public class S13000268 : Event
    {
        public S13000268()
        {
            this.EventID = 13000268;
        }

        public override void OnEvent(ActorPC pc)
        {
            BitMask<wsj> wsj_mask = new BitMask<wsj>(pc.CMask["wsj"]);
            //int selection;
            Say(pc, 13000269, 131, "お〜〜〜♪$R;" +
            "愛しのマイラヴァ〜〜〜♪$R;" +
            "$R僕の愛に答えておくれ〜〜〜♪$R;", "ソウルフルな男");

            Say(pc, 131, "い〜え〜〜♪$R;" +
            "$R今は〜〜、だ〜め〜〜よ〜♪$R;" +
            "まだ〜、はやすぎる〜〜わ〜〜〜♪$R;", "愛しのマイラヴァー");

            Say(pc, 131, "ねえ、私、きれい〜〜？？$R;", "愛しのマイラヴァー");
            if (Select(pc, "どうする？", "", "ぎゃー！！！化け物！！！", "きれい")==2)
            {
                Say(pc, 131, "まあ、ありがとう♪$R;" +
                "うふふっ、うふふっ！$R;" +
                "あの人、私が蛇女だって知らないの。$R;" +
                "$P本当のこと、どうしても言えなかった。$R;" +
                "嫌われたくなかったから。$R;" +
                "$R騙すの、とってもつらかった……。$R;" +
                "$P本当のこと、言えばよかったな……。$R;", "愛しのマイラヴァー");
                Wait(pc, 330);
                ShowEffect(pc, 13000268, 4011);
                Wait(pc, 1980);
                PlaySound(pc, 2040, false, 100, 50);
                wsj_mask.SetValue(wsj.消失, true);
                //消失
                NPCHide(pc, 13000268);
                Say(pc, 0, 131, "満足して成仏したようだ……。$R;", " ");

                Say(pc, 13000269, 131, "お〜〜〜？？？$R;" +
                "愛しのマイラヴァ〜〜〜♪$R;" +
                "$Rどこに消えたんだ〜い〜？？？$R;", "ソウルフルな男");
            }
        }
    }
}