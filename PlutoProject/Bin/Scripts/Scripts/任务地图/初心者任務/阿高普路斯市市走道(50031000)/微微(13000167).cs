using System;
using System.Collections.Generic;
using System.Text;

using SagaDB.Actor;
using SagaMap.Scripting;

using SagaLib;
using SagaScript.Chinese.Enums;
//所在地圖:阿高普路斯市市走道(50031000) NPC基本信息:微微(13000167) X:11 Y:19
namespace SagaScript.M50031000
{
    public class S13000167 : Event
    {
        public S13000167()
        {
            this.EventID = 13000167;
        }

        public override void OnEvent(ActorPC pc)
        {
            BitMask<Beginner_01> Beginner_01_mask = new BitMask<Beginner_01>(pc.CMask["Beginner_01"]);

            if (!Beginner_01_mask.Test(Beginner_01.再次遇上微微))
            {
                Beginner_01_mask.SetValue(Beginner_01.再次遇上微微, true);

                Say(pc, 0, 0, "哎! 原来您在这里呀!$R;" +
                              "$R对不起…$R;" +
                              "他们又从我这里逃走了。$R;" +
                              "$P您好像从『他们』$R;" +
                              "做的次元缝里落到『这个时代』来。$R;" +
                              "$P详细的情况，以后再说吧!$R;" +
                              "快逃出去!$R;" +
                              "走道尽头就是出口!$R;" +
                              "$R『这个时代』的阿克罗波利斯…$R;" +
                              "快要毁灭了…!$R;", "蒂塔");
            }
        }
    }
}
