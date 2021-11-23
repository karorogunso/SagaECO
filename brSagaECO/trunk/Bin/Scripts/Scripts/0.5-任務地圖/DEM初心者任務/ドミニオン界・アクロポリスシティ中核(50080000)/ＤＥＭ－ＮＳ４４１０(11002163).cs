using System;
using System.Collections.Generic;
using System.Text;

using SagaDB.Actor;
using SagaMap.Scripting;

using SagaLib;
using SagaScript.Chinese.Enums;
namespace SagaScript.M50080000
{
    public class S11002163 : Event
    {
        public S11002163()
        {
            this.EventID = 11002163;
        }

        public override void OnEvent(ActorPC pc)
        {
            BitMask<DEMNewbie> newbie = new BitMask<DEMNewbie>(pc.CMask["DEMNewbie"]);
            if (!newbie.Test(DEMNewbie.领取EP))
            {
                do
                {
                    Say(pc, 131, "起きたか、$R;" +
                    "MS-67$R;" +
                    "$P起動するのに随分と時間が$R;" +
                    "かかったようだな。$R;" +
                    "$P私は君の教育を任された$R;" +
                    "型番号「ＤＥＭ－ＮＳ４４１０」だ。$R;" +
                    "$Rまずは、我々自身について$R;" +
                    "インプットしてもらおう。$R;", "ＤＥＭ－ＮＳ４４１０");

                    Say(pc, 131, "我々は、この地の住民から$R;" +
                    "「ＤＥＭ」と呼ばれている人型兵器だ。$R;" +
                    "$P我々を統一する「マザー」の意志により、$R;" +
                    "この地を進攻している。$R;" +
                    "$Pこの地に住むドミニオンという生命体は、$R;" +
                    "我々と敵対するヒトの種族だ。$R;" +
                    "$Pヒトは他にも、$R;" +
                    "エミル種族、タイタニア種族がいるが、$R;" +
                    "基本的に全て敵と認識して問題ない。$R;" +
                    "$Rここまではインプットできたか？$R;", "ＤＥＭ－ＮＳ４４１０");
                }
                while (Select(pc, "どうする？", "", "もう一度聞く", "次の話を聞く") != 2);

                pc.EP++;
                newbie.SetValue(DEMNewbie.领取EP, true);
            }
            if (pc.CL < 10)
            {
                Say(pc, 131, "よろしい。$R;" +
                "$Pさて、君にはこれから簡単な訓練を$R;" +
                "受けてもらい、すぐに戦場に出てもらう。$R;" +
                "$Rそれが君が作られた理由であり、$R;" +
                "存在意義なのだ。$R;" +
                "$P…とその前に、$R;" +
                "我々の成長手段について教えておこう。$R;" +
                "$P今、開いたウインドウは、$R;" +
                "「コストリミットウインドウ」というものだ。$R;" +
                "$P我々は生体ではない。$R;" +
                "$Rそのため、成長と言っても出来ることは$R;" +
                "自身のカスタマイズだ。$R;" +
                "$P我々には、「ＥＰ」と呼ばれるポイントが$R;" +
                "当てられている。$R;" +
                "$PこのＥＰを消費することにより、$R;" +
                "$R「コストリミット」を$R;" +
                "$R上昇させることができる。$R;" +
                "$P「コストリミット」がある程度まで$R;" +
                "上昇すると、君自身のレベルも上昇する。$R;" +
                "$Pまた、「ＤＥＭＩＣ」と呼ばれるパネルも$R;" +
                "拡張されるのだが…$R;" +
                "これは後で解説しよう。$R;" +
                "$Pとりあえず、ＥＰを使って$R;" +
                "「コストリミット」を上げてみてくれ。$R;" +
                "$P「EP消費」ボタンを押して、$R;" +
                "消費したいEP量を入力するのだ。$R;" +
                "$R終わったらコストリミットウインドウを$R;" +
                "閉じれば完了だ。$R;", "ＤＥＭ－ＮＳ４４１０");

                DEMCL(pc);
                return;
            }
            else
            {
                Say(pc, 131, "コストリミットを$R;" +
                "上昇させられたようだな。$R;" +
                "$Pでは、次の解説に移ろう。$R;" +
                "$R場所を移動するぞ。$R;", "ＤＥＭ－ＮＳ４４１０");
                int oldMap = pc.CInt["Beginner_Map"];
                pc.CInt["Beginner_Map"] = CreateMapInstance(50081000, 10023100, 250, 132);
                LoadSpawnFile(pc.CInt["Beginner_Map"], "DB/Spawns/50081000.xml");
                Warp(pc, (uint)pc.CInt["Beginner_Map"], 10, 14);

                DeleteMapInstance(oldMap);
            }
        }
    }
}