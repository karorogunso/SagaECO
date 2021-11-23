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

                Say(pc, 0, 0, "哎! 原來您在這裡呀!$R;" +
                              "$R對不起…$R;" +
                              "他們又從我這裡逃走了。$R;" +
                              "$P您好像從『那些傢伙』$R;" +
                              "做的次元縫裡逃到『這個時代』來。$R;" +
                              "$P詳細的情況，以後再說吧!$R;" +
                              "快逃出去!$R;" +
                              "走道盡頭就是出口!$R;" +
                              "$R『這個時代』的阿高普路斯市…$R;" +
                              "快要毀滅了…!$R;", "微微");
            }
        }
    }
}
