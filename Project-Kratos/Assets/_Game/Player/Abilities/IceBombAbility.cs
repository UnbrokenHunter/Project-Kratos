using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;

namespace ProjectKratos
{
    public class IceBombAbility : PlayerAbility
    {
        
        protected override void Ability()
        {
            var obj = Addressables.LoadAssetAsync<GameObject>("Assets/_Game/Player/Bullets/Ice Bomb.prefab").Result;
            if (obj != null)
                _shoot.ShootBullet(obj);

        }
    }
}
