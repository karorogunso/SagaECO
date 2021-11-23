using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using SagaDB.Actor;

namespace SagaMap.Skill.SkillDefinations.Shaman
{
    public class ColdBlast:ISkill
    {
        #region ISkill Members

        public int TryCast(ActorPC pc, Actor dActor, SkillArg args)
        {
            return 0;
        }

        public void Proc(Actor sActor, Actor dActor, SkillArg args, byte level)
        {
            float factor = 0;
            switch (level)
            {
                case 1:
                    factor = 1.9f;
                    break;
                case 2:
                    factor = 2.3f;
                    break;
                case 3:
                    factor = 2.7f;
                    break;
                case 4:
                    factor =3.1f;
                    break;
                case 5:
                    factor = 3.5f;
                    break;
            }
            List<Actor> actors = Manager.MapManager.Instance.GetMap(dActor.MapID).GetActorsArea(dActor, 100, true);
            List<Actor> affected = new List<Actor>();
            //取得有效Actor（即怪物）
            foreach (Actor i in actors)
            {
                if (SkillHandler.Instance.CheckValidAttackTarget(sActor, i))
                    affected.Add(i);
            }
            SkillHandler.Instance.MagicAttack(sActor, affected, args, SagaLib.Elements.Water, factor);
            if (sActor.type == ActorType.PC)
            {
                if (((ActorPC)sActor).Skills.ContainsKey(3029))
                {
                    if (SagaLib.Global.Random.Next(0, 100) < 10 * ((ActorPC)sActor).Skills[3029].Level)
                    {
                        foreach (Actor i in affected)
                        {
                            Additions.Global.Freeze skill = new SagaMap.Skill.Additions.Global.Freeze(args.skill, i, 2000);
                            SkillHandler.ApplyAddition(i, skill);
                        }
                        EffectArg arg = new EffectArg();
                        arg.effectID = 5104;
                        arg.actorID = dActor.ActorID;
                        if (sActor.type == ActorType.PC)
                            SagaMap.Network.Client.MapClient.FromActorPC((ActorPC)sActor).map.SendEventToAllActorsWhoCanSeeActor(Map.EVENT_TYPE.SHOW_EFFECT, arg, (ActorPC)sActor, true);
                    }
                }
            }
        }

        #endregion
    }
}
