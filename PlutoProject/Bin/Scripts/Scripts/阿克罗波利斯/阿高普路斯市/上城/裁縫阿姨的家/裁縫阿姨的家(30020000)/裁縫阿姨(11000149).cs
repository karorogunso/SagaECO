using System;
using System.Collections.Generic;
using System.Text;

using SagaDB.Actor;
using SagaMap.Scripting;

using SagaLib;
using SagaScript.Chinese.Enums;

using SagaDB.Item;
//所在地圖:裁縫阿姨的家(30020000) NPC基本信息:裁縫阿姨(11000149) X:3 Y:1
namespace SagaScript.M30020000
{
    public class S11000149 : Event
    {
        public S11000149()
        {
            this.EventID = 11000149;
        }

        public override void OnEvent(ActorPC pc)
        {
            BitMask<Neko_01> Neko_01_cmask = pc.CMask["Neko_01"];
            BitMask<Neko_01> Neko_01_amask = pc.AMask["Neko_01"];
            BitMask<Neko_02> Neko_02_amask = pc.AMask["Neko_02"];
            BitMask<Neko_02> Neko_02_cmask = pc.CMask["Neko_02"];
            BitMask<Neko_04> Neko_04_amask = pc.AMask["Neko_04"];
            BitMask<Neko_04> Neko_04_cmask = pc.CMask["Neko_04"];


            if (Neko_04_amask.Test(Neko_04.任務開始) &&
                !Neko_04_amask.Test(Neko_04.任務結束) &&
                Neko_04_cmask.Test(Neko_04.被詢問犯人的事) &&
                !Neko_04_cmask.Test(Neko_04.被告知犯人是小孩))
            {
                Neko_04_cmask.SetValue(Neko_04.被告知犯人是小孩, true); 
                Say(pc, 0, 131, "桃子!$R这位奶奶是谁啊?$R;", "猫灵(绿子)");
                Say(pc, 0, 131, "什么?$R绿子!什么奶奶啊!!$R;", "猫灵（桃子）");
                Say(pc, 131, "奶奶?$R叫我奶奶?$R;" +
                    "$R嗯嗯…叫奶奶!!$R;");
                Say(pc, 0, 131, "咪 咪 喵…$R;", "猫灵（桃子）");
                Say(pc, 131, "那个就那样了$R…你又被猫灵缠着了?$R;" +
                    "$R是人太好还是太傻啊$R;");
                Say(pc, 0, 131, "还是…那样啊(叹息)$R;");
                Say(pc, 0, 131, "咪咪!咪咪喵!喵!$R;" +
                    "$R咪咪!咪咪喵!喵!$R;");
                Say(pc, 131, "嗯嗯!这样啊!$R;" +
                    "$R…虽然不知道是什么意思$R但好像是在说「犯人是小孩!」$R;" +
                    "$P…到底什么意思啊?$R;");
                Say(pc, 0, 131, "犯人是小孩…??$R;");
                return;
            }
            //*/
            if (Neko_02_cmask.Test(Neko_02.藍任務失敗))
            {
                Neko_02_cmask.SetValue(Neko_02.藍任務失敗, false);
                Say(pc, 131, "…快把「原型」停止，小猫的灵魂…$R真可怜啊$R;" +
                    "$R不要那么伤心了，不是你的错啊$R;" +
                    "$R你只是做了一件$R不管是谁一定要做的事情而已$R;" +
                    "$R那猫灵也总有一天会理解的$R;");
                return;
            }
            if (!Neko_02_amask.Test(Neko_02.藍任務結束) &&
                Neko_02_cmask.Test(Neko_02.獲知原始的事情) &&
                !Neko_02_cmask.Test(Neko_02.得到藍) &&
                !Neko_02_cmask.Test(Neko_02.得到三角巾) &&
                CountItem(pc, 10043700) >= 1 &&
                CountItem(pc, 10021300) >= 1 &&
                CountItem(pc, 10019800) >= 1)
            {
                switch (Select(pc, "要不要委托制作『裁缝阿姨的三角巾』", "", "委托", "放弃"))
                {
                    case 1:
                        Neko_02_cmask.SetValue(Neko_02.得到三角巾, true);
                        TakeItem(pc, 10043700, 1);
                        TakeItem(pc, 10021300, 1);
                        TakeItem(pc, 10019800, 1);
                        GiveItem(pc, 10017904, 1);
                        Say(pc, 131, "得到『裁缝阿姨的三角巾』$R;");
                        Say(pc, 131, "可以的话，快阻止「原型」的复活吧$R;");
                        break;
                    case 2:
                        break;
                }
                return;
            }
            if (!Neko_02_amask.Test(Neko_02.藍任務結束) &&
                Neko_02_cmask.Test(Neko_02.獲知原始的事情) &&
                !Neko_02_cmask.Test(Neko_02.得到藍))
            {
                Say(pc, 131, "可以的话，快阻止「原型」的复活吧$R;");
                return;
            }
            if (!Neko_02_amask.Test(Neko_02.藍任務結束) &&
                Neko_02_cmask.Test(Neko_02.聽取建議) &&
                !Neko_02_cmask.Test(Neko_02.獲知原始的事情))
            {

                if (pc.Inventory.Equipments.ContainsKey(EnumEquipSlot.PET))
                {
                    if (pc.Inventory.Equipments[EnumEquipSlot.PET].ItemID == 10017900 ||
                        pc.Inventory.Equipments[EnumEquipSlot.PET].ItemID == 10017902)
                    {
                        藍(pc);
                        return;
                    }
                }
            }
            if (!Neko_02_amask.Test(Neko_02.藍任務結束) &&
                Neko_02_cmask.Test(Neko_02.與裁縫阿姨第一次對話) &&
                !Neko_02_cmask.Test(Neko_02.得知維修方法))
            {
                Say(pc, 131, "修理故障的活动木偶的话$R猫灵也会离开活动木偶回来的$R;" +
                    "$R如果是唐卡人说不定会告诉您$R修理活动木偶的方法$R;" +
                    "$R工匠会打扮成道具鉴定师的模样$R;" +
                    "不知道会不会在阿克罗波利斯~$R;");
                判斷(pc);
                return;
            }
            if (!Neko_02_amask.Test(Neko_02.藍任務結束) &&
                Neko_02_cmask.Test(Neko_02.藍任務開始) && 
                !Neko_02_cmask.Test(Neko_02.與裁縫阿姨第一次對話))
            {

                if (pc.Inventory.Equipments.ContainsKey(EnumEquipSlot.PET))
                {
                    if (pc.Inventory.Equipments[EnumEquipSlot.PET].ItemID == 10017900 ||
                        pc.Inventory.Equipments[EnumEquipSlot.PET].ItemID == 10017902)
                    {
                        Neko_02_cmask.SetValue(Neko_02.與裁縫阿姨第一次對話, true);
                        Say(pc, 131, "哎哟！猫灵好吵啊！$R;" +
                            "$R怎么了?$R;");
                        Say(pc, 0, 131, "喵!!咪咪!!喵…$R;", "猫灵(桃子)");
                        Say(pc, 131, "真是真是!$R;" +
                            "是吗?这样啊?$R不论怎样我都会给您做啊$R;");
                        Say(pc, 131, "…??$R;");
                        Say(pc, 131, "你的猫灵好像发现了朋友$R;" +
                            "$P可能把出故障的活动木偶$R当做了主人$P凭依的状态下怎样都无法脱离啊$R;");
                        Say(pc, 0, 131, "喵~~~$R;", "猫灵(桃子)");
                        Say(pc, 131, "修理故障的活动木偶的话$R猫灵也会离开活动木偶回来的$R;" +
                            "$R如果食堂卡人说不定会告诉您$R修理活动木偶的方法$R;" +
                            "$R唐卡是活动木偶和飞空庭的$R最大的生产国$R;" +
                            "$R可是真的奇怪啊…$R为什么“猫灵”$R会在没有生命的活动木偶上…$R;" +
                            "$R难道…?不是…不会是的$R;");
                        Say(pc, 131, "…?$R;");
                        return;
                    }
                }
            }
            


            if (!Neko_01_amask.Test(Neko_01.桃子任務完成) &&
                Neko_01_cmask.Test(Neko_01.再次與祭祀對話) &&
                !Neko_01_cmask.Test(Neko_01.得到裁縫阿姨的三角巾))
            {
                Say(pc, 131, "哎呀!$R好可爱的宠物啊!$R;" +
                    "$P坐在肩膀上…$R好…好…心情好像很好啊$R;");
                Say(pc, 0, 131, "咪~嗷$R;", "");
                Say(pc, 131, "…??$R;");
                if (!Neko_01_amask.Test(Neko_01.桃子任務完成) &&
                    Neko_01_cmask.Test(Neko_01.再次與祭祀對話) &&
                    !Neko_01_cmask.Test(Neko_01.得到裁縫阿姨的三角巾) &&
                    CountItem(pc, 10043700) >= 1 &&
                    CountItem(pc, 10021300) >= 1 &&
                    CountItem(pc, 10019800) >= 1)
                {
                    Say(pc, 131, "哎呀!$R;" +
                        "那『棉缎带』还有『布』和『线』！$R;" +
                        "$P只要集齐这三样$R就可以给那家伙制作漂亮的礼物了$R;" +
                        "$R怎么样?要制作吗?$R;");
                    switch (Select(pc, "要制作吗？", "", "要", "不要"))
                    {
                        case 1:
                            Neko_01_cmask.SetValue(Neko_01.得到裁縫阿姨的三角巾, true);
                            TakeItem(pc, 10043700, 1);
                            TakeItem(pc, 10021300, 1);
                            TakeItem(pc, 10019800, 1);
                            GiveItem(pc, 10017904, 1);
                            Say(pc, 131, "得到『裁缝阿姨的三角巾』$R;");
                            Say(pc, 0, 131, "咪-咪-喵$R;");
                            Say(pc, 131, "好可爱啊…看来心情很好啊$R太好了$R;" +
                                "$P哎呀！你的小猫不见了？$R真是，怎么搞的?$R;" +
                                "$R这么可爱…$R;" +
                                "$R…对了!$R我奶奶说小猫喜欢「温暖的光」$R;" +
                                "$P 给那小猫温暖的光的话$R说不定能看到它的样子呢？$R怎么样?$R;");
                            Say(pc, 0, 131, "喵~$R;");
                            break;
                        case 2:
                            Say(pc, 131, "真是…没办法啊…$R下次再来吧$R;");
                            break;
                    }
                    return;
                }
                Say(pc, 131, "真的可惜啊...$R只要有『棉缎带』还有『布』和『线』，$R;"+
			     "就可以给它制作漂亮的礼物呢…$R;");
                return;
            }
            /*
            if (!_0b12)
            {
                判斷(pc);
                return;
            }
            */
            判斷(pc);
        }

