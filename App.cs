//App.cs
using System;
using System.Collections.Generic;
using System.Text;

static class App
{
    static List<User> userlist = new List<User>();
    private static bool isRunning = true;

    public static void Start()
    {
        ASCII.displayLogo();
        Console.WriteLine(PromptMessages.PRESS_ANY_KEY);
        Console.ReadKey();
        while (isRunning)
        {
            Login();
        }
    }

    public static void Register()
    {
        string fNameInp = GetFirstName();
        string mNameInp = GetMiddleName();
        string lNameInp = GetLastName();
        string userInp = GetUsername();
        int pinInp = GetAndConfirmPin();
        double initBalance = GetInitialDeposit();

        string confirmationInfo = BuildConfirmationMessage(fNameInp, mNameInp, lNameInp, userInp, initBalance);
        bool confirmed = MenuHelper.ShowConfirmation(confirmationInfo);

        if (confirmed)
        {
            User newUser = new User(fNameInp, mNameInp, lNameInp, userInp, pinInp, initBalance);
            userlist.Add(newUser);
            Console.WriteLine();
            Console.WriteLine(ValidatorMessages.CREATED_MSG);
        }
        else
        {
            Console.WriteLine();
            Console.WriteLine(ValidatorMessages.CREATED_CANCEL);
        }
    }

    private static string GetFirstName()
    {
        return Validators.GetValidName(
            FieldNames.FIRST_NAME,
            InfoLimits.MIN_FIRST_NAME,
            InfoLimits.MAX_FIRST_NAME,
            allowSpecialChar: false
        );
    }

    private static string GetMiddleName()
    {
        return Validators.GetValidName(
            FieldNames.MIDDLE_NAME,
            InfoLimits.MIN_MIDDLE_NAME,
            InfoLimits.MAX_MIDDLE_NAME,
            allowSpecialChar: false,
            allowEmpty: true
        );
    }

    private static string GetLastName()
    {
        return Validators.GetValidName(
            FieldNames.LAST_NAME,
            InfoLimits.MIN_LAST_NAME,
            InfoLimits.MAX_LAST_NAME,
            allowSpecialChar: true
        );
    }

    private static string GetUsername()
    {
        Console.WriteLine(CreateUserMessages.CREATE_USERNAME);
        Console.WriteLine();
        return Validators.GetValidUsername();
    }

    private static int GetAndConfirmPin()
    {
        Console.WriteLine(CreateUserMessages.CREATE_PIN);
        int pinInp = Validators.GetValidPin();

        Console.WriteLine(CreateUserMessages.CONFIRM_PIN);
        return Validators.ConfirmPin(pinInp);
    }

    private static double GetInitialDeposit()
    {
        Console.WriteLine(CreateUserMessages.ENTER_INITIAL_DEPOSIT);
        return Validators.GetValidInitialDeposit();
    }

    private static string BuildConfirmationMessage(string fName, string mName, string lName, string username, double deposit)
    {
        string fullName = fName + " " + (string.IsNullOrEmpty(mName) ? "" : mName + " ") + lName;

        return CreateUserMessages.CONFIRM_INFO + "\n" +
               FieldNames.FULL_NAME + PromptMessages.COLON + fullName + "\n" +
               FieldNames.USERNAME + PromptMessages.COLON + username + "\n" +
               FieldNames.PIN + PromptMessages.COLON + new string('*', InfoLimits.PIN_LENGTH) + "\n" +
               FieldNames.INITIAL_DEPOSIT + PromptMessages.COLON +
               PromptMessages.CURRENCY_SYMBOL + deposit.ToString("N2");
    }

    public static void Login()
    {
        int choice = MenuHelper.ShowLogin(ASCII.welcomeMsg);
        switch (choice)
        {
            case 0: AttemptLogin(); break;
            case 1: Register(); break;
            case 2: isRunning = false; break;
        }
    }

