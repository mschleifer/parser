using System;
using System.Collections.Generic;

namespace mtgparser
{
    public class Card
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public bool IsToken { get; set; }
        public int Cmc { get; set; }
        public int RarityId { get; set; }
        public string SetCode { get; set; }
    }

    public class MtgaJsonCard
    {
        public int GrpId { get; set; }
        public int TitleId { get; set; }
        public bool IsToken { get; set; }
        public int Cmc { get; set; }
        public int Rarity { get; set; }
        public string Set { get; set; }
    }
}