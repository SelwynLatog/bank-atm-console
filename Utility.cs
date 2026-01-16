using System;
using System.Linq;

public static class Validators
{
    public static string GetValidName(
        string fieldName,
        int minChar,
        int maxChar,
        bool allowSpecialChar,
        bool allowEmpty = false
    )
    {
        while (true)
        {
            Console.Write(PromptMessages.ENTER + fieldName + PromptMessages.COLON);
            string input = Console.ReadLine();

            if (allowEmpty && string.IsNullOrWhiteSpace(input))
                return string.Empty;

            // EMPTY CHECK
            if (string.IsNullOrWhiteSpace(input))
            {
                Console.WriteLine(fieldName + ValidatorMessages.NO_EMPTY);
                continue;
            }

            input = input.Trim();

            // LENGTH CHECK
            if (input.Length < minChar || input.Length > maxChar)
            {
                Console.WriteLine(fieldName + ValidatorMessages.INVALID_RANGE + " " + minChar + " - " + maxChar + ValidatorMessages.CHARACTERS_ALLOWED);
                continue;
            }

            // Validate using helper method
            var validationResult = ValidateNameFormat(input, fieldName, allowSpecialChar);
            if (!validationResult.IsValid)
            {
                Console.WriteLine(validationResult.ErrorMessage);
                continue;
            }

            // CASE FORMATTING
            input = FormatNameCase(input);
            Console.WriteLine(fieldName + ValidatorMessages.RECORDED_MSG);
            return input;
        }
    }

    private static (bool IsValid, string ErrorMessage) ValidateNameFormat(
        string input,
        string fieldName,
        bool allowSpecialChar)
    {
        for (int i = 0; i < input.Length; i++)
        {
            char c = input[i];

            // SPACE RULES
            if (c == ' ')
            {
                if (i == 0)
                    return (false, fieldName + ValidatorMessages.NO_LEAD_SPACE);

                if (i == input.Length - 1)
                    return (false, fieldName + ValidatorMessages.NO_TRAIL_SPACE);

                if (input[i - 1] == ' ')
                    return (false, fieldName + ValidatorMessages.NO_CONSECUTIVE_SPACE);

                continue;
            }

            // SPECIAL CHAR RULES (' and -)
            if (c == '\'' || c == '-')
            {
                if (!allowSpecialChar)
                    return (false, fieldName + ValidatorMessages.NO_SPECIAL_CHAR);

                if (i == 0 || i == input.Length - 1 || input[i - 1] == c)
                    return (false, fieldName + ValidatorMessages.INVALID_CHAR_PLACEMENT);

                continue;
            }

            // LETTERS ONLY
            if (!char.IsLetter(c))
            {
                return (false, fieldName + ValidatorMessages.ONLY_LETTERS);
            }
        }

        return (true, string.Empty);
    }

    private static string FormatNameCase(string input)
    {
        char[] chars = input.ToLower().ToCharArray();
        bool newWord = true;

        for (int i = 0; i < chars.Length; i++)
        {
            if (chars[i] == ' ' || chars[i] == '\'' || chars[i] == '-')
            {
                newWord = true;
                continue;
            }

            if (newWord && char.IsLetter(chars[i]))
            {
                chars[i] = char.ToUpper(chars[i]);
                newWord = false;
            }
        }

        return new string(chars);
    }

    public static string GetValidUsername()
    {
        while (true)
        {
            Console.Write(PromptMessages.ENTER + FieldNames.USERNAME + PromptMessages.COLON);
            string input = Console.ReadLine();

            if (string.IsNullOrWhiteSpace(input))
            {
                Console.WriteLine(FieldNames.USERNAME + ValidatorMessages.NO_EMPTY);
                continue;
            }

            input = input.Trim();

            // LENGTH CHECK
            if (input.Length < InfoLimits.MIN_USERNAME_LENGTH || input.Length > InfoLimits.MAX_USERNAME_LENGTH)
            {
                Console.WriteLine(FieldNames.USERNAME + ValidatorMessages.INVALID_RANGE + " " + InfoLimits.MIN_USERNAME_LENGTH + " - " + InfoLimits.MAX_USERNAME_LENGTH + ValidatorMessages.CHARACTERS_ALLOWED);
                continue;
            }

            // Must start with a letter
            if (!char.IsLetter(input[0]))
            {
                Console.WriteLine(FieldNames.USERNAME + ValidatorMessages.USERNAME_START_LETTER);
                continue;
            }

            // Can only contain letters, numbers, and underscores
            bool isValid = true;
            foreach (char c in input)
            {
                if (!char.IsLetterOrDigit(c) && c != '_')
                {
                    Console.WriteLine(FieldNames.USERNAME + ValidatorMessages.USERNAME_INVALID_CHARS);
                    isValid = false;
                    break;
                }
            }

            if (!isValid)
                continue;

            // Check for consecutive underscores
            if (input.Contains("__"))
            {
                Console.WriteLine(FieldNames.USERNAME + ValidatorMessages.USERNAME_NO_CONSECUTIVE_UNDERSCORE);
                continue;
            }

            // Cannot end with underscore
            if (input.EndsWith("_"))
            {
                Console.WriteLine(FieldNames.USERNAME + ValidatorMessages.USERNAME_NO_TRAILING_UNDERSCORE);
                continue;
            }

            Console.WriteLine(FieldNames.USERNAME + ValidatorMessages.RECORDED_MSG);
            return input.ToLower();
        }
    }

