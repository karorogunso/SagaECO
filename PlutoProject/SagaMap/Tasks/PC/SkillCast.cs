using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

using SagaLib;
using SagaDB.Actor;

using SagaMap.Network.Client;
namespace SagaMap.Tasks.PC
{
    public class SkillCast : MultiRunTask
    {
        MapClient client;
        SkillArg skill;
        public SkillCast(MapClient client, SkillArg skill)
        {
            if (skill.argType == SkillArg.ArgType.Cast)
            {
                this.dueTime = (int)skill.delay;
                this.period = (int)skill.delay;
            }
            else if (skill.argType == SkillArg.ArgType.Item_Cast)
            {
                this.dueTime = (int)skill.item.BaseData.cast;
                this.period = (int)skill.item.BaseData.cast;
            }
            this.client = client;
            this.skill = skill;
        }

        public override void CallBack()
        {
            ClientManager.EnterCriticalArea();
            try
            {
                if (client.Character != null)
                {
                    client.Character.Tasks.Remove("SkillCast");
                    if (skill.argType == SkillArg.ArgType.Cast)
                        client.OnSkillCastComplete(skill);
                    if (skill.argType == SkillArg.ArgType.Item_Cast)
                        client.OnItemCastComplete(skill);
                }
                this.Deactivate();
            }
            catch (Exception ex)
            {
                Logger.ShowError(ex);
                this.Deactivate();
            }
            ClientManager.LeaveCriticalArea();
        }
    }
}
