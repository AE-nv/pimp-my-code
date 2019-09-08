package dojo.supermarket.model;

public class Offer {
    private SpecialOfferType offerType;
    private final Product product;
    private double argument;

    public Offer(SpecialOfferType offerType, Product product, double argument) {
        this.offerType = offerType;
        this.argument = argument;
        this.product = product;
    }

    public Product getProduct() {
        return this.product;
    }

    public double getArgument() {
        return this.argument;
    }

    public Discount apply(double quantity, double unitPrice) {
        return this.offerType.getDiscount(product, quantity, this,  unitPrice);
    }

}
