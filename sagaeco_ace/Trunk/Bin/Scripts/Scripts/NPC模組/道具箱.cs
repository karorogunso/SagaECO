using System;
using System.Collections.Generic;
using System.Text;

using SagaDB.Actor;
using SagaMap.Scripting;

namespace SagaScript
{
    public abstract class 道具箱 : Event
    {
        byte badge01, badge02;
        string prize01, prize02;

        public 道具箱()
        {

        }

        protected void Init(uint eventID, byte badge01, string prize01, byte badge02, string prize02)
        {
            this.EventID = eventID;
            this.badge01 = badge01;
            this.prize01 = prize01;
            this.badge02 = badge02;
            this.prize02 = prize02;
        }

        public override void OnEvent(ActorPC pc)
        {
            string badge_01, badge_02;
            string[] badge = new string[] {"埃米尔徽章", "铜徽章", "银徽章", "金徽章" };

            badge_01 = badge[badge01];
            badge_02 = badge[badge02];

            PlaySound(pc, 2559, false, 100, 50);

            Say(pc, 0, 65535, "会有什么厉害的道具呢?$R;" +
                              "真是叫人期待的道具箱!$R;", " ");

            switch (Select(pc, "要放什么徽章?", "", badge_01, badge_02, "没兴趣"))
            {
                case 1:
                    //尚未實裝道具箱功能
                    Say(pc, 0, 65535, "目前尚未实装$R;", " ");

                    Say(pc, 0, 65535, "没有铜徽章!$R;", " ");
                    break;

                case 2:
                    //尚未實裝道具箱功能
                    Say(pc, 0, 65535, "目前尚未实装$R;", " ");

                    Say(pc, 0, 65535, "没有埃米尔徽章!$R;", " ");
                    break;

                case 3:
                    break;
            }
        }
    }
}
