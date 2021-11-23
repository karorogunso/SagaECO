using System;
using System.Collections.Generic;
using System.Text;
using SagaLib;
using SagaDB.Actor;
using SagaMap.Scripting;
using SagaScript.Chinese.Enums;
namespace SagaScript.M10051100
{
    public class S11000199 : Event
    {
        public S11000199()
        {
            this.EventID = 11000199;
        }

        public override void OnEvent(ActorPC pc)
        {
            BitMask<Job2X_11> Job2X_11_mask = pc.CMask["Job2X_11"];
            BitMask<AYSD> mask = pc.CMask["AYSD"];
            BitMask<Neko_06> Neko_06_amask = pc.AMask["Neko_06"];
            BitMask<Neko_06> Neko_06_cmask = pc.CMask["Neko_06"];

            if (Neko_06_amask.Test(Neko_06.杏子任务开始) &&
                !Neko_06_amask.Test(Neko_06.获得杏子) &&
                Neko_06_cmask.Test(Neko_06.尋找阿伊斯) &&
                !Neko_06_cmask.Test(Neko_06.獲知恢復的方法) &&
                CountItem(pc, 10011001) > 0)
            {

                Say(pc, 131, "まあ、$R小さなかわいいお客様！$R;" +
                "$Rあら？$R……あなたは１つの身体に２つの生命を$R宿していらっしゃるのね？$R;", "アイシー族長");

                Say(pc, 0, 131, "アイシー族長に$Rこれまでのいきさつを話した。$R;", " ");

                Say(pc, 131, "…はい、そのタイタニアの方の$Rおっしゃる通りですわ。$R;" +
                "$P私たちは冒険者が届けてくださった$R『不思議な水晶』に$R自分たちの生命の一部を投射して$R一族を増やします。$R;" +
                "$Pその時に特殊なこしらえをした$Rプリズムを使います。$R;" +
                "$Rただ…$Rあなたがたの魂の分離ができるかどうか$R…試してみないとわかりませんわ。$R;", "アイシー族長");
                PlaySound(pc, 2040, false, 100, 50);

                Say(pc, 0, 131, "『タイタスの水晶』を失った！$R;", " ");

                TakeItem(pc, 10011001, 1);

                Say(pc, 0, 131, "『アイシーのプリズム』を手に入れた！$R;", " ");

                GiveItem(pc, 10011002, 1);

                Say(pc, 131, "お持ちになった水晶を$Rプリズム状にこしらえましたわ。$R;", "アイシー族長");

                Say(pc, 0, 131, "わぁ～い、ありがとう～！$R;" +
                "ねぇ♪ねぇ♪$R;" +
                "KOU$R;" +
                "$Rじゃあ、じゃあ、今から…$R;", "ネコマタ（杏）");

                Say(pc, 131, "まあっ！待って！$R;" +
                "$R今ここで分離をしてしまうと$Rあなたがたの魂は$Rどこかへ吹き飛んでしまいますわ。$R;" +
                "$P必ずあなたの抜け殻の近くで行って。$R;" +
                "$Rそれにもうひとつ重要なことが…$R;", "アイシー族長");

                Say(pc, 0, 131, "？？？$R;", "ネコマタ（杏）");

                Say(pc, 131, "先ほど心を失ったネコマタたちのお話を$Rされてましたでしょう？$R;" +
                "$R分離をする前に$R必ずその心も一緒に集めてください。$R;", "アイシー族長");

                Say(pc, 0, 131, "お姉ちゃんたちのココロ？$R;", "ネコマタ（杏）");

                Say(pc, 131, "ええ、$R;" +
                "この人の魂が肉体に戻るときに$Rネコマタたちの心も近くにあれば$R;" +
                "$Rあなたのお姉さまたちも$R元に戻ることができるはずですわ。$R;", "アイシー族長");

                Say(pc, 0, 131, "でも、どこにいるんだろう…。$R;", " ");

                Say(pc, 131, "……。$R;" +
                "$Rあなたとの$R思い出の場所……思い出の人……$R;" +
                "$Pネコマタたちが$R今でも心の拠りどころにしている場所に$Rきっと、きっといますわ。$R;" +
                "$R思い出して。$R;", "アイシー族長");

                Say(pc, 0, 131, "……うん。$R;" +
                "$R行こう、杏！$Rお姉ちゃんたちをさがしに。$R;", " ");

                Say(pc, 0, 131, "うん！$R;", "ネコマタ（杏）");
                Neko_06_cmask.SetValue(Neko_06.獲知恢復的方法, true);
                return;
            }

            if (Job2X_11_mask.Test(Job2X_11.轉職開始) && CountItem(pc, 50035651) == 0 && pc.Job == PC_JOB.RANGER)
            {
                if (CheckInventory(pc, 50035651, 1))
                {
                    if (pc.Marionette == null)
                    {
                        Say(pc, 131, "正在找『冰靈之花』是嗎?$R;" +
                            "$R不是什麼貴重物品，給您吧$R;" +
                            "$P只是…人用手觸摸$R;" +
                            "就會化掉的…$R;" +
                            "$R變身活動木偶後$R;" +
                            "再跟我說吧$R;");
                        return;
                    }
                    GiveItem(pc, 50035651, 1);
                    Say(pc, 131, "正在找『冰靈之花』是嗎?$R;" +
                        "$R不是什麼貴重物品，給您吧$R;" +
                        "$P請收下吧$R;");
                    return;
                }
                Say(pc, 131, "正在找『冰靈之花』是嗎?$R;" +
                    "$R給您吧$R;" +
                    "不是什麼貴重物品$R;" +
                    "$P只是行李好像太多了$R;" +
                    "$R整理行李後，再來吧$R;");
                return;
            }

            if (mask.Test(AYSD.給予奇怪的水晶) && !mask.Test(AYSD.回答水晶的狀況))//_2A40 && !_2A41)
            {
                Say(pc, 131, "歡迎到我們的島$R;" +
                    "您給我們的新生命，正茁壯成長唷$R;");
                mask.SetValue(AYSD.回答水晶的狀況, true);
                //_2A41 = true;
            }

            if (!mask.Test(AYSD.第一次對話))//_2A39)
            {
                Say(pc, 131, "我是愛伊斯族的族長$R;" +
                    "這島是我們愛伊斯的故鄉…$R;");
                mask.SetValue(AYSD.第一次對話, true);
                //_2A39 = true;
            }

            if (CountItem(pc, 10011000) == 0)
            {
                Say(pc, 131, "我們跟諾頓王國簽定協議$R;" +
                    "要保衛這座島的獨立自主呀$R;");
                return;
            }

            Say(pc, 131, "您有『奇怪的水晶』呀$R;" +
                "$P您是經過回廊來到這裡，$R要給我們賦予新的生命的吧？$R;");

            製作活動木偶愛伊斯(pc);
        }

