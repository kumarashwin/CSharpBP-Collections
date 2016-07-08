using Acme.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Acme.Biz
{
    /// <summary>
    /// Manages the approved vendors from whom Acme purchases our inventory.
    /// </summary>
    public class Vendor
    {
        public int VendorId { get; set; }
        public string CompanyName { get; set; }
        public string Email { get; set; }

        /// <summary>
        /// Sends a product order to the vendor.
        /// </summary>
        /// <param name="product">Product to order.</param>
        /// <param name="quantity">Quantity of the product to order.</param>
        /// <param name="deliverBy">Requested delivery date.</param>
        /// <param name="instructions">Delivery instructions.</param>
        /// <returns></returns>
        public OperationResult<bool> PlaceOrder(Product product, int quantity,
                                            DateTimeOffset? deliverBy = null,
                                            string instructions = "standard delivery")
        {
            if (product == null)
                throw new ArgumentNullException(nameof(product));
            if (quantity <= 0)
                throw new ArgumentOutOfRangeException(nameof(quantity));
            if (deliverBy <= DateTimeOffset.Now)
                throw new ArgumentOutOfRangeException(nameof(deliverBy));

            var success = false;

            var orderTextBuilder = new StringBuilder("Order from Acme, Inc" +
                            System.Environment.NewLine +
                            "Product: " + product.ProductName +
                            System.Environment.NewLine +
                            "Quantity: " + quantity);
            if (deliverBy.HasValue)
            {
                orderTextBuilder.Append( System.Environment.NewLine +
                            "Deliver By: " + deliverBy.Value.ToString("d"));
            }
            if (!String.IsNullOrWhiteSpace(instructions))
            {
                orderTextBuilder.Append( System.Environment.NewLine +
                            "Instructions: " + instructions);
            }
            var orderText = orderTextBuilder.ToString();

            var emailService = new EmailService();
            var confirmation = emailService.SendMessage("New Order", orderText,
                                                                     this.Email);
            if (confirmation.StartsWith("Message sent:"))
            {
                success = true;
            }
            var operationResult = new OperationResult<bool>(success, orderText);
            return operationResult;
        }   


        /// <summary>
        /// Sends an email to welcome a new vendor.
        /// </summary>
        /// <returns></returns>
        public string SendWelcomeEmail(string message)
        {
            var emailService = new EmailService();
            var subject = ("Hello " + this.CompanyName).Trim();
            var confirmation = emailService.SendMessage(subject,
                                                        message,
                                                        this.Email);
            return confirmation;
        }

        public override string ToString()
        {
            return $"Vendor: {this.CompanyName} ({this.VendorId})";
        }

        public override bool Equals(object obj)
        {
            if (obj == null || this.GetType() != obj.GetType())
                return false;

            Vendor compareVendor = obj as Vendor;
            if (compareVendor != null &&
                this.VendorId == compareVendor.VendorId &&
                this.CompanyName == compareVendor.CompanyName &&
                this.Email == compareVendor.Email)
                return true;

            return base.Equals(obj);
        }

        public override int GetHashCode() => base.GetHashCode();

        public static List<String> SendEmail(ICollection<Vendor> vendors, string message)
        {
            var confirmations = new List<String>();
            var emailService = new EmailService();

            foreach(var vendor in vendors)
            {
                var subject = $"Important message for: {vendor.CompanyName}";
                var confirmation = emailService.SendMessage(subject, message, vendor.Email);

                confirmations.Add(confirmation);
            }

            return confirmations;
        }
    }
}
