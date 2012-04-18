using UnityEngine;
using System.Collections;
using System;
using System.Text;
using System.Net;
using System.Net.Sockets;
using System.Threading;
 
public class UDPSend : MonoBehaviour{

    private static int localPort;
    public string IP="127.0.0.1";  // define in init
    public int port=8051;  // define in init
    IPEndPoint remoteEndPoint;
    UdpClient client;
    string strMessage="";


    public void Start(){
        init(); 
    }
	
	void FixedUpdate(){
		sendString("test");
		Debug.Log("Sending message");
	}
    

    // init
    public void init(){
        remoteEndPoint = new IPEndPoint(IPAddress.Parse(IP), port);
        client = new UdpClient();
    }

 

    // sendData
    private void sendString(string message){
        try {
                    byte[] data = Encoding.UTF8.GetBytes(message);
                    client.Send(data, data.Length, remoteEndPoint);
        }catch (Exception err){
        }

    }

    private void sendEndless(string testStr){
        do{
			sendString(testStr);
        }while(true);
    }
}