using System.Collections;
using TMPro;
using UnityEngine;

public class DataPanelController : MonoBehaviour
{
    [SerializeField] GameEvent PanelsActivated;
    [SerializeField] GameEvent PanelsDeactivated;
    [SerializeField] Transform playerTransform;
    [SerializeField] BoolVariable deactivatePanels;
    [SerializeField] IntVariable panelFontSize;
    [SerializeField] Vector2Variable minMaxPanelDistance;
    [SerializeField] BoolVariable zoomPanelsIn;
    [SerializeField] BoolVariable zoomPanelsOut;
    [SerializeField] Vector2Variable panelRotater;
    [SerializeField] FloatVariable panelRotateSpeed;
    [SerializeField] GameObject panelParent;
    [SerializeField] Panel[] panels = new Panel[4];
    [SerializeField] FontAssetVariable font;
    [Header("Data Panel")]
    [SerializeField] TMP_Text dataPanelText;
    [SerializeField] float dataTextDelay;
    [Header("Info Panel 1")]
    [TextArea(1, 10)] [SerializeField] string infoDataPanel1;
    [SerializeField] TMP_Text infoPanel1Text;
    [Header("Info Panel 2")]
    [TextArea(1, 10)] [SerializeField] string infoDataPanel2;
    [SerializeField] TMP_Text infoPanel2Text;
    [Header("Facts Panel")]
    [SerializeField] TMP_Text factsText;
    [SerializeField] float factTextDelay;
    bool panelsActive = false;

    void OnEnable()
    {
        deactivatePanels.ValuedChangeTrue += DeactivatePanels;
    }

    void OnDisable()
    {
        deactivatePanels.ValuedChangeTrue -= DeactivatePanels;
    }

    void Start()
    {
        dataPanelText.font = font.font;
        factsText.font = font.font;
        infoPanel1Text.font = font.font;
        infoPanel2Text.font = font.font;
        DeactivatePanels();
    }

    void Update()
    {
        if (panelsActive)
        {
            RotatePanels();
            ZoomPanels();
        }
    }

    public void StreamData(string data)
    {
        StartCoroutine(ShowText(data));
        InsertInfo();
    }

    IEnumerator ShowText(string text)
    {
        dataPanelText.text = "";
        foreach (char c in text)
        {
            dataPanelText.text += c;
            yield return new WaitForSeconds(dataTextDelay);
        }
    }

    public void StreamFacts(string text) => StartCoroutine(ShowFacts(text));

    IEnumerator ShowFacts(string text)
    {
        factsText.text = "";
        foreach (char c in text)
        {
            factsText.text += c;
            yield return new WaitForSeconds(factTextDelay);
        }
    }

    void InsertInfo()
    {
        StartCoroutine(ShowInfo(infoPanel1Text, infoDataPanel1));
        StartCoroutine(ShowInfo(infoPanel2Text, infoDataPanel2));
    }

    IEnumerator ShowInfo(TMP_Text text, string data)
    {
        text.text = "";
        foreach (char c in data)
        {
            text.text += c;
            yield return null;
        }
    }
    
    void DeactivatePanels()
    {
        StopAllCoroutines();
        for (int i = 0; i < panels.Length; i++)
            panels[i].gameObject.SetActive(false);
        panelsActive = false;
        PanelsDeactivated?.Invoke();
    }

    public void ActivatePanels()
    {
        for (int i = 0; i < panels.Length; i++)
            panels[i].gameObject.SetActive(true);
        panelsActive = true;
        PanelsActivated?.Invoke();
    }

    void RotatePanels()
    {
        if (panelRotater.Value.x > .1f)
            panelParent.transform.Rotate(Vector3.up * panelRotateSpeed.Value * Time.fixedDeltaTime, Space.Self);
        if (panelRotater.Value.x < -.1f)
            panelParent.transform.Rotate(Vector3.down * panelRotateSpeed.Value * Time.fixedDeltaTime, Space.Self);
    }

    void ZoomPanels()
    {
        if (zoomPanelsIn.Value)
        {
            for (int i = 0; i < panels.Length; i++)
                if (Vector3.Distance(panels[i].transform.position, playerTransform.position) > minMaxPanelDistance.Value.x)
                    panels[i].ZoomPanelIn();
        }
        else if (zoomPanelsOut.Value)
        {
            for (int i = 0; i < panels.Length; i++)
                if (Vector3.Distance(panels[i].transform.position, playerTransform.position) < minMaxPanelDistance.Value.y)
                    panels[i].ZoomPanelOut();
        }
    }
}

