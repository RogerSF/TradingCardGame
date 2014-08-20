using System;
using UnityEngine;
using System.Net.Sockets;
using System.Collections;
using System.Collections.Generic;
//Written by Michael Bethke
public class Opponent
{
	
	public bool connected = false;
	
	public string name;
	
	public List<GameCard> cards = new List<GameCard> ();
}


public class NetworkManager : MonoBehaviour
{
	
	DebugLog debugLog;
	DeckManager deckManager;
	
	internal enum ConnectionType { None, Hosting, Connecting, Connected, Playing }
	internal ConnectionType connectionType;
	
	internal bool hosting = false;
	
	internal bool info = false;
	internal string infoString = "";
	
	
	internal Opponent opponent = new Opponent ();
	
	
	internal bool options = false;
	

	void Start ()
	{
		
		debugLog = GameObject.FindGameObjectWithTag ( "DebugLog" ).GetComponent<DebugLog>();
		deckManager = GameObject.FindGameObjectWithTag ( "Manager" ).GetComponent<DeckManager> ();
		
		connectionType = ConnectionType.None;
	}


	public bool SetupHost ( string port )
	{
		
		debugLog.ReceiveMessage ( "\tSetting Up Server on " + port );
		
		hosting = true;
		connectionType = ConnectionType.Hosting;
		debugLog.ReceiveMessage ( "\tConnection Type Set to Hosting" );
		
		return true;
	}
		
		
	public bool ShutdownHost ()
	{
		
		opponent.name = null;
		opponent.cards.Clear ();
		
		info = false;
		infoString = null;
		
		hosting = false;
		connectionType = ConnectionType.None;
		
		debugLog.ReceiveMessage ( "\tConnection Type Set to None" );
		
		return true;
	}
	

	public void ReceiveConnection ( string receivedOpponentName )
	{
		
		if ( hosting == true )
		{
			
			if ( connectionType == ConnectionType.Hosting )
			{
				
				opponent.name = receivedOpponentName;
				opponent.cards = new List<GameCard> ();
				
				opponent.cards.Add ( deckManager.masterDeck.gameCards[0] );
				
				connectionType = ConnectionType.Connected;
				
				debugLog.ReceiveMessage ( "\nConnection Received" );
				debugLog.ReceiveMessage ( "\t" + opponent.name );
				
				debugLog.ReceiveMessage ( "\tConnection Type Set to Connected" );
			}
		}
	}
	
	
	public void DisconnectOpponent ()
	{
		
		debugLog.ReceiveMessage ( "\nOpponent Disconnected" );
		
		opponent.name = null;
		
		infoString = "Your opponent has disconnected.";
		info = true;
		
		hosting = true;
		connectionType = ConnectionType.Hosting;
		
		debugLog.ReceiveMessage ( "\tConnection Type Reset" );
	}
	
	
	public bool BootOpponent ()
	{
		
		debugLog.ReceiveMessage ( "\tSending Disconnect Instruct" );
		
		//Send Message Here
		
		return true;
	}
}
