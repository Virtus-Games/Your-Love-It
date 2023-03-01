using System.Collections;
using TMPro;
using UnityEngine;

public class BuidingManager : Singleton<BuidingManager>
{
    [SerializeField] private TextMeshProUGUI TimerText;
    [SerializeField] private GameObject ArounUI;
    public Transform MoneyBox;

    [HideInInspector]
    public bool isCompledTimer = false;
    public float giveTime;
    private float _timeValue;
    public GameObject Button;
    public GameObject BeatifulBar;

    public GameObject MoneyPrefab;
    public Transform MoneyParent;

    [HideInInspector]
    public Supportes[] supportes;


    public void OnStartButton()
    {
        if (GameManager.Instance.Gamestate == GameManager.GAMESTATE.Ingame)
            Play();
    }


    private void Start() => _timeValue = giveTime;

    private void Update()
    {
        if (GameManager.Instance.Gamestate == GameManager.GAMESTATE.Ingame)
        {
            giveTime -= Time.deltaTime;

            TimerText.SetText(Mathf.Max(0, Mathf.Round(giveTime)).ToString());

            if (giveTime <= 0 && !isCompledTimer)
            {

            }
        }
    }

    public void TransformMoney(Vector2 pos)
    {
        StartCoroutine(InstantceAndTranslate(pos));
    }

    IEnumerator InstantceAndTranslate(Vector3 pos)
    {
        GameObject objx = Instantiate(MoneyPrefab, pos, Quaternion.identity, MoneyParent);

        while (Vector3.Distance(objx.transform.position, MoneyBox.transform.position) > 2f)
        {
            objx.transform.position = Vector3.Lerp(objx.transform.position, MoneyBox.transform.position, 2f * Time.deltaTime);
            yield return null;
        }

        UIManager.Instance.SetCoin(20);
        Destroy(objx);
    }

    private void Play()
    {

        BeatifulBar.SetActive(true);

        CameraController.Instance.StartCamera();

        TimerText.gameObject.SetActive(false);

        ArounUI.SetActive(false);

        supportes = FindObjectsOfType<Supportes>();

        Node[] nodes = FindObjectsOfType<Node>();

        foreach (Node node in nodes)
        {
            node.ClosePlaka();
            node.CloseRendImage(true);
        }

        isCompledTimer = true;
        Button.SetActive(false);
    }

    public void ResetValue()
    {
        BeatifulBar.SetActive(false);
        Button.gameObject.SetActive(true);
        isCompledTimer = false;
        SetSupportes(true);
        ArounUI.SetActive(true);
    }

    public void SetSupportes(bool isReset)
    {
        foreach (Supportes sup in supportes)
        {
            if (!isReset)
                sup.SetAnimatation();
            else
                sup.ResetAnimatation();
        }
    }
}
