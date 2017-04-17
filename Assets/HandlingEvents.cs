using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;


public class HandlingEvents : MonoBehaviour {

    public AudioSource Click_Sounds;
    public AudioClip Yes, Maybe, No , npc_fish,npc_polo,npc_build,npc_farm;
    public AudioClip ali, mant, tree, hy;
         
    public Sprite Initial; //Drag your first sprite here in inspector.
    public Sprite Second; //Drag your second sprite here in inspector.

    public Texture Farmer;
    public Texture Fisherman;
    public Texture Poli;
    public Texture Nature;

    public Texture Info_Ali, Info_Mana, Info_Orange, Info_WaterHy;

    public GameObject Win_Screen;
    public GameObject Lose_Screen;
    public GameObject Lose_text;

    // The button sprites for press down and push up
    public Sprite Many_up;
    public Sprite Many_Down;
    public Sprite some_up;
    public Sprite some_Down;
    public Sprite less_up;
    public Sprite less_Down;
    public Sprite Next_Up;
    public Sprite Next_down;


    // The 3 buttons for Options

    public GameObject Many;
    public GameObject Some;
    public GameObject Less;
    public GameObject Next_Turn;

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
    public GameObject InfoBox;

    int turncount;

    public ParticleSystem Particle_A;
    public int currentEvent = 0;

    int[] Sprite_Count = new int[Max_Events];

    public int Chaos = 0;   // Used to determine How well/bad the city is doing.
    


    int Cash_In_Hand = 10000;

    IEnumerator Next_Pop()
    {
        Next_Turn.GetComponent<Image>().sprite = Next_down;
        yield return new WaitForSeconds(0.3f);
        Next_Turn.GetComponent<Image>().sprite = Next_Up;
    }

    IEnumerator button()
    {

        Many.GetComponent<Image>().sprite = Many_Down;
        yield return new WaitForSeconds(0.5f);
        Many.GetComponent<Image>().sprite = Many_up;
        GameObject.FindWithTag("TextBox").SetActive(false);
        GameObject.FindWithTag("ButtonClose").SetActive(false);
        Text_UI.SetActive(false);
        StartCoroutine(waiting());

    }
    IEnumerator button1()
    {

        Some.GetComponent<Image>().sprite = some_Down;
        yield return new WaitForSeconds(0.5f);
        Some.GetComponent<Image>().sprite = some_up;
        GameObject.FindWithTag("TextBox").SetActive(false);
        GameObject.FindWithTag("ButtonClose").SetActive(false);
        Text_UI.SetActive(false);
        StartCoroutine(waiting());

    }
    IEnumerator button2()
    {

        Less.GetComponent<Image>().sprite = less_Down;
        yield return new WaitForSeconds(0.5f);
        Less.GetComponent<Image>().sprite = less_up;
        GameObject.FindWithTag("TextBox").SetActive(false);
        GameObject.FindWithTag("ButtonClose").SetActive(false);
        Text_UI.SetActive(false);
        StartCoroutine(waiting());

    }



    IEnumerator waiting()               // Popping up all the sprites
    {

        GameObject.FindWithTag("Background").GetComponent<Image>().sprite = Second;
        foreach (GameObject Water in Water_Dummy)
        {
            Water.SetActive(false);

        }

        for (int j = 0; j < Max_Events; j++)
        {

            if (j == 0) Click_Sounds.clip = ali;
            else if (j == 1) Click_Sounds.clip = hy;
            else if (j == 2) Click_Sounds.clip = mant;
            else if (j == 3) Click_Sounds.clip = tree;


            for (int i = 0; i < Sprite_Count[j]; i++)
                {
                    Water_Dummy[i + (j * 10)].SetActive(true);
                    Click_Sounds.Play();
                    yield return new WaitForSeconds(0.3f);

                }
        }
        GameObject.FindWithTag("Background").GetComponent<Image>().sprite = Initial;
    }

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

        turncount++;

        for (int i = 0; i < Max_Events; i++)
            if (Sprite_Count[i] > 5) Chaos++;

