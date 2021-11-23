using System;
using System.Collections.Generic;
using System.Text;

using SagaDB.Actor;
using SagaMap.Scripting;

using SagaLib;
using SagaScript.Chinese.Enums;
//所在地圖:帳篷(30200000) NPC基本信息:冒險家前輩(11000958) X:1 Y:2
namespace SagaScript.M30200000
{
    public class S11000958 : Event
    {
        public S11000958()
        {
            this.EventID = 11000958;
        }

        public override void OnEvent(ActorPC pc)
        {
            BitMask<Beginner_02> Beginner_02_mask = new BitMask<Beginner_02>(pc.CMask["Beginner_02"]);

            int selection;

            if (!Beginner_02_mask.Test(Beginner_02.已經與冒險家前輩進行第一次對話))
                初次與冒險家前輩進行對話(pc);
            else
            {
                //驚訝!!
                Say(pc, 11000958, 131, "…啊!!$R;", "冒险家前辈");

                Say(pc, 11000958, 131, "初心者!?$R;" +
                                       "$R想再听一次是吗?$R;", "冒险家前辈");
            }

            selection = Select(pc, "想问什么呢?", "", "怎么了?", "这是什么地方?", "到外面去");

            while (selection != 3)
            {
                switch (selection)
                {
                    case 1:
                        Say(pc, 11000958, 131, "啊…那个啊?$R;" +
                                               "为了给初心者看看「帐篷」，$R;" +
                                               "开心地把「帐篷」设置好。$R;" +
                                               "$R可是没想到「持久度」…$R;" +
                                               "$P啊，对不起、对不起!!$R;" +
                                               "$R「帐篷」、「持久度」是什么，$R;" +
                                               "你应该都不知道吧?$R;" +
                                               "$P所谓的「帐篷」。$R;" +
                                               "$R就是如果有『帐篷』和$R;" +
                                               "技能「露营」的话，$R;" +
                                               "就可以制作一个安全的空间喔!$R;" +
                                               "$R是进地牢的时候，$R;" +
                                               "暂时的避难所…$R;" +
                                               "可以这么说吧?$R;" +
                                               "$P「持久度」就是说道具的寿命。$R;" +
                                               "$R道具的「持久度」下降到0的话，$R;" +
                                               "就会变成「废品」而无法再使用了。$R;" +
                                               "$P也有一些道具，一旦故障的话，$R;" +
                                               "就无法维修了喔。$R;" +
                                               "$R在道具的说明部分里，$R;" +
                                               "应该都有记载。$R;" +
                                               "使用前确认看看比较好!$R;" +
                                               "$P『帐篷』也是无法维修的道具。$R;" +
                                               "$R因为「露营」技能要用到，$R;" +
                                               "但是帐篷挺稀有的，$R;" +
                                               "想弄到手…有点困难呀!$R;" +
                                               "$P我出去的话，就要把「帐篷」收好$R;" +
                                               "只要收帐篷「持久度」就有可能会下降喔!$R;" +
                                               "$R每使用一次「帐篷」$R;" +
                                               "「持久度」都有可能会下降的。$R;" +
                                               "$P所以在苦恼要不要出去呀…$R;" +
                                               "$R留我自己在这里就可行了。$R;", "冒险家前辈");
                        break;

                    case 2:
                        Say(pc, 11000958, 131, "这是刚刚外面看到$R;" +
                                               "「帐篷」的内部。" +
                                               "$P在「帐篷」里，$R;" +
                                               "就像在村子里一样，$R;" +
                                               "体力会自然恢复!$R;" +
                                               "$R旅行途中，想休息一下的话，$R;" +
                                               "真的很棒啊!$R;" +
                                               "$P虽然需要「露营」技能，$R;" +
                                               "但只要是生产系的话，$R;" +
                                               "谁都可以学呀!$R;", "冒险家前辈");
                        break;
                }

                selection = Select(pc, "想问什么呢?", "", "怎么了?", "这是什么地方?", "到外面去");
            }

            if (!Beginner_02_mask.Test(Beginner_02.冒險家前輩給予各類藥水))
            {
                冒險家前輩給予各類藥水(pc);
                return;
            }

            Say(pc, 11000958, 131, "是…是吗?$R;" +
                                   "$R缠着您真是非常抱歉!$R;", "冒险家前辈");
        }

        void 初次與冒險家前輩進行對話(ActorPC pc)
        {
            BitMask<Beginner_02> Beginner_02_mask = new BitMask<Beginner_02>(pc.CMask["Beginner_02"]);

            Beginner_02_mask.SetValue(Beginner_02.已經與冒險家前輩進行第一次對話, true);

            //三條斜線
            Say(pc, 11000958, 131, "呼…那出去看看吗?$R;" +
                                   "$R啊啊，持久度非常低呀…$R;" +
                                   "吱吱…$R;", "冒险家前辈");
            //惊讶!!
            Say(pc, 11000958, 131, "…啊!!$R;", "冒险家前辈");

            Say(pc, 11000958, 131, "初心者!?$R;" +
                                   "$R有什么想要问我的吗?$R;", "冒险家前辈");
        }

        void 冒險家前輩給予各類藥水(ActorPC pc)
        {
            BitMask<Beginner_02> Beginner_02_mask = new BitMask<Beginner_02>(pc.CMask["Beginner_02"]);

            Beginner_02_mask.SetValue(Beginner_02.冒險家前輩給予各類藥水, true);

            Say(pc, 11000958, 131, "是…是吗?$R;" +
                                   "$R缠着您真是非常抱歉!$R;" +
                                   "如果不介意的话，给您这个吧?", "冒险家前辈");

            PlaySound(pc, 2040, false, 100, 50);
            GiveItem(pc, 10000103, 1);
            GiveItem(pc, 10000102, 1);
            GiveItem(pc, 10000108, 1);
            Say(pc, 0, 0, "得到『生命药水』、$R;" +
                          "『魔法药水』、$R;" +
                          "『耐力药水』!$R;", " ");

            Say(pc, 11000958, 131, "现在给的药水，$R;" +
                                   "各种恢复效果都不一样呢!$R;" +
                                   "『生命药水』是用来恢復HP值，$R;" +
                                   "『魔法药水』是用来恢復MP值，$R;" +
                                   "『耐力药水』是用来恢復SP值。$R;" +
                                   "$P想确认有什么效果的话，$R;" +
                                   "用滑鼠右键点击图示，$R;" +
                                   "就可以看到有关的说明啰。", "冒险家前辈");
        }
    }
}
