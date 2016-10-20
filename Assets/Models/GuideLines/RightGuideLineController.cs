using UnityEngine;
using System.Collections;

public class RightGuideLineController : MonoBehaviour
{

    private Vector3[] points_standard;
    public Vector3[] points2;

    public LineRenderer lineRendererRight;
    private Color color1;
    private Color color2;
    private float minX;
    private float maxY;

    private GameObject mainCameraObject;

    public RightGuideLineController(Vector3[] pathList)
    {
        // init the path list
        points2 = (Vector3[])pathList.Clone();

    }

    // Use this for initialization
    void Start()
    {

        // init color
        color1 = Color.blue;
        color2 = Color.blue;


        // init main camera
        mainCameraObject = Camera.main.gameObject;

        points_standard = points2;
        points2 = points_standard;

        points2 = Curver.MakeSmoothCurve(points2, 10.0f);

        // draw first line
        lineRendererRight = gameObject.GetComponent<LineRenderer>();
        lineRendererRight.SetColors(color1, color2);
        lineRendererRight.SetWidth(0.5f, 0.5f);
        lineRendererRight.SetVertexCount(points2.Length);


        int counter = 0;
        foreach (var point in points2)
        {
            lineRendererRight.SetPosition(counter, point - new Vector3(-20, 0, 0));
            counter++;
        }

    }

    // Update is called once per frame
    void Update()
    {
        // get character's position
        points_standard[0] = mainCameraObject.transform.position;

        points2 = (Vector3[])points_standard.Clone();

        points2[0][0] += 20;
        points2[0][1] -= 5;

        points2 = Curver.MakeSmoothCurve(points2, 10.0f);


        int counter = 0;
        foreach (var point in points2)
        {
            lineRendererRight.SetPosition(counter, point - new Vector3(-20, 0, 0));
            counter++;
        }
    }


    public void setPathList(Vector3[] pathList)
    {
        // init the path list
        points2 = (Vector3[])pathList.Clone();

    }
}
