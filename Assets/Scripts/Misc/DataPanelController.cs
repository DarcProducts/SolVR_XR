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
    [SerializeField] TMP_FontAsset font;
    [SerializeField] Color fontColor;

    [Header("Data Panel")]
    [SerializeField] TMP_Text dataPanelText;

    [Header("Info Panel 1")]
    [TextArea(1, 10)] [SerializeField] string infoDataPanel1;

    [SerializeField] TMP_Text infoPanel1Text;

    [Header("Info Panel 2")]
    [TextArea(1, 10)] [SerializeField] string infoDataPanel2;

    [SerializeField] TMP_Text infoPanel2Text;

    [Header("Facts Panel")]
    [SerializeField] TMP_Text factsText;

    bool panelsActive = false;

    void OnEnable() => deactivatePanels.ValuedChangeTrue += DeactivatePanels;

    void OnDisable() => deactivatePanels.ValuedChangeTrue -= DeactivatePanels;

    void Start()
    {
        dataPanelText.font = font;
        dataPanelText.color = fontColor;
        factsText.font = font;
        factsText.color = fontColor;
        infoPanel1Text.font = font;
        infoPanel1Text.color = fontColor;
        infoPanel2Text.font = font;
        infoPanel2Text.color = fontColor;
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

    public void StreamData(string data) => InsertText(data, dataPanelText);

    public void StreamInfo() => InsertInfo();

    public void StreamFacts(string text) => InsertText(text, factsText);

    void InsertInfo()
    {
        InsertText(infoDataPanel1, infoPanel1Text);
        InsertText(infoDataPanel2, infoPanel2Text);
    }

    void InsertText(string text, TMP_Text textObj)
    {
        textObj.text = "";
        foreach (char c in text)
            textObj.text += c;
    }

    void DeactivatePanels()
    {
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