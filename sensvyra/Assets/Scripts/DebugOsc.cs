using OscJack;
using UnityEngine;

public class DebugOsc : MonoBehaviour
{
    OscServer server;

    void OnEnable()
    {
        // Must match the port your Node app is sending to
        server = new OscServer(9000);

        // Log incoming float messages
        server.MessageDispatcher.AddCallback("/calm", (address, data) =>
            Debug.Log("CALM " + data.GetElementAsFloat(0))
        );

        server.MessageDispatcher.AddCallback("/focus", (address, data) =>
            Debug.Log("FOCUS " + data.GetElementAsFloat(0))
        );
    }

    void OnDisable()
    {
        server?.Dispose();
    }
}
