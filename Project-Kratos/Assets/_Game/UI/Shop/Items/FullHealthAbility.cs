namespace ProjectKratos.Shop
{
    public class FullHealthAbility : ShopItem
    {
        public override void BuyItem()
        {
            _variables.PlayerInteractions.AddHealth(_variables.MaxHealth);
        }
    }
}
