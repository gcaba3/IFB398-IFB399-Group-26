using prototype2.Classes;
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

        public static void InitializeData()
        {
            products = new List<Product>();
            favourites = new List<Product>();
            quotes = new List<Quote>();
            orders = new List<Order>();
            invoices = new List<Invoice>();

            CreateFreshQuote();

            GetProductsFromDatabase();
            CreateFavorites();
            GetQuotesFromDatabase();
            GetOrdersFromDatabase();
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
                Name = "Phono Diamond 270w Module",
                Category = "Solar Panel",
                Manufacturer = "Phono Solar",
                Price = 309.99,
                Stock = 120,
                ImagePath = "phonodiamond270.png",
                Id = products.Count
            });

            products.Add(new Product
            {
                Name = "Suntech 310w Module",
                Category = "Solar Panel",
                Manufacturer = "Suntech",
                Price = 409.99,
                Stock = 114,
                ImagePath = "Suntech310wModule.jpg",
                Id = products.Count
            });

            products.Add(new Product
            {
                Name = "ABB PVI 3000TL OUTD",
                Category = "Solar Inverter",
                Manufacturer = "ABB",
                Price = 300.00,
                Stock = 32,
                ImagePath = "ABBPVI3000TLOUTD.jpg",
                Id = products.Count
            });

            products.Add(new Product
            {
                Name = "SG10EC Three Phase 10kW Inverter",
                Category = "Solar Inverter",
                Manufacturer = "Sungrow",
                Price = 1499.00,
                Stock = 26,
                ImagePath = "SG10ECThreePhase10kWInverter.jpg",
                Id = products.Count
            });

            products.Add(new Product
            {
                Name = "LG Chem RESU 6.4EX Lithium battery",
                Category = "Battery",
                Manufacturer = "LG",
                Price = 30,
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
            AddSampleQuotePlacedOrderFour();
            AddSampleQuotePlacedOrderThree();
            AddSampleQuotePlacedOrderTwo();
            AddSampleQuotePlacedOrderOne();
            AddSampleQuoteValidated();
            AddSampleQuotePendingResponse();
        }

        private static void AddSampleQuotePlacedOrderFour()
        {
            Dictionary<Product, int> quoteProducts = new Dictionary<Product, int>
            {
                { products[0], 15 },
                { products[2], 5 }
            };
            string status = QuoteStatus.OrderPlaced;
            DateTime date = new DateTime(2017, 12, 2, 3, 31, 1);

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
            DateTime date = new DateTime(2018, 3, 11, 2, 13, 54);

            AddSampleQuote(quoteProducts, status, date);
        }

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
            Quote quote = new Quote
            {
                Number = "Q" + (quotes.Count + 1).ToString("D5"),
                Status = status,
                Products = quoteProducts,
                TotalPrice = 0,
                Date = date
            };
            CalculateSalesPrice(quote);

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

        private static void CalculateSalesPrice(SalesDocument document)
        {
            double price = 0;
            foreach (KeyValuePair<Product, int> product in document.Products)
            {
                price += product.Key.Price * product.Value;
            }
            document.TotalPrice = price - (price * 0.1);
        }

        private static void CreateFreshQuote()
        {
            newQuote = new Quote
            {
                Number = "Q00000",
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
                    CheckIfNewQuoteEmpty();
                }
                else if (newQuote.Products[product] != quantity) newQuote.Products[product] = quantity;
            }
            else
            {
                newQuote.Products.Add(product, quantity);
                newQuote.Status = QuoteStatus.Incomplete;
            }
            newQuote.Date = DateTime.Now;
        }

        public static void RemoveFromQuote(String productId)
        {
            Product removedProduct = GetProductFromId(productId);
            newQuote.Products.Remove(removedProduct);
            CheckIfNewQuoteEmpty();
            newQuote.Date = DateTime.Now;
        }

        private static void CheckIfNewQuoteEmpty()
        {
            if (newQuote.Products.Count == 0) newQuote.Status = QuoteStatus.Empty;
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
            DateTime date = new DateTime(2017, 12, 2, 4, 33, 1);

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
            DateTime date = new DateTime(2018, 1, 15, 1, 2, 15);

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
            DateTime date = new DateTime(2018, 3, 12, 3, 14, 55);

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
            DateTime date = new DateTime(2018, 3, 9, 1, 19, 19);

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
            DateTime date = new DateTime(2018, 3, 10, 3, 13, 13);

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
            DateTime date = new DateTime(2018, 3, 11, 4, 14, 14);

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
            DateTime date = new DateTime(2018, 3, 13, 2, 4, 3);

            AddSampleOrder(orderProducts, status, date);
        }

        private static void AddSampleOrder(Dictionary<Product, int> orderProducts, string status, DateTime date)
        {
            Order order = new Order
            {
                Number = "SO" + (orders.Count + 1).ToString("D5"),
                Status = status,
                Products = orderProducts,
                TotalPrice = 0,
                Date = date
            };
            CalculateSalesPrice(order);

            orders.Add(order);
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
            DateTime date = new DateTime(2017, 2, 12, 4, 33, 1);
            AddSampleInvoice(invoiceProducts, status, date);
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
            AddSampleInvoice(invoiceProducts, status, date);
        }
        private static void AddSampleInvoicePartial()
        {
            Dictionary<Product, int> invoiceProducts = new Dictionary<Product, int>
            {
                { products[1], 9 },
                { products[4], 4 }
            };

            string status = InvoiceStatus.Partial;
            DateTime date = new DateTime(2018, 1, 15, 1, 2, 1);
            AddSampleInvoice(invoiceProducts, status, date);
        }
        private static void AddSampleInvoiceUnpaid()
        {
            Dictionary<Product, int> invoiceProducts = new Dictionary<Product, int>
            {
                { products[1], 12 },
                { products[3], 1 }
            };

            string status = InvoiceStatus.Unpaid;
            DateTime date = new DateTime(2018, 3, 12, 3, 14, 55);
            AddSampleInvoice(invoiceProducts, status, date);
        }

        private static void AddSampleInvoice(Dictionary<Product, int> invoiceProducts, string status, DateTime date)
        {
            Invoice invoice = new Invoice
            {
                Number = "I" + (invoices.Count + 1).ToString("D5"),
                Status = status,
                Products = invoiceProducts,
                TotalPrice = 0,
                Date = date
            };
            CalculateSalesPrice(invoice);

            invoices.Add(invoice);
        }
    }
}
