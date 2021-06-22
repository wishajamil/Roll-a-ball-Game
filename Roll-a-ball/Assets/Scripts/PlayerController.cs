/*
     unity12_wjamil1_Rollaball
    
     Wisha Jamil
     Account: wjamil1@gamedev.cs.gsu.edu
     CSc 4821
     Unity PC12 Homework
     Due date: 04/18/2021
    
     Description::
         Make the player will roll around the plane collecting yellow objects. 
	     every yellow cube is worth 10 points, if the player crashes with red traps/hazards,
         the game will end.
     Input:
         Game inputs are the x and y movement of the player
     Output:
         Game output is the player moving using the up/down/left/right arrow keys
*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;

public class PlayerController : MonoBehaviour
{
    public float speed = 0;
    public TextMeshProUGUI countText;
	public GameObject winTextObject;

    private Rigidbody rb;
    private float movementX;
    private float movementY;
    private int count;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        count = 0;
		SetCountText();
        winTextObject.SetActive(false);
    }

    private void OnMove(InputValue movementValue)
    {
        Vector2 movementVector = movementValue.Get<Vector2>();

        movementX = movementVector.x;
        movementY = movementVector.y;
    }

    private void FixedUpdate()
    {
        Vector3 movement = new Vector3(movementX, 0.0f, movementY);
        rb.AddForce(movement * speed);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("PickUp")) 
        {
            other.gameObject.SetActive(false);
			count = count + 10;
			SetCountText();
        }
        if (other.gameObject.CompareTag("Trap")){
	        countText.text = "Game Over!"; 
            GetComponent<Rigidbody>().isKinematic = true;
        }
    }

    void SetCountText()
	{
		countText.text = "Score: " + count.ToString();
		if (count >= 120) 
		{
            winTextObject.SetActive(true);
            GetComponent<Rigidbody>().isKinematic = true;
		}
	}
}
