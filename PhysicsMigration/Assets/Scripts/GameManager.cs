using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameObject target, bulletPrefab, waterSprite, springPrefab, rodPrefab;
    private GameObject gun;
    public ForceManager manager;
    public Particle2DLink mLink;
    public bool isTarget = false;
    GunBehaviors gunBehaviors;

    // Start is called before the first frame update
    void Start()
    {
        CreateTarget(new Vector3(-82.0f, 25.0f, 0.0f)); //create target at start
                                       //need to set to random position
        gun = GameObject.Find("Gun");
        //manager = GameObject.Find("ForceManager").GetComponent<ForceManager>();

    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Return))
        {
            if (gun.GetComponent<GunBehaviors>().currentNum == 0) //regular projectile
            {
                GameObject newBullet = Instantiate(bulletPrefab);
                newBullet.GetComponent<BulletBehavior>().SetVariables(newBullet);
                newBullet.transform.position = gun.transform.position;
                //newBullet.GetComponent<BulletBehavior>().isForceGen = false;
            }
            else if(gun.GetComponent<GunBehaviors>().currentNum == 1) //spring projectile
            {
                SpringProjectile();
            }
            else if(gun.GetComponent<GunBehaviors>().currentNum == 2) //rod projectile
            {
                GameObject newParticleLink = new GameObject("ParticleLink");
                Particle2DLink tempParticleLink = newParticleLink.AddComponent<Particle2DLink>();
                mLink = tempParticleLink;
                RodProjectile();
            }
        }

        if(!isTarget)
        {
            CreateTarget(new Vector3(Random.Range(-110, 110), Random.Range(-60, 60), 0.0f));
        }
    }

    void CreateTarget(Vector3 pos)
    {
        GameObject newTarget = Instantiate(target);

        // newTarget.transform.position = new Vector3(Random.Range(-120, 120), Random.Range(-70, 70), 0.0f);
        newTarget.transform.position = pos;
        newTarget.GetComponent<TargetBehavior>().SetVariables(newTarget);
        ForceGenerator2D bouyancyForce = manager.NewBouyancyForceGenerator(newTarget, -(waterSprite.transform.localScale.y) / 2, 75.0f, (waterSprite.transform.localScale.y) / 2, 5.0f);
        manager.addForceGenerator(bouyancyForce);
        newTarget.GetComponent<TargetBehavior>().forceGen = bouyancyForce;
        isTarget = true;
    }

    void SpringProjectile()
    {
        GameObject newBullet1 = Instantiate(springPrefab);
        GameObject newBullet2 = Instantiate(springPrefab);
        newBullet1.GetComponent<BulletBehavior>().SetVariables(newBullet1);
        newBullet2.GetComponent<BulletBehavior>().SetVariables(newBullet2);
        newBullet1.transform.position = gun.transform.position;
        newBullet2.transform.position = gun.transform.position;

        newBullet1.GetComponent<BulletBehavior>().isForceGen = true;
       // newBullet2.GetComponent<BulletBehavior>().isForceGen = false;

        ForceGenerator2D springForce = manager.NewSpringForceGenerator(newBullet1, newBullet2, 1.0f, 10.0f);
        manager.addForceGenerator(springForce);

        newBullet1.GetComponent<BulletBehavior>().forceGen = springForce;
    }
    
    void RodProjectile()
    {
        GameObject newBullet1 = Instantiate(rodPrefab);
        GameObject newBullet2 = Instantiate(rodPrefab);
        newBullet1.GetComponent<BulletBehavior>().SetVariables(newBullet1);
        newBullet2.GetComponent<BulletBehavior>().SetVariables(newBullet2);
        newBullet1.transform.position = gun.transform.position;
        newBullet2.transform.position = gun.transform.position;

        newBullet1.GetComponent<BulletBehavior>().isParticleLink = true;
        //newBullet2.GetComponent<BulletBehavior>().isParticleLink = true;

        Particle2DLink pLink = mLink.NewLink(newBullet1, newBullet2, 10.0f);

        newBullet1.GetComponent<BulletBehavior>().particleLink = pLink;
    }
}
