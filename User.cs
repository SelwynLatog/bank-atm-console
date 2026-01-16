using System;
using System.Collections.Generic;

public class User
{
    private string fName;
    private string mName;
    private string lName;
    private string username;
    private int pin;
    private double balance;
    private List<Transaction> transactionHistory;
    
    public User(string fName, string mName, string lName, string username, int pin, double balance)
    {
        this.fName = fName;
        this.mName = mName;
        this.lName = lName;
        this.username = username;
        this.pin = pin;
        this.balance = balance;
        this.transactionHistory = new List<Transaction>();
        
        // Record initial deposit as first transaction
        Transaction initialDeposit = new Transaction(
            TransactionTypes.INITIAL_DEPOSIT,
            balance,
            0,
            balance
        );
        transactionHistory.Add(initialDeposit);
    }
    
    // Getters
    public string getFirstName()
    {
        return fName;
    }
    
    public string getMiddleName()
    {
        return mName;
    }
    
    public string getLastName()
    {
        return lName;
    }
    
    public string getUsername()
    {
        return username;
    }
    
    public int getPin()
    {
        return pin;
    }
    
    public double getBalance()
    {
        return balance;
    }

    public List<Transaction> getTransactionHistory()
    {
        return transactionHistory;
    }
    
    // Balance modification methods
    public void Withdraw(double amount)
    {
        double previousBalance = balance;
        balance -= amount;
        
        Transaction transaction = new Transaction(
            TransactionTypes.WITHDRAWAL,
            amount,
            previousBalance,
            balance
        );
        transactionHistory.Add(transaction);
    }
    
    public void Deposit(double amount)
    {
        double previousBalance = balance;
        balance += amount;
        
        Transaction transaction = new Transaction(
            TransactionTypes.DEPOSIT,
            amount,
            previousBalance,
            balance
        );
        transactionHistory.Add(transaction);
    }
    
    public bool CanWithdraw(double amount)
    {
        return balance >= amount;
    }
}