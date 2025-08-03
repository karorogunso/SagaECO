using System;
using System.Collections.Generic;
using System.Text;

using SagaDB.Actor;
using SagaMap.Scripting;

using SagaLib;
using SagaScript.Chinese.Enums;
//所在地圖:黑之聖堂(30121000) NPC基本信息:黑之聖堂祭司(11000042) X:8 Y:7
namespace SagaScript.M30121000
{
    public class S11000042 : Event
    {
        public S11000042()
        {
            this.EventID = 11000042;
        }

        public override void OnEvent(ActorPC pc)
        {
            BitMask<Acropolisut_01> Acropolisut_01_mask = new BitMask<Acropolisut_01>(pc.CMask["Acropolisut_01"]);
            BitMask<JobBasic_08> JobBasic_08_mask = new BitMask<JobBasic_08>(pc.CMask["JobBasic_08"]);
            BitMask<Puppet_01> Puppet_01_mask = pc.CMask["Puppet_01"];  


            Say(pc, 11000042, 131, "……$R;" +
                                   "$P这里是黑之圣堂，$R;" +
                                   "是暗术使行会的本部。$R;", "黑之圣堂祭司");

            if (CountItem(pc, 10047201) >= 1)
            {
                Say(pc, 131, "……$R;" +
                    "$P想给『心之容器』里注入心??$R;" +
                    "$R呵呵呵…$R;" +
                    "要支付代价吧?$R;");
                if (Select(pc, "要付出什么呢?", "", "攻击１", "魔法１", "灵活１", "放弃") != 4)
                {
                    Puppet_01_mask.SetValue(Puppet_01.聖堂祭司給予洋鐵的心, true);
                    //_3A24 = true;
                    TakeItem(pc, 10047201, 1);
                    GiveItem(pc, 10047200, 1);
                    Say(pc, 131, "嗯…$R;" +
                        "既然您已经做好了准备，那就收下吧$R;");
                    PlaySound(pc, 4006, false, 100, 50);
                    Say(pc, 131, "得到了『洋铁的心』！$R;");
                    Say(pc, 131, "我很喜欢全力以赴努力不懈的态度$R;" +
                        "所以没有额外的要求$R;" +
                        "$R这次就算是我半买半送的吧$R;");
                }
                return;
            }

            if (JobBasic_08_mask.Test(JobBasic_08.魔攻師轉職成功) &&
                !JobBasic_08_mask.Test(JobBasic_08.已經轉職為魔攻師))
            {
                魔攻師轉職完成(pc);
                return;
            }

            if (pc.Job == PC_JOB.NOVICE)
            {
                if (JobBasic_08_mask.Test(JobBasic_08.選擇轉職為魔攻師) &&
                    !JobBasic_08_mask.Test(JobBasic_08.已經轉職為魔攻師))
                {
                    魔攻師轉職任務(pc);
                    return;
                }
                else
                {
                    魔攻師簡介(pc);
                    return;
                }
            }

            if (pc.JobBasic == PC_JOB.WARLOCK)
            {
                Say(pc, 11000042, 131, pc.Name + "……$R;" +
                                       "……什么事情啊?$R;", "黑之圣堂祭司");
                switch (Select(pc, "做什么呢?", "", "任务服务台", "购买诺森入国许可证", "转职", "什么都不做"))
                {
                    case 1:
                        if (Acropolisut_01_mask.Test(Acropolisut_01.已經與黑之聖堂祭司詢問過任務服務台))
                        {
                            Acropolisut_01_mask.SetValue(Acropolisut_01.已經與黑之聖堂祭司詢問過任務服務台, true);

                            Say(pc, 11000042, 131, "……$R;" +
                                                   "$P给属于行会的人们，$R;" +
                                                   "介绍各种的事情…$R;" +
                                                   "$P失败后知道会怎么样吧?$R;" +
                                                   "$R他实在过於自负。$R;", "黑之圣堂祭司");
                        }
                        else
                        {
                            Say(pc, 11000042, 131, "……$R;" +
                                                   "已经好了…$R;", "黑之圣堂祭司");

                            HandleQuest(pc, 12);
                        }
                        break;
                    case 2:
                        Say(pc, 131, "到诺森去？$R;");
                        break;
                    case 3:
                        進階轉職(pc);
                        OpenShopBuy(pc, 82);
                        break;
                    case 4:
                        break;
                }
                return;
            }
            Say(pc, 131, "……已經晚了……一切都太遲了…$R;");
        }

