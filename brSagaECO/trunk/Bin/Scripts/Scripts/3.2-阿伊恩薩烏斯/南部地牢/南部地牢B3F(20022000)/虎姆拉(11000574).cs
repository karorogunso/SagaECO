using System;
using System.Collections.Generic;
using System.Text;
using SagaLib;
using SagaDB.Actor;
using SagaMap.Scripting;
using SagaScript.Chinese.Enums;
namespace SagaScript.M10063000
{
    public class S11000574 : Event
    {
        public S11000574()
        {
            this.EventID = 11000574;
        }

        public override void OnEvent(ActorPC pc)
        {
            BitMask<NBDLFlags> mask = new BitMask<NBDLFlags>(pc.CMask["NBDL"]);
            byte x , y;
            x = (byte)Global.Random.Next(121, 135);
            y = (byte)Global.Random.Next(49, 59);
            if (mask.Test(NBDLFlags.寻找特效藥))//_2A53
            {
                if (CountItem(pc, 10000601) >= 1)
                {
                    Say(pc, 11000575, 131, "啊！這不就是『火焰特效藥』！！$R;" +
                        "$R真是！不知道該怎麼說$R;" +
                        "$P真的謝謝您$R;" +
                        "我是不會忘掉您的恩情的$R;" +
                        "$R這次要給您力量作謝禮$R;");
                    TakeItem(pc, 10000601, 1);
                    GiveItem(pc, 10021700, 1);
                    Say(pc, 131, "給他『火焰特效藥』$R;" +
                        "得到活動木偶虎姆拉$R;");
                    mask.SetValue(NBDLFlags.寻找特效藥, false);
                    Warp(pc, 20022000, x, y);
                    //_2A53 = false;
                    //WARP 779
                    return;
                }
                Say(pc, 11000575, 131, "特效藥還有很長時間才能拿到嗎？$R;");
                switch (Select(pc, "回到原來地方嗎？", "", "好阿", "不要"))
                {
                    case 1:
                        Warp(pc, 20022000, x, y);
                        //WARP 779
                        break;
                    case 2:
                        break;
                }
                return;
            }
            Say(pc, 11000575, 131, "來這裡的人$R;" +
                "到底有多少啊？$R;" +
                "$R太強了$R;" +
                "$P能不能拜託您一件事呢？$R;");
            switch (Select(pc, "接受嗎？", "", "接受", "不接受"))
            {
                case 1:
                    Say(pc, 11000575, 131, "太感謝了$R;" +
                        "最近身體不好，$R;" +
                        "不能動啊$R;" +
                        "能不能幫我$R;" +
                        "去拿一些自然復原特效藥?$R;" +
                        "去鐵火山$R;" +
                        "塞爾曼德應該有那種藥$R;" +
                        "可以幫我拿一點回來嗎?$R;" +
                        "$P拜託了$R;");
                    mask.SetValue(NBDLFlags.接受特效藥任务, true);
                    mask.SetValue(NBDLFlags.寻找特效藥, true);
                    //_2A52 = true;
                    //_2A53 = true;
                    Warp(pc, 20022000, x, y);
                    //WARP 779
                    break;
                case 2:
                    Say(pc, 11000574, 131, "是嗎？$R;" +
                        "最近的人都太冷淡了$R;");
                    Warp(pc, 20022000, x, y);
                    //WARP 779
                    break;
            }

        }
    }
}