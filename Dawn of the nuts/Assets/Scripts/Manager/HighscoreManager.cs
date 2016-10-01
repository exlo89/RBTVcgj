using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using System.Collections;

public class HighscoreManager : MonoBehaviour {

	private int score;
    public int Score
    {
        get
        {
            return score;
        }
        set
        {
            score = value;
        }
    }
}
