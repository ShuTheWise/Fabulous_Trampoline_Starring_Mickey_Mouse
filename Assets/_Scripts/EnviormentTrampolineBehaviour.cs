using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets._2D;

public class EnviormentTrampolineBehaviour : MonoBehaviour {


    private PlatformerCharacter2D m_Character;

    // Use this for initialization
    void Start ()
    {
        m_Character = GameObject.FindGameObjectWithTag("Player").GetComponent<PlatformerCharacter2D>();

    }
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnCollisionEnter2D(Collision2D other)
    {
        if(other.collider.tag == "Player")
        {
            m_Character.Move(0.4f, false, true);
            InvokeRepeating("MovePlayer", 1f, 1f);


        }

    }

    private void MovePlayer()
    {
        m_Character.Move(10f, false, false);
    }

    /*
    void On
    {
        if (other.tag == "Player")
      
    }*/
}
