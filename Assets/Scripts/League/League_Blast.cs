using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class League_Blast : MonoBehaviour
{
    //Variables
    internal int direction = 1;
    public bool playerBlast = true;
    public float speed = 9.0f;
    int damage = 40;

    public float lifetime = 1.0f;
    float lifeTimer = 0f;

    public GameObject markPrefab;

    //Components
    internal GameObject creator;
    Rigidbody2D rbody;
    [SerializeField] AudioHandler sfxHit;

    //Start
    void Start()
    {
        rbody = GetComponent<Rigidbody2D>();
    }

    //Update
    void Update()
    {
        rbody.velocity = new Vector2(speed * direction, 0);
        Timer();
    }

    //Timer
    void Timer()
    {
        lifeTimer += Time.deltaTime;

        if (lifeTimer >= lifetime)
        {
            Destroy(gameObject);
        }
    }

    //Hit
    private void OnTriggerEnter2D(Collider2D collision)
    {
        //Hit Enemy
        if (playerBlast)
        {
            if (collision.CompareTag("Enemy"))
            {
                GameObject inst = Instantiate(markPrefab,
                    new Vector3(collision.transform.position.x, collision.transform.position.y, collision.transform.position.z), transform.rotation);
                inst.GetComponent<League_Mark>().playerMark = true;
                inst.GetComponent<League_Mark>().creator = creator;
                inst.GetComponent<League_Mark>().target = collision.gameObject;
                collision.GetComponent<League_Opponent>().markedBy = inst;
                //deal damage
                collision.gameObject.GetComponent<League_Opponent>().health -= damage;
                Destroy(gameObject);
                //audio
                sfxHit.PlayAudio();
            }
        }
        //Hit Player
        else
        {
            if (collision.CompareTag("Player"))
            {
                GameObject inst = Instantiate(markPrefab,
                    new Vector3(collision.transform.position.x, collision.transform.position.y, collision.transform.position.z), transform.rotation);
                inst.GetComponent<League_Mark>().playerMark = false;
                inst.GetComponent<League_Mark>().target = collision.gameObject;
                collision.GetComponent<Player_League>().markedBy = inst;
                //deal damage
                collision.gameObject.GetComponent<Player_League>().health -= damage;
                Destroy(gameObject);
                //audio
                sfxHit.PlayAudio();
            }
        }

        //Wall
        if (collision.CompareTag("Wall"))
        {
            Destroy(gameObject);
        }
    }
}
