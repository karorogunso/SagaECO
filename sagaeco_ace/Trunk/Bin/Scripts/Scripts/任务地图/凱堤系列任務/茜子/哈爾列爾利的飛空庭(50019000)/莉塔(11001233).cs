using System;
using System.Collections.Generic;
using System.Text;

using SagaDB.Actor;
using SagaDB.Item;
using SagaMap.Scripting;

using SagaLib;
using SagaScript.Chinese.Enums;
namespace SagaScript.M50019000
{
    public class S11001233 : Event
    {
        public S11001233()
        {
            this.EventID = 11001233;
        }

        public override void OnEvent(ActorPC pc)
        {
            BitMask<Neko_05> Neko_05_amask = pc.AMask["Neko_05"];
            BitMask<Neko_05> Neko_05_cmask = pc.CMask["Neko_05"];

            //MUSIC 1081 0 0 100
            if (Neko_05_amask.Test(Neko_05.茜子任务开始) &&
                Neko_05_cmask.Test(Neko_05.飛空庭完成) &&
                !Neko_05_cmask.Test(Neko_05.得到茜子))
            {
                pc.CInt["Neko_05_Map_06"] = CreateMapInstance(50016000, 10062000, 110, 87);
                Warp(pc, (uint)pc.CInt["Neko_05_Map_06"], 8, 4);
                return;
            }
            if (Neko_05_amask.Test(Neko_05.茜子任务开始) &&
                Neko_05_cmask.Test(Neko_05.把電腦唯讀記憶體給哈爾列爾利) &&
                !Neko_05_cmask.Test(Neko_05.飛空庭完成))
            {
                Neko_05_cmask.SetValue(Neko_05.飛空庭完成, true);
                Say(pc, 11001229, 131, "用「客人」给我的『电脑只读记忆体』$R终于把我的飞空庭完成了♪$R;" +
                    "$R真是全都多亏「客人」啊！$R;");
                Say(pc, 11001233, 131, "哈利路亚……$R;");
                Say(pc, 11001229, 131, "嗯?妈妈?怎么了?$R;" +
                    "$R不是说有什么重要的话吗！？$R;");
                Say(pc, 11001233, 131, "……$R;" +
                    "$P哈利路亚…$R;" +
                    "$R你的生命…快要…$R;");
                Say(pc, 11001233, 131, "人工制造的石像…生命非常短暂……$R;" +
                    "$R像我这样的木偶制作大师$R虽然付出了无数的努力。$R但关于石像寿命，还是没法克服啊…$R;" +
                    "$P所以不要再制造飞空庭了多陪妈妈吧$R;" +
                    "$R妈妈我会努力的$R让我们哈利路亚多活一天$R;");
                Say(pc, 0, 131, "……！！$R;", "“猫灵（茜子）”");
                Say(pc, 11001229, 131, "……$R;" +
                    "$R其实我也感觉到了$R;" +
                    "$R最近妈妈特别担心我的身体状况…$R;" +
                    "$R所以我想快点完成我的飞空庭$R;" +
                    "$P妈妈！我的生命还有多久?$R;");
                Say(pc, 11001233, 131, "不知道！我真的不知道！泣……$R;" +
                    "$R没想到会这么快…$R只知剩下的时间没多久…$R;" +
                    "$P真的对不起！哈利路亚！$R妈妈对不起你！！$R;");
                Say(pc, 11001229, 131, "妈妈不是的…$R我可以来到这个世界已经觉得很幸福了$R;" +
                    "$R真的谢谢您把我造出来$R妈妈，谢谢！$R;");
                Say(pc, 11001233, 131, "哈利路亚…$R;" +
                    "$R我觉得…哈利路亚…$R还有茜也很可怜…$R;");
                Say(pc, 11001229, 131, "……$R;" +
                    "$P现在开始，我可以坐着飞空庭$R到世界每个角落去冒险$R这将会是漫长的旅程吧♪$R;");
                Say(pc, 0, 131, "什么…！?$R;", "猫灵（全体）");
                Say(pc, 0, 131, "等…等一下！！$R;" +
                    "$R不要任意妄为！！$R;" +
                    "$P可是…真的没办法啊$R;" +
                    "$R如果哈利路亚一定要去的话$R那只能一起去啊…$R;", "“猫灵（茜子）”");
                Say(pc, 11001229, 131, "不…我会自己去的$R;");
                Say(pc, 0, 131, "什么…?$R;", "“猫灵（茜子）”");
                Say(pc, 11001229, 131, "可能会是……很长…很长的旅行喔$R;" +
                    "$R我想去某个地方的$R「通往天空的塔」那里$R;" +
                    "$P想去与仅存的机械时代的科学和塔$R连接在一起的泰达尼亚世界那里$R;" +
                    "$R也许会有…延长我们活动木偶石像$R短暂生命的方法啊$R;" +
                    "$P那茜呢…$R;" +
                    "$R跟「客人」在一起…$R还有跟其他姐妹在一起比较好$R;");
                Say(pc, 0, 131, "……$R;" +
                    "$R为什么…?$R哈利路亚…到底……为什么?$R;", "“猫灵（茜子）”");
                Say(pc, 11001229, 131, "啊啊！茜！茜子！$R;" +
                    "$R别哭…虽然我…不能流泪$R;" +
                    "$R可是要和茜分开，也很伤心的$R;" +
                    "$P「客人」…$R;" +
                    "$R要好好照顾茜啊！拜託了！$R;" +
                    "$P妈妈！还有「客人」！$R;" +
                    "$R真的！很感谢！$R;" +
                    "$R茜…要健康啊！$R;");
                Say(pc, 0, 131, "呜呜！哈利路亚！我会等你的…$R;" +
                    "$R哈利路亚…！！！$R;", "“猫灵（茜子）”");
                Wait(pc, 1000);
                ShowEffect(pc, 0, 4023);
                ShowEffect(pc, 11001233, 4023);
                Wait(pc, 1666);
                pc.CInt["Neko_05_Map_06"] = CreateMapInstance(50016000, 10062000, 110, 87);
                Warp(pc, (uint)pc.CInt["Neko_05_Map_06"], 8, 4);
                //EVENTMAP_IN 16 1 8 4 2
                //EVENTEND
                return;
            }
            Say(pc, 11001229, 131, "我的飞空庭完成了♪$R;");
            //EVENTEND
        }
    }
}