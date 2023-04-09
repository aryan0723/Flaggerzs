using UnityEngine;

public class Gun : MonoBehaviour, Item
{
    public GameObject bullet;
    public float bulletSpeed = 10f;
    public float fireRate = 0.5f;
    private float nextFire = 0f;
    public Transform firePoint;
    public int ammo = 10;


    public void Use()
    {
        Fire();
    }
    private void Fire()
    {

    }
}