        void 魔攻師簡介(ActorPC pc)
        {
            BitMask<JobBasic_08> JobBasic_08_mask = new BitMask<JobBasic_08>(pc.CMask["JobBasic_08"]);

            int selection;

            Say(pc, 11000042, 131, "我是…$R;" +
                                   "$R管理暗术使行会的暗术使总管。$R;" +
                                   "$P您是初心者吧?$R;", "黑之圣堂祭司");

            selection = Select(pc, "想做什么?", "", "我想成为『暗术使』!", "『暗术使』是什么样的职业?", "任务服务台", "什么也不做");

            while (selection != 4)
            {

                switch (selection)
                {
                    case 1:
                        JobBasic_08_mask.SetValue(JobBasic_08.選擇轉職為魔攻師, true);

                        if (pc.Race == PC_RACE.TITANIA)
                        {
                            if (!JobBasic_08_mask.Test(JobBasic_08.已經從闇之精靈那裡把心染為黑暗))
                            {
                                Say(pc, 11000042, 131, "想成为合格的暗术使，$R;" +
                                                       "必须在内心里蕴藏著「暗属性」。$R;" +
                                                       "$P去求见存在这世界上，$R;" +
                                                       "某个角落的「暗之精灵」，$R;" +
                                                       "赐予您「暗属性」之后，$R;" +
                                                       "再来找我吧…$R;", "黑之圣堂祭司");
                            }
                            else
                            {
                                Say(pc, 11000042, 131, "……$R;" +
                                                       "$P您明白了吗?$R;", "黑之圣堂祭司");

                                Say(pc, 11000042, 131, "这个城市的「下城」里，$R;" +
                                                       "有个熟悉「黑暗魔法」的人。$R;" +
                                                       "$P他的名字叫「黑百特」。$R;" +
                                                       "$R先去见过那个傢伙后，$R;" +
                                                       "再回来找我吧!$R;", "黑之圣堂祭司");
                            }
                        }
                        else
                        {
                            Say(pc, 11000042, 131, "这个城市的「下城」里，$R;" +
                                                   "有个熟悉「黑暗魔法」的人。$R;" +
                                                   "$P他的名字叫「布莱克」。$R;" +
                                                   "$R先去见过那个傢伙后，$R;" +
                                                   "再回来找我吧!$R;", "黑之圣堂祭司");                      
                        }
                        return;

                    case 2:
                        Say(pc, 11000042, 131, "……$R;" +
                                               "$P我们是属于黑暗的魔法师…$R;" +
                                               "$R暗术使比较适合泰达尼亚或多米尼翁，$R;" +
                                               "这种魔力较高的种族。$R;" +
                                               "$P特别是多米尼翁，最占优势!$R;" +
                                               "因为他们先天上就具有「暗属性」…$R;" +
                                               "$R所以暗术使中多米尼翁比较多…$R;" +
                                               "$P相反的，多米尼翁以外的种族，$R;" +
                                               "要成为暗术使，可是吃了不少苦呢!$R;" +
                                               "$R您明白了吗?$R;", "黑之圣堂祭司");

                        switch (Select(pc, "还有疑问吗?", "", "能再详细的说明一次吗?", "我知道了"))
                        {
                            case 1:
                                Say(pc, 11000042, 131, "『暗术使』是使用「黑暗魔法」的职业。$R;" +
                                                       "$P在「黑暗魔法」中，攻击性的魔法比较多。$R;" +
                                                       "$R在远距离使用「黑暗魔法」的话，$R;" +
                                                       "您可以在不受伤的情况下，$R;" +
                                                       "轻松的取得胜利…$R;" +
                                                       "$P同时暗术使也是灵活多变的职业…$R;" +
                                                       "$R只要您喜欢，也可以拿起手中的武器$R;" +
                                                       "与敌人近距离对战来个亲密接触…$R;" +
                                                       "$P如果魔攻师选择提升自己的力量，$R;" +
                                                       "即使是不死系的魔物们都要退避三舍。$R;", "黑之圣堂祭司");
                                break;
                                
                            case 2:
                                break;
                        }
                        break;

                    case 3:
                        Say(pc, 11000042, 131, "……$R;" +
                                               "$P这里只替『暗术使』介绍任务…$R;", "黑之圣堂祭司");
                        break;
                }

                selection = Select(pc, "想做什么?", "", "我想成为『暗术使』!", "『暗术使』是什么样的职业?", "任务服务台", "什么也不做");
            } 
        }

