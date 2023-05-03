using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;

public struct ReadyStatus : INetworkSerializable
{
    public int sceneID;
    public bool flag;

    public void NetworkSerialize<T>(BufferSerializer<T> serializer) where T : IReaderWriter
    {
        serializer.SerializeValue(ref sceneID);
        serializer.SerializeValue(ref flag);
    }
}
