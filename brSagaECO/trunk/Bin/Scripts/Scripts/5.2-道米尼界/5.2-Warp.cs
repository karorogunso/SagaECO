using System;
using System.Collections.Generic;
using System.Text;

using SagaDB.Item;
using SagaDB.Actor;
using SagaMap.Scripting;

using SagaLib;
using SagaScript.Chinese.Enums;
namespace SagaScript
{
    //難民テント传送
    public class P10001628 : Event
    {
        public P10001628()
        {
            this.EventID = 10001628;
        }

        public override void OnEvent(ActorPC pc)
        {
            BitMask<emojie_00> emojie_00_mask = new BitMask<emojie_00>(pc.CMask["emojie_00"]);
            if (pc.MapID == 12035001)
            {
                emojie_00_mask.SetValue(emojie_00.开关, true);
            }
            else
            {
                emojie_00_mask.SetValue(emojie_00.开关, false);
            }
            Warp(pc, 32200000, 2, 5);
            Wait(pc, 1000);
            if (emojie_00_mask.Test(emojie_00.开关))
            {
                NPCShow(pc, 11001643);
            }
            else
            {
                NPCShow(pc, 11001642);
            }
        }
    }

    public class P10001608 : Event
    {
        public P10001608()
        {
            this.EventID = 10001608;
        }

        public override void OnEvent(ActorPC pc)
        {
            Warp(pc, 12035000, 246, 2);
        }
    }

    public class P10001609 : Event
    {
        public P10001609()
        {
            this.EventID = 10001609;
        }

        public override void OnEvent(ActorPC pc)
        {
            Warp(pc, 12035001, 246, 2);
        }
    }

    public class P10001610 : Event
    {
        public P10001610()
        {
            this.EventID = 10001610;
        }

        public override void OnEvent(ActorPC pc)
        {
            Warp(pc, 12028000, 1, 245);
        }
    }

    public class P10001611 : Event
    {
        public P10001611()
        {
            this.EventID = 10001611;
        }

        public override void OnEvent(ActorPC pc)
        {
            Warp(pc, 12028001, 1, 245);
        }
    }

    public class P10001612 : Event
    {
        public P10001612()
        {
            this.EventID = 10001612;
        }

        public override void OnEvent(ActorPC pc)
        {
            Warp(pc, 12020000, 41, 245);
        }
    }

    public class P10001613 : Event
    {
        public P10001613()
        {
            this.EventID = 10001613;
        }

        public override void OnEvent(ActorPC pc)
        {
            Warp(pc, 12020001, 41, 245);
        }
    }

    public class P10001602 : Event
    {
        public P10001602()
        {
            this.EventID = 10001602;
        }

        public override void OnEvent(ActorPC pc)
        {
            Warp(pc, 12028000, 42, 1);
        }
    }

    public class P10001615 : Event
    {
        public P10001615()
        {
            this.EventID = 10001615;
        }

        public override void OnEvent(ActorPC pc)
        {
            Warp(pc, 12028001, 42, 1);
        }
    }

    public class P10001616 : Event
    {
        public P10001616()
        {
            this.EventID = 10001616;
        }

        public override void OnEvent(ActorPC pc)
        {
            Warp(pc, 12019000, 255, 168);
        }
    }

    public class P10001617 : Event
    {
        public P10001617()
        {
            this.EventID = 10001617;
        }

        public override void OnEvent(ActorPC pc)
        {
            Warp(pc, 12019001, 255, 168);
        }
    }

    public class P10001618 : Event
    {
        public P10001618()
        {
            this.EventID = 10001618;
        }

        public override void OnEvent(ActorPC pc)
        {
            Warp(pc, 12020000, 1, 168);
        }
    }

    public class P10001619 : Event
    {
        public P10001619()
        {
            this.EventID = 10001619;
        }

        public override void OnEvent(ActorPC pc)
        {
            Warp(pc, 12020001, 1, 168);
        }
    }

    public class P10001632 : Event
    {
        public P10001632()
        {
            this.EventID = 10001632;
        }

        public override void OnEvent(ActorPC pc)
        {
            Warp(pc, 12022000, 1, 159);
        }
    }

    public class P10001633 : Event
    {
        public P10001633()
        {
            this.EventID = 10001633;
        }

