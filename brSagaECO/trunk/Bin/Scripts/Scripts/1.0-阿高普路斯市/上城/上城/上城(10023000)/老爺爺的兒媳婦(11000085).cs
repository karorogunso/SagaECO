using System;
using System.Collections.Generic;
using System.Text;

using SagaDB.Actor;
using SagaMap.Scripting;

using SagaLib;
using SagaScript.Chinese.Enums;
//所在地圖:上城(10023000) NPC基本信息:老爺爺的兒媳婦(11000085) X:162 Y:91
namespace SagaScript.M10023000
{
    public class S11000085 : Event
    {
        public S11000085()
        {
            this.EventID = 11000085;
        }

        public override void OnEvent(ActorPC pc)
        {
            BitMask<JobBasic_12> JobBasic_12_mask = new BitMask<JobBasic_12>(pc.CMask["JobBasic_12"]);

            if (JobBasic_12_mask.Test(JobBasic_12.給予老爺爺的眼鏡) &&
                !JobBasic_12_mask.Test(JobBasic_12.給予老爺爺的褲子))
            {
                商人轉職任務(pc);
                return;
            }

            Say(pc, 11000085, 131, "天氣真好!$R;", "老爺爺的兒媳婦");
        }

        void 商人轉職任務(ActorPC pc)
        {
            if (CountItem(pc, 80010100) == 0)
            {
                Say(pc, 11000085, 181, "啊…找我有什麼事嗎?$R;" +
                                       "$R啊? 老爺爺的褲子呢?$R;" +
                                       "已經洗得乾乾淨淨了，請轉告他。$R;", "老爺爺的兒媳婦");

                PlaySound(pc, 2030, false, 100, 50);
                GiveItem(pc, 80010100, 1);
                Say(pc, 0, 0, "得到『老爺爺的褲子』!$R;", " ");
            }
            else
            {
                Say(pc, 11000085, 181, "啊…還沒去找老爺爺嗎?$R;", "老爺爺的兒媳婦");
            }
        }
    }
}
