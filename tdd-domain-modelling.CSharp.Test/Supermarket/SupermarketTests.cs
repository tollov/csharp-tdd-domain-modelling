﻿using NUnit.Framework;
using tdd_domain_modelling.CSharp.Main.Supermarket;

namespace tdd_domain_modelling.CSharp.Test.Supermarket
{
    class SupermarketTests
    {
        [Test]
        public void OrderTotalCostTest()
        {
            Order order = new Order();
            decimal totalCost = order.TotalCost();
            Assert.That(totalCost == 0m);
            order.AddItem(new Product("Apple", 1m), 3);
            order.AddItem(new Product("Pear", 2m), 1);
            Assert.That(order.TotalCost() == 5m);
            order.AddItem(new Product("Wine", 41m), 7);
            Assert.That(order.TotalCost() == 5m + 41m * 7);
        }

        [Test]
        public void OrderItemsTest()
        {
            Order order = new Order();
            List<Tuple<Product, int>> items = order.Items;
            order.AddItem(new Product("Apple", 1m), 3);
            order.AddItem(new Product("Grapes", 5m), 1);
            Assert.That(items.Count == 2);
        }

        [Test]
        public void OrderGenerateItimizedReceiptTest()
        {
            Order order = new Order();
            order.AddItem(new Product("Ham", 20m), 2);
            order.AddItem(new Product("Yoghurt", 12m), 4);
            order.AddItem(new Product("Milk", 12m), 3);
            order.AddItem(new Product("Extra Virgin Olive Oil", 30m), 1);
            order.AddItem(new Product("Toamto", 4m), 25);
            string receipt = order.GenerateItimizedReceipt();
            Assert.That(receipt.Length > 0);
            Console.WriteLine(receipt);
        }

        [Test]
        public void ProductTest()
        {
            Product product1 = new Product("Milk", 12.5m);
            Product product2 = new Product("Flour", 15m);
            Product product3 = new Product("Eggs", 15m);
            Assert.That(product1.Price < product2.Price);
            Assert.That(product2.Price == product3.Price);
        }

        [Test]
        public void ShopperAddToCartTest()
        {
            Shopper shopper1 = new Shopper("Phillip");
            Shopper shopper2 = new Shopper("Cathrine");
            Product cheese = new Product("Cheese", 20m);
            Product wine = new Product("Wine", 45m);
            Product fish = new Product("Fish", 25m);
            Assert.That(shopper1.Cart.Items.Count == 0);
            Assert.That(shopper2.Cart.Items.Count == 0);
            shopper1.AddToCart(wine);
            shopper2.AddToCart(cheese);
            shopper2.AddToCart(fish);
            Assert.That(shopper1.Cart.Items.Count == 1);
            Assert.That(shopper2.Cart.Items.Count == 2);
            Assert.That(shopper1.Cart.TotalCost() == shopper2.Cart.TotalCost());
        }

        [Test]
        public void ShopperClearCartTest()
        {
            Shopper shopper = new Shopper("Phillip");
            Product wine = new Product("Wine", 45m);
            Product orange = new Product("Orange", 4m);
            Assert.That(shopper.Cart.Items.Count == 0);
            shopper.AddToCart(wine, 200);
            shopper.AddToCart(orange);
            Assert.That(shopper.Cart.Items.Count == 2);
            shopper.ClearCart();
            Assert.That(shopper.Cart.Items.Count == 0);
            shopper.AddToCart(orange);
            Assert.That(shopper.Cart.Items.Count == 1);
        }

        [Test]
        public void ShopperBuyItemsTest()
        {
            Shopper shopper = new Shopper("Phillip");
            Product wine = new Product("Wine", 45m);
            for (int i = 0;  i < 7; i++)
            {
                shopper.AddToCart(wine, 90);
                Assert.That(shopper.Cart.Items.Count() == 1);
                Assert.That(shopper.Orders.Count() == i);
                shopper.BuyItems();
                Assert.That(shopper.Cart.Items.Count() == 0);
                Assert.That(shopper.Orders.Count() == i + 1);
            }
        }
    }
}
