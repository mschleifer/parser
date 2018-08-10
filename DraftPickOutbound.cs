using System;
using System.Collections.Generic;

namespace mtgparser
{
    public class DraftPickOutbound
    {
        public int Id { get; set; }
        public string Method { get; set; }
        public Params Params { get; set; }
    }

    public class Params
    {
        public string DraftId { get; set; }
        public int CardId { get; set; }
        public int PackNumber { get; set; }
        public int PickNumber { get; set; }
    }
}