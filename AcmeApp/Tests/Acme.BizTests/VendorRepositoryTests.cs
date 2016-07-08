using Microsoft.VisualStudio.TestTools.UnitTesting;
using Acme.Biz;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Acme.Biz.Tests
{
    [TestClass()]
    public class VendorRepositoryTests
    {
        [TestMethod]
        public void RetrieveValueTest()
        {
            var repo = new VendorRepository();
            var expected = 42;

            var actual = repo.RetrieveValue<int>("Select ...", 42);

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void RetrieveValueStringTest()
        {
            var repo = new VendorRepository();
            var expected = "test";

            var actual = repo.RetrieveValue<string>("Select ...", "test");

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void RetrieveValueObjectTest()
        {
            var repo = new VendorRepository();
            var vendor = new Vendor();
            var expected = vendor;

            var actual = repo.RetrieveValue<Vendor>("Select ...", vendor);

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void RetrieveTest()
        {
            var repo = new VendorRepository();
            var expected = new List<Vendor>() {
                new Vendor { VendorId = 1, CompanyName = "ABC Corp", Email = "contact@abc.com" },
                new Vendor { VendorId = 2, CompanyName = "XYZ Corp", Email = "contact@xyz.com" }};

            var actual = repo.Retrieve();

            CollectionAssert.AreEqual(expected, actual.ToList());
        }

        [TestMethod]
        public void RetrieveWithIteratorTest()
        {
            var repo = new VendorRepository();
            var expected = new List<Vendor>() {
                new Vendor { VendorId = 1, CompanyName = "ABC Corp", Email = "contact@abc.com" },
                new Vendor { VendorId = 2, CompanyName = "XYZ Corp", Email = "contact@xyz.com" }};

            var vendorIterator = repo.RetrieverWithIterator();

            foreach (var item in vendorIterator)
                Console.WriteLine(item);

            CollectionAssert.AreEqual(expected,
                                      actual: vendorIterator.ToList());
        }

        [TestMethod()]
        public void RetrieveAllTest()
        {
            var repo = new VendorRepository();
            var expected = new List<Vendor>(){
                new Vendor { VendorId = 5, CompanyName = "Amalgamated Toys", Email = "contact@amt.com" },
                new Vendor { VendorId = 6, CompanyName = "Toy Blocks", Email = "contact@toy.com" }};

            //var test = new List<int>() { 1, 2, 3, 4, 5, 6 };

            var vendors = repo.RetrieveAll();

            var query = from v in vendors
                        where v.CompanyName.Contains("Toy")
                        orderby v.CompanyName
                        select v;

            query = vendors
                .Where(delegate (Vendor vendor) { return vendor.CompanyName.Contains("Toy"); })
                .OrderBy( delegate(Vendor v) {return v.CompanyName;});

            query = vendors
                .Where(e => e.CompanyName.Contains("Toy"))
                .OrderBy(e => e.CompanyName);

            Console.WriteLine(query.Count());

            CollectionAssert.AreEqual(expected, actual: query.ToList());
        }
    }
}