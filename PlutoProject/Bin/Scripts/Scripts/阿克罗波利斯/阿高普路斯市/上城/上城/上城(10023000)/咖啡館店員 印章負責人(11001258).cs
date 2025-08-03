using System;
using System.Collections.Generic;
using System.Text;

using SagaDB.Actor;
using SagaMap.Scripting;

using SagaLib;
using SagaScript.Chinese.Enums;
//所在地圖:上城(10023000) NPC基本信息:咖啡館店員 印章負責人(11001258) X:106 Y:122
namespace SagaScript.M10023000
{
    public class S11001258 : Event
    {
        public S11001258()
        {
            this.EventID = 11001258;

        }

        public override void OnEvent(ActorPC pc)
        {
            BitMask<Acropolisut_01> Acropolisut_01_mask = pc.CMask["Acropolisut_01"];
            if (!Acropolisut_01_mask.Test(Acropolisut_01.獲得手冊))
            {
                Say(pc, 0, 131, "……おや？$R;" +
                    "何かカードのようなものが落ちている。$R;");
                switch (Select(pc, "拾いますか？", "", "いいえ", "はい"))
                {
                    case 1:
                        break;
                    case 2:
                        if (CheckInventory(pc, 10029387, 1))
                        {
                            Acropolisut_01_mask.SetValue(Acropolisut_01.獲得手冊, true);
                            GiveItem(pc, 10029387, 1);
                            Say(pc, 11001258, 131, "あ～っ、すみません。$R;" +
                                "拾ってくれてありがとうございます。$R;" +
                                "$R後で拾おうって思ってたのですが$R;" +
                                "カウンターの外に落ちちゃったから$R;" +
                                "つい面倒で……ウフフ！$R;" +
                                "$Pそ～だっ、よかったら$R;" +
                                "その『モンスタースタンプ帳』$R;" +
                                "貰ってください。$R;" +
                                "$Rこれは、酒屋で新しくはじめた$R;" +
                                "「モンスタースタンプ集め」に$R;" +
                                "便利なカードなんですよ。$R;" +
                                "$Pそこのボードに詳しい説明が$R;" +
                                "書いてあるので$R;" +
                                "よければ読んでみてください。$R;");
                            return;
                        }
                        PlaySound(pc, 2041, false, 100, 50);
                        Say(pc, 0, 131, "荷物がいっぱいで持てなかった！$R;");
                        break;
                }
            }
            if (CountItem(pc, 10029388) >= 1)
            {
                Say(pc, 11001258, 159, "いらっしゃいませ。$R;" +
                    "今日はどうなさいますか？$R;" +
                    "$R……あら？$R;" +
                    "なんだかボロボロな$R;" +
                    "モンスタースタンプ帳ですね。$R;" +
                    "$Pえー…と…、あっ、これは$R;" +
                    "１０年前のスタンプ帳のようです。$R;" +
                    "$R今とはスタンプの種類や$R;" +
                    "景品がちょっと違うんですよね。$R;" +
                    "お父さんのなのかしら？$R;" +
                    "$Pウフフ、大丈夫ですよ。$R;" +
                    "ちゃんと景品と交換できます。$R;" +
                    "$Rもちろん、当時の景品ですから$R;" +
                    "今のものと多少景品が異なります。$R;" +
                    "その点はご注意ください。$R;" +
                    "$Pえーと、プルルスタンプが$R;" +
                    "揃ってるみたいですね。$R;" +
                    "$R景品と交換しますか？$R;");
                switch (Select(pc, "どうする？", "", "いいえ", "はい"))
                {
                    case 1:
                        break;
                    case 2:
                        if (!CheckInventory(pc, 10000000, 1))
                        {
                            Say(pc, 11001258, 131, "景品をお渡しするので$R;" +
                                "荷物を軽くしてきてください♪$R;" +
                                "……ウフフ！$R;");
                            return;
                        }
                        TakeItem(pc, 10029388, 1);
                        GiveRandomTreasure(pc, "STAMP_G");
                        PlaySound(pc, 4006, false, 100, 50);
                        Say(pc, 0, 131, "１０年前のプルルスタンプの$R;" +
                            "景品を手に入れた！$R;");
                        EXIT(pc);
                        break;
                }
            }

            Say(pc, 11001258, 159, "欢迎光临。$R;" +
                "今天有何贵干？$R;");
            switch (Select(pc, "怎么办？", "", "什么也不做", "交换礼物", "领取新的印章手册", "闲聊一会吧"))
            {
                case 1:
                    break;
                case 2:
                    if (!CheckInventory(pc, 10000000, 1))
                    {
                        Say(pc, 11001258, 131, "景品をお渡しするので$R;" +
                            "荷物を軽くしてきてください♪$R;" +
                            "……ウフフ！$R;");
                        return;
                    }
                    if (CheckStampGenre(pc, StampGenre.Pururu) &&
                        CheckStampGenre(pc, StampGenre.Field) &&
                        CheckStampGenre(pc, StampGenre.Coast) &&
                        CheckStampGenre(pc, StampGenre.Wild) &&
                        CheckStampGenre(pc, StampGenre.Cave) &&
                        CheckStampGenre(pc, StampGenre.Snow) &&
                        CheckStampGenre(pc, StampGenre.Colliery) &&
                        CheckStampGenre(pc, StampGenre.Northan) &&
                        CheckStampGenre(pc, StampGenre.IronSouth) &&
                        CheckStampGenre(pc, StampGenre.SouthDungeon) &&
                        CheckStampGenre(pc, StampGenre.Special))
                    {
                        PlaySound(pc, 2449, false, 100, 50);
                        ShowEffect(pc, 9911);
                        Wait(pc, 333);
                        ShowEffect(pc, 9911);
                        Wait(pc, 333);
                        ShowEffect(pc, 9911);
                        Wait(pc, 333);
                        ShowEffect(pc, 9911);
                        Wait(pc, 333);
                        ShowEffect(pc, 9911);
                        Say(pc, 11001258, 131, "オールコンプリート出ました～！$R;" +
                            "$Pおめでとうございます。$R;" +
                            "オールコンプリートの景品と$R;" +
                            "交換しますか？$R;");
                        switch (Select(pc, "どの景品と交換する？", "", "交換しない", "オールコンプリートの景品", "スペシャルコンプリートの景品", "100個コンプリートの景品", "通常コンプリートの景品"))
                        {
                            case 1:
                                return;
                            case 2:
                                Say(pc, 11001258, 131, "スタンプ帳に記載された$R;" +
                                    "すべてのスタンプは$R;" +
                                    "消えてしまいますがよろしいですか？$R;");
                                switch (Select(pc, "どうする？", "", "交換しない", "交換する"))
                                {
                                    case 1:
                                        return;
                                    case 2:
                                        Say(pc, 11001258, 131, "こちらがオールコンプリートの$R;" +
                                            "景品になります。$R;" +
                                            "$Rどうぞ、お受け取りください。$R;");
                                        switch (Select(pc, "どうする？", "", "やめる", "背負い魔・ブーストパパ", "背負い魔・ブーストパパ（光）", "背負い魔・ブーストパパ（闇）"))
                                        {
                                            case 1:
                                                return;
                                            case 2:
                                                PlaySound(pc, 4006, false, 100, 50);
                                                Say(pc, 0, 131, "オールコンプリートの景品を$R;" +
                                                    "手に入れた！$R;");
                                                GiveItem(pc, 10057600, 1);
                                                break;
                                            case 3:
                                                PlaySound(pc, 4006, false, 100, 50);
                                                Say(pc, 0, 131, "オールコンプリートの景品を$R;" +
                                                    "手に入れた！$R;");
                                                GiveItem(pc, 10057650, 1);
                                                break;
                                            case 4:
                                                PlaySound(pc, 4006, false, 100, 50);
                                                Say(pc, 0, 131, "オールコンプリートの景品を$R;" +
                                                    "手に入れた！$R;");
                                                GiveItem(pc, 10057651, 1);
                                                break;
                                        }
                                        ClearStampGenre(pc, StampGenre.Pururu);
                                        ClearStampGenre(pc, StampGenre.Field);
                                        ClearStampGenre(pc, StampGenre.Coast);
                                        ClearStampGenre(pc, StampGenre.Wild);
                                        ClearStampGenre(pc, StampGenre.Cave);
                                        ClearStampGenre(pc, StampGenre.Snow);
                                        ClearStampGenre(pc, StampGenre.Colliery);
                                        ClearStampGenre(pc, StampGenre.Northan);
                                        ClearStampGenre(pc, StampGenre.IronSouth);
                                        ClearStampGenre(pc, StampGenre.SouthDungeon);
                                        ClearStampGenre(pc, StampGenre.Special);
                                        EXIT(pc);
                                        break;
                                }
                                break;
                            case 3:
                                Say(pc, 11001258, 131, "スタンプ帳に記載された$R;" +
                                    "スペシャルスタンプは$R;" +
                                    "消えてしまいますがよろしいですか？$R;");
                                switch (Select(pc, "どうする？", "", "交換しない", "交換する"))
                                {
                                    case 1:
                                        break;
                                    case 2:
                                        Say(pc, 11001258, 131, "こちらがスペシャルコンプリートの$R;" +
                                            "景品になります。$R;" +
                                            "$Rどうぞ、お受け取りください。$R;");
                                        PlaySound(pc, 4006, false, 100, 50);
                                        Say(pc, 0, 131, "スペシャルコンプリートの景品を$R;" +
                                            "手に入れた！$R;");
                                        GiveRandomTreasure(pc, "STAMP_A");
                                        ClearStampGenre(pc, StampGenre.Special);
                                        EXIT(pc);
                                        break;
                                }
                                break;
                            case 4:
                                一百種(pc);
                                break;
                            case 5:
                                普通印章(pc);
                                break;
                        }
                        return;
                    }
                    if (CheckStampGenre(pc, StampGenre.Special))
                    {
                        PlaySound(pc, 2449, false, 100, 50);
                        ShowEffect(pc, 9911);
                        Wait(pc, 333);
                        ShowEffect(pc, 9911);
                        Wait(pc, 333);
                        ShowEffect(pc, 9911);
                        Say(pc, 11001258, 131, "スペシャルコンプリート出ました～！$R;" +
                            "$Pおめでとうございます。$R;" +
                            "スペシャルコンプリートの景品と$R;" +
                            "交換しますか？$R;");
                        switch (Select(pc, "どの景品と交換する？", "", "交換しない", "スペシャルコンプリートの景品", "通常コンプリートの景品"))
                        {
                            case 1:
                                break;
                            case 2:
                                Say(pc, 11001258, 131, "スタンプ帳に記載された$R;" +
                                    "スペシャルスタンプは$R;" +
                                    "消えてしまいますがよろしいですか？$R;");
                                switch (Select(pc, "どうする？", "", "交換しない", "交換する"))
                                {
                                    case 1:
                                        break;
                                    case 2:
                                        Say(pc, 11001258, 131, "こちらがスペシャルコンプリートの$R;" +
                                            "景品になります。$R;" +
                                            "$Rどうぞ、お受け取りください。$R;");
                                        PlaySound(pc, 4006, false, 100, 50);
                                        Say(pc, 0, 131, "スペシャルコンプリートの景品を$R;" +
                                            "手に入れた！$R;");
                                        GiveRandomTreasure(pc, "STAMP_A");
                                        ClearStampGenre(pc, StampGenre.Special);
                                        EXIT(pc);
                                        break;
                                }
                                break;
                            case 3:
                                普通印章(pc);
                                break;
                        }
                        return;
                    }

                    if (CheckStampGenre(pc, StampGenre.Pururu) &&
                        CheckStampGenre(pc, StampGenre.Field) &&
                        CheckStampGenre(pc, StampGenre.Coast) &&
                        CheckStampGenre(pc, StampGenre.Wild) &&
                        CheckStampGenre(pc, StampGenre.Cave) &&
                        CheckStampGenre(pc, StampGenre.Snow) &&
                        CheckStampGenre(pc, StampGenre.Colliery) &&
                        CheckStampGenre(pc, StampGenre.Northan) &&
                        CheckStampGenre(pc, StampGenre.IronSouth) &&
                        CheckStampGenre(pc, StampGenre.SouthDungeon))
                    {
                        PlaySound(pc, 2449, false, 100, 50);
                        ShowEffect(pc, 9911);
                        Wait(pc, 333);
                        ShowEffect(pc, 9911);
                        Wait(pc, 333);
                        ShowEffect(pc, 9911);
                        Say(pc, 11001258, 131, "100個コンプリート出ました～！$R;" +
                            "$Pおめでとうございます。$R;" +
                            "100個コンプリートの景品と$R;" +
                            "交換しますか？$R;");
                        switch (Select(pc, "どの景品と交換する？", "", "交換しない", "100個コンプリートの景品", "通常コンプリートの景品"))
                        {
                            case 1:
                                break;
                            case 2:
                                一百種(pc);
                                break;
                            case 3:
                                普通印章(pc);
                                break;
                        }
                        return;
                    }
                    if (CheckStampGenre(pc, StampGenre.Pururu) ||
                        CheckStampGenre(pc, StampGenre.Field) ||
                        CheckStampGenre(pc, StampGenre.Coast) ||
                        CheckStampGenre(pc, StampGenre.Wild) ||
                        CheckStampGenre(pc, StampGenre.Cave) ||
                        CheckStampGenre(pc, StampGenre.Snow) ||
                        CheckStampGenre(pc, StampGenre.Colliery) ||
                        CheckStampGenre(pc, StampGenre.Northan) ||
                        CheckStampGenre(pc, StampGenre.IronSouth) ||
                        CheckStampGenre(pc, StampGenre.SouthDungeon))
                    {
                        PlaySound(pc, 2449, false, 100, 50);
                        ShowEffect(pc, 9911);
                        Say(pc, 11001258, 131, "通常コンプリート出ました～！$R;" +
                            "$Pおめでとうございます。$R;" +
                            "通常コンプリートの景品と$R;" +
                            "交換しますか？$R;");
                        switch (Select(pc, "どうする？", "", "交換しない", "交換する"))
                        {
                            case 1:
                                break;
                            case 2:
                                普通印章(pc);
                                break;
                        }
                        return;
                    }
                    Say(pc, 11001258, 131, "あぅ～、残念。$R;" +
                        "スタンプが揃ってないみたい……。$R;");
                    break;
                case 3:
                    if (CheckInventory(pc, 10029387, 1))
                    {
                        GiveItem(pc, 10029387, 1);
                        Say(pc, 11001258, 131, "あら、なくしちゃいました？$R;" +
                            "今、新しいスタンプ帳を発行しますね。$R;");
                        ShowEffect(pc, 8013);
                        PlaySound(pc, 3277, false, 100, 50);
                        Say(pc, 11001258, 131, "はい、新しいスタンプ帳です。$R;" +
                            "今までに押したことのあるスタンプは$R;" +
                            "私のほうで押しておきました。$R;" +
                            "$R不能再丟了唷♪$R;" +
                            "……呵呵呵！$R;");
                        return;
                    }
                    PlaySound(pc, 2041, false, 100, 50);
                    Say(pc, 11001200, 131, "荷物がいっぱいで持てなかった！$R;");
                    break;
                case 4:
                    Say(pc, 11001258, 131, "このスタンプ、$R;" +
                        "酒屋のマスターが１人で$R;" +
                        "モンスターに$R;" +
                        "持たせてまわっているんですよ。$R;" +
                        "$R……ウフフ！$R;");
                    break;
            }

        }

