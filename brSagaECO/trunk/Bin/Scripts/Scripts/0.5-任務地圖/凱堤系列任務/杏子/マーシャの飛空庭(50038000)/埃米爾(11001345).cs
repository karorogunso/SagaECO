using System;
using System.Collections.Generic;
using System.Text;
using SagaLib;
using SagaDB.Actor;
using SagaDB.Item;
using SagaMap.Scripting;
using SagaScript.Chinese.Enums;

namespace SagaScript.M50038000
{
    public class S11001345 : Event
    {
        public S11001345()
        {
            this.EventID = 11001345;
        }

        public override void OnEvent(ActorPC pc)
        {
            BitMask<Neko_06> Neko_06_amask = pc.AMask["Neko_06"];
            BitMask<Neko_06> Neko_06_cmask = pc.CMask["Neko_06"];

            if (Neko_06_amask.Test(Neko_06.杏子任务开始) &&
                !Neko_06_amask.Test(Neko_06.获得杏子) &&
                Neko_06_cmask.Test(Neko_06.與埃米爾對話) &&
                Neko_06_cmask.Test(Neko_06.與瑪莎對話) &&
                !Neko_06_cmask.Test(Neko_06.尋找阿伊斯))
            {
                Say(pc, 111, "あ、$Rノーザンシティーの上空に着いたよ。$R;", "エミル");
                if (Select(pc, "どうする？", "", "飛空庭を降りる", "まだ降りない") == 1)
                {
                    Neko_06_cmask.SetValue(Neko_06.尋找阿伊斯, true);
                    Say(pc, 111, "アイシー島は空港から北西へ行った$R「永遠への北限」の西岸にあるよ。$R;" +
                    "$Rじゃあ、気をつけてね！$R;", "エミル");
                    Warp(pc, 10050000, 74, 141);
                }
            }
            else if (Neko_06_amask.Test(Neko_06.杏子任务开始) &&
                    !Neko_06_amask.Test(Neko_06.获得杏子) &&
                    Neko_06_cmask.Test(Neko_06.與埃米爾對話) &&
                    !Neko_06_cmask.Test(Neko_06.尋找阿伊斯))
                {
                    Say(pc, 11001345, 111, "……。$R;", "エミル");

                    Say(pc, 0, 111, "（あ…、考えこんじゃった。）$R;", " ");
                }

                else if (Neko_06_amask.Test(Neko_06.杏子任务开始) &&
                !Neko_06_amask.Test(Neko_06.获得杏子) &&
                Neko_06_cmask.Test(Neko_06.與提多對話) &&
                !Neko_06_cmask.Test(Neko_06.與埃米爾對話) &&
                !Neko_06_cmask.Test(Neko_06.尋找阿伊斯))
            {
                Say(pc, 11001345, 111, "あれ！？$Rネコマタがいる！？$R;" +
                "$Rどうしてこの飛空庭に？$Rご主人とはぐれちゃったのかい？$R;" +
                "$Pかわいいネコマタだなー。$R女の子なのかな…$R;", "エミル");

                Say(pc, 0, 111, "ボクは男の子だよ！！$R;", "ネコマタ（杏）");

                Say(pc, 11001345, 111, "うわっ！人の言葉をしゃべった！？$R;" +
                "$R…新型のマリオネットかな？$R;", "エミル");

                Say(pc, 0, 111, "エミルにこれまでのいきさつを話した。$R;", " ");

                Say(pc, 11001345, 111, pc.Name + "だったんだ。$R;" +
                "$Rへーえ、$Rそれでネコマタの姿に、$R;" +
                "$R不思議なこともあるもんだね。$R;" +
                "$P…うん、$Rこの飛空庭はゴルドーさんの依頼で$Rノーザンに仕入れに行く途中だよ。$R;" +
                "$Rノーザンシティ前の空港までだね？$R送ってあげるよ。$R;", "エミル");

                Say(pc, 0, 111, "ありがと～えみる♪$R;", "ネコマタ（杏）");

                Say(pc, 0, 111, "…あれ？そういえばマーシャは？$R;", " ");

                Say(pc, 11001345, 111, "ちょっと１人になりたいって$R家の中にいるよ。$R;" +
                "$R…マーシャ、最近少し元気がないんだ。$R;", "エミル");

                Say(pc, 0, 111, "……。$R;", " ");

                Say(pc, 0, 111, "えみるのせいなんだ！$R;", "ネコマタ（杏）");

                Say(pc, 0, 131, "うわあああぁっ！！声に出して！$Rしかも人の言葉で！！$R;" +
                "$Rてか、$R考えてたことをしゃべらないで！$R;", " ");

                Say(pc, 0, 111, "だって聞こえるんだもん。$R;", "ネコマタ（杏）");

                Say(pc, 11001345, 111, "え！？…僕…僕のせい？$R;" +
                "$R……。$R;", "エミル");

                Say(pc, 0, 111, "いや！いまのっ！今の無し！！$R;" +
                "$Rあんず～～！！$R;", " ");

                Say(pc, 0, 111, "ぶーー。$R;", "ネコマタ（杏）");
                Neko_06_cmask.SetValue(Neko_06.與埃米爾對話, true);
            }
        }
    }
}