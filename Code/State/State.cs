using UnityEngine;

[CreateAssetMenu]
public class State : ScriptableObject
{
    public string ID;
    public State nextState;
}