        void 判斷(ActorPC pc)
        {
            BitMask<Puppet_02> Puppet_02_mask = pc.CMask["Puppet_02"];
            if (Puppet_02_mask.Test(Puppet_02.要求製作泰迪))
            {
                木偶泰迪(pc);
                return;
            }
            if (CountItem(pc, 10020208) >= 1)
            {
                Say(pc, 131, "呃?你拿着的那个东西$R;" +
                    "好像是『缝制玩偶的布』啊$R;" +
                    "$R…原来是这样啊$R;" +
                    "$P这是能动的玩偶$R;" +
                    "『活动木偶泰迪』的材料!$R;" +
                    "$R只要你愿意我可以给你制作喔$R;");
                switch (Select(pc, "要不要请她帮忙呢?", "", "要", "不要"))
                {
                    case 1:
                        Say(pc, 131, "不要着急啦，还差几样材料$R;" +
                            "$R一个是『棉花』还有一个$R;" +
                            "是『阿拉克尼的丝』$R;" +
                            "最后是3根『针』$R;" +
                            "$P但是那针不是一般的针$R;" +
                            "$P是需要得到裁缝之神守护的$R;" +
                            "3根特殊针$R;" +
                            "$P用什么办法可以弄到那种针?$R;" +
                            "$R那个…我不太清楚$R;" +
                            "我从来不外出的$R;" +
                            "$P无论如何找一找试试看吧$R;" +
                            "我会在这里等您的$R;");
                        Puppet_02_mask.SetValue(Puppet_02.要求製作泰迪, true);
                        break;
                    case 2:
                        普通販賣(pc);
                        break;
                }
                return;
            }
            普通販賣(pc);
        }

