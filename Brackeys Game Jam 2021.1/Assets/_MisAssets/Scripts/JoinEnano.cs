using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JoinEnano : MonoBehaviour
{
    public float joiningVelocity = 2f;

    public MovementData movementData;

    private Movement3D movement;
    private Jump3D jump;

    private Enano joiningEnano;

    private bool sentandose = false;

    private void Awake()
    {
        jump = GetComponent<Jump3D>();
        movement = GetComponent<Movement3D>();
    }

    public void Join(Enano enanoToJoin, Enano mainEnano)
    {
        jump.Allowed = false;
        //movement.Allowed = false;
        movement.autoMove = true;
        movement.autoMovementData = movementData;

        joiningEnano = enanoToJoin;

        float dist = joiningEnano.transform.position.x - MainEnano.transform.position.x;
        float dir = dist > 0 ? 1 : dist < 0 ? -1 : 0;

        movement.LateralInput = dir;

        

        //movement.rb.velocity = new Vector3(dir * joiningVelocity, 0, 0);

        enanoToJoin.Join();
    }

    private void FixedUpdate()
    {
        if (joiningEnano == null) return;
        if (sentandose) return;
        if(movement.LateralInput>0)
        {
            if (joiningEnano.transform.position.x - MainEnano.transform.position.x<=0)
            {
                StartCoroutine(Sentarse());
            }
        }
        else if(movement.LateralInput < 0)
        {
            if (joiningEnano.transform.position.x - MainEnano.transform.position.x >= 0)
            {
                StartCoroutine(Sentarse());
            }
        }
        else
        {
            StartCoroutine(Sentarse());
        }
        

    }

    IEnumerator Sentarse()
    {
        //animación sentarse
        sentandose = true;
        transform.position = new Vector3(joiningEnano.transform.position.x, transform.position.y, transform.position.z);
        movement.LateralInput = 0;

        yield return new WaitForSeconds(0f); //lo que dure la animación de sentarse

        jump.Allowed = true;
        //movement.Allowed = true;
        movement.autoMove = false;
        EnanoManager.Instance.SetNewEnano(joiningEnano);
        yield return new WaitForEndOfFrame();
        joiningEnano = null;
        sentandose = false;

    }

    

    public Enano MainEnano
    {
        get
        {
            return EnanoManager.Instance.MainEnano;
        }
    }
}
