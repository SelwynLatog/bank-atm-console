public static class CreateUserMessages
{
    public const string ENTER_FIRST_NAME = "Enter first name:";
    public const string ENTER_MIDDLE_NAME = "Enter middle name:";
    public const string ENTER_LAST_NAME = "Enter last name:";
    public const string CREATE_USERNAME = "Create your username";
    public const string CONFIRM_INFO = "Please check and confirm if your information is correct.";
    public const string CREATE_PIN = "Create your 6-digit pin:";
    public const string CONFIRM_PIN = "Re-enter your pin:";
    public const string ENTER_INITIAL_DEPOSIT = "Enter initial deposit:";
}

public static class LoginMessages
{
    public const string LOGIN_SUCCESSFUL = "Login Successful, Welcome,";
    public const string LOGIN_FAILED = "Invalid username or PIN.";
    public const string ATTEMPTS_REMAINING = "attempt(s) remaining.";
    public const string MAX_LOG_EXCEEDED = "Maximum login attempts exceeded. Program terminated.";
}

public static class BalanceLimits
{
    public const double MIN_INIT_DEPOSIT = 500.00;
    public const double MAX_INIT_DEPOSIT = 10000.00;
    public const double MIN_BALANCE = 100.00;
}

public static class TransactionLimits
{
    public const double MIN_WITHDRAWAL = 100.00;
    public const double MAX_WITHDRAWAL = 50000.00;
    public const double MIN_DEPOSIT = 100.00;
    public const double MAX_DEPOSIT = 100000.00;
}

public static class InfoLimits
{
    public const int MIN_FIRST_NAME = 2;
    public const int MAX_FIRST_NAME = 20;
    public const int MIN_MIDDLE_NAME = 2;
    public const int MAX_MIDDLE_NAME = 20;
    public const int MIN_LAST_NAME = 2;
    public const int MAX_LAST_NAME = 20;
    public const int MIN_USERNAME_LENGTH = 5;
    public const int MAX_USERNAME_LENGTH = 20;
    public const int PIN_LENGTH = 6;
    public const int MAX_PIN_ATTEMPTS = 3;
    public const int MAX_LOG_ATTEMPTS = 3;
}

public static class FieldNames
{
    public const string FIRST_NAME = "First Name";
    public const string MIDDLE_NAME = "Middle Name";
    public const string LAST_NAME = "Last Name";
    public const string USERNAME = "Username";
    public const string PIN = "PIN";
    public const string INITIAL_DEPOSIT = "Initial Deposit";
    public const string FULL_NAME = "Full Name";
}

public static class TransactionFields
{
    public const string WITHDRAW = "WITHDRAW";
    public const string DEPOSIT = "DEPOSIT";
    public const string WITHDRAWAL_AMOUNT = "Withdrawal Amount";
    public const string DEPOSIT_AMOUNT = "Deposit Amount";
}

public static class TransactionMessages
{
    public const string AMOUNT_POSITIVE = "Amount must be greater than zero.";
    public const string INSUFFICIENT_FUNDS = "Insufficient funds for this withdrawal.";
    public const string CURRENT_BALANCE = "Your current balance is: ";
    public const string BALANCE_TOO_LOW = "This withdrawal would bring your balance below the minimum required.";
    public const string MINIMUM_BALANCE = "Minimum balance required: ";
    public const string MAXIMUM_WITHDRAWAL = "Maximum you can withdraw: ";
    
    public const string WITHDRAWAL_TOO_SMALL = "Minimum withdrawal amount is ";
    public const string WITHDRAWAL_TOO_LARGE = "Maximum withdrawal amount is ";
    public const string DEPOSIT_TOO_SMALL = "Minimum deposit amount is ";
    public const string DEPOSIT_TOO_LARGE = "Maximum deposit amount is ";
    
    public const string WITHDRAWAL_SUCCESSFUL = "Withdrawal successful!";
    public const string DEPOSIT_SUCCESSFUL = "Deposit successful!";
    public const string NEW_BALANCE = "New balance: ";
    
    public const string TRANSACTION_CANCELLED = "Transaction cancelled.";
}