    public static int GetValidPin()
    {
        while (true)
        {
            Console.Write(PromptMessages.ENTER + FieldNames.PIN + PromptMessages.COLON);
            string input = ReadMaskedInput();

            if (string.IsNullOrWhiteSpace(input))
            {
                Console.WriteLine(FieldNames.PIN + ValidatorMessages.NO_EMPTY);
                continue;
            }

            input = input.Trim();

            // Must be exactly 6 digits
            if (input.Length != InfoLimits.PIN_LENGTH)
            {
                Console.WriteLine(FieldNames.PIN + ValidatorMessages.PIN_EXACT_LENGTH + InfoLimits.PIN_LENGTH + ValidatorMessages.DIGITS);
                continue;
            }

            // Must be all digits
            if (!input.All(char.IsDigit))
            {
                Console.WriteLine(FieldNames.PIN + ValidatorMessages.PIN_ONLY_DIGITS);
                continue;
            }

            int pin = int.Parse(input);

            // Check for sequential patterns (123456, 654321)
            if (IsSequentialPattern(input))
            {
                Console.WriteLine(FieldNames.PIN + ValidatorMessages.PIN_NO_SEQUENTIAL);
                continue;
            }

            // Check for repeating digits (111111, 000000)
            if (IsRepeatingPattern(input))
            {
                Console.WriteLine(FieldNames.PIN + ValidatorMessages.PIN_NO_REPEATING);
                continue;
            }

            Console.WriteLine(FieldNames.PIN + ValidatorMessages.RECORDED_MSG);
            return pin;
        }
    }

    public static int ConfirmPin(int originalPin)
    {
        int attempts = 0;

        while (attempts < InfoLimits.MAX_PIN_ATTEMPTS)
        {
            Console.Write(PromptMessages.ENTER + FieldNames.PIN + " to confirm" + PromptMessages.COLON);
            string input = ReadMaskedInput();

            if (string.IsNullOrWhiteSpace(input))
            {
                Console.WriteLine(FieldNames.PIN + ValidatorMessages.NO_EMPTY);
                attempts++;
                continue;
            }

            input = input.Trim();

            if (!input.All(char.IsDigit) || input.Length != InfoLimits.PIN_LENGTH)
            {
                Console.WriteLine(FieldNames.PIN + ValidatorMessages.PIN_EXACT_LENGTH + InfoLimits.PIN_LENGTH + ValidatorMessages.DIGITS);
                attempts++;
                continue;
            }

            int confirmPin = int.Parse(input);

            if (confirmPin == originalPin)
            {
                Console.WriteLine(FieldNames.PIN + ValidatorMessages.PIN_CONFIRMED);
                return confirmPin;
            }

            attempts++;
            Console.WriteLine(ValidatorMessages.PIN_MISMATCH + (InfoLimits.MAX_PIN_ATTEMPTS - attempts) + ValidatorMessages.ATTEMPTS_REMAINING);
        }

        throw new InvalidOperationException(ValidatorMessages.PIN_CONFIRMATION_FAILED);
    }

    private static bool IsSequentialPattern(string pin)
    {
        // Check ascending (123456, 012345, etc.)
        bool isAscending = true;
        for (int i = 1; i < pin.Length; i++)
        {
            if (pin[i] != pin[i - 1] + 1)
            {
                isAscending = false;
                break;
            }
        }

        // Check descending (654321, 987654, etc.)
        bool isDescending = true;
        for (int i = 1; i < pin.Length; i++)
        {
            if (pin[i] != pin[i - 1] - 1)
            {
                isDescending = false;
                break;
            }
        }

        return isAscending || isDescending;
    }

