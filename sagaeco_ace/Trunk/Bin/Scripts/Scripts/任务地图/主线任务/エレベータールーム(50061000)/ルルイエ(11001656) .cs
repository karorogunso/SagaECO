using System;
using System.Collections.Generic;
using System.Text;
using SagaLib;
using SagaDB.Actor;
using SagaMap.Scripting;

using SagaScript.Chinese.Enums;
namespace SagaScript.M50061000
{
    public class S11001656 : Event
    {
        public S11001656()
        {
            this.EventID = 11001656;
        }

        public override void OnEvent(ActorPC pc)
        {
            BitMask<saga8_01> saga8_01_mask = new BitMask<saga8_01>(pc.CMask["saga8_01"]);
            //int selection;
            if (saga8_01_mask.Test(saga8_01.第二次话说))
            {


                Say(pc, 131, "もう少し待ってね、今設定している$R;" +
                "ところだから、……えーっと。$R;", "ルルイエ");
                ShowEffect(pc, 11001654, 4501);
                ShowEffect(pc, 11001655, 4501);
                ShowEffect(pc, 11001656, 4501);
                ShowEffect(pc, 11001657, 4501);
                ShowEffect(pc, 11001665, 5148);
                NPCShow(pc, 11001665);//让NPC出现
                ShowEffect(pc, 11001666, 5148);
                NPCShow(pc, 11001666);//让NPC出现
                ShowEffect(pc, 11001667, 5148);
                NPCShow(pc, 11001667);//让NPC出现
                Say(pc, 11001654, 112, "うわぁっ！？$R;", "エミル");

                Say(pc, 11001655, 65535, "なにっ！？$R;", "マーシャ");

                Say(pc, 11001657, 65535, "チッ！こんな時に！$R;" +
                "ルルイエ！$R;", "ベリアル");

                Say(pc, 131, "分かってるわよ！$R;" +
                "もう少し待って！$R;", "ルルイエ");

                Say(pc, 11001654, 65535, "ど、どうする！？$R;", "エミル");

                Say(pc, 11001655, 65535, "決まってるじゃない！$R;" +
                "戦うのよ！$R;", "マーシャ");

                Say(pc, 11001657, 65535, "そうだな……。$R;" +
                "$Rここはオレ達で何とかする。$R;" +
                "だから、お前だけでも先に行け！$R;", "ベリアル");

                Say(pc, 131, "そういうことみたいだから$R;" +
                "ここはあたし達にまかせてね。$R;", "ルルイエ");

                Say(pc, 11001657, 65535, "お前ら、足手まといには$R;" +
                "なるなよ？$R;", "ベリアル");

                Say(pc, 11001654, 65535, "がんばるよ。$R;", "エミル");

                Say(pc, 11001655, 65535, "あたしを誰だと思ってるのよ！$R;", "マーシャ");

                Say(pc, 131, "それじゃ、先に行っててね♪$R;", "ルルイエ");
                ShowEffect(pc, 5313);
                Wait(pc, 990);
                Wait(pc, 1980);
                Warp(pc, 12058000, 128, 156);
            }
        }
    }
}
    