
using UnityEngine;

[System.Serializable]
public enum RotationType{
     FORWARD,
     BACKWARD,
     LEFT,
     RIGHT

}

[CreateAssetMenu(fileName = "AroundSO", menuName = "AroundSO", order = 1)]
public class AroundSO : ScriptableObject
{
     public string AroundName;
     public Sprite AroundSprite;
     public Vector3 AroundRotation;

     public RotationType rotationType;

}
