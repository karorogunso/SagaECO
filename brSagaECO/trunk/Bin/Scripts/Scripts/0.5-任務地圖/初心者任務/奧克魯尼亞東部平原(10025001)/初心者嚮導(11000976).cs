using System;
using System.Collections.Generic;
using System.Text;

using SagaDB.Actor;
using SagaMap.Scripting;

using SagaLib;
using SagaScript.Chinese.Enums;
//所在地圖:奧克魯尼亞東部平原(10025001) NPC基本信息:初心者嚮導(11000976) X:111 Y:121
namespace SagaScript.M10025001
{
    public class S11000976 : Event
    {
        public S11000976()
        {
            this.EventID = 11000976;
        }

        public override void OnEvent(ActorPC pc)
        {
            BitMask<Beginner_02> Beginner_02_mask = new BitMask<Beginner_02>(pc.CMask["Beginner_02"]);

            if (Beginner_02_mask.Test(Beginner_02.轉交感謝信任務開始) &&
                !Beginner_02_mask.Test(Beginner_02.已經把信轉交給初心者嚮導) && 
                CountItem(pc, 10043190) > 0)
            {
                轉交感謝信任務(pc);
                return;
            }

            Say(pc, 11000976, 131, "我是初心者嚮導唷!$R;" +
                                   "$R不知道做什麼好的時候，$R;" +
                                   "就到我這裡來吧!$R;" +
                                   "$P我一直在「阿高普路斯市」的$R;" +
                                   "東、南、西、北吊橋上。$R;" +
                                   "$P到學校的話，能學得到各種知識喔!$R;" +
                                   "$R有不知道的一定要去看看啊!$R;" +
                                   "$P啊，對了!$R;" +
                                   "這學校只有等級15以下者才能進去唷!$R;" +
                                   "$R那等級以上的話，$R;" +
                                   "不需要說明吧?$R;" +
                                   "$P還有往城市方向，再過去一點，$R;" +
                                   "就會有「隊伍招募廣場」喔!$R;" +
                                   "$R在那個廣場裡面，$R;" +
                                   "就像在城市裡一樣，$R;" +
                                   "體力會自然恢復的。$R;" +
                                   "$P休息的時候使用，還不錯呀!$R;" +
                                   "$R比坐在別的地方，$R;" +
                                   "恢復得快。$R;", "初心者嚮導");
        }

        void 轉交感謝信任務(ActorPC pc)
        {
            BitMask<Beginner_02> Beginner_02_mask = new BitMask<Beginner_02>(pc.CMask["Beginner_02"]);

            Beginner_02_mask.SetValue(Beginner_02.已經把信轉交給初心者嚮導, true);

            Say(pc, 11000976, 131, "嗯? 找我有什麼事情?$R;", "初心者嚮導");

            TakeItem(pc, 10043190, 1);
            Say(pc, 0, 0, "遞上了『感謝信』!$R;", " ");

            Say(pc, 11000976, 131, "這個是…$R;" +
                                   "$R這樣的都…真的謝謝!$R;" +
                                   "$P要不要給寫這信的孩子轉告一聲呢?$R;" +
                                   "「謝謝!! 以後請加油!」$R;" +
                                   "這樣的話呢?$R;", "初心者嚮導");
        }  
    }
}