    public static void AttemptLogin()
    {
        string userLog;
        int pinLog;
        int attempts = 0;

        while (attempts < InfoLimits.MAX_LOG_ATTEMPTS)
        {
            Console.Clear();
            Console.WriteLine(ASCII.welcomeMsg);
            Console.WriteLine();
            
            userLog = Validators.GetUsernameLogin();
            pinLog = Validators.GetPinLogin();

            User foundUser = null;
            for (int i = 0; i < userlist.Count; i++)
            {
                if (userlist[i].getUsername() == userLog && userlist[i].getPin() == pinLog)
                {
                    foundUser = userlist[i];
                    break;
                }
            }

            if (foundUser != null)
            {
                Console.WriteLine();
                Console.WriteLine(LoginMessages.LOGIN_SUCCESSFUL + " " + foundUser.getFirstName() + "!");
                Console.WriteLine(PromptMessages.PRESS_ANY_KEY);
                Console.ReadKey();
                MainMenu(foundUser);
                return;
            }
            
            attempts++;
            int remaining = InfoLimits.MAX_LOG_ATTEMPTS - attempts;
            
            Console.WriteLine();
            Console.WriteLine(LoginMessages.LOGIN_FAILED);
            
            if (remaining > 0)
            {
                Console.WriteLine(remaining + " " + LoginMessages.ATTEMPTS_REMAINING);
                Console.WriteLine(PromptMessages.PRESS_ANY_KEY);
                Console.ReadKey();
            }
        }
        
        Console.Clear();
        Console.WriteLine(LoginMessages.MAX_LOG_EXCEEDED);
        Console.WriteLine(PromptMessages.PRESS_ANY_KEY);
        Console.ReadKey();
        Environment.Exit(0);
    }

    public static void MainMenu(User user)
    {
        bool inMainMenu = true;
        
        while (inMainMenu)
        {
            int choice = MenuHelper.ShowMainMenu(ASCII.logo);
            switch (choice)
            {
                case 0: Withdraw(user); break;
                case 1: Deposit(user); break;
                case 2: CheckBalance(user); break;
                case 3: ViewTransactions(user); break;
                case 4: inMainMenu = false; break;
            }
        }
    }

    public static void Withdraw(User user)
    {
        Console.Clear();
        Console.WriteLine(ASCII.logo);
        Console.WriteLine();
        Console.WriteLine(MenuHeaders.WITHDRAW);
        Console.WriteLine();
        Console.WriteLine(TransactionMessages.CURRENT_BALANCE + PromptMessages.CURRENCY_SYMBOL + user.getBalance().ToString("N2"));
        Console.WriteLine();

        double amount = TransactionValidators.GetValidWithdrawal(user);
        
        string confirmationInfo = BuildWithdrawalConfirmation(amount, user.getBalance());
        bool confirmed = MenuHelper.ShowConfirmation(confirmationInfo);

        if (confirmed)
        {
            user.Withdraw(amount);
            Console.WriteLine();
            Console.WriteLine(TransactionMessages.WITHDRAWAL_SUCCESSFUL);
            Console.WriteLine(TransactionMessages.NEW_BALANCE + PromptMessages.CURRENCY_SYMBOL + user.getBalance().ToString("N2"));
        }
        else
        {
            Console.WriteLine();
            Console.WriteLine(TransactionMessages.TRANSACTION_CANCELLED);
        }
        
        Console.WriteLine();
        Console.WriteLine(PromptMessages.PRESS_ANY_KEY);
        Console.ReadKey();
    }

    private static string BuildWithdrawalConfirmation(double amount, double currentBalance)
    {
        double newBalance = currentBalance - amount;
        
        return ConfirmationMessages.CONFIRM_WITHDRAWAL + "\n" +
               ConfirmationMessages.AMOUNT + PromptMessages.CURRENCY_SYMBOL + amount.ToString("N2") + "\n" +
               ConfirmationMessages.CURRENT_BALANCE + PromptMessages.CURRENCY_SYMBOL + currentBalance.ToString("N2") + "\n" +
               ConfirmationMessages.NEW_BALANCE + PromptMessages.CURRENCY_SYMBOL + newBalance.ToString("N2");
    }

