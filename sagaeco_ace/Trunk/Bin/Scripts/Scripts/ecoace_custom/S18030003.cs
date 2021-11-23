using System;
using System.Collections.Generic;
using System.Text;
using SagaLib;
using SagaDB.Item;
using SagaDB.Map;
using SagaDB.Actor;
using SagaMap.Scripting;
using SagaScript.Chinese.Enums;
namespace SagaScript
{
    public class S18030003 : Event
    {
        public S18030003()
        {
            this.EventID = 18030003;
        }

        public override void OnEvent(ActorPC pc)
        {
            Say(pc, 0, "咪呦咪呦!", "爱心德普提");
            BitMask<ace_kujierdiag> ace_kujierdiag_mask = new BitMask<ace_kujierdiag>(pc.CMask["ace_kujierdiag"]);
            if (!ace_kujierdiag_mask.Test(ace_kujierdiag.diag_0001))
            {
                Say(pc, 0, "你知道ECO之心吗?!", "爱心德普提");
                Say(pc, 0, "就是长的像棉花糖一样的东东!$R$R只要集齐15个,就可以得到$R我们准备的兑换券哦!$R$R", "爱心德普提");
                Say(pc, 0, "咪呦咪呦!!! 咪呦咪呦!!!!!$R$R$R", "爱心德普提");
                Say(pc, 0, "煌逼之心, 就来找我哦!$R$R咪呦咪呦!!!$R", "爱心德普提");
                ace_kujierdiag_mask.SetValue(ace_kujierdiag.diag_0001, true);
            }
            switch (Select(pc, "要查看激赏礼券列表吗?", "", "激赏礼券列表(1-10)", "激赏礼券列表(11-20)", "激赏礼券列表(21 - 30)", "取消"))
            {
                case 1:
                    {
                        Wait(pc, 1000);
                        Say(pc, 0, "咦? 这部分坏掉了诶...!", "爱心德普提");
                        Wait(pc, 5000);
                        Say(pc, 0, "可是....你还想要吗???", "爱心德普提");
                        Wait(pc, 1000);
                        switch (Select(pc,"应该怎么回答呢?","","想要!","咪呦～QAQ","喵喵喵!～♪","汪汪汪!!!","想捏碎这个混蛋","不要"))
                        {
                            case 1:
                            {
                                Say(pc, 0, "咦?", "爱心德普提");
                                return;
                            }
                            case 2:
                            {
                                Say(pc, 0, "咦咦?", "爱心德普提");
                                return;
                            }
                            case 3:
                            {
                                Say(pc, 0, "咦咦咦?", "爱心德普提");
                                return;
                            }
                            case 4:
                            {
                                Say(pc, 0, "咦咦咦咦?", "爱心德普提");
                                return;
                            }
                            case 5:
                            {
                                    Say(pc, 0, "好....好...好啦好啦! $R$R我知道啦.....拿出来就好了嘛!", "爱心德普提");
                                    Say(pc, 0, "嘿嘿嘿嘿...旧kuji是涨价了!", "爱心德普提");
                                    Say(pc, 0, "现在,需要30心$R才能兑换一个旧的kuji系列$R(kuji1-10)", "系统");
                                    /*想通关，想攻破我的地下城？来试试这个吧！是的，我知道你讨厌我。我设计这种东西就是要让你来恨我！来恨我吧！ */
                                    Wait(pc, 500);
                                    PlaySound(pc, 6011, false, 100, 50);
                                    Wait(pc, 100);
                                    PlaySound(pc, 6011, false, 100, 50);
                                    Wait(pc, 100);
                                    PlaySound(pc, 6011, false, 100, 50);
                                    Wait(pc, 100);
                                    PlaySound(pc, 6011, false, 100, 50);
                                    Wait(pc, 100);
                                    PlaySound(pc, 6001, false, 100, 50);
                                    Say(pc, 0, "想通关，想攻破我的地下城？$R来试试这个吧！$R$R是的，我知道你讨♂厌我。$R我设计这种东西就是要让你来♂恨♂我！$R$R来♂恨♂我吧！", "♂F♂子♂");
                                    Wait(pc, 6000);
                                    if (CountItem(pc, 22000103) >= 30)
                                    {
                                        switch (Select(pc, "激赏礼券列表", "", "激赏礼券1", "激赏礼券2", "激赏礼券3", "激赏礼券4", "激赏礼券5", "激赏礼券6", "激赏礼券7", "激赏礼券8", "激赏礼券9", "激赏礼券10", "取消"))
                                        {
                                            case 1:
                                                {
                                                    PlaySound(pc, 2040, false, 100, 50);
                                                    Say(pc, 0, "得到了激赏礼券1", "爱心德普提");
                                                    GiveItem(pc, 22001000, 1);
                                                    TakeItem(pc, 22000103, 30);
                                                    break;
                                                }
                                            case 2:
                                                {
                                                    PlaySound(pc, 2040, false, 100, 50);
                                                    Say(pc, 0, "得到了激赏礼券2", "爱心德普提");
                                                    GiveItem(pc, 22001001, 1);
                                                    TakeItem(pc, 22000103, 30);
                                                    break;
                                                }
                                            case 3:
                                                {
                                                    PlaySound(pc, 2040, false, 100, 50);
                                                    Say(pc, 0, "得到了激赏礼券3", "爱心德普提");
                                                    GiveItem(pc, 22001002, 1);
                                                    TakeItem(pc, 22000103, 30);
                                                    break;
                                                }
                                            case 4:
                                                {
                                                    PlaySound(pc, 2040, false, 100, 50);
                                                    Say(pc, 0, "得到了激赏礼券4", "爱心德普提");
                                                    GiveItem(pc, 22001003, 1);
                                                    TakeItem(pc, 22000103, 30);
                                                    break;
                                                }
                                            case 5:
                                                {
                                                    PlaySound(pc, 2040, false, 100, 50);
                                                    Say(pc, 0, "得到了激赏礼券5", "爱心德普提");
                                                    GiveItem(pc, 22001004, 1);
                                                    TakeItem(pc, 22000103, 30);
                                                    break;
                                                }
                                            case 6:
                                                {
                                                    PlaySound(pc, 2040, false, 100, 50);
                                                    Say(pc, 0, "得到了激赏礼券6", "爱心德普提");
                                                    GiveItem(pc, 22001005, 1);
                                                    TakeItem(pc, 22000103, 30);
                                                    break;
                                                }
                                            case 7:
                                                {
                                                    PlaySound(pc, 2040, false, 100, 50);
                                                    Say(pc, 0, "得到了激赏礼券7", "爱心德普提");
                                                    GiveItem(pc, 22001006, 1);
                                                    TakeItem(pc, 22000103, 30);
                                                    break;
                                                }
                                            case 8:
                                                {
                                                    PlaySound(pc, 2040, false, 100, 50);
                                                    Say(pc, 0, "得到了激赏礼券8", "爱心德普提");
                                                    GiveItem(pc, 22001007, 1);
                                                    TakeItem(pc, 22000103, 30);
                                                    break;
                                                }
                                            case 9:
                                                {
                                                    PlaySound(pc, 2040, false, 100, 50);
                                                    Say(pc, 0, "得到了激赏礼券9", "爱心德普提");
                                                    GiveItem(pc, 22001008, 1);
                                                    TakeItem(pc, 22000103, 30);
                                                    break;
                                                }
                                            case 10:
                                                {
                                                    PlaySound(pc, 2040, false, 100, 50);
                                                    Say(pc, 0, "得到了激赏礼券10", "爱心德普提");
                                                    GiveItem(pc, 22001009, 1);
                                                    TakeItem(pc, 22000103, 30);
                                                    break;
                                                }
                                            case 11:
                                                {
                                                    return;
                                                }
                                        }
                                }
                                else
                                {
                                    Say(pc, 0, "就是不给你咪呦♪", "爱心德普提");
                                }
                                return;
                            }
                        }
                        return;
                    }
                case 2:
                    {
                        Say(pc, 0, "就是不给你喵♪", "爱心德普提");
                        /*
                        if (CountItem(pc, 22000103) >= 15)
                        {
                            switch (Select(pc, "激赏礼券列表", "", "激赏礼券11", "激赏礼券12", "激赏礼券13", "激赏礼券14", "激赏礼券15", "激赏礼券16", "激赏礼券17", "激赏礼券18", "激赏礼券19", "激赏礼券20", "取消"))
                            {
                                case 1:
                                    {
                                        PlaySound(pc, 2040, false, 100, 50);
                                        Say(pc, 0, "得到了激赏礼券11", "爱心德普提");
                                        GiveItem(pc, 22001010, 1);
                                        TakeItem(pc, 22000103, 15);
                                        break;
                                    }
                                case 2:
                                    {
                                        PlaySound(pc, 2040, false, 100, 50);
                                        Say(pc, 0, "得到了激赏礼券12", "爱心德普提");
                                        GiveItem(pc, 22001011, 1);
                                        TakeItem(pc, 22000103, 15);
                                        break;
                                    }
                                case 3:
                                    {
                                        PlaySound(pc, 2040, false, 100, 50);
                                        Say(pc, 0, "得到了激赏礼券13", "爱心德普提");
                                        GiveItem(pc, 22001012, 1);
                                        TakeItem(pc, 22000103, 15);
                                        break;
                                    }
                                case 4:
                                    {
                                        PlaySound(pc, 2040, false, 100, 50);
                                        Say(pc, 0, "得到了激赏礼券14", "爱心德普提");
                                        GiveItem(pc, 22001013, 1);
                                        TakeItem(pc, 22000103, 15);
                                        break;
                                    }
                                case 5:
                                    {
                                        PlaySound(pc, 2040, false, 100, 50);
                                        Say(pc, 0, "得到了激赏礼券15", "爱心德普提");
                                        GiveItem(pc, 22001014, 1);
                                        TakeItem(pc, 22000103, 15);
                                        break;
                                    }
                                case 6:
                                    {
                                        PlaySound(pc, 2040, false, 100, 50);
                                        Say(pc, 0, "得到了激赏礼券16", "爱心德普提");
                                        GiveItem(pc, 22001015, 1);
                                        TakeItem(pc, 22000103, 15);
                                        break;
                                    }
                                case 7:
                                    {
                                        PlaySound(pc, 2040, false, 100, 50);
                                        Say(pc, 0, "得到了激赏礼券17", "爱心德普提");
                                        GiveItem(pc, 22001016, 1);
                                        TakeItem(pc, 22000103, 15);
                                        break;
                                    }
                                case 8:
                                    {
                                        PlaySound(pc, 2040, false, 100, 50);
                                        Say(pc, 0, "得到了激赏礼券18", "爱心德普提");
                                        GiveItem(pc, 22001017, 1);
                                        TakeItem(pc, 22000103, 15);
                                        break;
                                    }
                                case 9:
                                    {
                                        PlaySound(pc, 2040, false, 100, 50);
                                        Say(pc, 0, "得到了激赏礼券19", "爱心德普提");
                                        GiveItem(pc, 22001018, 1);
                                        TakeItem(pc, 22000103, 15);
                                        break;
                                    }
                                case 10:
                                    {
                                        PlaySound(pc, 2040, false, 100, 50);
                                        Say(pc, 0, "得到了激赏礼券20", "爱心德普提");
                                        GiveItem(pc, 22001019, 1);
                                        TakeItem(pc, 22000103, 15);
                                        break;
                                    }
                                case 11:
                                    {
                                        return;
                                    }
                            }
                         
                        }
                        else
                        {
                            Say(pc, 0, "咪呦,什么都没有哦!", "爱心德普提");
                        }
                        */
                        return;
                    }
                case 3:
                    {
                        if (CountItem(pc, 22000103) >= 15)
                        {
                            PlaySound(pc, 6014, false, 100, 50);
                            Say(pc, 0, "呵呵", "爱心德普提");
                            Say(pc, 0, "这次会得到ECO兑换券", "爱心德普提");
                            Say(pc, 0, "不要乱丢哦", "爱心德普提");
                            Say(pc, 0, "其实它一点用都没有$R$R$R哭哭....", "爱心德普提");
                            
                            switch (Select(pc, "激赏礼券列表", "", "激赏礼券21", "激赏礼券22", "激赏礼券23", "激赏礼券24", "激赏礼券25", "激赏礼券26", "激赏礼券27", "激赏礼券28", "激赏礼券29", "激赏礼券30", "取消"))
                            {
                                case 1:
                                    {
                                        PlaySound(pc, 2040, false, 100, 50);
                                        Say(pc, 0, "得到了激赏礼券21", "爱心德普提");
                                        GiveItem(pc, 22001020, 1);
                                        TakeItem(pc, 22000103, 15);
                                        Wait(pc, 300);
                                        PlaySound(pc, 6016, false, 100, 50);
                                        Say(pc, 0, "得到了ECO兑换券.", "爱心德普提");
                                        GiveItem(pc, 22000182, 1);
                                        break;
                                    }
                                case 2:
                                    {
                                        PlaySound(pc, 2040, false, 100, 50);
                                        Say(pc, 0, "得到了激赏礼券22", "爱心德普提");
                                        GiveItem(pc, 22001021, 1);
                                        TakeItem(pc, 22000103, 15);
                                        Wait(pc, 300);
                                        PlaySound(pc, 6016, false, 100, 50);
                                        Say(pc, 0, "得到了ECO兑换券.", "爱心德普提");
                                        GiveItem(pc, 22000182, 1);
                                        break;
                                    }
                                case 3:
                                    {
                                        PlaySound(pc, 2040, false, 100, 50);
                                        Say(pc, 0, "得到了激赏礼券23", "爱心德普提");
                                        GiveItem(pc, 22001022, 1);
                                        TakeItem(pc, 22000103, 15);
                                        Wait(pc, 300);
                                        PlaySound(pc, 6016, false, 100, 50);
                                        Say(pc, 0, "得到了ECO兑换券.", "爱心德普提");
                                        GiveItem(pc, 22000182, 1);
                                        break;
                                    }
                                case 4:
                                    {
                                        PlaySound(pc, 2040, false, 100, 50);
                                        Say(pc, 0, "得到了激赏礼券24", "爱心德普提");
                                        GiveItem(pc, 22001023, 1);
                                        TakeItem(pc, 22000103, 15);
                                        Wait(pc, 300);
                                        PlaySound(pc, 6016, false, 100, 50);
                                        Say(pc, 0, "得到了ECO兑换券.", "爱心德普提");
                                        GiveItem(pc, 22000182, 1);
                                        break;
                                    }
                                case 5:
                                    {
                                        PlaySound(pc, 2040, false, 100, 50);
                                        Say(pc, 0, "得到了激赏礼券25", "爱心德普提");
                                        GiveItem(pc, 22001024, 1);
                                        TakeItem(pc, 22000103, 15);
                                        Wait(pc, 300);
                                        PlaySound(pc, 6016, false, 100, 50);
                                        Say(pc, 0, "得到了ECO兑换券.", "爱心德普提");
                                        GiveItem(pc, 22000182, 1);
                                        break;
                                    }
                                case 6:
                                    {
                                        PlaySound(pc, 2040, false, 100, 50);
                                        Say(pc, 0, "得到了激赏礼券26", "爱心德普提");
                                        GiveItem(pc, 22001025, 1);
                                        TakeItem(pc, 22000103, 15);
                                        Wait(pc, 300);
                                        PlaySound(pc, 6016, false, 100, 50);
                                        Say(pc, 0, "得到了ECO兑换券.", "爱心德普提");
                                        GiveItem(pc, 22000182, 1);
                                        break;
                                    }
                                case 7:
                                    {
                                        PlaySound(pc, 2040, false, 100, 50);
                                        Say(pc, 0, "得到了激赏礼券27", "爱心德普提");
                                        GiveItem(pc, 22001026, 1);
                                        TakeItem(pc, 22000103, 15);
                                        Wait(pc, 300);
                                        PlaySound(pc, 6016, false, 100, 50);
                                        Say(pc, 0, "得到了ECO兑换券.", "爱心德普提");
                                        GiveItem(pc, 22000182, 1);
                                        break;
                                    }
                                case 8:
                                    {
                                        PlaySound(pc, 2040, false, 100, 50);
                                        Say(pc, 0, "得到了激赏礼券28", "爱心德普提");
                                        GiveItem(pc, 22001027, 1);
                                        TakeItem(pc, 22000103, 15);
                                        Wait(pc, 300);
                                        PlaySound(pc, 6016, false, 100, 50);
                                        Say(pc, 0, "得到了ECO兑换券.", "爱心德普提");
                                        GiveItem(pc, 22000182, 1);
                                        break;
                                    }
                                case 9:
                                    {
                                        PlaySound(pc, 2040, false, 100, 50);
                                        Say(pc, 0, "得到了激赏礼券29", "爱心德普提");
                                        GiveItem(pc, 22001028, 1);
                                        TakeItem(pc, 22000103, 15);
                                        Wait(pc, 300);
                                        PlaySound(pc, 6016, false, 100, 50);
                                        Say(pc, 0, "得到了ECO兑换券.", "爱心德普提");
                                        GiveItem(pc, 22000182, 1);
                                        break;
                                    }
                                case 10:
                                    {
                                        PlaySound(pc, 2040, false, 100, 50);
                                        Say(pc, 0, "得到了激赏礼券30", "爱心德普提");
                                        GiveItem(pc, 22001029, 1);
                                        TakeItem(pc, 22000103, 15);
                                        Wait(pc, 300);
                                        PlaySound(pc, 6016, false, 100, 50);
                                        Say(pc, 0, "得到了ECO兑换券.", "爱心德普提");
                                        GiveItem(pc, 22000182, 1);
                                        break;
                                    }
                                case 11:
                                    {
                                        return;
                                    }
                            }

                        }
                        else
                        {
                            Say(pc, 0, "咪呦,什么都没有哦!", "爱心德普提");
                        }
                        return;
                    }
                case 4:
                    {
                        return;
                    }
            }
        }
    }
}
namespace SagaScript.Chinese.Enums
{
    public enum ace_kujierdiag
    {
        diag_0001 = 0x1,//与德普提对话过了
        diag_0002 = 0x2,//与德普提开过玩笑了
    }
}
