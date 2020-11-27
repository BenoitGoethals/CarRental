using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;
using Shared.model;

namespace Shared.validator
{
    public class CarValidator: AbstractValidator<Car>
    {
        public CarValidator()
        {
        }
    }
}
