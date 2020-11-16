using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetBehavior : MonoBehaviour
{
    public ForceGenerator2D forceGen;
    GameManager gameManager;

    // Start is called before the first frame update
    void Start()
    {
        forceGen = gameObject.GetComponent<ForceGenerator2D>();
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
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
