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
    public class S11001058 : Event
    {
        public S11001058()
        {
            this.EventID = 11001058;
        }

        public override void OnEvent(ActorPC pc)
        {
            Say(pc, 131, "マイマイ島行き、まもなく出発です。$R;");
            if (pc.Skills2.ContainsKey(706) || pc.SkillsReserve.ContainsKey(706))
            {
                Say(pc, 131, "あ、あれ？$R;" +
                    pc.Name + "さんですよね？$R;" +
                    "$Rいや～、僕ファンなんですよ。$R;" +
                    "あっ、握手してください！$R;");
                switch (Select(pc, "どうする？", "", "断る！", "握手する"))
                {
                    case 1:
                        Say(pc, 131, "な、なんだよ……。$R;" +
                            "ちょっと、イメージ違うな……。$R;");
                        switch (Select(pc, "どうする？", "", "乗らない", "乗る（200ゴールド）"))
                        {
                            case 1:
                                break;
                            case 2:
                                if (pc.Gold > 199)
                                {
                                    pc.Gold -= 200;
                                    PlaySound(pc, 2040, false, 100, 50);
                                    Say(pc, 131, "200ゴールド払った。$R;");
                                    Say(pc, 131, "では、飛空庭へどうぞ。$R;");
                                    PlaySound(pc, 2426, false, 100, 50);
                                    Say(pc, 131, "マイマイ島へ出発しまーす！$R;");
                                    PlaySound(pc, 2438, false, 100, 50);
                                    ShowEffect(pc, 94, 215, 8066);
                                    Wait(pc, 2000);
                                    Warp(pc, 10059000, 70, 148);
                                    return;
                                }
                                PlaySound(pc, 2041, false, 100, 50);
                                Say(pc, 131, "お金が足りません。$R;");
                                break;
                        }
                        break;
                    case 2:
                        Say(pc, 131, "わあ、ありがとう。$R;" +
                            "この手は一生洗わないぞっ♪$R;" +
                            "$Rそうだ、お礼といっては何ですが$R;" +
                            "マイマイ島まで乗っていきませんか？$R;" +
                            "もちろん、タダです！$R;");
                        switch (Select(pc, "どうする？", "", "遠慮する", "乗っていく"))
                        {
                            case 1:
                                break;
                            case 2:
                                PlaySound(pc, 2426, false, 100, 50);
                                Say(pc, 131, "マイマイ島へ出発しまーす！$R;");
                                PlaySound(pc, 2438, false, 100, 50);
                                ShowEffect(pc, 94, 215, 8066);
                                Wait(pc, 2000);
                                    Warp(pc, 10059000, 70, 148);
                                break;
                        }
                        break;
                }
                return;
            }
            switch (Select(pc, "どうする？", "", "乗らない", "乗る（200ゴールド）"))
            {
                case 1:
                    break;
                case 2:
                    if (pc.Gold > 199)
                    {
                        pc.Gold -= 200;
                        PlaySound(pc, 2040, false, 100, 50);
                        Say(pc, 131, "200ゴールド払った。$R;");
                        Say(pc, 131, "では、飛空庭へどうぞ。$R;");
                        PlaySound(pc, 2426, false, 100, 50);
                        Say(pc, 131, "マイマイ島へ出発しまーす！$R;");
                        PlaySound(pc, 2438, false, 100, 50);
                        ShowEffect(pc, 94, 215, 8066);
                        Wait(pc, 2000);
                                    Warp(pc, 10059000, 70, 148);
                        return;
                    }
                    PlaySound(pc, 2041, false, 100, 50);
                    Say(pc, 131, "お金が足りません。$R;");
                    break;
            }
        }
    }
}