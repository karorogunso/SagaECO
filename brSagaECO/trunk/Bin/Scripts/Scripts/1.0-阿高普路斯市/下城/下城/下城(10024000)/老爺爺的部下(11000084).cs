using System;
using System.Collections.Generic;
using System.Text;

using SagaDB.Actor;
using SagaMap.Scripting;

using SagaLib;
using SagaScript.Chinese.Enums;
//所在地圖:下城(10024000) NPC基本信息:老爺爺的部下(11000084) X:169 Y:202
namespace SagaScript.M10024000
{
    public class S11000084 : Event
    {
        public S11000084()
        {
            this.EventID = 11000084;
        }

        public override void OnEvent(ActorPC pc)
        {
            BitMask<JobBasic_12> JobBasic_12_mask = new BitMask<JobBasic_12>(pc.CMask["JobBasic_12"]);

            if (JobBasic_12_mask.Test(JobBasic_12.給予老爺爺的魔杖) &&
                !JobBasic_12_mask.Test(JobBasic_12.給予老爺爺的眼鏡))
            {
                商人轉職任務(pc);
                return;
            }

            Say(pc, 11000084, 131, "…我在執行機密任務!!$R;", "老爺爺的部下");
        }

        void 商人轉職任務(ActorPC pc)
        {
            if (CountItem(pc, 80040800) == 0)
            {
                Say(pc, 11000084, 181, "有位老爺爺在找眼鏡?$R;" +
                                       "$P是不是在「咖啡館」前的老爺爺啊?$R;" +
                                       "$R如果是的話，應該就在這裡!$R;" +
                                       "請幫我轉交給老爺爺吧!$R;", "老爺爺的部下");

                PlaySound(pc, 2030, false, 100, 50);
                GiveItem(pc, 80040800, 1);
                Say(pc, 0, 0, "得到『老爺爺的眼鏡』!$R;", " ");
            }
            else
            {
                Say(pc, 11000084, 181, "快回去找老爺爺吧!$R;", "老爺爺的部下");
            }
        }
    }
}
