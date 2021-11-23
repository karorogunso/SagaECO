using System;
using System.Collections.Generic;
using System.Text;

using SagaDB.Actor;
using SagaMap.Scripting;

using SagaLib;
using SagaScript.Chinese.Enums;
//所在地圖:上城(10023000) NPC基本信息:老爺爺的孩子(11000081) X:122 Y:167
namespace SagaScript.M10023000
{
    public class S11000081 : Event
    {
        public S11000081()
        {
            this.EventID = 11000081;
        }

        public override void OnEvent(ActorPC pc)
        {
            BitMask<JobBasic_12> JobBasic_12_mask = new BitMask<JobBasic_12>(pc.CMask["JobBasic_12"]);

            if (JobBasic_12_mask.Test(JobBasic_12.轉交商人總管的信) &&
                !JobBasic_12_mask.Test(JobBasic_12.給予老爺爺的錢包))
            {
                商人轉職任務(pc);
                return;
            }

            Say(pc, 11000081, 131, "疲勞的話，喝藥水是最好的。$R;" +
                                   "$R不像吃食物那樣花很長的時間，$R;" +
                                   "真的很方便。$R;", "老爺爺的孩子");
        }

        void 商人轉職任務(ActorPC pc)
        {
            if (CountItem(pc, 80002600) == 0)
            {
                Say(pc, 11000081, 181, "在找老爺爺的錢包嗎?$R;" +
                                       "$R是這個嗎?$R;", "老爺爺的孩子");

                PlaySound(pc, 2030, false, 100, 50);
                GiveItem(pc, 80002600, 1);
                Say(pc, 0, 0, "得到『老爺爺的錢包』!$R;", " ");
            }
            else
            {
                Say(pc, 11000081, 181, "老爺爺再找你!$R", "老爺爺的孩子");
            }
        }
    }
}
