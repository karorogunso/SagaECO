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
                Say(pc, 11000139, 131, "呵呵，得到「雷魚的汗」$R;" +
                    "是不是還不能滿足呢？$R;" +
                    "$R那讓我帶您這個朋友去旅行吧。$R;");
                if (CheckInventory(pc, 10019400, 1))
                {
                    GiveItem(pc, 10019400, 1);
                    PlaySound(pc, 4006, false, 100, 50);
                    Say(pc, 0, 131, "得到了「活動木偶瑪歐斯」$R;");
                    Say(pc, 11000139, 131, "雖然對火的話會比較弱，$R;" +
                        "但是還是能夠幫到您的。$R;" +
                        "$R希望您珍惜它阿。$R;");
                    Puppet_Fish_mask.SetValue(Puppet_Fish.得到歐瑪斯, false);
                    return;
                }
                Say(pc, 11000139, 131, "您的行李太多了，整理一下吧。$R;");
                return;
            }
            if (CountItem(pc, 10007400) >= 1)
            {
                Say(pc, 11000139, 131, "咦？「乾魚」？$R;" +
                    "$R現在把您變回原來的樣子吧$R;");
                Fade(pc, FadeType.Out, FadeEffect.Black);
                Wait(pc, 1000);
                PlaySound(pc, 3040, false, 100, 50);
                Wait(pc, 4000);
                Fade(pc, FadeType.In, FadeEffect.Black);
                if (CountItem(pc, 10007450) >= 1)
                {
                    Say(pc, 0, 131, "「乾魚」變成了瑪歐斯$R;");
                    TakeItem(pc, 10007450, 1);
                    Say(pc, 11000139, 131, "啊，變回原來的樣子了。$R;" +
                        "沒穿衣服的樣子，$R;" +
                        "可能因為害羞，躲到後面去了。$R;" +
                        "$R已經是知道羞恥的年紀了阿。$R;" +
                        "$P真的非常感謝。$R;" +
                        "送您一個謝禮好像有點……$R;" +
                        "可還是要送您一些好東西唷!$R;");
                    PlaySound(pc, 2040, false, 100, 50);
                    Say(pc, 0, 131, "得到了「雷魚的汗」。$R;");
                    GiveItem(pc, 10008600, 1);
                    Puppet_Fish_mask.SetValue(Puppet_Fish.得到歐瑪斯, true);
                    return;
                }
                Say(pc, 0, 131, "「乾魚」變成「鮭魚」了。$R;");
                Say(pc, 11000139, 131, "嗯，仔細看看$R;" +
                    "好像是普通的「鮭魚」阿$R;" +
                    "$R好像是外形相似，看錯了$R;");
                PlaySound(pc, 2040, false, 100, 50);
                Say(pc, 0, 131, "得到「鮭魚」$R;");
                TakeItem(pc, 10007400, 1);
                GiveItem(pc, 10007250, 1);
                Puppet_Fish_mask.SetValue(Puppet_Fish.幫助過, true);
                return;
            }
            if (CountItem(pc, 10007450) >= 1)
            {
                Say(pc, 11000139, 131, "咦？「乾魚」？$R;" +
                    "$R現在把您變回原來的樣子吧$R;");
                Fade(pc, FadeType.Out, FadeEffect.Black);
                Wait(pc, 1000);
                PlaySound(pc, 3040, false, 100, 50);
                Wait(pc, 4000);
                Fade(pc, FadeType.In, FadeEffect.Black);
                if (CountItem(pc, 10007450) >= 1)
                {
                    Say(pc, 0, 131, "「乾魚」變成了瑪歐斯$R;");
                    TakeItem(pc, 10007450, 1);
                    Say(pc, 11000139, 131, "啊，變回原來的樣子了。$R;" +
                        "沒穿衣服的樣子，$R;" +
                        "可能因為害羞，躲到後面去了。$R;" +
                        "$R已經是知道羞恥的年紀了阿。$R;" +
                        "$P真的非常感謝。$R;" +
                        "送您一個謝禮好像有點……$R;" +
                        "可還是要送您一些好東西唷!$R;");
                    PlaySound(pc, 2040, false, 100, 50);
                    Say(pc, 0, 131, "得到了「雷魚的汗」。$R;");
                    GiveItem(pc, 10008600, 1);
                    Puppet_Fish_mask.SetValue(Puppet_Fish.得到歐瑪斯, true);
                    return;
                }
                Say(pc, 0, 131, "「乾魚」變成「鮭魚」了。$R;");
                Say(pc, 11000139, 131, "嗯，仔細看看$R;" +
                    "好像是普通的「鮭魚」阿$R;" +
                    "$R好像是外形相似，看錯了$R;");
                PlaySound(pc, 2040, false, 100, 50);
                Say(pc, 0, 131, "得到「鮭魚」$R;");
                TakeItem(pc, 10007400, 1);
                GiveItem(pc, 10007250, 1);
                Puppet_Fish_mask.SetValue(Puppet_Fish.幫助過, true);
                return;
            }
            Say(pc, 11000139, 131, "我是瑪歐斯的長老$R;");
        }
    }
}