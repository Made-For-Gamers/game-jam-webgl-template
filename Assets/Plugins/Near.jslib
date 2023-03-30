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

    //Load Contract
    CallContract: async function (contractId, methodName, arg, accountId, networkId, isChange) {
        const accountID = UTF8ToString(accountId);
        const contractID = UTF8ToString(contractId);
        const method = UTF8ToString(methodName);
        const args = UTF8ToString(arg);
        let argument = {};

        const nearConnection = await connect(connectionConfig(UTF8ToString(networkId)));
        const account = await nearConnection.account(accountID);
        const contract = await new Contract(account, contractID, {
            viewMethods: isChange ? [] : [method],
            changeMethods: isChange ? [method] : [],
            sender: accountID,
        });

        try {
            argument = JSON.parse(args);
        } catch (error) {
            console.log("Argument Parse Error: ", error);
        }

        const theContract = contract[method];
        const data = await theContract(argument);
        var json = JSON.stringify(data, null, 2);
        SendMessage('Scripts', 'DisplayContract', json);
    },
});

