using System.Collections;
using System.Collections.Generic;
using ProjectKratos.Bullet;
using UnityEngine;
using UnityEngine.AddressableAssets;

namespace ProjectKratos
{
    public class ExplosiveAbility : PlayerAbility
    {
        
        public override void TriggerAbility()
        {
            var obj = Addressables.LoadAssetAsync<GameObject>("Assets/_Game/Player/Bullets/Bomb.prefab").Result;
            _shoot.ShootBullet(obj);
        }
    }
}
