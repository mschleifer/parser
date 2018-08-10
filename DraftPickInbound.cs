using System;
using System.Collections.Generic;

namespace mtgparser
{
    public class DraftPickInbound
    {
        public string PlayerId { get; set; }
        public string EventName { get; set; }
        public string DraftId { get; set; }
        public string DraftStatus { get; set; }
        public int PackNumber { get; set; }
        public int PickNumber { get; set; }
        public IList<int> DraftPack { get; set; }
        public IList<int> PickedCards { get; set; }
        public decimal RequestUnits { get; set; }

    }
}