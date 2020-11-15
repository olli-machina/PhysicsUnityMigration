using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetBehavior : MonoBehaviour
{
   // bool inWater = false;
    public ForceGenerator2D forceGen;
    GameManager gameManager;
    //BuoyancyForceGenerator buoyancy;


    // Start is called before the first frame update
    void Start()
    {
        forceGen = gameObject.GetComponent<ForceGenerator2D>();
        Debug.Log("Force: " + forceGen);
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        //buoyancy = gameObject.GetComponents<BuoyancyForceGenerator>();
        //forceGen = BuoyancyForceGenerator();
        //forceGen = Instantiate(buoyancy);
        //ForceManager.addForceGenerator(forceGen);
    }

    // Update is called once per frame
    void Update()
    {
        if (gameObject.transform.position.y > 70.0 || gameObject.transform.position.y < -70.0
            || gameObject.transform.position.x < -120.0 || gameObject.transform.position.x > 120.0) //change to screen height
        {
            gameManager.isTarget = false;
            Destroy(gameObject);
        }
    }

    public void SetVariables(GameObject target)
    {
        Particle2D info = target.GetComponent<Particle2D>();
        info.speed = 600.0f;
        info.Acceleration = new Vector3(0.0f, -20.0f, 0.0f);
        info.Velocity = target.transform.forward * info.speed;
        info.Velocity.z = 0.0f;
        info.DampingConstant = 0.99f;
    }
}
