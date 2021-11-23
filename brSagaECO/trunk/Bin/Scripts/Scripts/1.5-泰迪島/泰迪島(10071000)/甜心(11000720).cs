using System;
using System.Collections.Generic;
using System.Text;

using SagaDB.Actor;
using SagaMap.Scripting;
//所在地圖:泰迪島(10071000) NPC基本信息:甜心(11000720) X:231 Y:104
namespace SagaScript.M10071000
{
    public class S11000720 : Event
    {
        public S11000720()
        {
            this.EventID = 11000720;
        }

        public override void OnEvent(ActorPC pc)
        {
            Say(pc, 11000719, 131, "真美呀!$R;", "寶貝");

            Say(pc, 11000720, 131, "大海果然充滿浪漫啊?!$R;", "甜心");

            Say(pc, 11000719, 131, "大海在星星形成沒多久就出現了，$R;" +
                                   "是由從地面冒出的$R;" +
                                   "氣體和水蒸氣而形成的唷。$R;" +
                                   "$P經過長時間，熱氣慢慢累積，$R;" +
                                   "大氣中的水分行成$R;" +
                                   "酸性雨下起來了。$R;" +
                                   "$R大海之所以會鹹，$R;" +
                                   "是因為雨點中含有鹽的成分，$R;" +
                                   "那是積在地面的雨水和岩石裡的鈉$R;" +
                                   "相互溶合後進入海裡的原因。$R;", "寶貝");
        }
    }
}
