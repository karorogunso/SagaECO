using System;
using System.Collections.Generic;
using System.Text;

using SagaDB.Actor;
using SagaMap.Scripting;

using SagaLib;
using SagaScript.Chinese.Enums;
namespace SagaScript.M50081000
{
    public class S11002164 : Event
    {
        public S11002164()
        {
            this.EventID = 11002164;
        }

        public override void OnEvent(ActorPC pc)
        {
            BitMask<DEMNewbie> newbie = new BitMask<DEMNewbie>(pc.CMask["DEMNewbie"]);
            if (!newbie.Test(DEMNewbie.介绍造型变换))
            {
                Say(pc, 131, "那么…$R;" +
                "一边移动一下身体，一边$R;" +
                "测试你的性能吧。$R;" +
                "$P为我们DEM族斗争、$R;" +
                "必须变更「形态」$R;" +
                "$P在「人形形态」$R;" +
                "我们能装备武器以及衣服$R;" +
                "但是我们发挥不了原来的能力。$R;" +
                "$P不过假如是「战斗形态」的话、$R;" +
                "就能发挥到最大威力。$R;" +
                "$P要变更「形态」的话、$R;" +
                "右方点击自己$R;" +
                "选择「变更形态」的话就可以了。$R;" +
                "$P那就会变成$R;" +
                "「战斗形态」。$R;", "ＤＥＭ－ＮＳ４４１０");
                newbie.SetValue(DEMNewbie.介绍造型变换, true);
                return;
            }
            if (!newbie.Test(DEMNewbie.给予改造部件))
            {
                if (pc.Form != DEM_FORM.MACHINA_FORM)
                {
                    Say(pc, 131, "那个形态不是战斗形态。$R;" +
                    "$P要变更「形态」的话、$R;" +
                    "右方点击自己$R;" +
                    "选择「变更形态」。$R;", "ＤＥＭ－ＮＳ４４１０");
                    return;
                }
                else
                {
                    Say(pc, 131, "唔..没事的话倒不需要转换形态$R;" +
                    "那么就来解说一下武器转换吧。$R;" +
                    "$P啊！好像还有些武装还没送到......$R;" +
                    "$R你想$R;" +
                    "近战攻击型$R;" +
                    "远距离攻击型$R;" +
                    "哪一方面的类型好？$R;", "ＤＥＭ－ＮＳ４４１０");
                    switch (Select(pc, "哪一方面的类型好？", "", "近战攻击型(未实装)", "远距离攻撃型"))
                    {
                        case 2:
                            Say(pc, 164, "是这样吗？$R;" +
                            "那么就送你这个吧。$R;", "ＤＥＭ－ＮＳ４４１０");
                            GiveItem(pc, 85302500, 1);
                            GiveItem(pc, 81700000, 1);
                            Say(pc, 0, 65535, "ガンハンド$R;" +
                            "■SK-[ルーキーバレット]$R;" +
                            "获得了。$R;", " ");
                            Say(pc, 131, "刚刚给你的、$R;" +
                            "是被称为「手部零件」的$R;" +
                            "マシナフォーム使用装备。$R;" +
                            "$Pそして今开いているウインドウは、$R;" +
                            "以及现在开著的视窗叫作「装备视窗」。$R;" +
                            "$P在マシナフォーム、$R;" +
                            "头部,身体,手腕,腿,背部$R;" +
                            "都能换上不同的零件。$R;" +
                            "$P那么立刻把装备装上吧。$R;" +
                            "$R安装了手腕部件。$R;" +
                            "$P把要更换的组件双按两下就可以换上了$R;", "ＤＥＭ－ＮＳ４４１０");
                            DEMParts(pc);
                            break;
                    }
                    newbie.SetValue(DEMNewbie.给予改造部件, true);
                    return;
                }
            }

            if (!pc.Inventory.Parts.ContainsKey(SagaDB.Item.EnumEquipSlot.RIGHT_HAND))
            {
                Say(pc, 131, "刚刚给你的、$R;" +
                "是被称为「手部零件」$R;" +
                "的マシナフォーム使用装备。$R;" +
                "$P以及现在开着的视窗叫作、$R;" +
                "「装备视窗」。$R;" +
                "$P在マシナフォーム、$R;" +
                "头部,身体,手腕,腿,背部$R;" +
                "都能换上不同的零件。$R;" +
                "$P那么立刻把装备装上吧。$R;" +
                "$R把要更换的组件双按两下就可以换上了$R;", "ＤＥＭ－ＮＳ４４１０");
                DEMParts(pc);
                return;
            }
            else
            {
                if (!newbie.Test(DEMNewbie.已经DEMIC改造完毕))
                {
                    Say(pc, 131, "好像成功装上部件了$R;" +
                    "$P部件的变更、$R;" +
                    "因为如果安装错误的话$R;" +
                    "是不能使用的。$R;" +
                    "$P那么，进行下一个教学吧。$R;" +
                    "$P现在开着的视窗、$R;" +
                    "被称为「DEMIC」。$R;" +
                    "$P刚刚给你的是强化容量的晶片、$R;" +
                    "但是不会使你变得强大。$R;" +
                    "$P为了使实际的性能有所提升、$R;" +
                    "必须要使用「晶片」。$R;" +
                    "$P在「DEMIC」里可以设定强化晶片$R;" +
                    "以提高性能。$R;", "ＤＥＭ－ＮＳ４４１０");

                    Say(pc, 131, "对于晶片、$R;" +
                    "有提高基本状态的「状态晶片」、$R;" +
                    "有技能学习的「技能晶片」$R;" +
                    "$P以及总结全部技术整合而成的、$R;" +
                    "「极限晶片」$R;" +
                    "$P以上的三种「晶片」$R;" +
                    "在DEMIC面板可以、$R;" +
                    "自由安装这些晶片$R;" +
                    "$R能够改变成使用者喜爱的战术取向。$R;" +
                    "$P超过了强化限界的话、$R;" +
                    "那就是DEMIC栏太少的缘故$R;" +
                    "如果晶片所佔的范围较多，$R;" +
                    "$R你就要换上小一点的晶片或是进行升级、$R;" +
                    "大概是这样了。$R;" +
                    "$P还有的是$R;" +
                    "使用DEMIC晶片功能是要扣除1EP作为资源费用$R;" +
                    "这样说明的话$R;" +
                    "会较容易明白吧。$R;" +
                    "$P刚刚送给你的「强化晶片」$R;" +
                    "把它放进DEMIC吧$R;" +
                    "首先按左方下的「入替」按键。$R;" +
                    "$P然后会开放了晶片面板$R;" +
                    "然后换上你需要的晶片。$R;" +
                    "$P然后把它放到你喜爱的位置。$R;" +
                    "$R但是请注意所占的格数、$R;" +
                    "当你决定好的时候，$R;" +
                    "$P按下「确定」就完成了整个DEMIC强化过程了$R;", "ＤＥＭ－ＮＳ４４１０");

                    pc.EP++;
                    DEMIC(pc);
                    newbie.SetValue(DEMNewbie.已经DEMIC改造完毕, true);
                }
                else
                {
                    if (!newbie.Test(DEMNewbie.要求去攻击靶子))
                    {
                        Say(pc, 131, "放在那里的是测试物$R;" +
                        "试试攻击它吧$R;" +
                        "$P把想攻击的目标，$R;" +
                        "用滑鼠左键点击它一下$R;" +
                        "攻撃処理を行ってくれる。$R;" +
                        "$他就会自动攻击$R;" +
                        "另外，刚刚在DEMIC部份、$R;" +
                        "如果有装上「技能晶片」的话，还可以使用技能$R;" +
                        "当你想使用技能时$R;" +
                        "打开技能视窗，双击滑鼠，$R;" +
                        "$P部屋の隅にテスト用の素体が$R;" +
                        "指定目标就能使用了。$R;" +
                        "测试用的目标已经准备好了$R;" +
                        "$P攻击它吧、$R;" +
                        "$P当你认为足够的话、$R;", "ＤＥＭ－ＮＳ４４１０");
                        newbie.SetValue(DEMNewbie.要求去攻击靶子, true);
                    }
                    else
                    {
                        Say(pc, 131, "已经可以吗？$R;", "ＤＥＭ－ＮＳ４４１０");
                        if (Select(pc, "已经可以吗？", "", "想再试一下", "没有问题") == 2)
                        {
                            Say(pc, 131, "那么立刻$R;" +
                            "上战场实战。。$R;" +
                            "$P正好ドミニオン和我们$R;" +
                            "有正在交战的部队。。$R;" +
                            "$R就去那里混在一起和ドミニオン交战吧$R;" +
                            "$P跟来吧。$R;", "ＤＥＭ－ＮＳ４４１０");
                            int oldMap = pc.CInt["Beginner_Map"];
                            pc.CInt["Beginner_Map"] = CreateMapInstance(50082000, 10023100, 250, 132);
                            Warp(pc, (uint)pc.CInt["Beginner_Map"], 58, 47);

                            DeleteMapInstance(oldMap);
                        }
                    }
                }
            }

        }
    }
}