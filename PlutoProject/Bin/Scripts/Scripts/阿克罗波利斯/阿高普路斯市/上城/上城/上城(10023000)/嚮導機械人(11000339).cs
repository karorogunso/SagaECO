using System;
using System.Collections.Generic;
using System.Text;

using SagaDB.Actor;
using SagaLib;
using SagaDB.Item;
using SagaMap.Scripting;
using SagaScript.Chinese.Enums;
//所在地圖:上城(10023000) NPC基本信息:嚮導機械人(11000339) X:132 Y:95
namespace SagaScript.M10023000
{
    public class S11000339 : Event
    {
        public S11000339()
        {
            this.EventID = 11000339;
        }

        public override void OnEvent(ActorPC pc)
        {
            BitMask<Neko_04> Neko_04_amask = pc.AMask["Neko_04"];
            BitMask<Neko_04> Neko_04_cmask = pc.CMask["Neko_04"];
            if (Neko_04_amask.Test(Neko_04.任務開始) &&
                !Neko_04_amask.Test(Neko_04.任務結束) &&
                Neko_04_cmask.Test(Neko_04.被告知去找機器人) &&
                Neko_04_cmask.Test(Neko_04.被告知犯人是小孩) &&
                !Neko_04_cmask.Test(Neko_04.被告知未見過小孩) &&
                pc.Inventory.Equipments.ContainsKey(EnumEquipSlot.PET))
            {
                if (pc.Inventory.Equipments[EnumEquipSlot.PET].ItemID == 10017900)
                {
                    Neko_04_cmask.SetValue(Neko_04.被告知未見過小孩, true);
                    Say(pc, 11000339, 131, "小孩吗?$R;" +
                        "$R…不!没见过啊$R;" +
                        "$P在我记忆中，出入行会宫殿的人里$R没有小孩喔!$R;");
                    Say(pc, 0, 131, "…那么，那孩子$R还在这个建筑物里吗?$R;" +
                        "$R明明在这里某处的!$R;", "\"猫灵（山吹）\"");
                    Say(pc, 0, 131, "好像是喔!$R;" +
                        "$R这建筑物…行会…哎！是什么呢？$R;" +
                        "除了主人的工作室还有很多房间呢！$R;" +
                        "$R有人要把那坏孩子藏起来吗?$R;" +
                        "$R好！$R不管怎么样!$R仔细的去每个房间找找看吧$R;", "\"猫灵（桃子）\"");
                    Say(pc, 0, 131, "桃子!干嘛那么开心啊$R主人不是因为寄存的东西被偷了$R还在烦嘛!$R;" +
                        "$R认真行动吧!$R;", "\"猫灵（山吹）\"");
                    Say(pc, 0, 131, "啊!对不起!$R;" +
                        "$R好像很有趣…所以…$R;", "\"猫灵（桃子）\"");
                    Say(pc, 0, 131, "有趣？？$R;", "\"猫灵（山吹）\"");
                    Say(pc, 0, 131, "嗯~$R我们不是在跟主人冒险嘛!$R;" +
                        "$R我一直都觉得很幸福^^$R好开心喔$R;", "\"猫灵（桃子）\"");
                    Say(pc, 0, 131, "是吗?$R;", "\"猫灵（山吹）\"");
                    Say(pc, 0, 131, "嗯…?$R绿怎么了…?$R;", "\"猫灵（桃子）\"");
                    return;
                }
            }
            if (Neko_04_amask.Test(Neko_04.任務開始) &&
                !Neko_04_amask.Test(Neko_04.任務結束) &&
                Neko_04_cmask.Test(Neko_04.被告知去找機器人) &&
                !Neko_04_cmask.Test(Neko_04.被告知犯人是小孩) &&
                pc.Inventory.Equipments.ContainsKey(EnumEquipSlot.PET))
            {
                if (pc.Inventory.Equipments[EnumEquipSlot.PET].ItemID == 10017900)
                {
                    Say(pc, 11000339, 131, "嗯？$R有没有见过可疑的人？$R;" +
                        "$P没有…没有可疑人!$R;" +
                        "$P我是要给大家带路，$R;" +
                        "所以经常在这里出入$R拍照后，会暂时储存在记忆体内$R;" +
                        "$P最起码昨天、今天这两天内$R没有见过可疑人物喔$R;" +
                        "$R没错的!!$R;");
                    Say(pc, 0, 131, "那个…$R是不是应该告诉主人啊?$R;" +
                        "$R我说的是犯人啊$R;", "\"猫灵（山吹）\"");
                    Say(pc, 0, 131, "虽然说是那样$R可是我们说的…话…$R主人不是听不懂嘛$R;", "\"猫灵（桃子）\"");
                    Say(pc, 0, 131, "那个嘛，是因为桃子对主人的爱$R不够才那样的$R;", "\"猫灵（山吹）\"");
                    Say(pc, 0, 131, "是吗…不是，不是的!$R;" +
                        "$R没可能的$R;", "\"猫灵（桃子）\"");
                    return;
                }
            }
            NavigateCancel(pc);
            /*
            if (!_1A15)
            {
                Say(pc, 11000339, 131, "欢迎来到阿克罗波利斯$R;" +
                    "我是向导机器人$R;");
            }//*/
            Say(pc, 11000339, 131, "这里是『行会宫殿』$R;" +
                "$R在这里，您可以转职各式各样的$R;" +
                "职业，也可以承接任务$R;");
            Say(pc, 11000338, 131, "要带您去其他地方吗?$R;");
            /*
            if (_1A15)
            {
                switch (Select(pc, "“要继续委托带路吗?", "", "请继续带我到其他地方", "放弃"))
                {
                    case 1:
                        break;
                    case 2:
                        Say(pc, 11000339, 131, "真可惜啊$R;");
                        _1A15 = false;
                        return;
                }
            }//*/
            switch (Select(pc, "请选择想去的地方", "", "白之圣堂", "黑之圣堂", "裁缝阿姨的家", "宝石商店", "放弃"))
            {
                case 1:
                    Say(pc, 11000339, 131, "跟着箭头方向走$R;" +
                        "它会带您去『白之圣堂』的$R;");
                    Navigate(pc, 160, 130);
                    //_1A15 = true;
                    break;
                case 2:
                    Say(pc, 11000339, 131, "跟着箭头方向走$R;" +
                        "它会带您去『黑之圣堂』的$R;");
                    Navigate(pc, 95, 130);
                    //_1A15 = true;
                    break;
                case 3:
                    Say(pc, 11000339, 131, "跟着箭头方向走$R;" +
                        "它会带您去『裁缝阿姨的家』的$R;");
                    Navigate(pc, 89, 97);
                    //_1A15 = true;
                    break;
                case 4:
                    Say(pc, 11000339, 131, "跟着箭头方向走$R;" +
                        "它会带您去『宝石商店』的$R;");
                    Navigate(pc, 150, 97);
                    //_1A15 = true;
                    break;
                case 5:
                    Say(pc, 11000339, 131, "真可惜啊$R;");
                    //_1A15 = false;
                    break;
            }
        }
    }
}