using UnityEngine;

public class DataHolder : MonoBehaviour
{
    [SerializeField] ObjectData objectData;

    public string GetObjectData()
    {
        if (objectData != null)
            return objectData.GetData();
        return "";
    }
    public void SetObjectData(ObjectData data)
    {
        if (objectData != null)
            objectData = data;
    }
    public string GetObjectFacts()
    {
        if (objectData != null)
            return objectData.GetFacts();
        return "";
    }
}
