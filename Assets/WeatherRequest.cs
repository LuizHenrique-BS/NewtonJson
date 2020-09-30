using System.Collections;
using System.Collections.Generic;
using Newtonsoft.Json;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;
public class WeatherRequest : MonoBehaviour
{
    private string url = "http://192.168.31.31:8001/api/professores";
    Text temperature;

    public void GetWeather()
    {
        StartCoroutine(MakeWeatherRequest());
    }

    IEnumerator MakeWeatherRequest ()
    {
        UnityWebRequest request = UnityWebRequest.Get(url);
        yield return request.SendWebRequest();

        if (request.isNetworkError || request.isHttpError)
        {
            Debug.Log(request.error);
        } else
        {
            var weather = JsonConvert.DeserializeObject<WeatherResponse>(request.downloadHandler.text);

            temperature = GameObject.Find("temp").GetComponent<Text>();
            temperature.text = weather.main.temp.ToString();
        }
    }
}
