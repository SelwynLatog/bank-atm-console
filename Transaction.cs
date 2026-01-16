using System;

public class Transaction
{
    private string type;
    private double amount;
    private double balanceBefore;
    private double balanceAfter;
    private DateTime timestamp;

    public Transaction(string type, double amount, double balanceBefore, double balanceAfter)
    {
        this.type = type;
        this.amount = amount;
        this.balanceBefore = balanceBefore;
        this.balanceAfter = balanceAfter;
        this.timestamp = DateTime.Now;
    }

    public string GetType()
    {
        return type;
    }

    public double GetAmount()
    {
        return amount;
    }

    public double GetBalanceBefore()
    {
        return balanceBefore;
    }

    public double GetBalanceAfter()
    {
        return balanceAfter;
    }

    public DateTime GetTimestamp()
    {
        return timestamp;
    }

    public string GetFormattedTimestamp()
    {
        return timestamp.ToString("MMM dd, yyyy hh:mm:ss tt");
    }
}