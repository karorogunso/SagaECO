
using System;
using System.Collections.Generic;
using System.Text;
using SagaLib;
using SagaDB.Actor;
using SagaDB.Item;
using SagaMap.Scripting;
using SagaScript.Chinese.Enums;
namespace SagaScript.M30210000
{
    public class S910000098 : Event
    {
        public S910000098()
        {
            this.EventID = 910000098;
        }

        public override void OnEvent(ActorPC pc)
        {
            if (CountItem(pc, 910000098) > 0)
            {
                if(pc.CInt["雲切任务"] <= 2)
                {
                    Say(pc, 0, "嗯...$R这部书有一股古老的味道...$R");
                    if (Select(pc, "怎么办呢？", "", "翻开看看？", "还是算了..") == 1)
                    {
                        Say(pc, 0, "……");
                        Say(pc, 0, "…………");
                        Say(pc, 0, "………………");
                        Say(pc, 0, "是一大堆看深奥的文字。。");
                        Say(pc, 0, "但书中间写着一句能看懂的字！$R$R“欲想领悟，请先自宫”");
                        if (Select(pc, "怎么办呢？", "", "自宫！", "还是算了..") == 1)
                        {
                            if(pc.Gender == PC_GENDER.FEMALE)
                                Say(pc, 0, "但是我好像是女孩纸耶...");
                            else
                            {
                                Say(pc, 0, "你举起屠刀");
                                Say(pc, 0, "对准下体");
                                Say(pc, 0, "丝");
                                Say(pc, 0, "丝毫");
                                Say(pc, 0, "丝毫没");
                                Say(pc, 0, "丝毫没有");
                                Say(pc, 0, "丝毫没有一");
                                Say(pc, 0, "丝毫没有一点");
                                Say(pc, 0, "丝毫没有一点犹");
                                Say(pc, 0, "丝毫没有一点犹豫");
                                Say(pc, 0, "丝毫没有一点犹豫地");
                                Say(pc, 0, "放弃了");
                            }
                            Say(pc, 0, "还是交给天天看看吧..");
                        }
                    }
                }
                if(pc.CInt["雲切任务"] ==3)
                {
                    Say(pc, 0, "嗯...$R这些部分都已经被天天教过了...$R");
                    Say(pc, 0, "这一页似乎就是修炼内容了吧！$R$R翻翻看看....");
                    Say(pc, 0, "书中写道：$R$R『使用雲切七七七次，你自会领悟』");
                    Select(pc, "……", "", "？？？？？？？？？？？", "MDZZ", "放学别走！", "你打野", "我有一句你妈批不知道当讲不当家", "呵呵", "嘻嘻");
                    pc.CInt["雲切任务"] = 4;
                }
                if (pc.CInt["雲切任务"] == 4)
                {
                    if(pc.CInt["雲切任务条件"] < 777)
                    {
                        Say(pc, 0, "修炼似乎还不够...$R还领悟不到精髓！！$R$R雲切修炼进度：" + pc.CInt["雲切任务条件"].ToString() + "/777");
                        return;
                    }
                    else
                    {
                        TakeItem(pc, 910000098, 1);
                        Say(pc, 0, "终！终于修成了！！！");
                        Say(pc, 0, "解锁了『雲切』进阶技巧");
                        Say(pc, 0, "然后书被你撕了");
                        pc.CInt["进阶技能解锁11002"] = 1;//解锁
                        pc.CInt["雲切任务"] = 5;
                        ShowEffect(pc, 5380);
                        return;
                    }
                }
            }
        }
    }
}

