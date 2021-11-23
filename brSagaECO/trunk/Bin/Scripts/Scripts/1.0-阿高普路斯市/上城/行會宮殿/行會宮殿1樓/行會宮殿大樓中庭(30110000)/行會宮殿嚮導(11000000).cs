using System;
using System.Collections.Generic;
using System.Text;

using SagaDB.Actor;
using SagaMap.Scripting;

using SagaLib;
using SagaScript.Chinese.Enums;
//所在地圖:行會宮殿大樓中庭(30110000) NPC基本信息:行會宮殿嚮導(11000000) X:10 Y:16
namespace SagaScript.M30110000
{
    public class S11000000 : Event
    {
        public S11000000()
        {
            this.EventID = 11000000;
        }

        public override void OnEvent(ActorPC pc)
        {
            BitMask<Acropolisut_01> Acropolisut_01_mask = new BitMask<Acropolisut_01>(pc.CMask["Acropolisut_01"]);

            int selection;

            if (!Acropolisut_01_mask.Test(Acropolisut_01.已經與行會宮殿嚮導進行第一次對話))
            {
                初次與行會宮殿嚮導進行對話(pc);
                return;
            }

            Say(pc, 11000000, 131, "歡迎來到「行會宮殿」唷!$R;", "行會宮殿嚮導");

            selection = Select(pc, "想聽哪裡的說明呢?", "", "大樓中庭", "2樓", "3樓", "4樓", "5樓", "什麼也不聽");

            while (selection != 6)
            {
                switch (selection)
                {
                    case 1:
                        Say(pc, 11000000, 131, "這裡就是大樓中庭唷!$R;" +
                                               "$R我是「行會宮殿嚮導」，$R;" +
                                               "各樓層的傳送點都在這裡喔。$R;", "行會宮殿嚮導");
                        break;

                    case 2:
                        Say(pc, 11000000, 131, "2樓設有戰士系的職業行會唷!$R;" +
                                               "$R想到劍士、騎士、盜賊$R;" +
                                               "和弓手行會的朋友們，$R;" +
                                               "請上2樓吧。$R;", "行會宮殿嚮導");
                        break;

                    case 3:
                        Say(pc, 11000000, 131, "3樓主要是生產系的職業行會唷!$R;" +
                                               "$R除此之外，$R;" +
                                               "還有魔法師和元素使的魔法系行會。$R;" +
                                               "$P想到魔法系、農夫、礦工$R;" +
                                               "和機械師行會的朋友們，$R;" +
                                               "請上3樓吧。$R;", "行會宮殿嚮導");
                        break;

                    case 4:
                        Say(pc, 11000000, 131, "4樓全是生產系的職業行會唷!$R;" +
                                               "$R想到商人、鍊金術師、冒險家$R;" +
                                               "和木偶使行會的朋友們，$R;" +
                                               "請上4樓吧。$R;", "行會宮殿嚮導");
                        break;

                    case 5:
                        Say(pc, 11000000, 131, "5樓是為了招待異世界的朋友們，$R;" +
                                               "而特別設置的專用樓層唷!$R;" +
                                               "$R所以埃米爾族的朋友們，$R;" +
                                               "是無法進入的喔。$R;", "行會宮殿嚮導");
                        break;
                }
                selection = Select(pc, "想聽哪裡的說明呢?", "", "大樓中庭", "2樓", "3樓", "4樓", "5樓", "什麼也不聽");
            }

            Say(pc, 11000000, 131, "那請您到處逛逛吧。$R;", "行會宮殿嚮導");
        }
        

        void 初次與行會宮殿嚮導進行對話(ActorPC pc)
        {
            BitMask<Acropolisut_01> Acropolisut_01_mask = new BitMask<Acropolisut_01>(pc.CMask["Acropolisut_01"]);

            Say(pc, 11000000, 131, pc.Name + "歡迎光臨!!$R;" +
                                   "$R歡迎來到「行會宮殿」唷!$R;" +
                                   "$P您是第一次到這裡來嗎?$R;" +
                                   "$R要聽聽有關「行會宮殿」的說明嗎?$R;", "行會宮殿嚮導");

            switch (Select(pc, "要聽關於「行會宮殿」的說明嗎?", "", "想聽", "不聽"))
            {
                case 1:
                    Acropolisut_01_mask.SetValue(Acropolisut_01.已經與行會宮殿嚮導進行第一次對話, true);

                    Say(pc, 11000000, 131, "「行會宮殿」是一個5層樓的建築物，$R;" +
                                           "裡面有各種行會的辦公室唷!$R;" +
                                           "$P主要事務是辦理$R;" +
                                           "各職業的行會入會手續，$R;" +
                                           "以及任務的承接。$R;" +
                                           "$P有關各職業的消息，$R;" +
                                           "可以在2樓到4樓的各行會總部詢問呀!$R;" +
                                           "$P對了，5樓是為了招待異世界的$R;" +
                                           "朋友們的樓層唷!$R;" +
                                           "$R所以埃米爾族的朋友們，$R;" +
                                           "是無法進入的喔。$R;", "行會宮殿嚮導");
                    break;

                case 2:
                    Say(pc, 11000000, 131, "那請您到處逛逛吧。$R;", "行會宮殿嚮導");
                    break;
            }
        }
    }
}
