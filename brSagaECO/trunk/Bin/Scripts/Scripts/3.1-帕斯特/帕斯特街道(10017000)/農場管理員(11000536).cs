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
                        "$R你帶著的胡蘿蔔是健康的嗎?$R;" +
                        "$P這是在我們農場特別製作的!!$R;" +
                        "$R正在舉辦安全又安心的$R無農藥栽培出來的$R特別營養劑活動中?$R;" +
                        "$R當然是免費的?$R;");
                    switch (Select(pc, "要使用看看嗎?", "", "使用", "放棄"))
                    {
                        case 1:
                            PlaySound(pc, 2030, false, 100, 50);
                            Say(pc, 131, "寵物成長的話，外貌也會改變的!$R;" +
                                "$R知道吧?$R;");
                            switch (Select(pc, "要使用看看嗎?", "", "使用", "放棄"))
                            {
                                case 1:
                                    Say(pc, 131, "那!!$R;" +
                                        "$R首先把曼陀蘿胡蘿蔔$R輕輕的埋在地上後$R;" +
                                        "$P在那旁邊插上營養增長器$R;" +
                                        "$R營養劑吸收到地上後$R稍等一下吧!$R;");
                                    Say(pc, 131, "…稍等一下!$R;");
                                    Say(pc, 131, "……稍等一下!$R;");
                                    Say(pc, 131, "…現在好了嗎?$R;" +
                                        "$R那麽抓著曼陀蘿胡蘿蔔的葉子後$R輕輕拔的話……$R;" +
                                        "$R嗯嗯……?$R;" +
                                        "$P啊…啊!怎麽…拔不出來……!$R哎呦!哎呦!哎呦……$R;" +
                                        "$R混賬……$R;" +
                                        "$P哎啊啊啊!!!$R;");
                                    PlaySound(pc, 4001, false, 100, 50);
                                    Wait(pc, 2000);
                                    PlaySound(pc, 2220, false, 100, 50);
                                    PlaySound(pc, 2766, false, 100, 50);
                                    PlaySound(pc, 2445, false, 50, 50);
                                    Wait(pc, 2000);
                                    Say(pc, 131, "呼呼……$R;" +
                                        "$R輕輕的拔出來，對…就是這樣！$R把這樣健康的胡蘿蔔……$R;" +
                                        "$P啊啊啊……?$R;" +
                                        "$R胡蘿蔔…成了蘿蔔…$R;" +
                                        "$P阿!失敗了?$R好像在很小一部份的基因組合裡$R有了點失誤!$R;" +
                                        "$R嘿嘿嘿?$R;");
                                    Say(pc, 131, "啊啊啊啊!?$R;" +
                                        "$R不是有安全又可以安心的成分嗎…$R;");
                                    //PETCHANGE TRANSFORM 10019203 ALL
                                    break;
                                case 2:
                                    Say(pc, 131, "是免費阿!?$R啊啊……$R;");
                                    break;
                            }
                            break;
                        case 2:
                            Say(pc, 131, "是免費阿!?$R啊啊……$R;");
                            break;
                    }
                    return;
                }
            }
            if (mask.Test(PSTJDFlags.栽培俱樂部))
            {
                switch (Select(pc, "歡迎來到栽培俱樂部", "", "去秘密的場所!", "現在不去"))
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
                Say(pc, 131, "喜歡『栽培』的人聚集的$R;" +
                    "生產系專用『栽培俱樂部』$R;" +
                    "你也要加入看看嗎?$R;" +
                    "$R入會費只是『黃麥子』20個!$R;" +
                    "$P加入栽培俱樂部的條件$R;" +
                    "就是『喜歡栽培的人』!$R;" +
                    "$R會告訴你對栽培特別合適的秘密場所$R;");
                switch (Select(pc, "加入嗎?", "", "不加入", "加入!"))
                {
                    case 1:
                        break;
                    case 2:
                        if (CountItem(pc, 10005900) >= 20)
                        {
                            mask.SetValue(PSTJDFlags.栽培俱樂部, true);
                            TakeItem(pc, 10005900, 20);
                            Say(pc, 131, "謝謝~!!$R;" +
                                "$R現在開始只要跟我説話$R;" +
                                "馬上給你往秘密的場所的指引!$R;");
                            switch (Select(pc, "歡迎來到栽培俱樂部", "", "去秘密的場所!", "現在不去"))
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
                        Say(pc, 131, "入會費是『黃麥子』20個$R;" +
                            "$R什麽時候都可以接受申請$R;" +
                            "所以什麽時候來都可以$R;");
                        break;
                }
                return;
            }
            Say(pc, 131, "你好!!天氣好啊!$R;" +
                "$R太陽微笑著，農作物也長得很好!$R;");
        }
    }
}
