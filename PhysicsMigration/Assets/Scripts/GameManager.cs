using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public GameObject target, bulletPrefab, waterSprite, springPrefab, rodPrefab, randomPrefab;
    private GameObject gun;
    public ForceManager Fmanager;
    public ParticleManager Pmanager;
    public Particle2DLink mLink;
   public Particle2DContact pContact;
    public bool isTarget = false, isAlive = true;
    GunBehaviors gunBehaviors;
    public Text scoreText;
    int score;

    // Start is called before the first frame update
    void Start()
    {
        CreateTarget(new Vector3(-82.0f, 25.0f, 0.0f)); //create target at start
        gun = GameObject.Find("Gun");
        //CreateParticle(new Vector3(Random.Range(-110, 110), 25f, 0.0f));
        //CreateParticle(new Vector3(Random.Range(-110, 110), 25f, 0.0f));

    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Return))
        {
            if (gun.GetComponent<GunBehaviors>().currentNum == 0) //regular projectile
            {
                GameObject newBullet = Instantiate(bulletPrefab);
                Pmanager.addParticle2D(newBullet);
                newBullet.GetComponent<BulletBehavior>().SetVariables(newBullet, gun);
                newBullet.transform.position = gun.transform.position;
                newBullet.transform.rotation = gun.transform.rotation;
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
            score++;
            scoreText.text = score.ToString();
        }

        if(Random.Range(0, 100) < 5)
        {
            CreateParticle(new Vector3(Random.Range(-110, 110), Random.Range(-60, 60), 0.0f));
        }
    }

    void CreateTarget(Vector3 pos)
    {
        GameObject newTarget = Instantiate(target);
        newTarget.transform.position = pos;
        Debug.Log(Pmanager);
        Pmanager.addParticle2D(newTarget);
        newTarget.GetComponent<TargetBehavior>().SetVariables(newTarget);
        ForceGenerator2D bouyancyForce = Fmanager.NewBouyancyForceGenerator(newTarget, -(waterSprite.transform.localScale.y) / 2, 75.0f, (waterSprite.transform.localScale.y) / 2, 5.0f);
        Fmanager.addForceGenerator(bouyancyForce);
        newTarget.GetComponent<TargetBehavior>().forceGen = bouyancyForce;
        isTarget = true;
    }
    
    void CreateParticle(Vector3 pos)
    {
        GameObject newParticle = Instantiate(randomPrefab);
        newParticle.transform.position = pos;
        Debug.Log(Pmanager);
        Pmanager.addParticle2D(newParticle);
        newParticle.GetComponent<TargetBehavior>().SetVariables(newParticle);
        ForceGenerator2D bouyancyForce = Fmanager.NewBouyancyForceGenerator(newParticle, -(waterSprite.transform.localScale.y) / 2, 20.0f, (waterSprite.transform.localScale.y) / 2, 5.0f);
        Fmanager.addForceGenerator(bouyancyForce);
        newParticle.GetComponent<TargetBehavior>().forceGen = bouyancyForce;
        //newParticle.GetComponent<Renderer>().material.color = new Color(Random.Range(0, 255), Random.Range(0, 255), Random.Range(0, 255));
        newParticle.GetComponent<Renderer>().material.color = new Color(Random.Range(0, 100)/100f, Random.Range(0, 100) / 100f, Random.Range(0, 100) / 100f);
        Debug.Log(newParticle.GetComponent<Renderer>().material.color);
        //isTarget = true;
    }

    void SpringProjectile()
    {
        GameObject newBullet1 = Instantiate(springPrefab);
        GameObject newBullet2 = Instantiate(springPrefab);
        newBullet1.GetComponent<BulletBehavior>().SetVariables(newBullet1, gun);
        newBullet2.GetComponent<BulletBehavior>().SetVariables(newBullet2, gun);
        newBullet1.transform.position = gun.transform.position;
        newBullet2.transform.position = gun.transform.position;

        newBullet1.GetComponent<BulletBehavior>().isForceGen = true;

        ForceGenerator2D springForce = Fmanager.NewSpringForceGenerator(newBullet1, newBullet2, 1.0f, 10.0f);
        Fmanager.addForceGenerator(springForce);

        newBullet1.GetComponent<BulletBehavior>().forceGen = springForce;
    }
    
    void RodProjectile()
    {
        GameObject newBullet1 = Instantiate(rodPrefab);
        GameObject newBullet2 = Instantiate(rodPrefab);
        newBullet1.GetComponent<BulletBehavior>().SetVariables(newBullet1, gun);
        newBullet2.GetComponent<BulletBehavior>().SetVariables(newBullet2, gun);
        newBullet1.transform.position = new Vector3(gun.transform.position.x, gun.transform.position.y + 10.0f, 0.0f);
        newBullet2.transform.position = gun.transform.position;

        newBullet1.GetComponent<BulletBehavior>().isParticleLink = true;

        Particle2DLink pLink = mLink.NewLink(newBullet1, newBullet2, 10.0f);
        //pContact.resolveContacts(mLink., Time.deltaTime);
        newBullet1.GetComponent<BulletBehavior>().particleLink = pLink;
    }
}
