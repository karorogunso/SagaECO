using System;
using System.Collections.Generic;
using System.Text;

using SagaDB.Actor;
using SagaMap.Scripting;

using SagaLib;
namespace SagaScript.M12035001
{
    public class S11001638 : Event
    {
        public S11001638()
        {
            this.EventID = 11001638;
        }

        public override void OnEvent(ActorPC pc)
        {
            if (pc.Level >= 25)
            {
                //已跪...........
                Say(pc, 131, "나는 어둠의 정령 이시스.$R;" +
                "$R죽은 도미니언들의 영혼을$R;" +
                "돌보고 있습니다…….$R;", "闇の精霊");

                Say(pc, 131, "……당신을$R;" +
                "그리워하고 있는 사람이 있습니다.$R;" +
                "$R이 쪽으로 와줬으면 이라고$R;" +
                "$R나에게 말하고 있습니다.$R;", "어둠의 정령");
                Say(pc, 131, "어떻게 하시겠어요？$R;", "闇の精霊");
                if (Select(pc, "어떻게 할까？", "", "그 곳에 데려가줘", "아무것도 아니야") == 1)
                {
                    Say(pc, 131, "후훗! 당신도 어둠의 힘에 노예가 되었군요$R;", "闇の精霊");
                    ShowEffect(pc, 5066);
                    ShowEffect(pc, 5180);
                    Wait(pc, 990);
                    Warp(pc, 12020001, 2, 87);
                }
                return;
            }
            Say(pc, 131, "わたしは、闇の精霊イシス。$R;" +
            "$R殺されたドミニオンたちの魂を$R;" +
            "癒しています……。$R;", "闇の精霊");

            Say(pc, 131, "……あなたのことを$R;" +
            "懐かしがっている人がいるわ。$R;" +
            "$Rこっちにきて欲しいって言ってる。$R;", "闇の精霊");


        }
    }
}


        
   


