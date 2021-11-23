using System;
using System.Collections.Generic;
using System.Text;
using SagaLib;
using SagaDB.Actor;
using SagaMap.Scripting;
using SagaScript.Chinese.Enums;
namespace SagaScript.M10065000
{
    public class S11000210 : Event
    {
        public S11000210()
        {
            this.EventID = 11000210;
        }

        public override void OnEvent(ActorPC pc)
        {
            //万圣节部分
            BitMask<wsj> wsj_mask = new BitMask<wsj>(pc.CMask["wsj"]);
            //int selection;
            if (wsj_mask.Test(wsj.对话))
            {
                万圣节(pc);
            }
            int selection;
            //EVT11000210
            //#JUDGE
            //#FLAG ON 0b49
            //#ME.ITEMSLOT_EMPTY > 0
            //#ITEM GET 10033200
            //#TRUE EVT1100021012
            //#FALSE NONE
            //SWITCH START
            //_0b20 = true;
            Say(pc, 131, "很漂亮的街道…$R;");
            Say(pc, 131, "是吧？$R;" +
                "想跟您在這條街上逛唷$R;");
            selection = Select(pc, "少年正在講著自己知道的故事", "", "關於這道橋", "關於城門", "關於光之噴泉", "關於薔微刻印", "關於城", "魔法行會總部", "現在到此為止");
            while (selection != 7)
            {
                switch (selection)
                {
                    case 1:
                        Say(pc, 131, "這座橋叫『愛之橋』唷$R;" +
                            "$R有一個傳說，$R;" +
                            "兩個人手拉手走過$R;" +
                            "就會永遠相愛喔$R;" +
                            "$P周圍環境不錯$R;" +
                            "每到聖誕節或情人節時$R;" +
                            "都聚集很多戀人呢$R;" +
                            "$R我們一會兒也會過那座橋的$R;");
                        break;
                    case 2:
                        Say(pc, 131, "真是個巨大的城門呀$R;" +
                            "$R從古至今一直守護著這城市$R;" +
                            "免受暴風雪和敵人襲擊喔$R;" +
                            "$P那門特別重，$R;" +
                            "以人的力量是打不開的$R;" +
                            "$P只能依靠女王陛下的魔力$R;" +
                            "才能進行開關喔$R;" +
                            "$R一定需要很龐大的魔力吧？$R;");
                        break;
                    case 3:
                        Say(pc, 131, "說的是城前閃爍的噴泉吧$R;" +
                            "$R噴出來的不是水，而是光的粒子唷$R;" +
                            "$P像是閃爍的愛人微笑$R;" +
                            "$R對我來說太閃耀了$R;");
                        break;
                    case 4:
                        Say(pc, 131, "薔微的刻印是諾頓的標誌呀$R;" +
                            "刻在街道的各處$R;" +
                            "$R看樣子女王陛下非常喜歡呢$R;");
                        break;
                    case 5:
                        Say(pc, 131, "這是女王陛下生活過的城喔$R;" +
                            "$R第一次看到女王陛下的時候$R;" +
                            "非常驚訝吧$R;");
                        break;
                    case 6:
                        Say(pc, 131, "慈祥的老爺爺$R;" +
                            "最近收到了零食唷$R;" +
                            "$R嘴裡嘀咕嘀咕的念著咒語$R;" +
                            "還有很多一直在祈禱的人$R;" +
                            "感覺有點恐怖的$R;");
                        break;
                    case 7:
                        Say(pc, 131, "挺博學呢！$R;" +
                            "好浪漫喔！$R;" +
                            "$R（說實話，很煩悶的）$R;");
                        break;
                }
                selection = Select(pc, "少年正在講著自己知道的故事", "", "關於這道橋", "關於城門", "關於光之噴泉", "關於薔微刻印", "關於城", "魔法行會總部", "現在到此為止");
            }

        }


        void 情人节(ActorPC pc)
        {
            //EVT1100021012
            TakeItem(pc, 10033200, 1);
            GiveItem(pc, 10033351, 1);
            Say(pc, 131, "什麼事？$R;" +
                "$R我們現在過著情人節$R;" +
                "不要打擾我們啦$R;" +
                "$P怎樣給您愛？$R;" +
                "$R把我誠懇的愛$R;" +
                "放在『空瓶』裡是嗎？$R;" +
                "$P有病啊？$R;" +
                "$R可能嗎？$R;" +
                "愛怎能那麼容易就能得到呢？$R;");
            Say(pc, 131, "把那個瓶子給我看看吧！$R;" +
                "哎~，哎~$R;");
            ShowEffect(pc, 5155);
            Wait(pc, 4000);
            Say(pc, 131, "『空瓶』$R;" +
                "變成了『充滿愛的瓶子』！$R;");
            Say(pc, 131, "(不做怎麼知道不行呢！)$R;");
            //EVENTEND
        }
        void 万圣节(ActorPC pc)
        {
            BitMask<wsj> wsj_mask = new BitMask<wsj>(pc.CMask["wsj"]);
            //int selection;
            Say(pc, 131, "なんでしゅ？$R;" +
            "$R僕らにわからないことなんて$R;" +
            "何もないでしゅよ。$R;", "ダーリン");

            Say(pc, 11000211, 131, "おまかせでしゅー！$R;", "ハニー");

            Say(pc, 131, "そうでしゅねー。$R;" +
            "$R男性の僧兵しゃんは$R;" +
            "手作り料理に弱いでしゅ。$R;", "ダーリン");

            Say(pc, 11000211, 131, "酒屋で作ってもらって$R;" +
            "自分で作ったことにしちゃったら$R;" +
            "どうでしゅ？$R;" +
            "$R別に、ばれたりしないでしゅ！$R;" +
            "（要領よく生きなきゃでしゅー！）$R;", "ハニー");
            wsj_mask.SetValue(wsj.对话2, true);
            wsj_mask.SetValue(wsj.对话, false);
            return;
        }
    }
}
