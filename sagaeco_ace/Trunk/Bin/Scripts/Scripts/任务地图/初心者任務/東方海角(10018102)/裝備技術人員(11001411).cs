using System;
using System.Collections.Generic;
using System.Text;

using SagaDB.Actor;
using SagaMap.Scripting;

using SagaLib;
using SagaScript.Chinese.Enums;
//所在地圖:東方海角(10018102) NPC基本信息:裝備技術人員(11001411) X:195 Y:69
namespace SagaScript.M10018102
{
    public class S11001411 : Event
    {
        public S11001411()
        {
            this.EventID = 11001411;
        }

        public override void OnEvent(ActorPC pc)
        {
            BitMask<Beginner_01> Beginner_01_mask = new BitMask<Beginner_01>(pc.CMask["Beginner_01"]);

            int selection;

            if (!Beginner_01_mask.Test(Beginner_01.已經與埃米爾進行第一次對話))
            {
                尚未與埃米爾對話(pc);
                return;
            }

            Say(pc, 11001411, 131, "好…$R;" +
                                   "$R哦? 您好，是初心者吧?$R;" +
                                   "$P我隶属于机械师行会。$R;" +
                                   "$R到这里是要来确认机械状态的。$R;" +
                                   "$P哦? 有事找我吗?$R;", "装备技术人员");

            selection = Select(pc, "想问什么呢?", "", "道具箱的使用方法", "道具票交换机的使用方法", "没有想问的");

            while (selection != 3)
            {
                switch (selection)
                {
                    case 1:
                        Say(pc, 11001411, 131, "道具箱$R;" +
                                               "$R只要有『埃米尔徽章』或$R;" +
                                               "『铜徽章』就可以使用!$R;" +
                                               "$P点击机械后，$R;" +
                                               "选择所要使用的徽章。$R;" +
                                               "$P想要使用的徽章，要在兑换前决定。$R;" +
                                               "$P兑换后，就会出现道具呀!$R;" +
                                               "$R至于会出现什么，兑换前是不知道的喔!$R;", "装备技术人员");
                        break;

                    case 2:
                        Say(pc, 11001411, 131, "至于道具票交换机，$R;" +
                                               "先点击这个机械，$R;" +
                                               "然后选择「道具票交换」。$R;" +
                                               "$P需要输入编号时，$R;" +
                                               "在输入视窗输入正确「道具编号」。$R;" +
                                               "$R请记得确认呀，不然是无法得到道具的。$R;" +
                                               "$P可以慢慢输入，$R;" +
                                               "但一定要正确!$R;" +
                                               "$P输入完，点击「确认」，$R;" +
                                               "就可以取得道具了。$R;" +
                                               "$R得到的道具$R;" +
                                               "可以在「道具视窗」确认喔!!$R;", "装备技术人员");
                        break;
                }

                selection = Select(pc, "想问什么呢?", "", "道具箱的使用方法", "道具票交换机的使用方法", "没有可问的");
            }        
        }

        void 尚未與埃米爾對話(ActorPC pc)
        {
            Say(pc, 11001411, 131, "已经跟埃米尔打招呼了吗? $R;" +
                                   "最好先跟他谈谈喔!$R;", "装备技术人员");
        } 
    }
}