        void 普通販賣(ActorPC pc)
        {
            switch (Select(pc, "想做什么呢?", "", "买男性服装", "买女性服装", "卖东西", "委托裁缝", "委托烹饪", "什么都不做"))
            {
                case 1:
                    OpenShopBuy(pc, 19);
                    Say(pc, 131, "再来玩吧$R;");
                    break;
                case 2:
                    OpenShopBuy(pc, 20);
                    Say(pc, 131, "再来玩吧$R;");
                    break;
                case 3:
                    OpenShopSell(pc, 20);
                    Say(pc, 131, "再来玩吧$R;");
                    break;
                case 4:
                    Synthese(pc, 2054, 5);
                    break;
                case 5:
                    Synthese(pc, 2040, 5);
                    break;
                case 6:
                    break;
            }
        }

        void 藍(ActorPC pc)
        {
            BitMask<Neko_02> Neko_02_cmask = pc.CMask["Neko_02"];

            Neko_02_cmask.SetValue(Neko_02.獲知原始的事情, true);
            Say(pc, 131, "还是那样了!?$R;" +
                "$R真是大事啊…!$R;");
            Say(pc, 0, 131, "…!?$R;", " ");
            Say(pc, 131, "平复一下心情后好好听啊$R;" +
                "$R那活动木偶塔依$R不是你认识的活动木偶$R;" +
                "$P那塔依是「原型」$R;");
            Say(pc, 0, 131, "…原型…?$R;", "猫灵(桃子)");
            Say(pc, 131, "是啊~$R;" +
                "$P活动木偶塔依$R是根据机械文明时代的设计图$R在身上贴上洋铁后制作而成的$R自动战斗兵器的复制品$R;" +
                "$P以现在的技术$R无法发挥它原来的性能$R所以只能当做活动木偶来用$R;");
            Say(pc, 131, "也可以说是在塔依中$R真的难得拥有「原来的性能」的$R;" +
                "$P把那个叫「原型」$R拥有人工智能的「原型」$R听说自己一个也可以瞬间$R把一个村落毁掉$R;" +
                "$P万一「原型」出现的话$R技术人员会把那个活动木偶停止$R之后立刻销毁$R;" +
                "$P万一以前的「破坏命令」$R还残留在电子头脑里的话$R就大事不妙了$R;");
            Say(pc, 0, 131, "…$R;", " ");
            Say(pc, 131, "要停止「原型」启动啊…$R;" +
                "$R现在开始告诉你停止塔依的方法$R;" +
                "$R塔依的背下面有个修理箱$R;" +
                "$R把它打开，里面有黄色插头$R把那个拔掉就可以了$R;" +
                "$P那样的话「原型」会$R马上停止活动的$R;" +
                "$P「原型」自己恢复的话$R那时候就没办法了$R;" +
                "$R可以的话尽快阻止「原型」复活啊$R;");

            if (CountItem(pc, 10017902) >= 1)
            {
                if (pc.Inventory.Equipments.ContainsKey(EnumEquipSlot.PET))
                {
                    if (pc.Inventory.Equipments[EnumEquipSlot.PET].ItemID == 10017900)
                    {
                        Say(pc, 0, 131, "稍等!!$R;", "猫灵(桃子)");
                        Say(pc, 131, "?…$R是粉紅色的猫灵…$R这真是没办法…$R;");
                        Say(pc, 0, 131, "不过…$R那样的话…蓝…也会消失啊!!$R;" +
                            "$P那可不行!!$R;", "猫灵(桃子)");
                        Say(pc, 131, "…$R;");
                        Say(pc, 0, 131, "…救救蓝$R;", "猫灵(绿子)");
                        Say(pc, 0, 131, "绿子!?$R;", "猫灵(桃子)");
                        Say(pc, 0, 131, "…求求您$R朋友们都在战争中死掉了$R;" +
                            "$R…现在就剩下我们两个$R;" +
                            "$R…就剩下我们两个$R;", "猫灵(绿子)");
                        Say(pc, 0, 131, "绿子…$R;", "猫灵(桃子)");
                        Say(pc, 131, "…$R;" +
                            "知道了$R虽然不能肯定…$R;" +
                            "$P可以拿跟以前一样的材料过来吗?$R;" +
                            "『棉缎带』、『布』和『线』$R;" +
                            "$P给您制作跟那猫灵一样的$R三角头巾吧$R;" +
                            "$P只能相信猫灵会凭依在$R我制作的三角头巾上$R;" +
                            "$R现在好了吗?$R;");
                        Say(pc, 0, 131, "喵$R;");
                        return;
                    }
                }
            }
            if (pc.Inventory.Equipments.ContainsKey(EnumEquipSlot.PET))
            {
                if (pc.Inventory.Equipments[EnumEquipSlot.PET].ItemID == 10017900)
                {
                    Say(pc, 0, 131, "稍等!!$R;", "猫灵(桃子)");
                    Say(pc, 131, "?…$R是粉红色的猫灵…$R这真是没办法…$R;");
                    Say(pc, 0, 131, "不过…$R那样的话…蓝…也会消失啊!!$R;" +
                        "$P那可不行!!$R;", "猫灵(桃子)");
                    Say(pc, 131, "…$R;");
                    Say(pc, 131, "…$R;" +
                        "知道了$R虽然不能肯定…$R;" +
                        "$P可以拿跟以前一样的材料过来吗?$R;" +
                        "『棉缎带』、『布』和『线』$R;" +
                        "$P给您制作跟那猫灵一样的$R三角头巾吧$R;" +
                        "$P只能相信猫灵会凭依在$R我制作的三角头巾上$R;" +
                        "$R现在好了吗?$R;");
                    Say(pc, 0, 131, "喵$R;");
                    return;
                }
            }
            if (pc.Inventory.Equipments.ContainsKey(EnumEquipSlot.PET))
            {
                if (pc.Inventory.Equipments[EnumEquipSlot.PET].ItemID == 10017902)
                {
                    Say(pc, 0, 131, "…救一下蓝$R;", "猫灵(绿子)");
                    Say(pc, 131, "?…$R是草绿色的猫灵…$R这真是没办法…$R;");
                    Say(pc, 0, 131, "…求求您$R朋友们都在战争中死掉了$R;" +
                        "$R…现在就剩下我们两个$R;" +
                        "$R…就剩下我们两个$R;", "猫灵(绿子)");
                    Say(pc, 131, "…$R;");
                    Say(pc, 131, "…$R;" +
                        "知道了$R虽然不能肯定…$R;" +
                        "$P可以拿跟以前一样的材料过来吗?$R;" +
                        "『棉缎带』、『布』和『线』$R;" +
                        "$P给您制作跟那猫灵一样的$R三角头巾吧$R;" +
                        "$P只能相信猫灵会凭依在$R我制作的三角头巾上$R;" +
                        "$R现在好了吗?$R;");
                    Say(pc, 0, 131, "喵$R;");
                    return;
                }
            }
            if (CountItem(pc, 10017900) >= 1)
            {
                if (pc.Inventory.Equipments.ContainsKey(EnumEquipSlot.PET))
                {
                    if (pc.Inventory.Equipments[EnumEquipSlot.PET].ItemID == 10017902)
                    {
                        Say(pc, 0, 131, "稍等!!$R;", "猫灵(桃子)");
                        Say(pc, 131, "?…$R是粉红色的猫灵…$R这真是没办法…$R;");
                        Say(pc, 0, 131, "不过…$R那样的话…蓝…也会消失啊!!$R;" +
                            "$P那可不行!!$R;", "猫灵(桃子)");
                        Say(pc, 131, "…$R;");
                        Say(pc, 0, 131, "…救救蓝$R;", "猫灵(绿子)");
                        Say(pc, 0, 131, "绿子!?$R;", "猫灵(桃子)");
                        Say(pc, 0, 131, "…求求您$R朋友们都在战争中死掉了$R;" +
                            "$R…现在就剩下我们两个$R;" +
                            "$R…就剩下我们两个$R;", "猫灵(绿子)");
                        Say(pc, 0, 131, "绿子…$R;", "猫灵(桃子)");
                        Say(pc, 131, "…$R;" +
                            "知道了$R虽然不能肯定…$R;" +
                            "$P可以拿跟以前一样的材料过来吗?$R;" +
                            "『棉缎带』、『布』和『线』$R;" +
                            "$P给您制作跟那猫灵一样的$R三角头巾吧$R;" +
                            "$P只能相信猫灵会凭依在$R我制作的三角头巾上$R;" +
                            "$R现在好了吗?$R;");
                        Say(pc, 0, 131, "喵$R;");
                        return;
                    }
                }
            }
            if (CountItem(pc, 10017900) >= 1 && CountItem(pc, 10017902) >= 1)
            {
                Say(pc, 0, 131, "稍等!!$R;", "猫灵(桃子)");
                Say(pc, 131, "?…$R是粉红色的猫灵…$R这真是没办法…$R;");
                Say(pc, 0, 131, "不过…$R那样的话…蓝…也会消失啊!!$R;" +
                    "$P那可不行!!$R;", "猫灵(桃子)");
                Say(pc, 131, "…$R;");
                Say(pc, 0, 131, "…救救蓝$R;", "猫灵(绿子)");
                Say(pc, 0, 131, "绿子!?$R;", "猫灵(桃子)");
                Say(pc, 0, 131, "…求求您$R朋友们都在战争中死掉了$R;" +
                    "$R…现在就剩下我们两个$R;" +
                    "$R…就剩下我们两个$R;", "猫灵(绿子)");
                Say(pc, 0, 131, "绿子…$R;", "猫灵(桃子)");
                Say(pc, 131, "…$R;" +
                    "知道了$R虽然不能肯定…$R;" +
                    "$P可以拿跟以前一样的材料过来吗?$R;" +
                    "『棉缎带』、『布』和『线』$R;" +
                    "$P给您制作跟那猫灵一样的$R三角头巾吧$R;" +
                    "$P只能相信猫灵会凭依在$R我制作的三角头巾上$R;" +
                    "$R现在好了吗?$R;");
                Say(pc, 0, 131, "喵$R;");
                return;
            }
            if (CountItem(pc, 10017900) >= 1)
            {
                Say(pc, 0, 131, "稍等!!$R;", "猫灵(桃子)");
                Say(pc, 131, "?…$R是粉红色的猫灵…$R这真是没办法…$R;");
                Say(pc, 0, 131, "不过…$R那样的话…蓝…也会消失啊!!$R;" +
                    "$P那可不行!!$R;", "猫灵(桃子)");
                Say(pc, 131, "…$R;");
                Say(pc, 131, "…$R;" +
                    "知道了$R虽然不能肯定…$R;" +
                    "$P可以拿跟以前一样的材料过来吗?$R;" +
                    "『棉缎带』、『布』和『线』$R;" +
                    "$P给您制作跟那猫灵一样的$R三角头巾吧$R;" +
                    "$P只能相信猫灵会凭依在$R我制作的三角头巾上$R;" +
                    "$R现在好了吗?$R;");
                Say(pc, 0, 131, "喵$R;");
                return;
            }
            if (CountItem(pc, 10017902) >= 1)
            {
                Say(pc, 0, 131, "…救一下蓝$R;", "猫灵(绿子)");
                Say(pc, 131, "?…$R是草绿色的猫灵…$R这真是没办法…$R;");
                Say(pc, 0, 131, "…求求您$R朋友们都在战争中死掉了$R;" +
                    "$R…现在就剩下我们两个$R;" +
                    "$R…就剩下我们两个$R;", "猫灵(绿子)");
                Say(pc, 131, "…$R;");
                Say(pc, 131, "…$R;" +
                    "知道了$R虽然不能肯定…$R;" +
                    "$P可以拿跟以前一样的材料过来吗?$R;" +
                    "『棉缎带』、『布』和『线』$R;" +
                    "$P给您制作跟那猫灵一样的$R三角头巾吧$R;" +
                    "$P只能相信猫灵会凭依在$R我制作的三角头巾上$R;" +
                    "$R现在好了吗?$R;");
                Say(pc, 0, 131, "喵$R;");
                return;
            }
        }

