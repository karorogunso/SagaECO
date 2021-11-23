using System;
using System.Collections.Generic;
using System.Text;
using SagaLib;
using SagaDB.Actor;
using SagaDB.Item;
using SagaMap.Scripting;
using SagaScript.Chinese.Enums;
namespace SagaScript.M30130001
{
    public class S11001374 : Event
    {
        public S11001374()
        {
            this.EventID = 11001374;
            this.alreadyHasQuest = "次に出る選択ウィンドウで$Rキャンセルすることもできますよ♪$R;";
            this.gotNormalQuest = "それじゃ、おねがいします！$R;" +
                "$Rクエストが無事達成できたら$R;" +
                "クエストカウンターまで$R;" +
                "報告しに来てくださいね♪$R;";
            this.gotTransportQuest = "えっとぉ、アイテムが重すぎて$R;" +
                "１回で渡せない場合は、何度かに$R;" +
                "わけて渡してください♪$R;";
            this.questCompleted = "ご苦労様でした。$R;" +
                "クエストは成功です！$R;";
            this.transport = "";
            this.questCanceled = "残念です……。$R;";
            this.questFailed = "失敗ですかぁ？$R;" +
                "$R……残念です。$R;" +
                "次は頑張ってくださいね！$R;";
            this.leastQuestPoint = 0;

        }

        public override void OnEvent(ActorPC pc)
        {
            BitMask<jjxs> jjxs_mask = new BitMask<jjxs>(pc.CMask["jjxs"]);
            if (CountItem(pc, 10052310) == 0 &&
              ((jjxs_mask.Test(jjxs.开始第一次测试) && !jjxs_mask.Test(jjxs.测试通过)) ||
               (jjxs_mask.Test(jjxs.开始第二次测试) && !jjxs_mask.Test(jjxs.入手徽章))))
            {
                GiveItem(pc, 10052310, 1);
                return;
            }

            if (jjxs_mask.Test(jjxs.入手徽章))
            {
                Say(pc, 131, "ようこそ、ブリーダーギルドへ$R;" +
                "$R今日はどういったご用件でしょうか？$R;" +
                "" + pc.Name + " さん$R;", "ブリーダーギルド受付");
                switch (Select(pc, "いかが致しましょうか？", "", "用は無いです", "制服購入", "ブローチについて"))
                {
                    case 2:
                        OpenShopBuy(pc, 287);
                        return;
                    case 3:
                        Say(pc, 131, "もしも、紛失や破損してしまった$R;" +
                        "場合は、それ相応の代価を支払って$R;" +
                        "いただきます。$R;", "ブリーダーギルド受付");
                        return;
                }

            }
            else if (jjxs_mask.Test(jjxs.测试通过))
            {
                Say(pc, 131, "それでは最終、二次試験の$R;" +
                "説明をさせていただきます。$R;" +
                "$P内容は前回と同様、制限時間以内に$R;" +
                "騎乗ペットに乗り試験官のいる$R;" +
                "目的地へ向かうだけです。$R;" +
                "騎乗ペットに関してはこちら側で$R;" +
                "用意いたしますのでご安心ください。$R;", "ブリーダーギルド受付");
                //HandleQuest(pc, 65);
                jjxs_mask.SetValue(jjxs.开始第二次测试, true);
                pc.CStr["TIME"] = DateTime.Now.ToString();
                Say(pc, 131, "必ず、試験官には騎乗ペットに$R;" +
                ".......。$R;" +
                "$Pそれと騎乗ペットの持ち逃げは$R;" +
                "厳禁です。$R;" +
                "$R悪いことしたら直ぐに$R;" +
                "わかってしまいますからね？$R;" +
                "$Pそれでは私から騎乗ペットを$R;" +
                "お渡ししますので、その後$R;" +
                "左奥の部屋へとお進みください。$R;", "ブリーダーギルド受付");
                return;
            }
            else if (jjxs_mask.Test(jjxs.面试通过))
            {
                Say(pc, 131, "面接合格おめでとうございます。$R;", "ブリーダーギルド受付");

                Say(pc, 131, "それでは二次試験の説明を$R;" +
                "させていただきます。$R;" +
                "$P内容は制限時間以内に騎乗ペットに乗り$R;" +
                "試験官のいる目的地へと向かうだけです。$R;" +
                "騎乗ペットに関してはこちら側で$R;" +
                "用意いたしますのでご安心ください。$R;", "ブリーダーギルド受付");

                Say(pc, 131, "因みにこちらの二次試験は今すぐに$R;" +
                "受けることも無いのですが$R;" +
                "いかが致しましょうか？$R;", "ブリーダーギルド受付");
                if (Select(pc, "いかが致しましょうか？", "", "このまま続けます", "では、また今度") == 1)
                {
                    Say(pc, 131, "試験の内容上ここをセープポイントに$R;" +
                    "しなければならないのですが$R;" +
                    "よろしいですか？$R;", "ブリーダーギルド受付");
                    if (Select(pc, "セーブポイントをここにする？", "", "嫌です", "わかりました") == 2)
                    {

                        Say(pc, 0, 131, "セーブポイントを$R;" +
                        "『ブリーダーギルド』に設定した。$R;", " ");
                        SetHomePoint(pc, 30130001, 7, 6);
                        Say(pc, 131, "それでは試験の申し込みを$R;" +
                        "お願い致します。$R;", "ブリーダーギルド受付");
                        //HandleQuest(pc, 64);
                        jjxs_mask.SetValue(jjxs.开始第一次测试, true);
                        pc.CStr["TIME"] = DateTime.Now.ToString();
                        Say(pc, 131, "必ず、試験官には騎乗ペットに$R;" +
                        "....。$R;" +
                        "$Pそれと騎乗ペットの持ち逃げは$R;" +
                        "厳禁です。$R;" +
                        "$R悪いことしたら直ぐに$R;" +
                        "わかってしまいますからね？$R;" +
                        "$Pそれでは私から騎乗ペットを$R;" +
                        "お渡ししますので、その後$R;" +
                        "左奥の部屋へとお進みください。$R;", "ブリーダーギルド受付");

                    }
                }
            }
        }
    }
}