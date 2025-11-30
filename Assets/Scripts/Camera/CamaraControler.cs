using UnityEngine;

public class CamaraControler : MonoBehaviour
{
    [SerializeField]
    private Transform objetivo;
    [SerializeField]
    private float velocidadCamara = 0.025f;
    [SerializeField]
    private Vector3 desplazamiento = new Vector3(0,0,-10);

    [SerializeField]
    private Transform limiteIzquierdo;
    [SerializeField]
    private Transform limiteDerecho;

    //VALORES
    private float mitadAnchoCamara;
    private float mitadAltoCamara;

    private void Start()
    {
        mitadAltoCamara = Camera.main.orthographicSize;
        float relacionAspecto = (float)Screen.width / (float)Screen.height;
        mitadAnchoCamara = mitadAltoCamara * relacionAspecto;

        transform.position = objetivo.position + desplazamiento;
    }

    private void LateUpdate()
    {
        Vector3 posicionDeseado = objetivo.position + desplazamiento;

        float limiteMinX = limiteIzquierdo.position.x + mitadAnchoCamara;
        float limiteMaxX = limiteDerecho.position.x - mitadAnchoCamara;

        float posicionRestringidaX = Mathf.Clamp(posicionDeseado.x, limiteMinX, limiteMaxX);

        posicionDeseado = new Vector3(posicionRestringidaX, posicionDeseado.y, posicionDeseado.z);

        Vector3 posicionSuavizada = Vector3.Lerp(transform.position, posicionDeseado, velocidadCamara * Time.deltaTime);

        transform.position = posicionSuavizada;
    }
}
