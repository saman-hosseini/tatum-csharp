using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TatumPlatform.Model.Requests;

namespace TatumPlatform.Model.Rules
{
    public class VeChainTransferValidator : BaseTransferValidator
    {
        public VeChainTransferValidator()
        {
            RuleFor(q => q.FromTag).Empty();
        }
    }
}
