package dojo.supermarket.model;

import java.util.ArrayList;
import java.util.HashMap;
import java.util.List;
import java.util.Map;

import dojo.supermarket.model.discount.FiveForAmountDiscounter;
import dojo.supermarket.model.discount.PercentDiscounter;
import dojo.supermarket.model.discount.ThreeForTwoDiscounter;
import dojo.supermarket.model.discount.TwoForAmountDiscounter;

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

    private Discount getDiscountForProduct(Product product, Map<Product, Offer> offers, SupermarketCatalog catalog) {
        if (offers.containsKey(product)) {
            Offer offer = offers.get(product);
            double quantity = productQuantities.get(product);
            double unitPrice = catalog.getUnitPrice(product);

            return offer.getOfferType().getDiscounter().applyDiscount(product, quantity, offer, unitPrice);
        }
        return null;
    }

}