        void 製作活動木偶愛伊斯(ActorPC pc)
        {
            BitMask<AYSD> mask = pc.CMask["AYSD"];

            switch (Select(pc, "給他『奇怪的水晶』嗎？", "", "給他", "不給他", "您為什麼需要『奇怪的水晶』？"))
            {
                case 1:
                    if (CheckInventory(pc, 10019300, 1))
                    {
                        TakeItem(pc, 10011000, 1);
                        Say(pc, 131, "給了『奇怪的水晶』$R;");
                        Say(pc, 131, "謝謝$R;" +
                            "$R現在我們種族$R會誕生一條新生命吧$R;" +
                            "$P請接受這個$R;" +
                            "$R這是附有我們愛伊斯族力量的水晶。$R把這水晶握在手中，聚精會神$R就會得到新的力量唷$R;");
                        GiveItem(pc, 10019300, 1);
                        Say(pc, 131, "得到了『活動木偶愛伊斯』$R;");
                        Say(pc, 131, "再見$R;");
                        mask.SetValue(AYSD.給予奇怪的水晶, true);
                        mask.SetValue(AYSD.回答水晶的狀況, true);
                        //_2A40 = true;
                        //_2A41 = true;
                        return;
                    }
                    Say(pc, 131, "行李太多了$R;" +
                        "$R減少行李後，再過來吧$R;");
                    break;
                case 2:
                    break;
                case 3:
                    Say(pc, 131, "我們愛伊斯族的靈魂，$R聽說是從充滿生命力的大地$R出現『奇怪的水晶』誕生的$R;" +
                        "$P在這極寒的土地$R沒有『奇怪的水晶』呀$R;" +
                        "$R所以為了保存種族$R只好借助冒險者的力量了$R;");

                    製作活動木偶愛伊斯(pc);
                    break;
            }
        }
    }
}