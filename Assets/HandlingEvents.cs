using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class HandlingEvents : MonoBehaviour {

    public Sprite Initial; //Drag your first sprite here in inspector.
    public Sprite Second; //Drag your second sprite here in inspector.


    public bool nextTurn = false;       // Flag check for next turn.
    public int prevEvent = 0;
    public int Max_Events = 5;
    public bool debugflag = true;
    public GameObject Text_Dummy;
    public GameObject Text_Button_Dummy;
    public GameObject[] Water_Dummy;
    



    int Cash_In_Hand = 10000;
    int Cash_Additional = 2000;



    void select_event()  // To decide which event to run
    {
        int currentEvent;
        do
        {
            currentEvent = Random.Range(1, Max_Events);
        } while (currentEvent == prevEvent);      //Random Seed to determine which event to run, Cannot repeat events 
        prevEvent = currentEvent;

        Text_Dummy.SetActive(true);
        Text_Button_Dummy.SetActive(true);


        switch (currentEvent)
        {

            case 1:                 // Crocodiles

                GameObject.FindWithTag("TextBox").GetComponent<Text>().text = "Crocodiles are awesome ";          // Enter Details On Crocs
                break;

            case 2:
                GameObject.FindWithTag("TextBox").GetComponent<Text>().text = "Water Hydrants are awesome ";          // Enter Details On Water hydrants
                break;

            case 3:
                GameObject.FindWithTag("TextBox").GetComponent<Text>().text = "Manatees are awesome ";          // Enter Details On Manatees
                break;

            case 4:
                GameObject.FindWithTag("TextBox").GetComponent<Text>().text = "Bats are awesome ";          // Enter Details On Whatever
                break;

        }

        

        nextTurn = false;
        if (debugflag) debugflag = false;
        else debugflag = true;



    }

    // Use this for initialization
    void Start () {

            Water_Dummy = GameObject.FindGameObjectsWithTag("WaterPlant");

        foreach(GameObject Water in Water_Dummy)
        {
            Water.SetActive(false);

        }

		
	}
	
	// Update is called once per frame
	void Update () {

        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);

            //  Ray ray = Camera.main.WorldToScreenPoint(Input.mousePosition);
            //  Debug.DrawRay(ray.origin, ray.direction * 10, Color.yellow);

            if (hit.collider) {

                if(hit.collider.tag.Contains("ButtonClose") ) // Close Text Menu Box just a test
                {
                    GameObject.FindWithTag("TextBox").SetActive(false);
                    GameObject.FindWithTag("ButtonClose").SetActive(false);



                    nextTurn = true;

                }
                if (hit.collider.tag.Contains("NextTurn") && nextTurn )      // Make the Major Gameplay Changes
                {

                     Cash_In_Hand += Cash_Additional;
                    GameObject.FindWithTag("Tax").GetComponent<Text>().text ="$"+Cash_In_Hand;

                    //        GameObject.FindWithTag("Background").GetComponent<Image>().sprite = Second;

                    foreach (GameObject Water in Water_Dummy)
                        Water.SetActive(true);

                    select_event();         
                }


            }





        }
	}
}