        public override void OnEvent(ActorPC pc)
        {
            Warp(pc, 12020000, 255, 159);
        }
    }

    public class P10001646 : Event
    {
        public P10001646()
        {
            this.EventID = 10001646;
        }

        public override void OnEvent(ActorPC pc)
        {
            BitMask<emojie_00> emojie_00_mask = new BitMask<emojie_00>(pc.CMask["emojie_00"]);
            if (emojie_00_mask.Test(emojie_00.开关))//去夜
            {
                NPCShow(pc, 11001578);//让NPC出现
                NPCHide(pc, 11001633);//隐藏NPC
                NPCHide(pc, 11001577);//隐藏NPC
                Warp(pc, 32200001, 2, 5);
            }
            else
            {
                NPCHide(pc, 11001578);//隐藏NPC
                NPCShow(pc, 11001633);//让NPC出现
                NPCShow(pc, 11001577);//让NPC出现
                Warp(pc, 32200001, 2, 5);
            }

        }

    }
    public class P10001620 : Event
    {
        public P10001620()
        {
            this.EventID = 10001620;
        }

        public override void OnEvent(ActorPC pc)
        {
            BitMask<emojie_00> emojie_00_mask = new BitMask<emojie_00>(pc.CMask["emojie_00"]);
            if (emojie_00_mask.Test(emojie_00.开关))//去夜
            {
                Warp(pc, 12020001, 108, 77);
            }
            else
            {
                Warp(pc, 12020000, 110, 75);
            }
        }
    }
    public class P10001638 : Event
    {
        public P10001638()
        {
            this.EventID = 10001638;
        }

        public override void OnEvent(ActorPC pc)
        {
            Warp(pc, 22004002, 105, 101);
        }
    }
    public class P10001639 : Event
    {
        public P10001639()
        {
            this.EventID = 10001639;
        }

        public override void OnEvent(ActorPC pc)
        {
            BitMask<emojie_00> emojie_00_mask = new BitMask<emojie_00>(pc.CMask["emojie_00"]);
            if (emojie_00_mask.Test(emojie_00.开关))//去夜
            {
                Warp(pc, 12020001, 182, 49);
            }
            else
            {
                Warp(pc, 12020000, 184, 49);
            }
        }
    }

    //黄金桥（12019000） TO 西部要塞(12021000)
    public class P10001623 : Event
    {
        public P10001623()
        {
            this.EventID = 10001623;
        }

        public override void OnEvent(ActorPC pc)
        {
            if (ODWarIsDefence(12021001) == false)
            {
                Warp(pc, 12021001, 182, 128);
                return;
            }

            if (ODWarIsDefence(12021001) == true)
            {
                Say(pc, 0, 0, "現在、ウェストフォートシティでは$R;" +
                "「防衛戦」が行われているため$R;" +
                "街の中へ入ることが出来ません。$R;" +
                "$R「羅城門」への抜け道を$R;" +
                "使用しますか？$R;", "");
                if (Select(pc, "「羅城門」へ移動する？", "", "いいえ", "はい") == 2)
                {
                    NPCShow(pc, 11001698);//让NPC出现
                    Warp(pc, 32003001, 20, 81);
                    return;
                }
            }



            Warp(pc, 12021000, 182, 128);
        }
    }

    public class P10001635 : Event
    {
        public P10001635()
        {
            this.EventID = 10001635;
        }

        public override void OnEvent(ActorPC pc)
        {
            Warp(pc, 32053000, 3, 6);
        }
    }

    public class P10001637 : Event
    {
        public P10001637()
        {
            this.EventID = 10001637;
        }

        public override void OnEvent(ActorPC pc)
        {
            Warp(pc, 12021000, 62, 131);
        }
    }

    public class P10001656 : Event
    {
        public P10001656()
        {
            this.EventID = 10001656;
        }

