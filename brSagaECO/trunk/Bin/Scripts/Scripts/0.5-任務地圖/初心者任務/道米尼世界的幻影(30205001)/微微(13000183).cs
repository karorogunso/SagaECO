using System;
using System.Collections.Generic;
using System.Text;

using SagaDB.Actor;
using SagaMap.Scripting;

using SagaLib;
//所在地圖:道米尼世界的幻影(30205001) NPC基本信息:微微(13000183) X:17 Y:19
namespace SagaScript.M30205001
{
    public class S13000178 : Event
    {
        public S13000178()
        {
            this.EventID = 13000183;
        }

        public override void OnEvent(ActorPC pc)
        {
            pc.CInt["Country"] = 1;

            Say(pc, 131, "あら、少し迷ってしまわれました？$R;" +
            "はじめまして、ですわね。$R;" +
            "$CL$R$R※ＥＣＯの世界へようこそ！$CD$R;" +
            "$CL　プロローグと共に$CD$R;" +
            "$CL　ＥＣＯの遊び方を紹介します。$CD$R;", "ティタ");
            Say(pc, 132, "私はティタ。$R;" +
            "$Rタイタニア第三氏族の大天使ですの。$R;" +
            "$Pここはあなたの夢の中…。$R;" +
            "エミルの世界の$Rとても澄み切った美しい星空ですわね。$R;" +
            "$CL$R【ワンポイント】$CD$R;" +
            "$CL　ＥＣＯの世界には、$CD$R;" +
            "$CL　エミル、タイタニア、$CD$R;" +
            "$CL　ドミニオンの３つの種族が$CD$R;" +
            "$CL　存在します。$CD$R;", "ティタ");
            Say(pc, 131, "心配いりませんわ…$R;" +
            "$R…ティタが$RＥＣＯの世界まであなたをご案内します。$R;", "ティタ");
            Say(pc, 0, 131, "ティタとお話を続けます？$R;" +
            "$Rそれともすぐに目を覚まして$Rアクロポリスシティを目指しますか？$R;", "ティタ");
            switch (Select(pc, "どうします？", "", "話を続ける", "すぐに目覚めてアクロポリスを目指す", "少し考える"))
            {
                case 1:
                    byte x, y;

                    Wait(pc, 1980);
                    PlaySound(pc, 2122, false, 100, 50);
                    ShowEffect(pc, 5607);
                    Wait(pc, 1980);
                    PlaySound(pc, 2122, false, 100, 50);
                    ShowEffect(pc, 5606);
                    Wait(pc, 990);
                    PlaySound(pc, 2122, false, 100, 50);
                    ShowEffect(pc, 5607);
                    Wait(pc, 1980);
                    Say(pc, 0, 131, "あっ…！$R;" +
                    "$R…これは…「彼ら」の波動…！？$R;", "ティタ");
                    ShowEffect(pc, 4023);
                    Wait(pc, 1980);
                    pc.CInt["Beginner_Map"] = CreateMapInstance(50030000, 10023100, 250, 132);

                    x = (byte)Global.Random.Next(2, 9);
                    y = (byte)Global.Random.Next(2, 4);

                    Warp(pc, (uint)pc.CInt["Beginner_Map"], x, y);
                    break;
                case 2:
                    Say(pc, 0, 131, "わかりました。$R;" +
                    "$R遠からずまたお目にかかりましょうね。$R;", "ティタ");
                    ShowEffect(pc, 4023);
                    Wait(pc, 1980);
                    Warp(pc, 30141000, 11, 17);
                    break;
            }

        }
    }
}
//路标
namespace SagaScript.M30205000
{
    public class S18000185 : Event
    {
        public S18000185()
        {
            this.EventID = 18000185;
        }

        public override void OnEvent(ActorPC pc)
        {
        }
    }
}
