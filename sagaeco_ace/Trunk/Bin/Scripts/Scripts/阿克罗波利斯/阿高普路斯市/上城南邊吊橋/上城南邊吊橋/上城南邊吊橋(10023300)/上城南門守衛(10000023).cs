using System;
using System.Collections.Generic;
using System.Text;

using SagaDB.Actor;
using SagaMap.Scripting;

using SagaLib;
using SagaScript.Chinese.Enums;
//所在地圖:上城南邊吊橋(10023300) NPC基本信息:上城南門守衛(10000023) X:129 Y:223
namespace SagaScript.M10023300
{
    public class S10000023 : Event
    {
        public S10000023()
        {
            this.EventID = 10000023;
        }

        public override void OnEvent(ActorPC pc)
        {
            BitMask<Knights> Knights_mask = pc.CMask["Knights"];

            if (CountItem(pc, 10042801) >= 1)
            {
                if (pc.Gold < 100)
                {
                    if (pc.PossesionedActors.Count != 0)
                    {
                        PlaySound(pc, 2225, false, 100, 50);
                        Say(pc, 131, "咇!!!!$R;" +
                            "禁止…禁止$R;" +
                            "$R凭依状态不能通过呀$R;" +
                            "$P嗯……$R;" +
                            "这里是禁止出入地带$R;" +
                            "为了不让凭依通过，$R;" +
                            "设置了检查凭依的特殊关卡呀$R;" +
                            "$P凭依通过$R指的是一般不能通过的地方，$R;" +
                            "可以在凭依状态下通过$R;" +
                            "$这是很方便的技术啊，$R;" +
                            "不过……$R;" +
                            "$P唉……$R;" +
                            "知道了就回去吧$R;");
                        return;
                    }
                    Warp(pc, 10023000, 127, 218);
                    return;
                }
                switch (Select(pc, "欢迎来到阿克罗波利斯!", "", "进入上城", "辛苦了", "不进去"))
                {
                    case 1:
                        if (pc.PossesionedActors.Count != 0)
                        {
                            PlaySound(pc, 2225, false, 100, 50);
                            Say(pc, 131, "咇!!!!$R;" +
                                "禁止…禁止$R;" +
                                "$R凭依状态不能通过呀$R;" +
                                "$P嗯……$R;" +
                                "这裡是禁止出入地带$R;" +
                                "为了不让凭依通过，$R;" +
                                "设置了检查凭依的特殊关卡呀$R;" +
                                "$P凭依通过$R指的是一般不能通过的地方，$R;" +
                                "可以在凭依状态下通过$R;" +
                                "$这是很方便的技术啊，$R;" +
                                "不过……$R;" +
                                "$P唉……$R;" +
                                "知道了就回去吧$R;");
                            return;
                        }
                        Warp(pc, 10023000, 127, 218);
                        break;
                    case 2:
                        switch (Select(pc, "什么都知道…", "", "不让人发现，拿出了100金币", "不给"))
                        {
                            case 1:
                                pc.Gold -= 100;
                                switch (Select(pc, "走哪条通道呢？", "", "通过东门的后方走道", "通过西门的后方走道", "通过南门的后方走道", "通过北门的后方走道"))
                                {
                                    case 1:
                                        Warp(pc, 10023100, 224, 127);
                                        break;
                                    case 2:
                                        Warp(pc, 10023200, 31, 127);
                                        break;
                                    case 3:
                                        Warp(pc, 10023300, 128, 225);
                                        break;
                                    case 4:
                                        Warp(pc, 10023400, 127, 31);
                                        break;
                                }
                                break;
                            case 2:
                                break;
                        }
                        break;
                    case 3:
                        break;
                }
                return;
            }
            if (Knights_mask.Test(Knights.告知加入騎士團的方法) &&
                !Knights_mask.Test(Knights.取得上城通行證))
            {
                Say(pc, 131, "进入上城的方法是秘密喔，$R;" +
                    "不要告诉别人啊！$R;");
                return;
            }
            if (Knights_mask.Test(Knights.考慮加入騎士團) &&
                !Knights_mask.Test(Knights.告知加入騎士團的方法) && 
                !Knights_mask.Test(Knights.取得上城通行證))
            {
                Say(pc, 131, "真的想加入混成骑士团吗？$R;" +
                    "$R那当然要加入我们最强的南军了！$R;" +
                    "$P嗯…$R;" +
                    "那么告诉您得到许可证的方法吧$R;" +
                    "从这个阶段下去$R;" +
                    "就到了阿克罗波利斯下城$R;" +
                    "知道了吗？$R;" +
                    "下城中央稍微向南走$R;" +
                    "有一位上了年纪的夫人$R;" +
                    "$P她就是被誉为$R;" +
                    "下城万物博士的长老，$R;" +
                    "找她吧！她会告诉您一些事情的，$R;" +
                    "对您会有帮助的，记住不要失礼呀。$R;");
                Knights_mask.SetValue(Knights.告知加入騎士團的方法, true);
                return;
            }
            if (Knights_mask.Test(Knights.告知無法加入南軍) &&
                !Knights_mask.Test(Knights.告知團長不理你的原因))
            {
                Say(pc, 131, "长官不见您？那当然了$R;" +
                    "$R如果您很有名，也许会见您，$R可能怕您是某个国家的间谍吧$R;" +
                    "$P您想在这里提高知名度，$R就得先宣传您的名字啊，$R先从简单的开始吧$R;" +
                    "$P帮助人，或受委托处理一些任务，$R就可以提高知名度唷$R;" +
                    "$R等您有名气了，那怕只是一点，$R长官当然会接见您了$R;" +
                    "$P因为骑士团一直缺少人手$R;");
                Knights_mask.SetValue(Knights.告知團長不理你的原因, true);
                return;
            }
            if (!Knights_mask.Test(Knights.取得上城通行證) && 
                Knights_mask.Test(Knights.告知沒有通行證))
            {
                Say(pc, 131, "又是您呀，真烦人$R;" +
                    "这里只有持有阿克罗波利斯上城$R;" +
                    "市民证的人才能进入啊$R;" +
                    "$R等您得到许可证后，再来吧$R;");
                return;
            }
            if (!Knights_mask.Test(Knights.取得上城通行證))
            {
                //WINDOWOPEN 8
                Say(pc, 131, "欢迎来到世界最大的贸易城市$R;" +
                    "阿克罗波利斯$R;" +
                    "$P这条街的构造有点特别呀，$R;" +
                    "$R第一次来的人容易迷路，$R;" +
                    "给您做个简单说明吧$R;");
                switch (Select(pc, "听说明吗？", "", "放弃", "我想听"))
                {
                    case 1:
                        Say(pc, 131, "对不起，没有许可証$R;" +
                            "不能进入喔，请回去吧$R;");
                        Knights_mask.SetValue(Knights.告知沒有通行證, true);
                        break;
                    case 2:
                        Say(pc, 131, "阿克罗波利斯座落在巨大的湖上$R;" +
                            "$R东西南北4边，都有巨大的吊桥$R;" +
                            "您现在站着的地方就是南门喔$R;" +
                            "东、西、北边也有$R跟这里差不多的地方$R;" +
                            "看地图就可以知道，$R这座城市，东西南北互相对称$R;" +
                            "$P还有分为地上、地下两层喔$R;" +
                            "$R我守着的这扇门里$R;" +
                            "宽宽的街道叫上城$R;" +
                            "其地下就是下城唷$R;" +
                            "$P下城怎么走是吗？$R;" +
                            "从我的右侧或左侧的转角绕过去$R;" +
                            "就会看见一条阶梯$R;" +
                            "顺着阶梯下去就行了$R;" +
                            "$P反正没有许可证，上城是进不去的$R;" +
                            "就是说您也进不去的$R;" +
                            "$R走吧，回去吧$R;");
                        Knights_mask.SetValue(Knights.告知沒有通行證, true);
                        break;
                }
                return;
            }
            if (pc.Gold < 100)
            {
                if (pc.PossesionedActors.Count != 0)
                {
                    PlaySound(pc, 2225, false, 100, 50);
                    Say(pc, 131, "咇!!!!$R;" +
                        "禁止…禁止$R;" +
                        "$R凭依状态不能通过呀$R;" +
                        "$P嗯……$R;" +
                        "这里是禁止出入地带$R;" +
                        "为了不让凭依通过，$R;" +
                        "设置了检查凭依的特殊关卡呀$R;" +
                        "$P凭依通过$R指的是一般不能通过的地方，$R;" +
                        "可以在凭依状态下通过$R;" +
                        "$这是很方便的技术啊，$R;" +
                        "不过……$R;" +
                        "$P唉……$R;" +
                        "知道了就回去吧$R;");
                    return;
                }
                Warp(pc, 10023000, 127, 218);
                return;
            }
            switch (Select(pc, "欢迎来到阿克罗波利斯!", "", "进入上城", "辛苦了", "不进去"))
            {
                case 1:
                    if (pc.PossesionedActors.Count != 0)
                    {
                        PlaySound(pc, 2225, false, 100, 50);
                        Say(pc, 131, "咇!!!!$R;" +
                            "禁止…禁止$R;" +
                            "$R凭依状态不能通过呀$R;" +
                            "$P嗯……$R;" +
                            "这里是禁止出入地带$R;" +
                            "为了不让凭依通过，$R;" +
                            "设置了检查凭依的特殊关卡呀$R;" +
                            "$P凭依通过$R指的是一般不能通过的地方，$R;" +
                            "可以在凭依状态下通过$R;" +
                            "$这是很方便的技术啊，$R;" +
                            "不过……$R;" +
                            "$P唉……$R;" +
                            "知道了就回去吧$R;");
                        return;
                    }
                    Warp(pc, 10023000, 127, 218);
                    break;
                case 2:
                    switch (Select(pc, "什么都知道…", "", "不让人发现，拿出了100金币", "不给"))
                    {
                        case 1:
                            pc.Gold -= 100;
                            switch (Select(pc, "走哪条通道呢？", "", "通过东门的后方走道", "通过西门的后方走道", "通过南门的后方走道", "通过北门的后方走道"))
                            {
                                case 1:
                                    Warp(pc, 10023100, 224, 127);
                                    break;
                                case 2:
                                    Warp(pc, 10023200, 31, 127);
                                    break;
                                case 3:
                                    Warp(pc, 10023300, 128, 225);
                                    break;
                                case 4:
                                    Warp(pc, 10023400, 127, 31);
                                    break;
                            }
                            break;
                        case 2:
                            break;
                    }
                    break;
                case 3:
                    break;
            }
        }
    }
}
