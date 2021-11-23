
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
    public class S910000099 : Event
    {
        public S910000099()
        {
            this.EventID = 910000099;
        }

        public override void OnEvent(ActorPC pc)
        {
            if(pc.Account.GMLevel > 200)
            {
                if (Select(pc, "兔纸>0<!!!!!", "", "点我点我点我挖！！[记录59]", "谁要点你！!![不清]") == 1)
{
                    pc.CInt["圣疗术任务"] = 5;
 pc.CInt["进阶技能解锁13102"] = 1;
}
            }
            if (CountItem(pc, 910000099) > 0)
            {
                if (pc.CInt["圣疗术任务"] == 1)
                {
                    Say(pc, 0, "嗯...$R要不要翻开看看呢$R");
                    if (Select(pc, "怎么办呢？", "", "翻开看看？", "还是算了..") == 1)
                    {
                        Say(pc, 0, "……");
                        Say(pc, 0, "…………");
                        Say(pc, 0, "………………");
                        Say(pc, 0, "是一大堆看深奥的医学文字。。");
                        Say(pc, 0, "……好难懂……");
                        Say(pc, 0, "去找疾风队长问问好了");
                    }
                }
                if (pc.CInt["圣疗术任务"] == 2)
                {
                    Say(pc, 0, "好吧。。。$R那就去进行1500次『圣疗术』吧...");
                    pc.CInt["圣疗术任务"] = 3;
                }
                if (pc.CInt["圣疗术任务"] == 3)
                {
                    if (pc.CInt["圣疗术任务条件"] < 1500)
                    {
                        Say(pc, 0, "修炼似乎还不够...$R还领悟不到精髓！！$R$R圣疗术修炼进度：" + pc.CInt["圣疗术任务条件"].ToString() + "/1500");
                        return;
                    }
                    else
                    {
                        TakeItem(pc, 910000099, 1);
                        Say(pc, 0, "呜。。呜哦！！$R终于完成1500次圣疗术了。");
                        Say(pc, 0, "那就来翻看看看吧！");
                        Say(pc, 0, "……");
                        Say(pc, 0, "（你果然能看懂了）");
                        Say(pc, 0, "……");
                        Say(pc, 0, "解锁了『圣疗术』进阶技巧");
                        Say(pc, 0, "然后你把书撕了，撕得粉碎！！");
                        pc.CInt["进阶技能解锁13102"] = 1;//解锁
                        pc.CInt["圣疗术任务"] = 5;
                        ShowEffect(pc, 5380);
                        return;
                    }
                }
            }
        }
    }
}

