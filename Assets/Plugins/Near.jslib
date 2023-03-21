mergeInto(LibraryManager.library, {

AuthenticateWithNearWallet: async function (appKey, contractName, networkId, nodeUrl, walletUrl) 
{  

    var config = {
        networkId: UTF8ToString(networkId),
        nodeUrl: UTF8ToString(nodeUrl),
        walletUrl: UTF8ToString(walletUrl)
    };

    var nearConnection = await nearApi.connect(config);
    
    var walletConnection = new nearApi.WalletConnection(nearConnection, UTF8ToString(appKey));
    
    walletConnection.requestSignIn({contractId: ""}); 

},

RemoveUrlParams: function()
{
history.replaceState('data to be passed', 'Title of the page', '/');
},


});

