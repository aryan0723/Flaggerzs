using UnityEngine;

public class SingleShotGun : Gun
{
    [SerializeField] Camera _camera;
    [SerializeField] Bullet projectile;
    [SerializeField] Transform projectileSpawn;
    [SerializeField] float muzzleVelocity = 50f;
    public override void Use()
    {
        Shoot();
    }

    private void Shoot()
    {
        Ray ray = _camera.ViewportPointToRay(new Vector3(0.5f, 0.5f));
        ray.origin = transform.position;

        Bullet newProjectile = Instantiate(projectile, projectileSpawn.position, projectileSpawn.rotation) as Bullet;
        newProjectile.SetSpeed(muzzleVelocity);
    }
}
