using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using SagaLib;
using SagaDB.Actor;
using SagaMap.Network.Client;
using SagaMap.Skill.Additions.Global;

namespace SagaMap.Skill.SkillDefinations.Global
{
    public class SkillEvent : ISkill
    {
        #region ISkill Members

        public class Parameter
        {
            public Actor sActor;
            public Actor dActor;
            public SkillArg args;
            public byte level;
        }

        public int TryCast(ActorPC pc, Actor dActor, SkillArg args)
        {
            if (MapClient.FromActorPC(pc).scriptThread != null)
                return -59;
            else
                return 0;
        }

        public void Proc(Actor sActor, Actor dActor, SkillArg args, byte level)
        {
            args.dActor = 0;
            args.showEffect = false;
            Parameter para = new Parameter();
            para.sActor = sActor;
            para.dActor = dActor;
            para.args = args;
            para.level = level;
            MapClient.FromActorPC((ActorPC)sActor).scriptThread = new System.Threading.Thread(Run);
            MapClient.FromActorPC((ActorPC)sActor).scriptThread.Start(para);
        }

        void Run(object par)
        {
            //ClientManager.EnterCriticalArea();
            try
            {
                RunScript((Parameter)par);
            }
            catch (Exception ex)
            {
                SagaLib.Logger.ShowError(ex);
            }
            //ClientManager.LeaveCriticalArea();
            MapClient.FromActorPC((ActorPC)((Parameter)par).sActor).scriptThread = null;
        }

        protected virtual void RunScript(Parameter para)
        {
        }
       
        #endregion
    }
}
