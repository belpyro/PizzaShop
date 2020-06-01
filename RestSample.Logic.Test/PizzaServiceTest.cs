using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using RestSample.Data.Contexts;
using RestSample.Data.Models;

namespace RestSample.Logic.Test
{
    [TestClass]
    public class PizzaServiceTest
    {
        [TestMethod]
        public void Test_Success_Add_New_Pizza()
        {
            var source = new List<PizzaDb> { new PizzaDb() { }, new PizzaDb() { }, }.AsQueryable();

            var dbSet = new Mock<DbSet<PizzaDb>>();
            dbSet.As<IQueryable<PizzaDb>>().Setup(s => s.Provider).Returns(source.Provider);
            dbSet.As<IQueryable<PizzaDb>>().Setup(s => s.ElementType).Returns(source.ElementType);
            dbSet.As<IQueryable<PizzaDb>>().Setup(s => s.GetEnumerator()).Returns(source.GetEnumerator());
            dbSet.As<IQueryable<PizzaDb>>().Setup(s => s.Expression).Returns(source.Expression);

            var contextMock = new Mock<PizzaShopContext>();
            contextMock.Setup(x => x.Pizzas).Returns(dbSet.Object);

            //call service add method


            contextMock.Verify(s => s.SaveChanges(), Times.Once);
            dbSet.Verify(s => s.Add(It.IsAny<PizzaDb>()), Times.Once);


        }
    }
}
