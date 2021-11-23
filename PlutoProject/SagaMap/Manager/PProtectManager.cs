using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using SagaLib;
using SagaDB.PProtect;

namespace SagaMap.Manager
{
    public class PProtectManager : Singleton<PProtectManager>
    {
        Dictionary<uint, PProtect> pprotects = new Dictionary<uint, PProtect>();
        List<PProtect> pprotects_l = new List<PProtect>();
        public PProtectManager()
        {

        }

        public void ADD(PProtect pp)
        {
            pp.ID = newID();
            pprotects.Add(pp.ID, pp);
            pprotects_l.Add(pp);
        }

        public PProtect GetPProtect(uint id)
        {
            if (pprotects.ContainsKey(id))
                return pprotects[id];
            return null;
        }

        public void Remove(uint id)
        {
            if (pprotects.ContainsKey(id))
            {
                if (pprotects_l.Contains(pprotects[id]))
                    pprotects_l.Remove(pprotects[id]);
                pprotects.Remove(id);
            }
            else
            {

            }
        }

        public List<PProtect> GetPProtectsOfPage(ushort page,out ushort max, int search = 0)
        {
            List<PProtect> temp = new List<PProtect>();
            if (pprotects_l.Count == 0)
            {
                max = 0;
                return temp;
            }

            List<PProtect> pprotects_t = pprotects_l;
            if (search > 0)
            {
                pprotects_t = pprotects_l.FindAll(x => x.TaskID == search);
            }


            int p = page;
            if ((page + 1) * 15 > pprotects_t.Count)
                p = pprotects_t.Count / 15;

            for(int i = p*15;i< pprotects_t.Count;i++)
            {
                temp.Add(pprotects_t[i]);
            }

            max = (ushort)((pprotects_t.Count / 15) + 1);
            return temp;
        }

        public ushort GetPProtectsPageMax()
        {
            return (ushort)((pprotects_l.Count / 15) + 1);
        }

        uint nowID = 1;
        uint newID()
        {
            while (pprotects.ContainsKey(nowID))
            {
                nowID++;
            }
            return nowID;
        }
    }
}
