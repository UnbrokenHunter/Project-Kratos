using System.Collections;
using System.Collections.Generic;
using ProjectKratos.Bullet;
using UnityEngine;
using UnityEngine.AddressableAssets;

namespace ProjectKratos
{
    public class ExplosiveAbility : PlayerAbility
    {
        protected override void Ability()
        {
            var obj = Addressables.LoadAssetAsync<GameObject>("Assets/_Game/Player/Bullets/Bomb.prefab").Result;
            if (obj != null)
                _shoot.ShootBullet(obj);
        }
    }
}
