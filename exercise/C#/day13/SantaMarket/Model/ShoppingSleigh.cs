namespace SantaMarket.Model
{
    public class ShoppingSleigh
    {
        private readonly List<ProductQuantity> _items = new();
        private readonly Dictionary<Product, double> _productQuantities = new();

        public IReadOnlyList<ProductQuantity> Items() => _items.AsReadOnly();

        public void AddItem(Product product) => AddItemQuantity(product, 1.0);

        public IReadOnlyDictionary<Product, double> ProductQuantities() => _productQuantities.AsReadOnly();

        public void AddItemQuantity(Product product, double quantity)
        {
            _items.Add(new ProductQuantity(product, quantity));
            if (_productQuantities.ContainsKey(product))
            {
                _productQuantities[product] += quantity;
            }
            else
            {
                _productQuantities[product] = quantity;
            }
        }

        public void HandleOffers(Receipt receipt, Dictionary<Product, Offer> offers, ISantamarketCatalog catalog)
        {
            foreach (var product in ProductQuantities().Keys)
            {
                var quantity = _productQuantities[product];
                if (offers.ContainsKey(product))
                {
                    var offer = offers[product];
                    var unitPrice = catalog.GetUnitPrice(product);
                    var quantityAsInt = (int)quantity;
                    Discount? discount = null;

                    switch (offer.OfferType)
                    {
                        case SpecialOfferType.TwoForAmount:
                            if (quantityAsInt >= 2)
                            {
                                discount = ComputeXForYDiscount(product, quantityAsInt, 2, unitPrice, offer.Argument);
                            }
                            break;

                        case SpecialOfferType.ThreeForTwo:
                            if (quantityAsInt >= 3)
                            {
                                discount = ComputeXForYDiscount(product, quantityAsInt, 3, unitPrice, 2 * unitPrice);
                            }
                            break;

                        case SpecialOfferType.FiveForAmount:
                            if (quantityAsInt >= 5)
                            {
                                discount = ComputeXForYDiscount(product, quantityAsInt, 5, unitPrice, offer.Argument);
                            }
                            break;

                        case SpecialOfferType.TenPercentDiscount:
                            discount = new Discount(product, $"{offer.Argument}% off",
                                -quantity * unitPrice * offer.Argument / 100.0);
                            break;

                        default:
                            // Autres cas ou pas d'offre
                            break;
                    }

                    if (discount != null)
                    {
                        receipt.AddDiscount(discount);
                    }
                }
            }
        }

        private Discount ComputeXForYDiscount(Product product, int quantity, int x, double unitPrice, double yAmount)
        {
            int setsOfX = quantity / x; // Nombre de lots X
            double total = setsOfX * yAmount + (quantity % x) * unitPrice; // Total avec réduction
            double discountAmount = quantity * unitPrice - total; // Montant de la réduction
            return new Discount(product, $"{x} for {yAmount}", -discountAmount);
        }
    }
}