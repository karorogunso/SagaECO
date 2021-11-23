using System;
using System.Collections.Generic;
using System.Text;

using SagaDB.Actor;
using SagaMap.Scripting;

using SagaLib;
using SagaScript.Chinese.Enums;
//所在地圖:少數民族利益代表的房間(30054000) NPC基本信息:瑪歐斯(11000029) X:3 Y:3
namespace SagaScript.M30054000
{
    public class S11000029 : Event
    {
        public S11000029()
        {
            this.EventID = 11000029;
        }

        public override void OnEvent(ActorPC pc)
        {
            BitMask<Neko_01> Neko_01_cmask = pc.CMask["Neko_01"];
            BitMask<Neko_01> Neko_01_amask = pc.AMask["Neko_01"];
            BitMask<Puppet_Fish> Puppet_Fish_mask = pc.CMask["Puppet_Fish"];

            if (!Neko_01_amask.Test(Neko_01.桃子任務完成) &&
                Neko_01_cmask.Test(Neko_01.與瑪歐斯對話) &&
                !Neko_01_cmask.Test(Neko_01.與雷米阿對話))
            {
                Say(pc, 131, "走開！！$R;" +
                    "$R您身上罩著什麼東西，$R可怕的東西！$R;");
                Say(pc, 11000030, 131, "瑪歐斯，怎麼了，没什麼啊$R;");
                Say(pc, 11000031, 131, "不是旁邊$R;");
                Say(pc, 131, "好像罩著什麼$R;" +
                    "$R去占卜師那裡看看吧。$R;");
                return;
            }

            if (!Neko_01_amask.Test(Neko_01.桃子任務完成) && 
                !Neko_01_cmask.Test(Neko_01.與瑪歐斯對話) &&
                Neko_01_cmask.Test(Neko_01.使用不明的鬍鬚))
            {
                Neko_01_cmask.SetValue(Neko_01.與瑪歐斯對話, true);
                Neko_01_cmask.SetValue(Neko_01.使用不明的鬍鬚, false);
                Say(pc, 131, "…?$R咦??$R;" +
                    "$P…！！！$R;");
                Say(pc, 11000030, 131, "瑪歐斯怎麼了？$R;");
                Say(pc, 11000031, 131, "怎麼了？$R;");
                Say(pc, 0, 131, "喵~~$R;", "???");
                Say(pc, 131, "啊！！$R;" +
                    "$R您是什麼東西，滚開！！$R;");
                Say(pc, 11000030, 131, "到底怎麼了？$R;");
                Say(pc, 11000031, 131, "怎麼了？$R;");
                Say(pc, 131, "$R走開，不要過來！1$R;");
                return;
            }

            if (Puppet_Fish_mask.Test(Puppet_Fish.得到歐瑪斯))
            {
                if (pc.Marionette != null)
                {
                    Say(pc, 131, "哦，來得好，同志$R;" +
                        "…??$R;" +
                        "$P嗯，好像有什麼不同呢。$R不過應該没事吧。$R;");
                    Say(pc, 11000030, 131, "應該没關係啊$R;");
                    Say(pc, 11000031, 131, "應該没關係啊$R;");
                    任務(pc);
                    return;
                }
                Say(pc, 131, "上次真是感謝您啊$R;" +
                    "$R但是這裡是我們種族神聖的地方，$R您還是回去吧。$R;");
                Say(pc, 11000030, 131, "還是回去吧$R;");
                Say(pc, 11000031, 131, "回去吧$R;");
                return;
            }
            
            if (CountItem(pc, 10007400) >= 1 || 
                CountItem(pc, 10007450) >= 1)
            {
                if (Puppet_Fish_mask.Test(Puppet_Fish.幫助))
                {
                    Say(pc, 131, "將『曬乾的海鮮』變回$R;" +
                        "原來的『瑪歐斯』吧。$R;");
                    return;
                }
                //*/
                Say(pc, 361, "這是『曬乾的海鮮』？$R;");
                Say(pc, 131, "$R啊，怎麼會這樣啊？$R;");
                Say(pc, 11000030, 131, "怎麼搞得？$R;");
                Say(pc, 11000031, 131, "有困難的事就説出來吧$R;");
                Say(pc, 131, "這個曬乾的海鮮是『乾了的瑪歐斯』$R;" +
                    "$R想讓他變回原來的樣子啊$R;" +
                    "不是特殊的地方，是變不回來的$R;" +
                    "$P但是我有事情要做，分不開身呢。$R;" +
                    "$R該怎麼辦呢？$R;");
                幫忙(pc);
                return;
            }

            if (pc.Marionette != null)
            {
                Say(pc, 131, "哦，來得好，同志$R;" +
                    "…??$R;" +
                    "$P嗯，好像有什麼不同呢。$R不過應該没事吧。$R;");
                Say(pc, 11000030, 131, "應該没關係啊$R;");
                Say(pc, 11000031, 131, "應該没關係啊$R;");
                任務(pc);
                return;
            }
            Say(pc, 131, "$R這裡是我們神聖的地方，$R外人請離開。$R;");
            Say(pc, 11000030, 131, "外人請離開$R;");
            Say(pc, 11000031, 131, "不要到這裡。$R;");
        }

