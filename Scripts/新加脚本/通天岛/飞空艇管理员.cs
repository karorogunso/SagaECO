
using System;
using System.Collections.Generic;
using System.Text;
using SagaLib;
using SagaDB.Actor;
using SagaDB.Item;
using SagaMap.Scripting;
using SagaScript.Chinese.Enums;
using WeeklyExploration;
using System.Globalization;
namespace SagaScript.M30210000
{
    public class S60000135 : Event
    {
        public S60000135()
        {
            this.EventID = 60000135;
        }

        public override void OnEvent(ActorPC pc)
        {
            if (pc.Account.GMLevel < 0)
            {
                Say(pc, 131, "我们鱼缸岛马上就要开通通往通天岛的飞空艇航线了！$R但是目前还没完工。", "飞空艇航线管理员");
                return;
            }
            uint mapid = 0;
            byte x = 0, y = 0;
            uint fee = 0;
            switch (Select(pc,"请选择航线","", "前往通天塔之岛(0G)", "离开"))
            {

                case 1:
                    mapid = 10058000;
                    x = 128;
                    y = 245;
                    fee = 0;
                    break;
            }
            if(pc.Gold > fee)
            {
                ShowEffect(pc, 8066);
                Wait(pc, 2000);
                pc.Gold -= fee;
                Warp(pc, mapid, x, y);
            }
            else
                Say(pc, 131, "钱似乎不够呢？", "飞空艇航线管理员");

        }
    }
}