using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using SagaDB;
using SagaDB.Actor;
using SagaLib;
using SagaMap.Manager;

namespace SagaMap.Skill
{
    public partial class SkillHandler
    {
        
        int fieldelements(Map map, byte x, byte y, Elements eletype)
        {
            int fieldele = 0;
            if (eletype == Elements.Neutral)
            {
                fieldele = map.Info.neutral[x, y];
            }
            if (eletype == Elements.Fire)
            {
                fieldele = map.Info.fire[x, y];
            }
            if (eletype == Elements.Water)
            {
                fieldele = map.Info.water[x, y];
            }
            if (eletype == Elements.Wind)
            {
                fieldele = map.Info.wind[x, y];
            }
            if (eletype == Elements.Earth)
            {
                fieldele = map.Info.earth[x, y];
            }
            if (eletype == Elements.Holy)
            {
                fieldele = map.Info.holy[x, y];
            }
            if (eletype == Elements.Dark)
            {
                fieldele = map.Info.dark[x, y];
            }
            return fieldele;
        }

        /*无 火 水 风 地 光 暗*/
        int[,] EFtype = new int[,]{
        {0,0,0,0,0,0,0},
        {0,2,3,1,0,4,5},
        {0,1,2,0,3,4,5},
        {0,3,0,2,1,4,5},
        {0,0,1,3,2,4,5},
        {0,5,5,5,5,2,6},
        {0,4,4,4,4,3,2}
        };


        int bonustype(Elements src, Elements dst)
        {
            return EFtype[(int)src, (int)dst];
        }

        int EleLimit1(int ElementValue)
        {
            if (ElementValue > 254)
                ElementValue = 255;
            return ElementValue;
        }

        int EleLimit2(int ElementValue)
        {
            if (ElementValue > 99)
                ElementValue = 100;
            return ElementValue;
        }


