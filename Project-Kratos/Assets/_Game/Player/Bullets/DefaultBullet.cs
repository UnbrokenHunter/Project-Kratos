using ProjectKratos.Bullet;
using ProjectKratos.Player;

namespace ProjectKratos
{
    public class DefaultBullet : BulletScript
    {
        protected override void ContactPlayer(PlayerHitInteractions player)
        {
            player.PlayerHit(this);
            Destroy(this.gameObject);
        }

    }
}
