using System;
using System.Collections.Generic;
using System.Text;

using SagaDB.Actor;
using SagaMap.Scripting;
//所在地圖:泰迪島(10071000) NPC基本信息:瑪瑪(11000721) X:182 Y:175
namespace SagaScript.M10071000
{
    public class S11000721 : Event
    {
        public S11000721()
        {
            this.EventID = 11000721;
        }

        public override void OnEvent(ActorPC pc)
        {
            Say(pc, 11000721, 131, "這裡是「小狗樂園」，$R;" +
                                   "是個能讓狗狗們自由自在$R;" +
                                   "玩耍的運動場。$R;", "瑪瑪");

            ShowEffect(pc, 10006, 4516);
            ShowEffect(pc, 10007, 4516);
            ShowEffect(pc, 10011, 4516);
            ShowEffect(pc, 10008, 4516);
            ShowEffect(pc, 10006, 4516);
            ShowEffect(pc, 10010, 4516);
            ShowEffect(pc, 10012, 4516);
            ShowEffect(pc, 10008, 4516);
            ShowEffect(pc, 10005, 4516);
            ShowEffect(pc, 10011, 4516);
            ShowEffect(pc, 10006, 4516);
            ShowEffect(pc, 10010, 4516);
            ShowEffect(pc, 10012, 4516);
            ShowEffect(pc, 10007, 4516);
            ShowEffect(pc, 10011, 4516);
            ShowEffect(pc, 10010, 4516);
            ShowEffect(pc, 10009, 4516);
            ShowEffect(pc, 10006, 4516);
            ShowEffect(pc, 10009, 4516);
            ShowEffect(pc, 10007, 4516);
            ShowEffect(pc, 10012, 4516);
            ShowEffect(pc, 10011, 4516);
            ShowEffect(pc, 10012, 4516);
            ShowEffect(pc, 10010, 4516);
            ShowEffect(pc, 10006, 4516);
            ShowEffect(pc, 10008, 4516);
            ShowEffect(pc, 10011, 4516);
            ShowEffect(pc, 10008, 4516);
            ShowEffect(pc, 10010, 4516);
            ShowEffect(pc, 10006, 4516);
            ShowEffect(pc, 10007, 4516);
            ShowEffect(pc, 10012, 4516);
            ShowEffect(pc, 10011, 4516);
            ShowEffect(pc, 10009, 4516);
            ShowEffect(pc, 10006, 4516);
            ShowEffect(pc, 10008, 4516);
            ShowEffect(pc, 10012, 4516);
            ShowEffect(pc, 10011, 4516);
            ShowEffect(pc, 10006, 4516);
            ShowEffect(pc, 10009, 4516);
            ShowEffect(pc, 10011, 4516);
            ShowEffect(pc, 10005, 4516);
            ShowEffect(pc, 10010, 4516);
            ShowEffect(pc, 10005, 4516);
            ShowEffect(pc, 10011, 4516);
            ShowEffect(pc, 10010, 4516);
            ShowEffect(pc, 10011, 4516);
            ShowEffect(pc, 10007, 4516);
            ShowEffect(pc, 10007, 4516);
            ShowEffect(pc, 10008, 4516);
        }
    }
}