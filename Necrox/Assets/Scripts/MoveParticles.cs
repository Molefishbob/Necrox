using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveParticles : MonoBehaviour {

    private ParticleSystem _parSystem;
    private ParticleSystem.Particle[] _theParticles;
    public GameObject _mainChar;
    private float _ySpeed;
    private float _xSpeed;
    private bool _xMovementDone;
    private bool _yMovementDone;

    // Use this for initialization
    void Start () {
        _xSpeed = transform.localPosition.x / 2;
        _ySpeed = transform.localPosition.y / 2;
        Debug.Log(string.Format("XSpeed: {0} YSpeed: {1}",_xSpeed,_ySpeed));
        Debug.Log(string.Format("XPosition: {0} YPosition: {1}",transform.localPosition.x,transform.localPosition.y));
        // int numParticlesAlive = _parSystem.GetParticles(_theParticles);
        // _parSystem = GetComponent<ParticleSystem>();
        // _theParticles = new ParticleSystem.Particle[_parSystem.main.maxParticles];
	}

    // Update is called once per frame
    private void LateUpdate()
    {
        Movement();

    }

    private void Movement()
    {
        XMovement();
        YMovement();
        if (_yMovementDone && _xMovementDone) {
            Destroy(gameObject);
        }
    }

    private void XMovement()
    {
        if (transform.localPosition.x > 0 && !_xMovementDone)
        {

            transform.localPosition = new Vector2(transform.localPosition.x - _xSpeed * Time.deltaTime, transform.localPosition.y);

            if (transform.localPosition.x <= 0)
            {
                Debug.Log("XDONE");
                transform.localPosition = new Vector2(0, transform.localPosition.y);
                _xMovementDone = true;

            }

        }
        else if (transform.localPosition.x < 0 && !_xMovementDone)
        {

            transform.localPosition = new Vector2(transform.localPosition.x + _xSpeed * Time.deltaTime, transform.localPosition.y);

            if (transform.localPosition.x >= 0)
            {
                Debug.Log("XDONE");
                transform.localPosition = new Vector2(0, transform.localPosition.y);
                _xMovementDone = true;

            }

        }
    }

    private void YMovement()
    {
        if (transform.localPosition.y > 0&& !_yMovementDone)
        {
            transform.localPosition = new Vector2(transform.localPosition.x, transform.localPosition.y + _ySpeed * Time.deltaTime);

            if (transform.localPosition.y <= 0)
            {

                Debug.Log("Y DONE");
                transform.localPosition = new Vector2(transform.localPosition.x, 0);
                _yMovementDone = true;

            }

        }
        else if (transform.localPosition.y < 0 && !_yMovementDone)
        {

            transform.localPosition = new Vector2(transform.localPosition.x, transform.localPosition.y - _ySpeed * Time.deltaTime);

            if (transform.localPosition.y >= 0)
            {

                Debug.Log("Y DONE");
                transform.localPosition = new Vector2(transform.localPosition.x, 0);
                _yMovementDone = true;

            }

        }

    }
    
}
