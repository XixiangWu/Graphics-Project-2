using UnityEngine;
using System.Collections;

public class LeftGuideLineController : MonoBehaviour
{

    private Vector3[] points_standard;
    public Vector3[] points1;

    public LineRenderer lineRendererLeft;
    private Color color1;
    private Color color2;
    private float minX;
    private float maxY;

    private GameObject mainCameraObject;

    // Use this for initialization
    void Start()
    {

        // init color
        color1 = Color.blue;
        color2 = Color.blue;


        // init main camera
        mainCameraObject = Camera.main.gameObject;

        points_standard = points1;
        points1 = points_standard;

        points1 = Curver.MakeSmoothCurve(points1, 10.0f);

        // draw first line
        lineRendererLeft = gameObject.GetComponent<LineRenderer>();
        lineRendererLeft.SetColors(color1, color2);
        lineRendererLeft.SetWidth(0.5f, 0.5f);
        lineRendererLeft.SetVertexCount(points1.Length);


        int counter = 0;
        foreach (var point in points1)
        {
            lineRendererLeft.SetPosition(counter, point - new Vector3(20,0,0));
            counter++;
        }

    }

    // Update is called once per frame
    void Update()
    {
        // get character's position
        points_standard[0] = mainCameraObject.transform.position;

        points1 = (Vector3[])points_standard.Clone();
        
        points1[0][0] -= 20;
        points1[0][1] -= 5;

        points1 = Curver.MakeSmoothCurve(points1, 10.0f);


        int counter = 0;
        foreach (var point in points1)
        {
            lineRendererLeft.SetPosition(counter, point - new Vector3(20, 0, 0));
            counter++;
        }
        counter = 0;

    }

    public void setPathList(Vector3[] pathList)
    {
        // init the path list
        points1 = (Vector3[])pathList.Clone();
    }
}
