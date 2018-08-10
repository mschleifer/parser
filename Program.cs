using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using HtmlAgilityPack;
using Newtonsoft.Json;

namespace mtgparser
{
    class Program
    {
        static void Main(string[] args)
        {
            var setupSucceeded = Setup.BuildDatabase();

            var filePath = "Log.htm";
            var htmlLog = new HtmlDocument();
            htmlLog.Load(filePath);

            Dictionary<int, string> cardDictionary = new Dictionary<int, string>();

            var rawNodes = htmlLog.DocumentNode.SelectNodes("//div[@class='header-text']").ToList();
            var processedNodes = rawNodes.Where(x => x.InnerHtml.Contains("Event.Draft") ||
                                                     x.InnerHtml.Contains("Draft.MakePick") ||
                                                     x.InnerHtml.Contains("Event.DeckSubmit") ||
                                                     x.InnerHtml.Contains("Event.MatchCreated") ||
                                                     x.InnerHtml.Contains("DuelScene.GameStart") ||
                                                     x.InnerHtml.Contains("DuelScene.GameStop") ||
                                                     x.InnerHtml.Contains("DuelScene.EndOfMatchReport") ||
                                                     x.InnerHtml.Contains("Rank.Updated") ||
                                                     x.InnerHtml.Contains("MatchGameRoomStateChangedEvent") ||
                                                     x.InnerHtml.Contains("Card picked"));

            Console.WriteLine($"Total nodes: {rawNodes.Count} " +
                              $"| Processed nodes: {processedNodes.Count()}"
                            );

            // Build card dictionary (to be replaced with a DB)
            for (var i = 0; i < processedNodes.Count(); i++)
            {
                if (processedNodes.ElementAt(i).InnerHtml.Contains("Card picked"))
                {
                    var node = processedNodes.ElementAt(i);
                    var cardName = node.InnerHtml.Substring(node.InnerHtml.IndexOf(':') + 1).Trim();

                    var pickNode = processedNodes.ElementAt(i + 1);
                    var pickJson = pickNode.InnerHtml.Substring(pickNode.InnerHtml.IndexOf('{'));
                    DraftPickOutbound draftPickOut = JsonConvert.DeserializeObject<DraftPickOutbound>(pickJson);

                    if (!cardDictionary.ContainsKey(draftPickOut.Params.CardId))
                    {
                        cardDictionary.Add(draftPickOut.Params.CardId, cardName);
                    }
                }
            }

            var draftPicksOutbound = processedNodes.Where(x => x.InnerHtml.Contains("Draft.MakePick") &&
                                                               x.InnerHtml.Contains("==>"))
                                                   .Select(x => JsonConvert.DeserializeObject<DraftPickOutbound>(x.InnerHtml.Substring(x.InnerHtml.IndexOf('{'))));
            // var draftPicksInbound = processedNodes.Where(x => x.InnerHtml.Contains("Draft.MakePick") &&
            //                                                   x.InnerHtml.Contains("<=="))
            //                                       .Select(x => JsonConvert.DeserializeObject<DraftPickInbound>(x.InnerHtml.Substring(x.InnerHtml.IndexOf('{'))));
            
            var draftPicks = draftPicksOutbound.Select(x => new DraftPick(x));

            foreach (var pick in draftPicks)
            {
                Console.WriteLine($"{cardDictionary[pick.CardId]} | Pack {pick.PackNumber} | Pick {pick.PickNumber}");
            }
        }
    }
}

// for (var i = 0; i < processedNodes.Count(); i++)
//             {
//                 if (processedNodes.ElementAt(i).InnerHtml.Contains("Card picked"))
//                 {
//                     var node = processedNodes.ElementAt(i);
//                     var cardName = node.InnerHtml.Substring(node.InnerHtml.IndexOf(':') + 1).Trim();

//                     var pickNode = processedNodes.ElementAt(i + 1);
//                     var pickJson = pickNode.InnerHtml.Substring(pickNode.InnerHtml.IndexOf('{'));
//                     DraftPickOutbound draftPickOut = JsonConvert.DeserializeObject<DraftPickOutbound>(pickJson);

//                     pickNode = processedNodes.ElementAt(i + 2);
//                     pickJson = pickNode.InnerHtml.Substring(pickNode.InnerHtml.IndexOf('{'));
//                     DraftPickInbound draftPickIn = JsonConvert.DeserializeObject<DraftPickInbound>(pickJson);

//                     cardDictionary.Add(draftPickOut.Params.CardId, cardName);

//                     Console.WriteLine($"{cardName} | Pack {draftPickOut.Params.PackNumber + 1} | Pick {draftPickOut.Params.PickNumber + 1}");
//                 }
//             }