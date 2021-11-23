
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using SagaDB.Actor;
using SagaMap.Skill.Additions.Global;
using SagaLib;
namespace SagaMap.Skill.SkillDefinations.Shaman
{
    /// <summary>
    /// 精靈祈願（精霊への祈り）
    /// </summary>
    public class PrayerToTheElf : ISkill
    {
        private SagaLib.Elements MapElement = SagaLib.Elements.Neutral;
        #region ISkill Members
        public int TryCast(ActorPC sActor, Actor dActor, SkillArg args)
        {
            Map map = Manager.MapManager.Instance.GetMap(sActor.MapID);
            if (map.CheckActorSkillInRange(SagaLib.Global.PosX8to16(args.x, map.Width), SagaLib.Global.PosY8to16(args.y, map.Height), 250))
            {
                return -17;
            }
            return 0;
        }
        public void Proc(Actor sActor, Actor dActor, SkillArg args, byte level)
        {
            int lifetime = 60000;
            Map map = Manager.MapManager.Instance.GetMap(sActor.MapID);
            int value = 0;
            foreach (var item in sActor.AttackElements)
            {
                if (value < SkillHandler.Instance.fieldelements(map, SagaLib.Global.PosX16to8(sActor.X, map.Width), SagaLib.Global.PosY16to8(sActor.Y, map.Height), item.Key))
                {
                    value = SkillHandler.Instance.fieldelements(map, SagaLib.Global.PosX16to8(sActor.X, map.Width), SagaLib.Global.PosY16to8(sActor.Y, map.Height), item.Key);
                    MapElement = item.Key;
                }
            }
            if(MapElement != Elements.Neutral)
            {
                PrayerToTheElfBuff skill = new PrayerToTheElfBuff(args.skill, sActor, MapElement.ToString() + "PrayerToTheElf", lifetime, MapElement, SagaLib.Global.PosX16to8(sActor.X, map.Width), SagaLib.Global.PosY16to8(sActor.Y, map.Height));
                SkillHandler.ApplyAddition(sActor, skill);
            }
        }
        public class PrayerToTheElfBuff : DefaultBuff
        {
            Map map;
            byte centerX;
            byte centerY;
            string prefix;
            private SagaLib.Elements MapElement;
            public PrayerToTheElfBuff(SagaDB.Skill.Skill skill, Actor actor, string AdditionName, int lifetime, SagaLib.Elements e, byte x, byte y)
                : base(skill, actor, AdditionName, lifetime)
            {
                this.OnAdditionStart += this.StartEvent;
                this.OnAdditionEnd += this.EndEvent;
                MapElement = e;
                centerX = x;
                centerY = y;
                prefix = MapElement.ToString() + "PrayerToTheElf";
                map = Manager.MapManager.Instance.GetMap(actor.MapID);
            }

