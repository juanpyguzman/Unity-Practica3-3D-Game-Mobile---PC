using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpiderControllerScript : MonoBehaviour
{

    static int speedHash = Animator.StringToHash("Speed");
    static int attackHash = Animator.StringToHash("Attack");
    public Animator animComponent = null;

    BoxCollider box;
    float original_z = 0;

    float verticalAxis = 0;
    float horizontalAxis = 0;


    void Start()
    {
        box = GetComponent<BoxCollider>();
        original_z = box.center.z;
        animComponent = GetComponent<Animator>();
    }

    void Update()
    {
        //Movimiento
        verticalAxis = Input.GetAxis("Vertical");
        horizontalAxis = Input.GetAxis("Horizontal");

        //Ataque
        if (Input.GetKeyDown(KeyCode.Space))
        {
            animComponent.SetTrigger(attackHash);
        }

        animComponent.SetFloat(speedHash, verticalAxis);

        //Movimiento del collider al atacar
        box.center = new Vector3(box.center.x, box.center.y, original_z + animComponent.GetFloat("Distance")*10.0f);
    }
}