        void 魔攻師轉職任務(ActorPC pc)
        {
            BitMask<JobBasic_08> JobBasic_08_mask = new BitMask<JobBasic_08>(pc.CMask["JobBasic_08"]);

            if (!JobBasic_08_mask.Test(JobBasic_08.魔攻師轉職任務完成))
            {
                黑暗魔法相關問題回答(pc);
            }

            if (JobBasic_08_mask.Test(JobBasic_08.魔攻師轉職任務完成) &&
                !JobBasic_08_mask.Test(JobBasic_08.魔攻師轉職成功))
            {
                申請轉職為魔攻師(pc);
                return;
            }
        }

        void 黑暗魔法相關問題回答(ActorPC pc)
        {
            BitMask<JobBasic_08> JobBasic_08_mask = new BitMask<JobBasic_08>(pc.CMask["JobBasic_08"]);

            if (JobBasic_08_mask.Test(JobBasic_08.已經從黑佰特那裡聽取有關黑暗魔法的知識))
            {
                問題01(pc);
            }
            else
            {
                Say(pc, 11000042, 131, "这个城市的「下城」里，$R;" +
                                       "有个熟悉黑暗知识的人。$R;" +
                                       "$P他的名字叫「布莱克」。$R;" +
                                       "$R先去见过那个家伙后，$R;" +
                                       "再回来找我吧!$R;" +
                                       "$P不要让我重复讲好几次…$R;", "黑之圣堂祭司");
            }
        }

        void 問題01(ActorPC pc)
        {
            Say(pc, 11000042, 131, "…$R;" +
                                   "$R好像是听到消息之后才来的啊…$R;" +
                                   "$R那么给您进行一个小小的测验吧!$R;" +
                                   "请您回答我所提出的问题。$R;" +
                                   "$P「暗属性」的相对属性是什么?$R;", "黑之圣堂祭司");

            switch (Select(pc, "「暗属性」的相对属性是什么?", "", "光属性", "圣属性", "没有"))
            {
                case 1:
                    問題回答錯誤(pc);
                    return;
                    
                case 2:
                    問題回答錯誤(pc);
                    return;

                case 3:
                    問題02(pc);
                    break;
            }
        }

        void 問題02(ActorPC pc)
        {
            PlaySound(pc, 2040, false, 100, 50);

            Say(pc, 11000042, 131, "是啊…$R;" +
                                   "$R看起来所有属性都像是有相对关系的，$R;" +
                                   "但是实际上不是那样…,;" +
                                   "$P请您回答我所提出的问题。$R;" +
                                   "$R跟『暗灵术』不相通的属性是?$R;", "黑之圣堂祭司");

            switch (Select(pc, "跟『暗灵术』不相通的属性是?", "", "光属性", "水属性", "暗属性"))
            {
                case 1:
                    問題03(pc);
                    break;
                    
                case 2:
                    問題回答錯誤(pc);
                    return;

                case 3:
                    問題回答錯誤(pc);
                    return;
            }
        }

        void 問題03(ActorPC pc)
        {
            PlaySound(pc, 2040, false, 100, 50);

            Say(pc, 11000042, 131, "答对了! 那是很常用的魔法…$R;" +
                                   "要好好的记下来喔!$R;" +
                                   "$P那下一个问题是…;" +
                                   "暗术使可以使用的装备是?$R;", "黑之圣堂祭司");

            switch (Select(pc, "暗术使可以使用的装备是?", "", "短剑", "斧头", "枪"))
            {
                case 1:
                    問題回答正確(pc);
                    break;
                    
                case 2:
                    問題回答錯誤(pc);
                    return;

                case 3:
                    問題回答錯誤(pc);
                    return;
            }
        }

