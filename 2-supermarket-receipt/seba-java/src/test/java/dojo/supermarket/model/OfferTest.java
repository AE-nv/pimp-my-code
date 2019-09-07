package dojo.supermarket.model;

import org.junit.jupiter.api.Test;

import static org.assertj.core.api.Assertions.assertThat;
import static org.junit.jupiter.api.Assertions.*;

class OfferTest {

    @Test
    void getProduct() {
        Product testProduct = new Product("test", ProductUnit.Each);

        Offer testOffer = new Offer(SpecialOfferType.ThreeForTwo, testProduct, 10.2);

        assertThat(testOffer.getProduct()).isEqualToComparingFieldByField(testProduct);
    }
}