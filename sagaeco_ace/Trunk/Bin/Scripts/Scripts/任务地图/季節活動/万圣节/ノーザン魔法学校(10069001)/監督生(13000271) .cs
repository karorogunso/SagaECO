using System;
using System.Collections.Generic;
using System.Text;

using SagaDB.Actor;
using SagaMap.Scripting;

using SagaLib;
using SagaScript.Chinese.Enums;
namespace SagaScript.M10069001
{
    public class S13000271 : Event
    {
        public S13000271()
        {
            this.EventID = 13000271;
        }

        public override void OnEvent(ActorPC pc)
        {
            Say(pc, 131, "" + pc.Name + "様？$R;" +
            "お帰りですの？$R;", "監督生");
            if (Select(pc, "アクロポリスに帰りますか？", "", "帰らない", "帰る！")==2)
            {
                Say(pc, 131, "夜間の飛行は危険ですわ。$R;" +
                "$R私が、アクロポリスシティまで$R;" +
                "ご一緒いたしましょう。$R;", "監督生");
                ShowEffect(pc, 13000271, 8015);
                NPCChangeView(pc, 13000271, 11001979);
                Wait(pc, 1650);
                Warp(pc, 10023000, 130, 139);
            }
        }
    }
}