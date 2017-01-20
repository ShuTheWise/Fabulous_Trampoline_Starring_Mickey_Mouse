using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets._2D;

public class EnviormentTrampolineBehaviour : MonoBehaviour
{
    public float upforce;
    private PlatformerCharacter2D m_Char;
    private Rigidbody2D body;
    private PlatformEffector2D effector;
    private bool _playerIntrigger;
    private void OnCollisionEnter2D(Collision2D other)
    {

        if (other.collider.tag == "Player")
        {
            _playerIntrigger = true;
            m_Char = other.gameObject.GetComponent<PlatformerCharacter2D>();
            body = other.gameObject.GetComponent<Rigidbody2D>();

        }
        if (other.collider.tag == "Moveable")
        {
            other.rigidbody.MoveRotation(Random.Range(0f, 10f));
            other.rigidbody.AddForce(new Vector2(Random.Range(-10f, 10f), 100f));
        }

    }

    private void Update()
    {
        if (_playerIntrigger)
        {
            if (_playerIntrigger && Mathf.Abs(body.velocity.y) < Mathf.Epsilon)
            {
                m_Char.TrampolineJump(upforce);
                GetComponent<AudioSource>().Play();
            }
        }
    }

    private void OnCollisionExit2D(Collision2D other)
    {
        if (other.collider.tag == "Player")
        {
            _playerIntrigger = false;
        }
    }

    private void Start()
    {
        GetComponentInChildren<Animator>().SetFloat("offset", Random.Range(0, 1f));
      //  GetComponentInChildren<>();


    }

    /*
    void On
    {
        if (other.tag == "Player")
      
    }*/
}
