using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "High Score", menuName = "Scriptable Objects/High Score", order = 1)]
public class HighScore : ScriptableObject
{
    public int player1Score;
    public int player2Score;
}