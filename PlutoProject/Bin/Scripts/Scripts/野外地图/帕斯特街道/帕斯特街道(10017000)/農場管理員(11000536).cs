using System;
using System.Collections.Generic;
using System.Text;
using SagaLib;
using SagaDB.Actor;
using SagaMap.Scripting;
using SagaScript.Chinese.Enums;
namespace SagaScript.M10017000
{
    public class S11000536 : Event
    {
        public S11000536()
        {
            this.EventID = 11000536;
        }

        public override void OnEvent(ActorPC pc)
        {
            BitMask<PSTJDFlags> mask = new BitMask<PSTJDFlags>(pc.CMask["PSTJD"]);
            byte x, y;
            if (pc.Pet != null)
            {
                if (pc.Pet.PetID == 10580000)
                {
                    Say(pc, 131, "你好!$R;" +
                        "$R你带着的胡萝卜是健康的吗?$R;" +
                        "$P这是在我们农场特别制作的!!$R;" +
                        "$R正在举办安全又安心的$R无农药栽培出来的$R特别营养剂活动中?$R;" +
                        "$R当然是免费的?$R;");
                    switch (Select(pc, "要使用看看吗?", "", "使用", "放弃"))
                    {
                        case 1:
                            PlaySound(pc, 2030, false, 100, 50);
                            Say(pc, 131, "宠物成长的话，外貌也会改变的!$R;" +
                                "$R知道吧?$R;");
                            switch (Select(pc, "要使用看看吗?", "", "使用", "放弃"))
                            {
                                case 1:
                                    Say(pc, 131, "那!!$R;" +
                                        "$R首先把曼陀罗胡萝卜$R轻轻的埋在地上后$R;" +
                                        "$P在那旁边插上营养增长器$R;" +
                                        "$R营养剂吸收到地上后$R稍等一下吧!$R;");
                                    Say(pc, 131, "…稍等一下!$R;");
                                    Say(pc, 131, "……稍等一下!$R;");
                                    Say(pc, 131, "…现在好了吗?$R;" +
                                        "$R那么抓著曼陀萝胡萝卜的叶子后$R轻轻拔的话……$R;" +
                                        "$R嗯嗯……?$R;" +
                                        "$P啊…啊!怎么…拔不出来……!$R哎呦!哎呦!哎呦……$R;" +
                                        "$R混账……$R;" +
                                        "$P哎啊啊啊!!!$R;");
                                    PlaySound(pc, 4001, false, 100, 50);
                                    Wait(pc, 2000);
                                    PlaySound(pc, 2220, false, 100, 50);
                                    PlaySound(pc, 2766, false, 100, 50);
                                    PlaySound(pc, 2445, false, 50, 50);
                                    Wait(pc, 2000);
                                    Say(pc, 131, "呼呼……$R;" +
                                        "$R轻轻的拔出来，对…就是这样！$R把这样健康的胡萝卜……$R;" +
                                        "$P啊啊啊……?$R;" +
                                        "$R胡萝卜…成了萝卜…$R;" +
                                        "$P啊!失败了?$R好像在很小一部份的基因组合里$R有了点失误!$R;" +
                                        "$R嘿嘿嘿?$R;");
                                    Say(pc, 131, "啊啊啊啊!?$R;" +
                                        "$R不是有安全又可以安心的成分吗…$R;");
                                    //PETCHANGE TRANSFORM 10019203 ALL
                                    break;
                                case 2:
                                    Say(pc, 131, "是免费啊!?$R啊啊……$R;");
                                    break;
                            }
                            break;
                        case 2:
                            Say(pc, 131, "是免费啊!?$R啊啊……$R;");
                            break;
                    }
                    return;
                }
            }
            if (mask.Test(PSTJDFlags.栽培俱樂部))
            {
                switch (Select(pc, "欢迎来到栽培俱乐部", "", "去秘密的场所!", "现在不去"))
                {
                    case 1:
                        x = (byte)Global.Random.Next (133, 136);
                        y = (byte)Global.Random.Next (67, 70);
                        Warp(pc, 10017001, x, y);
                        break;
                    case 2:
                        break;
                }
                return;
            }
            if (pc.Job == PC_JOB.TATARABE ||
                pc.Job == PC_JOB.BLACKSMITH ||
                pc.Job == PC_JOB.MACHINERY ||
                pc.Job == PC_JOB.FARMASIST ||
                pc.Job == PC_JOB.ALCHEMIST ||
                pc.Job == PC_JOB.MARIONEST ||
                pc.Job == PC_JOB.RANGER ||
                pc.Job == PC_JOB.EXPLORER ||
                pc.Job == PC_JOB.TREASUREHUNTER ||
                pc.Job == PC_JOB.MERCHANT ||
                pc.Job == PC_JOB.TRADER ||
                pc.Job == PC_JOB.GAMBLER)
            {
                Say(pc, 131, "喜欢『栽培』的人聚集的$R;" +
                    "生產系专用『栽培俱乐部』$R;" +
                    "你也要加入看看吗?$R;" +
                    "$R入会费只是『巨麦』20个!$R;" +
                    "$P加入栽培俱乐部的条件$R;" +
                    "就是『喜欢栽培的人』!$R;" +
                    "$R会告诉你对栽培特别合适的秘密场所$R;");
                switch (Select(pc, "加入吗?", "", "不加入", "加入!"))
                {
                    case 1:
                        break;
                    case 2:
                        if (CountItem(pc, 10005900) >= 20)
                        {
                            mask.SetValue(PSTJDFlags.栽培俱樂部, true);
                            TakeItem(pc, 10005900, 20);
                            Say(pc, 131, "谢谢~!!$R;" +
                                "$R现在开始只要跟我说话$R;" +
                                "马上给你往秘密的场所的指引!$R;");
                            switch (Select(pc, "欢迎来到栽培俱乐部", "", "去秘密的场所!", "现在不去"))
                            {
                                case 1:
                                    x = (byte)Global.Random.Next(133, 136);
                                    y = (byte)Global.Random.Next(67, 70);
                                    Warp(pc, 10017001, x, y);
                                    break;
                                case 2:
                                    break;
                            }
                            return;
                        }
                        Say(pc, 131, "入会费是『巨麦』20个$R;" +
                            "$R什么时候都可以接受申请$R;" +
                            "所以什么时候来都可以$R;");
                        break;
                }
                return;
            }
            Say(pc, 131, "你好!!天气好啊!$R;" +
                "$R太阳微笑着，农作物也长得很好!$R;");
        }
    }
}
