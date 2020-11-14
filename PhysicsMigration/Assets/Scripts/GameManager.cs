using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameObject target, bulletPrefab;
    Transform gun;

    // Start is called before the first frame update
    void Start()
    {
        CreateTarget(new Vector3(-82.0f, 25.0f, 0.0f)); //create target at start
                                       //need to set to random position
        gun = GameObject.Find("Gun").GetComponent<Transform>();

    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown("space"))
        {
            GameObject newBullet = Instantiate(bulletPrefab);
            newBullet.GetComponent<BulletBehavior>().SetVariables(newBullet);
            newBullet.transform.position = gun.position;
        }

        if(!target)
        {
            CreateTarget(new Vector3(Random.Range(-120, 120), Random.Range(-70, 70), 0.0f));
        }
    }

    void CreateTarget(Vector3 pos)
    {
        GameObject newTarget = Instantiate(target);
       // newTarget.transform.position = new Vector3(Random.Range(-120, 120), Random.Range(-70, 70), 0.0f);
        newTarget.transform.position = pos;
        newTarget.GetComponent<TargetBehavior>().SetVariables(newTarget);
    }
}
