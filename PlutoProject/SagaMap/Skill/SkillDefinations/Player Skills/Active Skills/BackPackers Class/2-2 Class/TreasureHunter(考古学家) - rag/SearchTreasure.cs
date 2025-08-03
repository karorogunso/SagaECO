
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using SagaDB.Actor;
using SagaMap.Skill.Additions.Global;
namespace SagaMap.Skill.SkillDefinations.TreasureHunter
{
    /// <summary>
    /// 寶物搜查（トレジャースキャンニング）
    /// </summary>
    public class SearchTreasure : ISkill
    {
        #region ISkill Members
        public int TryCast(ActorPC sActor, Actor dActor, SkillArg args)
        {

            return 0;

        }

        public void Proc(Actor sActor, Actor dActor, SkillArg args, byte level)
        {
            int[] range = { 0, 5000, 7000, 10000 };
            ActorSkill actor = new ActorSkill(args.skill, sActor);
            int lifetime = 10000 * level;
            DefaultBuff skill = new DefaultBuff(args.skill, sActor, "SearchTreasure", lifetime);
            skill.OnAdditionStart += this.StartEventHandler;
            skill.OnAdditionEnd += this.EndEventHandler;
            SkillHandler.ApplyAddition(sActor, skill);

            //Define MapClient
            SagaMap.Network.Client.MapClient client = SagaMap.Network.Client.MapClient.FromActorPC((ActorPC)sActor);

            //Locate Map
            Map map = Manager.MapManager.Instance.GetMap(sActor.MapID);

            //Search Range
            List<Actor> affected = map.GetActorsArea(sActor, (short)range[level], false);

            int i = 0;


            byte arrX = 255;
            byte arrY = 255;
            double length = 255;

            //Search Mob
            foreach (Actor act in affected)
            {
                if (act.type == ActorType.MOB)
                {
                    ActorMob m = (ActorMob)act;
                    if (m.BaseData.mobType == SagaDB.Mob.MobType.TREASURE_BOX_MATERIAL || m.BaseData.mobType == SagaDB.Mob.MobType.CONTAINER_MATERIAL || m.BaseData.mobType == SagaDB.Mob.MobType.TIMBER_BOX_MATERIAL)
                    {
                        if (m.HP <= 0) continue;
                        i++;
                        if (SagaLib.Global.PosX16to8(act.X, map.Width) <= arrX && SagaLib.Global.PosY16to8(act.Y, map.Width) <= arrY)
                        {
                            if (map.GetLengthD(actor.X, actor.Y, SagaLib.Global.PosX16to8(act.X, map.Width), SagaLib.Global.PosY16to8(act.Y, map.Width)) <= length)
                            {
                                length = map.GetLengthD(actor.X, actor.Y, SagaLib.Global.PosX16to8(act.X, map.Width), SagaLib.Global.PosY16to8(act.Y, map.Width));
                                arrX = SagaLib.Global.PosX16to8(act.X, map.Width);
                                arrY = SagaLib.Global.PosY16to8(act.Y, map.Width);
                            }

                        }
                        //break;
                    }
                }
            }

            //If Not Mob found
            if (i <= 0)
            {
                client.SendSystemMessage("沒有對象於搜索範圍內。");
                return;
            }
            else
            {

                Packets.Server.SSMG_NPC_NAVIGATION p = new SagaMap.Packets.Server.SSMG_NPC_NAVIGATION();
                p.X = SagaLib.Global.PosX16to8(arrX, map.Width);
                p.Y = SagaLib.Global.PosY16to8(arrY, map.Width);
                p.Type = 0;
                client.netIO.SendPacket(p);

                client.SendSystemMessage("已進入搜索狀態。");
            }

        }
        void StartEventHandler(Actor actor, DefaultBuff skill)
        {


        }
        void EndEventHandler(Actor actor, DefaultBuff skill)
        {
            //Define MapClient
            SagaMap.Network.Client.MapClient client = SagaMap.Network.Client.MapClient.FromActorPC((ActorPC)actor);

            client.SendSystemMessage("已解除搜索狀態。");
            Packets.Server.SSMG_NPC_NAVIGATION_CANCEL p = new SagaMap.Packets.Server.SSMG_NPC_NAVIGATION_CANCEL();
            client.netIO.SendPacket(p);

        }
        #endregion
    }
}
