package dojo.supermarket.model;

import org.junit.jupiter.api.Test;

import static org.assertj.core.api.Assertions.assertThat;
import static org.junit.jupiter.api.Assertions.*;

class ProductTest {

    @Test
    void getUnit() {
        Product eachProduct = new Product("theProduct", ProductUnit.Each);
        assertThat(eachProduct.getUnit()).isEqualTo(ProductUnit.Each);

        Product kiloProduct = new Product("theProduct", ProductUnit.Kilo);
        assertThat(kiloProduct.getUnit()).isEqualTo(ProductUnit.Kilo);
    }

    @Test
    void equals() {
        Product product1 = new Product("1", ProductUnit.Each);
        Product product2 = new Product("1", ProductUnit.Each);
        Product product3 = new Product("1", ProductUnit.Kilo);

        assertEquals(product1, product1);
        assertEquals(product1, product2);
        assertEquals(product2, product1);
        assertNotEquals(product1, product3);
        assertNotEquals(product3, product2);
    }
}