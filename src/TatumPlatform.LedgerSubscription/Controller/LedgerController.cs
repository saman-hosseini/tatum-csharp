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
using Microsoft.EntityFrameworkCore;
using System.IO;
using System.Net.Http;
using ClosedXML.Excel;

namespace TatumPlatform.LedgerSubscription.Controller
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = SchemesNamesConst.TokenAuthenticationDefaultScheme)]
    public class LedgerController : ControllerBase
    {
        [AllowAnonymous]
        public async Task<string> Get()
        {
            return "Hi";
        }

        [AllowAnonymous]
        [HttpPost]
        public async Task<string> FileTest(IFormFile file)
        {
            using (var stream = new MemoryStream())
            {
                await file.CopyToAsync(stream);
                stream.Position = 0;
                using (var workBook = new XLWorkbook(stream))
                {
                    IXLWorksheet workSheet = workBook.Worksheet(1);
                    foreach (IXLRow row in workSheet.Rows())
                    {
                        foreach (IXLCell cell in row.Cells())
                        {
                        }
                        for (int i = 0; i < row.CellCount(); i++)
                        {
                            var firstCol = row.Cell(0);
                            var secondCol = row.Cell(1);
                        }
                    }
                }
                return "Hi file";
            }
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
                    await context.IncomingRequest.AddAsync(new IncomingRequest() { JsonData = output });
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
            catch (Exception ex)
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
