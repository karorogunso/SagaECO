using System;
using System.Collections.Generic;
using System.Text;
using SagaLib;
using SagaDB.Actor;
using SagaDB.Item;
using SagaMap.Scripting;
using SagaScript.Chinese.Enums;

namespace SagaScript.M50039000
{
    public class S11001347 : Event
    {
        public S11001347()
        {
            this.EventID = 11001347;
        }

        public override void OnEvent(ActorPC pc)
        {
            BitMask<Neko_06> Neko_06_amask = pc.AMask["Neko_06"];
            BitMask<Neko_06> Neko_06_cmask = pc.CMask["Neko_06"];

            if (Neko_06_amask.Test(Neko_06.杏子任务开始) &&
                !Neko_06_amask.Test(Neko_06.获得杏子) &&
                Neko_06_cmask.Test(Neko_06.與瑪莎對話))
            {
                Say(pc, 135, "……。$R;", "マーシャ");
            }

            else if (Neko_06_amask.Test(Neko_06.杏子任务开始) &&
                !Neko_06_amask.Test(Neko_06.获得杏子) &&
                Neko_06_cmask.Test(Neko_06.與提多對話) &&
                !Neko_06_cmask.Test(Neko_06.與瑪莎對話))
            {
                Say(pc, 135, "…あれ？$R;" +
                "$Rまあ、ネコマタちゃん。$R;" +
                "$Rどうしたの？ご主人様はまだお外？$R;", "マーシャ");

                Say(pc, 0, 111, "（…ネコマタだと思われてる…。）$R;", " ");

                Say(pc, 135, "かわいい♪$R;" +
                "$Rネコちゃん、近くにいらっしゃい。$R;" +
                "$P…エミルがお友達を乗せたのかしら？$R;" +
                "$R…また女の子なのかな…$R;" +
                "$P……。$R;" +
                "$P…ネコちゃん。$R;" +
                "$Rお姉さんね、$Rエミルと一緒にいるときが大好きなのに$R;" +
                "$Rエミルと一緒にいるときの自分が…$R;" +
                "$R…大嫌い。$R;", "マーシャ");

                Say(pc, 0, 111, "ムギュッ！$R;", " ");

                Say(pc, 0, 111, "（抱っこされた！？）$R;" +
                "$P……………………$R;" +
                "……………………$R;" +
                "$P……………………$R;" +
                "うわぁあぁ………$R;" +
                "$P……………………$R;" +
                "う～～む…………$R;" +
                "$P…………はっ！$R;" +
                "$R（杏！しゃ、しゃべっちゃダメ！$R;" +
                "あん……ず…？）$R;", " ");

                Say(pc, 0, 111, "（……スウ……）$R;", "ネコマタ（杏）");

                Say(pc, 0, 111, "（…杏…ね、寝てる…。）$R;", " ");
                Neko_06_cmask.SetValue(Neko_06.與瑪莎對話, true);
                return;
            }
        }
    }
}