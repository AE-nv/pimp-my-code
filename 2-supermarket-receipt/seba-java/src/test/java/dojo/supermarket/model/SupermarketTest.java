package dojo.supermarket.model;

import java.util.Collections;
import java.util.List;

import org.junit.jupiter.api.Test;

import static org.assertj.core.api.Assertions.assertThat;
import static org.junit.jupiter.api.Assertions.assertEquals;

public class SupermarketTest {

    private static final String THREE_FOR_TWO = "3 for 2";
    private static final String TWO_FOR = "2 for ";
    private static final String FIVE_FOR = "5 for ";
    private static final String PERCENT_DISCOUNT = "% off";

    // Todo: test all kinds of discounts are applied properly

    @Test
    public void noSpecialOffers() {
        SupermarketCatalog catalog = new FakeCatalog();
        Product apples = new Product("apples", ProductUnit.Kilo);
        catalog.addProduct(apples, 1.99);

        ShoppingCart cart = new ShoppingCart();
        cart.addItemQuantity(apples, 2.5);

        Teller teller = new Teller(catalog);

        // ACT
        Receipt receipt = teller.checksOutArticlesFrom(cart);

        // ASSERT
        assertEquals(4.975, receipt.getTotalPrice(), 0.01);
        assertEquals(Collections.emptyList(), receipt.getDiscounts());
        assertEquals(1, receipt.getItems().size());

        ReceiptItem receiptItem = receipt.getItems().get(0);
        assertEquals(apples, receiptItem.getProduct());
        assertEquals(1.99, receiptItem.getPrice());
        assertEquals(2.5 * 1.99, receiptItem.getTotalPrice());
        assertEquals(2.5, receiptItem.getQuantity());

    }

    @Test
    public void threeForTwo() {
        SupermarketCatalog catalog = new FakeCatalog();
        Product toothbrush = new Product("toothbrush", ProductUnit.Each);
        catalog.addProduct(toothbrush, 0.99);

        ShoppingCart cart = new ShoppingCart();
        cart.addItemQuantity(toothbrush, 3);

        Teller teller = new Teller(catalog);
        teller.addSpecialOffer(SpecialOfferType.ThreeForTwo, toothbrush, 2345);

        Receipt receipt = teller.checksOutArticlesFrom(cart);

        assertEquals(0.99 * 2, receipt.getTotalPrice(), 0.01);
        assertThat(receipt.getDiscounts()).hasSize(1);
        assertThat(receipt.getDiscounts().get(0)).isEqualToComparingFieldByField(new Discount(toothbrush, THREE_FOR_TWO, 0.99));
    }

    @Test
    public void addingProductsSeparately() {
        SupermarketCatalog catalog = new FakeCatalog();
        Product toothbrush = new Product("toothbrush", ProductUnit.Each);
        catalog.addProduct(toothbrush, 0.99);

        ShoppingCart cart = new ShoppingCart();
        cart.addItemQuantity(toothbrush, 1);
        cart.addItemQuantity(toothbrush, 1);
        cart.addItemQuantity(toothbrush, 1);

        Teller teller = new Teller(catalog);
        teller.addSpecialOffer(SpecialOfferType.ThreeForTwo, toothbrush, 2345);

        Receipt receipt = teller.checksOutArticlesFrom(cart);

        assertEquals(0.99 * 2, receipt.getTotalPrice(), 0.01);
        assertThat(receipt.getDiscounts()).hasSize(1);
        assertThat(receipt.getDiscounts().get(0)).isEqualToComparingFieldByField(new Discount(toothbrush, THREE_FOR_TWO, 0.99));
    }

    @Test
    public void twoForFixedPrice() {
        SupermarketCatalog catalog = new FakeCatalog();
        Product tomatoBox = new Product("Box of cherry tomatoes", ProductUnit.Each);
        catalog.addProduct(tomatoBox, 0.69);

        ShoppingCart cart = new ShoppingCart();
        cart.addItem(tomatoBox);
        cart.addItem(tomatoBox);

        Teller teller = new Teller(catalog);
        teller.addSpecialOffer(SpecialOfferType.TwoForAmount, tomatoBox, 0.99);

        Receipt receipt = teller.checksOutArticlesFrom(cart);

        assertEquals(0.99, receipt.getTotalPrice(), 0.01);
        assertThat(receipt.getDiscounts()).hasSize(1);
        assertThat(receipt.getDiscounts().get(0))
            .isEqualToComparingFieldByField(new Discount(tomatoBox, TWO_FOR + 0.99, 0.3899999999999999));

    }

    @Test
    public void fiveForFixedPrice() {
        SupermarketCatalog catalog = new FakeCatalog();
        Product tubeOfToothPaste = new Product("Tube of toothpaste", ProductUnit.Each);
        catalog.addProduct(tubeOfToothPaste, 1.79);

        ShoppingCart cart = new ShoppingCart();
        cart.addItemQuantity(tubeOfToothPaste, 5);

        Teller teller = new Teller(catalog);
        teller.addSpecialOffer(SpecialOfferType.FiveForAmount, tubeOfToothPaste, 7.49);

        Receipt receipt = teller.checksOutArticlesFrom(cart);

        assertEquals(7.49, receipt.getTotalPrice(), 0.01);
        assertThat(receipt.getDiscounts()).hasSize(1);
        assertThat(receipt.getDiscounts().get(0))
            .isEqualToComparingFieldByField(new Discount(tubeOfToothPaste, FIVE_FOR + 7.49, 1.459999999999999));

    }

    @Test
    public void tenPercentDiscount() {
        SupermarketCatalog catalog = new FakeCatalog();
        Product rice = new Product("Bag of rice", ProductUnit.Each);
        catalog.addProduct(rice, 2.49);

        ShoppingCart cart = new ShoppingCart();
        cart.addItem(rice);

        Teller teller = new Teller(catalog);
        teller.addSpecialOffer(SpecialOfferType.TenPercentDiscount, rice, 10.0);

        Receipt receipt = teller.checksOutArticlesFrom(cart);

        assertEquals(2.241, receipt.getTotalPrice(), 0.01);
        assertThat(receipt.getDiscounts()).hasSize(1);
        assertThat(receipt.getDiscounts().get(0))
            .isEqualToComparingFieldByField(new Discount(rice, 10.0 + PERCENT_DISCOUNT, 0.24900000000000003));

    }

    @Test
    public void twentyPercentDiscount() {
        SupermarketCatalog catalog = new FakeCatalog();
        Product apples = new Product("Kilo of apples", ProductUnit.Kilo);
        catalog.addProduct(apples, 1.99);

        ShoppingCart cart = new ShoppingCart();
        cart.addItemQuantity(apples, 2.5);

        Teller teller = new Teller(catalog);
        teller.addSpecialOffer(SpecialOfferType.TenPercentDiscount, apples, 20.0);

        Receipt receipt = teller.checksOutArticlesFrom(cart);

        assertEquals(3.9799999999999995, receipt.getTotalPrice(), 0.01);
        assertThat(receipt.getDiscounts()).hasSize(1);
        assertThat(receipt.getDiscounts().get(0))
            .isEqualToComparingFieldByField(new Discount(apples, 20.0 + PERCENT_DISCOUNT, 0.995));

    }

}
