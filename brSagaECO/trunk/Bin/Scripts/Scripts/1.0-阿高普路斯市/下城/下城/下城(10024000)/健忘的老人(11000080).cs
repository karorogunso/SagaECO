using System;
using System.Collections.Generic;
using System.Text;

using SagaDB.Actor;
using SagaMap.Scripting;

using SagaLib;
using SagaScript.Chinese.Enums;
//所在地圖:下城(10024000) NPC基本信息:健忘的老人(11000080) X:201 Y:133
namespace SagaScript.M10024000
{
    public class S11000080 : Event
    {
        public S11000080()
        {
            this.EventID = 11000080;
        }

        public override void OnEvent(ActorPC pc)
        {
            BitMask<JobBasic_12> JobBasic_12_mask = new BitMask<JobBasic_12>(pc.CMask["JobBasic_12"]);

            if (JobBasic_12_mask.Test(JobBasic_12.選擇轉職為商人) &&
                !JobBasic_12_mask.Test(JobBasic_12.商人轉職任務完成))
            {
                商人轉職任務(pc);
                return;
            }

            Say(pc, 11000080, 131, "什麼東西忘了?$R;", "健忘的老人");
        }

        void 商人轉職任務(ActorPC pc)
        {
            BitMask<JobBasic_12> JobBasic_12_mask = new BitMask<JobBasic_12>(pc.CMask["JobBasic_12"]);

            if (!JobBasic_12_mask.Test(JobBasic_12.轉交商人總管的信))
            {
                轉交商人總管的信(pc);
                return;
            }

            if (!JobBasic_12_mask.Test(JobBasic_12.給予老爺爺的錢包))
            {
                給予老爺爺的錢包(pc);
                return;
            }

            if (!JobBasic_12_mask.Test(JobBasic_12.給予老爺爺的手帕))
            {
                給予老爺爺的手帕(pc);
                return;
            }

            if (!JobBasic_12_mask.Test(JobBasic_12.給予老爺爺的魔杖))
            {
                給予老爺爺的魔杖(pc);
                return;
            }

            if (!JobBasic_12_mask.Test(JobBasic_12.給予老爺爺的眼鏡))
            {
                給予老爺爺的眼鏡(pc);
                return;
            }

            if (!JobBasic_12_mask.Test(JobBasic_12.給予老爺爺的褲子))
            {
                給予老爺爺的褲子(pc);
                return;
            }

            if (!JobBasic_12_mask.Test(JobBasic_12.給予老爺爺的假牙))
            {
                給予老爺爺的假牙(pc);
                return;
            }

            if (JobBasic_12_mask.Test(JobBasic_12.給予老爺爺的假牙) &&
                CountItem(pc, 80008500) > 0)
            {
                Say(pc, 11000080, 131, "快回去找商人總管喔!$R", "健忘的老人");
                return;
            }
        }

        void 轉交商人總管的信(ActorPC pc)
        {
            BitMask<JobBasic_12> JobBasic_12_mask = new BitMask<JobBasic_12>(pc.CMask["JobBasic_12"]);

            if (CountItem(pc, 10043080) > 0)
            {
                JobBasic_12_mask.SetValue(JobBasic_12.轉交商人總管的信, true);

                PlaySound(pc, 2040, false, 100, 50);
                TakeItem(pc, 10043080, 1);
                Say(pc, 0, 0, "把『信 (任務)』交給他。$R;", " ");

                Say(pc, 11000080, 131, "啊! 大事不妙!!$R;" +
                                       "$P好像把錢包放在我孩子那，$R;" +
                                       "有沒有人幫我去拿啊?$R;", "健忘的老人");
            }
        }

