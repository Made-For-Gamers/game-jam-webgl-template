mergeInto(LibraryManager.library, {

    //Request to sign into Near wallet
    Login: async function (contractId, networkId) {
        const nearConnection = await connect(connectionConfig(UTF8ToString(networkId)));
        const walletConnection = new WalletConnection(nearConnection);
        walletConnection.requestSignIn(UTF8ToString(contractId));
    },

    //Logout of Near wallet
    Logout: async function (networkId) {
        const nearConnection = await connect(connectionConfig(UTF8ToString(networkId)));
        const walletConnection = new WalletConnection(nearConnection);
        walletConnection.signOut();
    },

    //Login status check
    LoginStatus: async function (networkId) {
        const nearConnection = await connect(connectionConfig(UTF8ToString(networkId)));
        const walletConnection = new WalletConnection(nearConnection);
        const status = walletConnection.isSignedIn();
        SendMessage('Scripts', 'ChangeLoginStatus', status ? 'true' : 'false');
    },

    //Account ID
    AccountId: async function (networkId) {
        const nearConnection = await connect(connectionConfig(UTF8ToString(networkId)));
        const walletConnection = new WalletConnection(nearConnection);
        const accountId = walletConnection.getAccountId();
        SendMessage('Scripts', 'UpdateAccountId', accountId);
    },

    //Account balance
    AccountBalance: async function (networkId, accountId) {
        const accountID = UTF8ToString(accountId);
        const nearConnection = await connect(connectionConfig(UTF8ToString(networkId)));
        const account = await nearConnection.account(accountID);
        const accountBalance = await account.getAccountBalance();
        SendMessage('Scripts', 'ChangeText', String(accountBalance.total));
    },

    //Call contract
    CallContract: async function (accountId) {
        const accountID = UTF8ToString(accountId);
        const contract = new Contract(
            accountID,
            "You_Contract_Name",
            {
                viewMethods: ["You_Contract_Method"],
            }
        );
        const response = await contract.You_Contract_Method();
        const message = JSON.stringify(response);
        SendMessage('Scripts', 'ChangeText', String(message));
    },








    
});

