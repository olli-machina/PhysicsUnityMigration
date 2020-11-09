using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetBehavior : MonoBehaviour
{
    bool inWater = false;
    ForceGenerator2D forceGen;
    BuoyancyForceGenerator buoyancy;


    // Start is called before the first frame update
    void Start()
    {
        forceGen = gameObject.GetComponent<ForceGenerator2D>();
    }

    // Update is called once per frame
    void Update()
    {
        //if(forceGen.)

        //if(gameObject.transform.position.y > )
       // if(gameObject.transform.position >= scr)

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