        void EXIT(ActorPC pc)
        {
            Say(pc, 11001258, 131, "モンスタースタンプ集めは$R;" +
                "何回でもチャレンジできます。$R;" +
                "$Rあなたの次のチャレンジを$R;" +
                "楽しみにお待ちしていますわ！$R;" +
                "……ウフフ。$R;");
        }

        void 普通印章(ActorPC pc)
        {
            switch (Select(pc, "何色のスタンプと交換する？", "", "交換しない", "次のページへ", "赤いプルルスタンプ", "紫の平原のスタンプ", "青い海岸のスタンプ", "水色の荒野のスタンプ", "緑の大陸ダンジョンのスタンプ"))
            {
                case 1:
                    break;
                case 2:
                    switch (Select(pc, "何色のスタンプと交換する？", "", "交換しない", "もとのページへ", "黄の雪国のスタンプ", "橙の廃炭鉱ダンジョンのスタンプ", "茶のノーザンダンジョンのスタンプ", "白のアイアンサウスのスタンプ", "灰のサウスダンジョンのスタンプ"))
                    {
                        case 1:
                            break;
                        case 2:
                            普通印章(pc);
                            break;
                        case 3:
                            if (CheckStampGenre(pc, StampGenre.Snow))
                            {
                                Say(pc, 11001258, 131, "スタンプ帳に記載された$R;" +
                                    "黄の雪国のスタンプは$R;" +
                                    "消えてしまいますがよろしいですか？$R;");
                                switch (Select(pc, "どうする？", "", "交換しない", "交換する"))
                                {
                                    case 1:
                                        break;
                                    case 2:
                                        Say(pc, 11001258, 131, "こちらが通常コンプリートの$R;" +
                                            "景品になります。$R;" +
                                            "$Rどうぞ、お受け取りください。$R;");
                                        PlaySound(pc, 4006, false, 100, 50);
                                        Say(pc, 0, 131, "通常コンプリートの景品を$R;" +
                                            "手に入れた！$R;");
                                        GiveRandomTreasure(pc, "STAMP_B");
                                        ClearStampGenre(pc, StampGenre.Snow);
                                        EXIT(pc);
                                        break;
                                }
                                return;
                            }
                            Say(pc, 11001258, 131, "あれれ？$R;" +
                                "その色のスタンプは$R;" +
                                "揃っていないみたいですよ。$R;");
                            普通印章(pc);
                            break;
                        case 4:
                            if (CheckStampGenre(pc, StampGenre.Colliery))
                            {
                                Say(pc, 11001258, 131, "スタンプ帳に記載された$R;" +
                                    "橙の廃炭鉱ダンジョンのスタンプは$R;" +
                                    "消えてしまいますがよろしいですか？$R;");
                                switch (Select(pc, "どうする？", "", "交換しない", "交換する"))
                                {
                                    case 1:
                                        break;
                                    case 2:
                                        Say(pc, 11001258, 131, "こちらが通常コンプリートの$R;" +
                                            "景品になります。$R;" +
                                            "$Rどうぞ、お受け取りください。$R;");
                                        PlaySound(pc, 4006, false, 100, 50);
                                        Say(pc, 0, 131, "通常コンプリートの景品を$R;" +
                                            "手に入れた！$R;");
                                        GiveRandomTreasure(pc, "STAMP_B");
                                        ClearStampGenre(pc, StampGenre.Colliery);
                                        EXIT(pc);
                                        break;
                                }
                                return;
                            }
                            Say(pc, 11001258, 131, "あれれ？$R;" +
                                "その色のスタンプは$R;" +
                                "揃っていないみたいですよ。$R;");
                            普通印章(pc);
                            break;
                        case 5:
                            if (CheckStampGenre(pc, StampGenre.Northan))
                            {
                                Say(pc, 11001258, 131, "スタンプ帳に記載された$R;" +
                                    "茶のノーザンダンジョンのスタンプは$R;" +
                                    "消えてしまいますがよろしいですか？$R;");
                                switch (Select(pc, "どうする？", "", "交換しない", "交換する"))
                                {
                                    case 1:
                                        break;
                                    case 2:
                                        Say(pc, 11001258, 131, "こちらが通常コンプリートの$R;" +
                                            "景品になります。$R;" +
                                            "$Rどうぞ、お受け取りください。$R;");
                                        PlaySound(pc, 4006, false, 100, 50);
                                        Say(pc, 0, 131, "通常コンプリートの景品を$R;" +
                                            "手に入れた！$R;");
                                        GiveRandomTreasure(pc, "STAMP_B");
                                        ClearStampGenre(pc, StampGenre.Northan);
                                        EXIT(pc);
                                        break;
                                }
                                return;
                            }
                            Say(pc, 11001258, 131, "あれれ？$R;" +
                                "その色のスタンプは$R;" +
                                "揃っていないみたいですよ。$R;");
                            普通印章(pc);
                            break;
                        case 6:
                            if (CheckStampGenre(pc, StampGenre.IronSouth))
                            {
                                Say(pc, 11001258, 131, "スタンプ帳に記載された$R;" +
                                    "白のアイアンサウスのスタンプは$R;" +
                                    "消えてしまいますがよろしいですか？$R;");
                                switch (Select(pc, "どうする？", "", "交換しない", "交換する"))
                                {
                                    case 1:
                                        break;
                                    case 2:
                                        Say(pc, 11001258, 131, "こちらが通常コンプリートの$R;" +
                                            "景品になります。$R;" +
                                            "$Rどうぞ、お受け取りください。$R;");
                                        PlaySound(pc, 4006, false, 100, 50);
                                        Say(pc, 0, 131, "通常コンプリートの景品を$R;" +
                                            "手に入れた！$R;");
                                        GiveRandomTreasure(pc, "STAMP_B");
                                        ClearStampGenre(pc, StampGenre.IronSouth);
                                        EXIT(pc);
                                        break;
                                }
                                return;
                            }
                            Say(pc, 11001258, 131, "あれれ？$R;" +
                                "その色のスタンプは$R;" +
                                "揃っていないみたいですよ。$R;");
                            普通印章(pc);
                            break;
                        case 7:
                            if (CheckStampGenre(pc, StampGenre.SouthDungeon))
                            {
                                Say(pc, 11001258, 131, "スタンプ帳に記載された$R;" +
                                    "灰のサウスダンジョンのスタンプは$R;" +
                                    "消えてしまいますがよろしいですか？$R;");
                                switch (Select(pc, "どうする？", "", "交換しない", "交換する"))
                                {
                                    case 1:
                                        break;
                                    case 2:
                                        Say(pc, 11001258, 131, "こちらが通常コンプリートの$R;" +
                                            "景品になります。$R;" +
                                            "$Rどうぞ、お受け取りください。$R;");
                                        PlaySound(pc, 4006, false, 100, 50);
                                        Say(pc, 0, 131, "通常コンプリートの景品を$R;" +
                                            "手に入れた！$R;");
                                        GiveRandomTreasure(pc, "STAMP_B");
                                        ClearStampGenre(pc, StampGenre.SouthDungeon);
                                        EXIT(pc);
                                        break;
                                }
                                return;
                            }
                            Say(pc, 11001258, 131, "あれれ？$R;" +
                                "その色のスタンプは$R;" +
                                "揃っていないみたいですよ。$R;");
                            普通印章(pc);
                            break;
                    }
                    break;
                case 3:
                    if (CheckStampGenre(pc, StampGenre.Pururu))
                    {
                        Say(pc, 11001258, 131, "スタンプ帳に記載された$R;" +
                            "赤いプルルスタンプは$R;" +
                            "消えてしまいますがよろしいですか？$R;");
                        switch (Select(pc, "どうする？", "", "交換しない", "交換する"))
                        {
                            case 1:
                                break;
                            case 2:
                                Say(pc, 11001258, 131, "こちらが通常コンプリートの$R;" +
                                    "景品になります。$R;" +
                                    "$Rどうぞ、お受け取りください。$R;");
                                PlaySound(pc, 4006, false, 100, 50);
                                Say(pc, 0, 131, "通常コンプリートの景品を$R;" +
                                    "手に入れた！$R;");
                                GiveRandomTreasure(pc, "STAMP_B");
                                ClearStampGenre(pc, StampGenre.Pururu);
                                EXIT(pc);
                                break;
                        }
                        return;
                    }
                    Say(pc, 11001258, 131, "あれれ？$R;" +
                        "その色のスタンプは$R;" +
                        "揃っていないみたいですよ。$R;");
                    普通印章(pc);
                    break;
                case 4:
                    if (CheckStampGenre(pc, StampGenre.Field))
                    {
                        Say(pc, 11001258, 131, "スタンプ帳に記載された$R;" +
                            "紫の平原のスタンプは$R;" +
                            "消えてしまいますがよろしいですか？$R;");
                        switch (Select(pc, "どうする？", "", "交換しない", "交換する"))
                        {
                            case 1:
                                break;
                            case 2:
                                Say(pc, 11001258, 131, "こちらが通常コンプリートの$R;" +
                                    "景品になります。$R;" +
                                    "$Rどうぞ、お受け取りください。$R;");
                                PlaySound(pc, 4006, false, 100, 50);
                                Say(pc, 0, 131, "通常コンプリートの景品を$R;" +
                                    "手に入れた！$R;");
                                GiveRandomTreasure(pc, "STAMP_B");
                                ClearStampGenre(pc, StampGenre.Field);
                                EXIT(pc);
                                break;
                        }
                        return;
                    }
                    Say(pc, 11001258, 131, "あれれ？$R;" +
                        "その色のスタンプは$R;" +
                        "揃っていないみたいですよ。$R;");
                    普通印章(pc);
                    break;
                case 5:
                    if (CheckStampGenre(pc, StampGenre.Coast))
                    {
                        Say(pc, 11001258, 131, "スタンプ帳に記載された$R;" +
                            "青い海岸のスタンプは$R;" +
                            "消えてしまいますがよろしいですか？$R;");
                        switch (Select(pc, "どうする？", "", "交換しない", "交換する"))
                        {
                            case 1:
                                break;
                            case 2:
                                Say(pc, 11001258, 131, "こちらが通常コンプリートの$R;" +
                                    "景品になります。$R;" +
                                    "$Rどうぞ、お受け取りください。$R;");
                                PlaySound(pc, 4006, false, 100, 50);
                                Say(pc, 0, 131, "通常コンプリートの景品を$R;" +
                                    "手に入れた！$R;");
                                GiveRandomTreasure(pc, "STAMP_B");
                                ClearStampGenre(pc, StampGenre.Coast);
                                EXIT(pc);
                                break;
                        }
                        return;
                    }
                    Say(pc, 11001258, 131, "あれれ？$R;" +
                        "その色のスタンプは$R;" +
                        "揃っていないみたいですよ。$R;");
                    普通印章(pc);
                    break;
                case 6:
                    if (CheckStampGenre(pc, StampGenre.Wild))
                    {
                        Say(pc, 11001258, 131, "スタンプ帳に記載された$R;" +
                            "水色の荒野のスタンプは$R;" +
                            "消えてしまいますがよろしいですか？$R;");
                        switch (Select(pc, "どうする？", "", "交換しない", "交換する"))
                        {
                            case 1:
                                break;
                            case 2:
                                Say(pc, 11001258, 131, "こちらが通常コンプリートの$R;" +
                                    "景品になります。$R;" +
                                    "$Rどうぞ、お受け取りください。$R;");
                                PlaySound(pc, 4006, false, 100, 50);
                                Say(pc, 0, 131, "通常コンプリートの景品を$R;" +
                                    "手に入れた！$R;");
                                GiveRandomTreasure(pc, "STAMP_B");
                                ClearStampGenre(pc, StampGenre.Wild);
                                EXIT(pc);
                                break;
                        }
                        return;
                    }
                    Say(pc, 11001258, 131, "あれれ？$R;" +
                        "その色のスタンプは$R;" +
                        "揃っていないみたいですよ。$R;");
                    普通印章(pc);
                    break;
                case 7:
                    if (CheckStampGenre(pc, StampGenre.Cave))
                    {
                        Say(pc, 11001258, 131, "スタンプ帳に記載された$R;" +
                            "緑の大陸ダンジョンのスタンプは$R;" +
                            "消えてしまいますがよろしいですか？$R;");
                        switch (Select(pc, "どうする？", "", "交換しない", "交換する"))
                        {
                            case 1:
                                break;
                            case 2:
                                Say(pc, 11001258, 131, "こちらが通常コンプリートの$R;" +
                                    "景品になります。$R;" +
                                    "$Rどうぞ、お受け取りください。$R;");
                                PlaySound(pc, 4006, false, 100, 50);
                                Say(pc, 0, 131, "通常コンプリートの景品を$R;" +
                                    "手に入れた！$R;");
                                GiveRandomTreasure(pc, "STAMP_B");
                                ClearStampGenre(pc, StampGenre.Cave);
                                EXIT(pc);
                                break;
                        }
                        return;
                    }
                    Say(pc, 11001258, 131, "あれれ？$R;" +
                        "その色のスタンプは$R;" +
                        "揃っていないみたいですよ。$R;");
                    普通印章(pc);
                    break;
            }
        }

