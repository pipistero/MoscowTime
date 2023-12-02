using System.Collections;
using System.Runtime.InteropServices;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class TimeController : MonoBehaviour
{
    [Header("Buttons")] 
    [SerializeField] private Button _getMoscowTimeButton;

    [DllImport("__Internal")]
    private static extern void Alert(string str);
    
    private void Start()
    {
        _getMoscowTimeButton.onClick.AddListener(() =>
        {
            StartCoroutine(GetMoscowTime());
        });
    }

    private IEnumerator GetMoscowTime()
    {
        using (UnityWebRequest webRequest = UnityWebRequest.Get("https://worldtimeapi.org/api/timezone/Europe/Moscow"))
        {
            yield return webRequest.SendWebRequest();

            if (webRequest.result == UnityWebRequest.Result.Success)
            {
                Debug.Log($"Received: {webRequest.downloadHandler.text}");
                Alert(webRequest.downloadHandler.text);
            }
            else
            {
                Debug.LogError("Error");
            }
        }
    }
}
