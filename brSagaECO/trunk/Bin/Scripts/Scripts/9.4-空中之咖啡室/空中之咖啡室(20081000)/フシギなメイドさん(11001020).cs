using System;
using System.Collections.Generic;
using System.Text;
using SagaLib;
using SagaDB.Actor;
using SagaMap.Scripting;
using SagaScript.Chinese.Enums;
namespace SagaScript.M20081000
{
    public class S11001020 : Event
    {
        public S11001020()
        {
            this.EventID = 11001020;
        }

        public override void OnEvent(ActorPC pc)
        {
            switch (Select(pc, "有什么可以帮助吗", "", "新发型", "真夏の紹介状", "ひぐらしの紹介状", "レナの紹介状", "おとぎの紹介状", "マネージャーの紹介状", "ハロウイン紹介状", "ハイカラ紹介状", "歌姫の紹介状", "真冬の紹介状", "バレンタイン紹介状", "スイートな紹介状", "デビューへの紹介状", "其他", "没有"))
            {
                case 1:
                    switch (Select(pc, "换最新发型", "", "アルバイトの紹介状", "軍式紹介状", "伝説の紹介状", "ティタの紹介状", "愛され紹介状", "北斗の紹介状", "夏休みの紹介状", "厳島 美晴の紹介状", "ロビン・グッドフェロウの紹介状", "アングリー紹介状", "マーシャの紹介状", "西洋ロマンの紹介状", "戻る"))
                    {


                        case 1:
                            if (CountItem(pc, 10020795) > 0)
                            {
                                switch (Select(pc, "アルバイトの紹介状", "", "清潔感のあるミディアム", "清潔感のあるショート", "戻る"))
                                {
                                    case 1:
                                        pc.HairStyle = 73;
                                        pc.Wig = 255;
                                        ShowEffect(pc, 4112);
                                        PlaySound(pc, 2213, false, 100, 50);
                                        TakeItem(pc, 10020795, 1);
                                        return;
                                    case 2:
                                        pc.HairStyle = 74;
                                        pc.Wig = 255;
                                        ShowEffect(pc, 4112);
                                        PlaySound(pc, 2213, false, 100, 50);
                                        TakeItem(pc, 10020795, 1);
                                        return;
                                }
                            }
                            else
                            {
                                Say(pc, 190, "需要アルバイトの紹介状$R;");
                            }
                            return;
                        case 2:
                            if (CountItem(pc, 10020796) > 0)
                            {
                                switch (Select(pc, "軍式紹介状", "", "半人前ショートおさげ", "風格あるロングおさげ", "戻る"))
                                {
                                    case 1:
                                        pc.HairStyle = 76;
                                        pc.Wig = 255;
                                        ShowEffect(pc, 4112);
                                        PlaySound(pc, 2213, false, 100, 50);
                                        TakeItem(pc, 10020796, 1);
                                        return;
                                    case 2:
                                        pc.HairStyle = 75;
                                        pc.Wig = 255;
                                        ShowEffect(pc, 4112);
                                        PlaySound(pc, 2213, false, 100, 50);
                                        TakeItem(pc, 10020796, 1);
                                        return;
                                }
                            }
                            else
                            {
                                Say(pc, 190, "需要軍式紹介状$R;");
                            }
                            return;
                        case 3:
                            if (CountItem(pc, 10020797) > 0)
                            {
                                switch (Select(pc, "伝説の紹介状", "", "おとぎ話の主人公風ロング", "おとぎ話の主人公風ショート", "戻る"))
                                {
                                    case 1:
                                        pc.HairStyle = 78;
                                        pc.Wig = 255;
                                        ShowEffect(pc, 4112);
                                        PlaySound(pc, 2213, false, 100, 50);
                                        TakeItem(pc, 10020797, 1);
                                        return;
                                    case 2:
                                        pc.HairStyle = 77;
                                        pc.Wig = 255;
                                        ShowEffect(pc, 4112);
                                        PlaySound(pc, 2213, false, 100, 50);
                                        TakeItem(pc, 10020797, 1);
                                        return;
                                }
                            }
                            return;
                        case 4:
                            if (CountItem(pc, 10020799) > 0)
                            {
                                switch (Select(pc, "ティタの紹介状", "", "ティタの紹介状", "戻る"))
                                {
                                    case 1:
                                        pc.HairStyle = 81;
                                        pc.Wig = 255;
                                        ShowEffect(pc, 4112);
                                        PlaySound(pc, 2213, false, 100, 50);
                                        TakeItem(pc, 10020799, 1);
                                        return;
                                }
                            }
                            else
                            {
                                Say(pc, 190, "需要ティタの紹介状$R;");
                            }
                            return;
                        case 5:
                            if (CountItem(pc, 10074400) > 0)
                            {
                                switch (Select(pc, "愛され紹介状", "", "愛されポニー", "愛されポニーリボン付き", "マニッシュボブ", "戻る"))
                                {
                                    case 1:
                                        pc.HairStyle = 82;
                                        pc.Wig = 64;
                                        ShowEffect(pc, 4112);
                                        PlaySound(pc, 2213, false, 100, 50);
                                        TakeItem(pc, 10074400, 1);
                                        return;
                                    case 2:
                                        pc.HairStyle = 82;
                                        pc.Wig = 66;
                                        ShowEffect(pc, 4112);
                                        PlaySound(pc, 2213, false, 100, 50);
                                        TakeItem(pc, 10074400, 1);
                                        return;
                                    case 3:
                                        pc.HairStyle = 79;
                                        pc.Wig = 255;
                                        ShowEffect(pc, 4112);
                                        PlaySound(pc, 2213, false, 100, 50);
                                        TakeItem(pc, 10074400, 1);
                                        return;
                                }
                            }
                            else
                            {
                                Say(pc, 190, "需要愛され紹介状$R;");
                            }

                            return;
                        case 6:
                            if (CountItem(pc, 10074500) > 0)
                            {
                                switch (Select(pc, "北斗の紹介状", "", "ユリアの髪型", "戻る"))
                                {
                                    case 1:
                                        pc.HairStyle = 83;
                                        pc.Wig = 255;
                                        ShowEffect(pc, 4112);
                                        PlaySound(pc, 2213, false, 100, 50);
                                        TakeItem(pc, 10074500, 1);
                                        return;
                                }
                            }
                            else
                            {
                                Say(pc, 190, "需要北斗の紹介状$R;");
                            }
                            return;
                        case 7:
                            if (CountItem(pc, 10074800) > 0)
                            {
                                switch (Select(pc, "夏休みの紹介状", "", "ニードルショート", "戻る"))
                                {
                                    case 1:
                                        pc.HairStyle = 85;
                                        pc.Wig = 255;
                                        ShowEffect(pc, 4112);
                                        PlaySound(pc, 2213, false, 100, 50);
                                        TakeItem(pc, 10074800, 1);
                                        return;
                                }
                            }
                            else
                            {
                                Say(pc, 190, "需要夏休みの紹介状$R;");
                            }
                            return;
                        case 8:
                            if (CountItem(pc, 10075202) > 0)
                            {
                                switch (Select(pc, "厳島 美晴の紹介状", "", "厳島美晴の髪型", "戻る"))
                                {
                                    case 1:
                                        pc.HairStyle = 86;
                                        pc.Wig = 255;
                                        ShowEffect(pc, 4112);
                                        PlaySound(pc, 2213, false, 100, 50);
                                        TakeItem(pc, 10075202, 1);
                                        return;
                                }
                            }
                            else
                            {
                                Say(pc, 190, "需要厳島 美晴の紹介状$R;");
                            }
                            return;
                        case 9:
                            if (CountItem(pc, 10075201) > 0)
                            {
                                switch (Select(pc, "ロビン・グッドフェロウの紹介状", "", "ロビン・グッドフェロウの髪型（ヘアバンドあり）", "ロビン・グッドフェロウの髪型（ヘアバンドなし）", "戻る"))
                                {
                                    case 1:
                                        pc.HairStyle = 87;
                                        pc.Wig = 69;
                                        ShowEffect(pc, 4112);
                                        PlaySound(pc, 2213, false, 100, 50);
                                        TakeItem(pc, 10075201, 1);
                                        return;
                                    case 2:
                                        pc.HairStyle = 88;
                                        pc.Wig = 69;
                                        ShowEffect(pc, 4112);
                                        PlaySound(pc, 2213, false, 100, 50);
                                        TakeItem(pc, 10075201, 1);
                                        return;
                                }
                            }
                            else
                            {
                                Say(pc, 190, "需要ロビン・グッドフェロウの紹介状$R;");
                            }
                            return;
                        case 10:
                            if (CountItem(pc, 10075203) > 0)
                            {
                                switch (Select(pc, "アングリー紹介状", "", "ドーリーカールヘア（ヘッドセットあり）", "ドーリーカールヘア（ヘッドセットなし）","スイートドーリーヘア", "戻る"))
                                {
                                    case 1:
                                        pc.HairStyle = 93;
                                        pc.Wig = 70;
                                        ShowEffect(pc, 4112);
                                        PlaySound(pc, 2213, false, 100, 50);
                                        TakeItem(pc, 10075203, 1);
                                        return;
                                    case 2:
                                        pc.HairStyle = 93;
                                        pc.Wig = 71;
                                        ShowEffect(pc, 4112);
                                        PlaySound(pc, 2213, false, 100, 50);
                                        TakeItem(pc, 10075203, 1);
                                        return;
                                    case 3:
                                        pc.HairStyle = 98;
                                        pc.Wig = 255;
                                        ShowEffect(pc, 4112);
                                        PlaySound(pc, 2213, false, 100, 50);
                                        TakeItem(pc, 10075203, 1);
                                        return;
                                }
                            }
                            else
                            {
                                Say(pc, 190, "需要アングリー紹介状$R;");
                            }
                            return;
                        case 11:
                            if (CountItem(pc, 10020798) > 0)
                            {
                                switch (Select(pc, "マーシャの紹介状", "", "マーシャの髪型", "戻る"))
                                {
                                    case 1:
                                        pc.HairStyle = 80;
                                        pc.Wig = 65;
                                        ShowEffect(pc, 4112);
                                        PlaySound(pc, 2213, false, 100, 50);
                                        TakeItem(pc, 10020798, 1);
                                        return;
                                }
                            }
                            else
                            {
                                Say(pc, 190, "需要マーシャの紹介状$R;");
                            }
                            return;
                        case 12:
                            if (CountItem(pc, 10075206) > 0)
                            {
                                TakeItem(pc, 10075206, 1);
                                if (pc.Gender > 0)
                                {
                                    pc.HairStyle = 100;
                                    pc.Wig = 255;
                                    ShowEffect(pc, 4112);
                                    PlaySound(pc, 2213, false, 100, 50);
                                }
                                else
                                {
                                    pc.HairStyle = 99;
                                    pc.Wig = 255;
                                    ShowEffect(pc, 4112);
                                    PlaySound(pc, 2213, false, 100, 50);
                                }
                            }
                            else
                            {
                                Say(pc, 190, "需要西洋ロマンの紹介状$R;");
                            }
                            return;
                    }//最新發型

                    break;



                        case 2:
                            if (CountItem(pc, 10020788) > 0)
                            {
                                TakeItem(pc, 10020788, 1);
                                if (pc.Gender > 0)
                                {
                                    pc.HairStyle = 56;
                                    pc.Wig = 56;
                                    ShowEffect(pc, 4112);
                                    PlaySound(pc, 2213, false, 100, 50);
                                }
                                else
                                {
                                    pc.HairStyle = 56;
                                    pc.Wig = 255;
                                    ShowEffect(pc, 4112);
                                    PlaySound(pc, 2213, false, 100, 50);
                                }
                            }
                            else
                            {
                                Say(pc, 190, "需要真夏の紹介状$R;");
                            }
                            return;
                        case 3:
                            if (CountItem(pc, 10020786) > 0)
                            {
                                if (pc.Gender > 0)
                                {
                                    switch (Select(pc, "ひぐらしの紹介状", "", "梨花の髪型", "沙都子の髪型", "不做了"))
                                    {
                                        case 1:
                                            pc.HairStyle = 53;
                                            pc.Wig = 255;
                                            ShowEffect(pc, 4112);
                                            PlaySound(pc, 2213, false, 100, 50);
                                            TakeItem(pc, 10020786, 1);
                                            return;
                                        case 2:
                                            pc.HairStyle = 52;
                                            pc.Wig = 255;
                                            ShowEffect(pc, 4112);
                                            PlaySound(pc, 2213, false, 100, 50);
                                            TakeItem(pc, 10020786, 1);
                                            return;
                                    }
                                }
                                else
                                {
                                    Say(pc, 190, "女孩专用$R;");
                                }

                            }
                            else
                            {
                                Say(pc, 190, "需要ひぐらしの紹介状$R;");
                            }

                            return;
                        case 4:
                            if (CountItem(pc, 10020791) > 0)
                            {
                                if (pc.Gender > 0)
                                {
                                    pc.HairStyle = 54;
                                    pc.Wig = 255;
                                    ShowEffect(pc, 4112);
                                    PlaySound(pc, 2213, false, 100, 50);
                                    TakeItem(pc, 10020791, 1);
                                }
                                else
                                {
                                    Say(pc, 190, "女孩专用$R;");
                                }
                            }
                            else
                            {
                                Say(pc, 190, "需要レナの紹介状$R;");
                            }
                            return;
                        case 5:
                            if (CountItem(pc, 10020783) > 0)
                            {
                                switch (Select(pc, "おとぎの紹介状", "", "くりんくりんショート", "ゆる三つ編み", "戻る"))
                                {

                                    case 1:
                                        pc.HairStyle = 45;
                                        pc.Wig = 255;
                                        ShowEffect(pc, 4112);
                                        PlaySound(pc, 2213, false, 100, 50);
                                        TakeItem(pc, 10020783, 1);
                                        return;
                                    case 2:
                                        pc.HairStyle = 44;
                                        pc.Wig = 255;
                                        ShowEffect(pc, 4112);
                                        PlaySound(pc, 2213, false, 100, 50);
                                        TakeItem(pc, 10020783, 1);
                                        return;
                                }
                            }
                            else
                            {
                                Say(pc, 190, "需要おとぎの紹介状$R;");
                            }

                            return;
                        case 6:
                            if (CountItem(pc, 10020785) > 0)
                            {
                                switch (Select(pc, "マネージャーの紹介状", "", "なまいきポニー", "世話焼きショート", "戻る"))
                                {
                                    case 1:
                                        pc.HairStyle = 49;
                                        pc.Wig = 54;
                                        ShowEffect(pc, 4112);
                                        PlaySound(pc, 2213, false, 100, 50);
                                        TakeItem(pc, 10020785, 1);
                                        return;
                                    case 2:
                                        pc.HairStyle = 48;
                                        pc.Wig = 255;
                                        ShowEffect(pc, 4112);
                                        PlaySound(pc, 2213, false, 100, 50);
                                        TakeItem(pc, 10020785, 1);
                                        return;
                                }

                            }
                            else
                            {
                                Say(pc, 190, "需要マネージャーの紹介状$R;");
                            }
                            return;
                        case 7:
                            if (CountItem(pc, 10020792) > 0)
                            {

                                switch (Select(pc, "ハロウイン紹介状", "", "リボン付きツインテール", "リボンなし", "戻る"))
                                {
                                    case 1:
                                        pc.HairStyle = 61;
                                        pc.Wig = 58;
                                        ShowEffect(pc, 4112);
                                        PlaySound(pc, 2213, false, 100, 50);
                                        TakeItem(pc, 10020792, 1);
                                        return;
                                    case 2:
                                        pc.HairStyle = 61;
                                        pc.Wig = 59;
                                        ShowEffect(pc, 4112);
                                        PlaySound(pc, 2213, false, 100, 50);
                                        TakeItem(pc, 10020792, 1);
                                        return;
                                }
                            }
                            else
                            {
                                Say(pc, 190, "需要ハロウイン紹介状$R;");
                            }
                            return;
                        case 8:
                            if (CountItem(pc, 10020794) > 0)
                            {
                                switch (Select(pc, "ハイカラ紹介状", "", "ボーイッシュなさらさらショート", "清楚なストレートロング（ウィッぐあり）", "清楚なストレートロング（ウィッグなし）", "戻る"))
                                {
                                    case 1:
                                        pc.HairStyle = 71;
                                        pc.Wig = 255;
                                        ShowEffect(pc, 4112);
                                        PlaySound(pc, 2213, false, 100, 50);
                                        TakeItem(pc, 10020794, 1);
                                        return;
                                    case 2:
                                        pc.HairStyle = 72;
                                        pc.Wig = 63;
                                        ShowEffect(pc, 4112);
                                        PlaySound(pc, 2213, false, 100, 50);
                                        TakeItem(pc, 10020794, 1);
                                        return;
                                    case 3:
                                        pc.HairStyle = 72;
                                        pc.Wig = 255;
                                        ShowEffect(pc, 4112);
                                        PlaySound(pc, 2213, false, 100, 50);
                                        TakeItem(pc, 10020794, 1);
                                        return;
                                }
                            }
                            else
                            {
                                Say(pc, 190, "需要ハイカラ紹介状$R;");
                            }
                            return;
                        case 9:
                            if (CountItem(pc, 10020784) > 0)
                            {
                                switch (Select(pc, "歌姫の紹介状", "", "歌姫らしいふわふわロングヘアー", "戻る"))
                                {
                                    case 1:

                                        pc.HairStyle = 47;
                                        pc.Wig = 255;
                                        ShowEffect(pc, 4112);
                                        PlaySound(pc, 2213, false, 100, 50);
                                        TakeItem(pc, 10020784, 1);
                                        return;
                                }
                            }

                            else
                            {
                                Say(pc, 190, "需要歌姫の紹介状$R;");
                            }
                            return;
                        case 10:
                            if (CountItem(pc, 10020793) > 0)
                            {
                                switch (Select(pc, "真冬の紹介状", "", "憧れのストレートロングヘア", "戻る"))
                                {
                                    case 1:
                                        pc.HairStyle = 62;
                                        pc.Wig = 255;
                                        ShowEffect(pc, 4112);
                                        PlaySound(pc, 2213, false, 100, 50);
                                        TakeItem(pc, 10020793, 1);
                                        return;
                                }
                            }
                            else
                            {
                                Say(pc, 190, "需要真冬の紹介状$R;");
                            }
                            return;
                        case 11:
                            if (CountItem(pc, 10020779) > 0)
                            {
                                switch (Select(pc, "バレンタイン紹介状", "", "かわいいツーサイドアップ", "戻る"))
                                {
                                    case 1:
                                        pc.HairStyle = 40;
                                        pc.Wig = 50;
                                        ShowEffect(pc, 4112);
                                        PlaySound(pc, 2213, false, 100, 50);
                                        TakeItem(pc, 10020779, 1);
                                        return;
                                }
                            }
                            else
                            {
                                Say(pc, 190, "需要バレンタイン紹介状$R;");
                            }


                            return;
                        case 12:
                            if (CountItem(pc, 10020782) > 0)
                            {
                                switch (Select(pc, "スイートな紹介状", "", "シュークリームボブ", "ロールツインテール", "戻る"))
                                {
                                    case 1:
                                        pc.HairStyle = 42;
                                        pc.Wig = 255;
                                        ShowEffect(pc, 4112);
                                        PlaySound(pc, 2213, false, 100, 50);
                                        TakeItem(pc, 10020782, 1);
                                        return;
                                    case 2:
                                        pc.HairStyle = 43;
                                        pc.Wig = 51;
                                        ShowEffect(pc, 4112);
                                        PlaySound(pc, 2213, false, 100, 50);
                                        TakeItem(pc, 10020782, 1);
                                        return;
                                }
                            }
                            else
                            {
                                Say(pc, 190, "需要スイートな紹介状$R;");
                            }
                            return;
                        case 13:
                            if (CountItem(pc, 10020780) > 0)
                            {
                                switch (Select(pc, "デビューへの紹介状", "", "優等生ぽいロングヘアー", "戻る"))
                                {
                                    case 1:
                                        pc.HairStyle = 41;
                                        pc.Wig = 255;
                                        ShowEffect(pc, 4112);
                                        PlaySound(pc, 2213, false, 100, 50);
                                        TakeItem(pc, 10020780, 1);

                                        return;
                                }
                            }
                            else
                            {
                                Say(pc, 190, "需要デビューへの紹介状$R;");
                            }

                            return;
                        case 14:
                            switch (Select(pc, "有什么可以帮助吗", "", "ルチルのヘアカタログ", "プリンセスの紹介状", "ふたご座の紹介状", "戻る"))
                            {
                                case 1:
                                    if (CountItem(pc, 10020759) > 0)
                                    {
                                        switch (Select(pc, "ルチルのヘアカタログ", "", "ルチルの髪型", "戻る"))
                                        {
                                            case 1:

                                                pc.HairStyle = 19;
                                                pc.Wig = 36;
                                                ShowEffect(pc, 4112);
                                                PlaySound(pc, 2213, false, 100, 50);
                                                TakeItem(pc, 10020759, 1);
                                                return;

                                        }
                                    }
                                    else
                                    {
                                        Say(pc, 190, "需要ルチルのヘアカタログ$R;");
                                    }

                                    return;
                                case 2:
                                    if (CountItem(pc, 10020769) > 0)
                                    {
                                        switch (Select(pc, "プリンセスの紹介状", "", "プリンセスの髪型", "プリンスの髪型", "戻る"))
                                        {
                                            case 1:
                                                pc.HairStyle = 27;
                                                pc.Wig = 45;
                                                ShowEffect(pc, 4112);
                                                PlaySound(pc, 2213, false, 100, 50);
                                                TakeItem(pc, 10020769, 1);
                                                return;
                                            case 2:
                                                pc.HairStyle = 28;
                                                pc.Wig = 255;
                                                ShowEffect(pc, 4112);
                                                PlaySound(pc, 2213, false, 100, 50);
                                                TakeItem(pc, 10020769, 1);
                                                return;
                                        }
                                    }
                                    else
                                    {
                                        Say(pc, 190, "需要プリンセスの紹介状$R;");
                                    }

                                    return;
                                case 3:
                                    if (CountItem(pc, 10020773) > 0)
                                    {
                                        switch (Select(pc, "ふたご座の紹介状", "", "後ろで髪の一部を結ったロングヘア", "戻る"))
                                        {
                                            case 1:
                                                pc.HairStyle = 31;
                                                pc.Wig = 44;
                                                ShowEffect(pc, 4112);
                                                PlaySound(pc, 2213, false, 100, 50);
                                                TakeItem(pc, 10020773, 1);
                                                return;
                                        }
                                    }
                                    else
                                    {
                                        Say(pc, 190, "需要ふたご座の紹介状$R;");
                                    }

                                    return;

                            }

                            return;


                    }

            }
        }
    }
