# Near WebGL API for Unity
Example scenes of how to do Near JavaScript API calls and Near RPC calls using the included but currently limited Near_API class.

<p>&nbsp;</p>

## Unity Project 

	Ø Unity version: 2021.3.21f1
	Ø Build platform: WebGL
	Ø Newtonsoft.JSON package used for RPC API service only
	Ø New Input sytem
	Ø Render Pipeline: URP

<p>&nbsp;</p>

## Installation


	1) Register a Near wallet on Testnet and/or Mainnet
	2) Fork this repo to local
	3) Open local folder from Unity Hub
	4) File / Build Settings - Set platform to WebGL
	5) Edit / Project Settings / Player / Other Settings / Active Input Handling - Input System Package (New) 
	6) Edit / Project Settings / Player / Resolution and Presentation - Select Near WebGL template
	7) File / Build And Run
	8) Select you network from the dropdown and click the Login button
	9) Sign in using your relevant Near wallet.


<p>&nbsp;</p>


## Classes

### Near_API class 
Class with a Near namespace that contains static methods that mainly calls JavaScript funtions in the JSLIB file (Plugin). Has static variables that stores the user account ID and login status.

<p>&nbsp;</p>

### WalletAuthenticate MonoBehavior Class
Used by the WalletLogin scene to calls the Near_API methods.

<p>&nbsp;</p>

### Near_RPC MonoBehavior Class
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

<p>&nbsp;</p>

## Other Resources

### NEAR
> Near JavaScript API documentation - https://docs.near.org/tools/near-api-js/quick-reference

> Near GitHub - https://github.com/orgs/near/repositories?type=all

> Near Client for Unity (Android / 3 year old repo) - https://github.com/near/near-api-unity

<p>&nbsp;</p>

### Morgan Page of Rogues
> Near API for Unity - GitHub https://github.com/morganpage/near-api-unity

> Adding blobkchain toUnity (video tutorial) https://youtu.be/vssV5ALChUM

> Near / Unity API Plugin (video tutorial) https://youtu.be/02_dk_gGePk
