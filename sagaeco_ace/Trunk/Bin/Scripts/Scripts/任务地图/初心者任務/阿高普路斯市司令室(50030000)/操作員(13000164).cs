using System;
using System.Collections.Generic;
using System.Text;

using SagaDB.Actor;
using SagaMap.Scripting;

using SagaLib;
using SagaScript.Chinese.Enums;
//所在地圖:阿高普路斯市司令室(50030000) NPC基本信息:操作員(13000164) X:2 Y:13
namespace SagaScript.M50030000
{
    public class S13000164 : Event
    {
        public S13000164()
        {
            this.EventID = 13000164;
        }

        public override void OnEvent(ActorPC pc)
        {
            BitMask<Beginner_01> Beginner_01_mask = new BitMask<Beginner_01>(pc.CMask["Beginner_01"]);

            if (!Beginner_01_mask.Test(Beginner_01.司令官下令發射主砲))
            {
                司令官下令發射主砲(pc);
                return;
            }

            Say(pc, 13000164, 135, "出口在房间的前面!$R;" +
                                   "赶紧从这里逃出去吧! 快!$R;", "操作员");
        }

        void 司令官下令發射主砲(ActorPC pc)
        {
            BitMask<Beginner_01> Beginner_01_mask = new BitMask<Beginner_01>(pc.CMask["Beginner_01"]);

            Beginner_01_mask.SetValue(Beginner_01.司令官下令發射主砲, true);

            Say(pc, 13000162, 131, "左舷!$R;" +
                                   "在干什么! 弹幕太薄了!$R;", "司令官");

            Say(pc, 13000163, 135, "敌人的机甲要塞，紧急靠近中!$R;" +
                                   "$R攻击输出功率下降百分之三十!$R;" +
                                   "不能再抵抗了!$R;", "操作员");

            Say(pc, 13000164, 135, "敌人已经在右边上城区域登陆了!$R;" +
                                   "好像已被突破防线了啊!$R;", "操作员");

            Say(pc, 13000162, 131, "预备部队集合!!$R;" +
                                   "不论怎么样…$R;" +
                                   "绝对不能让敌人侵入中央区。$R;" +
                                   "$P……好的，卸下机动模式!$R;" +
                                   "$R集合城里所有的能量，$R;" +
                                   "发射威力最大的主炮!$R;" +
                                   "目标是敌人的机甲要塞『玛依玛依』。$R;", "司令官");

            Say(pc, 13000163, 135, "司…司令…要是…$R;" +
                                   "$R…失去动力的话，$R;" +
                                   "阿克罗波利斯怎么办…$R;", "操作员");

            Say(pc, 13000162, 131, "反正这样不能持续多久的，$R;" +
                                   "$R这次就跟他们决一死战!$R;" +
                                   "一口气消灭那帮混蛋!$R;", "司令官");

            Say(pc, 13000164, 135, "知…知道了!$R;" +
                                   "进入发射主炮程序!$R;", "操作员");

            Say(pc, 13000162, 131, "…!!什么…!!$R;" +
                                   "$R那些平民到这里来想干什么?$R;" +
                                   "$P已经下令戒严!$R;" +
                                   "让他们立即避难呀!$R;", "司令官");
        } 
    }
}
