using Google.Protobuf.Protocol;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ChatUIManager : MonoBehaviour {

    public TMP_InputField ChatInputField;
    public Button ChatSendBtn;
    public bool IsChat;

    private void Start() {
        ChatSendBtn.onClick.AddListener(() => SendMessage());
    }

    public void SendMessage() {
        if (ChatInputField.text == string.Empty)
            return;

        C_Chat chatMessage = new C_Chat();
        chatMessage.Message = ChatInputField.text + " ";
        Managers.Network.Send(chatMessage);

        ChatInputField.text = string.Empty;
    }

    public void SetIsChat(bool isChat) {
        IsChat = isChat;
    }
}