        switch (currentEvent)
        {

            case 0:                 // Crocodiles

                GameObject.FindWithTag("TextBox").GetComponent<Text>().text = "Hello, Mayor! The government wants to give money to the city that preserves the most <color=#ff5400>alligators</color>, and I want to pass a bill to bring more here. Can I get your support? "; // Enter Details On Crocs
                GameObject.FindWithTag("Hero").GetComponent<RawImage>().texture = Poli;
                InfoBox.GetComponent<RawImage>().texture = Info_Ali;
                Click_Sounds.clip = npc_polo;
                Click_Sounds.Play();

                break;

            case 1:                 //Water Hyacint
                GameObject.FindWithTag("TextBox").GetComponent<Text>().text = " Greetings! I’m a developer here in Lakeville, and I want to bring a new plant here to attract tourists with a pretty scenery. <color=#ff5400> Water Hyacinth</color> are beautiful, and I’ve heard Manatees love to eat them, so what do you say?";          // Enter Details On Water hydrants
                GameObject.FindWithTag("Hero").GetComponent<RawImage>().texture = Nature;
                InfoBox.GetComponent<RawImage>().texture = Info_WaterHy;
                Click_Sounds.clip = npc_build;
                Click_Sounds.Play();

                break;

            case 2:                 //Manatees
                GameObject.FindWithTag("TextBox").GetComponent<Text>().text = " Good afternoon, Mayor! <color=#ff5400> Manatees</color> are endangered and need our help.  If you help us rescue more Manatees, the town’s citizens will be extremely grateful.  Plus, they’ll help eat any pesky invasive plants, like Water Hyacinth.  Will you help us out? ";          // Enter Details On Manatees
                GameObject.FindWithTag("Hero").GetComponent<RawImage>().texture = Fisherman;
                InfoBox.GetComponent<RawImage>().texture = Info_Mana;
                Click_Sounds.clip = npc_fish;
                Click_Sounds.Play();

                break;

            case 3:                 //Orange Trees
                GameObject.FindWithTag("TextBox").GetComponent<Text>().text = " Hey Mayor, my other crops haven’t been selling as well this year. if we could import some more <color=#ff5400>Orange Trees</color> to our city, the citizens would appreciate it, Lakeville would certainly profit, and I might be able to survive the rest of the year after all.  So, how about it?";          // Enter Details On Whatever
                GameObject.FindWithTag("Hero").GetComponent<RawImage>().texture = Farmer;
                InfoBox.GetComponent<RawImage>().texture = Info_Orange;
                Click_Sounds.clip = npc_farm;
                Click_Sounds.Play();

                break;

        }

        

        nextTurn = false;
        if (debugflag) debugflag = false;
        else debugflag = true;

