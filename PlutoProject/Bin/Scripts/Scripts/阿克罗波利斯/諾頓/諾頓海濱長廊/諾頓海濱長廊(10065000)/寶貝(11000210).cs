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
                "想跟您在这条街上逛唷$R;");
            selection = Select(pc, "少年正在讲着自己知道的故事", "", "关于这道桥", "关于城门", "关于光之喷泉", "关于蔷微刻印", "关于城", "魔法行会总部", "现在到此为止");
            while (selection != 7)
            {
                switch (selection)
                {
                    case 1:
                        Say(pc, 131, "这座桥叫『爱之桥』$R;" +
                            "$R有一个传说，$R;" +
                            "两个人手拉手走过$R;" +
                            "就会永远相爱喔$R;" +
                            "$P周围环境不错$R;" +
                            "每到圣诞节或情人节时$R;" +
                            "都聚集很多恋人呢$R;" +
                            "$R我们一会儿也会过那座桥的$R;");
                        break;
                    case 2:
                        Say(pc, 131, "真是个巨大的城门呀$R;" +
                            "$R从古至今一直守护著这城市$R;" +
                            "免受暴风雪和敌人袭击喔$R;" +
                            "$P那门特别重，$R;" +
                            "以人的力量是打不开的$R;" +
                            "$P只能依靠女王陛下的魔力$R;" +
                            "才能进行开关喔$R;" +
                            "$R一定需要很庞大的魔力吧？$R;");
                        break;
                    case 3:
                        Say(pc, 131, "说的是城前闪烁的喷泉吧$R;" +
                            "$R喷出来的不是水，而是光的粒子唷$R;" +
                            "$P像是闪烁的爱人微笑$R;" +
                            "$R对我来说太闪耀了$R;");
                        break;
                    case 4:
                        Say(pc, 131, "蔷微的刻印是诺森的标志呀$R;" +
                            "刻在街道的各处$R;" +
                            "$R看样子女王陛下非常喜欢呢$R;");
                        break;
                    case 5:
                        Say(pc, 131, "这是女王陛下生活过的城喔$R;" +
                            "$R第一次看到女王陛下的时候$R;" +
                            "非常惊讶吧$R;");
                        break;
                    case 6:
                        Say(pc, 131, "慈祥的老爷爷$R;" +
                            "最近收到了零食哦$R;" +
                            "$R嘴裡嘀咕嘀咕的念著咒语$R;" +
                            "还有很多一直在祈祷的人$R;" +
                            "感觉有点恐怖的$R;");
                        break;
                    case 7:
                        Say(pc, 131, "挺博学呢！$R;" +
                            "好浪漫喔！$R;" +
                            "$R（说实话，很烦闷的）$R;");
                        break;
                }
                selection = Select(pc, "少年正在讲着自己知道的故事", "", "关于这道桥", "关于城门", "关于光之喷泉", "关于蔷微刻印", "关于城", "魔法行会总部", "现在到此为止");
            }

        }


        void 情人节(ActorPC pc)
        {
            //EVT1100021012
            TakeItem(pc, 10033200, 1);
            GiveItem(pc, 10033351, 1);
            Say(pc, 131, "什么事？$R;" +
                "$R我们现在过着情人节$R;" +
                "不要打扰我们啦$R;" +
                "$P怎样给您爱？$R;" +
                "$R把我诚恳的爱$R;" +
                "放在『空瓶』里是吗？$R;" +
                "$P哈哈哈？$R;" +
                "$R怎么可能啦？$R;" +
                "爱怎能那么容易就能得到呢？$R;");
            Say(pc, 131, "把那个瓶子给我看看吧！$R;" +
                "哎~，哎~$R;");
            ShowEffect(pc, 5155);
            Wait(pc, 4000);
            Say(pc, 131, "『空瓶』$R;" +
                "变成了『充满爱的瓶子』！$R;");
            Say(pc, 131, "(不做怎么知道不行呢！)$R;");
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
