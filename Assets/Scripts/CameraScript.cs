using UnityEngine;

public class CameraScript : MonoBehaviour
{
    public GameObject SirDominic;  // Objeto de juego SirDominic que la cámara seguirá

    // Update se llama una vez por fotograma
    void Update()
    {
        // Obtiene la posición actual de la cámara
        Vector3 position = transform.position;
        
        // Actualiza la posición en x de la cámara para que coincida con la de SirDominic
        position.x = SirDominic.transform.position.x;
        
        // Asigna la nueva posición a la cámara
        transform.position = position;
    }
}







