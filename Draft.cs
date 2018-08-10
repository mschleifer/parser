using System;
using System.Collections.Generic;

namespace mtgparser
{
    public class Draft
    {
        public string PlayerId { get; set; }
        public string EventName { get; set; }
        public string DraftId { get; set; }
        public string DraftStatus { get; set; }
        public IList<DraftPick> Picks { get; set; }
        public IList<Match> Matches { get; set; }

    }

    public class Match
    {
        public string Opponent { get; set; }
        public string Result { get; set; }
        public IList<Game> Games { get; set; }
    }

    public class Game
    {
        public string Result { get; set; }
    }
}