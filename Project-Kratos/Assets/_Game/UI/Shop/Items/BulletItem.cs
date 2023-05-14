using ProjectKratos.Bullet;
using UnityEngine;

namespace ProjectKratos.Shop
{
    public class BulletItem : ShopItem
    {
        [SerializeField] private BulletScript _bullet;
        [SerializeField] private GameObject _shopMenuRef;
        
        public override void BuyItem()
        {
            _variables.SetNewBullet(_bullet.gameObject);
            
            _shopMenuRef.SetActive(false);
        }
    }
}
