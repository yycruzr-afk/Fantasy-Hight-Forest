using UnityEditor.AnimatedValues;
using UnityEngine;

public class CheckGround : MonoBehaviour
{

    private bool enSuelo = false;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        enSuelo = true;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        enSuelo = false;
    }

    public bool GetEnSuelo() { return enSuelo; }

}