    private static bool IsRepeatingPattern(string pin)
    {
        char firstDigit = pin[0];
        return pin.All(c => c == firstDigit);
    }

    public static double GetValidInitialDeposit()
    {
        while (true)
        {
            Console.Write(PromptMessages.ENTER + FieldNames.INITIAL_DEPOSIT + " (" + PromptMessages.CURRENCY_SYMBOL + BalanceLimits.MIN_INIT_DEPOSIT.ToString("N2") + " - " + PromptMessages.CURRENCY_SYMBOL + BalanceLimits.MAX_INIT_DEPOSIT.ToString("N2") + ")" + PromptMessages.COLON);
            string input = Console.ReadLine();

            if (string.IsNullOrWhiteSpace(input))
            {
                Console.WriteLine(FieldNames.INITIAL_DEPOSIT + ValidatorMessages.NO_EMPTY);
                continue;
            }

            if (!double.TryParse(input.Trim(), out double amount))
            {
                Console.WriteLine(ValidatorMessages.INVALID_AMOUNT);
                continue;
            }

            if (amount < BalanceLimits.MIN_INIT_DEPOSIT || amount > BalanceLimits.MAX_INIT_DEPOSIT)
            {
                Console.WriteLine(FieldNames.INITIAL_DEPOSIT + ValidatorMessages.AMOUNT_RANGE + PromptMessages.CURRENCY_SYMBOL + BalanceLimits.MIN_INIT_DEPOSIT.ToString("N2") + ValidatorMessages.AND + PromptMessages.CURRENCY_SYMBOL + BalanceLimits.MAX_INIT_DEPOSIT.ToString("N2") + ".");
                continue;
            }

            amount = Math.Round(amount, 2);
            Console.WriteLine(FieldNames.INITIAL_DEPOSIT + " of " + PromptMessages.CURRENCY_SYMBOL + amount.ToString("N2") + ValidatorMessages.RECORDED_MSG);
            return amount;
        }
    }

    public static string GetUsernameLogin()
    {
        while (true)
        {
            Console.Write(PromptMessages.ENTER + FieldNames.USERNAME + PromptMessages.COLON);
            string input = Console.ReadLine();

            if (string.IsNullOrWhiteSpace(input))
            {
                Console.WriteLine(FieldNames.USERNAME + ValidatorMessages.NO_EMPTY);
                continue;
            }

            input = input.Trim().ToLower();

            if (input.Length < InfoLimits.MIN_USERNAME_LENGTH || input.Length > InfoLimits.MAX_USERNAME_LENGTH)
            {
                Console.WriteLine(FieldNames.USERNAME + ValidatorMessages.INVALID_RANGE + " " + InfoLimits.MIN_USERNAME_LENGTH + " - " + InfoLimits.MAX_USERNAME_LENGTH + ValidatorMessages.CHARACTERS_ALLOWED);
                continue;
            }

            return input;
        }
    }

    public static int GetPinLogin()
    {
        while (true)
        {
            Console.Write(PromptMessages.ENTER + FieldNames.PIN + PromptMessages.COLON);
            string input = ReadMaskedInput();

            if (string.IsNullOrWhiteSpace(input))
            {
                Console.WriteLine(FieldNames.PIN + ValidatorMessages.NO_EMPTY);
                continue;
            }

            if (input.Length != InfoLimits.PIN_LENGTH || !input.All(char.IsDigit))
            {
                Console.WriteLine(FieldNames.PIN + ValidatorMessages.PIN_EXACT_LENGTH + InfoLimits.PIN_LENGTH + ValidatorMessages.DIGITS);
                continue;
            }

            return int.Parse(input);
        }
    }

