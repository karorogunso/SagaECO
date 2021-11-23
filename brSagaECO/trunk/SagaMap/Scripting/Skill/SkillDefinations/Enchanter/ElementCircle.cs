using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SagaDB.Actor;
using SagaLib;
using SagaMap.Skill.Additions.Global;
namespace SagaMap.Skill.SkillDefinations.Enchanter
{
    /// <summary>
    /// 大地結界（アースパワーサークル）
    /// 神風結界（ウィンドパワーサークル）
    /// 火焰結界（ファイアパワーサークル）
    /// 寒冰結界（ウォーターパワーサークル）
    /// </summary>
    public class ElementCircle:ISkill 
    {
        private SagaLib.Elements MapElement = SagaLib.Elements.Neutral;
        bool MobUse;
        public ElementCircle(SagaLib.Elements e)
        {
            MapElement = e;
            MobUse = false;
        }
        public ElementCircle(SagaLib.Elements e, bool MobUse)
        {
            MapElement = e;
            this.MobUse = MobUse;
        }
        #region ISkill Members

        public int TryCast(ActorPC pc, Actor dActor, SkillArg args)
        {
            Map map = Manager.MapManager.Instance.GetMap(pc.MapID);
            if (map.CheckActorSkillInRange(SagaLib.Global.PosX8to16(args.x, map.Width), SagaLib.Global.PosY8to16(args.y, map.Height), 100))
            {
                return -17;
            }
            return 0;
        }

        public void Proc(Actor sActor, Actor dActor, SkillArg args, byte level)
        {
            if (MobUse)
            {
                level = 5;
            }
            int lifetime = 25 + 5 * level;
            ElementCircleBuff skill = new ElementCircleBuff(args.skill, sActor, MapElement.ToString() + "ElementCircle", lifetime, MapElement, args.x,args.y);
            SkillHandler.ApplyAddition(sActor, skill);
        }
        public class ElementCircleBuff : DefaultBuff
        {
            Map map;
            byte centerX;
            byte centerY;
            string prefix;
            private SagaLib.Elements MapElement ;
            public ElementCircleBuff(SagaDB.Skill.Skill skill, Actor actor, string AdditionName, int lifetime, SagaLib.Elements e,byte x,byte y)
                : base(skill, actor, AdditionName, lifetime)
            {
                this.OnAdditionStart += this.StartEvent;
                this.OnAdditionEnd += this.EndEvent;
                MapElement = e;
                centerX = x;
                centerY = y;
                prefix = MapElement.ToString() + "ElementCircle";
                map = Manager.MapManager.Instance.GetMap(actor.MapID);
            }

            void StartEvent(Actor actor, DefaultBuff skill)
            {
                switch (MapElement)
                {
                    case Elements.Dark:
                        for (int x = centerX - 1; x <= centerX + 1; x++)
                        {
                            for (int y = centerY - 1; y <= centerY + 1; y++)
                            {
                                if (x >= 0 && x <= 255)
                                {
                                    if (y >= 0 && y <= 255)
                                    {
                                        if (skill.Variable.ContainsKey(getVariableKey(x, y)))
                                            skill.Variable.Remove(getVariableKey(x, y));
                                        skill.Variable.Add(getVariableKey(x, y), map.Info.dark[x, y]);
                                        map.Info.dark[x, y] = 50;
                                    }
                                }
                            }
                        }
                        break;
                    case Elements.Holy:
                        for (int x = centerX - 1; x <= centerX + 1; x++)
                        {
                            for (int y = centerY - 1; y <= centerY + 1; y++)
                            {
                                if (x >= 0 && x <= 255)
                                {
                                    if (y >= 0 && y <= 255)
                                    {
                                        if (skill.Variable.ContainsKey(getVariableKey(x, y)))
                                            skill.Variable.Remove(getVariableKey(x, y));
                                        skill.Variable.Add(getVariableKey(x, y), map.Info.holy[x, y]);
                                        map.Info.holy[x, y] = 50;
                                    }
                                }
                            }
                        }
                        break;
                    case Elements.Fire:
                        for (int x = centerX - 1; x <= centerX + 1; x++)
                        {
                            for (int y = centerY - 1; y <= centerY + 1; y++)
                            {
                                if (x >= 0 && x <= 255)
                                {
                                    if (y >= 0 && y <= 255)
                                    {
                                        if (skill.Variable.ContainsKey(getVariableKey(x, y)))
                                            skill.Variable.Remove(getVariableKey(x, y));
                                        skill.Variable.Add(getVariableKey(x, y), map.Info.fire[x, y]);
                                        map.Info.fire[x, y] = 50;
                                    }
                                }
                            }
                        }
                        break;
                    case Elements.Water:
                        for (int x = centerX - 1; x <= centerX + 1; x++)
                        {
                            for (int y = centerY - 1; y <= centerY + 1; y++)
                            {
                                if (x >= 0 && x <= 255)
                                {
                                    if (y >= 0 && y <= 255)
                                    {
                                        if (skill.Variable.ContainsKey(getVariableKey(x, y)))
                                            skill.Variable.Remove(getVariableKey(x, y));
                                        skill.Variable.Add(getVariableKey(x, y), map.Info.water[x, y]);
                                        map.Info.water[x, y] = 50;
                                    }
                                }
                            }
                        }
                        break;
                    case Elements.Wind:
                        for (int x = centerX - 1; x <= centerX + 1; x++)
                        {
                            for (int y = centerY - 1; y <= centerY + 1; y++)
                            {
                                if (x >= 0 && x <= 255)
                                {
                                    if (y >= 0 && y <= 255)
                                    {
                                        if (skill.Variable.ContainsKey(getVariableKey(x, y)))
                                            skill.Variable.Remove(getVariableKey(x, y));
                                        skill.Variable.Add(getVariableKey(x, y), map.Info.wind[x, y]);
                                        map.Info.wind[x, y] = 50;
                                    }
                                }
                            }
                        }
                        break;
                    case Elements.Earth:
                        for (int x = centerX - 1; x <= centerX + 1; x++)
                        {
                            for (int y = centerY - 1; y <= centerY + 1; y++)
                            {
                                if (x >= 0 && x <= 255)
                                {
                                    if (y >= 0 && y <= 255)
                                    {
                                        if (skill.Variable.ContainsKey(getVariableKey(x, y)))
                                            skill.Variable.Remove(getVariableKey(x, y));
                                        skill.Variable.Add(getVariableKey(x, y), map.Info.earth[x, y]);
                                        map.Info.earth[x, y] = 50;
                                    }
                                }
                            }
                        }
                        break;
                }
            }

