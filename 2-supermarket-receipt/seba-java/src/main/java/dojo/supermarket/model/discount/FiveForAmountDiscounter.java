package dojo.supermarket.model.discount;

import dojo.supermarket.model.Discount;
import dojo.supermarket.model.Offer;
import dojo.supermarket.model.Product;

public class FiveForAmountDiscounter implements Discounter{
    @Override
    public Discount applyDiscount(Product product, double quantity, Offer offer, double unitPrice) {
        if (quantity >= 5) {
            double amountOfTimesDiscountIsApplied = Math.floor(quantity/5);
            double priceDiscounted =
                amountOfTimesDiscountIsApplied * 5 * unitPrice - amountOfTimesDiscountIsApplied * offer.getArgument();
            return new Discount(product, 5 + " for " + offer.getArgument(), priceDiscounted);
        }
        return null;
    }
}
