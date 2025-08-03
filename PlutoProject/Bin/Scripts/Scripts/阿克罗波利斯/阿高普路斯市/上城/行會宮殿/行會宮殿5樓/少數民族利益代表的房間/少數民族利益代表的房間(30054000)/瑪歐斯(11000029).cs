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
                Say(pc, 131, "走开！！$R;" +
                    "$R您身上罩着什么东西，$R可怕的东西！$R;");
                Say(pc, 11000030, 131, "鱼人，怎么了，没什么啊$R;");
                Say(pc, 11000031, 131, "不是旁边$R;");
                Say(pc, 131, "好像罩着什么$R;" +
                    "$R去占卜师那里看看吧。$R;");
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
                Say(pc, 11000030, 131, "鱼人怎么了？$R;");
                Say(pc, 11000031, 131, "怎么了？$R;");
                Say(pc, 0, 131, "喵~~$R;", "???");
                Say(pc, 131, "啊！！$R;" +
                    "$R什么东西，滚开！！$R;");
                Say(pc, 11000030, 131, "到底怎么了？$R;");
                Say(pc, 11000031, 131, "怎么了？$R;");
                Say(pc, 131, "$R走开，不要过来！！$R;");
                return;
            }

            if (Puppet_Fish_mask.Test(Puppet_Fish.得到歐瑪斯))
            {
                if (pc.Marionette != null)
                {
                    Say(pc, 131, "哦，来得好，同伴$R;" +
                        "…??$R;" +
                        "$P嗯，好像有什么不同呢。$R不过应该没事吧。$R;");
                    Say(pc, 11000030, 131, "应该没关系啊$R;");
                    Say(pc, 11000031, 131, "应该没关系啊$R;");
                    任務(pc);
                    return;
                }
                Say(pc, 131, "上次真是感谢您啊$R;" +
                    "$R但是这里是我们种族神圣的地方，$R您还是回去吧。$R;");
                Say(pc, 11000030, 131, "还是回去吧$R;");
                Say(pc, 11000031, 131, "回去吧$R;");
                return;
            }
            
            if (CountItem(pc, 10007400) >= 1 || 
                CountItem(pc, 10007450) >= 1)
            {
                if (Puppet_Fish_mask.Test(Puppet_Fish.幫助))
                {
                    Say(pc, 131, "将『晒干的海鲜』变回$R;" +
                        "原来的『鱼人』吧。$R;");
                    return;
                }
                //*/
                Say(pc, 361, "这是『晒干的海鲜』？$R;");
                Say(pc, 131, "$R啊，怎么会这样啊？$R;");
                Say(pc, 11000030, 131, "怎么搞得？$R;");
                Say(pc, 11000031, 131, "有困难的事就说出来吧$R;");
                Say(pc, 131, "这个晒干的海鲜是『干的了鱼人』$R;" +
                    "$R想让他变回原来的样子啊$R;" +
                    "不是特殊的地方，是变不回来的$R;" +
                    "$P但是我有事情要做，分不开身呢。$R;" +
                    "$R该怎么办呢？$R;");
                幫忙(pc);
                return;
            }

            if (pc.Marionette != null)
            {
                    Say(pc, 131, "哦，来得好，同伴$R;" +
                        "…??$R;" +
                        "$P嗯，好像有什么不同呢。$R不过应该没事吧。$R;");
                    Say(pc, 11000030, 131, "应该没关系啊$R;");
                    Say(pc, 11000031, 131, "应该没关系啊$R;");
                任務(pc);
                return;
            }
            Say(pc, 131, "$R这里是我们神圣的地方，$R外人请离开。$R;");
            Say(pc, 11000030, 131, "外人请离开$R;");
            Say(pc, 11000031, 131, "不要到这里。$R;");
        }

        void 任務(ActorPC pc)
        {
            BitMask<OMASI> omasi_mask = pc.CMask["OMASI"];
            switch (Select(pc, "做什么呢？", "", "任务服务台", "什么也不做"))
            {
                case 1:
                    if (!omasi_mask.Test(OMASI.第一次对话))
                    {
                        Say(pc, 131, "成为我们的力量吧。$R;");
                        Say(pc, 11000030, 131, "需要您的力量$R;");
                        Say(pc, 11000031, 131, "需要您的帮助$R;");
                        Say(pc, 131, "$R在我们的嘱托里还有很特殊的。$R;");
                        Say(pc, 11000030, 131, "还包括比较难的事情。$R;");
                        Say(pc, 11000031, 131, "简单的也有$R;");
                        Say(pc, 131, "我们相信您$R;");
                        Say(pc, 11000030, 131, "因为相信您，才拜托您的$R;");
                        Say(pc, 11000031, 131, "因为信赖您，才拜托您的$R;");
                        omasi_mask.SetValue(OMASI.第一次对话, true);
                        任務(pc);
                        return;
                    }
                    HandleQuest(pc, 47);
                    break;
                case 2:
                    Say(pc, 131, "不管什么时候都过来吧，$R我会等您的。$R;");
                    Say(pc, 11000030, 131, "再来啦$R;");
                    Say(pc, 11000031, 131, "再来玩啊。$R;");
                    break;
            }
        }

        void 幫忙(ActorPC pc)
        {
            BitMask<Puppet_Fish> Puppet_Fish_mask = pc.CMask["Puppet_Fish"];
            Say(pc, 11000030, 131, "真是，难道我们什么也做不了吗？$R;");
            Say(pc, 11000031, 131, "什么也做不了吗？$R;");
            Say(pc, 0, 131, "用哀怨的眼睛看著我。。$R;" +
                "$R帮忙吗？$R;", " ");
            switch (Select(pc, "帮忙吗？", "", "帮忙", "不帮忙"))
            {
                case 1:
                    Say(pc, 131, "就等您那句话了。$R;" +
                        "$P在大六的洞窟深处，有我们伙伴$R『鱼人』生活的隐秘的村庄。$R;" +
                        "$R在那里的长老就能把$R『晒干的海鲜』变回原样了。$R;" +
                        "$P入口很隐秘，可能不好找，$R但是希望您尽力啊。$R;");
                    Say(pc, 11000030, 131, "一定要尽力找啊$R;");
                    Say(pc, 11000031, 131, "一定要帮我找到啦！$R;");
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