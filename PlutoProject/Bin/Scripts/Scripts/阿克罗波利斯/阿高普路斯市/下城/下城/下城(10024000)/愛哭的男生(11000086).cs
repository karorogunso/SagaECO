using System;
using System.Collections.Generic;
using System.Text;

using SagaDB.Actor;
using SagaMap.Scripting;

using SagaLib;
using SagaScript.Chinese.Enums;
//所在地圖:下城(10024000) NPC基本信息:愛哭的男生(11000086) X:98 Y:157
namespace SagaScript.M10024000
{
    public class S11000086 : Event
    {
        public S11000086()
        {
            this.EventID = 11000086;
        }

        public override void OnEvent(ActorPC pc)
        {
            BitMask<JobBasic_12> JobBasic_12_mask = new BitMask<JobBasic_12>(pc.CMask["JobBasic_12"]);

            if (JobBasic_12_mask.Test(JobBasic_12.給予老爺爺的褲子) &&
               !JobBasic_12_mask.Test(JobBasic_12.給予老爺爺的假牙))
            {
                商人轉職任務(pc);
                return;
            }

            Say(pc, 11000086, 181, "我特别喜欢茶!$R;" +
                                   "咕嗒~ 咕嗒~…$R;" +
                                   "啊~真好喝!!$R;", "爱哭的男生");
        }

        void 商人轉職任務(ActorPC pc)
        {
            if (CountItem(pc, 80022000) == 0)
            {
                Say(pc, 11000086, 181, "你在找假牙?$R;" +
                                       "$R你说的是老爷爷的假牙吧?$R;" +
                                       "求求老爷爷不要再把假牙放在我杯里，$R;" +
                                       "请你一定要转交给他。$R;", "爱哭的男生");

                PlaySound(pc, 2030, false, 100, 50);
                GiveItem(pc, 80022000, 1);
                Say(pc, 0, 0, "得到『老爷爷的假牙』!$R;", " ");
            }
            else
            {
                Say(pc, 11000086, 181, "快回去找老爷爷吧!$R;", "爱哭的男生");
            }
        }
    }
}
