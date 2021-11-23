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
                        Say(pc, 131, "正在找『冰灵之花』是吗?$R;" +
                            "$R不是什么贵重物品，给您吧$R;" +
                            "$P只是…人用手触摸$R;" +
                            "就会化掉的…$R;" +
                            "$R变身活动木偶后$R;" +
                            "再跟我说吧$R;");
                        return;
                    }
                    GiveItem(pc, 50035651, 1);
                    Say(pc, 131, "正在找『冰灵之花』是吗?$R;" +
                        "$R不是什么贵重物品，给您吧$R;" +
                        "$P请收下吧$R;");
                    return;
                }
                Say(pc, 131, "正在找『冰灵之花』是吗?$R;" +
                    "$R给您吧$R;" +
                    "不是什么贵重物品$R;" +
                    "$P只是行李好像太多了$R;" +
                    "$R整理行李后，再来吧$R;");
                return;
            }

            if (mask.Test(AYSD.給予奇怪的水晶) && !mask.Test(AYSD.回答水晶的狀況))//_2A40 && !_2A41)
            {
                Say(pc, 131, "欢迎到我们的岛$R;" +
                    "您给我们的新生命，正茁壮成长$R;");
                mask.SetValue(AYSD.回答水晶的狀況, true);
                //_2A41 = true;
            }

            if (!mask.Test(AYSD.第一次對話))//_2A39)
            {
                Say(pc, 131, "我是冰精灵一族的族长$R;" +
                    "这岛是我们冰精灵的故乡…$R;");
                mask.SetValue(AYSD.第一次對話, true);
                //_2A39 = true;
            }

            if (CountItem(pc, 10011000) == 0)
            {
                Say(pc, 131, "我们跟诺森王国签定协议$R;" +
                    "要保卫这座岛的独立自主呀$R;");
                return;
            }

            Say(pc, 131, "您有『不可思议的水晶』呀$R;" +
                "$P您是经过回廊来到这里，$R要给我们赋予新的生命的吧？$R;");

            製作活動木偶愛伊斯(pc);
        }

        void 製作活動木偶愛伊斯(ActorPC pc)
        {
            BitMask<AYSD> mask = pc.CMask["AYSD"];

            switch (Select(pc, "给他『不可思议的水晶』吗？", "", "给他", "不给他", "您为什么需要『不可思议的水晶』？"))
            {
                case 1:
                    if (CheckInventory(pc, 10019300, 1))
                    {
                        TakeItem(pc, 10011000, 1);
                        Say(pc, 131, "给了『不可思议的水晶』$R;");
                        Say(pc, 131, "谢谢$R;" +
                            "$R现在我们种族$R会诞生一条新生命吧$R;" +
                            "$P请接受这个$R;" +
                            "$R这是附有我们冰精灵力量的水晶。$R把这水晶握在手中，聚精会神$R就会得到新的力量$R;");
                        GiveItem(pc, 10019300, 1);
                        Say(pc, 131, "得到了『活动木偶·冰精灵』$R;");
                        Say(pc, 131, "再见$R;");
                        mask.SetValue(AYSD.給予奇怪的水晶, true);
                        mask.SetValue(AYSD.回答水晶的狀況, true);
                        //_2A40 = true;
                        //_2A41 = true;
                        return;
                    }
                    Say(pc, 131, "行李太多了$R;" +
                        "$R减少行李后，再过来吧$R;");
                    break;
                case 2:
                    break;
                case 3:
                    Say(pc, 131, "我们冰精灵的灵魂，$R听说是从充满生命力的大地$R出现『不可思议的水晶』诞生的$R;" +
                        "$P在这极寒的土地$R没有『奇怪的水晶』呀$R;" +
                        "$R所以为了保存种族$R只好借助冒险者的力量了$R;");

                    製作活動木偶愛伊斯(pc);
                    break;
            }
        }
    }
}