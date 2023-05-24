using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Serialization;

//this code was provided by Dave / GameDevelopment (https://www.youtube.com/watch?v=UjkSFoLxesw)
public class NavMeshAI : MonoBehaviour
{
    //projectile
    public GameObject bullet;
    public float fireRate = 0.15f;
    private float fireTime;

    //launch point for where the bullet will spawn 
    public Transform ProjLaunchPoint;
    public GameObject gun;
    // enemyTurret rotatT;

    //AI related
    public NavMeshAgent agent;
    private Transform player;
    public LayerMask whatIsPlayer, whatIsGround;

    public AudioManager audManager;
    
    //looking towards player
    public float rotSpeed;
    Quaternion rotation;
    

    //patroling
    public Vector3 walkPoint;
    bool WPSet;
    public float walkPointRange;

    //Attacking
    public float timeBetweenAttacks;
    bool attackDone;

    //states
    public float sightRange, attackRange;
    public bool playerISRange, playerIARange;
    
    //audio
    private int audioIndex;
    

    void Awake() 
    {
        player = GameObject.Find("player").transform;
        agent = GetComponent<NavMeshAgent>(); 
        audManager = FindObjectOfType<AudioManager>();
        audioIndex = audManager.SFX.FindIndex(sfx => sfx.unit == gameObject); //finds where its located in the audio manager list
        Debug.Log(audioIndex);
        
        //detecting if audioIndex will spit out an error 
        if (audManager == null || audioIndex == -1 || audioIndex >= audManager.SFX.Count || audManager.SFX[audioIndex].unit != gameObject)
        {
            audIsManaged();
        }
        else
        {
            Debug.Log(audioIndex);
        }
    }

    void Update() 
    {
        //checkes to see if index is non existent within the audio manager
        switch(audioIndex)
        {
            case -1:
                audIsManaged();
                break;
        }
        
        //setting the ranges for the sight range and attack range of the AI 
        playerISRange = Physics.CheckSphere(transform.position, sightRange, whatIsPlayer);
        playerIARange = Physics.CheckSphere(transform.position, attackRange, whatIsPlayer);
        //checks for different enemy states 
        if(!playerISRange && !playerIARange)
        {
            Patroling();
        }

        if(playerISRange && !playerIARange)
        {
            ChaseP();
        }

        if(playerISRange && playerIARange)
        {
            lookAtPlayer();
            
            AttackP();
        }
    }
    
    //adds enemies to audio manager if not in list
    public void audIsManaged()
    {
        // audioIndex = audManager.SFX.FindIndex(sfx => sfx.unit == gameObject); //finds where its located in the audio manager list
        if (audioIndex < 0 || audioIndex >= audManager.SFX.Count || audManager.SFX[audioIndex].unit != gameObject)
        {
            audManager.SFX.Add(new AudioUnits(gameObject, GetComponent<AudioSource>())); //adds the objects to the list
            audioIndex = audManager.SFX.FindIndex(sfx => sfx.unit == gameObject); //finds where its located in the audio manager list
            Debug.Log(audioIndex);
            
            audManager.LoadAudioClipsFromFolder("Ghosts", audioIndex);
            audioIndex = audManager.SFX.Count - 1;
        }
        
    }
    
    
    void Patroling() 
    {
        if(!WPSet)
        {
            searchWalkPoint();
        }

        if(WPSet)
        {
            agent.SetDestination(walkPoint);
        }
        Vector3 distToWalkPoint = transform.position - walkPoint;

        //walkpoint reached
        if(distToWalkPoint.magnitude < 1f)
        {
            WPSet = false; //resetting the walkpoint calculation to generate another coord for the AI to lock onto
        }
    }

    void searchWalkPoint()
    {
        //calculate a random point to go to
        float randomZ = Random.Range(-walkPointRange, walkPointRange);
        float randomX = Random.Range(-walkPointRange, walkPointRange);

        walkPoint = new Vector3(transform.position.x + randomX, transform.position.y, transform.position.z + randomZ); //coords for AI

        if(Physics.Raycast(walkPoint, -transform.up, 2f, whatIsGround))
        {
            WPSet = true;
        }

    }

    void ChaseP() 
    {
        agent.SetDestination(player.position);
        audManager.playSFX(audioIndex);
    }

    void AttackP() 
    {
        //make sure enemy doesnt move when attacking
        agent.SetDestination(transform.position);

        //transform.LookAt(player); //consistently faces the player

        //AI will attack player if it hasnt already
        if(!attackDone)
        {
            //spawns bullet
            gun.GetComponent<EgunBehav>().fire(); //accesses the gun behave data script from enemy gun 
            Debug.Log("attacking p");
            attackDone = true;
            Invoke(nameof(resetAtt), timeBetweenAttacks);
        }
    }
    
    //rotates towards players with the ability to delay (so its not aimbot like) 
    void lookAtPlayer()
    {
        rotation = Quaternion.LookRotation(player.position - transform.position); //calculate of the rotation needed to face the player
        transform.rotation = Quaternion.RotateTowards(transform.rotation, rotation, Time.fixedDeltaTime * rotSpeed); //consistently faces the player with a delay
    }

    void resetAtt()
    {
        attackDone = false;
    }


    private void OnDrawGizmosSelected() 
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRange);
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, sightRange);
    }

       

}
