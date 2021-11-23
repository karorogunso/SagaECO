using System;
using System.Collections.Generic;
using System.Text;

using SagaDB.Actor;
using SagaMap.Scripting;

using SagaLib;
using SagaScript.Chinese.Enums;
namespace SagaScript.M10069001
{
    public class S13000266 : Event
    {
        public S13000266()
        {
            this.EventID = 13000266;
        }

        public override void OnEvent(ActorPC pc)
        {
            Say(pc, 131, "侍女たちと、かくれんぼをして$R;" +
            "遊んでいたのじゃが$R;" +
            "見つかる前に$R;" +
            "死んでしまったんじゃよ〜。$R;" +
            "$Rせつないの〜。$R;" +
            "$Pでも、お前さんに$R;" +
            "見つけてもらったから$R;" +
            "よしとしようかの〜〜〜。$R;", "忘れられた王様");
            Wait(pc, 330);
            ShowEffect(pc, 13000266, 4011);
            Wait(pc, 1980);
            PlaySound(pc, 2040, false, 100, 50);
            //消失
            NPCHide(pc, 13000266);
            Say(pc, 0, 131, "満足して成仏したようだ……。$R;", " ");
        }
    }
}