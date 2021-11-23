using System;
using System.Collections.Generic;
using System.Text;

using SagaDB.Actor;
using SagaMap.Scripting;

using SagaLib;
namespace SagaScript.M31304000
{
    public class S11001866 : Event
    {
        public S11001866()
        {
            this.EventID = 11001866;
        }

        public override void OnEvent(ActorPC pc)
        {
            Say(pc, 0, "くっそおお、もうダメだ！！$R;" +
            "我慢出来ないっ！！$R;", "興味のある男");

            Say(pc, 0, "くっそおお、もうダメだ！！$R;" +
            "我慢出来ないっ！！$R;", "興味のある男");
            NPCMove(pc, 11001866, 65, 78, 340, 11, 464, 20);
            Wait(pc, 4620);

            Say(pc, 336, "見ていろッ！！$R;" +
            "俺は何度でも立ち上がってやる！！$R;" +
            "$Rそして、俺はッ！！$R;" +
            "必ず俺だけの楽園を手にいれてやる！！$R;", "興味のある男");
            NPCMove(pc, 11001866, 65, 61, 460, 7, 464, 20);
            Wait(pc, 4620);

            Say(pc, 0, 0, "キャー！！$R;" +
            "ヘンタイよッ！$R;", "女性の声A");
            PlaySound(pc, 2101, false, 100, 50);
            Wait(pc, 495);
            PlaySound(pc, 3290, false, 100, 50);

            Say(pc, 0, 0, "……ソードディレイキャンセル。$R;", "女性の声A");
            Wait(pc, 330);
            PlaySound(pc, 3331, false, 100, 50);

            Say(pc, 0, 0, "……ゼン。$R;", "女性の声B");

            Say(pc, 0, "……やべっ！逃げ……。$R;", "興味のある男");
            PlaySound(pc, 3435, false, 100, 50);

            Say(pc, 0, 0, "スタンショット！！$R;", "女性の声C");
            Wait(pc, 330);
            PlaySound(pc, 3254, false, 100, 50);

            Say(pc, 0, 0, "百鬼哭ッ！！$R;", "女性の声A");

            Say(pc, 0, "ぬおっ！$R;", "興味のある男");
            ShowEffect(pc, 65, 69, 5314);

            Say(pc, 0, 0, "これでとどめよッ！$R;" +
            "エレメンタルラース！$R;", "女性の声B");

            Say(pc, 0, "ちょ、まって……。$R;", "興味のある男");
            Wait(pc, 330);
            ShowEffect(pc, 65, 69, 8073);

            Say(pc, 0, 0, "ついでに持ってけ！$R;" +
            "クロスクレスト！！$R;", "女性の声C");
            Wait(pc, 330);

            Say(pc, 0, "ぐあああッ！！$R;", "興味のある男");
            NPCMove(pc, 11001866, 65, 95, 1300, 11, 308, 20);
            Wait(pc, 3300);
            NPCMove(pc, 11001866, 0, 0, 1300, 18, 111, 10);

            Say(pc, 0, "……ぐふっ。$R;", "興味のある男");
        }


    }
}


