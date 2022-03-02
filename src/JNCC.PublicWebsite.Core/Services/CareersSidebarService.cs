﻿using JNCC.PublicWebsite.Core.Extensions;
using JNCC.PublicWebsite.Core.Interfaces.Services;
using JNCC.PublicWebsite.Core.Models;
using JNCC.PublicWebsite.Core.ViewModels;
using System.Collections.Generic;
using System.Linq;
using Umbraco.Extensions;

namespace JNCC.PublicWebsite.Core.Services
{
    internal sealed class CareersSidebarService : SidebarServiceBase, ICareersSidebarService
    {
        private const int NumberOfLatestJobs = 5;
        public CareersSidebarService(INavigationItemService navigationItemService
            , IDataHubRawQueryService dataHubRawQueryService
            ) : base(navigationItemService
                , dataHubRawQueryService
                )
        { }

        public CareersSidebarViewModel GetViewModel(CareersLandingPage model)
        {
            return new CareersSidebarViewModel
            {
                PrimaryCallToActionButton = _navigationItemService.GetViewModel(model.SidebarPrimaryCallToActionButton),
                LatestJobs = GetLatestJobs(model),
                SeeAlsoLinks = _navigationItemService.GetViewModels(model.SidebarSeeAlsoLinks)
            };
        }

        private IEnumerable<JobItemViewModel> GetLatestJobs(CareersLandingPage model)
        {
            var latestJobs = model.Children<IndividualJobPage>().OrderByDescending(x => x.UpdateDate).Take(NumberOfLatestJobs);
            var viewModels = new List<JobItemViewModel>();

            if (latestJobs.Any() == false)
            {
                return viewModels;
            }

            foreach (var job in latestJobs)
            {
                var viewModel = new JobItemViewModel()
                {
                    JobTitle = job.GetHeadline(),
                    Url = job.Url(),
                    Grade = job.Grade,
                    Location = job.Location
                };

                viewModels.Add(viewModel);
            }

            return viewModels;
        }
    }
}