        float efcal(Actor sActor, Actor dActor, Elements skillelement, int eleValue, int skilltype, bool heal)/*type=0,物理；1，魔法*/
        {
            //Map map;
            //map = MapManager.Instance.GetMap(dActor.MapID);
            //byte x, y;
            //x = Global.PosX16to8(dActor.X, map.Width);
            //y = Global.PosY16to8(dActor.Y, map.Height);
            float res = 1f;

            #region Attack Element & Element Value

            Elements attackElement = Elements.Neutral;
            int atkValue = 0;

            foreach (Elements i in sActor.AttackElements.Keys)
            {
                if (atkValue < (sActor.AttackElements[i] + sActor.Status.attackelements_item[i] + sActor.Status.attackelements_iris[i] + sActor.Status.attackelements_skill[i]))
                {
                    attackElement = i;
                    atkValue = sActor.AttackElements[i] + sActor.Status.attackelements_item[i] + sActor.Status.attackelements_iris[i] + sActor.Status.attackelements_skill[i];
                }
            }
            if (sActor.type == ActorType.MOB)
            {
                attackElement = Elements.Neutral;
                atkValue = 100;
            }
            //只有物理类无属性技能最后攻击属性按角色自身武器判定 其他情况都由技能属性决定
            if (skilltype == 0 && skillelement == Elements.Neutral)
            {
                //atkValue += fieldelements(map, x, y, attackElement); 取消地图属性影响 不直观
            }
            else
            {
                attackElement = skillelement;
                //物理技能攻击属性值自带25 魔法攻击属性值自带50
                if (skilltype == 0)
                {
                    atkValue = 25 + atkValue;
                }
                else
                {
                    atkValue = 50 + atkValue;
                }
            }
            atkValue = EleLimit2(atkValue);

            #endregion


            int defValue = 0;
            int type = 0;
            float Factor = 1f;

            if (heal)
            {
                //heal类技能atk用的是负值
                //if ((dActor.Elements[Elements.Dark] + dActor.Status.elements_item[Elements.Dark] + dActor.Status.elements_iris[Elements.Dark] + dActor.Status.elements_skill[Elements.Dark] + fieldelements(map, x, y, Elements.Dark)) <= 100) 取消地图属性
                if ((dActor.Elements[Elements.Dark] + dActor.Status.elements_item[Elements.Dark] + dActor.Status.elements_iris[Elements.Dark] + dActor.Status.elements_skill[Elements.Dark]) <= 100)
                {
                    //res = (1f + (float)Math.Sqrt((dActor.Elements[Elements.Holy] + dActor.Status.elements_item[Elements.Holy] + dActor.Status.elements_iris[Elements.Holy] + dActor.Status.elements_skill[Elements.Holy] + fieldelements(map, x, y, Elements.Holy)) / 100.0)) * ((float)Math.Sqrt(1.0 - (dActor.Elements[Elements.Dark] + dActor.Status.elements_item[Elements.Dark] + dActor.Status.elements_iris[Elements.Dark] + dActor.Status.elements_skill[Elements.Dark] + fieldelements(map, x, y, Elements.Dark)) / 100.0));
                    res = (1f + (float)Math.Sqrt((dActor.Elements[Elements.Holy] + dActor.Status.elements_item[Elements.Holy] + dActor.Status.elements_iris[Elements.Holy] + dActor.Status.elements_skill[Elements.Holy]) / 100.0)) * ((float)Math.Sqrt(1.0 - (dActor.Elements[Elements.Dark] + dActor.Status.elements_item[Elements.Dark] + dActor.Status.elements_iris[Elements.Dark] + dActor.Status.elements_skill[Elements.Dark]) / 100.0));
                }
                else
                {
                    //res = -(float)Math.Sqrt((dActor.Elements[Elements.Dark] + dActor.Status.elements_item[Elements.Dark] + dActor.Status.elements_iris[Elements.Dark] + dActor.Status.elements_skill[Elements.Dark] + fieldelements(map, x, y, Elements.Dark)) / 100.0 - 1.0);
                    res = -(float)Math.Sqrt((dActor.Elements[Elements.Dark] + dActor.Status.elements_item[Elements.Dark] + dActor.Status.elements_iris[Elements.Dark] + dActor.Status.elements_skill[Elements.Dark]) / 100.0 - 1.0);
                }
            }
            else
            {
                foreach (Elements i in dActor.Elements.Keys)//混合防御计算
                {
                    //defValue = dActor.Elements[i] + dActor.Status.elements_item[i] + dActor.Status.elements_iris[i] + dActor.Status.elements_skill[i] + fieldelements(map, x, y, i);
                    defValue = dActor.Elements[i] + dActor.Status.elements_item[i] + dActor.Status.elements_iris[i] + dActor.Status.elements_skill[i];
                    type = bonustype(attackElement, i);
                    if (type == 0)
                    {
                        Factor = 1f;
                    }
                    if (type == 1)
                    {
                        if (defValue > 100)
                        {
                            defValue = 100;
                        }
                        Factor = 1f + (float)Math.Sqrt(atkValue * defValue / 10000.0);
                    }
                    if (type == 2)
                    {
                        if (defValue < 50)
                        {
                            Factor = 0.8f + 0.2f * (float)Math.Sqrt(1.0 - defValue / 50.0);
                        }
                        else
                        {
                            if (defValue <= 100)
                            {
                                Factor = 0.8f - 0.2f * (float)Math.Sqrt(defValue / 50.0 - 1.0);
                            }
                            else
                            {
                                if (defValue > 200)
                                {
                                    defValue = 200;
                                }
                                Factor = -(float)Math.Sqrt(atkValue * defValue / 10000.0);
                            }
                        }
                    }
                    if (type == 3)
                    {
                        if (defValue > 100)
                        {
                            defValue = 100;
                        }
                        Factor = (float)Math.Sqrt(1.0 - defValue / 100.0);
                    }
                    if (type == 4)
                    {
                        if (defValue > 100)
                        {
                            defValue = 100;
                        }
                        Factor = 1f + 0.5f * (float)Math.Sqrt(atkValue * defValue / 10000.0);
                    }
                    if (type == 5)
                    {
                        if (defValue < 50)
                        {
                            Factor = 0.9f + 0.1f * (float)Math.Sqrt(1.0 - defValue / 50.0);
                        }
                        else
                        {
                            if (defValue > 100)
                            {
                                defValue = 100;
                            }
                            Factor = 0.9f - 0.1f * (float)Math.Sqrt(defValue / 50.0 - 1.0);
                        }
                    }
                    if (type == 6)
                    {
                        if (defValue > 100)
                        {
                            defValue = 100;
                        }
                        Factor = 1f + 1.25f * (float)Math.Sqrt(atkValue * defValue / 10000.0);
                    }
                    res = res * Factor;
                }
            }
            return 1f;
        }

        
        float CalcElementBonus(Actor sActor, Actor dActor, Elements element, int skilltype, bool heal)
        {
            return CalcElementBonus(sActor, dActor, element, 50, skilltype, heal);

        }

        /// <summary>
        /// 只计算角色属性状态引起的倍率 不考虑技能特殊状态对属性倍率的影响
        /// </summary>
        /// <param name="sActor"></param>
        /// <param name="dActor"></param>
        /// <param name="element"></param>
        /// <param name="eleValue">元素值</param>
        /// <param name="skilltype">0-物理，1-魔法</param>
        /// <param name="heal"></param>
        /// <returns></returns>
        float CalcElementBonus(Actor sActor, Actor dActor, Elements element, int eleValue, int skilltype, bool heal)
        {
            return efcal(sActor, dActor, element, eleValue, skilltype, heal);

        }
    }
}
