using System;
using System.Collections.Generic;
using System.Text;
using SagaLib;
using SagaDB.Actor;
using SagaMap.Scripting;
using SagaScript.Chinese.Enums;
namespace SagaScript.M30163000
{
    public class S11000234 : Event
    {
        public S11000234()
        {
            this.EventID = 11000234;
        }


        public override void OnEvent(ActorPC pc)
        {
            //万圣节部分
            BitMask<wsj> wsj_mask = new BitMask<wsj>(pc.CMask["wsj"]);
            //int selection;
            if (wsj_mask.Test(wsj.女王))
            {
                Say(pc, 131, "小部屋ですか？$R;" +
                "$Rええ、たしかに$R;" +
                "この魔法ギルド総本山にあります。$R;" +
                "$Pそこのワープから$R;" +
                "普段は行くことが出来るのですが$R;" +
                "今は行くことは出来ませんよ。$R;" +
                "$Pなぜって？$R;" +
                "僕もよく知りませんが、先ほどから$R;" +
                "結界が張られているようです。$R;" +
                "$R中に入りたいんですか？$R;" +
                "それは、無理じゃないかなぁ？$R;" +
                "$P結界をやぶる『破結界石』が$R;" +
                "あればいいんでしょうけどね。$R;" +
                "$Rいや、さすがにそんなの$R;" +
                "都合よく持ってないですよ。$R;" +
                "$P破結界石については$R;" +
                "街の魔法屋さんに聞けばわかるかも$R;" +
                "しれませんね。$R;", "見習いの少年");
                wsj_mask.SetValue(wsj.界石, true);
                wsj_mask.SetValue(wsj.女王, false);
                return;
            }

           
            if (CountItem(pc, 50024100) >= 1)
            {
                Say(pc, 131, "买了『星星贝雷帽』？$R;" +
                    "星星贝雷帽不错喔$R;" +
                    "$R我喜欢红色$R;" +
                    "上次在诺森市委托裁缝阿姨$R;" +
                    "$R染了顏色哦$R;" +
                    "$P那个阿姨？$R;" +
                    "说想回归自然$R;" +
                    "就不知道搬到哪裡去了$R;" +
                    "$R不知过的怎么样了$R;");
                return;
            }
            //未加入开门以后判定
            if (pc.Job == PC_JOB.WIZARD
             || pc.Job == PC_JOB.SORCERER
             || pc.Job == PC_JOB.SAGE
             || pc.Job == PC_JOB.SHAMAN
             || pc.Job == PC_JOB.ELEMENTER
             || pc.Job == PC_JOB.ENCHANTER
             || pc.Job == PC_JOB.VATES
             || pc.Job == PC_JOB.DRUID
             || pc.Job == PC_JOB.BARD
             || pc.Job == PC_JOB.WARLOCK
             || pc.Job == PC_JOB.GAMBLER
             || pc.Job == PC_JOB.NECROMANCER)
            {
                if (pc.Level > 39)
                {
                    Say(pc, 131, "这里的魔法行会$R;" +
                        "有很多隐蔽的房间$R;" +
                        "$P不过施了魔法，$R;" +
                        "像我这样水准低的魔法师$R;" +
                        "是看不到的啦$R;" +
                        "$P也许您能看到吧$R;" +
                        "找到那个房间。$R;");
                    return;
                }
                Say(pc, 131, "这里的魔法行会，$R;" +
                    "有很多隐蔽的房间$R;" +
                    "不过施了魔法，$R;" +
                    "像我这样水准低的人$R;" +
                    "是看不到的啦$R;" +
                    "$P可能您也看不到吧。$R;");
                //EVENTEND
                return;
            }
            Say(pc, 131, "……$R;" +
                "我在冥想，不要妨碍我啊$R;");
        }
    }
}