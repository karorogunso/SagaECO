using System;
using System.Collections.Generic;
using System.Text;

using SagaDB.Actor;
using SagaMap.Scripting;

using SagaLib;
using SagaScript.Chinese.Enums;
namespace SagaScript.M50082000
{
    public class S11002165 : Event
    {
        public S11002165()
        {
            this.EventID = 11002165;
        }

        public override void OnEvent(ActorPC pc)
        {
            BitMask<DEMNewbie> newbie = new BitMask<DEMNewbie>(pc.CMask["DEMNewbie"]);
            Say(pc, 131, "ここから先に進めば、$R;" +
            "仲間に会うことができるはずだ。$R;" +
            "$R既に敵を追い詰めたと聞いたが…$R;" +
            "念のため加勢してこい。$R;", "ＤＥＭ－ＮＳ４４１０");

        }
    }
}