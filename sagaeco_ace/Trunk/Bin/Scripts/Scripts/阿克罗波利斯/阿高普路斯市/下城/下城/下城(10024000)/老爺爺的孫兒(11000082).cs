using System;
using System.Collections.Generic;
using System.Text;

using SagaDB.Actor;
using SagaMap.Scripting;

using SagaLib;
using SagaScript.Chinese.Enums;
//所在地圖:下城(10024000) NPC基本信息:老爺爺的孫兒(11000082) X:51 Y:130
namespace SagaScript.M10024000
{
    public class S11000082 : Event
    {
        public S11000082()
        {
            this.EventID = 11000082;
        }

        public override void OnEvent(ActorPC pc)
        {
            BitMask<JobBasic_12> JobBasic_12_mask = new BitMask<JobBasic_12>(pc.CMask["JobBasic_12"]);

            if (JobBasic_12_mask.Test(JobBasic_12.給予老爺爺的錢包) &&
                !JobBasic_12_mask.Test(JobBasic_12.給予老爺爺的手帕))
            {
                商人轉職任務(pc);
                return;
            }

            Say(pc, 11000082, 181, "啊…能去玩得话就好了。$R;", "老爷爷的孙子");
        }

        void 商人轉職任務(ActorPC pc)
        {
            if (CountItem(pc, 80021300) == 0)
            {
                Say(pc, 11000082, 181, "在找老爷爷的手帕?$R;" +
                                       "$P来! 在这里。$R;" +
                                       "这个手帕转交给老爷爷吧!$R;", "老爷爷的孙子");

                PlaySound(pc, 2030, false, 100, 50);
                GiveItem(pc, 80021300, 1);
                Say(pc, 0, 0, "得到『老爷爷的手帕』!$R;", " ");
            }
            else
            {
                Say(pc, 11000082, 181, "快回去找老爷爷吧!$R;", "老爷爷的孙子");            
            }
        }
    }
}
