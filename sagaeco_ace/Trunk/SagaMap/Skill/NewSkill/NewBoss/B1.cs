using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using SagaDB.Actor;
using SagaMap.Skill.SkillDefinations.Global;
using SagaLib;
using SagaMap;
using SagaMap.Mob;
using SagaDB.Skill;

namespace SagaMap.Skill.SkillDefinations.NewBoss
{
    class B1 : ISkill
    {
        #region ISkill Members

        public int TryCast(ActorPC pc, Actor dActor, SkillArg args)
        {
            return 0;
        }

        public void Proc(Actor sActor, Actor dActor, SkillArg args, byte level)
        {
            float factor = 1;
            Map map = Manager.MapManager.Instance.GetMap(sActor.MapID);
            //7个！
            /*3水光4
             2地无风5
              7暗火6*/
            short effectrange = 200;
            short damagerange = 300;
            int effectx1 = 0;
            int effecty1 = 0;
            int effectx2 = -2 * effectrange;
            int effecty2 = 0;
            int effectx3 = -effectrange;
            int effecty3 = (int)(effectrange * Math.Sqrt(3));
            int effectx4 = effectrange;
            int effecty4 = (int)(effectrange * Math.Sqrt(3));
            int effectx5 = 2 * effectrange;
            int effecty5 = 0;
            int effectx6 = effectrange;
            int effecty6 = (int)(-effectrange * Math.Sqrt(3));
            int effectx7 = -effectrange;
            int effecty7 = (int)(-effectrange * Math.Sqrt(3));
            short damagex1 = (short)(effectx1 + SagaLib.Global.PosX8to16(args.x,map.Width));
            short damagey1 = (short)(effecty1 + SagaLib.Global.PosY8to16(args.y,map.Height));
            short damagex2 = (short)(effectx2 + SagaLib.Global.PosX8to16(args.x,map.Width));
            short damagey2 = (short)(effecty2 + SagaLib.Global.PosY8to16(args.y,map.Height));
            short damagex3 = (short)(effectx3 + SagaLib.Global.PosX8to16(args.x,map.Width));
            short damagey3 = (short)(effecty3 + SagaLib.Global.PosY8to16(args.y,map.Height));
            short damagex4 = (short)(effectx4 + SagaLib.Global.PosX8to16(args.x,map.Width));
            short damagey4 = (short)(effecty4 + SagaLib.Global.PosY8to16(args.y,map.Height));
            short damagex5 = (short)(effectx5 + SagaLib.Global.PosX8to16(args.x,map.Width));
            short damagey5 = (short)(effecty5 + SagaLib.Global.PosY8to16(args.y,map.Height));
            short damagex6 = (short)(effectx6 + SagaLib.Global.PosX8to16(args.x,map.Width));
            short damagey6 = (short)(effecty6 + SagaLib.Global.PosY8to16(args.y,map.Height));
            short damagex7 = (short)(effectx7 + SagaLib.Global.PosX8to16(args.x,map.Width));
            short damagey7 = (short)(effecty7 + SagaLib.Global.PosY8to16(args.y,map.Height));
            /*
            SkillArg args1 = args.Clone();
            SkillArg args2 = args.Clone();
            SkillArg args3 = args.Clone();
            SkillArg args4 = args.Clone();
            SkillArg args5 = args.Clone();
            SkillArg args6 = args.Clone();
            SkillArg args7 = args.Clone();
            args1.x = SagaLib.Global.PosX16to8((short)damagex1, map.Width);
            args1.y = SagaLib.Global.PosY16to8((short)damagey1, map.Height);
            args2.x = SagaLib.Global.PosX16to8((short)damagex2, map.Width);
            args2.y = SagaLib.Global.PosY16to8((short)damagey2, map.Height);
            args3.x = SagaLib.Global.PosX16to8((short)damagex3, map.Width);
            args3.y = SagaLib.Global.PosY16to8((short)damagey3, map.Height);
            args4.x = SagaLib.Global.PosX16to8((short)damagex4, map.Width);
            args4.y = SagaLib.Global.PosY16to8((short)damagey4, map.Height);
            args5.x = SagaLib.Global.PosX16to8((short)damagex5, map.Width);
            args5.y = SagaLib.Global.PosY16to8((short)damagey5, map.Height);
            args6.x = SagaLib.Global.PosX16to8((short)damagex6, map.Width);
            args6.y = SagaLib.Global.PosY16to8((short)damagey6, map.Height);
            args7.x = SagaLib.Global.PosX16to8((short)damagex7, map.Width);
            args7.y = SagaLib.Global.PosY16to8((short)damagey7, map.Height);
            */
            List<Actor> list1 = map.GetRoundAreaActors(damagex1, damagey1, damagerange);
            List<Actor> list2 = map.GetRoundAreaActors(damagex2, damagey2, damagerange);
            List<Actor> list3 = map.GetRoundAreaActors(damagex3, damagey3, damagerange);
            List<Actor> list4 = map.GetRoundAreaActors(damagex4, damagey4, damagerange);
            List<Actor> list5 = map.GetRoundAreaActors(damagex5, damagey5, damagerange);
            List<Actor> list6 = map.GetRoundAreaActors(damagex6, damagey6, damagerange);
            List<Actor> list7 = map.GetRoundAreaActors(damagex7, damagey7, damagerange);
            List<Actor> affected1 = new List<Actor>();
            List<Actor> affected2 = new List<Actor>();
            List<Actor> affected3 = new List<Actor>();
            List<Actor> affected4 = new List<Actor>();
            List<Actor> affected5 = new List<Actor>();
            List<Actor> affected6 = new List<Actor>();
            List<Actor> affected7 = new List<Actor>();
            foreach (Actor i in list1)
            {
                if (SkillHandler.Instance.CheckValidAttackTarget(sActor, i))
                    affected1.Add(i);
            }
            foreach (Actor i in list2)
            {
                if (SkillHandler.Instance.CheckValidAttackTarget(sActor, i))
                    affected2.Add(i);
            }
            foreach (Actor i in list3)
            {
                if (SkillHandler.Instance.CheckValidAttackTarget(sActor, i))
                    affected3.Add(i);
            }
            foreach (Actor i in list4)
            {
                if (SkillHandler.Instance.CheckValidAttackTarget(sActor, i))
                    affected4.Add(i);
            }
            foreach (Actor i in list5)
            {
                if (SkillHandler.Instance.CheckValidAttackTarget(sActor, i))
                    affected5.Add(i);
            }
            foreach (Actor i in list6)
            {
                if (SkillHandler.Instance.CheckValidAttackTarget(sActor, i))
                    affected6.Add(i);
            }
            foreach (Actor i in list7)
            {
                if (SkillHandler.Instance.CheckValidAttackTarget(sActor, i))
                    affected7.Add(i);
            }

            ////应该乘个旋转矩阵
            //SkillArg args1 = args.Clone();
            SkillArg args2 = args.Clone();
            SkillArg args3 = args.Clone();
            SkillArg args4 = args.Clone();
            SkillArg args5 = args.Clone();
            SkillArg args6 = args.Clone();
            SkillArg args7 = args.Clone();
            SkillHandler.Instance.MagicAttack(sActor, affected1, args, SagaLib.Elements.Neutral, factor);
            SkillHandler.Instance.MagicAttack(sActor, affected2, args2, SagaLib.Elements.Earth, factor);
            SkillHandler.Instance.MagicAttack(sActor, affected3, args3, SagaLib.Elements.Water, factor);
            SkillHandler.Instance.MagicAttack(sActor, affected4, args4, SagaLib.Elements.Holy, factor);
            SkillHandler.Instance.MagicAttack(sActor, affected5, args5, SagaLib.Elements.Wind, factor);
            SkillHandler.Instance.MagicAttack(sActor, affected6, args6, SagaLib.Elements.Fire, factor);
            SkillHandler.Instance.MagicAttack(sActor, affected7, args7, SagaLib.Elements.Dark, factor);

            args.Add(args2);
            args.Add(args3);
            args.Add(args4);
            args.Add(args5);
            args.Add(args6);
            args.Add(args7);

            EffectArg arg = new EffectArg();

            arg.effectID = 5622;
            arg.x = SagaLib.Global.PosX16to8(damagex1, map.Width);
            arg.y = SagaLib.Global.PosY16to8(damagey1, map.Height);
            map.SendEventToAllActorsWhoCanSeeActor(Map.EVENT_TYPE.SHOW_EFFECT, arg, sActor, true);

            arg.effectID = 5225;
            arg.x = SagaLib.Global.PosX16to8(damagex2, map.Width);
            arg.y = SagaLib.Global.PosY16to8(damagey2, map.Height);
            map.SendEventToAllActorsWhoCanSeeActor(Map.EVENT_TYPE.SHOW_EFFECT, arg, sActor, true);
            arg.effectID = 5613;
            arg.x = SagaLib.Global.PosX16to8(damagex3, map.Width);
            arg.y = SagaLib.Global.PosY16to8(damagey3, map.Height);
            map.SendEventToAllActorsWhoCanSeeActor(Map.EVENT_TYPE.SHOW_EFFECT, arg, sActor, true);
            arg.effectID = 5065;
            arg.x = SagaLib.Global.PosX16to8(damagex4, map.Width);
            arg.y = SagaLib.Global.PosY16to8(damagey4, map.Height);
            map.SendEventToAllActorsWhoCanSeeActor(Map.EVENT_TYPE.SHOW_EFFECT, arg, sActor, true);
            arg.effectID = 5604;
            arg.x = SagaLib.Global.PosX16to8(damagex5, map.Width);
            arg.y = SagaLib.Global.PosY16to8(damagey5, map.Height);
            map.SendEventToAllActorsWhoCanSeeActor(Map.EVENT_TYPE.SHOW_EFFECT, arg, sActor, true);
            arg.effectID = 5307;
            arg.x = SagaLib.Global.PosX16to8(damagex6, map.Width);
            arg.y = SagaLib.Global.PosY16to8(damagey6, map.Height);
            map.SendEventToAllActorsWhoCanSeeActor(Map.EVENT_TYPE.SHOW_EFFECT, arg, sActor, true);
            arg.effectID = 5619;
            arg.x = SagaLib.Global.PosX16to8(damagex7, map.Width);
            arg.y = SagaLib.Global.PosY16to8(damagey7, map.Height);
            map.SendEventToAllActorsWhoCanSeeActor(Map.EVENT_TYPE.SHOW_EFFECT, arg, sActor, true);
        }
        #endregion
    }
}