        void 問題回答正確(ActorPC pc)
        {
            BitMask<JobBasic_08> JobBasic_08_mask = new BitMask<JobBasic_08>(pc.CMask["JobBasic_08"]);

            PlaySound(pc, 2040, false, 100, 50);

            Say(pc, 11000042, 131, "正确答案…$R;" +
                                   "$P嗯…好像情报收集得还不错。$R;" +
                                   "$P好吧! 就让您加入暗术使的行列…;" +
                                   "$R真的要成为『暗术使』吗?$R;", "黑之圣堂祭司");

            switch (Select(pc, "要转职为『暗术使』吗?", "", "转职为『暗术使』", "还是算了吧"))
            {
                case 1:
                    JobBasic_08_mask.SetValue(JobBasic_08.魔攻師轉職任務完成, true);
                    break;
                    
                case 2:
                    break;
            }
        }

        void 問題回答錯誤(ActorPC pc)
        {
            BitMask<JobBasic_08> JobBasic_08_mask = new BitMask<JobBasic_08>(pc.CMask["JobBasic_08"]);

            JobBasic_08_mask.SetValue(JobBasic_08.已經從黑佰特那裡聽取有關黑暗魔法的知識, false);

            PlaySound(pc, 2041, false, 100, 50);

            Say(pc, 11000042, 131, "……$R;" +
                                   "$R再回去找「布莱克」，$R;" +
                                   "更加了解后再回来吧!$R;", "黑之圣堂祭司");
        }

        void 申請轉職為魔攻師(ActorPC pc)
        {
            BitMask<JobBasic_08> JobBasic_08_mask = new BitMask<JobBasic_08>(pc.CMask["JobBasic_08"]);

            Say(pc, 11000042, 131, "这是象征『暗术使』的『暗术使纹章』，$R;" +
                                   "请您好好保管!$R;", "黑之圣堂祭司");

            if (pc.Inventory.Equipments.Count == 0)
            {
                JobBasic_08_mask.SetValue(JobBasic_08.魔攻師轉職成功, true);

                PlaySound(pc, 3087, false, 100, 50);
                ShowEffect(pc, 4131);
                Wait(pc, 3960);

                Say(pc, 11000042, 131, "……$R;" +
                                       "$P……好了，$R;" +
                                       "您已经成为了『暗术使』。$R;" +
                                       "呵呵呵…$R;", "黑之圣堂祭司");

                PlaySound(pc, 4012, false, 100, 50);
                ChangePlayerJob(pc, PC_JOB.WARLOCK);

                Say(pc, 0, 0, "您已经转职为『暗术使』了!$R;", " ");

                Say(pc, 11000042, 131, "$P给您成为黑暗伙伴的象征。$R;" +
                                       "$R先穿衣服吧…$R;", "黑之圣堂祭司");
            }
            else
            {
                Say(pc, 11000042, 131, "纹章是烙印在皮肤上的，$R;" +
                                       "先把装备脱掉吧。$R;", "黑之圣堂祭司");
            }
        }

        void 魔攻師轉職完成(ActorPC pc)
        {
            BitMask<JobBasic_08> JobBasic_08_mask = new BitMask<JobBasic_08>(pc.CMask["JobBasic_08"]);

            if (pc.Inventory.Equipments.Count != 0)
            {

                JobBasic_08_mask.SetValue(JobBasic_08.已經轉職為魔攻師, true);

                Say(pc, 11000042, 131, "给您成为黑暗伙伴的证据吧…$R;", "黑之圣堂祭司");

                PlaySound(pc, 2040, false, 100, 50);
                GiveItem(pc, 50050600, 1);
                Say(pc, 0, 0, "得到『黑暗胸针』!$R;", " ");

                LearnSkill(pc, 3083);
                Say(pc, 0, 0, "学到『暗灵术』!R;", " ");

                Say(pc, 11000042, 131, "已经无法回头了…$R;" +
                                       "$R呵呵呵…$R;", "黑之圣堂祭司");
            }
            else
            {
                Say(pc, 11000042, 131, "请先穿上衣服。$R;", "黑之圣堂祭司");
            }
        }

