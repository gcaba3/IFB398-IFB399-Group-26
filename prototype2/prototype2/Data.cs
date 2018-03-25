using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace prototype2
{
    public static class Data
    {
        public static List<Product> products;
        public static List<Product> favourites;
        public static List<Quote> quotes;
        public static Quote newQuote;
        public static List<Order> orders;
        public static List<Invoice> invoices;
        public static int numSalesOrders, numInvoices; // just used to make up document numbers for now

        public static void InitializeData()
        {
            products = new List<Product>();
            favourites = new List<Product>();
            quotes = new List<Quote>();
            orders = new List<Order>();
            invoices = new List<Invoice>();
            numSalesOrders = 0;
            numInvoices = 0;

            CreateFreshQuote();

            GetProductsFromDatabase();
            CreateFavorites();
            
            GetOrdersFromDatabase();
            GetQuotesFromDatabase();
            GetInvoicesFromDatabase();            
        }

        private static void CreateFavorites()
        {
            favourites.Add(products[0]);
            favourites.Add(products[2]);
        }

        /// <summary>
        /// Makes up some products that could be listed in the database and adds them to the list of products
        /// </summary>
        private static void GetProductsFromDatabase()
        {
            products.Add(new Product
            {
                Description = "[PH270W] Phono Diamond 270W Poly",
                Category = "Solar Panel",
                Manufacturer = "Phono Solar",
                Price = 156.60,
                Stock = 120,
                ImagePath = "phonodiamond270.png",
                Id = products.Count
            });

            products.Add(new Product
            {
                Description = "[STP320-24/Vem] Suntech Power 320W Poly",
                Category = "Solar Panel",
                Manufacturer = "Suntech",
                Price = 201.60,
                Stock = 114,
                ImagePath = "Suntech310wModule.jpg",
                Id = products.Count
            });

            products.Add(new Product
            {
                Description = "[ABBPVI-5.0] PVI 5000 OUTD",
                Category = "Solar Inverter",
                Manufacturer = "ABB",
                Price = 1280,
                Stock = 32,
                ImagePath = "ABBPVI3000TLOUTD.jpg",
                Id = products.Count
            });

            products.Add(new Product
            {
                Description = "[SG5KTL-D ] Sungrow 5kW dual MPPT inverter",
                Category = "Solar Inverter",
                Manufacturer = "Sungrow",
                Price = 1499.00,
                Stock = 26,
                ImagePath = "SG10ECThreePhase10kWInverter.jpg",
                Id = products.Count
            });

            products.Add(new Product
            {
                Description = "[LGRESU6.5] RESU 6.5",
                Category = "Battery",
                Manufacturer = "LG",
                Price = 4599,
                Stock = 56,
                ImagePath = "LGChemRESULithiumbattery",
                Id = products.Count
            });
        }

        /// <summary>
        /// Makes up some quotes that could be kept in the database and adds them to the list of quotes
        /// </summary>
        private static void GetQuotesFromDatabase()
        {
            //AddSampleQuotePlacedOrderFour();
            //AddSampleQuotePlacedOrderThree();
            //AddSampleQuotePlacedOrderTwo();
            //AddSampleQuotePlacedOrderOne();
            AddSampleQuoteValidated();
            AddSampleQuotePendingResponse();
        }

        /*private static void AddSampleQuotePlacedOrderFour()
        {
            Dictionary<Product, int> quoteProducts = new Dictionary<Product, int>
            {
                { products[0], 15 },
                { products[2], 5 }
            };
            string status = QuoteStatus.OrderPlaced;
            DateTime date = new DateTime(2017, 12, 2, 15, 31, 1);

            AddSampleQuote(quoteProducts, status, date);
        }

        private static void AddSampleQuotePlacedOrderThree()
        {
            Dictionary<Product, int> quoteProducts = new Dictionary<Product, int>
            {
                { products[4], 5 },
                { products[1], 10 },
                { products[2], 15 }
            };
            string status = QuoteStatus.OrderPlaced;
            DateTime date = new DateTime(2018, 1, 3, 11, 21, 51);

            AddSampleQuote(quoteProducts, status, date);
        }

        private static void AddSampleQuotePlacedOrderTwo()
        {
            Dictionary<Product, int> quoteProducts = new Dictionary<Product, int>
            {
                { products[1], 9 },
                { products[4], 4 }
            };
            string status = QuoteStatus.OrderPlaced;
            DateTime date = new DateTime(2018, 1, 14, 12, 1, 59);

            AddSampleQuote(quoteProducts, status, date);
        }

        private static void AddSampleQuotePlacedOrderOne()
        {
            Dictionary<Product, int> quoteProducts = new Dictionary<Product, int>
            {
                { products[1], 12 },
                { products[3], 1 }
            };
            string status = QuoteStatus.OrderPlaced;
            DateTime date = new DateTime(2018, 3, 11, 14, 13, 54);

            AddSampleQuote(quoteProducts, status, date);
        }*/

        private static void AddSampleQuoteValidated()
        {
            Dictionary<Product, int> quoteProducts = new Dictionary<Product, int>
            {
                { products[0], 24 },
                { products[2], 1 }
            };
            string status = QuoteStatus.Validated;
            DateTime date = new DateTime(2018, 3, 12, 10, 50, 46);

            AddSampleQuote(quoteProducts, status, date);
        }

        private static void AddSampleQuotePendingResponse()
        {
            Dictionary<Product, int> quoteProducts = new Dictionary<Product, int>
            {
                { products[1], 22 },
                { products[3], 3 }
            };
            string status = QuoteStatus.PendingResponse;
            DateTime date = new DateTime(2018, 3, 13, 12, 34, 52);

            AddSampleQuote(quoteProducts, status, date);
        }

        private static void AddSampleQuote(Dictionary<Product, int> quoteProducts, string status, DateTime date)
        {
            numSalesOrders++;
            Quote quote = new Quote
            {
                Number = "SO" + numSalesOrders.ToString("D5"),
                Status = status,
                Products = quoteProducts,
                TotalPrice = 0,
                Date = date
            };

            UpdatePrice(quote);

            quotes.Add(quote);
        }

        public static Product GetProductFromId(string productId)
        {
            return products.Single(Product => Product.Id == Int32.Parse(productId));
        }

        public static void AddFavouriteToList(string productId)
        {
            var addedProduct = products.Single(Product => Product.Id == Int32.Parse(productId));
            favourites.Add(addedProduct);

        }
        public static void RemoveFavouriteFromList(string productId)
        {
            var removedProduct = GetProductFromId(productId);
            favourites.Remove(removedProduct);
        }

        public static void UpdatePrice(SalesDocument document)
        {
            double price = 0;
            foreach (KeyValuePair<Product, int> product in document.Products)
            {
                price += product.Key.Price * product.Value;
            }
            document.TotalPrice = price - (price * 0.1);
        }

        public static void CreateFreshQuote()
        {
            newQuote = new Quote
            {
                Number = "SO00000",
                Status = QuoteStatus.Empty,
                Products = new Dictionary<Product, int>(),
                TotalPrice = 0,
                Date = DateTime.Now
            };
        }

        /// <summary>
        /// Edits the quantity of the product that will be added to the order
        /// </summary>
        /// <param name="productId"></param>
        /// <param name="quantity"></param>
        public static void EditQuoteProductQuantity(String productId, int quantity)
        {
            Product product = GetProductFromId(productId);
            if (newQuote.Products.ContainsKey(product))
            {
                if (quantity == 0)
                {
                    newQuote.Products.Remove(product);
                    if (CheckIfNewQuoteEmpty())
                        return;
                }
                else if (newQuote.Products[product] != quantity) newQuote.Products[product] = quantity;
            }
            else
            {
                newQuote.Products.Add(product, quantity);
                newQuote.Status = QuoteStatus.Incomplete;
            }
            newQuote.Date = DateTime.Now;

            UpdatePrice(newQuote);
        }

        public static void RemoveFromQuote(String productId)
        {
            Product removedProduct = GetProductFromId(productId);
            newQuote.Products.Remove(removedProduct);
            CheckIfNewQuoteEmpty();
            newQuote.Date = DateTime.Now;
        }

        private static bool CheckIfNewQuoteEmpty()
        {
            if (newQuote.Products.Count == 0)
            {
                newQuote.Status = QuoteStatus.Empty;
                return true;
            }

            return false;
        }

        public static Quote GetQuoteFromId(string quoteNumber)
        {
            if (quoteNumber == "SO00000")
                return newQuote;
            else
                return quotes.Single(Quote => Quote.Number == quoteNumber.ToString());
        }

        public static void DeleteQuote(Quote quote)
        {
            quotes.Remove(quote);
        }

        /// <summary>
        /// Makes up some orders that could be kept in the database and adds them to the list of orders
        /// </summary>
        private static void GetOrdersFromDatabase()
        {
            AddSampleOrderCompleteFour();
            AddSampleOrderCompleteThree();
            AddSampleOrderCompleteTwo();
            AddSampleOrderCompleteOne();
            AddSampleOrderInTransit();
            AddSampleOrderDispatchReady();
            AddSampleOrderPacking();
            AddSampleOrderConfirmedSale();
        }

        private static void AddSampleOrderCompleteFour()
        {
            Dictionary<Product, int> orderProducts = new Dictionary<Product, int>
            {
                { products[0], 15 },
                { products[2], 5 }
            };

            string status = OrderStatus.Complete;
            DateTime date = new DateTime(2017, 12, 14, 4, 33, 1);

            AddSampleOrder(orderProducts, status, date);
        }
        private static void AddSampleOrderCompleteThree()
        {
            Dictionary<Product, int> orderProducts = new Dictionary<Product, int>
            { 
                { products[4], 5 },
                { products[1], 10 },
                { products[2], 15 }
            };

            string status = OrderStatus.Complete;
            DateTime date = new DateTime(2018, 1, 4, 12, 22, 50);

            AddSampleOrder(orderProducts, status, date);
        }
        private static void AddSampleOrderCompleteTwo()
        {
            Dictionary<Product, int> orderProducts = new Dictionary<Product, int>
            {
                { products[1], 9 },
                { products[4], 4 }
            };

            string status = OrderStatus.Complete;
            DateTime date = new DateTime(2018, 1, 15, 13, 2, 15);

            AddSampleOrder(orderProducts, status, date);
        }
        private static void AddSampleOrderCompleteOne()
        {
        Dictionary<Product, int> orderProducts = new Dictionary<Product, int>
        {
            { products[1], 12 },
            { products[3], 1 }
            };

            string status = OrderStatus.Complete;
            DateTime date = new DateTime(2018, 3, 12, 15, 14, 55);

            AddSampleOrder(orderProducts, status, date);
        }
        private static void AddSampleOrderInTransit()
        {
            Dictionary<Product, int> orderProducts = new Dictionary<Product, int>
            {
                { products[1], 15 },
                { products[3], 4 }
            };

            string status = OrderStatus.InTransit;
            DateTime date = new DateTime(2018, 3, 9, 13, 19, 19);

            AddSampleOrder(orderProducts, status, date);
        }
        private static void AddSampleOrderDispatchReady()
        {
            Dictionary<Product, int> orderProducts = new Dictionary<Product, int>
            {
                { products[1], 20 },
                { products[4], 4 }
            };

            string status = OrderStatus.DispatchReady;
            DateTime date = new DateTime(2018, 3, 10, 15, 13, 13);

            AddSampleOrder(orderProducts, status, date);
        }
        private static void AddSampleOrderPacking()
        {
            Dictionary<Product, int> orderProducts = new Dictionary<Product, int>
            {
                { products[0], 15 },
                { products[3], 3 }
            };
            string status = OrderStatus.Packing;
            DateTime date = new DateTime(2018, 3, 11, 16, 14, 14);

            AddSampleOrder(orderProducts, status, date);
        }
        private static void AddSampleOrderConfirmedSale()
        {
            Dictionary<Product, int> orderProducts = new Dictionary<Product, int>
            {
                { products[1], 9 },
                { products[4], 4 }
            };

            string status = OrderStatus.ConfirmedSale;
            DateTime date = new DateTime(2018, 3, 13, 14, 4, 3);

            AddSampleOrder(orderProducts, status, date);
        }

        private static void AddSampleOrder(Dictionary<Product, int> orderProducts, string status, DateTime date)
        {
            numSalesOrders++;
            Order order = new Order
            {
                Number = "SO" + numSalesOrders.ToString("D5"),
                Status = status,
                Products = orderProducts,
                TotalPrice = 0,
                Date = date
            };
            UpdatePrice(order);

            orders.Add(order);
        }

        public static Order GetOrderFromId(string orderNumber)
        {
            return orders.Single(Order => Order.Number == orderNumber.ToString());
        }

        /// <summary>
        /// Makes up some invoices that could be kept in the database and adds them to the list of invoices
        /// </summary>
        private static void GetInvoicesFromDatabase()
        {
            AddSampleInvoicePaid();
            AddSampleInvoiceOverdue();
            AddSampleInvoicePartial();
            AddSampleInvoiceUnpaid();
        }

        private static void AddSampleInvoicePaid()
        {
            Dictionary<Product, int> invoiceProducts = new Dictionary<Product, int>
            {
                { products[0], 15 },
                { products[2], 5 }
            };

            string status = InvoiceStatus.Paid;
            DateTime date = new DateTime(2017, 2, 12, 16, 33, 1);
            AddSampleInvoice(invoiceProducts, status, date, "SO00001", 0);
        }
        private static void AddSampleInvoiceOverdue()
        {
            Dictionary<Product, int> invoiceProducts = new Dictionary<Product, int>
            {
                { products[4], 5 },
                { products[1], 10 },
                { products[2], 15 }
            };

            string status = InvoiceStatus.Overdue;
            DateTime date = new DateTime(2018, 1, 4, 12, 22, 50);
            AddSampleInvoice(invoiceProducts, status, date, "SO00002", 0);
        }
        private static void AddSampleInvoicePartial()
        {
            Dictionary<Product, int> invoiceProducts = new Dictionary<Product, int>
            {
                { products[1], 9 },
                { products[4], 4 }
            };

            string status = InvoiceStatus.Partial;
            DateTime date = new DateTime(2018, 1, 15, 13, 2, 1);
            AddSampleInvoice(invoiceProducts, status, date, "SO00003", 4000);
        }
        private static void AddSampleInvoiceUnpaid()
        {
            Dictionary<Product, int> invoiceProducts = new Dictionary<Product, int>
            {
                { products[1], 12 },
                { products[3], 1 }
            };

            string status = InvoiceStatus.Unpaid;
            DateTime date = new DateTime(2018, 3, 12, 15, 14, 55);
            AddSampleInvoice(invoiceProducts, status, date, "SO00004", 0);
        }

        private static void AddSampleInvoice(Dictionary<Product, int> invoiceProducts, string status, DateTime date, string source, double amountPaid)
        {
            numInvoices++;
            Invoice invoice = new Invoice
            {
                Number = "IN" + (invoices.Count + 1).ToString("D5"),
                Status = status,
                Products = invoiceProducts,
                TotalPrice = 0,
                Date = date,
                DateDue = date.AddDays(60),
                Source = source,
                AmountPaid = amountPaid
            };
            UpdatePrice(invoice);

            invoice.AmountDue = invoice.TotalPrice - invoice.AmountDue;

            invoices.Add(invoice);
        }

        public static Invoice GetInvoiceFromId(string invoiceNumber)
        {
            return invoices.Single(Invoice => Invoice.Number == invoiceNumber.ToString());
        }

        public static void ConvertQuoteToOrder(Quote quote)
        {
            Order order = new Order
            {
                Date = DateTime.Now,
                Number = quote.Number,
                Products = quote.Products,
                Status = OrderStatus.ConfirmedSale,
                TotalPrice = quote.TotalPrice,
            };

            orders.Add(order);
            quotes.Remove(quote);
        }
    }
}
