using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;

public class EnemyDeployer : MonoBehaviour
{
    public Transform FollowPlayer;
    public GameObject BulletPrefab;
    public Transform FirePoint;
    
    public float TimerToFollow;
    public GameObject DestroyItSelf;
    
    public float TimerToDestroy;
    public VisualEffect destroySpecialEffect;
    
    public bool weaponIsCoolingDown;
    public float Speed;
    
    
    public void Shooting()
    {
        if (!weaponIsCoolingDown)
        {
            weaponIsCoolingDown = true;
            GameObject bulletclone = Instantiate(BulletPrefab, FirePoint.position, transform.rotation);
            StartCoroutine(WeaponCoolingDown(Random.Range(1, 3)));
            bulletclone.GetComponent<Rigidbody>().velocity = transform.forward * Speed;
        }
    }

    private IEnumerator WeaponCoolingDown(float timer)
    {
        yield return new WaitForSeconds(timer);
        weaponIsCoolingDown = false;
    }
}
