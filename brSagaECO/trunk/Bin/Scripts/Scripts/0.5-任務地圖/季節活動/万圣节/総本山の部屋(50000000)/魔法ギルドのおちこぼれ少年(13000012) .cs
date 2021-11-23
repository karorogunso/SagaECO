using System;
using System.Collections.Generic;
using System.Text;

using SagaDB.Actor;
using SagaMap.Scripting;

using SagaLib;
using SagaScript.Chinese.Enums;
namespace SagaScript.M50000000
{
    public class S13000012 : Event
    {
        public S13000012()
        {
            this.EventID = 13000012;
        }

        public override void OnEvent(ActorPC pc)
        {
            BitMask<wsj> wsj_mask = new BitMask<wsj>(pc.CMask["wsj"]);
            //int selection;
            if (wsj_mask.Test(wsj.扫把))
            {
                Say(pc, 112, "あーあ…。$R;" +
                "せっかく友達が出来たのにさぁ。$R;" +
                "ひどいよ、大導師様……。$R;", "魔法ギルドのおちこぼれ少年");
                wsj_mask.SetValue(wsj.入手扫把, true);
                wsj_mask.SetValue(wsj.扫把, false);
                Warp(pc, 10023000, 130, 139);
                return;
            }
            Say(pc, 131, "れ？$R;" +
            "$R喫茶店じゃないよなぁ。$R;" +
            "$Pここって、確か。$R;" +
            "魔法ギルド総本山だよっ！$R;" +
            "おっかしいなぁ！$R;" +
            "$Rなんでだ、なんでだ〜！？$R;", "魔法ギルドのおちこぼれ少年");

            Say(pc, 0, 131, "これ、ヘンピコ。$R;", " ");

            Say(pc, 131, "うっわ……、大導師様。$R;", "魔法ギルドのおちこぼれ少年");

            Say(pc, 0, 131, "お前、また掃除をサボったじゃろう。$R;" +
            "しばらくそこで反省してなさい！$R;", " ");
            ShowEffect(pc, 13000012, 8013);
            PlaySound(pc, 2430, false, 100, 50);

            Say(pc, 374, "イタッ！$R;", "魔法ギルドのおちこぼれ少年");

            Say(pc, 131, "ちょ……、うっそ。$R;" +
            "大導師様、おれの友達もいるんだけど！$R;" +
            "ねえ、聞いてますか！$R;" +
            "$Rねえってばー！！$R;" +
            "$P……反応なし！$R;" +
            "$Pあー、ゴメン。$R;" +
            "なんだか、巻き込んじゃったね$R;" +
            "$Rさっきの、オレのお師匠様なんだけど$R;" +
            "年のわりに、頭に血が上りやすく$R;" +
            "まわりが見えなくなるタイプでさぁ。$R;" +
            "$Pおれの魔法で出れないかな……。$R;" +
            "$Rそ〜れっ！$R;" +
            "そ〜れっ！$R;" +
            "そ〜〜〜〜れっ！$R;" +
            "$P……。$R;" +
            "$Pちっ、結界張りやがったな！$R;" +
            "闇の愛だけでも$R;" +
            "飛ばせるといいんだけど……。$R;" +
            "$Rそ〜〜〜〜〜〜れっ！$R;", "魔法ギルドのおちこぼれ少年");
            wsj_mask.SetValue(wsj.开始, true);
            wsj_mask.SetValue(wsj.扫把, true);
            ShowEffect(pc, 4011);
            Wait(pc, 1980);
            Warp(pc, 10067000, 36, 32);
        }
    }
}