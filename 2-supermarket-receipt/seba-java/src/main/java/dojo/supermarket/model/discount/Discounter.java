package dojo.supermarket.model.discount;

import dojo.supermarket.model.Discount;
import dojo.supermarket.model.Offer;
import dojo.supermarket.model.Product;

public interface Discounter {
    Discount applyDiscount(Product product,double quantity, Offer offer, double unitPrice);
}
