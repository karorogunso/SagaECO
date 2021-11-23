using System;
using System.Collections.Generic;
using System.Text;
using SagaLib;
using SagaDB.Actor;
using SagaDB.Item;
using SagaMap.Scripting;
using SagaScript.Chinese.Enums;
namespace SagaScript.M10062000
{
    public class S11001336 : Event
    {
        public S11001336()
        {
            this.EventID = 11001336;
        }

        public override void OnEvent(ActorPC pc)
        {
            Say(pc, 131, "高速飛空庭、トンカエクスプレス！$R;" +
                "アクロポリス行きはここだ～よ$R;");
            if (pc.Skills2.ContainsKey(706) || pc.SkillsReserve.ContainsKey(706))
            {
                Say(pc, 131, "おや、おめえさん！$R;" +
                    pc.Name + "さんじゃろ？$R;" +
                    "$Pいやぁ、おら、おめえさんのこと$R;" +
                    "知ってるだ～よ。$R;" +
                    "$Rもし、アクロポリスシティに$R;" +
                    "行きたいのならば$R;" +
                    "こっそり乗せてやるけど$R;" +
                    "どうすっぺ？$R;");
                switch (Select(pc, "どうする？", "", "乗らない", "乗る"))
                {
                    case 1:
                        break;
                    case 2:
                        Say(pc, 131, "んじゃ、こっちこっち！$R;" +
                            "この荷物置き場のすみっこに$R;" +
                            "隠れてるだ～よ！$R;");
                        PlaySound(pc, 2426, false, 100, 50);
                        Say(pc, 131, "本日は、トンカエクスプレス$R;" +
                            "アクロポリス行きを$R;" +
                            "ご利用くださいまして$R;" +
                            "まことにありがとうございます。$R;" +
                            "$P途中、強い風の影響で$R;" +
                            "庭が大きく揺れることがありますが$R;" +
                            "振り落とされることなどないよう$R;" +
                            "ご注意願います。$R;" +
                            "$Pそれでは$R;" +
                            "アクロポリスへ出発しまーす！$R;");
                        PlaySound(pc, 2438, false, 100, 50);
                        ShowEffect(pc, 94, 215, 8066);
                        Wait(pc, 2000);
                        Warp(pc, 10023000, 153, 194);
                        break;
                }
                return;
            }
            /*
            if (_Xa31)
            {
                Say(pc, 131, "トンカシティを$R;" +
                    "飛空庭の母港に登録すると$R;" +
                    "アクロポリスシティとトンカを結ぶ$R;" +
                    "超特急、飛空庭エクスプレスが$R;" +
                    "利用できるだ～よ$R;" +
                    "$P登録は、トンカで出來るだ～よ$R;" +
                    "トンカはいい町だ～よ$R;");
                return;
            }
            */
            switch (Select(pc, "どうする？", "", "乗らない", "乗る（1000ゴールド）"))
            {
                case 1:
                    break;
                case 2:
                    Say(pc, 131, "1000ゴールドいただきま～す$R;");

                    if (pc.Gold > 999)
                    {
                        pc.Gold -= 1000;
                        PlaySound(pc, 2040, false, 100, 50);
                        Say(pc, 131, "1000ゴールド払った。$R;");
                        Say(pc, 131, "それじゃ、席にご案内すっぺ。$R;");
                        PlaySound(pc, 2426, false, 100, 50);
                        Say(pc, 131, "本日は、トンカエクスプレス$R;" +
                            "アクロポリス行きを$R;" +
                            "ご利用くださいまして$R;" +
                            "まことにありがとうございます。$R;" +
                            "$P途中、強い風の影響で$R;" +
                            "庭が大きく揺れることがありますが$R;" +
                            "振り落とされることなどないよう$R;" +
                            "ご注意願います。$R;" +
                            "$Pそれでは$R;" +
                            "アクロポリスへ出発しまーす！$R;");
                        PlaySound(pc, 2438, false, 100, 50);
                        ShowEffect(pc, 94, 215, 8066);
                        Wait(pc, 2000);
                        Warp(pc, 10023000, 153, 194);
                        return;
                    }
                    Say(pc, 131, "……あら、お持ちじゃないっぺ？$R;" +
                        "困っただ～よ。$R;" +
                        "う～ん、う～ん……。$R;" +
                        "$P仕方がないだな。$R;" +
                        "ちょっとまけてあげるだ～よ。$R;" +
                        "800ゴールドでどうだっぺ？$R;");
                    if (pc.Gold > 799)
                    {
                        pc.Gold -= 800;
                        PlaySound(pc, 2040, false, 100, 50);
                        Say(pc, 131, "800ゴールド払った。$R;");
                        Say(pc, 131, "それじゃ、席にご案内すっぺ。$R;");
                        PlaySound(pc, 2426, false, 100, 50);
                        Say(pc, 131, "本日は、トンカエクスプレス$R;" +
                            "アクロポリス行きを$R;" +
                            "ご利用くださいまして$R;" +
                            "まことにありがとうございます。$R;" +
                            "$P途中、強い風の影響で$R;" +
                            "庭が大きく揺れることがありますが$R;" +
                            "振り落とされることなどないよう$R;" +
                            "ご注意願います。$R;" +
                            "$Pそれでは$R;" +
                            "アクロポリスへ出発しまーす！$R;");
                        PlaySound(pc, 2438, false, 100, 50);
                        ShowEffect(pc, 94, 215, 8066);
                        Wait(pc, 2000);
                        Warp(pc, 10023000, 153, 194);
                        return;
                    }
                    Say(pc, 131, "……あら、お持ちじゃないっぺ？$R;" +
                        "う～ん、これ以上$R;" +
                        "まけてあげることは出來ないだ～よ。$R;" +
                        "$Rごめんな～。$R;" +
                        "$Pちょっと、歩くけど$R;" +
                        "アイアンサウス経由で行けば$R;" +
                        "100ゴールドですむだ～よ。$R;");
                    break;
            }
        }
    }
}