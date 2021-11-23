
using System;
using System.Collections.Generic;
using System.Text;
using SagaLib;
using SagaDB.Actor;
using SagaDB.Item;
using SagaMap;
using SagaMap.Skill;
using SagaMap.Scripting;
using SagaScript.Chinese.Enums;
namespace SagaScript.M30210000
{
    public class S9100000302 : Event
    {
        public S9100000302()
        {
            this.EventID = 910000556;
        }

        public override void OnEvent(ActorPC pc)
        {
            foreach (var item in SagaMap.Manager.MapClientManager.Instance.OnlinePlayer)
            {
                if (item.Character.MapID == 21180001)
                {
                    item.Character.TInt["临时HP"] = 0;
                    item.Character.TInt["临时ATK"] = 0;
                    item.Character.TInt["临时MATK"] = 0;
                    item.SendSystemMessage("来自东方的神秘力量消失。");
                    SkillHandler.Instance.ShowEffectOnActor(item.Character, 5314);
                       
                }

            }

        }
    }
}

