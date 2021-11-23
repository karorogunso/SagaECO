using System;
using System.Collections.Generic;
using System.Text;

using SagaDB.Actor;
using SagaMap.Scripting;
//所在地圖:泰迪島(10071000) NPC基本信息:仰望大海的泰迪(11000724) X:239 Y:143
namespace SagaScript.M10071000
{
    public class S11000724 : Event
    {
        public S11000724()
        {
            this.EventID = 11000724;
        }

        public override void OnEvent(ActorPC pc)
        {
            Say(pc, 11000724, 131, "大海真美呀!!$R;" +
                                   "$R夏天快點來就好了。$R;" +
                                   "$P您是客人呀?$R;" +
                                   "$R對不起…$R;" +
                                   "『海灘商店』只在夏天營業。$R;" +
                                   "$P『海灘開放』時，$R;" +
                                   "一定要再來呀，$R;" +
                                   "我也有賣泳衣呀。$R;", "仰望大海的泰迪");

            ShowEffect(pc, 10005, 4516);
            ShowEffect(pc, 10012, 4516);
            ShowEffect(pc, 10012, 4516);
            ShowEffect(pc, 10009, 4516);
            ShowEffect(pc, 10005, 4516);
            ShowEffect(pc, 10008, 4516);
            ShowEffect(pc, 10010, 4516);
            ShowEffect(pc, 10005, 4516);
            ShowEffect(pc, 10007, 4516);
            ShowEffect(pc, 10005, 4516);
            ShowEffect(pc, 10005, 4516);
            ShowEffect(pc, 10010, 4516);
        }
    }
}
