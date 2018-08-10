using System;

namespace mtgparser
{
    public class DraftPick
    {
        public int Id { get; set; }
        public string DraftId { get; set; }
        public int CardId { get; set; }
        public int PackNumber { get; set; }
        public int PickNumber { get; set; }

        public DraftPick(DraftPickOutbound pickDataOut) {
            DraftId = pickDataOut.Params.DraftId;
            CardId = pickDataOut.Params.CardId;
            PackNumber = pickDataOut.Params.PackNumber + 1;
            PickNumber = pickDataOut.Params.PickNumber + 1;
        }
    }    
}