using System;
using System.Collections.Generic;
using System.Text;

using SagaDB.Actor;
using SagaMap.Scripting;

using SagaLib;
using SagaScript.Chinese.Enums;
//所在地圖:東方海角(10018103) NPC基本信息:帕斯特國境警備員(11001416) X:235 Y:77
namespace SagaScript.M10018103
{
    public class S11001416 : Event
    {
        public S11001416()
        {
            this.EventID = 11001416;
        }

        public override void OnEvent(ActorPC pc)
        {
            BitMask<Beginner_01> Beginner_01_mask = new BitMask<Beginner_01>(pc.CMask["Beginner_01"]);

            if (!Beginner_01_mask.Test(Beginner_01.已經與埃米爾進行第一次對話))
            {
                尚未與埃米爾對話(pc);
                return;
            }

            Say(pc, 11001416, 131, "您好!$R;" +
                                   "$R这座桥连接法伊斯特岛，$R;" +
                                   "不过现在还不能通过喔!$R;" +
                                   "$P开放的法伊斯特岛的视点!$R;" +
                                   "$R宽阔的景象非常壮观!!$R;" +
                                   "想去的话，先提高实力再来吧!$R;" +
                                   "$P哎呀!$R;" +
                                   "您对这个世界还不太明白吗?$R;" +
                                   "$R这个世界由一个很大的大陆$R;" +
                                   "「阿克罗尼亚大陆」和$R;" +
                                   "周围的很多岛屿组成的!$R;" +
                                   "$P阿克罗尼亚大陆中央是「阿克罗波利斯」，$R;" +
                                   "$R先到那里增强力量吧!$R;" +
                                   "$P这里是阿克罗尼亚大陆东边，$R;" +
                                   "叫「东方海角」。$R;" +
                                   "$R在小地图视窗$R;" +
                                   "观看整个大陆的地图，$R;" +
                                   "可以马上找到现在的位置喔!$R;" +
                                   "$P小地图使用方法知道了吗?$R;" +
                                   "去找那边的多米尼翁女孩，$R;" +
                                   "她会仔细告诉您的。$R;" +
                                   "$P一边看着整个大陆的地图，$R;" +
                                   "一边听她说明吧!$R;" +
                                   "$R旁边的大桥$R;" +
                                   "是连接阿克罗尼亚大陆$R;" +
                                   "和「法伊斯特岛」的桥。$R;" +
                                   "$P阿克罗尼亚大陆的$R;" +
                                   "北方是「诺森岛」，$R;" +
                                   "$R南方是「艾恩萨乌斯火山岛」，$R;" +
                                   "$R西南方是「摩戈岛」。$R;" +
                                   "$P力量提升后，逐一去看看吧!$R;", "法伊斯特国境警备员");
        }

        void 尚未與埃米爾對話(ActorPC pc)
        {
            Say(pc, 11001416, 131, "这么快?没跟埃米尔说话吗?$R;", "法伊斯特国境警备员");
        }  
    }
}
