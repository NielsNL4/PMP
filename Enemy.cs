using System.Collections;
using System.Collections.Generic;
using UnityEngine.AI;
using UnityEngine;

public class Enemy : MonoBehaviour
{

    Transform player;

    public uint health = 100;
    

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;

        
    }

    // Update is called once per frame
    void Update()
    {
        
        if(GetComponent<NavMeshAgent>().enabled)
            transform.GetComponent<NavMeshAgent>().destination = player.position;

        if(health <= 0)
            Destroy(transform.gameObject);
    }

    public void Damage(uint amount){
        if(health > 0)
            health -= amount;

    }
}