        void 一百種(ActorPC pc)
        {
            Say(pc, 11001258, 131, "スタンプ帳に記載された$R;" +
                "スペシャルスタンプ以外のスタンプは$R;" +
                "消えてしまいますがよろしいですか？$R;");
            switch (Select(pc, "どうする？", "", "交換しない", "交換する"))
            {
                case 1:
                    break;
                case 2:
                    int sel = 0;
                    do
                    {
                        switch (Select(pc, "どんなアイテムが欲しいですか？", "", "やっぱりやめる", "武器系の景品", "防具系の景品", "家具系の景品", "一か八かのチャレンジをする"))
                        {
                            case 1:
                                return;
                            case 2:
                                Say(pc, 11001258, 131, "こちらが100個コンプリートの$R;" +
                                    "景品になります。$R;" +
                                    "$Rどうぞ、お受け取りください。$R;");
                                GiveRandomTreasure(pc, "STAMP_C");
                                break;
                            case 3:
                                Say(pc, 11001258, 131, "こちらが100個コンプリートの$R;" +
                                    "景品になります。$R;" +
                                    "$Rどうぞ、お受け取りください。$R;");
                                GiveRandomTreasure(pc, "STAMP_D");
                                break;
                            case 4:
                                Say(pc, 11001258, 131, "こちらが100個コンプリートの$R;" +
                                    "景品になります。$R;" +
                                    "$Rどうぞ、お受け取りください。$R;");
                                GiveRandomTreasure(pc, "STAMP_E");
                                break;
                            case 5:
                                Say(pc, 11001258, 131, "一か八かのチャレンジは$R;" +
                                    "当たれば天国、外れれば地獄。$R;" +
                                    "$R非常にギャンブル性の高い$R;" +
                                    "ラインナップとなっております。$R;" +
                                    "$R覚悟の上で、お選びください。$R;");
                                switch (Select(pc, "どうする？", "", "景品を選びなおす", "一か八かのチャレンジをする"))
                                {
                                    case 1:
                                        break;
                                    case 2:
                                        Say(pc, 11001258, 131, "こちらが100個コンプリートの$R;" +
                                            "景品になります。$R;" +
                                            "$Rどうぞ、お受け取りください。$R;");
                                        GiveRandomTreasure(pc, "STAMP_F");
                                        PlaySound(pc, 4006, false, 100, 50);
                                        Say(pc, 0, 131, "100個コンプリートの景品を$R;" +
                                            "手に入れた！$R;");
                                        ClearStampGenre(pc, StampGenre.Pururu);
                                        ClearStampGenre(pc, StampGenre.Field);
                                        ClearStampGenre(pc, StampGenre.Coast);
                                        ClearStampGenre(pc, StampGenre.Wild);
                                        ClearStampGenre(pc, StampGenre.Cave);
                                        ClearStampGenre(pc, StampGenre.Snow);
                                        ClearStampGenre(pc, StampGenre.Colliery);
                                        ClearStampGenre(pc, StampGenre.Northan);
                                        ClearStampGenre(pc, StampGenre.IronSouth);
                                        ClearStampGenre(pc, StampGenre.SouthDungeon);
                                        EXIT(pc);
                                        return;
                                }
                                break;
                        }
                    } while (sel == 4);
                    PlaySound(pc, 4006, false, 100, 50);
                    Say(pc, 0, 131, "100個コンプリートの景品を$R;" +
                        "手に入れた！$R;");
                    ClearStampGenre(pc, StampGenre.Pururu);
                    ClearStampGenre(pc, StampGenre.Field);
                    ClearStampGenre(pc, StampGenre.Coast);
                    ClearStampGenre(pc, StampGenre.Wild);
                    ClearStampGenre(pc, StampGenre.Cave);
                    ClearStampGenre(pc, StampGenre.Snow);
                    ClearStampGenre(pc, StampGenre.Colliery);
                    ClearStampGenre(pc, StampGenre.Northan);
                    ClearStampGenre(pc, StampGenre.IronSouth);
                    ClearStampGenre(pc, StampGenre.SouthDungeon);
                    EXIT(pc);
                    break;
            }
        }
    }
}
