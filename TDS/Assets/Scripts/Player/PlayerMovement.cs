using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerMovement : Entity
{

    public Gun gun;
    Vector2 m_Position;
    public Rigidbody2D centerRb;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {

        moveHorizontal = Input.GetAxis("Horizontal");// Pega o input horizontal
        moveVertical = Input.GetAxis("Vertical");// Pega o input Vertical


        switch (state)
        {
            case 0://IDLE
                if (moveHorizontal != 0 || moveVertical != 0)
                    state = 1;
                break;
            case 1://WALKING ON GROUND



            
                break;
        }

        if (Input.GetMouseButtonDown(0))
        {
            gun.Fire();
        }

        m_Position = Camera.main.ScreenToWorldPoint(Input.mousePosition);

    }

    private void FixedUpdate()
    {

        Vector2 aimDirection = m_Position - rb.position;
        float aimAngle = Mathf.Atan2(aimDirection.y, aimDirection.x) * Mathf.Rad2Deg;
        centerRb.rotation = aimAngle;
        centerRb.position = rb.position;

        switch (state)
        {
            case 0://Idle
                break;
            case 1://Walking on ground

                position.x = rb.position.x + moveHorizontal*speed;
                position.y = rb.position.y + moveVertical*speed;

                rb.position = position;
                Debug.Log("Ta aqui!");
                state = 0;
                break;
        }
        centerRb.position = rb.position;
    }

}
