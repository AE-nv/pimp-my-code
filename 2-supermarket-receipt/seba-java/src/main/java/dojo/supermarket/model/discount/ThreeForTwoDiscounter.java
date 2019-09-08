package dojo.supermarket.model.discount;

import dojo.supermarket.model.Discount;
import dojo.supermarket.model.Offer;
import dojo.supermarket.model.Product;

public class ThreeForTwoDiscounter implements Discounter {
    @Override
    public Discount applyDiscount(Product product, double quantity, Offer offer, double unitPrice) {
        if (quantity >= 3.0) {
            double amountOfTimesDiscountIsApplied = Math.floor(quantity/3);
            double priceDiscounted = amountOfTimesDiscountIsApplied * unitPrice;
            return new Discount(product, "3 for 2", priceDiscounted);
        }
        return null;
    }
}
