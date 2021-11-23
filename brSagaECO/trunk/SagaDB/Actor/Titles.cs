using SagaLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SagaDB.Actor
{
    public enum Page
        {
            One = 1,
            Two ,
            Three,
            Four,
            Five ,
            Six,
            Seven,
            Eight ,
            Nine,
            Ten,
        }
    public class Titles
    {
        Dictionary<Page, LongBitMask> titles = new Dictionary<Page, LongBitMask>();
        public Titles()
        {
            titles.Add(Page.One, new LongBitMask());
            titles.Add(Page.Two, new LongBitMask());
            titles.Add(Page.Three, new LongBitMask());
            titles.Add(Page.Four, new LongBitMask());
            titles.Add(Page.Five, new LongBitMask());
            titles.Add(Page.Six, new LongBitMask());
            titles.Add(Page.Seven, new LongBitMask());
            titles.Add(Page.Eight, new LongBitMask());
            titles.Add(Page.Nine, new LongBitMask());
            titles.Add(Page.Ten, new LongBitMask());
        }

        public void Dispose()
        {
            titles = null;
        }

        public LongBitMask this[Page page]
        {
            get
            {
                if (titles == null)
                    titles = new Dictionary<Page, LongBitMask>();
                return titles[page];
            }
        }
    }
}
