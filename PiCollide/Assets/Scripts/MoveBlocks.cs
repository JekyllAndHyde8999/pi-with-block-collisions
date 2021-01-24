using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveBlocks : MonoBehaviour
{
    public GameObject block2;
    //public int digits; //number of digits of pi to be calculated

    //clack sound
    public AudioSource clack;

    //initial velocities for blocks
    private float speed1 = 0f;
    private float speed2 = 0.3f;

    //initial positions for blocks
    private Vector3 init_pos1;
    private Vector3 init_pos2;

    //counter for number of collisions
    private int collisions = 0;

    // Start is called before the first frame update
    void Start()
    {
        init_pos1 = gameObject.transform.position;
        init_pos2 = block2.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if ((speed1 <= 0) && (speed2 <= 0) && (speed2 < speed1))
        {
            gameObject.transform.position = init_pos1;
            block2.transform.position = init_pos2;
            Debug.Log(collisions);
        }
        else
        {
            gameObject.transform.position += Vector3.forward * speed1 * Time.fixedDeltaTime;
            block2.transform.position += Vector3.forward * speed2 * Time.fixedDeltaTime;
            //Debug.Log(collisions);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log(collision.gameObject.tag);

        float mass1 = gameObject.GetComponent<Rigidbody>().mass;
        float mass2 = block2.GetComponent<Rigidbody>().mass;

        float sumM = mass1 + mass2;
        float diffM = mass1 - mass2;

        float newSpeed1 = speed1;
        float newSpeed2 = speed2;

        if (collision.gameObject.CompareTag("Wall"))
        {
            newSpeed1 = speed1 * -1;
            newSpeed2 = speed2;

            collisions++;

            //clack.Play();
        }
        else if (collision.gameObject.CompareTag("Mass2"))
        {
            //Conserve momentum

            newSpeed1 = (diffM / sumM * speed1) + (2 * mass2 / sumM * speed2);
            newSpeed2 = (2 * mass1 / sumM * speed1) - (diffM / sumM * speed2);

            collisions++;

            //clack.Play();
        }

        speed1 = newSpeed1;
        speed2 = newSpeed2;
    }
}
