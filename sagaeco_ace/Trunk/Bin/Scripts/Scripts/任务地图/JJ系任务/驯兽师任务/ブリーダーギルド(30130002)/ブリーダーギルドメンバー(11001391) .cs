using System;
using System.Collections.Generic;
using System.Text;
using SagaLib;
using SagaDB.Actor;
using SagaMap.Scripting;
using SagaScript.Chinese.Enums;
namespace SagaScript.M30130002
{
    public class S11001391 : Event
    {
        public S11001391()
        {
            this.EventID = 11001391;
        }

        public override void OnEvent(ActorPC pc)
        {
            BitMask<jjxs> jjxs_mask = new BitMask<jjxs>(pc.CMask["jjxs"]);

            if (pc.PossesionedActors.Count == 0 && jjxs_mask.Test(jjxs.面试) && !jjxs_mask.Test(jjxs.面试通过))
            {
                Say(pc, 131, "どうぞ、こちらが面接室と$R;" +
                "なっており……ます。$R;", "ブリーダーギルドメンバー");
                jjxs_mask.SetValue(jjxs.面试, false);
                Warp(pc, 50057000, 5, 4);
                return;
            }

            Say(pc, 131, "ここからは関係者以外$R;" +
            "立ち入り禁止……です。$R;", "ブリーダーギルドメンバー");
        }
    }
}