        void 給予老爺爺的錢包(ActorPC pc)
        {
            BitMask<JobBasic_12> JobBasic_12_mask = new BitMask<JobBasic_12>(pc.CMask["JobBasic_12"]);

            if (CountItem(pc, 80002600) > 0)
            {
                JobBasic_12_mask.SetValue(JobBasic_12.給予老爺爺的錢包, true);

                PlaySound(pc, 2040, false, 100, 50);
                TakeItem(pc, 80002600, 1);
                Say(pc, 0, 0, "把『老爺爺的錢包』交給他。$R;", " ");

                Say(pc, 11000080, 131, "噢噢! 這不是我的錢包嗎!$R;" +
                                       "特意幫我拿過來的嗎?$R;" +
                                       "真是善良的孩子啊!$R;" +
                                       "$R因為沒有這個，連茶都沒能喝啊!$R;" +
                                       "真是感謝啊…$R;" +
                                       "$P呼…連自己出汗了都不知道，$R;" +
                                       "…等一下，先擦一下汗…$R;" +
                                       "$R啊!?$R;" +
                                       "啊! 大事不妙!!$R;" +
                                       "手帕放在孫子的地方啊!$R;" +
                                       "$R有沒有人幫我去拿啊?$R;", "健忘的老人");
            }
            else
            {
                Say(pc, 11000080, 131, "啊啊…我要的東西還沒拿到嗎?$R;", "健忘的老人");            
            }
        }

        void 給予老爺爺的手帕(ActorPC pc)
        {
            BitMask<JobBasic_12> JobBasic_12_mask = new BitMask<JobBasic_12>(pc.CMask["JobBasic_12"]);

            if (CountItem(pc, 80021300) > 0)
            {
                JobBasic_12_mask.SetValue(JobBasic_12.給予老爺爺的手帕, true);

                PlaySound(pc, 2040, false, 100, 50);
                TakeItem(pc, 80021300, 1);
                Say(pc, 0, 0, "把『老爺爺的手帕』交給他。$R;", " ");

                Say(pc, 11000080, 131, "噢噢! 這不是我的手帕嗎?$R;" +
                                       "特意幫我拿過來的嗎?$R;" +
                                       "真是親切的孩子啊!$R;" +
                                       "$R沒有手帕擦不了汗啊?$R;" +
                                       "真是感謝啊…$R;" +
                                       "$P呼…真費神，$R;" +
                                       "突然很疲勞啊。$R;" +
                                       "$R啊!?$R;" +
                                       "$P不好了! 不好了!!$R;" +
                                       "魔杖好像放在朋友家裡忘記拿了。$R;" +
                                       "$R有沒有人幫我去拿啊?$R;", "健忘的老人");
            }
            else
            {
                Say(pc, 11000080, 131, "啊啊…我要的東西還沒拿到嗎?$R;", "健忘的老人");
            }
        }

        void 給予老爺爺的魔杖(ActorPC pc)
        {
            BitMask<JobBasic_12> JobBasic_12_mask = new BitMask<JobBasic_12>(pc.CMask["JobBasic_12"]);

            if (CountItem(pc, 80040900) > 0)
            {
                JobBasic_12_mask.SetValue(JobBasic_12.給予老爺爺的魔杖, true);

                PlaySound(pc, 2040, false, 100, 50);
                TakeItem(pc, 80040900, 1);
                Say(pc, 0, 0, "把『老爺爺的魔杖』交給他。$R;", " ");

                Say(pc, 11000080, 131, "這是我的魔杖!$R;" +
                                       "特意給我拿過來的嗎?$R;" +
                                       "真是善良的孩子啊!$R;" +
                                       "$R因為沒有魔杖而辛苦了呢，$R;" +
                                       "真是感謝啊…$R;" +
                                       "$P呼…讓我這下慢慢的看一下女孩子…$R;" +
                                       "$R啊!?$R;" +
                                       "$P這下出大事了，$R;" +
                                       "眼鏡好像放在部下那裡了啊!$R;" +
                                       "$R有沒有人幫我去拿啊?$R;", "健忘的老人");
            }
            else
            {
                Say(pc, 11000080, 131, "啊啊…我要的東西還沒拿到嗎?$R;", "健忘的老人");
            }
        }

