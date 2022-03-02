﻿using JNCC.PublicWebsite.Core.Extensions;
using JNCC.PublicWebsite.Core.Interfaces.Services;
using JNCC.PublicWebsite.Core.Models;
using JNCC.PublicWebsite.Core.ViewModels;
using System.Collections.Generic;
using System.Linq;
using Umbraco.Extensions;

namespace JNCC.PublicWebsite.Core.Services
{
    internal sealed class CareersLandingPageService : ICareersLandingPageService
    {
        public IEnumerable<CareersListItemViewModel> GetCareers(CareersLandingPage model)
        {
            var jobs = model.Children<IndividualJobPage>();
            var viewModels = new List<CareersListItemViewModel>();

            if (jobs.Any() == false)
            {
                return viewModels;
            }

            foreach (var job in jobs.OrderBy(x => x.SortOrder))
            {
                var viewModel = new CareersListItemViewModel()
                {
                    Grade = job.Grade,
                    JobTitle = job.GetHeadline(),
                    Location = job.Location,
                    Team = job.Team,
                    Type = job.TypeOfAppointment,
                    Url = job.Url()
                };

                viewModels.Add(viewModel);
            }

            return viewModels;
        }
    }
}
