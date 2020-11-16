using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunBehaviors : MonoBehaviour
{

    public GameObject prefab;
    public GameObject currentProjectile;
    public float rotateSpeed = 500.0f;
    public List<GameObject> weapons;
    public int currentNum = 0, numTypes;

    // Start is called before the first frame update
    void Start()
    {
        numTypes = weapons.Count;
        currentProjectile = prefab;
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Alpha1) || Input.GetKey(KeyCode.Alpha1))
        {
            transform.Rotate(Vector3.forward * rotateSpeed * Time.deltaTime);
        }

        else if (Input.GetKeyDown(KeyCode.Alpha2) || Input.GetKey(KeyCode.Alpha2))
        {
            transform.Rotate(Vector3.back * rotateSpeed * Time.deltaTime);
        }


        else if(Input.GetKeyDown(KeyCode.W))
        {
            currentNum++;
            if (currentNum >= numTypes)
                currentNum = 0;
            currentProjectile = weapons[currentNum];
        }
   }
}
