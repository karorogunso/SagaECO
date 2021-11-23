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

            Say(pc, 11000080, 131, "什么东西忘了?$R;", "健忘的老人");
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
                Say(pc, 11000080, 131, "快回去找商人总管喔!$R", "健忘的老人");
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
                Say(pc, 0, 0, "把『信 (任务)』交给他。$R;", " ");

                Say(pc, 11000080, 131, "啊! 大事不妙!!$R;" +
                                       "$P好像把钱包放在我孩子那，$R;" +
                                       "有没有人帮我去拿啊?$R;", "健忘的老人");
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
                Say(pc, 0, 0, "把『老爷爷的钱包』交给他。$R;", " ");

                Say(pc, 11000080, 131, "噢噢! 这不是我的钱包吗!$R;" +
                                       "特意帮我拿过来的吗?$R;" +
                                       "真是善良的孩子啊!$R;" +
                                       "$R因为没有这个，连茶都没能喝啊!$R;" +
                                       "真是感谢啊…$R;" +
                                       "$P呼…连自己出汗了都不知道，$R;" +
                                       "…等一下，先擦一下汗…$R;" +
                                       "$R啊!?$R;" +
                                       "啊! 大事不妙!!$R;" +
                                       "手帕放在孙子的地方啊!$R;" +
                                       "$R有没有人帮我去拿啊?$R;", "健忘的老人");
            }
            else
            {
                Say(pc, 11000080, 131, "啊啊…我要的东西还没拿到吗?$R;", "健忘的老人");            
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
                Say(pc, 0, 0, "把『老爷爷的手帕』交给他。$R;", " ");

                Say(pc, 11000080, 131, "噢噢! 这不是我的手帕吗?$R;" +
                                       "特意帮我拿过来的吗?$R;" +
                                       "真是亲切的孩子啊!$R;" +
                                       "$R没有手帕擦不了汗啊?$R;" +
                                       "真是感谢啊…$R;" +
                                       "$P呼…真费神，$R;" +
                                       "突然很疲劳啊。$R;" +
                                       "$R啊!?$R;" +
                                       "$P不好了! 不好了!!$R;" +
                                       "魔杖好像放在朋友家里忘记拿了。$R;" +
                                       "$R有没有人帮我去拿啊?$R;", "健忘的老人");
            }
            else
            {
                Say(pc, 11000080, 131, "啊啊…我要的东西还没拿到吗?$R;", "健忘的老人");
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
                Say(pc, 0, 0, "把『老爷爷的魔杖』交给他。$R;", " ");

                Say(pc, 11000080, 131, "这是我的魔杖!$R;" +
                                       "特意给我拿过来的吗?$R;" +
                                       "真是善良的孩子啊!$R;" +
                                       "$R因为没有魔杖而辛苦了呢，$R;" +
                                       "真是感谢啊…$R;" +
                                       "$P呼…让我这下慢慢的看一下女孩子…$R;" +
                                       "$R啊!?$R;" +
                                       "$P这下出大事了，$R;" +
                                       "眼镜好像放在部下那里了啊!$R;" +
                                       "$R有没有人帮我去拿啊?$R;", "健忘的老人");
            }
            else
            {
                Say(pc, 11000080, 131, "啊啊…我要的东西还没拿到吗?$R;", "健忘的老人");
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
                Say(pc, 0, 0, "把『老爷爷的眼镜』交给他。$R;", " ");

                Say(pc, 11000080, 131, "哦! 这个不是我的眼镜吗?!$R;" +
                                       "为了我，这样辛苦地拿过来啊?$R;" +
                                       "真是善良的孩子啊!$R;" +
                                       "$R因为没有眼镜而辛苦了呢，$R;" +
                                       "真是感谢啊…$R;" +
                                       "$P呼…这下没有什么忘掉的吧?$R;" +
                                       "我是这样完美的人啊…$R;" +
                                       "$R啊!?$R;" +
                                       "$P这下出大事了，$R;" +
                                       "我最喜爱的裤子，儿媳妇说要洗，$R;" +
                                       "还没洗完啊?$R;" +
                                       "$R能不能帮我去拿啊?$R;", "健忘的老人");
            }
            else
            {
                Say(pc, 11000080, 131, "啊啊…我要的东西还没拿到吗?$R;", "健忘的老人");
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
                Say(pc, 0, 0, "把『老爷爷的裤子』交给他。$R;", " ");

                Say(pc, 11000080, 131, "啊 咿喔 喔喔喔喔$R;" +
                                       "啊 喔喔 啊咇咇$R;" +
                                       "啊咇咇咇 啊咇 啊咇咇$R;" +
                                       "$R呜喔 喔喔喔 呜咇咇咇?$R;" +
                                       "啊喔喔 啊咇咇$R;" +
                                       "$P呜叭叭 呜啊啊啊$R;" +
                                       "$R呜喔?$R;" +
                                       "$P咇咇 呜咇咇…呜啊喔喔$R;" +
                                       "呜喔 喔喔喔 呜咇咇咇?$R;" +
                                       "啊咇咇咇 啊咇 啊咇咇$R;" +
                                       "$R呜咇…咇咇…呜咇咇咇咇$R;", "健忘的老人");
            }
            else
            {
                Say(pc, 11000080, 131, "呜喔…呜啊喔喔呜咇咇咇呜?$R;", "健忘的老人");
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
                Say(pc, 0, 0, "把『老爷爷的假牙』交给他。$R;", " ");

                Say(pc, 11000080, 131, "啊啊…真的很感谢啊!$R;" +
                                       "现在好像没什么缺的了。$R;" +
                                       "$R但是跟我有什么关系吗?$R;" +
                                       "$R你刚刚给我的商人总管的信?$R;" +
                                       "哈…我都忘记了…待我看一下…$R;" +
                                       "$R这傢伙把印章掉在我家?!$R;" +
                                       "$P真是粗心大意的傢伙…$R;" +
                                       "$R不好意思……$R;" +
                                       "帮我把印章转交给总管可以吗?$R;", "健忘的老人");

                PlaySound(pc, 2040, false, 100, 50);
                GiveItem(pc, 80008500, 1);
                Say(pc, 0, 0, "得到『商人的图章』!$R;", " ");
            }
            else
            {
                Say(pc, 11000080, 131, "啊啊…我要的东西还没拿到吗?$R;", "健忘的老人");
            }
        }
    }
}
