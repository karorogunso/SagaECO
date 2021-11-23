using System;
using System.Collections.Generic;
using System.Text;

using SagaDB.Actor;
using SagaMap.Scripting;

using SagaLib;
using SagaScript.Chinese.Enums;
//所在地圖:東方海角(10018101) NPC基本信息:帕斯特國境警備員(11000922) X:235 Y:77
namespace SagaScript.M10018101
{
    public class S11000922 : Event
    {
        public S11000922()
        {
            this.EventID = 11000922;
        }

        public override void OnEvent(ActorPC pc)
        {
            BitMask<Beginner_01> Beginner_01_mask = new BitMask<Beginner_01>(pc.CMask["Beginner_01"]);

            if (!Beginner_01_mask.Test(Beginner_01.已經與埃米爾進行第一次對話))
            {
                尚未與埃米爾對話(pc);
                return;
            }

            Say(pc, 11000922, 131, "您好!$R;" +
                                   "$R這座橋連接帕斯特島，$R;" +
                                   "不過現在還不能通過喔!$R;" +
                                   "$P開放的帕斯特島的視點!$R;" +
                                   "$R寬闊的景象非常壯觀!!$R;" +
                                   "想去的話，先提高實力再來吧!$R;" +
                                   "$P哎呀!$R;" +
                                   "您對這個世界還不太明白嗎?$R;" +
                                   "$R這個世界由一個很大的大陸$R;" +
                                   "「奧克魯尼亞大陸」和$R;" +
                                   "周圍的很多島嶼組成唷!$R;" +
                                   "$P奧克魯尼亞大陸中央是「阿高普路斯市」，$R;" +
                                   "$R先到那裡增強力量吧!$R;" +
                                   "$P這裡是奧克魯尼亞大陸東邊，$R;" +
                                   "叫「東方海角」。$R;" +
                                   "$R在小地圖視窗$R;" +
                                   "觀看整個大陸的地圖，$R;" +
                                   "可以馬上找到現在的位置喔!$R;" +
                                   "$P小地圖使用方法知道了嗎?$R;" +
                                   "去找那邊的道米尼女孩，$R;" +
                                   "她會仔細告訴您的。$R;" +
                                   "$P一邊看著整個大陸的地圖，$R;" +
                                   "一邊聽她說明吧!$R;" +
                                   "$R旁邊的大橋$R;" +
                                   "是連接奧克魯尼亞大陸$R;" +
                                   "和「 帕斯特島」的橋。$R;" +
                                   "$P奧克魯尼亞大陸的$R;" +
                                   "北方是「諾頓島」，$R;" +
                                   "$R南方是「薩烏斯火山島」，$R;" +
                                   "$R西南方是「摩根島」。$R;" +
                                   "$P力量提升後，逐一去看看吧!$R;", "帕斯特國境警備員");
        }

        void 尚未與埃米爾對話(ActorPC pc)
        {
            Say(pc, 11000922, 131, "這麼快就想跟埃米爾說話嗎?$R;", "帕斯特國境警備員");
        }  
    }
}
