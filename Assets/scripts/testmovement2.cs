/*using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestMovement : MonoBehaviour
{
    public float moveSpeed = 5f; // Imposta un valore di velocità realistico
    public Transform movePoint;

    void Start()
    {
        movePoint.parent = null; // Assicura che il movePoint non sia figlio di altri oggetti
    }

    void Update()
    {
        // Muove l'oggetto verso il movePoint
        //transform.position = Vector3.MoveTowards(transform.position, movePoint.position, moveSpeed * Time.deltaTime);
        Vector3 newPosition = Vector3.MoveTowards(transform.position, movePoint.position, moveSpeed * Time.deltaTime);
        transform.position = newPosition;

        // Cambia la posizione del movePoint in base all'input
        if (Mathf.Abs(Input.GetAxisRaw("Horizontal")) == 1f)
        {
            movePoint.position += new Vector3(Input.GetAxisRaw("Horizontal"), 0f, 0f);
        }

        if (Mathf.Abs(Input.GetAxisRaw("Vertical")) == 1f)
        {
            movePoint.position += new Vector3(0f, Input.GetAxisRaw("Vertical"), 0f);
        }
    }
}
*/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestMovement : MonoBehaviour
{
    public float moveSpeed = 0f; // Imposta a 0 per testare
    public Transform movePoint;

    void Start()
    {
        movePoint.parent = null;
        Debug.Log("MoveSpeed at Start: " + moveSpeed);
    }

    void FixedUpdate()
    {
        Debug.Log("FixedDeltaTime: " + Time.fixedDeltaTime);

        // Muove l'oggetto verso il movePoint
        Vector3 newPosition = Vector3.MoveTowards(transform.position, movePoint.position, moveSpeed * Time.fixedDeltaTime);
        transform.position = newPosition;

        Debug.Log("Transform Position: " + transform.position);
        Debug.Log("MovePoint Position: " + movePoint.position);

        // Cambia la posizione del movePoint in base all'input
        if (Mathf.Abs(Input.GetAxisRaw("Horizontal")) == 1f)
        {
            movePoint.position += new Vector3(Input.GetAxisRaw("Horizontal"), 0f, 0f);
        }

        if (Mathf.Abs(Input.GetAxisRaw("Vertical")) == 1f)
        {
            movePoint.position += new Vector3(0f, Input.GetAxisRaw("Vertical"), 0f);
        }
    }
}
