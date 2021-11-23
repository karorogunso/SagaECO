using System;
using System.Collections.Generic;
using System.Text;

using SagaDB.Actor;
using SagaMap.Scripting;
namespace SagaScript.M12021000
{
    public class S11001663 : Event
    {
        public S11001663()
        {
            this.EventID = 11001663;
        }


        public override void OnEvent(ActorPC pc)
        {
            if (pc.Gender > 0)
            {
                if (CountItem(pc, 10032810) > 9 && CountItem(pc, 10037600) > 9)
                {
                    女キャラ髪型変更(pc);
                    TakeItem(pc, 10032810, 10);
                    TakeItem(pc, 10037600, 10);
                    return;
                }
                else
                {
                    Say(pc, 131, "アレとアレを１０個…"); 
                }
            }
            else
            {
                if (CountItem(pc, 10032810) > 9 && CountItem(pc, 10037600) > 9)
                {
                    男キャラ髪型変更(pc);
                    TakeItem(pc, 10032810, 10);
                    TakeItem(pc, 10037600, 10);
                }
                else
                {
                    Say(pc, 131, "アレとアレを１０個…"); 
                }
            }

        }

        void 女キャラ髪型変更(ActorPC pc)
        {
            int sel;
            do
            {
                sel = Select(pc, "何をする？", "", "髪型変更", "やっぱりやめた");
                switch (sel)
                {
                    case 1:
                        int sel1;
                        do
                        {
                            sel1 = Select(pc, "どの髪型にしますか？", "", "特別紹介状", "プリティ紹介状", "ルチルのヘアカタログ", "ちょいかわ紹介状", "ゴージャス紹介状"
                                , "おてんば紹介状", "おめかし紹介状", "姫様紹介状", "ラブモテ紹介状", "クラシックな紹介状"
                                , "プリンセスの紹介状", "ふたご座の紹介状", "機械時代のヘアカタログ", "ラテンの紹介状", "魔女の紹介状"
                                , "ワイルドな紹介状", "武家の紹介状", "バレンタイン紹介状", "スイートな紹介状", "デビューへの紹介状"
                                , "おとぎの紹介状", "マネージャーの紹介状", "歌姫の紹介状", "真夏の紹介状", "委員長の紹介状"
                                , "ハロウイン紹介状", "真冬の紹介状", "ハイカラ紹介状", "やっぱりやめた");//2/10現在女性用
                            switch (sel1)
                            {
                                case 1:
                                    switch (Select(pc, "特別紹介状", "", "ツインテール", "特別なポニー", "戻る"))
                                    {
                                        case 1:
                                            pc.HairStyle = 5;
                                            pc.Wig = 8;
                                            ShowEffect(pc, 4112);
                                            PlaySound(pc, 2213, false, 100, 50);
                                            return;
                                        case 2:
                                            pc.HairStyle = 5;
                                            pc.Wig = 19;
                                            return;
                                    }
                                    break;
                                case 2:
                                    switch (Select(pc, "プリティ紹介状", "", "ショートツインテール", "外はねぼぶ", "戻る"))
                                    {
                                        case 1:
                                            pc.HairStyle = 7;
                                            pc.Wig = 35;
                                            ShowEffect(pc, 4112);
                                            PlaySound(pc, 2213, false, 100, 50);
                                            return;
                                        case 2:
                                            pc.HairStyle = 18;
                                            pc.Wig = 255;
                                            ShowEffect(pc, 4112);
                                            PlaySound(pc, 2213, false, 100, 50);
                                            return;
                                    }
                                    break;
                                case 3:
                                    switch (Select(pc, "ルチルのヘアカタログ", "", "ルチルの髪型", "戻る"))
                                    {
                                        case 1:
                                            pc.HairStyle = 19;
                                            pc.Wig = 36;
                                            ShowEffect(pc, 4112);
                                            PlaySound(pc, 2213, false, 100, 50);
                                            return;
                                    }
                                    break;
                                case 4:
                                    switch (Select(pc, "ちょいかわ紹介状", "", "スポーティーポニー", "ショート＆エクステ", "戻る"))
                                    {
                                        case 1:
                                            pc.HairStyle = 20;
                                            pc.Wig = 37;
                                            ShowEffect(pc, 4112);
                                            PlaySound(pc, 2213, false, 100, 50);
                                            return;
                                        case 2:
                                            pc.HairStyle = 0;
                                            pc.Wig = 38;
                                            ShowEffect(pc, 4112);
                                            PlaySound(pc, 2213, false, 100, 50);
                                            return;
                                    }
                                    break;
                                case 5:
                                    switch (Select(pc, "ゴージャス紹介状", "", "縦ロール", "正統派おさげ", "戻る"))
                                    {
                                        case 1:
                                            pc.HairStyle = 21;
                                            pc.Wig = 39;
                                            ShowEffect(pc, 4112);
                                            PlaySound(pc, 2213, false, 100, 50);
                                            return;
                                        case 2:
                                            pc.HairStyle = 22;
                                            pc.Wig = 255;
                                            ShowEffect(pc, 4112);
                                            PlaySound(pc, 2213, false, 100, 50);
                                            return;
                                    }
                                    break;
                                case 6:
                                    switch (Select(pc, "おてんば紹介状", "", "元気のいい妹の髪形", "戻る"))
                                    {
                                        case 1:
                                            pc.HairStyle = 23;
                                            pc.Wig = 40;
                                            ShowEffect(pc, 4112);
                                            PlaySound(pc, 2213, false, 100, 50);
                                            return;
                                    }
                                    break;
                                case 7:
                                    switch (Select(pc, "おめかし紹介状", "", "ロングツイン", "戻る"))
                                    {
                                        case 1:
                                            pc.HairStyle = 24;
                                            pc.Wig = 41;
                                            ShowEffect(pc, 4112);
                                            PlaySound(pc, 2213, false, 100, 50);
                                            return;
                                    }
                                    break;
                                case 8:
                                    switch (Select(pc, "姫様紹介状", "", "姫様の髪型", "戻る"))
                                    {
                                        case 1:
                                            pc.HairStyle = 25;
                                            pc.Wig = 255;
                                            ShowEffect(pc, 4112);
                                            PlaySound(pc, 2213, false, 100, 50);
                                            return;
                                    }
                                    break;
                                case 9:
                                    switch (Select(pc, "ラブモテ紹介状", "", "新しい出会いを予感させる髪型", "戻る"))
                                    {
                                        case 1:
                                            pc.HairStyle = 26;
                                            pc.Wig = 255;
                                            ShowEffect(pc, 4112);
                                            PlaySound(pc, 2213, false, 100, 50);
                                            return;
                                    }
                                    break;
                                case 10:
                                    switch (Select(pc, "クラシックな紹介状", "", "片側ロールヘアー", "戻る"))
                                    {
                                        case 1:
                                            pc.HairStyle = 29;
                                            pc.Wig = 43;
                                            ShowEffect(pc, 4112);
                                            PlaySound(pc, 2213, false, 100, 50);
                                            return;
                                    }
                                    break;
                                case 11:
                                    switch (Select(pc, "プリンセスの紹介状", "", "プリンセスの髪型", "プリンスの髪型", "戻る"))
                                    {
                                        case 1:
                                            pc.HairStyle = 27;
                                            pc.Wig = 45;
                                            ShowEffect(pc, 4112);
                                            PlaySound(pc, 2213, false, 100, 50);
                                            return;
                                        case 2:
                                            pc.HairStyle = 28;
                                            pc.Wig = 255;
                                            ShowEffect(pc, 4112);
                                            PlaySound(pc, 2213, false, 100, 50);
                                            return;
                                    }
                                    break;
                                case 12:
                                    switch (Select(pc, "ふたご座の紹介状", "", "後ろで髪の一部を結ったロングヘア", "戻る"))
                                    {
                                        case 1:
                                            pc.HairStyle = 31;
                                            pc.Wig = 44;
                                            ShowEffect(pc, 4112);
                                            PlaySound(pc, 2213, false, 100, 50);
                                            return;
                                    }
                                    break;
                                case 13:
                                    switch (Select(pc, "機械時代のヘアカタログ", "", "女の子用", "戻る"))
                                    {
                                        case 1:
                                            pc.HairStyle = 17;
                                            pc.Wig = 255;
                                            ShowEffect(pc, 4112);
                                            PlaySound(pc, 2213, false, 100, 50);
                                            return;
                                    }
                                    break;
                                case 14:
                                    switch (Select(pc, "ラテンの紹介状", "", "情熱的なアップ", "情熱的なショート", "戻る"))
                                    {
                                        case 1:
                                            pc.HairStyle = 32;
                                            pc.Wig = 46;
                                            ShowEffect(pc, 4112);
                                            PlaySound(pc, 2213, false, 100, 50);
                                            return;
                                        case 2:
                                            pc.HairStyle = 34;
                                            pc.Wig = 255;
                                            ShowEffect(pc, 4112);
                                            PlaySound(pc, 2213, false, 100, 50);
                                            return;
                                    }
                                    break;
                                case 15:
                                    switch (Select(pc, "魔女の紹介状", "", "アンニュイ", "ヘアピン", "戻る"))
                                    {
                                        case 1:
                                            pc.HairStyle = 35;
                                            pc.Wig = 255;
                                            ShowEffect(pc, 4112);
                                            PlaySound(pc, 2213, false, 100, 50);
                                            return;
                                        case 2:
                                            pc.HairStyle = 36;
                                            pc.Wig = 255;
                                            ShowEffect(pc, 4112);
                                            PlaySound(pc, 2213, false, 100, 50);
                                            return;
                                    }
                                    break;
                                case 16:
                                    switch (Select(pc, "ワイルドな紹介状", "", "ワイルドショート", "ワイルドミディアム", "戻る"))
                                    {
                                        case 1:
                                            pc.HairStyle = 37;
                                            pc.Wig = 255;
                                            ShowEffect(pc, 4112);
                                            PlaySound(pc, 2213, false, 100, 50);
                                            return;
                                        case 2:
                                            pc.HairStyle = 38;
                                            pc.Wig = 255;
                                            ShowEffect(pc, 4112);
                                            PlaySound(pc, 2213, false, 100, 50);
                                            return;
                                    }
                                    break;
                                case 17:
                                    switch (Select(pc, "武家の紹介状", "", "女の子用", "戻る"))
                                    {
                                        case 1:
                                            pc.HairStyle = 39;
                                            pc.Wig = 49;
                                            ShowEffect(pc, 4112);
                                            PlaySound(pc, 2213, false, 100, 50);
                                            return;
                                    }
                                    break;
                                case 18:
                                    switch (Select(pc, "バレンタイン紹介状", "", "かわいいツーサイドアップ", "戻る"))
                                    {
                                        case 1:
                                            pc.HairStyle = 40;
                                            pc.Wig = 50;
                                            ShowEffect(pc, 4112);
                                            PlaySound(pc, 2213, false, 100, 50);
                                            return;
                                    }
                                    break;
                                case 19:
                                    switch (Select(pc, "スイートな紹介状", "", "シュークリームボブ", "ロールツインテール", "戻る"))
                                    {
                                        case 1:
                                            pc.HairStyle = 42;
                                            pc.Wig = 255;
                                            ShowEffect(pc, 4112);
                                            PlaySound(pc, 2213, false, 100, 50);
                                            return;
                                        case 2:
                                            pc.HairStyle = 43;
                                            pc.Wig = 51;
                                            ShowEffect(pc, 4112);
                                            PlaySound(pc, 2213, false, 100, 50);
                                            return;
                                    }
                                    break;
                                case 20:
                                    switch (Select(pc, "デビューへの紹介状", "", "優等生ぽいロングヘアー", "戻る"))
                                    {
                                        case 1:
                                            pc.HairStyle = 41;
                                            pc.Wig = 255;
                                            ShowEffect(pc, 4112);
                                            PlaySound(pc, 2213, false, 100, 50);
                                            return;
                                    }
                                    break;
                                case 21:
                                    switch (Select(pc, "おとぎの紹介状", "", "くりんくりんショート", "ゆる三つ編み", "戻る"))
                                    {
                                        case 1:
                                            pc.HairStyle = 45;
                                            pc.Wig = 255;
                                            ShowEffect(pc, 4112);
                                            PlaySound(pc, 2213, false, 100, 50);
                                            return;
                                        case 2:
                                            pc.HairStyle = 44;
                                            pc.Wig = 255;
                                            ShowEffect(pc, 4112);
                                            PlaySound(pc, 2213, false, 100, 50);
                                            return;
                                    }
                                    break;
                                case 22:
                                    switch (Select(pc, "マネージャーの紹介状", "", "なまいきポニー", "世話焼きショート", "戻る"))
                                    {
                                        case 1:
                                            pc.HairStyle = 49;
                                            pc.Wig = 54;
                                            ShowEffect(pc, 4112);
                                            PlaySound(pc, 2213, false, 100, 50);
                                            return;
                                        case 2:
                                            pc.HairStyle = 48;
                                            pc.Wig = 255;
                                            ShowEffect(pc, 4112);
                                            PlaySound(pc, 2213, false, 100, 50);
                                            return;
                                    }
                                    break;
                                case 23:
                                    switch (Select(pc, "歌姫の紹介状", "", "歌姫らしいふわふわロングヘアー", "戻る"))
                                    {
                                        case 1:
                                            pc.HairStyle = 47;
                                            pc.Wig = 255;
                                            ShowEffect(pc, 4112);
                                            PlaySound(pc, 2213, false, 100, 50);
                                            return;
                                    }
                                    break;
                                case 24:
                                    switch (Select(pc, "真夏の紹介状", "", "ロングのミニツイン付きヘアー", "戻る"))
                                    {
                                        case 1:
                                            pc.HairStyle = 56;
                                            pc.Wig = 56;
                                            ShowEffect(pc, 4112);
                                            PlaySound(pc, 2213, false, 100, 50);
                                            return;
                                    }
                                    break;
                                case 25:
                                    switch (Select(pc, "委員長の紹介状", "", "イケてるベリーショート", "イケてるボブ", "戻る"))
                                    {
                                        case 1:
                                            pc.HairStyle = 58;
                                            pc.Wig = 255;
                                            ShowEffect(pc, 4112);
                                            PlaySound(pc, 2213, false, 100, 50);
                                            return;
                                        case 2:
                                            pc.HairStyle = 57;
                                            pc.Wig = 255;
                                            ShowEffect(pc, 4112);
                                            PlaySound(pc, 2213, false, 100, 50);
                                            return;
                                    }
                                    break;
                                case 26:
                                    switch (Select(pc, "ハロウイン紹介状", "", "リボン付きツインテール", "リボンなし", "戻る"))
                                    {
                                        case 1:
                                            pc.HairStyle = 61;
                                            pc.Wig = 58;
                                            ShowEffect(pc, 4112);
                                            PlaySound(pc, 2213, false, 100, 50);
                                            return;
                                        case 2:
                                            pc.HairStyle = 61;
                                            pc.Wig = 59;
                                            ShowEffect(pc, 4112);
                                            PlaySound(pc, 2213, false, 100, 50);
                                            return;
                                    }
                                    break;
                                case 27:
                                    switch (Select(pc, "真冬の紹介状", "", "憧れのストレートロングヘア", "戻る"))
                                    {
                                        case 1:
                                            pc.HairStyle = 62;
                                            pc.Wig = 255;
                                            ShowEffect(pc, 4112);
                                            PlaySound(pc, 2213, false, 100, 50);
                                            return;
                                    }
                                    break;
                                case 28:
                                    switch (Select(pc, "ハイカラ紹介状", "", "ボーイッシュなさらさらショート", "清楚なストレートロング（ウィッぐあり）", "清楚なストレートロング（ウィッグなし）", "戻る"))
                                    {
                                        case 1:
                                            pc.HairStyle = 71;
                                            pc.Wig = 255;
                                            ShowEffect(pc, 4112);
                                            PlaySound(pc, 2213, false, 100, 50);
                                            return;
                                        case 2:
                                            pc.HairStyle = 72;
                                            pc.Wig = 63;
                                            ShowEffect(pc, 4112);
                                            PlaySound(pc, 2213, false, 100, 50);
                                            return;
                                        case 3:
                                            pc.HairStyle = 72;
                                            pc.Wig = 255;
                                            ShowEffect(pc, 4112);
                                            PlaySound(pc, 2213, false, 100, 50);
                                            return;
                                    }
                                    break;
                                case 29:
                                    return;
                            }
                        } while (sel1 != 30);
                        break;
                }
            } while (sel != 2);
        }

