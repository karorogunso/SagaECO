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
                    新发型(pc);//最新發型
                    break;

                case 2:
                    if (CountItem(pc, 10020788) > 0)
                    {
                        if (pc.Gender > 0)
                        {
                            換發型(pc, 56, 56, 10020788);
                            return;
                        }
                        else
                        {
                            Say(pc, 190, "女孩专用$R;");
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
                        switch (Select(pc, "ひぐらしの紹介状", "", "梨花の髪型", "沙都子の髪型", "不做了"))
                        {
                            case 1:
                                換發型(pc, 53, 255, 10020786);
                                return;
                            case 2:
                                換發型(pc, 52, 255, 10020786);
                                return;
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
                            換發型(pc, 54, 255, 10020791);
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
                                換發型(pc, 45, 255, 10020783);
                                return;
                            case 2:
                                換發型(pc, 44, 255, 10020783);
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
                                換發型(pc, 49, 54, 10020785);
                                return;
                            case 2:
                                換發型(pc, 48, 255, 1002078);
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
                                換發型(pc, 61, 58, 10020792);
                                return;
                            case 2:
                                換發型(pc, 61, 59, 10020792);
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
                                換發型(pc, 71, 255, 1002079);
                                return;
                            case 2:
                                換發型(pc, 72, 63, 10020794);
                                return;
                            case 3:
                                換發型(pc, 72, 255, 10020794);
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
                                換發型(pc, 47, 255, 10020784);
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
                                換發型(pc, 62, 255, 10020793);
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
                                換發型(pc, 40, 50, 10020779);
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
                                換發型(pc, 42, 255, 10020782);
                                return;
                            case 2:
                                換發型(pc, 43, 51, 10020782);
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
                                換發型(pc, 41, 255, 10020780);

                                return;
                        }
                    }
                    else
                    {
                        Say(pc, 190, "需要デビューへの紹介状$R;");
                    }

                    return;
                case 14:
                    其他(pc);
                    return;
            }
        }

        void 換發型(ActorPC pc, byte HairStyle, byte Wig, uint item)
        {
            pc.HairStyle = HairStyle;
            pc.Wig = Wig;
            ShowEffect(pc, 4112);
            PlaySound(pc, 2213, false, 100, 50);
            TakeItem(pc, item, 1);
        }

        void 新发型(ActorPC pc)
        {

            switch (Select(pc, "换最新发型", "", "アルバイトの紹介状", "軍式紹介状", "伝説の紹介状", "ティタの紹介状", "愛され紹介状", "北斗の紹介状", "夏休みの紹介状", "厳島 美晴の紹介状", "ロビン・グッドフェロウの紹介状", "アングリー紹介状", "マーシャの紹介状", "西洋ロマンの紹介状", "クラシックな紹介状", "戻る"))
            {


                case 1:
                    if (CountItem(pc, 10020795) > 0)
                    {
                        switch (Select(pc, "アルバイトの紹介状", "", "清潔感のあるミディアム", "清潔感のあるショート", "戻る"))
                        {
                            case 1:
                                換發型(pc, 73, 255, 10020795);
                                return;
                            case 2:
                                換發型(pc, 74, 255, 10020795);
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
                                換發型(pc, 76, 255, 10020796);
                                return;
                            case 2:
                                換發型(pc, 75, 255, 10020796);
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
                                換發型(pc, 78, 255, 10020797);
                                return;
                            case 2:
                                換發型(pc, 77, 255, 10020797);
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
                                換發型(pc, 81, 255, 10020799);
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
                                換發型(pc, 82, 64, 10074400);
                                return;
                            case 2:
                                換發型(pc, 82, 66, 10074400);
                                return;
                            case 3:
                                換發型(pc, 79, 255, 10074400);
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
                                換發型(pc, 83, 255, 10074500);
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
                                換發型(pc, 85, 255, 10074800);
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
                                換發型(pc, 86, 255, 10075202);
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
                                換發型(pc, 87, 69, 10075201);
                                return;
                            case 2:
                                換發型(pc, 88, 69, 10075201);
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
                        switch (Select(pc, "アングリー紹介状", "", "ドーリーカールヘア（ヘッドセットあり）", "ドーリーカールヘア（ヘッドセットなし）", "スイートドーリーヘア", "戻る"))
                        {
                            case 1:
                                換發型(pc, 93, 70, 10075203);
                                return;
                            case 2:
                                換發型(pc, 93, 71, 10075203);
                                return;
                            case 3:
                                換發型(pc, 98, 255, 10075203);
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
                                換發型(pc, 80, 65, 10020798);
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
                        if (pc.Gender > 0)
                        {
                            換發型(pc, 100, 255, 10075206);
                            return;
                        }
                        else
                        {
                            Say(pc, 190, "女孩专用$R;");
                        }
                    }
                    else
                    {
                        Say(pc, 190, "需要西洋ロマンの紹介状$R;");
                    }
                    return;
                case 13:
                    if (CountItem(pc, 10075212) > 0)
                    {
                        switch (Select(pc, "ライブの紹介状", "", "ロング三つ編みおさげ", "セミロング三つ編みおさげ", "戻る"))
                        {
                            case 1:
                                換發型(pc, 108, 77, 10020795);
                                return;
                            case 2:
                                換發型(pc, 109, 255, 10020795);
                                return;
                        }
                    }
                    else
                    {
                        Say(pc, 190, "需要ライブの紹介状$R;");
                    }
                    return;
                case 14:
                    if (CountItem(pc, 10020770) > 0)
                    {
                        if (pc.Gender > 0)
                        {
                            換發型(pc, 29, 43, 10075206);
                            return;
                        }
                        else
                        {
                            Say(pc, 190, "女孩专用$R;");
                        }
                    }
                    else
                    {
                        Say(pc, 190, "需要クラシックな紹介状$R;");
                    }
                    return;
            }
        }

        void 其他(ActorPC pc)
        {

            switch (Select(pc, "有什么可以帮助吗", "", "ルチルのヘアカタログ", "プリンセスの紹介状", "ふたご座の紹介状", "わがまま紹介状", "委員長の紹介状", "貴族の紹介状", "空賊の紹介状", "武家の紹介状", "ワイルドな紹介状", "魔女の紹介状（アンニュイ）", "魔女の紹介状（ヘアピン付き）", "ラテンの紹介状", "クラシックな紹介状", "ラブモテ紹介状", "戻る"))
            {
                case 1:
                    if (CountItem(pc, 10020759) > 0)
                    {
                        switch (Select(pc, "ルチルのヘアカタログ", "", "ルチルの髪型", "戻る"))
                        {
                            case 1:
                                換發型(pc, 19, 36, 10020759);
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
                                換發型(pc, 27, 45, 10020769);
                                return;
                            case 2:
                                換發型(pc, 28, 255, 10020769);
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
                                換發型(pc, 31, 44, 10020773);
                                return;
                        }
                    }
                    else
                    {
                        Say(pc, 190, "需要ふたご座の紹介状$R;");
                    }

                    return;
                case 4:
                    if (CountItem(pc, 10075211) > 0)
                    {
                        換發型(pc, 76, 107, 10075211);
                        return;
                    }
                    else
                    {
                        Say(pc, 190, "需要わがまま紹介状$R;");
                    }
                    return;
                case 5:
                    if (CountItem(pc, 10020789) > 0)
                    {
                        換發型(pc, 57, 255, 10020789);
                        return;
                    }
                    else
                    {
                        Say(pc, 190, "需要委員長の紹介状$R;");
                    }
                    return;
                case 6:
                    if (CountItem(pc, 10020790) > 0)
                    {
                        換發型(pc, 60, 57, 10020790);
                        return;
                    }
                    else
                    {
                        Say(pc, 190, "需要貴族の紹介状$R;");
                    }
                    return;
                case 7:
                    if (CountItem(pc, 10020787) > 0)
                    {
                        換發型(pc, 55, 55, 10020787);
                        return;
                    }
                    else
                    {
                        Say(pc, 190, "需要空賊の紹介状$R;");
                    }
                    return;
                case 8:
                    if (CountItem(pc, 10020777) > 0)
                    {
                        換發型(pc, 39, 49, 10020777);
                        return;
                    }
                    else
                    {
                        Say(pc, 190, "需要武家の紹介状$R;");
                    }
                    return;
                case 9:
                    if (CountItem(pc, 10020776) > 0)
                    {
                        換發型(pc, 38, 255, 10020776);
                        return;
                    }
                    else
                    {
                        Say(pc, 190, "需要ワイルドな紹介状$R;");
                    }
                    return;
                case 10:
                    if (CountItem(pc, 10020775) > 0)
                    {
                        換發型(pc, 35, 255, 10020775);
                        return;
                    }
                    else
                    {
                        Say(pc, 190, "需要魔女の紹介状$R;");
                    }
                    return;
                case 11:
                    if (CountItem(pc, 10020775) > 0)
                    {
                        換發型(pc, 36, 255, 10020775);
                        return;
                    }
                    else
                    {
                        Say(pc, 190, "需要魔女の紹介状$R;");
                    }
                    return;
                case 12:
                    if (CountItem(pc, 10020774) > 0)
                    {
                        換發型(pc, 32, 46, 10020774);
                        return;
                    }
                    else
                    {
                        Say(pc, 190, "需要ラテンの紹介状$R;");
                    }
                    return;
                case 13:
                    if (CountItem(pc, 10020770) > 0)
                    {
                        換發型(pc, 29, 45, 10020770);
                        return;
                    }
                    else
                    {
                        Say(pc, 190, "需要クラシックな紹介状$R;");
                    }
                    return;
                case 14:
                    if (CountItem(pc, 10020768) > 0)
                    {
                        換發型(pc, 26, 255, 10020768);
                        return;
                    }
                    else
                    {
                        Say(pc, 190, "需要ラブモテ紹介状$R;");
                    }
                    return;

            }
        }

    }

}
