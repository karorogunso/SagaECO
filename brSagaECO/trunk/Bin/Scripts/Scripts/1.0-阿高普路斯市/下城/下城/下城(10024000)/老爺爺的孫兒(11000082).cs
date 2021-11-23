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

            Say(pc, 11000082, 181, "啊…能去玩得話就好了。$R;", "老爺爺的孫兒");
        }

        void 商人轉職任務(ActorPC pc)
        {
            if (CountItem(pc, 80021300) == 0)
            {
                Say(pc, 11000082, 181, "在找老爺爺的手帕?$R;" +
                                       "$P來! 在這裡。$R;" +
                                       "這個手帕轉交給老爺爺吧!$R;", "老爺爺的孫兒");

                PlaySound(pc, 2030, false, 100, 50);
                GiveItem(pc, 80021300, 1);
                Say(pc, 0, 0, "得到『老爺爺的手帕』!$R;", " ");
            }
            else
            {
                Say(pc, 11000082, 181, "快回去找老爺爺吧!$R;", "老爺爺的孫兒");            
            }
        }
    }
}
