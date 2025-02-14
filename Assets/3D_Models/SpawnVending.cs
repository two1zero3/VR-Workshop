using UnityEngine;
using Meta.XR.MRUtilityKit;

public class SpawnVending : MonoBehaviour
{

    public GameObject prefab;

    public MRUK.SurfaceType surfaceType = (MRUK.SurfaceType)~0;

    public int numb = 1;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if (MRUK.Instance)
        {
            MRUK.Instance.RegisterSceneLoadedCallback(() =>
            {
                StartSpawn(MRUK.Instance.GetCurrentRoom());

            });
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void StartSpawn (MRUKRoom room)
    {

        RoomMeshAnchor roomMesh = FindAnyObjectByType<RoomMeshAnchor>();
        roomMesh.tag = "MiningSurface";
        // find key wall
        var wallScale = Vector2.zero;
        var keyWall = room.GetKeyWall(out wallScale);
        var anchorBottomCenter = keyWall.transform.position - keyWall.transform.up * wallScale.y / 2;
        for (int i = 0; i < numb; i++)
        {
            //spawn the prefab at the key wall bottom center
            GameObject obj = Instantiate(prefab, keyWall.transform.position, keyWall.transform.rotation, transform);
            obj.transform.position = anchorBottomCenter;
        }
    }
}