public static class ValidatorMessages
{
    public const string NO_EMPTY = " cannot be empty.";
    public const string INVALID_RANGE = " is invalid range.";
    public const string CHARACTERS_ALLOWED = " number of characters allowed.";
    public const string NO_LEAD_SPACE = " must not start with a space.";
    public const string NO_TRAIL_SPACE = " must not end with a space.";
    public const string NO_CONSECUTIVE_SPACE = " must not have consecutive spaces.";
    public const string NO_SPECIAL_CHAR = " must not contain special characters.";
    public const string INVALID_CHAR_PLACEMENT = " has invalid character placement.";
    public const string ONLY_LETTERS = " must only contain letters.";
    public const string RECORDED_MSG = " recorded successfully.";
    public const string CREATED_MSG = "Account created successfully.";
    public const string CREATED_CANCEL = "Account creation cancelled.";

    // Username messages
    public const string USERNAME_START_LETTER = " must start with a letter.";
    public const string USERNAME_INVALID_CHARS = " can only contain letters, numbers, and underscores.";
    public const string USERNAME_NO_CONSECUTIVE_UNDERSCORE = " cannot contain consecutive underscores.";
    public const string USERNAME_NO_TRAILING_UNDERSCORE = " cannot end with an underscore.";
    
    // PIN messages
    public const string PIN_ONLY_DIGITS = " must contain only digits.";
    public const string PIN_EXACT_LENGTH = " must be exactly ";
    public const string DIGITS = " digits.";
    public const string PIN_NO_SEQUENTIAL = " cannot be a sequential pattern (e.g., 123456, 654321).";
    public const string PIN_NO_REPEATING = " cannot have all the same digits (e.g., 111111, 000000).";
    public const string PIN_MISMATCH = "PINs do not match. ";
    public const string ATTEMPTS_REMAINING = " attempt(s) remaining.";
    public const string PIN_CONFIRMED = " confirmed successfully.";
    public const string PIN_CONFIRMATION_FAILED = "PIN confirmation failed after maximum attempts.";
    
    // Deposit messages
    public const string INVALID_AMOUNT = "Invalid amount. Please enter a valid number.";
    public const string AMOUNT_RANGE = " must be between ";
    public const string AND = " and ";
}

public static class PromptMessages
{
    public const string ENTER = "Enter ";
    public const string COLON = ": ";
    public const string CURRENCY_SYMBOL = "₱";
    public const string MASK_SYMBOL = "*";
    public const string PRESS_ANY_KEY = "Press Any Key To Continue";
}

public static class MenuHeaders
{
    public const string WITHDRAW = "WITHDRAW";
    public const string DEPOSIT = "DEPOSIT";
    public const string CHECK_BALANCE = "CHECK BALANCE";
    public const string TRANSACTION_HISTORY = "TRANSACTION HISTORY";
}

public static class DisplayMessages
{
    public const string ACCOUNT_HOLDER = "Account Holder: ";
    public const string USERNAME_DISPLAY = "Username: ";
    public const string CURRENT_BALANCE_DISPLAY = "Current Balance: ";
    public const string FEATURE_COMING_SOON = "This feature is coming soon!";
}

public static class ConfirmationMessages
{
    public const string CONFIRM_WITHDRAWAL = "Confirm Withdrawal:";
    public const string CONFIRM_DEPOSIT = "Confirm Deposit:";
    public const string AMOUNT = "Amount: ";
    public const string CURRENT_BALANCE = "Current Balance: ";
    public const string NEW_BALANCE = "New Balance: ";
}

public static class MenuOptions
{
    public static readonly string[] YES_NO = { "YES", "NO" };
    public static readonly string[] LOGIN_MENU = { "LOGIN", "REGISTER", "EXIT" };
    public static readonly string[] MAIN_MENU = { "WITHDRAW", "DEPOSIT", "CHECK BALANCE", "VIEW TRANSACTIONS", "LOGOUT" };
}

public static class TransactionTypes
{
    public const string WITHDRAWAL = "WITHDRAWAL";
    public const string DEPOSIT = "DEPOSIT";
    public const string INITIAL_DEPOSIT = "INITIAL DEPOSIT";
}

public static class TransactionHistoryMessages
{
    public const string NO_TRANSACTIONS = "No transactions found.";
    public const string TRANSACTION_COUNT = "Total Transactions: ";
    public const string TYPE = "Type: ";
    public const string AMOUNT = "Amount: ";
    public const string BALANCE_BEFORE = "Balance Before: ";
    public const string BALANCE_AFTER = "Balance After: ";
    public const string DATE_TIME = "Date & Time: ";
    public const string SEPARATOR = "-----------------------------------------------";
}

public static class Cursor_Configs
{
    public const int blinkTimerMS = 500;
}