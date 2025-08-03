using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using SagaDB.Actor;
using SagaLib;
namespace SagaMap.Skill.SkillDefinations.Global
{
    /// <summary>
    /// 元素风暴,各属性风暴通用技能
    /// サンダーストーム(闪电风暴)
    /// アクアストーム(水瓶风暴)
    /// アースストーム(大地风暴)
    /// ファイアストーム(烈焰风暴)
    /// </summary>
    public class ElementStorm : ISkill
    {
        Elements element;
        public ElementStorm(Elements e)
        {
            this.element = e;
        }
        public int TryCast(SagaDB.Actor.ActorPC sActor, SagaDB.Actor.Actor dActor, SkillArg args)
        {
            return 0;
        }
        public void Proc(SagaDB.Actor.Actor sActor, SagaDB.Actor.Actor dActor, SkillArg args, byte level)
        {
            float factor = 1.5f + 0.5f * level;
            ActorSkill actorS = new ActorSkill(args.skill, sActor);
            Map map = Manager.MapManager.Instance.GetMap(sActor.MapID);
            List<Actor> actors = map.GetActorsArea(SagaLib.Global.PosX8to16(args.x, map.Width), SagaLib.Global.PosY8to16(args.y, map.Height), 250, null);
            List<Actor> affected = new List<Actor>();
            foreach (Actor i in actors)
            {
                if (SkillHandler.Instance.CheckValidAttackTarget(sActor, i))
                    affected.Add(i);
            }

            SkillHandler.Instance.MagicAttack(sActor, affected, args, element, factor);
        }


    }
}