        if (turncount > 9)             // End Game Box to be added
        {

            Text_Dummy.SetActive(false);
            Text_Button_Dummy.SetActive(false);
            Text_UI.SetActive(false);
            if (Chaos < 10)
                Win_Screen.SetActive(true);
            else {

                if (Sprite_Count[0] > 6) Lose_text.GetComponent<Text>().text += "Too many aligators! Get some Orange Trees next time";
                if (Sprite_Count[1] > 6) Lose_text.GetComponent<Text>().text += "Too many Water Hycants ! Get some Manatees next time";
                if (Sprite_Count[2] > 6) Lose_text.GetComponent<Text>().text += "Too many Manatees! Get some Aligators Trees next time";
                if (Sprite_Count[3] > 6) Lose_text.GetComponent<Text>().text += "Too many Orange trees! Get some Water Hycants next time";

                Lose_Screen.SetActive(true);

                 }


        }

    }

    GameObject[] FindGameObjectsWithTags(params string[] tags)
    {
        var all = new List<GameObject>();

        foreach (string tag in tags)
        {
            var temp = GameObject.FindGameObjectsWithTag(tag).ToList();
            all = all.Concat(temp).ToList();
        }

        return all.ToArray();
    }


    // Use this for initialization
    void Start() {

        turncount = 0;
        Win_Screen.SetActive(false);
        Lose_Screen.SetActive(false);
        InfoBox = GameObject.FindWithTag("Info");
        InfoBox.SetActive(false);
        Water_Dummy = FindGameObjectsWithTags(new string[]{"Croc", "WaterPlant", "Manatee", "Orange"});
        

        foreach (GameObject Water in Water_Dummy)
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
            if (InfoBox.activeSelf == true)
            {
                InfoBox.SetActive(false);
                Text_Dummy.SetActive(true);
                Text_Button_Dummy.SetActive(true);
                Text_UI.SetActive(true);
            }

            RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);

            //  Ray ray = Camera.main.WorldToScreenPoint(Input.mousePosition);
            //  Debug.DrawRay(ray.origin, ray.direction * 10, Color.yellow);

            if (hit.collider) {

                if(hit.collider.tag.Contains("ButtonClose") ) // Info Text Box .
                {
                    GameObject.FindWithTag("TextBox").SetActive(false);
                    GameObject.FindWithTag("ButtonClose").SetActive(false);
                    Text_UI.SetActive(false);
                    InfoBox.SetActive(true);
                    nextTurn = false;

                }
                if(hit.collider.tag.Contains("OptionA"))                // Best Case for Advisor
                {
                    StartCoroutine(button());
                    Click_Sounds.clip = Yes;
                    Click_Sounds.Play();
                    /*
                            GameObject.FindWithTag("TextBox").SetActive(false);
                            GameObject.FindWithTag("ButtonClose").SetActive(false);
                            Text_UI.SetActive(false);
                            StartCoroutine(button());
                      */

                    Sprite_Count[currentEvent] += 3;
                    Particle_A.Play(); // Yellow sparkles
                    nextTurn = true;
                }
                if (hit.collider.tag.Contains("OptionB"))               // Balanced approach 
                {
                
                    StartCoroutine(button1());
                    Click_Sounds.clip = Maybe;
                    Click_Sounds.Play();
                    /*
                    GameObject.FindWithTag("TextBox").SetActive(false);
                    GameObject.FindWithTag("ButtonClose").SetActive(false);
                    Text_UI.SetActive(false);
                    StartCoroutine(waiting());
                    */

                    Sprite_Count[currentEvent] += 2;
                    nextTurn = true;

                }
                if (hit.collider.tag.Contains("OptionC"))           // Worst case for Advisor
                {

                    StartCoroutine(button2());
                    Click_Sounds.clip = No;
                    Click_Sounds.Play();
                    /*GameObject.FindWithTag("TextBox").SetActive(false);
                    GameObject.FindWithTag("ButtonClose").SetActive(false);
                    Text_UI.SetActive(false);
                      StartCoroutine(waiting());

                    */

                    Sprite_Count[currentEvent]++;

                    nextTurn = true;

                }

                if (Sprite_Count[currentEvent] > 10) Sprite_Count[currentEvent] = 10; 
                
                
                

                if (hit.collider.tag.Contains("NextTurn") && nextTurn)      // Make the Major Gameplay Changes
                {

                    StartCoroutine(Next_Pop());

                    // Species Rock Paper Scissors here                                     0 = Aligator , 1 =WaterHY 2=Manatee 3 = Orange tree

                    if (Sprite_Count[0] > 8 && Sprite_Count[2] > 1) Sprite_Count[2] -= 2;       // Aligator Eat Manatee
                    else if (Sprite_Count[0] > 5 && Sprite_Count[2] > 0) Sprite_Count[2]--;

                    if (Sprite_Count[2] > 8 && Sprite_Count[1] > 1) Sprite_Count[1] -= 2;       // Manatee Eat WaterHy
                    else if (Sprite_Count[2] > 5 && Sprite_Count[1] > 0) Sprite_Count[1]--;

                    if (Sprite_Count[1] > 8 && Sprite_Count[3] > 1) Sprite_Count[3] -= 2;      //WaterHY affect orange tree ?? 
                    else if (Sprite_Count[1] > 5 && Sprite_Count[3] > 0) Sprite_Count[3]--;

                    if (Sprite_Count[3] > 8 && Sprite_Count[0] > 1) Sprite_Count[0] -= 2;       // Orange tree affect Aligators??
                    else if (Sprite_Count[3] > 5 && Sprite_Count[0] > 0) Sprite_Count[0]--;



                    GameObject.FindWithTag("Tax").GetComponent<Text>().text = "$" + Cash_In_Hand;

                    // Like this for switching between backgrounds
                    //          if(Chaos > 15 ) {
                    //                          GameObject.FindWithTag("Background").GetComponent<Image>().sprite = Second;
                    //                            }


                    // Displays All the Events ( -- o(N^2) make more efficient) ) 
                    /*
                    for (int j=0;j<Max_Events;j++)
                        for(int i =0; i<Sprite_Count[j];i++ )
                             Water_Dummy[i+(j*10)].SetActive(true);
                    */

                    select_event();                     // Calls the Random for next event
                }


            }





        }
	}
}
