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

            Say(pc, 11000976, 131, "我是初心者向导哦!$R;" +
                                   "$R不知道做什么好的时候，$R;" +
                                   "就到我这里来吧!$R;" +
                                   "$P我一直在「阿克罗波利斯」的$R;" +
                                   "东、南、西、北吊桥上。$R;" +
                                   "$P到学校的话，能学得到各种知识喔!$R;" +
                                   "$R有不知道的一定要去看看啊!$R;" +
                                   "$P啊，对了!$R;" +
                                   "这学校只有等级15以下者才能进去唷!$R;" +
                                   "$R那等级以上的话，$R;" +
                                   "不需要说明吧?$R;" +
                                   "$P还有往城市方向，再过去一点，$R;" +
                                   "就会有「队伍招募广场」喔!$R;" +
                                   "$R在那个广场里面，$R;" +
                                   "就像在城市里一样，$R;" +
                                   "体力会自然恢復的。$R;" +
                                   "$P休息的时候使用，还不错呀!$R;" +
                                   "$R比坐在别的地方，$R;" +
                                   "恢复得快。$R;", "初心者向导");
        }

        void 轉交感謝信任務(ActorPC pc)
        {
            BitMask<Beginner_02> Beginner_02_mask = new BitMask<Beginner_02>(pc.CMask["Beginner_02"]);

            Beginner_02_mask.SetValue(Beginner_02.已經把信轉交給初心者嚮導, true);

            Say(pc, 11000976, 131, "嗯? 找我有什么事情?$R;", "初心者向导");

            TakeItem(pc, 10043190, 1);
            Say(pc, 0, 0, "递上了『感谢信』!$R;", " ");

            Say(pc, 11000976, 131, "这个是…$R;" +
                                   "$R这样的都…真的谢谢!$R;" +
                                   "$P要不要给写这信的孩子转告一声呢?$R;" +
                                   "「谢谢!! 以后请加油!」$R;" +
                                   "这样的话呢?$R;", "初心者向导");
        }  
    }
}