        void 木偶泰迪(ActorPC pc)
        {
            switch (Select(pc, "什么事情啊?", "", "买男性服装", "买女性服装", "卖东西", "委托裁缝", "委托烹饪", "制作活动木偶泰迪", "什么都不做"))
            {
                case 1:
                    OpenShopBuy(pc, 19);
                    Say(pc, 131, "再来玩吧$R;");
                    break;
                case 2:
                    OpenShopBuy(pc, 20);
                    Say(pc, 131, "再来玩吧$R;");
                    break;
                case 3:
                    OpenShopSell(pc, 20);
                    Say(pc, 131, "再来玩吧$R;");
                    break;
                case 4:
                    Synthese(pc, 2054, 5);
                    break;
                case 5:
                    Synthese(pc, 2040, 5);
                    break;
                case 6:
                    if (CountItem(pc, 10020208) >= 1 &&
                        CountItem(pc, 10019701) >= 1 &&
                        CountItem(pc, 10019702) >= 1 &&
                        CountItem(pc, 10019703) >= 1 &&
                        CountItem(pc, 10024002) >= 1 &&
                        CountItem(pc, 10019600) >= 1)
                    {
                        Say(pc, 131, "材料都弄齐了!$R;" +
                            "现在开始就是我做的事了$R;" +
                            "交给我吧!$R;");
                        Say(pc, 131, "给了她『缝制玩偶的布』$R;" +
                            "给了她『阿拉克尼的丝』$R;" +
                            "给了她『棉花』$R;" +
                            "给了她『早晨的针』$R;" +
                            "给了她『白天的针』$R;" +
                            "给了她『夜晚的针』$R;");

                        Fade(pc, FadeType.Out, FadeEffect.Black);
                        Wait(pc, 1000);
                        Wait(pc, 1000);
                        Fade(pc, FadeType.In, FadeEffect.Black);
                        Say(pc, 131, "好了!$R;" +
                            "我呕心沥血的杰作$R;" +
                            "$R很可爱吧?$R;" +
                            "好好珍惜使用吧！$R;");
                        PlaySound(pc, 4006, false, 100, 50);
                        Say(pc, 131, "得到了『活动木偶泰迪』$R;");
                        TakeItem(pc, 10020208, 1);
                        TakeItem(pc, 10019701, 1);
                        TakeItem(pc, 10019702, 1);
                        TakeItem(pc, 10019703, 1);
                        TakeItem(pc, 10024002, 1);
                        TakeItem(pc, 10019600, 1);
                        GiveItem(pc, 10022000, 1);
                        return;
                    }
                    Say(pc, 131, "活动木偶泰迪的材料是$R;" +
                        "『缝制玩偶的布』$R;" +
                        "『棉花』$R;" +
                        "『阿拉克尼的丝』$R;" +
                        "『針』$R;" +
                        "$P针如果不是得到裁缝之神守护的$R;" +
                        "特殊针，是不行的啊$R;" +
                        "$R裁缝之神化成蝴蝶的样子$R;" +
                        "注视着这个世界呢$R;" +
                        "$P无论怎么样，你找找看吧$R;" +
                        "我会等着的!$R;");
                    木偶泰迪(pc);
                    break;
                case 7:
                    break;
            }
        }

