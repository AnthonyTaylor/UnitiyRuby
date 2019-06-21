using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NonPlayerCharacter : MonoBehaviour
{
    public float displayTime = 4.0f;
    public GameObject dialogBox;
    public GameObject beginText;
    public GameObject completeText;
    float timerDisplay;

    // Start is called before the first frame update

    /// <summary>
    /// Deacivates Dialog box
    /// </summary>
    void Start()
    {
        dialogBox.SetActive(false);
        beginText.SetActive(false);
        completeText.SetActive(false);
        timerDisplay = -1.0f;
    }

    // Update is called once per frame

    /// <summary>
    /// Checks timer display
    /// Lowers timer display
    /// Disables dialog box when at 0
    /// </summary>
    void Update()
    {
        if (timerDisplay >= 0)
        {
            timerDisplay -= Time.deltaTime;
            if (timerDisplay < 0)
            {
                dialogBox.SetActive(false);
                beginText.SetActive(false);
                completeText.SetActive(false);
            }
        }
    }

    /// <summary>
    /// Shows dialog box,
    /// Changes message depending on number of remaining enemies
    /// </summary>
    /// <param name="complete">Are all enemies dead?</param>
    public void DisplayDialog(bool complete)
    {

        timerDisplay = displayTime;
        dialogBox.SetActive(true);
        if (complete == true)
        {
            completeText.SetActive(true);
            beginText.SetActive(false);
        }
        else
        {
            completeText.SetActive(false);
            beginText.SetActive(true);
        }
    }
}
