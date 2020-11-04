using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    GameObject target;

    // Start is called before the first frame update
    void Start()
    {
        target = Instantiate(target); //make the target at the beginning of the game
                                       //need to set to random position
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
