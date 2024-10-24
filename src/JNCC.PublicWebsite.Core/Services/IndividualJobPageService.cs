﻿using JNCC.PublicWebsite.Core.Interfaces.Services;
using JNCC.PublicWebsite.Core.Models;
using JNCC.PublicWebsite.Core.Utilities;
using JNCC.PublicWebsite.Core.Extensions;
using JNCC.PublicWebsite.Core.ViewModels;
using Umbraco.Extensions;

namespace JNCC.PublicWebsite.Core.Services
{
    internal sealed class IndividualJobPageService : IIndividualJobPageService
    {
        public IEnumerable<AccordionItemViewModel> GetTabbedContent(IndividualJobPage model)
        {
            var tabbedContent = new List<AccordionItemViewModel>
            {
                new AccordionItemViewModel ( "background", "Background", model.BackgroundContent ),
                new AccordionItemViewModel ( "post-duties", "Post Duties", model.PostDutiesContent ),
                new AccordionItemViewModel ( "competences", "Competences", model.CompetencesContent ),
                new AccordionItemViewModel ( "salary-and-benefits", "Salary & Benefits", model.SalaryBenefitsContent )
            };

            return tabbedContent.Where(x => ExistenceUtility.IsNullOrWhiteSpace(x.Content) == false).ToArray();
        }

        public IReadOnlyDictionary<string, string> GetKeyData(IndividualJobPage model)
        {
            var keyData = new Dictionary<string, string>
            {
                { "Ref No", model.ReferenceNumber },
                { "Grade", model.Grade },
                { "Type of appointment", model.TypeOfAppointment },
                { "Location",  model.Location },
                { "Team", model.Team },                
            };

            if(model.ClosingDate != DateTime.MinValue)
            {
                keyData.Add("Closing Date", model.ClosingDate.ToJobExpirationDate());
            }

            return keyData.Where(x => string.IsNullOrWhiteSpace(x.Value) == false)
                          .ToDictionary(x => x.Key, x => x.Value);
        }
    }
}
