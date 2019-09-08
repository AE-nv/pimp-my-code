package dojo.supermarket.model;

import dojo.supermarket.model.discount.Discounter;
import dojo.supermarket.model.discount.FiveForAmountDiscounter;
import dojo.supermarket.model.discount.PercentDiscounter;
import dojo.supermarket.model.discount.ThreeForTwoDiscounter;
import dojo.supermarket.model.discount.TwoForAmountDiscounter;

public enum SpecialOfferType {
    ThreeForTwo(new ThreeForTwoDiscounter()),
    PercentDiscount(new PercentDiscounter()),
    TwoForAmount(new TwoForAmountDiscounter()),
    FiveForAmount(new FiveForAmountDiscounter());

    private Discounter offerTypeDiscounter;

    SpecialOfferType(Discounter discounter) {
        this.offerTypeDiscounter = discounter;
    }

    public Discounter getDiscounter() {
        return this.offerTypeDiscounter;
    }
}
