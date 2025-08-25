using Unity.Netcode;
using UnityEngine;

public class GameManager : NetworkBehaviour
{
    private static GameManager instance;
    [SerializeField] private Transform PlayerPrefab;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
   
    void Start()
    {
        
    }

    public override void OnNetworkSpawn()
    {
        print(NetworkManager.Singleton.LocalClientId);
        InstancePlayerRpc(NetworkManager.Singleton.LocalClientId);
    }
    [Rpc(SendTo.Server)]
    public void InstancePlayerRpc(ulong ownerID)
    {
        Transform player = Instantiate(PlayerPrefab);
        //player.GetComponent<NetworkObject>().Spawn(true);
        player.GetComponent<NetworkObject>().SpawnWithOwnership(NetworkManager.Singleton.LocalClientId, true);
    }
    void Update()
    {
        
    }

    public static GameManager Instance => instance;
}
