package dojo.supermarket.model;

import java.util.ArrayList;
import java.util.HashMap;
import java.util.List;
import java.util.Map;

public class ShoppingCart {

    private final List<ProductQuantity> items = new ArrayList<>();
    Map<Product, Double> productQuantities = new HashMap<>();

    List<ProductQuantity> getItems() {
        return new ArrayList<>(items);
    }

    void addItem(Product product) {
        this.addItemQuantity(product, 1.0);
    }

    Map<Product, Double> productQuantities() {
        return productQuantities;
    }

    public void addItemQuantity(Product product, double quantity) {
        items.add(new ProductQuantity(product, quantity));
        if (productQuantities.containsKey(product)) {
            productQuantities.put(product, productQuantities.get(product) + quantity);
        } else {
            productQuantities.put(product, quantity);
        }
    }

    void handleOffers(Receipt receipt, Map<Product, Offer> offers, SupermarketCatalog catalog) {
        for (Product product : productQuantities().keySet()) {
            Discount discount = getDiscountForProduct(product, offers, catalog);

            if (discount != null) {
                receipt.addDiscount(discount);
            }
        }

    }


    private Discount getDiscountForProduct(Product p, Map<Product, Offer> offers, SupermarketCatalog catalog) {
        if (offers.containsKey(p)) {
            Offer offer = offers.get(p);
            double quantity = productQuantities.get(p);
            double unitPrice = catalog.getUnitPrice(p);

            int quantityAsInt = (int) quantity;

            switch (offer.offerType) {
                case ThreeForTwo:
                    return getThreeForTwoDiscount(p, quantity, unitPrice, quantityAsInt);
                case PercentDiscount:
                    return getPercentDiscount(p, quantity, offer, unitPrice);
                case TwoForAmount:
                    return getTwoForAmountDiscount(p, quantity, offer, unitPrice, quantityAsInt);
                case FiveForAmount:
                    return getFiveForAmountDiscount(p, quantity, offer, unitPrice, quantityAsInt);
                default:
                    return null;
            }
        }
        return null;
    }

    private Discount getPercentDiscount(Product p, double quantity, Offer offer, double unitPrice) {
        Discount discount;
        discount = new Discount(p, offer.argument + "% off", quantity * unitPrice * offer.argument / 100.0);
        return discount;
    }

    private Discount getThreeForTwoDiscount(Product p, double quantity, double unitPrice, int quantityAsInt) {
        if (quantityAsInt > 2) {
            double discountAmount =
                quantity * unitPrice - (((quantityAsInt / 2) * 2 * unitPrice) + quantityAsInt % 3 * unitPrice);
            return new Discount(p, "3 for 2", discountAmount);
        }
        return null;
    }

    private Discount getFiveForAmountDiscount(Product p, double quantity, Offer offer, double unitPrice, int quantityAsInt) {
        if (quantityAsInt >= 5) {
            double discountTotal = unitPrice * quantity - (offer.argument * (quantityAsInt / 5) + quantityAsInt % 5 * unitPrice);
            return new Discount(p, 5 + " for " + offer.argument, discountTotal);
        }
        return null;
    }

    private Discount getTwoForAmountDiscount(Product p, double quantity, Offer offer, double unitPrice, int quantityAsInt) {
        if (quantityAsInt >= 2) {
            double total = offer.argument * (quantityAsInt / 2) + quantityAsInt % 2 * unitPrice;
            double discountN = unitPrice * quantity - total;
            return new Discount(p, "2 for " + offer.argument, discountN);
        }
        return null;
    }
}
