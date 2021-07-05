﻿namespace Jobzy.Services.Interfaces
{
    using System.Threading.Tasks;

    using Jobzy.Data.Models;

    public interface IBalanceManager
    {
        Task<bool> AddMoneyAsync(Balance balance, decimal amount);
    }
}
