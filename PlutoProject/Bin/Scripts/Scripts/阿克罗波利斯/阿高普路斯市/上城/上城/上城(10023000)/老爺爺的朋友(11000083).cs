using System;
using System.Collections.Generic;
using System.Text;

using SagaDB.Actor;
using SagaMap.Scripting;

using SagaLib;
using SagaScript.Chinese.Enums;
//所在地圖:上城(10023000) NPC基本信息:老爺爺的朋友(11000083) X:49 Y:176
namespace SagaScript.M10023000
{
    public class S11000083 : Event
    {
        public S11000083()
        {
            this.EventID = 11000083;
        }

        public override void OnEvent(ActorPC pc)
        {
            BitMask<JobBasic_12> JobBasic_12_mask = new BitMask<JobBasic_12>(pc.CMask["JobBasic_12"]);

            if (JobBasic_12_mask.Test(JobBasic_12.給予老爺爺的手帕) &&
                !JobBasic_12_mask.Test(JobBasic_12.給予老爺爺的魔杖))
            {
                商人轉職任務(pc);
                return;
            }

            Say(pc, 11000083, 181, "嗯…$R;" +
                                   "$P呃嗯!$R;" +
                                   "$P…$R;" +
                                   "$P(痒痒…)$R;" +
                                   "$P(痒)$R;" +
                                   "$P呃嗯…$R;" +
                                   "嗯…$R;" +
                                   "$P呃嗯…$R;" +
                                   "$P呃嗯!$R;" +
                                   "$P…$R;" +
                                   "$P(不出来啊……)$R;" +
                                   "$P(啊烦啊!)$R;" +
                                   "$P(不能出来的爽快一些?)$R;" +
                                   "$P(痒痒…)$R;" +
                                   "$P…$R;" +
                                   "$P(不出来啊?)$R;" +
                                   "$P呃嗯…嘿啾!$R;" +
                                   "$P…$R;" +
                                   "$P啊! 爽啊!$R;", "老爷爷的朋友");
        }

        void 商人轉職任務(ActorPC pc)
        {
            if (CountItem(pc, 80040900) == 0)
            {
                Say(pc, 11000083, 181, "在找老头的杖吗?$R;" +
                                       "$R如果是他的杖的话就在这里!$R;" +
                                       "你帮我转交给他吧。$R;", "老爷爷的朋友");

                PlaySound(pc, 2030, false, 100, 50);
                GiveItem(pc, 80040900, 1);
                Say(pc, 0, 0, "得到『老爷爷的魔杖』!$R;", " ");
                return;
            }
            else
            {
                Say(pc, 11000083, 131, "有事吗?$R;", "老爷爷的朋友");
            }
        }
    }
}
