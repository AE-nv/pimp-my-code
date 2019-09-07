package dojo.supermarket.model;

import org.junit.jupiter.api.Test;

import static org.junit.jupiter.api.Assertions.*;

class ReceiptItemTest {

    @Test
    void equals() {
        Product testProduct = new Product("test", ProductUnit.Kilo);

        ReceiptItem item1 = new ReceiptItem(testProduct, 1, 12.99, 12.99);
        ReceiptItem item2 = new ReceiptItem(testProduct, 1, 12.99, 12.99);
        ReceiptItem item3 = new ReceiptItem(testProduct, 1, 12.98, 12.99);

        assertEquals(item1, item1);
        assertEquals(item1, item2);
        assertEquals(item2, item1);
        assertNotEquals(item1, item3);
        assertNotEquals(item3, item2);
    }
}