        void 進階轉職(ActorPC pc)
        {
            BitMask<Job2X_08> mask = pc.CMask["Job2X_08"];
            if (mask.Test(Job2X_08.轉職結束))//_3A40)
            {
                if (pc.Inventory.Equipments.Count == 0)
                {
                    Say(pc, 131, "……$R;" +
                        "$P穿衣服吧…$R;");
                    return;
                }
                Say(pc, 131, "……$R;" +
                    "$P对您还是较困难$R;");
                return;
            }
            if (pc.Job == PC_JOB.WARLOCK && pc.JobLevel1 > 29)
            {
                if (mask.Test(Job2X_08.轉職開始))//_3A39)
                {
                    if (CountItem(pc, 10034000) >= 1 && CountItem(pc, 10018209) >= 1)
                    {
                        Say(pc, 131, "……$R;" +
                            "$P嗯…$R;" +
                            "$P好啊…$R;" +
                            "认定您为『秘术使』吧!$R;" +
                            "$R真的要成为秘术使吗?$R;");
                        int a = 0;
                        while (a == 0)
                        {
                            switch (Select(pc, "要转职吗?", "", "要成为秘术使!", "转职时的注意事项", "不要"))
                            {
                                case 1:
                                    Say(pc, 131, "给您象徵『秘术使』的$R;" +
                                        "『秘术使纹章』$R;");
                                    if (pc.Inventory.Equipments.Count == 0)
                                    {
                                        switch (Select(pc, "要转职吗?", "", "要成为秘术使!", "不要"))
                                        {
                                            case 1:
                                                TakeItem(pc, 10034000, 1);
                                                TakeItem(pc, 10018209, 1);
                                                pc.JEXP = 0;
                                                mask.SetValue(Job2X_08.轉職結束, true);
                                                //_3A40 = true;
                                                ChangePlayerJob(pc, PC_JOB.CABALIST);
                                                //PARAM ME.JOB = 73
                                                PlaySound(pc, 3087, false, 100, 50);
                                                ShowEffect(pc, 4131);
                                                Wait(pc, 4000);
                                                Say(pc, 131, "……$R;" +
                                                    "$P……好了$R;" +
                                                    "您已经是『秘术使』了!$R;" +
                                                    "$R呵呵呵…$R;");
                                                PlaySound(pc, 4012, false, 100, 50);
                                                Say(pc, 131, "转职成『秘术使』!$R;");
                                                break;
                                            case 2:
                                                Say(pc, 131, "…吓到了?$R呵呵呵…$R;");
                                                break;
                                        }
                                        return;
                                    }
                                    Say(pc, 131, "纹章会烙印在皮肤上的$R;" +
                                        "把装备脱掉后再来吧$R;");
                                    return;
                                case 2:
                                    Say(pc, 131, "转职到『秘术使』的话$R;" +
                                        "LV会成为1…$R;" +
                                        "$R但是转职的之后拥有的$R;" +
                                        "技能和技能点数是不会变的$R;" +
                                        "$P而且…转职之前没有练熟的技能$R;" +
                                        "一旦转职了就无法练下去$R;" +
                                        "$R比如说，职业LV30时转职的话$R;" +
                                        "本来的职业LV30以上的技能$R;" +
                                        "转职后就无法在练下去了…$R;" +
                                        "$P好好想想吧…$R;");
                                    break;
                                case 3:
                                    Say(pc, 131, "…吓到了?$R呵呵呵…$R;");
                                    return;
                            }
                        }
                        return;
                    }
                    Say(pc, 131, "向黑暗奉献的供品$R;" +
                        "『玉桂罐头』和『黑暗之羽的羽毛』$R;" +
                        "都准备好了！$R;" +
                        "$P那样的话，就把您认定为$R;" +
                        "『秘术使』吧…$R;");
                    return;
                }
                if (pc.Inventory.Equipments.Count == 0)
                {
                    Say(pc, 131, "……$R;" +
                        "$P穿衣服吧…$R;");
                    return;
                }
                Say(pc, 131, "……$R;" +
                    "$P您的黑暗也…相当的深了…$R;");
                Say(pc, 131, "向黑暗奉献的供品$R;" +
                    "『玉桂罐头』和『黑暗之羽的羽毛』$R;" +
                    "都准备好了！$R;" +
                    "$P那样的话，就把您认定为$R;" +
                    "『秘术使』吧…$R;");
                mask.SetValue(Job2X_08.轉職開始, true);
                //_3A39 = true;
                return;
            }
            Say(pc, 131, "……$R;" +
                "$P对您还是比较困难$R;");
        }
    }
}
