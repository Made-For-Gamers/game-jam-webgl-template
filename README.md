# Near WebGL API for Unity
Example scenes of how to do Near JavaScript API calls and Near RPC calls using the included but currently limited Near_API class.

<p>&nbsp;</p>

## Unity Project 

	Ø Unity version: 2021.3.21f1
	Ø Build platform: WebGL
	Ø Json .Net for Unity (used for the RPC API calls only)
	Ø New Input sytem

<p>&nbsp;</p>

## Near_API class 
Class with a Near namespace that contains static methods that mainly calls JavaScript funtions in the JSLIB file (Plugin). Has static variables that stores the user account ID and login status.

<p>&nbsp;</p>

## WalletAuthenticate MonoBehavior Class
Used by the WalletLogin scene to calls the Near_API methods.

<p>&nbsp;</p>

## Near_RPC MonoBehavior Class
Example of posting json to the Near RPC API and returning a user's account details. Uses 2 other classes to handle the JSON fields.

	1) Post_ViewAccount class - JSON post fields
	2) ViewAccount class - Returned JSON fields
 
<p>&nbsp;</p>

## Scenes


### WalletLogin scene

Default scene with the following functions.

	1) Login
	2) Logout
	3) Check login status
	4) Get account ID
	5) Get account balance
	6) Navigate to the RPC scene



### RPC scene

Displays the user account details called from the RPC API.
