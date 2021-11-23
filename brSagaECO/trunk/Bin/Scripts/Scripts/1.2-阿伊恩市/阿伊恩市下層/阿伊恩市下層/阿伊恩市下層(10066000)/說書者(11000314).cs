using System;
using System.Collections.Generic;
using System.Text;
using SagaLib;
using SagaDB.Actor;
using SagaMap.Scripting;
using SagaScript.Chinese.Enums;
namespace SagaScript.M10066000
{
    public class S11000314 : Event
    {
        public S11000314()
        {
            this.EventID = 11000314;
        }

        public override void OnEvent(ActorPC pc)
        {
            Say(pc, 131, "來，可愛的孩子們$R;" +
                "想聽什麼故事啊？$R;");
            Say(pc, 11000318, 131, "有公主的故事？$R;");
            Say(pc, 11000315, 131, "那個沒意思呢$R;" +
                "給我們講擊退魔物的那種$R;" +
                "刺激的故事吧$R;");
            Say(pc, 131, "想聽什麼故事呢$R;");
            switch (Select(pc, "聽什麼故事呢？", "", "虎姆拉的傳說", "不聽"))
            {
                case 1:
                    Say(pc, 11000314, 131, "有種樣子雖然像虎姆拉$R但不是虎姆拉的人。$R;" +
                        "只要得到他翅膀的人就可以敞開$R;" +
                        "$R虎姆拉走的路唷。$R;" +
                        "$P這就是從古代開始傳下來$R;" +
                        "關於虎姆拉的傳說啦$R;");
                    break;
            }
        }
    }
}