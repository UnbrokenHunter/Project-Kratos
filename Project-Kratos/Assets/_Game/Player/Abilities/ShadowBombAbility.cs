using UnityEngine;
using UnityEngine.AddressableAssets;

namespace ProjectKratos
{
    public class ShadowBombAbility : PlayerAbility
    {
        protected override void Ability()
        {
            var obj = Addressables.LoadAssetAsync<GameObject>("Assets/_Game/Player/Bullets/Shadow Bomb.prefab").Result;
            if (obj != null)
                _shoot.ShootBullet(obj);

        }
    }
}
