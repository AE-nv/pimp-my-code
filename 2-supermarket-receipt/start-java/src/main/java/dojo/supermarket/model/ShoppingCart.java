package dojo.supermarket.model;

import java.util.*;

public class ShoppingCart {

	private final List<ProductQuantity> items = new ArrayList<>();
	private Map<Product, Double> productQuantities = new HashMap<>();


	List<ProductQuantity> getItems() {
		return new ArrayList<>(items);
	}

	void addItem(Product product) {
		this.addItemQuantity(product, 1.0);
	}

	private Map<Product, Double> productQuantities() {
		return productQuantities;
	}


	void addItemQuantity(Product product, double quantity) {
		items.add(new ProductQuantity(product, quantity));
		if (productQuantities.containsKey(product)) {
			productQuantities.put(product, productQuantities.get(product) + quantity);
		} else {
			productQuantities.put(product, quantity);
		}
	}

	void handleOffers(Receipt receipt, Map<Product, Offer> offers, SupermarketCatalog catalog) {
		for (Product p : productQuantities().keySet()) {
			double quantity = productQuantities.get(p);
			if (offers.containsKey(p)) {
				Offer offer = offers.get(p);
				Optional<Discount> discount = calculateDiscount(catalog, p, quantity, offer);
				discount.ifPresent(receipt::addDiscount);
			}
		}
	}

	private Optional<Discount> calculateDiscount(SupermarketCatalog catalog, Product p, double quantity, Offer offer) {
		double unitPrice = catalog.getUnitPrice(p);
		int quantityAsInt = (int) quantity;
		Discount discount = null;
		int x = 1;
		if (offer.offerType == SpecialOfferType.ThreeForTwo) {
			x = 3;

		} else if (offer.offerType == SpecialOfferType.TwoForAmount) {
			x = 2;
			if (quantityAsInt >= 2) {
				double totalWithDiscountApplied = offer.argument * (quantityAsInt / x) + quantityAsInt % 2 * unitPrice;
				double discountAmount = unitPrice * quantity - totalWithDiscountApplied;
				discount = new Discount(p, "2 for " + offer.argument, discountAmount);
			}

		}
		int numberOfXs = quantityAsInt / x;
		if (offer.offerType == SpecialOfferType.ThreeForTwo && quantityAsInt > 2) {
			double discountAmount =
					quantity * unitPrice - ((numberOfXs * 2 * unitPrice) + quantityAsInt % 3 * unitPrice);
			discount = new Discount(p, "3 for 2", discountAmount);
		}

		if (offer.offerType == SpecialOfferType.TenPercentDiscount) {
			discount = new Discount(p, offer.argument + "% off", quantity * unitPrice * offer.argument / 100.0);
		}

		if (offer.offerType == SpecialOfferType.FiveForAmount) {
			return calculateFiveForAmountDiscount(p, quantity, offer, unitPrice, quantityAsInt);
		}

		return Optional.ofNullable(discount);
	}

	private Optional<Discount> calculateFiveForAmountDiscount(Product p, double quantity, Offer offer,
	                                                          double unitPrice,
	                                                          int quantityAsInt) {
		if (quantityAsInt >= 5) {
			double discountTotal =
					unitPrice * quantity - (offer.argument * (quantityAsInt / 5) + quantityAsInt % 5 * unitPrice);
			return Optional.of(new Discount(p, "5 for " + offer.argument, discountTotal));
		}
		return Optional.empty();
	}
}
