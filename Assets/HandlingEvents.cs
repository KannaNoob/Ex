using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class HandlingEvents : MonoBehaviour {

    public Sprite Initial; //Drag your first sprite here in inspector.
    public Sprite Second; //Drag your second sprite here in inspector.
    public Texture Farmer;
    public Texture Fisherman;


    public bool nextTurn = false;       // Flag check for next turn.
    /*public bool WaterHy = false;
    public int WaterHyCount = 2;
    */
    public int prevEvent = 0;
    const int Max_Events = 4;
    public bool debugflag = true;
    public GameObject Text_UI;
    public GameObject Text_Dummy;
    public GameObject Text_Button_Dummy;
    public GameObject[] Water_Dummy;

    public ParticleSystem Particle_A;
    public int currentEvent = 0;

    int[] Sprite_Count = new int[Max_Events];

    public float Chaos = 0.0f;   // Used to determine How well/bad the city is doing.
    


    int Cash_In_Hand = 10000;
    int Cash_Additional = 2000;



    void select_event()  // To decide which event to run
    {
        do
        {
            currentEvent = Random.Range(0, Max_Events);
        } while (currentEvent == prevEvent);      //Random Seed to determine which event to run, Cannot repeat events 
        prevEvent = currentEvent;

        Text_Dummy.SetActive(true);
        Text_Button_Dummy.SetActive(true);
        Text_UI.SetActive(true);

        switch (currentEvent)
        {

            case 1:                 // Crocodiles

                GameObject.FindWithTag("TextBox").GetComponent<Text>().text = "Hello, Mayor! The government wants to give money to the city that preserves the most alligators, and I want to pass a bill to bring more here. Can I get your support? "; // Enter Details On Crocs
                GameObject.FindWithTag("Hero").GetComponent<RawImage>().texture = Fisherman;                
                break;

            case 0:
                GameObject.FindWithTag("TextBox").GetComponent<Text>().text = " Greetings! I’m a developer here in Lakeville, and I want to bring a new plant here to attract tourists with a pretty scenery.  Water Hyacinth are beautiful, and I’ve heard Manatees love to eat them, so what do you say?";          // Enter Details On Water hydrants
                GameObject.FindWithTag("Hero").GetComponent<RawImage>().texture = Fisherman;
                break;

            case 2:
                GameObject.FindWithTag("TextBox").GetComponent<Text>().text = " Good afternoon, Mayor!  Manatees are endangered and need our help.  If you help us rescue more Manatees, the town’s citizens will be extremely grateful.  Plus, they’ll help eat any pesky invasive plants, like Water Hyacinth.  Will you help us out? ";          // Enter Details On Manatees
                GameObject.FindWithTag("Hero").GetComponent<RawImage>().texture = Farmer;
                break;

            case 3:
                GameObject.FindWithTag("TextBox").GetComponent<Text>().text = " Hey Mayor, my other crops haven’t been selling as well this year. if we could import some more Orange Trees to our city, the citizens would appreciate it, Lakeville would certainly profit, and I might be able to survive the rest of the year after all.  So, how about it?";          // Enter Details On Whatever
                GameObject.FindWithTag("Hero").GetComponent<RawImage>().texture = Farmer;
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

        for(int i = 0; i < Sprite_Count.Length; i++)
            Sprite_Count[i] = 0;

		
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
                    Text_UI.SetActive(false);
                    nextTurn = true;

                }
                if(hit.collider.tag.Contains("OptionA"))                // Best Case for Advisor
                {
                    GameObject.FindWithTag("TextBox").SetActive(false);
                    GameObject.FindWithTag("ButtonClose").SetActive(false);
                    Text_UI.SetActive(false);
                    Sprite_Count[currentEvent] += 3;
                    Particle_A.Play(); // Yellow sparkles

                    nextTurn = true;
                }
                if (hit.collider.tag.Contains("OptionB"))               // Balanced approach 
                {
                    GameObject.FindWithTag("TextBox").SetActive(false);
                    GameObject.FindWithTag("ButtonClose").SetActive(false);
                    Text_UI.SetActive(false);
                    Sprite_Count[currentEvent] += 2;


                    nextTurn = true;

                }
                if (hit.collider.tag.Contains("OptionC"))           // Worst case for Advisor
                {
                    GameObject.FindWithTag("TextBox").SetActive(false);
                    GameObject.FindWithTag("ButtonClose").SetActive(false);
                    Text_UI.SetActive(false);

                    Sprite_Count[currentEvent]++;


                    nextTurn = true;

                }

                if (Sprite_Count[currentEvent] > 10) Sprite_Count[currentEvent] = 10; 
                


                if (hit.collider.tag.Contains("NextTurn") && nextTurn)      // Make the Major Gameplay Changes
                {

                    Cash_In_Hand += Cash_Additional;
                    GameObject.FindWithTag("Tax").GetComponent<Text>().text = "$" + Cash_In_Hand;

                    // Like this for switching between backgrounds
                    //          if(Chaos > 15 ) {
                    //                          GameObject.FindWithTag("Background").GetComponent<Image>().sprite = Second;
                    //                            }

                    
                    // Displays All the Events ( -- o(N^2) make more efficient) ) 
                    for (int j=0;j<Max_Events;j++)
                        for(int i =0; i<Sprite_Count[j];i++ )
                             Water_Dummy[i+(j*10)].SetActive(true);

                    select_event();                     // Calls the Random for next event
                }


            }





        }
	}
}
