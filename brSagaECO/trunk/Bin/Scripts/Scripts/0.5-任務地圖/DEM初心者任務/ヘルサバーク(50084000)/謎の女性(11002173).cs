using System;
using System.Collections.Generic;
using System.Text;

using SagaDB.Actor;
using SagaMap.Scripting;

using SagaLib;
using SagaScript.Chinese.Enums;
namespace SagaScript.M50084000
{
    public class S11002173 : Event
    {
        public S11002173()
        {
            this.EventID = 11002173;
        }

        public override void OnEvent(ActorPC pc)
        {
            BitMask<DEMNewbie> newbie = new BitMask<DEMNewbie>(pc.CMask["DEMNewbie"]);

            if (!newbie.Test(DEMNewbie.第一次跟迷之女性说话))
            {
                NPCMove(pc, 11002173, -10050, -10550, 315, MoveType.NONE);
                Say(pc, 131, "…ふむ、珍しい奴もいたものだな。$R;" +
                "$PＤＥＭなのにＤＥＭに追われる存在…$R;" +
                "$Pここ最近は、$R;" +
                "そういう「例外」も多くなったと聞く。$R;" +
                "$R…まぁ、我にはどうでも良いことだがな。$R;", "謎の女性");

                if (Select(pc, "……", "", "何者なのかを聞く", "ドミニオンの少女のことを聞く") == 2)
                {
                    Say(pc, 131, "ドミニオンの少女？$R;" +
                    "$P…ああ、そういえば先ほど、$R;" +
                    "心が壊れた少女を見たな…。$R;" +
                    "$Pかの者であれば、$R;" +
                    "仲間であろうドミニオンが$R;" +
                    "保護しているのを見たぞ。$R;", "謎の女性");
                    NPCMove(pc, 11002173, -10050, -10550, 135, MoveType.NONE);
                    Say(pc, 111, "しかし、こんな場所に次元断層が$R;" +
                    "生まれているとはな…。$R;" +
                    "$Pそこに見える光の柱が、次元断層だ。$R;" +
                    "時々発生する、世界の歪み…だな。$R;" +
                    "$P歪みはどこかに通じているのだが、$R;" +
                    "どこに通じているのかはわからん。$R;" +
                    "$Pこの世界なのか、別の世界なのか…。$R;" +
                    "$Pいずれにせよ、貴様は運が良い。$R;" +
                    "$P貴様がＤＥＭとして生きたいのあれば、$R;" +
                    "ここに留まって死ぬが良い。$R;" +
                    "$Pだが、もし、自分の道を進みたいと$R;" +
                    "いうのであれば、$R;" +
                    "この次元断層に飛び込んでみるのも手だ。$R;" +
                    "$R…どこに行くのかはわからんがな。$R;" +
                    "$P選択するのは貴様だ。$R;" +
                    "$R自由にするが良いさ。$R;", "謎の女性");
                }
            }
            else
            {
                Say(pc, 111, "……。$R;", "謎の女性");
                Say(pc, 0, 65535, "女性は鋭い眼差しで$R;" +
                "強大な力の渦を見つめている…。$R;", " ");
            }
        }
    }
}