            void StartEvent(Actor actor, DefaultBuff skill)
            {
                byte elevalue = (byte)(skill.skill.Level * 10);
                switch (MapElement)
                {
                    case Elements.Dark:
                        for (int x = centerX - 2; x <= centerX + 2; x++)
                        {
                            for (int y = centerY - 2; y <= centerY + 2; y++)
                            {
                                if (x >= 0 && x <= 255)
                                {
                                    if (y >= 0 && y <= 255)
                                    {
                                        if (skill.Variable.ContainsKey(getVariableKey(x, y)))
                                            skill.Variable.Remove(getVariableKey(x, y));
                                        skill.Variable.Add(getVariableKey(x, y), map.Info.dark[x, y]);
                                        map.Info.dark[x, y] += elevalue;
                                    }
                                }
                            }
                        }
                        break;
                    case Elements.Holy:
                        for (int x = centerX - 2; x <= centerX + 2; x++)
                        {
                            for (int y = centerY - 2; y <= centerY + 2; y++)
                            {
                                if (x >= 0 && x <= 255)
                                {
                                    if (y >= 0 && y <= 255)
                                    {
                                        if (skill.Variable.ContainsKey(getVariableKey(x, y)))
                                            skill.Variable.Remove(getVariableKey(x, y));
                                        skill.Variable.Add(getVariableKey(x, y), map.Info.holy[x, y]);
                                        map.Info.holy[x, y] += elevalue;
                                    }
                                }
                            }
                        }
                        break;
                    case Elements.Fire:
                        for (int x = centerX - 2; x <= centerX + 2; x++)
                        {
                            for (int y = centerY - 2; y <= centerY + 2; y++)
                            {
                                if (x >= 0 && x <= 255)
                                {
                                    if (y >= 0 && y <= 255)
                                    {
                                        if (skill.Variable.ContainsKey(getVariableKey(x, y)))
                                            skill.Variable.Remove(getVariableKey(x, y));
                                        skill.Variable.Add(getVariableKey(x, y), map.Info.fire[x, y]);
                                        map.Info.fire[x, y] += elevalue;
                                    }
                                }
                            }
                        }
                        break;
                    case Elements.Water:
                        for (int x = centerX - 2; x <= centerX + 2; x++)
                        {
                            for (int y = centerY - 2; y <= centerY + 2; y++)
                            {
                                if (x >= 0 && x <= 255)
                                {
                                    if (y >= 0 && y <= 255)
                                    {
                                        if (skill.Variable.ContainsKey(getVariableKey(x, y)))
                                            skill.Variable.Remove(getVariableKey(x, y));
                                        skill.Variable.Add(getVariableKey(x, y), map.Info.water[x, y]);
                                        map.Info.water[x, y] += elevalue;
                                    }
                                }
                            }
                        }
                        break;
                    case Elements.Wind:
                        for (int x = centerX - 2; x <= centerX + 2; x++)
                        {
                            for (int y = centerY - 2; y <= centerY + 2; y++)
                            {
                                if (x >= 0 && x <= 255)
                                {
                                    if (y >= 0 && y <= 255)
                                    {
                                        if (skill.Variable.ContainsKey(getVariableKey(x, y)))
                                            skill.Variable.Remove(getVariableKey(x, y));
                                        skill.Variable.Add(getVariableKey(x, y), map.Info.wind[x, y]);
                                        map.Info.wind[x, y] += elevalue;
                                    }
                                }
                            }
                        }
                        break;
                    case Elements.Earth:
                        for (int x = centerX - 2; x <= centerX + 2; x++)
                        {
                            for (int y = centerY - 2; y <= centerY + 2; y++)
                            {
                                if (x >= 0 && x <= 255)
                                {
                                    if (y >= 0 && y <= 255)
                                    {
                                        if (skill.Variable.ContainsKey(getVariableKey(x, y)))
                                            skill.Variable.Remove(getVariableKey(x, y));
                                        skill.Variable.Add(getVariableKey(x, y), map.Info.earth[x, y]);
                                        map.Info.earth[x, y] += elevalue;
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
                        for (int x = centerX - 2; x <= centerX + 2; x++)
                        {
                            for (int y = centerY - 2; y <= centerY + 2; y++)
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
                        for (int x = centerX - 2; x <= centerX + 2; x++)
                        {
                            for (int y = centerY - 2; y <= centerY + 2; y++)
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
                        for (int x = centerX - 2; x <= centerX + 2; x++)
                        {
                            for (int y = centerY - 2; y <= centerY + 2; y++)
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
                        for (int x = centerX - 2; x <= centerX + 2; x++)
                        {
                            for (int y = centerY - 2; y <= centerY + 2; y++)
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
                        for (int x = centerX - 2; x <= centerX + 2; x++)
                        {
                            for (int y = centerY - 2; y <= centerY + 2; y++)
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
                        for (int x = centerX - 2; x <= centerX + 2; x++)
                        {
                            for (int y = centerY - 2; y <= centerY + 2; y++)
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
