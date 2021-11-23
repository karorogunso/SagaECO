using System;
using System.Collections.Generic;
using System.Text;
using SagaLib;
using SagaDB.Actor;
using SagaMap.Scripting;
using SagaScript.Chinese.Enums;
namespace SagaScript.M10063000
{
    public class S11000576 : Event
    {
        public S11000576()
        {
            this.EventID = 11000576;
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
                    Say(pc, 11000575, 131, "啊！这不就是『炎之特效药』！！$R;" +
                        "$R真是！不知道该怎么说$R;" +
                        "$P真的谢谢您$R;" +
                        "我是不会忘掉您的恩情的$R;" +
                        "$R这次要给您力量作谢礼$R;");
                    TakeItem(pc, 10000601, 1);
                    GiveItem(pc, 10021700, 1);
                    Say(pc, 131, "给他『炎之特效药』$R;" +
                        "得到活动木偶火焰凤凰$R;");
                    mask.SetValue(NBDLFlags.寻找特效藥, false);
                    Warp(pc, 20022000, x, y);
                    //_2A53 = false;
                    //WARP 779
                    return;
                }
                Say(pc, 11000575, 131, "特效药还有很长时间才能拿到吗？$R;");
                switch (Select(pc, "回到原来地方吗？", "", "好啊", "不要"))
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
            Say(pc, 11000575, 131, "来这里的人$R;" +
                "到底有多少啊？$R;" +
                "$R太强了$R;" +
                "$P能不能拜托您一件事呢？$R;");
            switch (Select(pc, "接受吗？", "", "接受", "不接受"))
            {
                case 1:
                    Say(pc, 11000575, 131, "太感谢了$R;" +
                        "最近身体不好，$R;" +
                        "不能动啊$R;" +
                        "能不能帮我$R;" +
                        "去拿一些自然复原的特效药?$R;" +
                        "去铁火山$R;" +
                        "火焰蜥蜴应该有那种药$R;" +
                        "可以帮我拿一点回来吗?$R;" +
                        "$P拜托了$R;");
                    mask.SetValue(NBDLFlags.接受特效藥任务, true);
                    mask.SetValue(NBDLFlags.寻找特效藥, true);
                    //_2A52 = true;
                    //_2A53 = true;
                    Warp(pc, 20022000, x, y);
                    //WARP 779
                    break;
                case 2:
                    Say(pc, 11000574, 131, "是吗？$R;" +
                        "最近的人都太冷淡了$R;");
                    Warp(pc, 20022000, x, y);
                    //WARP 779
                    break;
            }

        }
    }
}