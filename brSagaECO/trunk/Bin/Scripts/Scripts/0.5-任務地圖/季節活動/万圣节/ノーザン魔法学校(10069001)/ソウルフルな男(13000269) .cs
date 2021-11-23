using System;
using System.Collections.Generic;
using System.Text;

using SagaDB.Actor;
using SagaMap.Scripting;

using SagaLib;
using SagaScript.Chinese.Enums;
namespace SagaScript.M10069001
{
    public class S13000269 : Event
    {
        public S13000269()
        {
            this.EventID = 13000269;
        }

        public override void OnEvent(ActorPC pc)
        {
            BitMask<wsj> wsj_mask = new BitMask<wsj>(pc.CMask["wsj"]);
            //int selection;
            if (wsj_mask.Test(wsj.消失))
            {
                Say(pc, 131, "お〜〜〜？？？$R;" +
                "愛しのマイラヴァ〜〜〜♪$R;" +
                "$Rどこに消えたんだ〜い〜？？？$R;", "ソウルフルな男");
                return;
            }
            Say(pc, 131, "お〜〜〜？？？$R;" +
            "愛しのマイラヴァ〜〜〜♪$R;" +
            "$Rどこに消えたんだ〜い〜？？？$R;", "ソウルフルな男");
            Say(pc, 131, "この壁の上に僕の思い人がいるんだ。$R;" +
            "$R愛しのマイラヴァ〜〜〜♪$R;" +
            "僕の愛に答えておくれ〜〜〜♪$R;", "ソウルフルな男");
        }
    }
}