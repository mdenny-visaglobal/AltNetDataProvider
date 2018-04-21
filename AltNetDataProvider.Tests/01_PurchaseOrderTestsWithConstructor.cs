﻿using System;
using AltNetDataProvider.Domain;
using FluentAssertions;
using NUnit.Framework;
// ReSharper disable ObjectCreationAsStatement

namespace AltNetDataProvider.Tests
{
    [TestFixture]
    public class PurchaseOrderTestsWithConstructor
    {
        [Test]
        public void SimpleConstruction()
        {
            var po = new PurchaseOrder(
                new Customer("123", "Celia Smith"),
                "PO333",
                DateTime.Today.AddDays(7),
                new[]
                {
                    new PurchaseOrderLine(
                        new Item("Item123", "Chair", 123, 456, new Money("USD", 100.0m)),
                        10)
                });

            po.Should().NotBeNull();
        }

        [Test]
        public void ItemWeight_CannotBeZero()
        {
            Assert.Throws<ArgumentException>(() => new Item("Item123", "Chair", 0, 456, new Money("USD", 100.0m)))
                .ParamName.Should().Be("weight");
        }

        [Test]
        public void Cannot_RequestDeliveryInThePast()
        {
            Assert.Throws<ArgumentException>(() => new TestObjectBuilder<PurchaseOrder>()
                .SetArgument(o => o.RequiredDeliveryDate, DateTime.Today.AddDays(-1))
                .Build());
        }
    }
}