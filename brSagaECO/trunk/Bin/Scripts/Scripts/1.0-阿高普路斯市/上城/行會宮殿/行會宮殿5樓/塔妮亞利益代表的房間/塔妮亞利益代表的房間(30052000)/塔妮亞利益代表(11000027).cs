using System;
using System.Collections.Generic;
using System.Text;
using SagaLib;
using SagaDB.Actor;
using SagaDB.Item;
using SagaMap.Scripting;
using SagaScript.Chinese.Enums;
//所在地圖:塔妮亞利益代表的房間(30052000) NPC基本信息:塔妮亞利益代表(11000027) X:3 Y:4
namespace SagaScript.M30052000
{
    public class S11000027 : Event
    {
        public S11000027()
        {
            this.EventID = 11000027;
        }

        public override void OnEvent(ActorPC pc)
        {
            BitMask<Neko_06> Neko_06_amask = pc.AMask["Neko_06"];
            BitMask<Neko_06> Neko_06_cmask = pc.CMask["Neko_06"];

            if (Neko_06_amask.Test(Neko_06.杏子任务开始) &&
                !Neko_06_amask.Test(Neko_06.获得杏子) &&
                Neko_06_cmask.Test(Neko_06.詢問裁縫阿姨) &&
                !Neko_06_cmask.Test(Neko_06.詢問塔尼亞代表))
            {
                Say(pc, 131, "ここは我がタイタニアの神聖な部屋。$R;" +
                "出て行きな…$R;" +
                "$P…おや？$R;" +
                "$Rほう、これは珍しい。$Rネコ魂と人の魂が対融合している…。$R;", "タイタニア族利益代表");

                Say(pc, 0, 131, "タイタニア利益代表に$Rこれまでのいきさつを話した。$R;", " ");

                Say(pc, 131, "…なるほど。$R;" +
                "$R魂と身体のはく離状態に関して$Rわが一族にとてもくわしい者がいます。$R;" +
                "$R…いるにはいるのですが…$R;", "タイタニア族利益代表");

                Say(pc, 0, 131, "……？？$R;", " ");

                Say(pc, 131, "彼は…$R;" +
                "$R…妹のことで頭がいっぱいで$R自分の「タイタニアの試練」の探求も$Rほったらかしてホントにもう…$R…ブツブツ$R;", "タイタニア族利益代表");

                Say(pc, 0, 131, "……？？$R;" +
                "$Rあ、あのう～～$R;", " ");

                Say(pc, 131, "はっ、し、失礼。$R;" +
                "$Rえー、オッホン。$Rわが一族の「タイタス」という者を$Rたずねてみると良いでしょう。$R;" +
                "$P彼の研究がおそらくこのエミルの世界で$R一番進んでいると思われます。$R;" +
                "$P彼に会うためには…$R;" +
                "$R…そうですね、この世界に散らばる$R『こころのかけら』を使うと良いですよ。$R;", "タイタニア族利益代表");
                Neko_06_cmask.SetValue(Neko_06.詢問塔尼亞代表, true);
                return;
            }
            Say(pc, 131, "ここは我がタイタニアの神聖な部屋。$R;" +
            "出て行きなさい。$R;", "タイタニア族利益代表");

        }
    }
}