        void 給予老爺爺的眼鏡(ActorPC pc)
        {
            BitMask<JobBasic_12> JobBasic_12_mask = new BitMask<JobBasic_12>(pc.CMask["JobBasic_12"]);

            if (CountItem(pc, 80040800) > 0)
            {
                JobBasic_12_mask.SetValue(JobBasic_12.給予老爺爺的眼鏡, true);

                PlaySound(pc, 2040, false, 100, 50);
                TakeItem(pc, 80040800, 1);
                Say(pc, 0, 0, "把『老爺爺的眼鏡』交給他。$R;", " ");

                Say(pc, 11000080, 131, "哦! 這個不是我的眼鏡嗎?!$R;" +
                                       "為了我，這樣辛苦地拿過來啊?$R;" +
                                       "真是善良的孩子啊!$R;" +
                                       "$R因為沒有眼鏡而辛苦了呢，$R;" +
                                       "真是感謝啊…$R;" +
                                       "$P呼…這下沒有什麼忘掉的吧?$R;" +
                                       "我是這樣完美的人啊…$R;" +
                                       "$R啊!?$R;" +
                                       "$P這下出大事了，$R;" +
                                       "我最喜愛的褲子，兒媳婦說要洗，$R;" +
                                       "還沒洗完啊?$R;" +
                                       "$R能不能幫我去拿啊?$R;", "健忘的老人");
            }
            else
            {
                Say(pc, 11000080, 131, "啊啊…我要的東西還沒拿到嗎?$R;", "健忘的老人");
            }
        }

        void 給予老爺爺的褲子(ActorPC pc)
        {
            BitMask<JobBasic_12> JobBasic_12_mask = new BitMask<JobBasic_12>(pc.CMask["JobBasic_12"]);

            if (CountItem(pc, 80010100) > 0)
            {
                JobBasic_12_mask.SetValue(JobBasic_12.給予老爺爺的褲子, true);

                PlaySound(pc, 2040, false, 100, 50);
                TakeItem(pc, 80010100, 1);
                Say(pc, 0, 0, "把『老爺爺的褲子』交給他。$R;", " ");

                Say(pc, 11000080, 131, "啊 咿喔 喔喔喔喔$R;" +
                                       "啊 喔喔 啊咇咇$R;" +
                                       "啊咇咇咇 啊咇 啊咇咇$R;" +
                                       "$R嗚喔 喔喔喔 嗚咇咇咇?$R;" +
                                       "啊喔喔 啊咇咇$R;" +
                                       "$P嗚叭叭 嗚啊啊啊$R;" +
                                       "$R嗚喔?$R;" +
                                       "$P咇咇 嗚咇咇…嗚啊喔喔$R;" +
                                       "嗚喔 喔喔喔 嗚咇咇咇?$R;" +
                                       "啊咇咇咇 啊咇 啊咇咇$R;" +
                                       "$R嗚咇…咇咇…嗚咇咇咇咇$R;", "健忘的老人");
            }
            else
            {
                Say(pc, 11000080, 131, "啊啊…我要的東西還沒拿到嗎?$R;", "健忘的老人");
            }
        }

        void 給予老爺爺的假牙(ActorPC pc)
        {
            BitMask<JobBasic_12> JobBasic_12_mask = new BitMask<JobBasic_12>(pc.CMask["JobBasic_12"]);

            if (CountItem(pc, 80022000) > 0)
            {
                JobBasic_12_mask.SetValue(JobBasic_12.給予老爺爺的假牙, true);

                PlaySound(pc, 2040, false, 100, 50);
                TakeItem(pc, 80022000, 1);
                Say(pc, 0, 0, "把『老爺爺的假牙』交給他。$R;", " ");

                Say(pc, 11000080, 131, "啊啊…真的很感謝啊!$R;" +
                                       "現在好像沒什麼缺的了。$R;" +
                                       "$R但是跟我有什麼關係嗎?$R;" +
                                       "$R你剛剛給我的商人總管的信?$R;" +
                                       "哈…我都忘記了…待我看一下…$R;" +
                                       "$R這傢伙把印章掉在我家?!$R;" +
                                       "$P真是粗心大意的傢伙…$R;" +
                                       "$R不好意思……$R;" +
                                       "幫我把印章轉交給總管可以嗎?$R;", "健忘的老人");

                PlaySound(pc, 2040, false, 100, 50);
                GiveItem(pc, 80008500, 1);
                Say(pc, 0, 0, "得到『商人的圖章』!$R;", " ");
            }
            else
            {
                Say(pc, 11000080, 131, "啊啊…我要的東西還沒拿到嗎?$R;", "健忘的老人");
            }
        }
    }
}
