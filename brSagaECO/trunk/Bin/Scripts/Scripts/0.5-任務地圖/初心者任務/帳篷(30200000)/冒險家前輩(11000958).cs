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
                Say(pc, 11000958, 131, "…啊!!$R;", "冒險家前輩");

                Say(pc, 11000958, 131, "初心者!?$R;" +
                                       "$R想再聽一次是嗎?$R;", "冒險家前輩"); 
            }

            selection = Select(pc, "想問什麼呢?", "", "怎麼了?", "這是什麼地方?", "到外面去");

            while (selection != 3)
            {
                switch (selection)
                {
                    case 1:
                        Say(pc, 11000958, 131, "啊…那個啊?$R;" +
                                               "為了給初心者看看「帳篷」，$R;" +
                                               "開心地把「帳篷」設置好。$R;" +
                                               "$R可是沒想到「持久度」…$R;" +
                                               "$P啊，對不起、對不起!!$R;" +
                                               "$R「帳篷」、「持久度」是什麼，$R;" +
                                               "你應該都不知道吧?$R;" +
                                               "$P所謂的「帳篷」。$R;" +
                                               "$R就是如果有『帳篷』和$R;" +
                                               "技能「露營」的話，$R;" +
                                               "就可以製作一個安全的空間喔!$R;" +
                                               "$R是進地牢的時候，$R;" +
                                               "暫時的避難所…$R;" +
                                               "可以這麼說吧?$R;" +
                                               "$P「持久度」就是說道具的壽命。$R;" +
                                               "$R道具的「持久度」下降到0的話，$R;" +
                                               "就會變成「廢品」而無法再使用了。$R;" +
                                               "$P也有一些道具，一旦故障的話，$R;" +
                                               "就無法維修了喔。$R;" +
                                               "$R在道具的說明部分裡，$R;" +
                                               "應該都有記載。$R;" +
                                               "使用前確認看看比較好!$R;" +
                                               "$P『帳篷』也是無法維修的道具。$R;" +
                                               "$R因為「露營」技能要用到，$R;" +
                                               "但是帳篷挺稀有的，$R;" +
                                               "想弄到手…有點困難呀!$R;" +
                                               "$P我出去的話，就要把「帳篷」收好$R;" +
                                               "只要收帳篷「持久度」就有可能會下降喔!$R;" +
                                               "$R每使用一次「帳篷」$R;" +
                                               "「持久度」都有可能會下降的。$R;" +
                                               "$P所以在苦惱要不要出去呀…$R;" +
                                               "$R留我自己在這裡就可行了。$R;", "冒險家前輩");
                        break;

                    case 2:
                        Say(pc, 11000958, 131, "這是剛剛外面看到$R;" +
                                               "「帳篷」的內部。" +
                                               "$P在「帳篷」裡，$R;" +
                                               "就像在村子裡一樣，$R;" +
                                               "體力會自然恢復唷!$R;" +
                                               "$R旅行途中，想休息一下的話，$R;" +
                                               "真的很棒啊!$R;" +
                                               "$P雖然需要「露營」技能，$R;" +
                                               "但只要是生產系的話，$R;" +
                                               "誰都可以學呀!$R;", "冒險家前輩");
                        break;
                }

                selection = Select(pc, "想問什麼呢?", "", "怎麼了?", "這是什麼地方?", "到外面去");
            }

            if (!Beginner_02_mask.Test(Beginner_02.冒險家前輩給予各類藥水))
            {
                冒險家前輩給予各類藥水(pc);
                return;
            }

            Say(pc, 11000958, 131, "是…是嗎?$R;" +
                                   "$R抓著您真是非常抱歉!$R;", "冒險家前輩");
        }

        void 初次與冒險家前輩進行對話(ActorPC pc)
        {
            BitMask<Beginner_02> Beginner_02_mask = new BitMask<Beginner_02>(pc.CMask["Beginner_02"]);

            Beginner_02_mask.SetValue(Beginner_02.已經與冒險家前輩進行第一次對話, true);

            //三條斜線
            Say(pc, 11000958, 131, "呼…那出去看看嗎?$R;" +
                                   "$R啊啊，持久度非常低呀…$R;" +
                                   "吱吱…$R;", "冒險家前輩");
            //驚訝!!
            Say(pc, 11000958, 131, "…啊!!$R;", "冒險家前輩");

            Say(pc, 11000958, 131, "初心者!?$R;" +
                                   "$R有什麼想要問我的嗎?$R;", "冒險家前輩");
        }

        void 冒險家前輩給予各類藥水(ActorPC pc)
        {
            BitMask<Beginner_02> Beginner_02_mask = new BitMask<Beginner_02>(pc.CMask["Beginner_02"]);

            Beginner_02_mask.SetValue(Beginner_02.冒險家前輩給予各類藥水, true);

            Say(pc, 11000958, 131, "是…是嗎?$R;" +
                                   "$R抓著您真是非常抱歉!$R;" +
                                   "如果不介意的話，給您這個吧?", "冒險家前輩");

            PlaySound(pc, 2040, false, 100, 50);
            GiveItem(pc, 10000103, 1);
            GiveItem(pc, 10000102, 1);
            GiveItem(pc, 10000108, 1);
            Say(pc, 0, 0, "得到『治癒藥水』、$R;" +
                          "『魔法藥水』、$R;" +
                          "『耐力藥水』!$R;", " ");

            Say(pc, 11000958, 131, "現在給的藥水，$R;" +
                                   "各種恢復效果都不一樣唷!$R;" +
                                   "『治癒藥水』是用來恢復HP值，$R;" +
                                   "『魔法藥水』是用來恢復MP值，$R;" +
                                   "『耐力藥水』是用來恢復SP值。$R;" +
                                   "$P想確認有什麼效果的話，$R;" +
                                   "用滑鼠右鍵點擊圖示，$R;" +
                                   "就可以看到有關的說明囉。", "冒險家前輩");
        }
    }
}
