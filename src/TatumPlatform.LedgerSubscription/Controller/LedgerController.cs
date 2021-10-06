using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TatumPlatform.LedgerSubscription.Model;

namespace TatumPlatform.LedgerSubscription.Controller
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    //[Authorize(Policy = "CheckHmacDigest")]
    //[Authorize(AuthenticationSchemes = SchemesNamesConst.TokenAuthenticationDefaultScheme)]
    public class LedgerController : ControllerBase
    {
        [AllowAnonymous]
        public string Get()
        {
            return "Hi";
        }

        [AllowAnonymous]
        [HttpPost]
        public async Task<string> Test(object obj)
        {
            string output = JsonSerializer.Serialize(obj);
            try
            {
                using (var context = new LedgerContext())
                {
                    await context.IncomingRequest.AddAsync(new IncomingRequest() { JsonData = output});
                    await context.SaveChangesAsync();
                }
                return "success";
            }
            catch (Exception ex)
            {
                return Exception(ex);
            }
        }

        [HttpPost]
        public async Task<string> AccountIncomingBlockchainTransaction(AccountIncomingBlockchainTransaction message)
        {
            try
            {
                using (var context = new LedgerContext())
                {
                    await context.AccountIncomingBlockchainTransactions.AddAsync(message);
                    await context.SaveChangesAsync();
                }
                return "success";
            }
            catch(Exception ex)
            {
                return Exception(ex);
            }
        }

        [HttpPost]
        public async Task<string> AccountPendingBlockchainTransaction(AccountPendingBlockchainTransaction message)
        {
            try
            {
                using (var context = new LedgerContext())
                {
                    await context.AccountPendingBlockchainTransaction.AddAsync(message);
                    await context.SaveChangesAsync();
                }
                return "success";
            }
            catch (Exception ex)
            {
                return Exception(ex);
            }
        }

        [HttpPost]
        public async Task<string> CustomerTradeMatch(CustomerTradeMatch message)
        {
            try
            {
                using (var context = new LedgerContext())
                {
                    await context.CustomerTradeMatch.AddAsync(message);
                    await context.SaveChangesAsync();
                }
                return "success";
            }
            catch (Exception ex)
            {
                return Exception(ex);
            }
        }

        [HttpPost]
        public async Task<string> KmsCompletedTx(KmsCompletedTx message)
        {
            try
            {
                using (var context = new LedgerContext())
                {
                    await context.KmsCompletedTx.AddAsync(message);
                    await context.SaveChangesAsync();
                }
                return "success";
            }
            catch (Exception ex)
            {
                return Exception(ex);
            }
        }

        [HttpPost]
        public async Task<string> KmsFailedTx(KmsFailedTx message)
        {
            try
            {
                using (var context = new LedgerContext())
                {
                    await context.KmsFailedTx.AddAsync(message);
                    await context.SaveChangesAsync();
                }
                return "success";
            }
            catch (Exception ex)
            {
                return Exception(ex);
            }
        }

        [HttpPost]
        public async Task<string> TransactionInTheBlock(TransactionInTheBlock message)
        {
            try
            {
                using (var context = new LedgerContext())
                {
                    await context.TransactionInTheBlock.AddAsync(message);
                    await context.SaveChangesAsync();
                }
                return "success";
            }
            catch (Exception ex)
            {
                return Exception(ex);
            }
        }

        private string Exception(Exception ex)
        {
            const int maxExceptionDepth = 5;
            if (ex == null)
            {
                return "";
            }
            var message = new StringBuilder(ex.Message);
            var inner = ex.InnerException;
            var depthCounter = 0;
            while (inner != null && depthCounter++ < maxExceptionDepth)
            {
                message.Append(" INNER EXCEPTION: ");
                message.Append(inner.Message);
                inner = inner.InnerException;
            }
            return message.ToString();
        }
    }
}