        public override void OnEvent(ActorPC pc)
        {
            if (ODWarIsDefence(12021001) == true)
            {
                if (ODWarCanApply(12021001))
                {
                    NPCMotion(pc, 11001722, 131);

                    Say(pc, 0, 0, "防衛戦中は勝つか負けるか……$R;" +
                    "または貴殿が倒れるまで$R;" +
                    "こちらへ戻ってくることは$R;" +
                    "出来ないぞ。$R;" +
                    "$Rそれでも行くか？$R;", "羅城門門兵");
                    switch (Select(pc, "準備はいいか？", "", "やめる", "先へ進む", "防衛方法を確認したい"))
                    {
                        case 2:
                            NPCMotion(pc, 11001722, 131);

                            Say(pc, 0, 0, "貴殿の武運を祈る！$R;", "羅城門門兵");
                            Warp(pc, 12021001, 11, 127);
                            return;
                        case 3:
                            NPCMotion(pc, 11001722, 131);

                            Say(pc, 0, 0, "分からないことがあれば$R;" +
                            "なんでも聞いてくれ。$R;", "羅城門門兵");
                            switch (Select(pc, "防衛方法の確認", "", "もういい", "街を防衛するには？", "壊れたシンボルの起動方法", "シンボルの回復方法", "シンボルはどこにあるの？", "注意点"))
                            {
                                case 2:
                                    Say(pc, 0, 0, "ウェストフォートに点在する$R;" +
                                    "シンボルを 45 分間$R;" +
                                    "1つでも守り抜くことが出来れば$R;" +
                                    "我々の勝利となる！$R;" +
                                    "$Rなぜ、45分なのかって？$R;" +
                                    "$P詳しい話は知らんが$R;" +
                                    "普段からこの街はバリアのような物に$R;" +
                                    "守られているそうだ。$R;" +
                                    "$R今まではそのバリアのおかげもあり$R;" +
                                    "敵が易々と街へ攻撃仕掛けることも$R;" +
                                    "出来なかったわけだが$R;" +
                                    "チャンプモンスターの出現が$R;" +
                                    "その事情が大きく変えたのだ。$R;" +
                                    "$P奴らの持つチャンプ能力が$R;" +
                                    "このバリアを中和し破壊活動を$R;" +
                                    "可能にしたのだよ。$R;" +
                                    "$Rその活動時間が丁度 45分間。$R;" +
                                    "つまり、敵の限界活動時間$R;" +
                                    "ぎりぎりまで、シンボルを$R;" +
                                    "守り抜くことが出来れば$R;" +
                                    "自然と我々の勝利になるというわけだ。$R;", "羅城門門兵");
                                    break;
                                case 3:
                                    Say(pc, 0, 0, "この街の各所に点在している$R;" +
                                    "「シンボル発生装置」を調べて$R;" +
                                    "「装置を起動する」を選択すると$R;" +
                                    "シンボルは展開するのだが$R;" +
                                    "ここで一つ気をつけなければ$R;" +
                                    "ならないことがある。$R;" +
                                    "$Pシンボルが展開される場所に$R;" +
                                    "障害物があるとシンボル上手く$R;" +
                                    "展開することが出来ない。$R;" +
                                    "$Rつまり、先にその障害物を$R;" +
                                    "除去する必要がある。$R;" +
                                    "$Rよく覚えておくといい。$R;", "羅城門門兵");
                                    break;
                                case 4:
                                    NPCMotion(pc, 11001722, 131);

                                    Say(pc, 0, 0, "シンボルを回復させるには$R;" +
                                    "スキル「シンボル修復」が必要になる。$R;" +
                                    "$Rこれは誰にでも扱えるスキルだが$R;" +
                                    "特にバックパッカー系が上手く$R;" +
                                    "扱う事ができるスキルだ。$R;" +
                                    "$P街へ入ったら$R;" +
                                    "一度試してみるといい。$R;", "羅城門門兵");

                                    break;
                                case 5:
                                    Say(pc, 0, 0, "シンボルの場所は$R;" +
                                    "「ＭＡＰ」上にに表示される$R;" +
                                    "赤い目印、「シンボル発生装置」の$R;" +
                                    "すぐ側に展開している。$R;" +
                                    "$Pさらに赤い目印に$R;" +
                                    "マウスカーソルを合わせることで$R;" +
                                    "近くにあるシンボルの名前が$R;" +
                                    "マップ上に表示されるからな。$R;" +
                                    "$Rここでは試せないが……$R;" +
                                    "まぁ、実際に試してみるといいだろう。$R;", "羅城門門兵");
                                    break;
                                case 6:

                                    break;
                            }
                            break;
                    }
                    return;
                }
            }
            Warp(pc, 12021000, 9, 127);
        }
    }

