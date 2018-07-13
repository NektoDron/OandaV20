﻿using OkonkwoOandaV20.TradeLibrary.Transaction;
using System.Threading.Tasks;

namespace OkonkwoOandaV20.TradeLibrary.REST
{
   public partial class Rest20
   {
      /// <summary>
      /// Close (partially or full) a specific open Trade in an Account
      /// </summary>
      /// <param name="accountID">Account identifier</param>
      /// <param name="tradeSpecifier">The Trade specifier</param>
      /// <returns>DeleteTradeResponse containing the details of the close</returns>
      public static async Task<TradeCloseResponse> PutTradeCloseAsync(string accountID, long tradeSpecifier, TradeCloseParameters parameters = null)
      {
         string uri = ServerUri(EServer.Account) + "accounts/" + accountID + "/trades/" + tradeSpecifier + "/close";

         parameters = parameters ?? new TradeCloseParameters();

         return await MakeRequestWithJSONBody<TradeCloseResponse, TradeCloseErrorResponse, TradeCloseParameters>("PUT", parameters, uri);
      }

      public class TradeCloseParameters
      {
         public TradeCloseParameters() { units = "ALL"; }

         /// <summary>
         /// Indication of how much of the Trade to close. Either the string “ALL”
         /// (indicating that all of the Trade should be closed), or a DecimalNumber
         /// representing the number of units of the open Trade to Close using a
         /// TradeClose MarketOrder. The units specified must always be positive, and
         /// the magnitude of the value cannot exceed the magnitude of the Trade’s
         /// open units.
         /// </summary>
         public string units { get; set; }
      }
   }

   public class TradeCloseResponse : Response
   {
      /// <summary>
      /// The MarketOrder Transaction created to close the Trade.
      /// </summary>
      public MarketOrderTransaction orderCreateTransaction { get; set; }

      /// <summary>
      /// The OrderFill Transaction that fills the Trade-closing MarketOrder and 
      /// closes the Trade.
      /// </summary>
      public OrderFillTransaction orderFillTransaction { get; set; }

      /// <summary>
      /// The OrderCancel Transaction that immediately cancelled the Trade-closing 
      /// MarketOrder.
      /// </summary>
      public OrderCancelTransaction orderCancelTransaction { get; set; }
   }

   public class TradeCloseErrorResponse : ErrorResponse
   {
      /// <summary>
      /// The MarketOrderReject Transaction that rejects the creation of the Trade-
      /// closing MarketOrder.
      /// </summary>
      public MarketOrderRejectTransaction orderRejectTransaction { get; set; }
   }
}
