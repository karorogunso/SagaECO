using System;
using System.Collections.Generic;
using System.Text;

using SagaDB.Actor;
using SagaMap.Scripting;

using SagaLib;
using SagaScript.Chinese.Enums;
//所在地圖:阿高普路斯市司令室(50030000) NPC基本信息:司令官(13000162) X:6 Y:8
namespace SagaScript.M50030000
{
    public class S13000162 : Event
    {
        public S13000162()
        {
            this.EventID = 13000162;
        }

        public override void OnEvent(ActorPC pc)
        {
            BitMask<Beginner_01> Beginner_01_mask = new BitMask<Beginner_01>(pc.CMask["Beginner_01"]);

            if (!Beginner_01_mask.Test(Beginner_01.司令官下令發射主砲))
            {
                司令官下令發射主砲(pc);
                return;
            }

            Say(pc, 13000162, 131, "…!!什麼…!!$R;" +
                                   "$R那些平民到這裡來想幹什麼?$R;" +
                                   "$P已經下令戒嚴!$R;" +
                                   "讓他們立即躲避呀!$R;", "司令官");
        }

        void 司令官下令發射主砲(ActorPC pc)
        {
            BitMask<Beginner_01> Beginner_01_mask = new BitMask<Beginner_01>(pc.CMask["Beginner_01"]);

            Beginner_01_mask.SetValue(Beginner_01.司令官下令發射主砲, true); 

            Say(pc, 13000162, 131, "左邊的高射炮!$R;" +
                                   "在幹什麼! 給我好好幹阿!$R;", "司令官");

            Say(pc, 13000163, 135, "敵人的機甲要塞，緊急靠近中!$R;" +
                                   "$R攻擊輸出功率下降 30%!$R;" +
                                   "不能再抵抗了!$R;", "操作員");

            Say(pc, 13000164, 135, "敵人已經在右邊上城區域登陸了!$R;" +
                                   "好像已被突破防線了阿啊!$R;", "操作員");

            Say(pc, 13000162, 131, "預備部隊集合!!$R:" +
                                   "不論怎麼樣…$R;" +
                                   "絕對不能讓敵人侵入中央區。$R;" +
                                   "$P……好的，卸下機動模式!$R;" +
                                   "$R集合城裡的戰鬥力，，$R;" +
                                   "發射威力最大的主炮呀!$R;" +
                                   "目標是敵人的機甲要塞『瑪依瑪依』。。$R;", "司令官");

            Say(pc, 13000163, 135, "司…司令…要是…$R;" +
                                   "$R…失去動力的話，$R;" +
                                   "阿高普路斯市怎麼辦…$R;", "操作員");

            Say(pc, 13000162, 131, "反正這樣不能持續多久的，$R;" +
                                   "$R這次就跟他們決一死戰!$R;" +
                                   "一口氣消滅那幫傢伙們阿!$R;", "司令官");

            Say(pc, 13000164, 135, "知…知道了!$R;" +
                                   "進入發射主炮群!$R;", "操作員");

            Say(pc, 13000162, 131, "…!!什麼…!!$R;" +
                                   "$R那些平民到這裡來想幹什麼??$R;" +
                                   "$P已經下令戒嚴!$R;" +
                                   "讓他們立即躲避呀!$R;", "司令官");
        } 
    }
}

namespace SagaScript.路标2
{
    public class S18000192 : Event
    {
        public S18000192()
        {
            this.EventID = 18000192;
        }

        public override void OnEvent(ActorPC pc)
        {
        }
    }
}