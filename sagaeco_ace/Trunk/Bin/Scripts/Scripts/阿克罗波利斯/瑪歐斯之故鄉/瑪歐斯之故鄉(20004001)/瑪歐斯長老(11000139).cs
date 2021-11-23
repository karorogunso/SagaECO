using System;
using System.Collections.Generic;
using System.Text;

using SagaDB.Actor;
using SagaMap.Scripting;

using SagaLib;
using SagaScript.Chinese.Enums;
namespace SagaScript.M20004001
{
    public class S11000139 : Event
    {
        public S11000139()
        {
            this.EventID = 11000139;
        }

        public override void OnEvent(ActorPC pc)
        {
            BitMask<Puppet_Fish> Puppet_Fish_mask = pc.CMask["Puppet_Fish"];
            if (Puppet_Fish_mask.Test(Puppet_Fish.得到歐瑪斯))
            {
                Say(pc, 11000139, 131, "呵呵，得到「雷鱼的汗」$R;" +
                    "是不是还不能满足呢？$R;" +
                    "$R那让我带您这个朋友去旅行吧。$R;");
                if (CheckInventory(pc, 10019400, 1))
                {
                    GiveItem(pc, 10019400, 1);
                    PlaySound(pc, 4006, false, 100, 50);
                    Say(pc, 0, 131, "得到了「活动木偶鱼人」$R;");
                    Say(pc, 11000139, 131, "虽然对火的话会比较弱，$R;" +
                        "但是还是能够帮到您的。$R;" +
                        "$R希望您珍惜它阿。$R;");
                    Puppet_Fish_mask.SetValue(Puppet_Fish.得到歐瑪斯, false);
                    return;
                }
                Say(pc, 11000139, 131, "您的行李太多了，整理一下吧。$R;");
                return;
            }
            if (CountItem(pc, 10007400) >= 1)
            {
                Say(pc, 11000139, 131, "咦？「干鱼」？$R;" +
                    "$R现在把您变回原来的样子吧$R;");
                Fade(pc, FadeType.Out, FadeEffect.Black);
                Wait(pc, 1000);
                PlaySound(pc, 3040, false, 100, 50);
                Wait(pc, 4000);
                Fade(pc, FadeType.In, FadeEffect.Black);
                if (CountItem(pc, 10007450) >= 1)
                {
                    Say(pc, 0, 131, "「干鱼」变成了鱼人$R;");
                    TakeItem(pc, 10007450, 1);
                    Say(pc, 11000139, 131, "啊，变回原来的样子了。$R;" +
                        "没穿衣服的样子，$R;" +
                        "可能因为害羞，躲到后面去了。$R;" +
                        "$R已经是知道羞耻的年纪了阿。$R;" +
                        "$P真的非常感谢。$R;" +
                        "送您一个谢礼好像有点……$R;" +
                        "可还是要送您一些好东西!$R;");
                    PlaySound(pc, 2040, false, 100, 50);
                    Say(pc, 0, 131, "得到了「雷鱼的汗」。$R;");
                    GiveItem(pc, 10008600, 1);
                    Puppet_Fish_mask.SetValue(Puppet_Fish.得到歐瑪斯, true);
                    return;
                }
                Say(pc, 0, 131, "「干鱼」变成「鲑鱼」了。$R;");
                Say(pc, 11000139, 131, "嗯，仔细看看$R;" +
                    "好像是普通的「鲑鱼」啊$R;" +
                    "$R好像是外形相似，看错了$R;");
                PlaySound(pc, 2040, false, 100, 50);
                Say(pc, 0, 131, "得到「鲑鱼」$R;");
                TakeItem(pc, 10007400, 1);
                GiveItem(pc, 10007250, 1);
                Puppet_Fish_mask.SetValue(Puppet_Fish.幫助過, true);
                return;
            }
            if (CountItem(pc, 10007450) >= 1)
            {
                Say(pc, 11000139, 131, "咦？「干鱼」？$R;" +
                    "$R现在把您变回原来的样子吧$R;");
                Fade(pc, FadeType.Out, FadeEffect.Black);
                Wait(pc, 1000);
                PlaySound(pc, 3040, false, 100, 50);
                Wait(pc, 4000);
                Fade(pc, FadeType.In, FadeEffect.Black);
                if (CountItem(pc, 10007450) >= 1)
                {
                    Say(pc, 0, 131, "「干鱼」变成了鱼人$R;");
                    TakeItem(pc, 10007450, 1);
                    Say(pc, 11000139, 131, "啊，变回原来的样子了。$R;" +
                        "没穿衣服的样子，$R;" +
                        "可能因为害羞，躲到后面去了。$R;" +
                        "$R已经是知道羞耻的年纪了阿。$R;" +
                        "$P真的非常感谢。$R;" +
                        "送您一个谢礼好像有点……$R;" +
                        "可还是要送您一些好东西!$R;");
                    PlaySound(pc, 2040, false, 100, 50);
                    Say(pc, 0, 131, "得到了「雷鱼的汗」。$R;");
                    GiveItem(pc, 10008600, 1);
                    Puppet_Fish_mask.SetValue(Puppet_Fish.得到歐瑪斯, true);
                    return;
                }
                Say(pc, 0, 131, "「干鱼」变成「鲑鱼」了。$R;");
                Say(pc, 11000139, 131, "嗯，仔细看看$R;" +
                    "好像是普通的「鲑鱼」啊$R;" +
                    "$R好像是外形相似，看错了$R;");
                PlaySound(pc, 2040, false, 100, 50);
                Say(pc, 0, 131, "得到「鲑鱼」$R;");
                TakeItem(pc, 10007400, 1);
                GiveItem(pc, 10007250, 1);
                Puppet_Fish_mask.SetValue(Puppet_Fish.幫助過, true);
                return;
            }
            Say(pc, 11000139, 131, "我是鱼人们的長老$R;");
        }
    }
}