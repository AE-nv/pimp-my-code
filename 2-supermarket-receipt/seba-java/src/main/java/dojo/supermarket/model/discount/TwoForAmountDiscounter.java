package dojo.supermarket.model.discount;

import dojo.supermarket.model.Discount;
import dojo.supermarket.model.Offer;
import dojo.supermarket.model.Product;

public class TwoForAmountDiscounter implements Discounter {
    @Override
    public Discount applyDiscount(Product product, double quantity, Offer offer, double unitPrice) {
        if (quantity >= 2) {
            double amountOfTimesDiscountIsApplied = Math.floor(quantity/2);
            double priceDiscounted =
                amountOfTimesDiscountIsApplied * 2 * unitPrice - amountOfTimesDiscountIsApplied * offer.getArgument();
            return new Discount(product, 2 + " for " + offer.getArgument(), priceDiscounted);
        }
        return null;
    }
}
