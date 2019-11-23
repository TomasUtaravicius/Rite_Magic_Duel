using Photon.Pun;
using System.Collections.Generic;
using UnityEngine;

public class SpellPool : MonoBehaviour, IPunPrefabPool
{
    /// <summary>Contains a GameObject per prefabId, to speed up instantiation.</summary>
    public readonly Dictionary<string, GameObject> ResourceCache = new Dictionary<string, GameObject>();

    /// <summary>Returns an inactive instance of a networked GameObject, to be used by PUN.</summary>
    /// <param name="prefabId">String identifier for the networked object.</param>
    /// <param name="position">Location of the new object.</param>
    /// <param name="rotation">Rotation of the new object.</param>
    /// <returns></returns>
    public GameObject Instantiate(string prefabId, Vector3 position, Quaternion rotation)
    {
        GameObject res = null;
        bool cached = ResourceCache.TryGetValue(prefabId, out res);
        if (!cached)
        {
            res = (GameObject)Resources.Load(prefabId, typeof(GameObject));
            if (res == null)
            {
                Debug.LogError("DefaultPool failed to load \"" + prefabId + "\" . Make sure it's in a \"Resources\" folder.");
            }
            else
            {
                this.ResourceCache.Add(prefabId, res);
            }
        }

        bool wasActive = res.activeSelf;
        if (wasActive) res.SetActive(false);

        GameObject instance = Instantiate(res, position, rotation);

        if (wasActive) res.SetActive(true);
        return instance;
    }

    /// <summary>Simply destroys a GameObject.</summary>
    /// <param name="gameObject">The GameObject to get rid of.</param>
    public void Destroy(GameObject gameObject)
    {
        gameObject.SetActive(false);
        ResourceCache.Add(gameObject.name, gameObject);
    }

    public void DestroyGroup(GameObject[] gameObjects)
    {
        for (int i = 0; i < gameObjects.Length; i++)
            Destroy(gameObjects[i]);
    }
}
