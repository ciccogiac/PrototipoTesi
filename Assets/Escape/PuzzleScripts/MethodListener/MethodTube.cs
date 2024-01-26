using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MethodTube : MonoBehaviour
{
    [SerializeField] Material material_default;
    [SerializeField] Material material_connected;
    [SerializeField] Material material_getter;
    [SerializeField] Material material_error;

    public MeshRenderer mesh;

    // Start is called before the first frame update
    /*
    void Start()
    {
        mesh = GetComponent<MeshRenderer>();
    }
    */

    private void OnValidate()
    {
        mesh = GetComponent<MeshRenderer>();
    }

    public void ChangeTubeColor(string color)
    {
        switch (color)
        {
            case "Connected":
                mesh.material = material_connected;
                break;

            case "Disconnect":
                mesh.material = material_default;
                break;

            case "Getter":
                mesh.material = material_getter;
                break;

            case "Error":
                mesh.material = material_error;
                break;

        }
    }


}
