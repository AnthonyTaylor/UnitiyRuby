using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpponentController : MonoBehaviour
{
    /// <summary>
    /// Number of opponents remaining
    /// </summary>
    public int opponentsLeft  { get; private set; }
    public RubyController ruby;
    public AudioClip questComplete;

    void Awake()
    {
        opponentsLeft = 0;
    }

    /// <summary>
    /// Lowers remaining enemy count,
    /// Will Handle onAllEnemiesDead
    /// </summary>
    public void opponentHasDied()
    {
        opponentsLeft--;

        if (opponentsLeft == 0)
        {
            //ruby.PlaySound(QuestComplete);
            // Enable 'Cleared' UI menu here
            // Destroy this if needs be

            ruby.PlaySound(questComplete);

           // Debug.Log("All enemies have died");
        }
        Debug.Log($"{opponentsLeft} enemies remaining");
    }


    /// <summary>
    /// increases remaining enemies count
    /// </summary>
    public void opponentHasAppeared()
    {
        opponentsLeft++;
    }
}
