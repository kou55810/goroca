using UnityEngine;

public class FindList: MonoBehaviour
{
    [SerializeField] RoomPanelContoroller roomPanelPrefab;
    [SerializeField] Transform roomListContent;

    void Start()
    {
        CreateRoomPanel();
    }
    public void CreateRoomPanel()
    {
        RoomPanelContoroller roomPanel = Instantiate(roomPanelPrefab, roomListContent);
        roomPanel.Init("test");
    }
}
