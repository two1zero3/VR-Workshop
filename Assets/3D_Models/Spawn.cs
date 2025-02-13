using UnityEngine;
using Meta.XR.MRUtilityKit;

public class RoomSpawner : MonoBehaviour
{
    public GameObject prefab;
    public float minRadius = 0.5f;
    public MRUKAnchor.SceneLabels Labels = ~(MRUKAnchor.SceneLabels)0;

    public MRUK.SurfaceType surfaceType = (MRUK.SurfaceType)~0;
    public int maxTryCount = 100;
    public int maxSpawnCount = 10;

    public LayerMask layerMask = 0;
    public Vector3 boxSize;

    public float distanceFromSurfaceForBoundsCheck = 0.1f;
    private void Start()
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
    public void StartSpawn(MRUKRoom room)
    {
        int skipped = 0;
        // find room mesh
        RoomMeshAnchor roomMesh = FindAnyObjectByType<RoomMeshAnchor>();
        roomMesh.tag = "MiningSurface";

        int objCount = 0;
        int tried = 0;
        int foundPos = 0;
        for (int i = 0; i < maxTryCount; i++)
        {
            tried++;
            if (room.GenerateRandomPositionOnSurface(surfaceType, minRadius, new LabelFilter(Labels), out var pos, out var normal))
            {
                foundPos++;
                Vector3 halfExtents = boxSize / 2;

                Vector3 center = pos + normal * (halfExtents.y + distanceFromSurfaceForBoundsCheck);
                Quaternion rotation = Quaternion.LookRotation(normal);


                // DebugExtension.DebugLocalCube(Matrix4x4.TRS(center, rotation, Vector3.one), halfExtents * 2, Color.red, Vector3.zero, 10);

                if (Physics.CheckBox(center, halfExtents, rotation, layerMask, QueryTriggerInteraction.Collide))
                {
                    skipped++;
                    continue;
                }
                GameObject obj = Instantiate(prefab, pos, Quaternion.LookRotation(normal), transform);
                objCount++;

                if (objCount >= maxSpawnCount)
                    break;
            }
        }

        //Debug.Log(tried + " " + foundPos + " " + skipped + " " + objCount);
    }
}