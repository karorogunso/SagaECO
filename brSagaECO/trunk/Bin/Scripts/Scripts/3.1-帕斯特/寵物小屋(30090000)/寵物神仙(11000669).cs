using System;
using System.Collections.Generic;
using System.Text;
using SagaLib;
using SagaDB.Actor;
using SagaMap.Scripting;
using SagaScript.Chinese.Enums;
namespace SagaScript.M30090000
{
    public class S11000669 : Event
    {
        public S11000669()
        {
            this.EventID = 11000669;
        }

        public override void OnEvent(ActorPC pc)
        {
            BitMask<mogenmogen> mogenmogen_mask = pc.CMask["mogenmogen"];
            BitMask<mogenmogen> mogenmogen_Amask = pc.AMask["mogenmogen"];

            Say(pc, 131, "回去吧$R;");

            if (!mogenmogen_Amask.Test(mogenmogen.获得宠物))
            {
                if (mogenmogen_mask.Test(mogenmogen.与宠物神仙对话))
                {
                    获得宠物(pc);
                    return;
                }
                if (mogenmogen_mask.Test(mogenmogen.请求完成))
                {
                    mogenmogen_mask.SetValue(mogenmogen.与宠物神仙对话, true);
                    ShowEffect(pc, 11000669, 4124);
                    Say(pc, 131, "啊，聽到了$R;" +
                        "$R聽到世界的爬爬蟲凱莉娥$R;" +
                        "興高采烈的歡呼聲呀$R;");
                    PlaySound(pc, 2643, false, 100, 50);
                    Say(pc, 131, "他是好人！$R;" +
                        "爺爺，相信我吧！$R;", "哈娜（心靈感應）");
                    PlaySound(pc, 2642, false, 100, 50);
                    Say(pc, 131, "給了我「洋白菜」唷$R;", "莫波（心靈感應）");
                    PlaySound(pc, 2643, false, 100, 50);
                    Say(pc, 131, "他看了看我的孩子們就來了$R;" +
                        "相信我吧…$R;", "摩根摩根（心靈感應）");

                    Say(pc, 131, "是嗎？原來是這樣呀$R;" +
                        "$R爬爬蟲凱莉娥$R;" +
                        "就像我孫子一樣的小傢伙$R;" +
                        "$P好像給您添麻煩了$R;" +
                        "為了報恩就給您一隻$R;" +
                        "爬爬蟲凱莉娥吧$R;");
                    获得宠物(pc);
                    return;
                }
                Say(pc, 131, "回去吧$R;");
                return;
            }

            Say(pc, 131, "呵呵呵…$R;");

            switch (Select(pc, "想怎麼做呢？", "", "治療寵物", "寵物的前世", "什麼也不做"))
            {
                case 1:
                    PetRecover(pc, 3);
                    break;
                case 2:
                    Say(pc, 131, "……$R;");
                    break;
            }
            //*/
        }

