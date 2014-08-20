using UnityEngine;
using UnityEditor;
using System.Collections;
 
public class MeshInfo : ScriptableObject
{   
    [MenuItem ("Custom/Show Mesh Info %#i")]
    public static void ShowCount()
    {
        int triangles = 0;
        int vertices = 0;
        int meshCount = 0;
        
        foreach (GameObject go in Selection.GetFiltered(typeof(GameObject), SelectionMode.TopLevel))
        {
            Component[] meshes = go.GetComponentsInChildren(typeof(MeshFilter));
 
            foreach (MeshFilter mesh in meshes)
            {
                if (mesh.sharedMesh)
                {
                    vertices += mesh.sharedMesh.vertexCount;
                    triangles += mesh.sharedMesh.triangles.Length / 3;
                    meshCount++;
                }
            }
        }
 
        EditorUtility.DisplayDialog("Vertex and Triangle Count", vertices
            + " vertices in selection.  " + triangles + " triangles in selection.  "
            + meshCount + " meshes in selection." + (meshCount > 0 ? ("  Average of " + vertices / meshCount
            + " vertices and " + triangles / meshCount + " triangles per mesh.") : ""), "OK", "");
    }
    
    [MenuItem ("Custom/Show Mesh Info %i", true)]
    public static bool ValidateShowCount()
    {
        return Selection.activeGameObject;
    }
    
}
