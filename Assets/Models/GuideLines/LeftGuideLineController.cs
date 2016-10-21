using UnityEngine;
using System.Collections;

public class LeftGuideLineController : MonoBehaviour
{

    private Vector3[] points_standard;
    public Vector3[] points;

    private LineRenderer lineRendererLeft;
    private Color color1;
    private Color color2;
    private float minX;
    private float maxY;

    private GameObject mainCameraObject;

    public LeftGuideLineController(Vector3[] pathList)
    {
        // init the path list
        points = (Vector3[])pathList.Clone();

    }

    // Use this for initialization
    void Start()
    {

        // init color
        color1 = new Color(28.0f / 255.0f, 171.0f / 255.0f, 25.0f / 255.0f, 1.0f);
        color2 = new Color(28.0f / 255.0f, 171.0f / 255.0f, 25.0f / 255.0f, 1.0f);

        // init main camera
        mainCameraObject = Camera.main.gameObject;

        points[0] = mainCameraObject.transform.position;
        points_standard = (Vector3[])points.Clone();

        points = (Vector3[])points_standard.Clone();

        points = Curver.MakeSmoothCurve(points, 20.0f);

        // draw first line
        lineRendererLeft = gameObject.GetComponent<LineRenderer>();
        lineRendererLeft.SetColors(color1, color2);
        lineRendererLeft.SetWidth(0.5f, 0.5f);
        lineRendererLeft.SetVertexCount(points.Length);


        int counter = 0;
        foreach (var point in points)
        {
            lineRendererLeft.SetPosition(counter, point - new Vector3(5, 0, 0));
            counter++;
        }
        

    }

    // Update is called once per frame
    void Update()
    {
        // get character's position
        points_standard[0] = mainCameraObject.transform.position;

        points = (Vector3[])points_standard.Clone();

        points[0][0] -= 20;
        points[0][1] -= 5;

        points = Curver.MakeSmoothCurve(points, 10.0f);
        lineRendererLeft.SetVertexCount(points.Length);

        int counter = 0;
        foreach (var point in points)
        {
            lineRendererLeft.SetPosition(counter, point - new Vector3(5, 0, 0));
            counter++;
        }
    }


    public void setPathList(Vector3[] pathList)
    {
        // init the path list
        points = (Vector3[])pathList.Clone();

    }


    public void deleteFirstPoint()
    {
        Vector3[] tempPoints;

        if (points_standard.Length-1 != 0) {
            tempPoints = new Vector3[points_standard.Length - 1];
        } else
        {
            tempPoints = new Vector3[points_standard.Length];
        }

        tempPoints[0] = points_standard[0];

        for (int i = 1; i < points_standard.Length - 1; i++)
        {
            tempPoints[i] = points_standard[i + 1];
        }

        points_standard = (Vector3[])tempPoints.Clone();
    }
}
