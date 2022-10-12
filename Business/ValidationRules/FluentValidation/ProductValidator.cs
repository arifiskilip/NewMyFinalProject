using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entities.Concrete;
using FluentValidation;

namespace Business.ValidationRules.FluentValidation
{
    public class ProductValidator : AbstractValidator<Product>
    {
        public ProductValidator()
        {
                RuleFor(x=>x.ProductName).NotEmpty();
                RuleFor(x => x.ProductName).MinimumLength(2);
                RuleFor(x => x.UnitPrice).NotEmpty();
                RuleFor(x => x.UnitPrice).GreaterThan(0);
                RuleFor(x => x.UnitPrice).GreaterThanOrEqualTo(10)
                    .When(x => x.CategoryId == 1);
                RuleFor(x => x.ProductName).Must(StarthWithA);
        }

        private bool StarthWithA(string arg)
        {
            return arg.StartsWith("A");
        }
    }
}
