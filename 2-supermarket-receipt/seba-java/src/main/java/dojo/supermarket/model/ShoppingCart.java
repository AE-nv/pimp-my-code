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
                    return getThreeForTwoDiscount(p, quantity, unitPrice);
                case PercentDiscount:
                    return getPercentDiscount(p, quantity, offer, unitPrice);
                case TwoForAmount:
                    return getTwoForAmountDiscount(p, quantity, offer, unitPrice);
                case FiveForAmount:
                    return getFiveForAmountDiscount(p, quantity, offer, unitPrice);
                default:
                    return null;
            }
        }
        return null;
    }

    private Discount getThreeForTwoDiscount(Product p, double quantity, double unitPrice) {
        if (quantity >= 3.0) {
            double amountOfTimesDiscountIsApplied = Math.floor(quantity/3);
            double priceDiscounted = amountOfTimesDiscountIsApplied * unitPrice;
            return new Discount(p, "3 for 2", priceDiscounted);
        }
        return null;
    }

    private Discount getPercentDiscount(Product p, double quantity, Offer offer, double unitPrice) {
        double priceDiscounted = quantity * unitPrice * offer.argument / 100.0;
        return new Discount(p, offer.argument + "% off", priceDiscounted);
    }

    private Discount getTwoForAmountDiscount(Product p, double quantity, Offer offer, double unitPrice) {
        return getXForAmountDiscount(p, quantity, offer, unitPrice, 2);
    }

    private Discount getFiveForAmountDiscount(Product p, double quantity, Offer offer, double unitPrice) {
        return getXForAmountDiscount(p, quantity, offer, unitPrice, 5);
    }

    private Discount getXForAmountDiscount(Product p, double quantity, Offer offer, double unitPrice, int amount) {
        if (quantity >= amount) {
            double amountOfTimesDiscountIsApplied = Math.floor(quantity/amount);
            double priceDiscounted =
                amountOfTimesDiscountIsApplied * amount * unitPrice - amountOfTimesDiscountIsApplied * offer.argument;
            return new Discount(p, amount + " for " + offer.argument, priceDiscounted);
        }
        return null;
    }
}