    private static string ReadMaskedInput()
    {
        string input = "";

        while (true)
        {
            ConsoleKeyInfo key = Console.ReadKey(true);

            if (key.Key == ConsoleKey.Enter)
            {
                Console.WriteLine();
                break;
            }

            if (key.Key == ConsoleKey.Backspace)
            {
                if (input.Length > 0)
                {
                    input = input.Substring(0, input.Length - 1);
                    Console.Write("\b \b");
                }
                continue;
            }

            if (char.IsDigit(key.KeyChar))
            {
                input += key.KeyChar;
                Console.Write(PromptMessages.MASK_SYMBOL);
            }
        }

        return input;
    }
}
public static class TransactionValidators
{
    public static double GetValidWithdrawal(User user)
    {
        double currentBalance = user.getBalance();
        
        while (true)
        {
            Console.Write(PromptMessages.ENTER + TransactionFields.WITHDRAWAL_AMOUNT + PromptMessages.COLON + PromptMessages.CURRENCY_SYMBOL);
            string input = Console.ReadLine();
            
            if (string.IsNullOrWhiteSpace(input))
            {
                Console.WriteLine(TransactionFields.WITHDRAWAL_AMOUNT + ValidatorMessages.NO_EMPTY);
                continue;
            }
            
            if (!double.TryParse(input.Trim(), out double amount))
            {
                Console.WriteLine(ValidatorMessages.INVALID_AMOUNT);
                continue;
            }
            
            if (amount <= 0)
            {
                Console.WriteLine(TransactionMessages.AMOUNT_POSITIVE);
                continue;
            }
            
            if (amount < TransactionLimits.MIN_WITHDRAWAL)
            {
                Console.WriteLine(TransactionMessages.WITHDRAWAL_TOO_SMALL + PromptMessages.CURRENCY_SYMBOL + TransactionLimits.MIN_WITHDRAWAL.ToString("N2") + ".");
                continue;
            }
            
            if (amount > TransactionLimits.MAX_WITHDRAWAL)
            {
                Console.WriteLine(TransactionMessages.WITHDRAWAL_TOO_LARGE + PromptMessages.CURRENCY_SYMBOL + TransactionLimits.MAX_WITHDRAWAL.ToString("N2") + ".");
                continue;
            }
            
            if (amount > currentBalance)
            {
                Console.WriteLine(TransactionMessages.INSUFFICIENT_FUNDS);
                Console.WriteLine(TransactionMessages.CURRENT_BALANCE + PromptMessages.CURRENCY_SYMBOL + currentBalance.ToString("N2"));
                continue;
            }
            
            double newBalance = currentBalance - amount;
            if (newBalance < BalanceLimits.MIN_BALANCE)
            {
                Console.WriteLine(TransactionMessages.BALANCE_TOO_LOW);
                Console.WriteLine(TransactionMessages.MINIMUM_BALANCE + PromptMessages.CURRENCY_SYMBOL + BalanceLimits.MIN_BALANCE.ToString("N2"));
                Console.WriteLine(TransactionMessages.MAXIMUM_WITHDRAWAL + PromptMessages.CURRENCY_SYMBOL + (currentBalance - BalanceLimits.MIN_BALANCE).ToString("N2"));
                continue;
            }
            
            amount = Math.Round(amount, 2);
            Console.WriteLine(TransactionFields.WITHDRAWAL_AMOUNT + " of " + PromptMessages.CURRENCY_SYMBOL + amount.ToString("N2") + ValidatorMessages.RECORDED_MSG);
            return amount;
        }
    }
    
    public static double GetValidDeposit()
    {
        while (true)
        {
            Console.Write(PromptMessages.ENTER + TransactionFields.DEPOSIT_AMOUNT + PromptMessages.COLON + PromptMessages.CURRENCY_SYMBOL);
            string input = Console.ReadLine();
            
            if (string.IsNullOrWhiteSpace(input))
            {
                Console.WriteLine(TransactionFields.DEPOSIT_AMOUNT + ValidatorMessages.NO_EMPTY);
                continue;
            }
            
            if (!double.TryParse(input.Trim(), out double amount))
            {
                Console.WriteLine(ValidatorMessages.INVALID_AMOUNT);
                continue;
            }
            
            if (amount <= 0)
            {
                Console.WriteLine(TransactionMessages.AMOUNT_POSITIVE);
                continue;
            }
            
            if (amount < TransactionLimits.MIN_DEPOSIT)
            {
                Console.WriteLine(TransactionMessages.DEPOSIT_TOO_SMALL + PromptMessages.CURRENCY_SYMBOL + TransactionLimits.MIN_DEPOSIT.ToString("N2") + ".");
                continue;
            }
            
            if (amount > TransactionLimits.MAX_DEPOSIT)
            {
                Console.WriteLine(TransactionMessages.DEPOSIT_TOO_LARGE + PromptMessages.CURRENCY_SYMBOL + TransactionLimits.MAX_DEPOSIT.ToString("N2") + ".");
                continue;
            }
            
            amount = Math.Round(amount, 2);
            Console.WriteLine(TransactionFields.DEPOSIT_AMOUNT + " of " + PromptMessages.CURRENCY_SYMBOL + amount.ToString("N2") + ValidatorMessages.RECORDED_MSG);
            return amount;
        }
    }

}