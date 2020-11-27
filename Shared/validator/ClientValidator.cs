using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;
using Shared.model;

namespace Shared.validator
{
  public  class ClientValidator:AbstractValidator<Client>
    {
        public ClientValidator()
        {
            RuleFor(x => x.ForName).NotEmpty();
            RuleFor(x => x.Nbr).NotEmpty();
            RuleFor(x => x.Name).NotEmpty();
            RuleFor(x => x.BirthDate).NotEmpty();
            RuleFor(x => x.Country).NotEmpty();
            RuleFor(x => x.City).NotEmpty();
            RuleFor(x => x.IdCarNbr).NotEmpty();
            RuleFor(x => x.Tel).NotEmpty();
            RuleFor(x => x.DrivingLicence).NotEmpty();
            RuleFor(x => x.Email).NotEmpty().EmailAddress();


        }
    }
}
