/**
 * SerialCommUnity (Serial Communication for Unity)
 * Author: Daniel Wilches <dwilches@gmail.com>
 *
 * This work is released under the Creative Commons Attributions license.
 * https://creativecommons.org/licenses/by/2.0/
 */
/*
move = 0x0001;
left mouse down = 0x0002;
left mouse up = 0x0004;
right mouse down = 0x0008;
right mouse up = 0x0010;
middle mouse down = 0x0020;
middle mouse up = 0x0040;
private const int absolute = 0x8000;  

more info about mouse_event https://msdn.microsoft.com/en-us/library/windows/desktop/ms646260(v=vs.85).aspx
*/

using UnityEngine;
using System.Collections;
using System.Runtime.InteropServices;

/**
 * Sample for reading using polling by yourself. In case you are fond of that.
 */
public class SampleUserPolling_JustRead : MonoBehaviour
{
    public SerialController serialController;

	public string[] messages;

	public Vector3 oldRot;
	public Vector3 newRot;

	public float differenceX;
	public float differenceY;

	public bool clicking1;
	public bool clicking2;

	[DllImport("user32.dll")]
	static extern void mouse_event (int flag, int x, int y, int data, int extraInfo);

	public void Move(int x, int y){
		mouse_event (0x0001, x, y, 0, 0);
	}

	public void MouseDown(){
		mouse_event (0x0002, 0, 0, 0, 0);
	}

	public void MouseUp(){
		mouse_event (0x0004, 0, 0, 0, 0);
	}

	public void ButtonDown(){
		//Second button code goes here
	}

	public void ButtonUp(){
		//Second button code goes here
	}

    // Initialization
    void Start()
    {
        serialController = GameObject.Find("SerialController").GetComponent<SerialController>();
	}

    // Executed each frame
    void Update()
    {
        string message = serialController.ReadSerialMessage();

        if (message == null)
            return;

        // Check if the message is plain data or a connect/disconnect event.
        if (ReferenceEquals(message, SerialController.SERIAL_DEVICE_CONNECTED))
            Debug.Log("Connection established");
        else if (ReferenceEquals(message, SerialController.SERIAL_DEVICE_DISCONNECTED))
            Debug.Log("Connection attempt failed or disconnection detected");

		messages = message.Split (" " [0]);


		if (messages [3] == "0" && !clicking1) {
			clicking1 = true;
			MouseDown ();
		}
		if (messages [3] == "1" && clicking1) {
			clicking1 = false;
			MouseUp ();
		}

		if (messages [4] == "0" && !clicking2) {
			clicking2 = true;
			ButtonDown ();
		}
		if (messages [4] == "1" && clicking2) {
			clicking2 = false;
			ButtonUp ();
		}

		
		newRot = new Vector3 (newRot.x > 0 ? newRot.x : (360 + newRot.x), newRot.y > 0 ? newRot.y : (360 + newRot.y), newRot.z > 0 ? newRot.z : (360 + newRot.z));


		if (oldRot.x < 180 && newRot.x > 180)
			differenceX = 360 - differenceX;
		if (oldRot.y < 180 && newRot.y > 180)
			differenceY = 360 - differenceY;

		if (oldRot.x > 180 && newRot.x < 180)
			differenceX = differenceX + 360;
		if (oldRot.y > 180 && newRot.y < 180)
			differenceY = differenceY + 360;

		if (differenceX < 50 && differenceX > -50 && differenceY < 50 && differenceY > -50)
			Move ((int)(differenceY * 10), (int)(differenceX * 10));
    }
}
