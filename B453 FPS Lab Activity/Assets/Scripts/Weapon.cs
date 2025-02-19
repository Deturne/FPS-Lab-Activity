using System.Collections;
using UnityEngine;

public abstract class Weapon : MonoBehaviour
{
    // The location in space where the projectiles (or raycast) will be spawned.
    [SerializeField] protected Transform firePoint;

    // How much damage this weapon does.
    [SerializeField] protected float damage;

    // The range of this weapon.
    [SerializeField] protected float range;
    [SerializeField] protected float firerate;
    [SerializeField] protected int bulletCount;
    [SerializeField] protected int maxCapacity;
    [SerializeField] protected PlayerController playerController;

    protected void Awake()
    {
        playerController = GameObject.Find("--- Player ---").GetComponent<PlayerController>();
        UIManager.Instance.UpdateAmmoUI(bulletCount, playerController.SpareRounds);
    }

    protected virtual void Shoot()
    {
        UIManager.Instance.UpdateAmmoUI(bulletCount, playerController.SpareRounds);
        if (bulletCount <= 0)
        {
            Reload();
        }
    }

    protected virtual void Reload()
    {
        StartCoroutine(ReloadCoroutine());
    }

    protected IEnumerator ReloadCoroutine()
    {
        if(playerController.SpareRounds >= maxCapacity)
        {
            bulletCount = maxCapacity;
            playerController.SpareRounds -= maxCapacity;
        }

        yield return new WaitForSeconds(1f);

        UIManager.Instance.UpdateAmmoUI(bulletCount, playerController.SpareRounds);
    }
}
