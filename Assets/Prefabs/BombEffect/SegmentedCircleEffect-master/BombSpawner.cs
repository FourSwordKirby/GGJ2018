using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class BombSpawner : MonoBehaviour {

    public int layers;
    public int segments;
    public float borderPercentage;

    public float innerRadius;
    public float outerRadius;
    public float thickness;

    private int definition = 20;

    public Material glowMaterial;
    public Material floorMaterial;

    public BombField fieldPrefab;

    // Use this for initialization
    public void Spawn (Vector3 position)
    {
        float fullArc = 360.0f / segments;
        float renderedArc = fullArc * (1.0f - borderPercentage);
        float spacing = ((outerRadius - innerRadius)-thickness) / (float) layers;

        int randomSeed = Random.Range(0, 10);

        BombField field = Instantiate(fieldPrefab.gameObject).GetComponent<BombField>();

        for (int j = 0; j < layers; j++)
        {
            GameObject parent = new GameObject("BombRing" + j);
            parent.transform.position = position;

            //Instantiating glow and ring
            GameObject glow = new GameObject("Glow" + j);
            glow.transform.parent = parent.transform;
            glow.transform.localPosition = Vector3.zero;

            GameObject ring = new GameObject("Ring" + j);
            ring.transform.parent = parent.transform;
            ring.transform.localPosition = Vector3.zero;


            Random.InitState(randomSeed);
            for (int i = 0; i < segments; i++)
            {
                float randomValue = Random.Range(0.0f, 1.0f);// * (0.3f * (j+1));
                if (randomValue < 0.5)
                    randomValue = Random.Range(0.0f, 0.25f);
                else
                    randomValue = Random.Range(0.75f, 1.0f);
                CreateGlow(renderedArc, fullArc * i, innerRadius + j*spacing, 2.0f + randomValue * 10.0f);
                CreateRingSegment(renderedArc, fullArc * i, innerRadius + j * spacing, randomValue);
            }

            List<MeshFilter> meshFilters = GetComponentsInChildren<MeshFilter>().Where(x => x.gameObject.name.Contains("Panel")).ToList();
            CombineInstance[] combine = new CombineInstance[meshFilters.Count];
            for (int i = 0; i < meshFilters.Count; i++)
            {
                combine[i].mesh = meshFilters[i].sharedMesh;
                combine[i].transform = meshFilters[i].transform.localToWorldMatrix;
                meshFilters[i].gameObject.active = false;

                Destroy(meshFilters[i].gameObject);


                //meshFilters[i].sharedMesh;
                //combine[i].transform = meshFilters[i].transform.localToWorldMatrix;
                //meshFilters[i].gameObject.active = false;
            }
            MeshFilter glowMF = glow.AddComponent<MeshFilter>();
            glowMF.mesh.CombineMeshes(combine);
            glowMF.mesh.RecalculateNormals();

            MeshRenderer glowMR = glow.AddComponent<MeshRenderer>();
            glowMR.material = glowMaterial;
            RotatingLight light = glow.AddComponent<RotatingLight>();
            light.renderer = glowMR;


            meshFilters = GetComponentsInChildren<MeshFilter>().Where(x => x.gameObject.name.Contains("Arc")).ToList();
            combine = new CombineInstance[meshFilters.Count];
            for (int i = 0; i < meshFilters.Count; i++)
            {
                combine[i].mesh = meshFilters[i].sharedMesh;
                combine[i].transform = meshFilters[i].transform.localToWorldMatrix;
                meshFilters[i].gameObject.active = false;

                Destroy(meshFilters[i].gameObject);

                //meshFilters[i].sharedMesh;
                //combine[i].transform = meshFilters[i].transform.localToWorldMatrix;
                //meshFilters[i].gameObject.active = false;
            }
            MeshFilter ringMF = ring.AddComponent<MeshFilter>();
            ringMF.mesh.CombineMeshes(combine);
            ringMF.mesh.RecalculateNormals();

            MeshRenderer ringMR = ring.AddComponent<MeshRenderer>();
            ringMR.material = floorMaterial;

            field.fieldRings.Add(parent);
        }

        field.transform.position = this.transform.position;
        field.SpawnField();
    }


    public void CreateGlow(float angle, float offset, float radius, float height)
    {
        List<Vector3> vertexList = new List<Vector3>();
        Quaternion offsetQuaternion = Quaternion.Euler(0.0f, offset, 0.0f);
        vertexList.Add(offsetQuaternion * Vector3.forward);

        float totalDistance = 2 * thickness + angle / 360.0f * Mathf.PI * 2 * radius + angle / 360.0f * Mathf.PI * 2 * (radius + thickness);

        float currentProgress = 0;
        float deltaAngle = angle / (float)definition;
        float arcPercentage = deltaAngle / 360.0f;
        for (int i = 0; i < definition + 1; i++)
        {
            Quaternion quaternion = Quaternion.Euler(0.0f, deltaAngle, 0.0f);
            vertexList.Add(quaternion * vertexList[i]);

            GameObject panel1 = new GameObject("Panel1");
            panel1.transform.parent = this.transform;
            panel1.transform.localScale = Vector3.one;
            panel1.transform.position = Vector3.zero;
            var panelFilter1 = panel1.AddComponent<MeshFilter>();

            if (i == definition)
            {
                Vector3 v0 = vertexList[i] * radius;//Vector3.zero + i * Vector3.left;
                Vector3 v1 = vertexList[i] * (radius + thickness);//Vector3.left + i * Vector3.left;
                Vector3 v2 = Vector3.up * height + vertexList[i] * radius;//Vector3.up + i * Vector3.left;
                Vector3 v3 = Vector3.up * height + vertexList[i] * (radius + thickness);//Vector3.left + Vector3.up + i * Vector3.left;
                panelFilter1.mesh = CreateQuad(v0, v1, v2, v3, currentProgress, thickness/totalDistance);
                currentProgress += thickness / totalDistance;
            }
            else
            {
                Vector3 v0 = vertexList[i] * radius;//Vector3.zero + i * Vector3.left;
                Vector3 v1 = vertexList[i + 1] * radius;//Vector3.left + i * Vector3.left;
                Vector3 v2 = Vector3.up * height + vertexList[i] * radius;//Vector3.up + i * Vector3.left;
                Vector3 v3 = Vector3.up * height + vertexList[i + 1] * radius;//Vector3.left + Vector3.up + i * Vector3.left;
                panelFilter1.mesh = CreateQuad(v0, v1, v2, v3, currentProgress, (arcPercentage * (Mathf.PI * 2 * radius)) / totalDistance);
                currentProgress += (arcPercentage * (Mathf.PI * 2 * radius)) / totalDistance;
            }
        }

        for (int i = definition; i >= 0; i--)
        {
            Quaternion quaternion = Quaternion.Euler(0.0f, -deltaAngle, 0.0f);
            vertexList.Add(quaternion * vertexList[i]);

            GameObject panel2 = new GameObject("Panel2");
            panel2.transform.parent = this.transform;
            panel2.transform.localScale = Vector3.one;
            panel2.transform.position = Vector3.zero;
            var panelFilter1 = panel2.AddComponent<MeshFilter>();

            if (i == 0)
            {
                Vector3 v0 = vertexList[0] * (radius + thickness);//Vector3.left + i * Vector3.left;
                Vector3 v1 = vertexList[0] * radius;//Vector3.zero + i * Vector3.left;
                Vector3 v2 = Vector3.up * height + vertexList[0] * (radius + thickness);//Vector3.left + Vector3.up + i * Vector3.left;
                Vector3 v3 = Vector3.up * height + vertexList[0] * radius;//Vector3.up + i * Vector3.left;
                panelFilter1.mesh = CreateQuad(v0, v1, v2, v3, currentProgress, thickness / totalDistance);
                currentProgress += thickness / totalDistance;
            }
            else
            {
                Vector3 v0 = vertexList[i] * (radius + thickness);//Vector3.zero + i * Vector3.left;
                Vector3 v1 = vertexList[i - 1] * (radius + thickness);//Vector3.left + i * Vector3.left;
                Vector3 v2 = Vector3.up * height + vertexList[i] * (radius + thickness);//Vector3.up + i * Vector3.left;
                Vector3 v3 = Vector3.up * height + vertexList[i - 1] * (radius + thickness);//Vector3.left + Vector3.up + i * Vector3.left;
                panelFilter1.mesh = CreateQuad(v0, v1, v2, v3, currentProgress, (arcPercentage * (Mathf.PI * 2 * (radius + thickness))) / totalDistance);
                currentProgress += (arcPercentage * (Mathf.PI * 2.0f * (radius + thickness))) / totalDistance;
            }
        }

    }

    public Mesh CreateQuad(Vector3 v0, Vector3 v1, Vector3 v2, Vector3 v3, float progress, float delta)
    {
        Mesh mesh = new Mesh();

        Vector3[] vertices = new Vector3[4];
        vertices[0] = v0;
        vertices[1] = v1;
        vertices[2] = v2;
        vertices[3] = v3;

        int[] tri = new int[12];

        //  Lower left triangle.
        tri[0] = 0;
        tri[1] = 2;
        tri[2] = 1;
        //  Upper right triangle.   
        tri[3] = 2;
        tri[4] = 3;
        tri[5] = 1;

        //  Lower left triangle.
        tri[0] = 0;
        tri[1] = 2;
        tri[2] = 1;
        //  Upper right triangle.   
        tri[3] = 2;
        tri[4] = 3;
        tri[5] = 1;

        //  Lower left triangle.
        tri[6] = 0;
        tri[7] = 0;
        tri[8] = 0;
        //  Upper right triangle.   
        tri[9] = 0;
        tri[10] = 0;
        tri[11] = 0;

        Vector2[] uv = new Vector2[4];

        //Mesh
        uv[0] = new Vector2(progress, 0);
        uv[1] = new Vector2(progress+delta, 0);
        uv[2] = new Vector2(progress, 1);
        uv[3] = new Vector2(progress+delta, 1);

        mesh.vertices = vertices;
        mesh.triangles = tri;
        mesh.uv = uv;

        mesh.RecalculateNormals();

        return mesh;
    }

    //randomly assign uv or actually make seperate objects
    void CreateRingSegment(float angle, float offset, float radius, float randomValue)
    {
        GameObject arc = new GameObject("Arc");
        arc.transform.parent = this.transform;
        arc.transform.localScale = Vector3.one;
        arc.transform.position = Vector3.zero;
        MeshFilter arcFilter = arc.AddComponent<MeshFilter>();
        Mesh arcMesh = arcFilter.mesh;

        Vector3[] vertices = new Vector3[(definition + 1) * 2 * 2];
        int[] triangles = new int[definition * 6 * 2];

        Vector3[] topVertices = new Vector3[(definition + 1) * 2 * 2];

        Vector2[] uv = new Vector2[(definition + 1) * 2 * 2];
        int halfway = (definition + 1) * 2;

        for (int i = 0; i < definition + 1; i++)
        {

            float progress = (float)i / (float)definition;
            float deltaAngle = Mathf.Deg2Rad * (progress * angle + offset);
            float x = Mathf.Sin(deltaAngle);
            float z = Mathf.Cos(deltaAngle);

            vertices[i * 2] = vertices[i * 2 + halfway] = new Vector3(x, 0f, z) * (radius + thickness);
            vertices[i * 2 + 1] = vertices[i * 2 + 1 + halfway] = new Vector3(x, 0f, z) * radius;

            topVertices[i * 2] = topVertices[i * 2 + halfway] = new Vector3(x, 0f, z) * (radius + thickness) + Vector3.up;
            topVertices[i * 2 + 1] = topVertices[i * 2 + 1 + halfway] = new Vector3(x, 0f, z) * radius + Vector3.up;

            uv[i * 2] = uv[i * 2 + halfway] = new Vector2(randomValue, 0f);//new Vector2(progress, 0f);
            uv[i * 2 + 1] = uv[i * 2 + 1 + halfway] = new Vector2(randomValue, 0f);//new Vector2(progress, 1f);

            if (i != definition)
            {
                triangles[i * 12] = i * 2;
                triangles[i * 12 + 1] = triangles[i * 12 + 4] = (i + 1) * 2;
                triangles[i * 12 + 2] = triangles[i * 12 + 3] = i * 2 + 1;
                triangles[i * 12 + 5] = (i + 1) * 2 + 1; ;

                triangles[i * 12 + 6] = i * 2 + halfway;
                triangles[i * 12 + 7] = triangles[i * 12 + 3] = i * 2 + 1 + halfway;
                triangles[i * 12 + 8] = triangles[i * 12 + 4] = (i + 1) * 2 + halfway;
                triangles[i * 12 + 11] = (i + 1) * 2 + 1 + halfway;

                triangles[i * 12] = i * 2;
                triangles[i * 12 + 1] = triangles[i * 12 + 4] = (i + 1) * 2;
                triangles[i * 12 + 2] = triangles[i * 12 + 3] = i * 2 + 1;
                triangles[i * 12 + 5] = (i + 1) * 2 + 1; ;

                triangles[i * 12 + 6] = i * 2 + halfway;
                triangles[i * 12 + 7] = triangles[i * 12 + 3] = i * 2 + 1 + halfway;
                triangles[i * 12 + 8] = triangles[i * 12 + 4] = (i + 1) * 2 + halfway;
                triangles[i * 12 + 11] = (i + 1) * 2 + 1 + halfway;
            }
        }

        arcMesh.vertices = vertices;
        arcMesh.triangles = triangles;
        arcMesh.uv = uv;

        arcMesh.RecalculateNormals();
    }



    void CreateMesh()
    {
        MeshFilter filter = gameObject.AddComponent<MeshFilter>();
        Mesh mesh = filter.mesh;
        mesh.Clear();

        float height = 1f;
        int nbSides = 24;

        // Outter shell is at radius1 + radius2 / 2, inner shell at radius1 - radius2 / 2
        float bottomRadius1 = .5f;
        float bottomRadius2 = .15f;
        float topRadius1 = .5f;
        float topRadius2 = .15f;

        int nbVerticesCap = nbSides * 2 + 2;
        int nbVerticesSides = nbSides * 2 + 2;
        #region Vertices

        // bottom + top + sides
        Vector3[] vertices = new Vector3[nbVerticesCap * 2 + nbVerticesSides * 2];
        int vert = 0;
        float _2pi = Mathf.PI * 2f;

        // Bottom cap
        int sideCounter = 0;
        while (vert < nbVerticesCap)
        {
            sideCounter = sideCounter == nbSides ? 0 : sideCounter;

            float r1 = (float)(sideCounter++) / nbSides * _2pi;
            float cos = Mathf.Cos(r1);
            float sin = Mathf.Sin(r1);
            vertices[vert] = new Vector3(cos * (bottomRadius1 - bottomRadius2 * .5f), 0f, sin * (bottomRadius1 - bottomRadius2 * .5f));
            vertices[vert + 1] = new Vector3(cos * (bottomRadius1 + bottomRadius2 * .5f), 0f, sin * (bottomRadius1 + bottomRadius2 * .5f));
            vert += 2;
        }

        // Top cap
        sideCounter = 0;
        while (vert < nbVerticesCap * 2)
        {
            sideCounter = sideCounter == nbSides ? 0 : sideCounter;

            float r1 = (float)(sideCounter++) / nbSides * _2pi;
            float cos = Mathf.Cos(r1);
            float sin = Mathf.Sin(r1);
            vertices[vert] = new Vector3(cos * (topRadius1 - topRadius2 * .5f), height, sin * (topRadius1 - topRadius2 * .5f));
            vertices[vert + 1] = new Vector3(cos * (topRadius1 + topRadius2 * .5f), height, sin * (topRadius1 + topRadius2 * .5f));
            vert += 2;
        }

        // Sides (out)
        sideCounter = 0;
        while (vert < nbVerticesCap * 2 + nbVerticesSides)
        {
            sideCounter = sideCounter == nbSides ? 0 : sideCounter;

            float r1 = (float)(sideCounter++) / nbSides * _2pi;
            float cos = Mathf.Cos(r1);
            float sin = Mathf.Sin(r1);

            vertices[vert] = new Vector3(cos * (topRadius1 + topRadius2 * .5f), height, sin * (topRadius1 + topRadius2 * .5f));
            vertices[vert + 1] = new Vector3(cos * (bottomRadius1 + bottomRadius2 * .5f), 0, sin * (bottomRadius1 + bottomRadius2 * .5f));
            vert += 2;
        }

        // Sides (in)
        sideCounter = 0;
        while (vert < vertices.Length)
        {
            sideCounter = sideCounter == nbSides ? 0 : sideCounter;

            float r1 = (float)(sideCounter++) / nbSides * _2pi;
            float cos = Mathf.Cos(r1);
            float sin = Mathf.Sin(r1);

            vertices[vert] = new Vector3(cos * (topRadius1 - topRadius2 * .5f), height, sin * (topRadius1 - topRadius2 * .5f));
            vertices[vert + 1] = new Vector3(cos * (bottomRadius1 - bottomRadius2 * .5f), 0, sin * (bottomRadius1 - bottomRadius2 * .5f));
            vert += 2;
        }
        #endregion

        #region Normales

        // bottom + top + sides
        Vector3[] normales = new Vector3[vertices.Length];
        vert = 0;

        // Bottom cap
        while (vert < nbVerticesCap)
        {
            normales[vert++] = Vector3.down;
        }

        // Top cap
        while (vert < nbVerticesCap * 2)
        {
            normales[vert++] = Vector3.up;
        }

        // Sides (out)
        sideCounter = 0;
        while (vert < nbVerticesCap * 2 + nbVerticesSides)
        {
            sideCounter = sideCounter == nbSides ? 0 : sideCounter;

            float r1 = (float)(sideCounter++) / nbSides * _2pi;

            normales[vert] = new Vector3(Mathf.Cos(r1), 0f, Mathf.Sin(r1));
            normales[vert + 1] = normales[vert];
            vert += 2;
        }

        // Sides (in)
        sideCounter = 0;
        while (vert < vertices.Length)
        {
            sideCounter = sideCounter == nbSides ? 0 : sideCounter;

            float r1 = (float)(sideCounter++) / nbSides * _2pi;

            normales[vert] = -(new Vector3(Mathf.Cos(r1), 0f, Mathf.Sin(r1)));
            normales[vert + 1] = normales[vert];
            vert += 2;
        }
        #endregion

        #region UVs
        Vector2[] uvs = new Vector2[vertices.Length];

        vert = 0;
        // Bottom cap
        sideCounter = 0;
        while (vert < nbVerticesCap)
        {
            float t = (float)(sideCounter++) / nbSides;
            uvs[vert++] = new Vector2(0f, t);
            uvs[vert++] = new Vector2(1f, t);
        }

        // Top cap
        sideCounter = 0;
        while (vert < nbVerticesCap * 2)
        {
            float t = (float)(sideCounter++) / nbSides;
            uvs[vert++] = new Vector2(0f, t);
            uvs[vert++] = new Vector2(1f, t);
        }

        // Sides (out)
        sideCounter = 0;
        while (vert < nbVerticesCap * 2 + nbVerticesSides)
        {
            float t = (float)(sideCounter++) / nbSides;
            uvs[vert++] = new Vector2(t, 0f);
            uvs[vert++] = new Vector2(t, 1f);
        }

        // Sides (in)
        sideCounter = 0;
        while (vert < vertices.Length)
        {
            float t = (float)(sideCounter++) / nbSides;
            uvs[vert++] = new Vector2(t, 0f);
            uvs[vert++] = new Vector2(t, 1f);
        }
        #endregion

        #region Triangles
        int nbFace = nbSides * 4;
        int nbTriangles = nbFace * 2;
        int nbIndexes = nbTriangles * 3;
        int[] triangles = new int[nbIndexes];

        // Bottom cap
        int i = 0;
        sideCounter = 0;
        while (sideCounter < nbSides)
        {
            int current = sideCounter * 2;
            int next = sideCounter * 2 + 2;

            triangles[i++] = next + 1;
            triangles[i++] = next;
            triangles[i++] = current;

            triangles[i++] = current + 1;
            triangles[i++] = next + 1;
            triangles[i++] = current;

            sideCounter++;
        }

        // Top cap
        while (sideCounter < nbSides * 2)
        {
            int current = sideCounter * 2 + 2;
            int next = sideCounter * 2 + 4;

            triangles[i++] = current;
            triangles[i++] = next;
            triangles[i++] = next + 1;

            triangles[i++] = current;
            triangles[i++] = next + 1;
            triangles[i++] = current + 1;

            sideCounter++;
        }

        // Sides (out)
        while (sideCounter < nbSides * 3)
        {
            int current = sideCounter * 2 + 4;
            int next = sideCounter * 2 + 6;

            triangles[i++] = current;
            triangles[i++] = next;
            triangles[i++] = next + 1;

            triangles[i++] = current;
            triangles[i++] = next + 1;
            triangles[i++] = current + 1;

            sideCounter++;
        }


        // Sides (in)
        while (sideCounter < nbSides * 4)
        {
            int current = sideCounter * 2 + 6;
            int next = sideCounter * 2 + 8;

            triangles[i++] = next + 1;
            triangles[i++] = next;
            triangles[i++] = current;

            triangles[i++] = current + 1;
            triangles[i++] = next + 1;
            triangles[i++] = current;

            sideCounter++;
        }
        #endregion

        mesh.vertices = vertices;
        mesh.normals = normales;
        mesh.uv = uvs;
        mesh.triangles = triangles;

        mesh.RecalculateBounds();
        ;
    }
}