            void EndEvent(Actor actor, DefaultBuff skill)
            {
                switch (MapElement)
                {
                    case Elements.Dark:
                        for (int x = centerX - 1; x <= centerX + 1; x++)
                        {
                            for (int y = centerY - 1; y <= centerY + 1; y++)
                            {
                                if (x >= 0 && x <= 255)
                                {
                                    if (y >= 0 && y <= 255)
                                    {
                                        map.Info.dark[x, y] = (byte)skill.Variable[getVariableKey(x, y)];
                                    }
                                }
                            }
                        }
                        break;
                    case Elements.Holy:
                        for (int x = centerX - 1; x <= centerX + 1; x++)
                        {
                            for (int y = centerY - 1; y <= centerY + 1; y++)
                            {
                                if (x >= 0 && x <= 255)
                                {
                                    if (y >= 0 && y <= 255)
                                    {
                                        map.Info.holy[x, y] = (byte)skill.Variable[getVariableKey(x, y)];
                                    }
                                }
                            }
                        }
                        break;
                    case Elements.Fire:
                        for (int x = centerX - 1; x <= centerX + 1; x++)
                        {
                            for (int y = centerY - 1; y <= centerY + 1; y++)
                            {
                                if (x >= 0 && x <= 255)
                                {
                                    if (y >= 0 && y <= 255)
                                    {
                                        map.Info.fire[x, y] = (byte)skill.Variable[getVariableKey(x, y)];
                                    }
                                }
                            }
                        }
                        break;
                    case Elements.Water:
                        for (int x = centerX - 1; x <= centerX + 1; x++)
                        {
                            for (int y = centerY - 1; y <= centerY + 1; y++)
                            {
                                if (x >= 0 && x <= 255)
                                {
                                    if (y >= 0 && y <= 255)
                                    {
                                        map.Info.water[x, y] = (byte)skill.Variable[getVariableKey(x, y)];
                                    }
                                }
                            }
                        }
                        break;
                    case Elements.Wind:
                        for (int x = centerX - 1; x <= centerX + 1; x++)
                        {
                            for (int y = centerY - 1; y <= centerY + 1; y++)
                            {
                                if (x >= 0 && x <= 255)
                                {
                                    if (y >= 0 && y <= 255)
                                    {
                                        map.Info.wind[x, y] = (byte)skill.Variable[getVariableKey(x, y)];
                                    }
                                }
                            }
                        }
                        break;
                    case Elements.Earth:
                        for (int x = centerX - 1; x <= centerX + 1; x++)
                        {
                            for (int y = centerY - 1; y <= centerY + 1; y++)
                            {
                                if (x >= 0 && x <= 255)
                                {
                                    if (y >= 0 && y <= 255)
                                    {
                                        map.Info.earth[x, y] = (byte)skill.Variable[getVariableKey(x, y)];
                                    }
                                }
                            }
                        }
                        break;
                }
            }
            string getVariableKey(int x, int y)
            {
                return prefix + string.Format("{0:000}", x) + string.Format("{0:000}", y);
            }
        }
        
        #endregion
    }
}
