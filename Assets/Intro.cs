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
                            Instructions.GetComponent<Text>().text = "   Second Set of instructions ";
                            count++;
                        }
                        else if (count == 1)
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
