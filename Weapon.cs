using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{

    public ParticleSystem muzzleFlash;

    public float range = 100;
    public uint damagePerHit = 100;

    public Transform emitter;
    // Start is called before the first frame update

    Spawn spawn;
    void Start()
    {
        spawn = GameObject.Find("Spawn").GetComponent<Spawn>();
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetButtonDown("Fire1")){
            Shoot();
        }
    }

    void Shoot(){
        muzzleFlash.Play();

        RaycastHit hit;
        if (Physics.Raycast(emitter.transform.position, emitter.transform.forward, out hit, range)){
            Debug.Log(hit.transform.name);

            Enemy enemy = hit.transform.GetComponent<Enemy>();
            if(hit.transform.name == "Enemy")
            {
                // enemy.Damage(damagePerHit);
                Destroy(hit.transform.gameObject);
                spawn.enemiesAlive -= 1;
            }
        }
    }
}
