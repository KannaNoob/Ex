using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;
using UnityEngine.UI;

public class Intro : MonoBehaviour
{
    bool flag = true;
    int count = 0;
    public GameObject Instructions;
    public GameObject EnableInstructions;
    public GameObject Remove_Background;

    // Use this for initialization
    void Start()
    {
        EnableInstructions.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {

            if (flag)
            {
                Remove_Background.SetActive(false);
                EnableInstructions.SetActive(true);
                flag = false;
            }
            //           SceneManager.LoadScene("Test");
            else
            {


                RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);

                if (hit.collider)
                {
                    if (hit.collider.tag.Contains("NextTurn"))
                    {


                        if (count == 0)
                        {
                            Instructions.GetComponent<Text>().text = "   As the leader of our town, we need you to help us create the best home for us and all the different animals and plants that live here. ";
                            count++;
                        }
                        else if (count == 1)
                        {
                            Instructions.GetComponent<Text>().text = "   You'll have 10 turns to handle different requests from varying citizens looking to bring more or reduce certain plants and animals in Lakeville. ";
                            count++;
                        }
                        else if (count == 2)
                        {
                            Instructions.GetComponent<Text>().text = " Each species affects the environment differently, and depending on the balance you create, could benefit or harm the habitat. ";
                            count++;
                        }
                        else if (count == 3)
                        {
                            Instructions.GetComponent<Text>().text = " When dealing with tough decisions, click the info boxes to learn how that species affects others.  Earn Happiness points and Money by keeping a balanced town of native, non-native, and invasive species. ";
                            count++;
                        }
                        else if (count == 4)
                        {
                            Instructions.GetComponent<Text>().text = " It's up to you to decide what's best for Lakeville's citizens and wildlife. Good luck!";
                            count++;
                        }
                        else if (count == 5)
                        {
                            count++;
                            SceneManager.LoadScene("Test");
                        }

                    }


                }
            }
        }

    }
}
