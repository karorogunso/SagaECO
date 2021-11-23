using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using SagaLib;
using SagaDB.Party;
using SagaDB.Actor;
using SagaDB.Tamaire;
using SagaMap.Network.Client;
using System.Xml;
using SagaLib.VirtualFileSystem;
using SagaMap.Scripting;

namespace SagaMap.Manager
{
    public class TamaireLendingManager : Singleton<TamaireLendingManager>
    {
        public TamaireLendingManager()
        {

        }

        public void ProcessLendingPost(TamaireLending lending)
        {
            List<TamaireLending> existinglendings =
                (from lendings in MapServer.charDB.GetTamaireLendings()
                 where lendings.Lender == lending.Lender
                 select lendings).ToList();
            if (existinglendings.Count() == 0)
                CreateLending(lending);
            else
                UpdateLending(lending);
        }

        private void CreateLending(TamaireLending lending)
        {
            ActorPC lender = MapServer.charDB.GetChar(lending.Lender);
            if (lender.TamaireLending != null)
                UpdateLending(lending);
            lender.TamaireLending = lending;
            MapServer.charDB.CreateTamaireLending(lending);
        }

        private void UpdateLending(TamaireLending lending)
        {
            ActorPC lender = MapServer.charDB.GetChar(lending.Lender);
            if (lender.TamaireLending != null)
            {
                lender.TamaireLending.Comment = lending.Comment;
                lender.TamaireLending.PostDue = lending.PostDue;
                lender.TamaireLending.Baselv = lending.Baselv;
                lender.TamaireLending.JobType = lending.JobType;
                MapServer.charDB.SaveTamaireLending(lender.TamaireLending);
            }
            else
                CreateLending(lending);
        }

        public void DeleteLending(ActorPC lender)
        {
        }
    }

    public class TamaireRentalManager: Singleton<TamaireRentalManager>
    {
        public TamaireRentalManager()
        {

        }

        public void CheckRentalExpiry(ActorPC renter)
        {
            if (renter.TamaireRental == null)
                return;
            if (renter.TamaireRental.CurrentLender == 0)
                return;
            ActorPC currentlender = MapServer.charDB.GetChar(renter.TamaireRental.CurrentLender);
            if (System.DateTime.Now > renter.TamaireRental.RentDue)
            {
                TerminateRental(renter, 0);
            }
        }
        public void TerminateRental(ActorPC renter, byte reason)
        {
            if (renter.TamaireRental == null)
                return;
            if (renter.TamaireRental.CurrentLender == 0)
                return;
            renter.TamaireRental.LastLender = renter.TamaireRental.CurrentLender;
            renter.TamaireRental.CurrentLender = 0;
            MapServer.charDB.SaveTamaireRental(renter.TamaireRental);

            ActorPC currentlender = MapServer.charDB.GetChar(renter.TamaireRental.CurrentLender);
            if (currentlender.TamaireLending.Renters.Contains(renter.CharID))
            {
                currentlender.TamaireLending.Renters.Remove(renter.CharID);
                MapServer.charDB.SaveTamaireLending(currentlender.TamaireLending);
            }
            MapClient.FromActorPC(renter).OnTamaireRentalTerminate(reason);
        }
        public void ProcessRental(ActorPC renter, ActorPC lender)
        {
            if (renter.TamaireRental==null)
            {
                renter.TamaireRental = new TamaireRental();
                renter.TamaireRental.Renter = renter.CharID;
                MapServer.charDB.CreateTamaireRental(renter.TamaireRental);
            }
            if (lender.TamaireLending == null)
                return;
            renter.TamaireRental.RentDue = DateTime.Now + TimeSpan.FromDays(2);
            renter.TamaireRental.CurrentLender = lender.CharID;
            MapServer.charDB.SaveTamaireRental(renter.TamaireRental);

            if (!lender.TamaireLending.Renters.Contains(renter.CharID))
                lender.TamaireLending.Renters.Add(renter.CharID);
            MapServer.charDB.SaveTamaireLending(lender.TamaireLending);

            int leveldiff = lender.TamaireLending.Baselv - renter.Level;
            ProcessRentalStatus(renter,leveldiff, lender.TamaireLending.JobType);
        }

