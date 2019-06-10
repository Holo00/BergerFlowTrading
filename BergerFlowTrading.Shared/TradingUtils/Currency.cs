using System;
using System.Collections.Generic;
using System.Text;

namespace BergerFlowTrading.Shared.TradingUtils
{
    public static class Currency
    {
        public static bool EndsWith3DigitCurrency(string symbol)
        {
            if (symbol.EndsWith("BTC")
               || symbol.EndsWith("ETH")
               || symbol.EndsWith("NEO")
               || symbol.EndsWith("KCS")
               || symbol.EndsWith("BNB")
               || symbol.EndsWith("USD")
               || symbol.EndsWith("LTC")
               || symbol.EndsWith("XRP"))
            {
                return true;
            }

            return false;
        }

        public static bool EndsWith4DigitCurrency(string symbol)
        {
            if (symbol.EndsWith("USDT")
                || symbol.EndsWith("DOGE"))
            {
                return true;
            }

            return false;
        }

        public static string GetBaseFromSymbol(string symbol)
        {
            try
            {
                if (symbol.Length == 6)
                {
                    return GetBaseFrom6(symbol);
                }
                else if (symbol.Length == 7)
                {
                    return GetBaseFrom7(symbol);
                }
                else if (symbol.Length == 5)
                {
                    return GetBaseFrom5(symbol);
                }
                else if (symbol.Length == 8)
                {
                    return GetBaseFrom8(symbol);
                }
                else if (symbol.Length == 9)
                {
                    return GetBaseFrom9(symbol);
                }
                else if (symbol.Length == 10)
                {
                    return GetBaseFrom10(symbol);
                }
                if (symbol.Length == 4)
                {
                    return GetBaseFrom4(symbol);
                }
                else
                {
                    throw new Exception($"Length of symbol {symbol} not supported.");
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public static string GetQuoteFromSymbol(string symbol)
        {
            try
            {
                if (symbol.Length == 6)
                {
                    return GetQuoteFrom6(symbol);
                }
                else if (symbol.Length == 7)
                {
                    return GetQuoteFrom7(symbol);
                }
                else if (symbol.Length == 5)
                {
                    return GetQuoteFrom5(symbol);
                }
                else if (symbol.Length == 8)
                {
                    return GetQuoteFrom8(symbol);
                }
                else if (symbol.Length == 9)
                {
                    return GetQuoteFrom9(symbol);
                }
                else if (symbol.Length == 10)
                {
                    return GetQuoteFrom10(symbol);
                }
                else if (symbol.Length == 4)
                {
                    return GetQuoteFrom4(symbol);
                }
                else
                {
                    throw new Exception($"Length of symbol {symbol} not supported.");
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }



        private static string GetBaseFrom4(string symbol)
        {
            if (EndsWith3DigitCurrency(symbol))
            {
                return symbol.Substring(0, 1);
            }
            else
            {
                throw new Exception($"Quote currency of symbol {symbol} not supported.");
            }
        }

        private static string GetQuoteFrom4(string symbol)
        {
            if (EndsWith3DigitCurrency(symbol))
            {
                return symbol.Substring(1, 3);
            }
            else
            {
                throw new Exception($"Quote currency of symbol {symbol} not supported.");
            }
        }


        private static string GetBaseFrom5(string symbol)
        {
            if (EndsWith3DigitCurrency(symbol))
            {
                return symbol.Substring(0, 2);
            }
            else if (EndsWith4DigitCurrency(symbol))
            {
                return symbol.Substring(0, 1);
            }
            else
            {
                throw new Exception($"Quote currency of symbol {symbol} not supported.");
            }
        }

        private static string GetQuoteFrom5(string symbol)
        {
            if (EndsWith3DigitCurrency(symbol))
            {
                return symbol.Substring(2, 3);
            }
            else if (EndsWith4DigitCurrency(symbol))
            {
                return symbol.Substring(1, 4);
            }
            else
            {
                throw new Exception($"Quote currency of symbol {symbol} not supported.");
            }
        }

        private static string GetBaseFrom6(string symbol)
        {
            if (EndsWith3DigitCurrency(symbol))
            {
                return symbol.Substring(0, 3);
            }
            else if (EndsWith4DigitCurrency(symbol))
            {
                return symbol.Substring(0, 2);
            }
            else
            {
                throw new Exception($"Quote currency of symbol {symbol} not supported.");
            }
        }


        private static string GetQuoteFrom6(string symbol)
        {
            if (EndsWith3DigitCurrency(symbol))
            {
                return symbol.Substring(3, 3);
            }
            else if (EndsWith4DigitCurrency(symbol))
            {
                return symbol.Substring(2, 4);
            }
            else
            {
                throw new Exception($"Quote currency of symbol {symbol} not supported.");
            }
        }


        private static string GetBaseFrom7(string symbol)
        {
            if (EndsWith3DigitCurrency(symbol))
            {
                return symbol.Substring(0, 4);
            }
            else if (EndsWith4DigitCurrency(symbol))
            {
                return symbol.Substring(0, 3);
            }
            else
            {
                throw new Exception($"Quote currency of symbol {symbol} not supported.");
            }
        }

        private static string GetQuoteFrom7(string symbol)
        {
            if (EndsWith3DigitCurrency(symbol))
            {
                return symbol.Substring(4, 3);
            }
            else if (EndsWith4DigitCurrency(symbol))
            {
                return symbol.Substring(3, 4);
            }
            else
            {
                throw new Exception($"Quote currency of symbol {symbol} not supported.");
            }
        }


        private static string GetBaseFrom8(string symbol)
        {
            if (EndsWith3DigitCurrency(symbol))
            {
                return symbol.Substring(0, 5);
            }
            else if (EndsWith4DigitCurrency(symbol))
            {
                return symbol.Substring(0, 4);
            }
            else
            {
                throw new Exception($"Quote currency of symbol {symbol} not supported.");
            }
        }

        private static string GetQuoteFrom8(string symbol)
        {
            if (EndsWith3DigitCurrency(symbol))
            {
                return symbol.Substring(5, 3);
            }
            else if (EndsWith4DigitCurrency(symbol))
            {
                return symbol.Substring(4, 4);
            }
            else
            {
                throw new Exception($"Quote currency of symbol {symbol} not supported.");
            }
        }


        private static string GetBaseFrom9(string symbol)
        {
            if (EndsWith3DigitCurrency(symbol))
            {
                return symbol.Substring(0, 6);
            }
            else if (EndsWith4DigitCurrency(symbol))
            {
                return symbol.Substring(0, 5);
            }
            else
            {
                throw new Exception($"Quote currency of symbol {symbol} not supported.");
            }
        }

        private static string GetQuoteFrom9(string symbol)
        {
            if (EndsWith3DigitCurrency(symbol))
            {
                return symbol.Substring(6, 3);
            }
            else if (EndsWith4DigitCurrency(symbol))
            {
                return symbol.Substring(5, 4);
            }
            else
            {
                throw new Exception($"Quote currency of symbol {symbol} not supported.");
            }
        }




        private static string GetBaseFrom10(string symbol)
        {
            if (EndsWith3DigitCurrency(symbol))
            {
                return symbol.Substring(0, 7);
            }
            else if (EndsWith4DigitCurrency(symbol))
            {
                return symbol.Substring(0, 6);
            }
            else
            {
                throw new Exception($"Quote currency of symbol {symbol} not supported.");
            }
        }

        private static string GetQuoteFrom10(string symbol)
        {
            if (EndsWith3DigitCurrency(symbol))
            {
                return symbol.Substring(7, 3);
            }
            else if (EndsWith4DigitCurrency(symbol))
            {
                return symbol.Substring(6, 4);
            }
            else
            {
                throw new Exception($"Quote currency of symbol {symbol} not supported.");
            }
        }
    }
}