    public class P10001658 : Event
    {
        public P10001658()
        {
            this.EventID = 10001658;
        }

        public override void OnEvent(ActorPC pc)
        {
            NPCShow(pc, 11001698);//让NPC出现
            Warp(pc, 32003001, 19, 81);
        }
    }

    public class P10001622 : Event
    {
        public P10001622()
        {
            this.EventID = 10001622;
        }

        public override void OnEvent(ActorPC pc)
        {
            Warp(pc, 12019000, 3, 80);
        }
    }
    //-------------------(saga10)-------------------//

    public class P10001765 : Event
    {
        public P10001765()
        {
            this.EventID = 10001765;
        }

        public override void OnEvent(ActorPC pc)
        {
            Warp(pc, 22197000, 32, 16);
            NPCHide(pc, 11002156);
            NPCHide(pc, 11002116);
            NPCHide(pc, 11002115);
            NPCHide(pc, 11002114);
            NPCHide(pc, 11002113);
            NPCHide(pc, 11002112);
            NPCHide(pc, 11002106);
            NPCHide(pc, 11002105);
            NPCHide(pc, 11002104);
            NPCHide(pc, 11002103);
        }
    }
    public class P10001766 : Event
    {
        public P10001766()
        {
            this.EventID = 10001766;
        }

        public override void OnEvent(ActorPC pc)
        {
            Warp(pc, 22198000, 97, 127);
        }
    }
    public class P10001767 : Event
    {
        public P10001767()
        {
            this.EventID = 10001767;
        }

        public override void OnEvent(ActorPC pc)
        {
            Warp(pc, 22198000, 247, 127);
        }
    }

    public class P10001768 : Event
    {
        public P10001768()
        {
            this.EventID = 10001768;
        }

        public override void OnEvent(ActorPC pc)
        {
            Warp(pc, 22199000, 72, 236);
        }
    }

    public class P10001769 : Event
    {
        public P10001769()
        {
            this.EventID = 10001769;
        }

        public override void OnEvent(ActorPC pc)
        {
            Warp(pc, 22199000, 48, 127);
        }
    }

    public class P10001770 : Event
    {
        public P10001770()
        {
            this.EventID = 10001770;
        }

        public override void OnEvent(ActorPC pc)
        {
            Warp(pc, 22199000, 140, 195);
        }
    }


    public class P10001771 : Event
    {
        public P10001771()
        {
            this.EventID = 10001771;
        }

        public override void OnEvent(ActorPC pc)
        {
            Warp(pc, 22199000, 112, 23);
        }
    }
    public class P10001772 : Event
    {
        public P10001772()
        {
            this.EventID = 10001772;
        }

        public override void OnEvent(ActorPC pc)
        {
            Warp(pc, 22199000, 192, 23);
        }
    }
    public class P10001773 : Event
    {
        public P10001773()
        {
            this.EventID = 10001773;
        }

        public override void OnEvent(ActorPC pc)
        {
            Warp(pc, 22199000, 201, 212);
        }
    }
    public class P10001774 : Event
    {
        public P10001774()
        {
            this.EventID = 10001774;
        }

        public override void OnEvent(ActorPC pc)
        {
            Warp(pc, 22200000, 26, 242);
        }
    }

    public class P12001090 : Event
    {
        public P12001090()
        {
            this.EventID = 12001090;
        }

        public override void OnEvent(ActorPC pc)
        {
            ShowEffect(pc, 4539);
            Say(pc, 0, 131, "是陷阱！！$R;");
            Warp(pc, 20091000, 187, 225);
        }
    }

    //カードラボ(30166000)>下城
    public class P10001759 : Event
    {
        public P10001759()
        {
            this.EventID = 10001759;
        }

        public override void OnEvent(ActorPC pc)
        {
            Warp(pc, 10024000, 50, 150);
        }
    }

    public class P10001793 : Event
    {
        public P10001793()
        {
            this.EventID = 10001793;
        }

        public override void OnEvent(ActorPC pc)
        {
            Warp(pc, 10024000, 158, 157);
            DeleteMapInstance(pc.CInt["DEM_Customize_Map"]);
        }
    }


}