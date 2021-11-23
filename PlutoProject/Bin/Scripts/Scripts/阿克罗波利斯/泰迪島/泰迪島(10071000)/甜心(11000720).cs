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
            Say(pc, 11000719, 131, "真美呀!$R;", "宝贝");

            Say(pc, 11000720, 131, "大海果然充满浪漫啊?!$R;", "甜心");

            Say(pc, 11000719, 131, "大海在星星形成没多久就出现了，$R;" +
                                   "是由从地面冒出的$R;" +
                                   "气体和水蒸气而形成的哦。$R;" +
                                   "$P经过长时间，热气慢慢累积，$R;" +
                                   "大气中的水分行成$R;" +
                                   "酸性雨下起来了。$R;" +
                                   "$R大海之所以会咸，$R;" +
                                   "是因为雨点中含有盐的成分，$R;" +
                                   "那是积在地面的雨水和岩石里的钠$R;" +
                                   "相互溶合后进入海里的原因。$R;", "宝贝");
        }
    }
}
