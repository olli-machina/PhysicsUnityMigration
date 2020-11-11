using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetBehavior : MonoBehaviour
{
   // bool inWater = false;
    ForceGenerator2D forceGen;
    BuoyancyForceGenerator buoyancy;


    // Start is called before the first frame update
    void Start()
    {
        //forceGen = gameObject.GetComponent<ForceGenerator2D>();
        //buoyancy = gameObject.GetComponents<BuoyancyForceGenerator>();
        //forceGen = BuoyancyForceGenerator();
        //forceGen = Instantiate(buoyancy);
        ForceManager.addForceGenerator(forceGen);
    }

    // Update is called once per frame
    void Update()
    {
        float height = 50.0f;// gameObject.GetComponent<BuoyancyForceGenerator>().buoyancyWaterHeight;
            //buoyancyWaterHeight;
        if ( height < gameObject.transform.position.y)
        {
            Debug.Log("WHY");
            //make gravity
        }
        else
        {
            ForceManager.updateall();
            Debug.Log("Hate");
           // buoyancy.BuoyancyForceGeneratorupdateForce(gameObject, Time.deltaTime);
        }

       // if(gameObject != pUnit)
       //{
       //   float distSQ = getSquaredDistance(pUnit.transform.position, gameObject.transform.position);
       //   if(distSQ <= 1600)
       //   {
       //       //add to score
       //       //spawn new one (in game manager?)
       //       Destroy(gameObject);
       //   }
       //}
    }
}
