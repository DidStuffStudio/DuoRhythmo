using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReloadSaveFileBrowser : MonoBehaviour
{
    public GameObject saveFileBrowserPrefab;
    public GameObject saveFileBrowser;


    public void ReinitializeBrowser()
    {
        Destroy(saveFileBrowser);
        GameObject newBrowser = Instantiate(saveFileBrowserPrefab, this.transform);
        newBrowser.GetComponent<RectTransform>().anchoredPosition = Vector2.zero;
        saveFileBrowser = newBrowser;
    }
}
