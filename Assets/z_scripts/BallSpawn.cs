using UnityEngine;
using System.Collections;

public class BallSpawn : MonoBehaviour {

	public int BallsInPlay = 0;
	public GameObject ball;
	public GameObject currentBall;
	public GameObject AIRedPaddle;
	public GameObject AIBluePaddle;
	
	// Use this for initialization
	void Start () {
	
		
		Instantiate(ball);
		currentBall = ball;
		BallsInPlay ++;
		PlayerPrefs.SetInt("BallsInPlay", BallsInPlay);
		
	}
	public int ReturnBalls()
	{
		return BallsInPlay;
	}
	
	void BallLost()
	{
		BallsInPlay--;
		currentBall = null;
		if(PlayerPrefs.GetInt("AI")==1)
		{
		AIRedPaddle.SendMessage("AIBallLost",SendMessageOptions.RequireReceiver);
		AIBluePaddle.SendMessage("AIBallLost",SendMessageOptions.RequireReceiver);
		}
		PlayerPrefs.SetInt("BallsInPlay", BallsInPlay);
	}
	
	
	// Update is called once per frame
	void Update () {
		BallsInPlay = PlayerPrefs.GetInt("BallsInPlay");
		if(BallsInPlay == 0)
		{
		Instantiate(ball);
		currentBall = ball;
		
		BallsInPlay ++;
		PlayerPrefs.SetInt("BallsInPlay", BallsInPlay);
		}
		if(Vector3.Distance(currentBall.transform.position,transform.position) > 20)
		{
			Destroy(currentBall.gameObject);
			BallLost();
		}
			
		
	}
}