        void 任務(ActorPC pc)
        {
            BitMask<OMASI> omasi_mask = pc.CMask["OMASI"];
            switch (Select(pc, "做什麼呢？", "", "任務服務台", "什麼也不做"))
            {
                case 1:
                    if (!omasi_mask.Test(OMASI.第一次对话))
                    {
                        Say(pc, 131, "成為我們的力量吧。$R;");
                        Say(pc, 11000030, 131, "需要您的力量$R;");
                        Say(pc, 11000031, 131, "需要您的幫助$R;");
                        Say(pc, 131, "$R在我們的囑托裡還有很特殊的。$R;");
                        Say(pc, 11000030, 131, "還包括比較難的事情。$R;");
                        Say(pc, 11000031, 131, "簡單的也有$R;");
                        Say(pc, 131, "我們相信您$R;");
                        Say(pc, 11000030, 131, "因為相信您，才拜託您的$R;");
                        Say(pc, 11000031, 131, "因為信賴您，才拜託您的$R;");
                        omasi_mask.SetValue(OMASI.第一次对话, true);
                        任務(pc);
                        return;
                    }
                    HandleQuest(pc, 47);
                    break;
                case 2:
                    Say(pc, 131, "不管什麼時候都過來吧，$R我會等您的。$R;");
                    Say(pc, 11000030, 131, "再來啦$R;");
                    Say(pc, 11000031, 131, "再來玩啊。$R;");
                    break;
            }
        }

        void 幫忙(ActorPC pc)
        {
            BitMask<Puppet_Fish> Puppet_Fish_mask = pc.CMask["Puppet_Fish"];
            Say(pc, 11000030, 131, "真是，難道我們什麼也做不了嗎？$R;");
            Say(pc, 11000031, 131, "什麼也做不了嗎？$R;");
            Say(pc, 0, 131, "用哀怨的眼睛看著我。。$R;" +
                "$R幫忙嗎？$R;", " ");
            switch (Select(pc, "幫忙嗎？", "", "幫忙", "不幫忙"))
            {
                case 1:
                    Say(pc, 131, "就等您那句話了。$R;" +
                        "$P在大陸的洞窟深處，有我們夥伴$R『瑪歐斯』生活的隱秘的村莊。$R;" +
                        "$R在那裡的長老就能把$R『曬乾的海鮮』變回原樣了。$R;" +
                        "$P入口很隱秘，可能不好找，$R但是希望您盡力啊。$R;");
                    Say(pc, 11000030, 131, "一定要盡力找啊$R;");
                    Say(pc, 11000031, 131, "一定要幫我找到啦！$R;");
                    Puppet_Fish_mask.SetValue(Puppet_Fish.幫助, true);
                    //_3a17 = true;
                    break;
                case 2:
                    幫忙(pc);
                    break;
            }
        }
    }
}
namespace SagaScript.Chinese.Enums
{
    public enum OMASI
    {
        第一次对话 = 0x1,
    }
}