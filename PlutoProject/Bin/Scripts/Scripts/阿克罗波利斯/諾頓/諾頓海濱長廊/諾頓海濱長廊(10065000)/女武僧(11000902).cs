using System;
using System.Collections.Generic;
using System.Text;
using SagaLib;
using SagaDB.Actor;
using SagaMap.Scripting;
using SagaScript.Chinese.Enums;
namespace SagaScript.M10065000
{
    public class S11000902 : Event
    {
        public S11000902()
        {
            this.EventID = 11000902;
        }

        public override void OnEvent(ActorPC pc)
        {
            BitMask<NDFlags> mask = new BitMask<NDFlags>(pc.CMask["ND"]);
            if (pc.Fame < 10)
            {
                Say(pc, 131, "为了调查最近发现的遗迹，$R;" +
                    "诺森正在召集调查队$R;" +
                    "$R对您来说，这件事情有点困难$R;" +
                    "$P想参加调查队，$R;" +
                    "先增强力量后再来吧。$R;");
                return;
            }
            if (mask.Test(NDFlags.协助) && mask.Test(NDFlags.遗迹))
            {
                Say(pc, 131, "调查需要很长时间$R;" +
                    "在那期间这里会开放的，$R;" +
                    "请协助我们调查吧。$R;");
                return;
            }
            Say(pc, 131, "诺森正在召集调查队，$R;" +
                "调查最近发现的遗迹$R;" +
                "$R不怕危险的话，$R;" +
                "请参加吧。$R;");
            switch (Select(pc, "参加吗？", "", "不参加", "遗迹？"))
            {
                case 1:
                    break;
                case 2:
                    mask.SetValue(NDFlags.遗迹, true);
                    Say(pc, 131, "这是一个非常庞大的遗迹$R;" +
                        "各国竞争调查遗跡，$R;" +
                        "现在已经有好几个调查队$R;" +
                        "探测遗迹内部啊$R;" +
                        "$P虽然我们不愿意，$R;" +
                        "但被派入调查队跟男士们一起调查$R;" +
                        "有兴趣就参加我们调查队协助我们吧$R;");
                    switch (Select(pc, "怎么办呢？", "", "协助", "拒绝"))
                    {
                        case 1:
                            mask.SetValue(NDFlags.协助, true);
                            Say(pc, 131, "准备完后，到这个房间吧$R;" +
                                "我用移动魔法$R;" +
                                "$R把您送到遗迹入口喔。$R;");
                            break;
                        case 2:
                            break;
                    }
                    break;
            }
        }
    }
}