        void 萬聖節(ActorPC pc)
        {

            //EVT1100014953
            switch (Select(pc, "怎么做好呢？", "", "就那样打招呼", "不给糖就捣蛋！"))
            {
                case 1:
                    判斷(pc);
                    break;
                case 2:
                    if (pc.Inventory.Equipments.ContainsKey(EnumEquipSlot.HEAD))
                    {
                        if (pc.Inventory.Equipments[EnumEquipSlot.HEAD].ItemID == 50025800 ||
                            pc.Inventory.Equipments[EnumEquipSlot.HEAD].ItemID == 50024350 ||
                            pc.Inventory.Equipments[EnumEquipSlot.HEAD].ItemID == 50024351 ||
                            pc.Inventory.Equipments[EnumEquipSlot.HEAD].ItemID == 50024352 ||
                            pc.Inventory.Equipments[EnumEquipSlot.HEAD].ItemID == 50024353 ||
                            pc.Inventory.Equipments[EnumEquipSlot.HEAD].ItemID == 50024354 ||
                            pc.Inventory.Equipments[EnumEquipSlot.HEAD].ItemID == 50024355 ||
                            pc.Inventory.Equipments[EnumEquipSlot.HEAD].ItemID == 50024356 ||
                            pc.Inventory.Equipments[EnumEquipSlot.HEAD].ItemID == 50024357 ||
                            pc.Inventory.Equipments[EnumEquipSlot.HEAD].ItemID == 50024358 ||
                            pc.Inventory.Equipments[EnumEquipSlot.HEAD].ItemID == 50022500 ||
                            pc.Inventory.Equipments[EnumEquipSlot.HEAD].ItemID == 50022600 ||
                            pc.Inventory.Equipments[EnumEquipSlot.HEAD].ItemID == 50022700 ||
                            pc.Inventory.Equipments[EnumEquipSlot.HEAD].ItemID == 50022800)
                        {
                            Say(pc, 131, "呵呵呵$R;" +
                                "$R小妖精长得好可爱啊$R;" +
                                "来！给你饼干，不许淘气啊$R;");
                            if (CheckInventory(pc, 10009300, 1))
                            {
                                //_0b12 = true;
                                GiveItem(pc, 10009300, 1);
                                return;
                            }
                            return;
                        }
                    }
                    Say(pc, 131, "嗯…打扮后再来吧$R;" +
                        "到时候我会给你饼干的$R;");
                    break;
            }
        }
    }
}
