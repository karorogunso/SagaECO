using System;
using System.Collections.Generic;
using System.Text;
using SagaLib;
using SagaDB.Actor;
using SagaDB.Item;
using SagaMap.Scripting;
using SagaScript.Chinese.Enums;

namespace SagaScript.M50037000
{
    public class S11001344 : Event
    {
        public S11001344()
        {
            this.EventID = 11001344;
        }

        public override void OnEvent(ActorPC pc)
        {
            BitMask<Neko_06> Neko_06_amask = pc.AMask["Neko_06"];
            BitMask<Neko_06> Neko_06_cmask = pc.CMask["Neko_06"];
            
            if (Neko_06_amask.Test(Neko_06.杏子任务开始) &&
                !Neko_06_amask.Test(Neko_06.获得杏子) &&
                Neko_06_cmask.Test(Neko_06.與提多對話) &&
                !Neko_06_cmask.Test(Neko_06.尋找阿伊斯))
            {
                Say(pc, 111, "……。$R;", "ティタ");

                Say(pc, 0, 131, "ティタは$Rほどなく復活するだろう…。$R;" +
                "$P冒険者のみなが『こころのかけら』を$R集めてくれたおかげだ。$R;" +
                "$Rありがとう、感謝しているよ。$R;", "タイタス");

                Say(pc, 0, 131, "（……あれ？$R;" +
                "$R…あんまり嬉しそうじゃない…？）$R;" +
                "$P（あー、タイタスさん心配性っぽいし$R;" +
                "$Rティタさんが復活するのは$R嬉しいけど複雑な気分なんだ…。）$R;" +
                "$P（……。$Rタイタスさんって、やっぱあれなのかな…$R;" +
                "$Rシス…）$R;", " ");

                Say(pc, 0, 131, "たいたすってシスコンなんだ！$R;", "ネコマタ（杏）");

                Say(pc, 0, 131, "うわぁっ！！声に出して！$R;" +
                "$Rてか、$Rなんでそんな言葉知ってるんだよ！$R;", " ");

                Say(pc, 0, 131, "お姉ちゃんたちがね、$Rいつも言ってたの。$R;" +
                "$R杏はシスコンなんだからねって。$R;" +
                "$Pたいたすはシスコン？$Rたいたすもシスコンなの？$Rたいたすもシスコンなんだ～～。$R;" +
                "$R…シスコンって何？$R;", "ネコマタ（杏）");

                Say(pc, 0, 131, "だから声に出すなって！$R;" +
                "$R…あいつら…$R弟に変な言葉おぼえさせて…。$R;" +
                "$P（タイタスさん、怒ってるかな…？）$Rチラッ$R;", " ");

                Say(pc, 11001343, 131, "……。$R;" +
                "$R…早く行きたまえ。$R;", "タイタス");

                Say(pc, 0, 131, "は、はい…。$R;", " ");
                if (Select(pc, "どうする？", "", "転送を頼む", "まだ頼まない") == 1)
                {
                    ShowEffect(pc, 4023);
                    Wait(pc, 1980);
                    pc.CInt["Neko_06_Map_04"] = CreateMapInstance(50038000, 10023000, 135, 64);
                    Warp(pc, (uint)pc.CInt["Neko_06_Map_04"], 7, 10);
                }
            }
            else if (Neko_06_amask.Test(Neko_06.杏子任务开始) &&
                !Neko_06_amask.Test(Neko_06.获得杏子) &&
                Neko_06_cmask.Test(Neko_06.詢問塔尼亞代表) &&
                !Neko_06_cmask.Test(Neko_06.與提多對話))
            {
                Say(pc, 0, 131, "……？$R;" +
                "$Rこのお姉ちゃん、からだがとうめい…？$R;", "(杏）");
            }
        }
    }
}