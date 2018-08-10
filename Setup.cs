using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using Newtonsoft.Json;

namespace mtgparser
{
    public class Setup
    {
        public static bool BuildDatabase()
        {
            var cardFilePath = "D:/Users/Matthew/Desktop/MTGADataFiles/data_cards_e4368ae2d4fde35ea777f1bf81e024f5.mtga";
            var textFilePath = "D:/Users/Matthew/Desktop/MTGADataFiles/data_loc_c6df9a83ee59b0ab85a775ca98d0bad4.mtga";
            Dictionary<long, string> textDictionary;
            using (StreamReader file = File.OpenText(textFilePath))
            {
                JsonSerializer serializer = new JsonSerializer();
                DataSet dataSet = (DataSet)serializer.Deserialize(file, typeof(DataSet));

                DataTable dataTable = dataSet.Tables["keys"];

                textDictionary = dataTable.Rows.Cast<DataRow>()
                                               .ToDictionary<DataRow, long, string>(row => (long)row[0],
                                                                                   row => row[1].ToString());
            }

            using (StreamReader file = File.OpenText(cardFilePath))
            {
                JsonSerializer serializer = new JsonSerializer();
                List<MtgaJsonCard> jsonCards = (List<MtgaJsonCard>)serializer.Deserialize(file, typeof(List<MtgaJsonCard>));

                var cards = jsonCards.Select(x => new Card() {
                    Id = x.GrpId,
                    Name = textDictionary[x.TitleId],
                    IsToken = x.IsToken,
                    Cmc = x.Cmc,
                    RarityId = x.Rarity,
                    SetCode = x.Set
                });

                // TODO: Jam these in dbo.Card
                Console.WriteLine(cards.Count());
            }

            return true;
        }
    }

}