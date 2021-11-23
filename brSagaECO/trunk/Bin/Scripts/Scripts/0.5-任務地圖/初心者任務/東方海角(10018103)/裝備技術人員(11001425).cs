using System;
using System.Collections.Generic;
using System.Text;

using SagaDB.Actor;
using SagaMap.Scripting;

using SagaLib;
using SagaScript.Chinese.Enums;
//所在地圖:東方海角(10018103) NPC基本信息:裝備技術人員(11001425) X:195 Y:69
namespace SagaScript.M10018103
{
    public class S11001425 : Event
    {
        public S11001425()
        {
            this.EventID = 11001425;
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

            Say(pc, 11001425, 131, "好…$R;" +
                                   "$R哦? 您好，是初心者吧?$R;" +
                                   "$P我隸屬於機械師行會。$R;" +
                                   "$R到這裡是要來確認機械狀態的。$R;" +
                                   "$P哦? 有事找我嗎?$R;", "裝備技術人員");

            selection = Select(pc, "想問什麼呢?", "", "道具箱的使用方法", "道具票交換機的使用方法", "沒有想問的");

            while (selection != 3)
            {
                switch (selection)
                {
                    case 1:
                        Say(pc, 11001425, 131, "道具箱$R;" +
                                               "$R只要有『埃米爾徽章』或$R;" +
                                               "『銅徽章』就可以使用唷!$R;" +
                                               "$P點擊機械後，$R;" +
                                               "選擇所要使用的徽章。$R;" +
                                               "$P想要使用的徽章，要在稅換前決定。$R;" +
                                               "$P稅換後，就會出現道具呀!$R;" +
                                               "$R至於會出現什麼，稅換前是不知道的喔!$R;", "裝備技術人員");
                        break;

                    case 2:
                        Say(pc, 11001425, 131, "至於道具票交換機，$R;" +
                                               "先點擊這個機械，$R;" +
                                               "然後選擇「道具票交換」。$R;" +
                                               "$P需要輸入編號時，$R;" +
                                               "在輸入視窗輸入正確「道具編號」。$R;" +
                                               "$R請記得確認呀，不然是無法得到道具的。$R;" +
                                               "$P可以慢慢輸入，$R;" +
                                               "但一定要正確唷!$R;" +
                                               "$P輸入完，點擊「確認」，$R;" +
                                               "就可以取得道具了。$R;" +
                                               "$R得到的道具$R;" +
                                               "可以在「道具視窗」確認喔!!$R;", "裝備技術人員");
                        break;
                }

                selection = Select(pc, "想問什麼呢?", "", "道具箱的使用方法", "道具票交換機的使用方法", "沒有可問的");
            }        
        }

        void 尚未與埃米爾對話(ActorPC pc)
        {
            Say(pc, 11001425, 131, "已經跟埃米爾打招呼了嗎? $R;" +
                                   "最好先跟他談談喔!$R;", "裝備技術人員");
        } 
    }
}
