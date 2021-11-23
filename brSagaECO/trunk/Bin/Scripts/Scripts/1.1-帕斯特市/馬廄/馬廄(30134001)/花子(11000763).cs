using System;
using System.Collections.Generic;
using System.Text;
using SagaLib;
using SagaDB.Actor;
using SagaMap.Scripting;
using SagaScript.Chinese.Enums;
namespace SagaScript.M30134001
{
    public class S11000763 : Event
    {
        public S11000763()
        {
            this.EventID = 11000763;
        }

        public override void OnEvent(ActorPC pc)
        {
            BitMask<PSTFlags> mask = pc.CMask["PST"];
            if (mask.Test(PSTFlags.獲得牛牛))//_5a80)
            {
                Say(pc, 364, "哞嗚！$R;");
                Say(pc, 11000762, 131, "小花今天也看起來很健康喔$R;");
                return;
            }
            if (mask.Test(PSTFlags.獲得牛牛的對話))//_5a79)
            {
                Say(pc, 331, "哞嗚~哞嗚嗚~$R;");
                Say(pc, 11000762, 131, "小花慌忙地動來動去$R;");
                return;
            }
            if (mask.Test(PSTFlags.給予甜草))//_5a78)
            {
                Say(pc, 331, "哞嗚！$R;");
                Say(pc, 11000762, 131, "小花慌忙地動來動去$R;");
                return;
            }
            if (mask.Test(PSTFlags.尋找甜草))//_5a77)
            {
                Say(pc, 131, "…$R;");
                Say(pc, 11000762, 131, "小花溫順得有點怪呢$R;");
                return;
            }
            if (mask.Test(PSTFlags.給予香草))//_5a76)
            {
                Say(pc, 131, "…$R;");
                Say(pc, 11000762, 131, "小花安靜的待著$R;");
                return;
            }
            if (mask.Test(PSTFlags.尋找香草))//_5a75)
            {
                Say(pc, 364, "哞嗚！$R;");
                Say(pc, 11000762, 131, "小花看起來很健康喔$R;");
                return;
            }
            if (mask.Test(PSTFlags.給予健康營養飲料))//_5a74)
            {
                Say(pc, 364, "哞嗚！$R;");
                Say(pc, 11000762, 131, "小花看起來很健康喔$R;");
                return;
            }
            Say(pc, 364, "哞嗚！$R;");
            Say(pc, 11000762, 131, "健康的牛牛$R;");
        }
    }
}