    public static void Deposit(User user)
    {
        Console.Clear();
        Console.WriteLine(ASCII.logo);
        Console.WriteLine();
        Console.WriteLine(MenuHeaders.DEPOSIT);
        Console.WriteLine();
        Console.WriteLine(TransactionMessages.CURRENT_BALANCE + PromptMessages.CURRENCY_SYMBOL + user.getBalance().ToString("N2"));
        Console.WriteLine();
        
        double amount = TransactionValidators.GetValidDeposit();
        
        string confirmationInfo = BuildDepositConfirmation(amount, user.getBalance());
        bool confirmed = MenuHelper.ShowConfirmation(confirmationInfo);
        
        if (confirmed)
        {
            user.Deposit(amount);
            Console.WriteLine();
            Console.WriteLine(TransactionMessages.DEPOSIT_SUCCESSFUL);
            Console.WriteLine(TransactionMessages.NEW_BALANCE + PromptMessages.CURRENCY_SYMBOL + user.getBalance().ToString("N2"));
        }
        else
        {
            Console.WriteLine();
            Console.WriteLine(TransactionMessages.TRANSACTION_CANCELLED);
        }
        
        Console.WriteLine();
        Console.WriteLine(PromptMessages.PRESS_ANY_KEY);
        Console.ReadKey();
    }

    private static string BuildDepositConfirmation(double amount, double currentBalance)
    {
        double newBalance = currentBalance + amount;
        
        return ConfirmationMessages.CONFIRM_DEPOSIT + "\n" +
               ConfirmationMessages.AMOUNT + PromptMessages.CURRENCY_SYMBOL + amount.ToString("N2") + "\n" +
               ConfirmationMessages.CURRENT_BALANCE + PromptMessages.CURRENCY_SYMBOL + currentBalance.ToString("N2") + "\n" +
               ConfirmationMessages.NEW_BALANCE + PromptMessages.CURRENCY_SYMBOL + newBalance.ToString("N2");
    }

    public static void CheckBalance(User user)
    {
        Console.Clear();
        Console.WriteLine(ASCII.logo);
        Console.WriteLine();
        Console.WriteLine(MenuHeaders.CHECK_BALANCE);
        Console.WriteLine();
        Console.WriteLine(DisplayMessages.ACCOUNT_HOLDER + user.getFirstName() + " " + user.getLastName());
        Console.WriteLine(DisplayMessages.USERNAME_DISPLAY + user.getUsername());
        Console.WriteLine();
        Console.WriteLine(DisplayMessages.CURRENT_BALANCE_DISPLAY + PromptMessages.CURRENCY_SYMBOL + user.getBalance().ToString("N2"));
        Console.WriteLine();
        Console.WriteLine(PromptMessages.PRESS_ANY_KEY);
        Console.ReadKey();
    }

    public static void ViewTransactions(User user)
    {
        Console.Clear();
        Console.WriteLine(ASCII.logo);
        Console.WriteLine();
        Console.WriteLine(MenuHeaders.TRANSACTION_HISTORY);
        Console.WriteLine();

        List<Transaction> transactions = user.getTransactionHistory();

        if (transactions.Count == 0)
        {
            Console.WriteLine(TransactionHistoryMessages.NO_TRANSACTIONS);
        }
        else
        {
            Console.WriteLine(TransactionHistoryMessages.TRANSACTION_COUNT + transactions.Count);
            Console.WriteLine();

            for (int i = transactions.Count - 1; i >= 0; i--)
            {
                Transaction t = transactions[i];
                
                Console.WriteLine(TransactionHistoryMessages.SEPARATOR);
                Console.WriteLine(TransactionHistoryMessages.TYPE + t.GetType());
                Console.WriteLine(TransactionHistoryMessages.AMOUNT + PromptMessages.CURRENCY_SYMBOL + t.GetAmount().ToString("N2"));
                Console.WriteLine(TransactionHistoryMessages.BALANCE_BEFORE + PromptMessages.CURRENCY_SYMBOL + t.GetBalanceBefore().ToString("N2"));
                Console.WriteLine(TransactionHistoryMessages.BALANCE_AFTER + PromptMessages.CURRENCY_SYMBOL + t.GetBalanceAfter().ToString("N2"));
                Console.WriteLine(TransactionHistoryMessages.DATE_TIME + t.GetFormattedTimestamp());
                Console.WriteLine(TransactionHistoryMessages.SEPARATOR);
                Console.WriteLine();
            }
        }

        Console.WriteLine(PromptMessages.PRESS_ANY_KEY);
        Console.ReadKey();
    }
}