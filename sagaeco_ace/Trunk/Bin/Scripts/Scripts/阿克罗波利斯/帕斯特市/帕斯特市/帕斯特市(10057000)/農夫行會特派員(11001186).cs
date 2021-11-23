using System;
using System.Collections.Generic;
using System.Text;
using SagaLib;
using SagaDB.Actor;
using SagaMap.Scripting;
using SagaScript.Chinese.Enums;
namespace SagaScript.M10057000
{
    public class S11001186 : Event
    {
        public S11001186()
        {
            this.EventID = 11001186;
        }

        public override void OnEvent(ActorPC pc)
        {

            BitMask<Sinker> mask = pc.AMask["Sinker"];
            if (mask.Test(Sinker.寶石商人給予詩迪的項鏈墜))//_2b02)
            {
                Say(pc, 159, "您好！$R;" +
                    "用昨天看到的合金，试做了项链坠！$R;" +
                    "不介意的话，分给大家吧！$R;");
                OpenShopBuy(pc, 198);
                return;
            }
            if (mask.Test(Sinker.獲得別針) && mask.Test(Sinker.看過告示牌))//_2b00 && _7a91)
            {
                Say(pc, 159, "您好！$R;" +
                    "今天的天气真好呢！$R;");
                return;
            }
            if (mask.Test(Sinker.未收到別針))//_7a99)
            {
                if (CheckInventory(pc, 10038101, 1))
                {
                    mask.SetValue(Sinker.未收到別針, false);
                    mask.SetValue(Sinker.獲得別針, true);
                    //_7a99 = false;
                    //_2b00 = true;
                    GiveItem(pc, 10038101, 1);
                    PlaySound(pc, 2040, false, 100, 50);
                    Say(pc, 131, "收到『别针』！$R;");
                    Say(pc, 131, "拿到宝石商人那里$R;" +
                        "说不定可以做成什么呢$R;" +
                        "$P反正这次真的谢谢您$R;" +
                        "如果有机会的话，再委托您$R;");
                    mask.SetValue(Sinker.收到合成藥, false);
                    mask.SetValue(Sinker.拒絕幫忙, false);
                    mask.SetValue(Sinker.收到不明的合金, false);
                    //_7a93 = false;
                    //_7a94 = false;
                    //_7a96 = false;
                    return;
                }
                Say(pc, 131, "行李太多了，无法给您$R;");
                mask.SetValue(Sinker.未收到別針, true);
                //_7a99 = true;
                return;
            }
            if (CountItem(pc, 10020762) >= 1)
            {
                Say(pc, 131, "转交了！$R;" +
                    "谢谢！$R;");
                PlaySound(pc, 2040, false, 100, 50);
                Say(pc, 131, "给他『合成测试报告』$R;");
                TakeItem(pc, 10020762, 1);
                Say(pc, 131, "您拿着的那个？$R;" +
                    "是这次合成试验时使用的合金啊！$R;" +
                    "$R真的是很漂亮的合金$R;" +
                    "报告书上写著『初合金』$R;" +
                    "$P加工使用起来有点小$R;" +
                    "但加工成饰品应该可以$R;" +
                    "$R啊!这裡刚好有『别针』$R;" +
                    "给您这个吧$R;");
                if (CheckInventory(pc, 10038101, 1))
                {
                    mask.SetValue(Sinker.未收到別針, false);
                    mask.SetValue(Sinker.獲得別針, true);
                    //_7a99 = false;
                    //_2b00 = true;
                    GiveItem(pc, 10038101, 1);
                    PlaySound(pc, 2040, false, 100, 50);
                    Say(pc, 131, "收到『别针』！$R;");
                    Say(pc, 131, "拿到宝石商人那里$R;" +
                        "说不定可以做成什么呢$R;" +
                        "$P反正这次真的谢谢您$R;" +
                        "如果有机会的话，再委托您$R;");
                    mask.SetValue(Sinker.收到合成藥, false);
                    mask.SetValue(Sinker.拒絕幫忙, false);
                    mask.SetValue(Sinker.收到不明的合金, false);
                    //_7a93 = false;
                    //_7a94 = false;
                    //_7a96 = false;
                    return;
                }
                Say(pc, 131, "行李太多了，无法给您$R;");
                mask.SetValue(Sinker.未收到別針, true);
                //_7a99 = true;
                return;
            }
            if (mask.Test(Sinker.收到合成藥) && mask.Test(Sinker.看過告示牌))//_7a93 && _7a91)
            {
                Say(pc, 131, "把『合成药』交给铁厂老板之前$R;" +
                    "都要小心翼翼的放好$R;");
                return;
            }
            if (mask.Test(Sinker.未收到合成藥))//_7a92)
            {
                if (CheckInventory(pc, 10000510, 1))
                {
                    mask.SetValue(Sinker.未收到合成藥, false);
                    mask.SetValue(Sinker.收到合成藥, true);
                    //_7a92 = false;
                    //_7a93 = true;
                    GiveItem(pc, 10000510, 1);
                    PlaySound(pc, 2040, false, 100, 50);
                    Say(pc, 131, "收到『合成药』!$R;");
                    Say(pc, 131, "刚才也説过了，一定要小心翼翼阿$R;" +
                        "对衝撃很敏感，所以尽量不要$R;" +
                        "使用施工的钥匙，拜託了！$R;" +
                        "$P考虑到準备回家乡的路上$R;" +
                        "会有无法预料的事情发生$R;" +
                        "把归还的地点改为帕斯特市吧$R;" +
                        "$R那么就...拜託了!$R;");

                    byte x, y;
                    x = (byte)Global.Random.Next(7, 17);
                    y = (byte)Global.Random.Next(120, 126);

                    SetHomePoint(pc, 10057000, x, y);
                    //PARAM ME.SAVEID = 422
                    Say(pc, 131, "储存点，变更为『法伊斯特』！$R;");
                    return;
                }
                Say(pc, 131, "行李太多了，无法给您$R;");
                mask.SetValue(Sinker.未收到合成藥, true);
                //_7a92 = true;
                return;
            }
            if (pc.Job == PC_JOB.WIZARD
                || pc.Job == PC_JOB.SORCERER
                || pc.Job == PC_JOB.SAGE
                || pc.Job == PC_JOB.SHAMAN
                || pc.Job == PC_JOB.ELEMENTER
                || pc.Job == PC_JOB.ENCHANTER
                || pc.Job == PC_JOB.VATES
                || pc.Job == PC_JOB.DRUID
                || pc.Job == PC_JOB.BARD
                || pc.Job == PC_JOB.WARLOCK
                || pc.Job == PC_JOB.GAMBLER
                || pc.Job == PC_JOB.NECROMANCER)
            {
                if (pc.Level > 34 && !mask.Test(Sinker.看過告示牌))//_7a91)
                {
                    Say(pc, 159, "您好！是看到阿克罗波利斯的$R;" +
                        "委托告示板过来的吗？$R;");
                    return;
                }
            }
            if (mask.Test(Sinker.看過告示牌))//_7a91)
            {
                Say(pc, 159, "看了委托告示板过来的吧！$R;" +
                    "远道而来!辛苦了$R;" +
                    "$R这次是向生产系拜托事情的$R;");
                Say(pc, 131, "工作的内容归内容$R;" +
                    "要是战士系或魔法系的冒险者$R;" +
                    "可以帮忙的话…当然最好不过$R;" +
                    "$R截头去尾，简单说就是帮我搬东西$R;" +
                    "$P把昨天刚完成的合成药$R;" +
                    "转交给艾恩萨乌斯的铁厂老板$R;" +
                    "$R是从食物中抽取的草本精华$R;" +
                    "调和而成的$R;" +
                    "因此要很小心搬运的东西$R;" +
                    "$P作为生产系的您$R;" +
                    "相信您会安全的帮我搬运$R;" +
                    "$R可以拜托您吗?$R;");
                switch (Select(pc, "怎么办?", "", "知道了", "困难呀"))
                {
                    case 1:
                        Say(pc, 131, "谢谢！$R;" +
                            "那么就拜托了$R;");
                        if (CheckInventory(pc, 10000510, 1))
                        {
                            mask.SetValue(Sinker.未收到合成藥, false);
                            mask.SetValue(Sinker.收到合成藥, true);
                            //_7a92 = false;
                            //_7a93 = true;
                            GiveItem(pc, 10000510, 1);
                            PlaySound(pc, 2040, false, 100, 50);
                            Say(pc, 131, "收到『合成药』!$R;");
                            Say(pc, 131, "刚才也说过了，一定要小心翼翼啊$R;" +
                                "对冲击很敏感，所以尽量不要$R;" +
                                "使用施工的钥匙，拜托了！$R;" +
                                "$P考虑到准备回来的路上$R;" +
                                "会有无法预料的事情发生$R;" +
                                "把归还的地点改为法伊斯特市吧$R;" +
                                "$R那么就...拜托了!$R;");

                            byte x, y;
                            x = (byte)Global.Random.Next(7, 17);
                            y = (byte)Global.Random.Next(120, 126);

                            SetHomePoint(pc, 10057000, x, y);
                            //PARAM ME.SAVEID = 422
                            Say(pc, 131, "储存点，变更为『法伊斯特』！$R;");
                            return;
                        }
                        Say(pc, 131, "行李太多了，无法给您$R;");
                        mask.SetValue(Sinker.未收到合成藥, true);
                        //_7a92 = true;
                        break;
                }
                return;
            }
            
            Say(pc, 159, "您好！$R;" +
                "今天的天气真好呢！$R;");
        }
    }
}