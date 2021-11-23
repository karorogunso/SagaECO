using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

using SagaLib;
using SagaDB.Actor;

using SagaMap.Network.Client;
using SagaMap.Mob;

namespace SagaMap.Tasks.Mob
{
    public class SkillCast : MultiRunTask
    {
        MobAI client;
        SkillArg skill;
        public SkillCast(MobAI ai, SkillArg skill)
        {
            if (skill.argType == SkillArg.ArgType.Cast)
            {
                this.dueTime = (int)skill.delay;
                this.period = (int)skill.delay;
            }
            this.client = ai;
            this.skill = skill;
        }

        public override void  CallBack()
        {

            try
            {
                ClientManager.EnterCriticalArea();
                client.Mob.Tasks.Remove("SkillCast");
                if (skill.argType == SkillArg.ArgType.Cast)
                    client.OnSkillCastComplete(skill);
                this.Deactivate();
                ClientManager.LeaveCriticalArea();
            }
            catch (Exception ex)
            {
                Logger.ShowError(ex);
                this.Deactivate();
            }

        }
    }
}
