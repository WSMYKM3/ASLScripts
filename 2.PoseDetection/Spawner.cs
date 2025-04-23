using UnityEngine;

public class Spwaner : MonoBehaviour
{
    public GameObject[] whichcube;
    public Transform[] points;
    public float beat = 60f / 105f; // or just 0.571f

    private float timer;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(timer > beat){
            GameObject cube = Instantiate(whichcube[Random.Range(0,2)],points[Random.Range(0,4)]);
            cube.transform.localPosition = Vector3.zero;
            cube.transform.Rotate(transform.forward, 90 * Random.Range(0,4));
            timer -= beat;
        }
        timer += Time.deltaTime;
    }
}