        void 男キャラ髪型変更(ActorPC pc)
        {
            int sel;
            do
            {
                sel = Select(pc, "何をする？", "", "髪型変更", "やっぱりやめた");
                switch (sel)
                {
                    case 1:
                        int sel1;
                        do
                        {
                            sel1 = Select(pc, "どの髪型にしますか？", "", "アニキの紹介状", "ちょいかわ紹介状", "ゴージャス紹介状"
                                , "おてんば紹介状", "おめかし紹介状", "姫様紹介状", "ラブモテ紹介状", "クラシックな紹介状"
                                , "プリンセスの紹介状", "ふたご座の紹介状", "機械時代のヘアカタログ", "ラテンの紹介状", "魔女の紹介状"
                                , "ワイルドな紹介状", "武家の紹介状", "バレンタイン紹介状", "スイートな紹介状", "デビューへの紹介状"
                                , "おとぎの紹介状", "マネージャーの紹介状", "歌姫の紹介状", "真夏の紹介状", "委員長の紹介状"
                                , "ハロウイン紹介状", "真冬の紹介状", "ハイカラ紹介状", "やっぱりやめた");//2/10現在男性用
                            switch (sel1)
                            {
                                case 1:
                                    switch (Select(pc, "アニキの紹介状", "", "モヒカン", "リーゼント", "戻る"))
                                    {
                                        case 1:
                                            pc.HairStyle = 11;
                                            pc.Wig = 255;
                                            ShowEffect(pc, 4112);
                                            PlaySound(pc, 2213, false, 100, 50);
                                            return;
                                        case 2:
                                            pc.HairStyle = 12;
                                            pc.Wig = 255;
                                            return;
                                    }
                                    break;
                                case 2:
                                    switch (Select(pc, "ちょいかわ紹介状", "", "スポーティーポニー", "ショート＆エクステ", "戻る"))
                                    {
                                        case 1:
                                            pc.HairStyle = 20;
                                            pc.Wig = 37;
                                            ShowEffect(pc, 4112);
                                            PlaySound(pc, 2213, false, 100, 50);
                                            return;
                                        case 2:
                                            pc.HairStyle = 1;
                                            pc.Wig = 38;
                                            ShowEffect(pc, 4112);
                                            PlaySound(pc, 2213, false, 100, 50);
                                            return;
                                    }
                                    break;
                                case 3:
                                    switch (Select(pc, "ゴージャス紹介状", "", "縦ロール", "正統派おさげ", "戻る"))
                                    {
                                        case 1:
                                            pc.HairStyle = 21;
                                            pc.Wig = 39;
                                            ShowEffect(pc, 4112);
                                            PlaySound(pc, 2213, false, 100, 50);
                                            return;
                                        case 2:
                                            pc.HairStyle = 22;
                                            pc.Wig = 255;
                                            ShowEffect(pc, 4112);
                                            PlaySound(pc, 2213, false, 100, 50);
                                            return;
                                    }
                                    break;
                                case 4:
                                    switch (Select(pc, "おてんば紹介状", "", "元気のいい弟の髪形", "戻る"))
                                    {
                                        case 1:
                                            pc.HairStyle = 14;
                                            pc.Wig = 40;
                                            ShowEffect(pc, 4112);
                                            PlaySound(pc, 2213, false, 100, 50);
                                            return;
                                    }
                                    break;
                                case 5:
                                    switch (Select(pc, "おめかし紹介状", "", "サラサラボブ", "戻る"))
                                    {
                                        case 1:
                                            pc.HairStyle = 15;
                                            pc.Wig = 255;
                                            ShowEffect(pc, 4112);
                                            PlaySound(pc, 2213, false, 100, 50);
                                            return;
                                    }
                                    break;
                                case 6:
                                    switch (Select(pc, "姫様紹介状", "", "若様の髪型", "戻る"))
                                    {
                                        case 1:
                                            pc.HairStyle = 16;
                                            pc.Wig = 42;
                                            ShowEffect(pc, 4112);
                                            PlaySound(pc, 2213, false, 100, 50);
                                            return;
                                    }
                                    break;
                                case 7:
                                    switch (Select(pc, "ラブモテ紹介状", "", "新しい出会いを予感させる髪型", "戻る"))
                                    {
                                        case 1:
                                            pc.HairStyle = 17;
                                            pc.Wig = 255;
                                            ShowEffect(pc, 4112);
                                            PlaySound(pc, 2213, false, 100, 50);
                                            return;
                                    }
                                    break;
                                case 8:
                                    switch (Select(pc, "クラシックな紹介状", "", "清涼感のあるフォーマルな髪型", "戻る"))
                                    {
                                        case 1:
                                            pc.HairStyle = 18;
                                            pc.Wig = 255;
                                            ShowEffect(pc, 4112);
                                            PlaySound(pc, 2213, false, 100, 50);
                                            return;
                                    }
                                    break;
                                case 9:
                                    switch (Select(pc, "プリンセスの紹介状", "", "プリンセスの髪型", "プリンスの髪型", "戻る"))
                                    {
                                        case 1:
                                            pc.HairStyle = 27;
                                            pc.Wig = 45;
                                            ShowEffect(pc, 4112);
                                            PlaySound(pc, 2213, false, 100, 50);
                                            return;
                                        case 2:
                                            pc.HairStyle = 28;
                                            pc.Wig = 255;
                                            ShowEffect(pc, 4112);
                                            PlaySound(pc, 2213, false, 100, 50);
                                            return;
                                    }
                                    break;
                                case 10:
                                    switch (Select(pc, "ふたご座の紹介状", "", "メッシュ入りショートヘア", "戻る"))
                                    {
                                        case 1:
                                            pc.HairStyle = 23;
                                            pc.Wig = 255;
                                            ShowEffect(pc, 4112);
                                            PlaySound(pc, 2213, false, 100, 50);
                                            return;
                                    }
                                    break;
                                case 11:
                                    switch (Select(pc, "機械時代のヘアカタログ", "", "男の子用", "戻る"))
                                    {
                                        case 1:
                                            pc.HairStyle = 19;
                                            pc.Wig = 255;
                                            ShowEffect(pc, 4112);
                                            PlaySound(pc, 2213, false, 100, 50);
                                            return;
                                    }
                                    break;
                                case 12:
                                    switch (Select(pc, "ラテンの紹介状", "", "情熱的なアップ", "情熱的なショート", "戻る"))
                                    {
                                        case 1:
                                            pc.HairStyle = 32;
                                            pc.Wig = 46;
                                            ShowEffect(pc, 4112);
                                            PlaySound(pc, 2213, false, 100, 50);
                                            return;
                                        case 2:
                                            pc.HairStyle = 34;
                                            pc.Wig = 255;
                                            ShowEffect(pc, 4112);
                                            PlaySound(pc, 2213, false, 100, 50);
                                            return;
                                    }
                                    break;
                                case 13:
                                    switch (Select(pc, "魔女の紹介状", "", "アンニュイ", "ヘアピン", "戻る"))
                                    {
                                        case 1:
                                            pc.HairStyle = 35;
                                            pc.Wig = 255;
                                            ShowEffect(pc, 4112);
                                            PlaySound(pc, 2213, false, 100, 50);
                                            return;
                                        case 2:
                                            pc.HairStyle = 36;
                                            pc.Wig = 255;
                                            ShowEffect(pc, 4112);
                                            PlaySound(pc, 2213, false, 100, 50);
                                            return;
                                    }
                                    break;
                                case 14:
                                    switch (Select(pc, "ワイルドな紹介状", "", "ワイルドショート", "ワイルドミディアム", "戻る"))
                                    {
                                        case 1:
                                            pc.HairStyle = 37;
                                            pc.Wig = 255;
                                            ShowEffect(pc, 4112);
                                            PlaySound(pc, 2213, false, 100, 50);
                                            return;
                                        case 2:
                                            pc.HairStyle = 38;
                                            pc.Wig = 255;
                                            ShowEffect(pc, 4112);
                                            PlaySound(pc, 2213, false, 100, 50);
                                            return;
                                    }
                                    break;
                                case 15:
                                    switch (Select(pc, "武家の紹介状", "", "ワイルドな流浪人のような髪型", "戻る"))
                                    {
                                        case 1:
                                            pc.HairStyle = 39;
                                            pc.Wig = 48;
                                            ShowEffect(pc, 4112);
                                            PlaySound(pc, 2213, false, 100, 50);
                                            return;
                                    }
                                    break;
                                case 16:
                                    switch (Select(pc, "バレンタイン紹介状", "", "とんがった前髪が特徴的なショート", "戻る"))
                                    {
                                        case 1:
                                            pc.HairStyle = 40;
                                            pc.Wig = 255;
                                            ShowEffect(pc, 4112);
                                            PlaySound(pc, 2213, false, 100, 50);
                                            return;
                                    }
                                    break;
                                case 17:
                                    switch (Select(pc, "スイートな紹介状", "", "シュークリームボブ", "ロールツインテール", "戻る"))
                                    {
                                        case 1:
                                            pc.HairStyle = 42;
                                            pc.Wig = 255;
                                            ShowEffect(pc, 4112);
                                            PlaySound(pc, 2213, false, 100, 50);
                                            return;
                                        case 2:
                                            pc.HairStyle = 43;
                                            pc.Wig = 51;
                                            ShowEffect(pc, 4112);
                                            PlaySound(pc, 2213, false, 100, 50);
                                            return;
                                    }
                                    break;
                                case 18:
                                    switch (Select(pc, "デビューへの紹介状", "", "長めのコンサバヘアー", "戻る"))
                                    {
                                        case 1:
                                            pc.HairStyle = 41;
                                            pc.Wig = 255;
                                            ShowEffect(pc, 4112);
                                            PlaySound(pc, 2213, false, 100, 50);
                                            return;
                                    }
                                    break;
                                case 19:
                                    switch (Select(pc, "おとぎの紹介状", "", "くりんくりんショート", "ゆる三つ編み", "戻る"))
                                    {
                                        case 1:
                                            pc.HairStyle = 45;
                                            pc.Wig = 255;
                                            ShowEffect(pc, 4112);
                                            PlaySound(pc, 2213, false, 100, 50);
                                            return;
                                        case 2:
                                            pc.HairStyle = 44;
                                            pc.Wig = 255;
                                            ShowEffect(pc, 4112);
                                            PlaySound(pc, 2213, false, 100, 50);
                                            return;
                                    }
                                    break;
                                case 20:
                                    switch (Select(pc, "マネージャーの紹介状", "", "なまいきポニー", "世話焼きショート", "戻る"))
                                    {
                                        case 1:
                                            pc.HairStyle = 49;
                                            pc.Wig = 54;
                                            ShowEffect(pc, 4112);
                                            PlaySound(pc, 2213, false, 100, 50);
                                            return;
                                        case 2:
                                            pc.HairStyle = 48;
                                            pc.Wig = 255;
                                            ShowEffect(pc, 4112);
                                            PlaySound(pc, 2213, false, 100, 50);
                                            return;
                                    }
                                    break;
                                case 21:
                                    switch (Select(pc, "歌姫の紹介状", "", "クラシック風に束ねた髪型", "戻る"))
                                    {
                                        case 1:
                                            pc.HairStyle = 47;
                                            pc.Wig = 53;
                                            ShowEffect(pc, 4112);
                                            PlaySound(pc, 2213, false, 100, 50);
                                            return;
                                    }
                                    break;
                                case 22:
                                    switch (Select(pc, "真夏の紹介状", "", "前髪長めのツンツンヘアー", "戻る"))
                                    {
                                        case 1:
                                            pc.HairStyle = 56;
                                            pc.Wig = 255;
                                            ShowEffect(pc, 4112);
                                            PlaySound(pc, 2213, false, 100, 50);
                                            return;
                                    }
                                    break;
                                case 23:
                                    switch (Select(pc, "委員長の紹介状", "", "イケてるベリーショート", "イケてるボブ", "戻る"))
                                    {
                                        case 1:
                                            pc.HairStyle = 58;
                                            pc.Wig = 255;
                                            ShowEffect(pc, 4112);
                                            PlaySound(pc, 2213, false, 100, 50);
                                            return;
                                        case 2:
                                            pc.HairStyle = 57;
                                            pc.Wig = 255;
                                            ShowEffect(pc, 4112);
                                            PlaySound(pc, 2213, false, 100, 50);
                                            return;
                                    }
                                    break;
                                case 24:
                                    switch (Select(pc, "ハロウイン紹介状", "", "さばさばしたイケメン風ロングヘアー", "戻る"))
                                    {
                                        case 1:
                                            pc.HairStyle = 61;
                                            pc.Wig = 255;
                                            ShowEffect(pc, 4112);
                                            PlaySound(pc, 2213, false, 100, 50);
                                            return;
                                    }
                                    break;
                                case 25:
                                    switch (Select(pc, "真冬の紹介状", "", "一つ結いのミディアムヘア", "戻る"))
                                    {
                                        case 1:
                                            pc.HairStyle = 63;
                                            pc.Wig = 60;
                                            ShowEffect(pc, 4112);
                                            PlaySound(pc, 2213, false, 100, 50);
                                            return;
                                    }
                                    break;
                                case 26:
                                    switch (Select(pc, "ハイカラ紹介状", "", "ボーイッシュなさらさらショート", "清楚なストレートロング（ウィッぐあり）", "清楚なストレートロング（ウィッグなし）", "戻る"))
                                    {
                                        case 1:
                                            pc.HairStyle = 71;
                                            pc.Wig = 255;
                                            ShowEffect(pc, 4112);
                                            PlaySound(pc, 2213, false, 100, 50);
                                            return;
                                        case 2:
                                            pc.HairStyle = 72;
                                            pc.Wig = 63;
                                            ShowEffect(pc, 4112);
                                            PlaySound(pc, 2213, false, 100, 50);
                                            return;
                                        case 3:
                                            pc.HairStyle = 72;
                                            pc.Wig = 255;
                                            ShowEffect(pc, 4112);
                                            PlaySound(pc, 2213, false, 100, 50);
                                            return;
                                    }
                                    break;
                                case 27:
                                    return;
                            }
                        } while (sel1 != 28);
                        break;
                }
            } while (sel != 2);
        }
    }
}
            
        
     
    