        void 获得宠物(ActorPC pc)
        {
            BitMask<mogenmogen> mogenmogen_Amask = pc.AMask["mogenmogen"];
            int a;
            if (pc.JobBasic != PC_JOB.MERCHANT)
            {
                a = Global.Random.Next(1, 8);
                switch (a)
                {
                    case 1:
                        if (CheckInventory(pc, 10007589, 1))
                        {
                            mogenmogen_Amask.SetValue(mogenmogen.获得宠物, true);
                            GiveItem(pc, 10007589, 1);
                            PlaySound(pc, 2040, false, 100, 50);
                            Say(pc, 131, "得到了迷你凱莉娥 ！$R;");
                            Say(pc, 131, "好好珍惜呀$R;");
                            return;
                        }
                        Say(pc, 131, "減少行李後，再來吧$R;");
                        break;
                    case 2:
                        if (CheckInventory(pc, 10007590, 1))
                        {
                            mogenmogen_Amask.SetValue(mogenmogen.获得宠物, true);
                            GiveItem(pc, 10007590, 1);
                            PlaySound(pc, 2040, false, 100, 50);
                            Say(pc, 131, "得到了迷你凱莉娥！$R;");
                            Say(pc, 131, "好好珍惜呀$R;");
                            return;
                        }
                        Say(pc, 131, "減少行李後，再來吧$R;");
                        break;
                    case 3:
                        if (CheckInventory(pc, 10007591, 1))
                        {
                            mogenmogen_Amask.SetValue(mogenmogen.获得宠物, true);
                            GiveItem(pc, 10007591, 1);
                            PlaySound(pc, 2040, false, 100, 50);
                            Say(pc, 131, "得到了迷你凱莉娥！$R;");
                            Say(pc, 131, "好好珍惜呀$R;");
                            return;
                        }
                        Say(pc, 131, "減少行李後，再來吧$R;");
                        break;
                    case 4:
                        if (CheckInventory(pc, 10007592, 1))
                        {
                            mogenmogen_Amask.SetValue(mogenmogen.获得宠物, true);
                            GiveItem(pc, 10007592, 1);
                            PlaySound(pc, 2040, false, 100, 50);
                            Say(pc, 131, "得到了迷你凱莉娥！$R;");
                            Say(pc, 131, "好好珍惜呀$R;");
                            return;
                        }
                        Say(pc, 131, "減少行李後，再來吧$R;");
                        break;
                    case 5:
                        if (CheckInventory(pc, 10007593, 1))
                        {
                            mogenmogen_Amask.SetValue(mogenmogen.获得宠物, true);
                            GiveItem(pc, 10007593, 1);
                            PlaySound(pc, 2040, false, 100, 50);
                            Say(pc, 131, "得到了迷你凱莉娥！$R;");
                            Say(pc, 131, "好好珍惜呀$R;");
                            return;
                        }
                        Say(pc, 131, "減少行李後，再來吧$R;");
                        break;
                    case 6:
                        if (CheckInventory(pc, 10007594, 1))
                        {
                            mogenmogen_Amask.SetValue(mogenmogen.获得宠物, true);
                            GiveItem(pc, 10007594, 1);
                            PlaySound(pc, 2040, false, 100, 50);
                            Say(pc, 131, "得到了迷你凱莉娥！$R;");
                            Say(pc, 131, "好好珍惜呀$R;");
                            return;
                        }
                        Say(pc, 131, "減少行李後，再來吧$R;");
                        break;
                    case 7:
                        if (CheckInventory(pc, 10007595, 1))
                        {
                            mogenmogen_Amask.SetValue(mogenmogen.获得宠物, true);
                            GiveItem(pc, 10007595, 1);
                            PlaySound(pc, 2040, false, 100, 50);
                            Say(pc, 131, "得到了迷你凱莉娥！$R;");
                            Say(pc, 131, "好好珍惜呀$R;");
                            return;
                        }
                        Say(pc, 131, "減少行李後，再來吧$R;");
                        break;
                    case 8:
                        if (CheckInventory(pc, 10007596, 1))
                        {
                            mogenmogen_Amask.SetValue(mogenmogen.获得宠物, true);
                            GiveItem(pc, 10007596, 1);
                            PlaySound(pc, 2040, false, 100, 50);
                            Say(pc, 131, "得到了迷你凱莉娥！$R;");
                            Say(pc, 131, "好好珍惜呀$R;");
                            return;
                        }
                        Say(pc, 131, "減少行李後，再來吧$R;");
                        break;
                }
                return;
            }
            a = Global.Random.Next(1, 8);
            switch (a)
            {
                case 1:
                    if (CheckInventory(pc, 10007581, 1))
                    {
                        mogenmogen_Amask.SetValue(mogenmogen.获得宠物, true);
                        GiveItem(pc, 10007581, 1);
                        PlaySound(pc, 2040, false, 100, 50);
                        Say(pc, 131, "得到了爬爬蟲凱莉娥！$R;");
                        Say(pc, 131, "好好珍惜呀$R;");
                        return;
                    }
                    Say(pc, 131, "減少行李後，再來吧$R;");
                    break;
                case 2:
                    if (CheckInventory(pc, 10007582, 1))
                    {
                        mogenmogen_Amask.SetValue(mogenmogen.获得宠物, true);
                        GiveItem(pc, 10007582, 1);
                        PlaySound(pc, 2040, false, 100, 50);
                        Say(pc, 131, "得到了爬爬蟲凱莉娥！$R;");
                        Say(pc, 131, "好好珍惜呀$R;");
                        return;
                    }
                    Say(pc, 131, "減少行李後，再來吧$R;");
                    break;
                case 3:
                    if (CheckInventory(pc, 10007583, 1))
                    {
                        mogenmogen_Amask.SetValue(mogenmogen.获得宠物, true);
                        GiveItem(pc, 10007583, 1);
                        PlaySound(pc, 2040, false, 100, 50);
                        Say(pc, 131, "得到了爬爬蟲凱莉娥！$R;");
                        Say(pc, 131, "好好珍惜呀$R;");
                        return;
                    }
                    Say(pc, 131, "減少行李後，再來吧$R;");
                    break;
                case 4:
                    if (CheckInventory(pc, 10007584, 1))
                    {
                        mogenmogen_Amask.SetValue(mogenmogen.获得宠物, true);
                        GiveItem(pc, 10007584, 1);
                        PlaySound(pc, 2040, false, 100, 50);
                        Say(pc, 131, "得到了爬爬蟲凱莉娥！$R;");
                        Say(pc, 131, "好好珍惜呀$R;");
                        return;
                    }
                    Say(pc, 131, "減少行李後，再來吧$R;");
                    break;
                case 5:
                    if (CheckInventory(pc, 10007585, 1))
                    {
                        mogenmogen_Amask.SetValue(mogenmogen.获得宠物, true);
                        GiveItem(pc, 10007585, 1);
                        PlaySound(pc, 2040, false, 100, 50);
                        Say(pc, 131, "得到了爬爬蟲凱莉娥！$R;");
                        Say(pc, 131, "好好珍惜呀$R;");
                        return;
                    }
                    Say(pc, 131, "減少行李後，再來吧$R;");
                    break;
                case 6:
                    if (CheckInventory(pc, 10007586, 1))
                    {
                        mogenmogen_Amask.SetValue(mogenmogen.获得宠物, true);
                        GiveItem(pc, 10007586, 1);
                        PlaySound(pc, 2040, false, 100, 50);
                        Say(pc, 131, "得到了爬爬蟲凱莉娥！$R;");
                        Say(pc, 131, "好好珍惜呀$R;");
                        return;
                    }
                    Say(pc, 131, "減少行李後，再來吧$R;");
                    break;
                case 7:
                    if (CheckInventory(pc, 10007587, 1))
                    {
                        mogenmogen_Amask.SetValue(mogenmogen.获得宠物, true);
                        GiveItem(pc, 10007587, 1);
                        PlaySound(pc, 2040, false, 100, 50);
                        Say(pc, 131, "得到了爬爬蟲凱莉娥！$R;");
                        Say(pc, 131, "好好珍惜呀$R;");
                        return;
                    }
                    Say(pc, 131, "減少行李後，再來吧$R;");
                    break;
                case 8:
                    if (CheckInventory(pc, 10007588, 1))
                    {
                        mogenmogen_Amask.SetValue(mogenmogen.获得宠物, true);
                        GiveItem(pc, 10007588, 1);
                        PlaySound(pc, 2040, false, 100, 50);
                        Say(pc, 131, "得到了爬爬蟲凱莉娥！$R;");
                        Say(pc, 131, "好好珍惜呀$R;");
                        return;
                    }
                    Say(pc, 131, "減少行李後，再來吧$R;");
                    break;
            }
        }
    }
}