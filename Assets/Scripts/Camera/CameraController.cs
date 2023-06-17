using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class CameraController : MonoBehaviour
{
    [SerializeField] Tilemap tilemap;
    Camera camera;
    // Start is called before the first frame update
    private void Start()
    {
        camera = GetComponent<Camera>();
    }

    // Update is called once per frame
    private void Update()
    {
        var (center,size) = CalculateSize();
        camera.transform.position = center;
        camera.orthographicSize = size;
    }

    private (Vector3 center, float size) CalculateSize() {
        Bounds b = new Bounds();
        tilemap.CompressBounds();
        b = tilemap.localBounds;
        b.Expand(-1);

        float vertical = b.size.y;
        float horizontal = b.size.x * camera.pixelHeight / camera.pixelWidth;

        float size = Mathf.Max(horizontal, vertical) * 0.5f;
        Vector3 center = b.center + new Vector3(0,0, -10);

        return (center, size);
    }
}
