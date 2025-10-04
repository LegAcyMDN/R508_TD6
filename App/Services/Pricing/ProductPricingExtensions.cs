using App.Models;

namespace App.Services.Pricing
{
    public static class ProductPricingExtensions
    {
        private const decimal TVA_RATE = 0.20m; // 20% TVA
        private const decimal INSTALLMENT_COMMISSION = 0.01m; // 1%
        private const decimal MAX_DISCOUNT = 0.80m; // 80%

        /// <summary>
        /// Calcule le prix TTC
        /// </summary>
        public static decimal GetPriceTTC(this Product product)
        {
            return product.ProductPrice * (1 + TVA_RATE);
        }

        /// <summary>
        /// Calcule le prix HT
        /// </summary>
        public static decimal GetPriceHT(this Product product)
        {
            return product.ProductPrice;
        }

        /// <summary>
        /// Applique une réduction avec validation
        /// </summary>
        public static decimal ApplyDiscount(this Product product, decimal discountPercent, bool isTTC = false)
        {
            if (discountPercent < 0 || discountPercent > MAX_DISCOUNT)
            {
                throw new ArgumentException($"Discount must be between 0 and {MAX_DISCOUNT * 100}%");
            }

            var basePrice = isTTC ? product.GetPriceTTC() : product.GetPriceHT();
            return basePrice * (1 - discountPercent);
        }

        /// <summary>
        /// Calcule le prix avec paiement en plusieurs fois
        /// </summary>
        public static decimal CalculateInstallmentPrice(
            this Product product,
            int numberOfInstallments,
            bool isTTC = false,
            decimal? discountPercent = null)
        {
            if (numberOfInstallments < 1)
            {
                throw new ArgumentException("Number of installments must be at least 1");
            }

            var basePrice = isTTC ? product.GetPriceTTC() : product.GetPriceHT();

            // Application de la réduction si présente
            if (discountPercent.HasValue)
            {
                basePrice = product.ApplyDiscount(discountPercent.Value, isTTC);
            }

            // Si paiement en une fois, pas de commission
            if (numberOfInstallments == 1)
            {
                return basePrice;
            }

            // Commission par échéance
            var commissionPerInstallment = basePrice * INSTALLMENT_COMMISSION;
            var totalCommission = commissionPerInstallment * numberOfInstallments;

            return basePrice + totalCommission;
        }

        /// <summary>
        /// Calcule le montant de chaque échéance
        /// </summary>
        public static decimal CalculateInstallmentAmount(
            this Product product,
            int numberOfInstallments,
            bool isTTC = false,
            decimal? discountPercent = null)
        {
            var totalPrice = product.CalculateInstallmentPrice(numberOfInstallments, isTTC, discountPercent);
            return totalPrice / numberOfInstallments;
        }
    }
}
