using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Finish2 : MonoBehaviour
{
    private bool _playerIn;
    private bool _crateIn;
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            _playerIn = true;
        }
        if (other.tag == "Moveable")
        {
            _crateIn = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            _playerIn = false;
        }
        if (other.tag == "Moveable")
        {
            _crateIn = false;
        }
    }

    private void Update()
    {
        if (_playerIn && _crateIn)
        {
            GameManager.Instance.End();
            GetComponent<AudioSource>().enabled = true;
        }
    }
}
