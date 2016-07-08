using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Acme.Biz
{
    public class VendorRepository
    {
        //private List<Vendor> vendors;
        
        /// <summary>
        /// Retrieve one vendor.
        /// </summary>
        /// <param name="vendorId">Id of the vendor to retrieve.</param>
        public Vendor Retrieve(int vendorId)
        {
            // Create the instance of the Vendor class
            Vendor vendor = new Vendor();

            // Code that retrieves the defined customer

            // Temporary hard coded values to return 
            if (vendorId == 1)
            {
                vendor.VendorId = 1;
                vendor.CompanyName = "ABC Corp";
                vendor.Email = "abc@abc.com";
            }
            return vendor;
        }

        /// <summary>
        /// Retrieve all vendors
        /// </summary>
        /// <returns></returns>
        public IEnumerable<Vendor> Retrieve()
        { 
            return new List<Vendor>() {
                new Vendor { VendorId = 1, CompanyName = "ABC Corp", Email = "contact@abc.com" },
                new Vendor { VendorId = 2, CompanyName = "XYZ Corp", Email = "contact@xyz.com" }};
        }

        /// <summary>
        /// Retrieve all vendors
        /// </summary>
        /// <returns></returns>
        public IEnumerable<Vendor> RetrieveAll()
        {
            return new List<Vendor>() {
                new Vendor { VendorId = 1, CompanyName = "ABC Corp", Email = "contact@abc.com" },
                new Vendor { VendorId = 2, CompanyName = "XYZ Corp", Email = "contact@xyz.com" },
                new Vendor { VendorId = 3, CompanyName = "EFG Ltd", Email = "contact@efg.com" },
                new Vendor { VendorId = 4, CompanyName = "HIJ AG", Email = "contact@hij.com" },
                new Vendor { VendorId = 5, CompanyName = "Amalgamated Toys", Email = "contact@amt.com" },
                new Vendor { VendorId = 6, CompanyName = "Toy Blocks", Email = "contact@toy.com" },
                new Vendor { VendorId = 7, CompanyName = "Home Products Inc", Email = "contact@home.com" }};
        }

        public IEnumerable<Vendor> RetrieverWithIterator()
        {
            var vendors = this.Retrieve();

            foreach (var vendor in vendors)
            {
                Console.WriteLine($"Vendor Id: {vendor.VendorId}");
                yield return vendor;
            }
        }


        /// <summary>
        /// Call the database to retrieve the value
        /// If no value returned, retrieve default value
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="defaultValue"></param>
        /// <returns></returns>
        public T RetrieveValue<T>(string sql, T defaultValue) =>
            defaultValue;

        /// <summary>
        /// Save data for one vendor.
        /// </summary>
        /// <param name="vendor">Instance of the vendor to save.</param>
        /// <returns></returns>
        public bool Save(Vendor vendor)
        {
            var success = true;

            // Code that saves the vendor

            return success;
        }
    }
}
