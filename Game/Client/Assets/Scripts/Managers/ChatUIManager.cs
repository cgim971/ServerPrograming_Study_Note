using Google.Protobuf.Protocol;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ChatUIManager : MonoBehaviour {

    public TMP_InputField ChatInputField;
    public TMP_InputField ReservationInputField;
    public Button ChatSendBtn;
    public bool IsChat;

    private void Start() {
        ChatSendBtn.onClick.AddListener(() => SendMessage());
    }

    public void SendMessage() {
        if (ChatInputField.text == string.Empty)
            return;

        SendMessage(ChatInputField.text);

        ChatInputField.text = string.Empty;
    }

    public void SendMessage(string message) {
        C_Chat chatMessage = new C_Chat();
        chatMessage.Message = message + " ";
        Managers.Network.Send(chatMessage);
    }

    public void ReservationMessage() {
        if (ReservationInputField.text == string.Empty)
            return;

        SendMessage(ReservationInputField.text);
    }

    public void SetIsChat(bool isChat) {
        IsChat = isChat;
    }
}