        public float CalcFactor(int leveldiff)
        {
            float factor = 1.0f;
            if (leveldiff < 0)
                leveldiff = -leveldiff;
            if (leveldiff >= 1 && leveldiff <= 5)
                factor = 0.99f;
            if (leveldiff >= 6 && leveldiff <= 10)
                factor = 0.97f;
            if (leveldiff >= 11 && leveldiff <= 15)
                factor = 0.95f;
            if (leveldiff >= 16 && leveldiff <= 20)
                factor = 0.90f;
            if (leveldiff >= 20 && leveldiff <= 105)
                factor = 0.90f-((int)(leveldiff/5))*0.05f;
            if (leveldiff >= 106 && leveldiff <= 109)
                factor = 0.02f;
            return factor;
        }

        public void ProcessRentalStatus(ActorPC renter,int leveldiff, byte jobtype)
        {
           float factor = CalcFactor(leveldiff);
            List<TamaireStatus> statuslist = TamaireStatusFactory.Instance.Items.Values.ToList();
            var query = from s in statuslist where (s.level==renter.Level&&s.jobtype==jobtype)select s;
            TamaireStatus status = query.ToList()[0];
            if (status != null)
            {
                renter.TamaireRental.hp = (short)(status.hp*factor);
                renter.TamaireRental.mp = (short)(status.mp * factor);
                renter.TamaireRental.sp = (short)(status.sp * factor);
                renter.TamaireRental.atk_min = (short)(status.atk_min * factor);
                renter.TamaireRental.atk_max = (short)(status.atk_max * factor);
                renter.TamaireRental.matk_min = (short)(status.matk_min * factor);
                renter.TamaireRental.matk_max = (short)(status.matk_max * factor);
                renter.TamaireRental.def = (short)(status.def * factor);
                renter.TamaireRental.mdef = (short)(status.mdef * factor);
                renter.TamaireRental.hit_melee = (short)(status.hit_melee * factor);
                renter.TamaireRental.hit_range = (short)(status.hit_range * factor);
                renter.TamaireRental.avoid_melee = (short)(status.avoid_melee * factor);
                renter.TamaireRental.avoid_range = (short)(status.avoid_range * factor);
                renter.TamaireRental.aspd = (short)(status.aspd * factor);
                renter.TamaireRental.cspd = (short)(status.cspd * factor);
            }
            else
            {
                renter.TamaireRental.hp = 0;
                renter.TamaireRental.mp = 0;
                renter.TamaireRental.sp = 0;
                renter.TamaireRental.atk_min = 0;
                renter.TamaireRental.atk_max = 0;
                renter.TamaireRental.matk_min = 0;
                renter.TamaireRental.matk_max = 0;
                renter.TamaireRental.def = 0;
                renter.TamaireRental.mdef = 0;
                renter.TamaireRental.hit_melee = 0;
                renter.TamaireRental.hit_range = 0;
                renter.TamaireRental.avoid_melee = 0;
                renter.TamaireRental.avoid_range = 0;
                renter.TamaireRental.aspd = 0;
                renter.TamaireRental.cspd = 0;
            }
        }
    }

    public class TamaireExperienceMagager:Singleton<TamaireExperienceMagager>
    {
        public void GiveReward(ActorPC pc)
        {
            if (pc.TamaireLending == null)
                return;

            for (int i = 0; i < pc.TamaireLending.Renters.Count(); i++)
                TamaireRentalManager.Instance.CheckRentalExpiry(MapServer.charDB.GetChar(pc.TamaireLending.Renters[i]));
            
            uint cexp = 0, jexp = 0;
            if (SagaDB.Tamaire.TamaireExpRewardFactory.Instance.Items.ContainsKey(pc.Level))
            {
                switch (pc.Race)
                {
                    case PC_RACE.DEM:
                        cexp = (uint)TamaireExpRewardFactory.Instance.Items[pc.Level].demcexp;
                        jexp = (uint)TamaireExpRewardFactory.Instance.Items[pc.Level].demjexp;
                        break;
                    default:
                        if (!pc.Rebirth)
                        {
                            cexp = (uint)TamaireExpRewardFactory.Instance.Items[pc.Level].cexp;
                            if (pc.Job!=PC_JOB.NOVICE && pc.Job != PC_JOB.NONE)
                                jexp = (uint)TamaireExpRewardFactory.Instance.Items[pc.Level].jexp;
                        }
                        else
                        {
                            cexp = (uint)TamaireExpRewardFactory.Instance.Items[pc.Level].cexp2;
                            jexp = (uint)TamaireExpRewardFactory.Instance.Items[pc.Level].jexp3;
                        }
                        break;
                }
            }
            cexp *= (uint)pc.TamaireLending.Renters.Count;
            jexp *= (uint)pc.TamaireLending.Renters.Count;
            ExperienceManager.Instance.ApplyTamaireExp(cexp, jexp, pc);
        }
    }
}