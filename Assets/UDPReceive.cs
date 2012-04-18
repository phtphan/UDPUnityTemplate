using UnityEngine;
using System.Collections;
using System;
using System.Text;
using System.Net;
using System.Net.Sockets;
using System.Threading;

public class UDPReceive : MonoBehaviour {
    Thread receiveThread; 
    UdpClient client; 
    public int port = 8051; // define > init
    public string lastReceivedUDPPacket="";
    public string allReceivedUDPPackets=""; // clean up this from time to time!

    // start from unity3d
    public void Start() {
        init(); 
    }

    // OnGUI
    void OnGUI(){
		Rect rectObj=new Rect(40,10,200,400);
		GUIStyle style = new GUIStyle();
		style.alignment = TextAnchor.UpperLeft;
		GUI.Box(rectObj,"# UDPReceive\n127.0.0.1 "+port+" #\n"
                    + "\nLast Packet: \n"+ lastReceivedUDPPacket
                    + "\n\nAll Messages: \n"+allReceivedUDPPackets
                ,style);
    }

    // init
    private void init(){
        receiveThread = new Thread(new ThreadStart(ReceiveData));
		receiveThread.IsBackground = true;
		receiveThread.Start();
    }

    // receive thread 
    private  void ReceiveData() {
        client = new UdpClient(port);
        while (true) {
            try {
                IPEndPoint anyIP = new IPEndPoint(IPAddress.Any, 0);
                byte[] data = client.Receive(ref anyIP);
                string text = Encoding.UTF8.GetString(data);
                lastReceivedUDPPacket=text;
                allReceivedUDPPackets=allReceivedUDPPackets+text;
        	}catch (Exception err) {
            }
        }
    }

    public string getLatestUDPPacket(){
        allReceivedUDPPackets="";
        return lastReceivedUDPPacket;
    }

}