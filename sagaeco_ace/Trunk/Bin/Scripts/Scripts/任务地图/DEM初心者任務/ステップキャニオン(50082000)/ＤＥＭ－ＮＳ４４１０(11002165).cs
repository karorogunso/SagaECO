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
            Say(pc, 131, "如果先从这里前进的话$R;" +
            "应该能见到同伴。$R;" +
            "$R听说已经追上敌人了…$R;" +
            "为他们提供援助吧。$R;", "ＤＥＭ－ＮＳ４４１０");

        }
    }
}