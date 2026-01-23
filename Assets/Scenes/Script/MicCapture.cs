using UnityEngine;

public class MicCapture : MonoBehaviour
{
    public string deviceName;
    public float rms;

    AudioClip micClip;
    const int sampleLength = 256;

    void Start()
    {
        if (Microphone.devices.Length > 0)
        {
            deviceName = Microphone.devices[0];
            micClip = Microphone.Start(deviceName, true, 1, AudioSettings.outputSampleRate);
        }
        else
        {
            Debug.LogError("マイクが見つかりません");
        }
    }

    void Update()
    {
        if (micClip == null) return;

        float[] samples = new float[sampleLength];
        int micPos = Microphone.GetPosition(deviceName) - sampleLength;
        if (micPos < 0) return;

        micClip.GetData(samples, micPos);

        float sum = 0f;
        for (int i = 0; i < sampleLength; i++)
        {
            sum += samples[i] * samples[i];
        }

        rms = Mathf.Sqrt(sum / sampleLength);
    }
}
