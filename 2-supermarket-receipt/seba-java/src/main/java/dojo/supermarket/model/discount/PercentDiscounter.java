package dojo.supermarket.model.discount;

import dojo.supermarket.model.Discount;
import dojo.supermarket.model.Offer;
import dojo.supermarket.model.Product;

public class PercentDiscounter implements Discounter {

    @Override
    public Discount applyDiscount(Product product, double quantity, Offer offer, double unitPrice) {
        double priceDiscounted = quantity * unitPrice * offer.getArgument() / 100.0;
        return new Discount(product, offer.getArgument() + "% off", priceDiscounted);
    }
}
