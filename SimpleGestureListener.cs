using UnityEngine;
using System.Collections;
using System;


public class SimpleGestureListener : MonoBehaviour, KinectGestures.GestureListenerInterface
{
	// GUI Text to display the gesture messages.
	public GUIText GestureInfo;
	
	// private bool to track if progress message has been displayed
	private bool progressDisplayed;

    public void UserDetected(uint userId, int userIndex)
    {
        // as an example - detect these user specific gestures
        KinectManager manager = KinectManager.Instance;

//      manager.DetectGesture(userId, KinectGestures.Gestures.Jump);
//      manager.DetectGesture(userId, KinectGestures.Gestures.Squat);

      manager.DetectGesture(userId, KinectGestures.Gestures.Walk);
      manager.DetectGesture(userId, KinectGestures.Gestures.Circle);

        manager.DetectGesture(userId, KinectGestures.Gestures.LeanLeft);
        manager.DetectGesture(userId, KinectGestures.Gestures.LeanRight);

        manager.DetectGesture(userId, KinectGestures.Gestures.Run);

        //		manager.DetectGesture(userId, KinectGestures.Gestures.Push);
        //		manager.DetectGesture(userId, KinectGestures.Gestures.Pull);

        //		manager.DetectGesture(userId, KinectWrapper.Gestures.SwipeUp);
        //		manager.DetectGesture(userId, KinectWrapper.Gestures.SwipeDown);

        if (GestureInfo != null)
        {
            GestureInfo.GetComponent<GUIText>().text = "";
        }
    }

    public void UserLost(uint userId, int userIndex)
	{
		if(GestureInfo != null)
		{
			GestureInfo.GetComponent<GUIText>().text = string.Empty;
		}
	}

	public void GestureInProgress(uint userId, int userIndex, KinectGestures.Gestures gesture, 
	                              float progress, KinectWrapper.NuiSkeletonPositionIndex joint, Vector3 screenPos)
	{
		//GestureInfo.guiText.text = string.Format("{0} Progress: {1:F1}%", gesture, (progress * 100));
		if(gesture == KinectGestures.Gestures.Click && progress > 0.3f)
		{
			string sGestureText = string.Format ("{0} {1:F1}% complete", gesture, progress * 100);
			if(GestureInfo != null)
				GestureInfo.GetComponent<GUIText>().text = sGestureText;
			
			progressDisplayed = true;
		}
        else if((gesture == KinectGestures.Gestures.LeanLeft || gesture == KinectGestures.Gestures.LeanRight) && progress > 0.5f)
        {
            if(GestureInfo!=null)
            {
                string sGestureText = string.Format("{0} - {1:F0} degrees", gesture, screenPos.z);
                GestureInfo.text = sGestureText;

                progressDisplayed = true;
            }
        }
	}

    public bool GestureCompleted(uint userId, int userIndex, KinectGestures.Gestures gesture,
                                  KinectWrapper.NuiSkeletonPositionIndex joint, Vector3 screenPos)
    {
        if (progressDisplayed)
            return true;

        Console.Write("{0}", gesture);

        Console.Write("{0}", KinectGestures.Gestures.Walk);

        if(gesture == KinectGestures.Gestures.Walk)
        {
           string sGestureText = gesture + "¤w°»´ú!";
        }
        else
        {
           string sGestureText = "Ä~Äò¨«!";
        }
       

                                                        
        //if (gesture == KinectGestures.Gestures.Click)
          //  sGestureText += string.Format(" at ({0:F1}, {1:F1})", screenPos.x, screenPos.y);

        //if (GestureInfo != null)
          //  GestureInfo.GetComponent<GUIText>().text = sGestureText;
                      
        //progressDisplayed = false;

        return true;
        
    }

    public bool GestureCancelled (uint userId, int userIndex, KinectGestures.Gestures gesture, 
	                              KinectWrapper.NuiSkeletonPositionIndex joint)
	{
		if(progressDisplayed)
		{

            progressDisplayed = false;

			// clear the progress info
			if(GestureInfo != null)
				GestureInfo.GetComponent<GUIText>().text = String.Empty;
			
	    }
		
		return true;
	}
	
}
