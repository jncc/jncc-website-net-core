angular.module('umbraco.deploy')
    .controller('UmbracoDeploy.DashboardController',
    [
        '$scope', '$window', '$location', 'deployNavigation', 'deployConfiguration', 'contentResource', 'assetsService', 'deployService',
        function ($scope, $window, $location, deployNavigation, deployConfiguration, contentResource, assetsService, deployService) {

            var vm = this;

            vm.config = deployConfiguration;
            vm.openProject = openProject;
            vm.openPayment = openPayment;
            vm.openDocumentation = openDocumentation;
            vm.feedbackMessageLevel = '';
            vm.dropDownOpen = false;

            function init() {

                assetsService.load(["lib/moment/moment.min.js"], $scope);

                if(deployService.feedbackMessageLevel) {
                    deployService.feedbackMessageLevel().then(function(data) {
                        vm.feedbackMessageLevel = data.FeedbackMessageLevel;
                    });
                }
            }

            function openProject() {
                $window.open("https://www.s1.umbraco.io/project/" + vm.config.ProjectAlias);
            };

            
            function openPayment() {
                $window.open("https://www.s1.umbraco.io/project/" + vm.config.ProjectAlias + '/paymentmethod');
            };

            function openDocumentation() {
                $window.open("https://docs.umbraco.com/umbraco-cloud/getting-started");
            };

            init();

            vm.navigation = deployNavigation;
        }
    ]);

angular.module('umbraco.deploy')
    .controller('UmbracoDeploy.ManagementDashboardController',
    [
        '$scope', '$location', 'deployQueueService', 'deployManagementResource', 'editorService', 'notificationsService',
        function ($scope, $location, deployQueueService, deployManagementResource, editorService, notificationsService) {
            var vm = this;
            var timer = 0;
            var schemaComparisonSortData = [];

            vm.isLoading = true;

            vm.refreshAll = refreshAll;
            vm.triggerOperation = triggerOperation;
            vm.downloadDeployArtifacts = downloadDeployArtifacts;
            vm.toggleFailedOperationDetail = toggleFailedOperationDetail;
            vm.copyFailedOperationDetailToClipboard = copyFailedOperationDetailToClipboard;
            vm.getSchemaComparisonSummary = getSchemaComparisonSummary;
            vm.scrollToId = scrollToId;
            vm.scrollToEntityType = scrollToEntityType;
            vm.scrollToAnchorLinks = scrollToAnchorLinks;
            vm.hasOutOfDateItem = hasOutOfDateItem;
            vm.showSchemaComparisonDetails = showSchemaComparisonDetails;
            vm.sortSchemaComparison = sortSchemaComparison;

            vm.showFailedOperationDetail = false;
            vm.failedOperationDetail = null;

            vm.triggeredOperation = "";
            vm.dashboard = null;

            function init() {
                deployQueueService.isLicensed().then(function (check) {
                    vm.isLicensed = check.success;
                    if (vm.isLicensed) {
                        refresh();
                        getConfigurationDetails();
                        getSchemaComparison();
                    } else {
                        vm.licenseFailureMessage = check.message;
                    }

                    vm.isLoading = false;
                });;
            }

            function refreshAll() {
                refresh(true);
                getSchemaComparison();
            }

            function refresh(clearOperationMessage) {
                if (timer > 0) {
                    clearTimeout(timer);
                }

                if (clearOperationMessage) {
                    vm.operationMessage = '';
                }

                vm.loading = true;
                deployManagementResource.getDashboard().then(function (result) {
                    vm.dashboard = result;
                    vm.loading = false;

                    // Clearing the timeout before setting ensures we're not causing duplicate calls
                    clearTimeout(timer);
                    timer = setTimeout(function () { refresh(true) }, 5000);
                });
            }

            function triggerOperation(operation) {
                vm.loading = true;
                vm.showFailedOperationDetail = false;
                vm.failedOperationDetail = null;
                vm.triggeredOperation = operation;
                deployManagementResource.triggerOperation(operation).then(function (result) {
                    vm.operationMessage = result;
                    refresh();
                    vm.loading = false;
                });
            }
            
            function downloadDeployArtifacts() {
                vm.loading = true;
                deployManagementResource.downloadDeployArtifacts().then(function (result) { 
                    return result;
                    vm.loading = false;
                });
            }

            function toggleFailedOperationDetail() {
                if (vm.showFailedOperationDetail) {
                    vm.showFailedOperationDetail = false;
                }
                else if (vm.failedOperationDetail) {
                    vm.showFailedOperationDetail = true;
                }
                else {
                    vm.loading = true;
                    deployManagementResource.getFailedOperationDetail().then(function (result) {
                        vm.failedOperationDetail = JSON.stringify(JSON.parse(result), null, 2);
                        vm.showFailedOperationDetail = true;
                        vm.loading = false;
                    });
                }
            }

            function copyFailedOperationDetailToClipboard() {
                navigator.clipboard.writeText(vm.failedOperationDetail);
                notificationsService.success("Failed operation details copied to clipboard.");
            }

            function getConfigurationDetails() {
                vm.loading = true;
                deployManagementResource.getConfigurationDetails().then(function (data) {
                    vm.configurationDetails = data;
                    vm.loading = false;
                });
            }

            function getSchemaComparison() {
                vm.loading = true;
                deployManagementResource.getSchemaComparison().then(function (data) {
                    vm.schemaComparison = data.schemaComparison;
                    initializeSchemaComparisonSortData();
                    vm.loading = false;
                });
            }

            function initializeSchemaComparisonSortData() {
                schemaComparisonSortData = [];
                for (var key in vm.schemaComparison) {
                    if (vm.schemaComparison.hasOwnProperty(key)) {
                        schemaComparisonSortData.push({
                            entityType: key,
                            sortBy: "label",
                            sortDirection: "asc",
                        });
                    }
                }
            }

            function getSchemaComparisonSummary(comparisonData) {
                var fullCount = comparisonData.length;
                var upToDateCount = comparisonData.filter(function (value) {
                    return value.isUpToDate;
                }).length;

                var upToDateCountLabel;
                if (fullCount === upToDateCount) {
                    upToDateCountLabel = "all are";
                } else if (upToDateCount === 0) {
                    upToDateCountLabel = "none are";
                } else if (upToDateCount === 1) {
                    upToDateCountLabel = "1 is";
                } else {
                    upToDateCountLabel = upToDateCount + " are";
                }

                return fullCount + " found, " + upToDateCountLabel + " up to date";
            }

            function scrollToId(id) {
                $location.hash(id);
            }

            function scrollToEntityType(index) {
                scrollToId("deploy-schema-comparison-" + index);
            }

            function scrollToAnchorLinks() {
                scrollToId("deploy-schema-comparison-anchor-links");
            }

            function hasOutOfDateItem(comparisonData) {
                return comparisonData.filter(function (value) {
                    return !value.isUpToDate;
                }).length > 0;
            }

            function showSchemaComparisonDetails(entityType, itemLabel, udi) {
                var schemaComparisonDetailEditor = {
                    view: "/App_Plugins/Deploy/views/dashboards/overlays/schema-comparison-detail.html",
                    title: "Schema Comparison",
                    subtitle: entityType + ": " + itemLabel,
                    udi: udi,
                    size: "medium",
                    submit: function (model) {
                    },
                    close: function () {
                        editorService.close();
                    }
                };
                editorService.open(schemaComparisonDetailEditor);
            }

            function sortSchemaComparison(entityType, field) {
                var currentSort = getSortData(entityType);
                if (!currentSort) {
                    return;
                }

                // If sorting by the same field as previously, just swap the sort order.
                if (currentSort.sortBy === field) {
                    currentSort.sortDirection = currentSort.sortDirection === "asc" ? "desc" : "asc";
                } else {
                    // If a new field, sort ascending by that field.
                    currentSort.sortBy = field;
                    currentSort.sortDirection = "asc";
                }

                var sd = currentSort.sortDirection === "asc" ? 1 : -1;
                vm.schemaComparison[entityType] = vm.schemaComparison[entityType].sort(function (a, b) {
                    return (a[field] > b[field] ? 1 : -1) * sd;
                });
            }

            function getSortData(entityType) {
                for (var i = 0; i < schemaComparisonSortData.length; i++) {
                    if (schemaComparisonSortData[i].entityType === entityType) {
                        return schemaComparisonSortData[i];
                    }
                }

                return null;
            }

            init();
        }
    ]);

angular.module('umbraco.deploy')
    .controller('UmbracoDeploy.OnPremDashboardController',
    [
        '$scope', '$window', '$location', 'deployNavigation', 'deployConfiguration', 'deployQueueService', 'assetsService', 'deployService',
        function ($scope, $window, $location, deployNavigation, deployConfiguration, deployQueueService, assetsService, deployService) {
            var vm = this;

            vm.config = deployConfiguration;
            vm.openProject = openProject;
            vm.openPayment = openPayment;
            vm.openDocumentation = openDocumentation;
            vm.feedbackMessageLevel = '';
            vm.dropDownOpen = false;

            function init() {

                assetsService.load(["lib/moment/moment.min.js"], $scope);

                deployQueueService.isLicensed().then(function (check) {
                    vm.isLicensed = check.success;
                });;

                if(deployService.feedbackMessageLevel) {
                    deployService.feedbackMessageLevel().then(function(data) {
                        vm.feedbackMessageLevel = data.FeedbackMessageLevel;
                    });
                }
            }

            function openProject() {
                $window.open("https://www.s1.umbraco.io/project/" + vm.config.ProjectAlias);
            };

            
            function openPayment() {
                $window.open("https://www.s1.umbraco.io/project/" + vm.config.ProjectAlias + '/paymentmethod');
            };

            function openDocumentation() {
                $window.open("https://docs.umbraco.com/umbraco-cloud/getting-started");
            };

            init();

            vm.navigation = deployNavigation;
        }
    ]);

angular.module('umbraco.deploy').controller('UmbracoDeploy.AddToQueueDialogController',
  function ($scope, $q, deployConfiguration, deployQueueService, navigationService, deployHelper, deployResource, treeService, $location, $routeParams, editorState, contentResource, dateHelper, userService) {
    var vm = this;

    vm.deployConfiguration = deployConfiguration;
    vm.addedToQueue = false;
    vm.includeDescendants = false;
    vm.releaseDate = null;
    vm.releaseDateFormatted = "";
    vm.currentUser = null;

    // $scope.currentNode will be null if this dialog has been accessed via the
    // "compare" dialog, when it was launched from the editor rather than the tree.
    vm.item = $scope.currentNode || editorState.current;

    vm.languages = [];
    vm.isContent = false;
    vm.contentHasVariants = false;
    vm.loading = true;
    vm.disableAddToQueue = false;

    var getIncludedLanguages = function () {
      return vm.languages.filter(function (language) {
        return language.include;
      });
    };

    var nowFormatted = moment().format("YYYY-MM-DD HH:mm");
    vm.releaseDateConfig = {
      enableTime: true,
      dateFormat: "Y-m-d H:i",
      time_24hr: true,
      minDate: nowFormatted,
      defaultDate: nowFormatted
    };

    var loadingPromises = [];

    // Load languages if deploying content
    if (vm.item.nodeType == "content") {
      vm.isContent = true;

      loadingPromises.push(contentResource.getById(vm.item.id).then(function (content) {
        vm.contentHasVariants = content.variants.length > 1;

        if (vm.contentHasVariants) {
          vm.languages = content.variants.filter(function (variant) {
            return !(variant.segment && variant.language);
          }).map(function (variant) {
            // Check variant queue for transfer permissions (only 10.2+ has this defined)
            var allowTransfer = variant.allowedActions === undefined || variant.allowedActions.includes('T');

            return {
              culture: variant.language.culture,
              name: variant.language.name,
              allowTransfer: allowTransfer,
              include: allowTransfer
            };
          });

          // Disable button when no languages are available/selected
          vm.disableAddToQueue = getIncludedLanguages().length === 0;
        }
      }));
    }

    loadingPromises.push(userService.getCurrentUser().then(function (user) {
      vm.currentUser = user;
    }));

    if (vm.item.hasChildren) {
      var treeAlias = treeService.getTreeAlias(vm.item);
      var routePath = vm.item.routePath || "";

      loadingPromises.push(deployResource.checkForRegisteredEntityWithPermissionToTransfer(treeAlias, routePath).then(function (data) {
        vm.withBranch = data.CanTransferDescendents;
      }));
    }

    // Hide loader when everything is completed
    $q.all(loadingPromises).then(function () {
      vm.loading = false;
    });

    vm.languagesChange = function () {
      vm.disableAddToQueue = !vm.contentHasVariants || getIncludedLanguages().length === 0;
    };

    vm.releaseDateChange = function (selectedDates, date) {
      if (!date) {
        console.log("not a date");
        return;
      }

      // The date being passed in here is the user's local date/time that they have selected.
      // We need to convert this date back to the server date on the model.
      // Note: we are following logic here used in schedule.controller.js in CMS.
      var serverTime = dateHelper.convertToServerStringTime(moment(date), Umbraco.Sys.ServerVariables.application.serverTimeOffset);
      vm.releaseDate = serverTime;
      vm.releaseDateFormatted = dateHelper.getLocalDate(vm.releaseDate, vm.currentUser.locale, "MMM Do YYYY, HH:mm");
    };

    vm.addToQueue = function () {
      var promises = [];

      if (vm.contentHasVariants) {
        var includedLanguages = getIncludedLanguages();

        if (vm.languages.length == includedLanguages.length) {
          // Add all languages
          promises.push(deployHelper.getDeployItem(vm.item, "*", vm.includeDescendants, vm.releaseDate, $routeParams, treeService, deployResource).then(function (deployItem) {
            deployQueueService.addToQueue(deployItem);
          }));
        } else {
          // Add all included language variants
          includedLanguages.forEach(function (language) {
            promises.push(deployHelper.getDeployItem(vm.item, language.culture, vm.includeDescendants, vm.releaseDate, $routeParams, treeService, deployResource).then(function (deployItem) {
              deployQueueService.addToQueue(deployItem);
            }));
          });
        }
      } else {
        // Add invariant content
        promises.push(deployHelper.getDeployItem(vm.item, null, vm.includeDescendants, vm.releaseDate, $routeParams, treeService, deployResource).then(function (deployItem) {
          deployQueueService.addToQueue(deployItem);
        }));
      }

      $q.all(promises).then(function () {
        vm.addedToQueue = true;
      });
    };

    vm.closeDialog = function () {
      navigationService.hideDialog();
    };

    vm.openTransferQueue = function () {
      navigationService.hideDialog();

      $location.path("/content").search({
        dashboard: 'deploy',
        mculture: $location.search().mculture
      });
    };
  }
);

(function () {
    "use strict";

    function CompareDialogController($scope, deployResource, languageResource, contentResource, editorState, assetsService, deployConfiguration, navigationService) {

        var vm = this;

        vm.loading = true;
        vm.comparableContentFound = false;
        vm.config = deployConfiguration;
        vm.compareWorkspace = {};
        vm.restoreButtonState = "init";
        vm.workspaceDropDownOpen = false;

        vm.changeDestination = changeDestination;
        vm.changeLanguage = changeLanguage;

        vm.queueForTransfer = queueForTransfer;
        vm.partialRestore = partialRestore;
        vm.close = close;

        vm.canTransfer = false;
        vm.canRestore = false;

        vm.selectedLanguage = null;
        vm.languages = [];
        vm.contentHasVariants = false;
        vm.nonComparableContentMessage = "";

        vm.labels = {};

        if ($scope.currentNode) {
            // - available (and use in preference) when dialog is opened from a tree
            vm.compareNode = $scope.currentNode;
        } else if (editorState.current) {
            // - available when dialog is opened an open editor
            vm.compareNode = editorState.current;
        }

        function onInit() {

            // Create some more space (don't think we have a better option here to launch with the size we want to start with).
            document.getElementById("dialog").style.width = "1000px";

            // Set the last workspace to restore from as default.
            if (vm.config.RestoreWorkspaces) {
                vm.compareWorkspace = _.last(vm.config.RestoreWorkspaces);
            }

            // Load the languages available.
            languageResource.getAll().then(function (languages) {
                vm.languages = languages;
                if (languages.length > 0) {
                    vm.selectedLanguage = languages[0];
                }

                // Load the local content so we can determine if it makes sense to allow language selection.
                contentResource.getById(vm.compareNode.id).then(function (content) {
                    vm.contentHasVariants = content.variants.length > 1;
                    if (!vm.contentHasVariants) {
                        vm.selectedLanguage = null;
                    }

                    // Set permissions for actions.
                    if (_.contains(content.allowedActions, "T")) {
                        vm.canTransfer = true;
                    }

                    if (_.contains(content.allowedActions, "Ø")) {
                        vm.canRestore = true;
                    }

                    // Load the diff library.
                    assetsService.loadJs('lib/jsdiff/diff.js', $scope).then(function () {

                        // Load the comparison data (currently only content items are supported).
                        getContentComparison();
                    });

                });

            });

        }

        function getContentComparison() {

            vm.loading = true;
            vm.comparableContentFound = false;
            deployResource.getContentComparison(vm.compareNode.udi, vm.selectedLanguage ? vm.selectedLanguage.culture : null, vm.compareWorkspace.Url).then(function (data) {
                if (!data.remote) {
                    vm.nonComparableContentMessage = "The selected item does not exist in the " + vm.compareWorkspace.Name + " workspace.";
                } else if (data.local.variantDisplay.state == "NotCreated") {
                    vm.nonComparableContentMessage = "The selected item does not exist in the current workspace in the " + vm.selectedLanguage.name + " language.";
                } else if (data.remote.variantDisplay.state == "NotCreated") {
                    vm.nonComparableContentMessage = "The selected item does not exist in the " + vm.compareWorkspace.Name + " workspace in the " + vm.selectedLanguage.name + " language.";
                } else {
                    createDiff(data.local.variantDisplay, data.remote.variantDisplay);

                    vm.localLastUpdatedOn = data.local.variantDisplay.updateDate;
                    vm.remoteLastUpdatedOn = data.remote.variantDisplay.updateDate;

                    vm.localLastUpdatedBy = data.local.lastEditedByName;
                    vm.remoteLastUpdatedBy = data.remote.lastEditedByName;

                    vm.localParentName = data.local.parentName;
                    vm.remoteParentName = data.remote.parentName;

                    vm.localNumberOfChildren = data.local.numberOfChildren;
                    vm.remoteNumberOfChildren = data.remote.numberOfChildren;

                    vm.localUrl = data.local.url;
                    vm.remoteUrl = data.remote.url;

                    vm.localReleaseDate = data.local.variantDisplay.releaseDate;
                    vm.remoteReleaseDate = data.remote.variantDisplay.releaseDate;

                    vm.localExpireDate = data.local.variantDisplay.expireDate;
                    vm.remoteExpireDate = data.remote.variantDisplay.expireDate;

                    vm.comparableContentFound = true;
                }

                vm.loading = false;
            });

        }

        // Prepares a diff between the local and remote versions.
        // This code has been copied and adapted from rollback comparison feature introduced in CMS 9.something (in rollback.controller.js).
        function createDiff(local, remote) {
            vm.diff = {};
            vm.diff.properties = [];

            // find diff in name
            vm.diff.name = Diff.diffWords(remote.name, local.name);

            // extract all properties from the tabs and create new object for the diff
            local.tabs.forEach(function (tab) {
                tab.properties.forEach(function (property) {
                    var remoteTabIndex = -1;
                    var remoteTabPropertyIndex = -1;
                    var remoteTabs = remote.tabs;

                    // find the property by alias, but only search until we find it
                    for (var oti = 0, length = remoteTabs.length; oti < length; oti++) {
                        var opi = remoteTabs[oti].properties.findIndex(function (p) { return p.alias === property.alias; });
                        if (opi !== -1) {
                            remoteTabIndex = oti;
                            remoteTabPropertyIndex = opi;
                            break;
                        }
                    }

                    if (remoteTabIndex !== -1 && remoteTabPropertyIndex !== -1) {
                        var remoteProperty = remote.tabs[remoteTabIndex].properties[remoteTabPropertyIndex];

                        // copy existing properties, so it doesn't manipulate existing properties on page
                        remoteProperty = Utilities.copy(remoteProperty);
                        property = Utilities.copy(property);

                        // we have to make properties storing values as object into strings (Grid, nested content, etc.)
                        if (property.value instanceof Object) {
                            property.value = stringifyObject(property.value);
                            property.isObject = true;
                        }

                        if (remoteProperty.value instanceof Object) {
                            remoteProperty.value = stringifyObject(remoteProperty.value);
                            remoteProperty.isObject = true;
                        }

                        // diff requires a string
                        property.value = property.value ? property.value + '' : '';
                        remoteProperty.value = remoteProperty.value ? remoteProperty.value + '' : '';

                        var diffProperty = {
                            'alias': property.alias,
                            'label': property.label,
                            'diff': property.isObject ? Diff.diffJson(remoteProperty.value, property.value) : Diff.diffWords(remoteProperty.value, property.value),
                            'isObject': property.isObject || remoteProperty.isObject
                        };

                        vm.diff.properties.push(diffProperty);
                    }
                });
            });
        }

        function stringifyObject(value) {
            return JSON.stringify(value, null, 1)
                .replaceAll(",umb://", ", umb://");    // Add some spacing to allow wrapping when we have lists of node references.
        }

        function changeDestination(workspace) {
            vm.compareWorkspace = workspace;
            getContentComparison();
        }

        function changeLanguage() {
            getContentComparison();
        }

        function queueForTransfer() {
            goToDialog("Queue for transfer", "addtoqueue");
        }

        function partialRestore() {
            goToDialog("Partial restore", "partial-restore");
        }

        function goToDialog(name, view) {
            navigationService.showDialog({
                action: {
                    name: name,
                    metaData: {
                        actionView: "../App_Plugins/Deploy/views/dialogs/" + view + ".html",
                        dialogMode: true
                    }
                },
                node: vm.compareNode
            });
        }

        function close() {
            navigationService.hideDialog();
        }
        
        onInit();
    }

    angular.module("umbraco.deploy").controller("UmbracoDeploy.CompareDialogController", CompareDialogController);
})();

// Note: although not used in this controller, injecting the deploySignalrService is necessary to intialise the SignalR functionality.
// For most installations we don't need it, as the first dashboard viewed under "Content" is the Deploy dashboard, which also uses this and will
// do the initialisation.
// However it's possible to configure a different first dashboard, which will lead to the "Transfer" functionality failing unless either the user
// has already viewed the Deploy dashboard (not guaranteed), or we ensure to also inject and initialise it here (see #39).
angular.module("umbraco.deploy").controller("UmbracoDeploy.DeployDialogController",
  function DeployDialogController($scope, $q, angularHelper, deployHelper, deployService, deployConfiguration, navigationService, editorState, deploySignalrService, $routeParams, treeService, deployResource, notificationsService, dateHelper, userService) {
    var vm = this;
    var timestampFormat = 'MMMM Do YYYY, HH:mm:ss';
    var serverTimestampFormat = 'YYYY-MM-DD HH:mm:ss,SSS';

    vm.config = deployConfiguration;
    vm.currentNode = editorState.current || $scope.currentNode;
    vm.currentNodeName = null;
    vm.deploy = {};
    vm.includeDescendants = false; // Deploying descendants is currently not supported (because this isn't a tree action)
    vm.releaseDate = null;
    vm.releaseDateFormatted = "";
    vm.currentUser = null;
    vm.deployButtonState = 'init';
    vm.feedbackMessageLevel = '';

    vm.languages = [];
    vm.isContent = false;
    vm.contentHasVariants = false;
    vm.loading = true;
    vm.disableTransferNow = false;

    var getIncludedLanguages = function () {
      return vm.languages.filter(function (language) {
        return language.include;
      });
    };

    var nowFormatted = moment().format("YYYY-MM-DD HH:mm");
    vm.releaseDateConfig = {
      enableTime: true,
      dateFormat: "Y-m-d H:i",
      time_24hr: true,
      minDate: nowFormatted,
      defaultDate: nowFormatted
    }

    var loadingPromises = [];

    if (vm.currentNode !== undefined) {
      vm.isContent = vm.currentNode.nodeType == "document";

      // Set node name for display.
      if (vm.currentNode.variants !== undefined && vm.currentNode.variants.length > 0) {
        var i = vm.currentNode.variants.length;
        while (i--) {
          if (vm.currentNode.variants[i].active === true) {
            vm.currentNodeName = vm.currentNode.variants[i].name;
          }
        }

        // Load languages if deploying content.
        if (vm.currentNode.variants.length > 1) {
          vm.contentHasVariants = true;

          if (vm.contentHasVariants) {
            vm.languages = vm.currentNode.variants.filter(function (variant) {
              return !(variant.segment && variant.language);
            }).map(function (variant) {
              // Check variant queue for transfer permissions (only 10.2+ has this defined)
              var allowTransfer = variant.allowedActions === undefined || variant.allowedActions.includes('T');

              return {
                culture: variant.language.culture,
                name: variant.language.name,
                allowTransfer: allowTransfer,
                include: allowTransfer
              };
            });

            // Disable button when no languages are available/selected
            vm.disableTransferNow = getIncludedLanguages().length === 0;
          }
        }

      } else {
        vm.currentNodeName = vm.currentNode.name;// Legacy code
      }
    }

    loadingPromises.push(userService.getCurrentUser().then(function (user) {
      vm.currentUser = user;
    }));

    vm.startInstantDeploy = startInstantDeploy;
    vm.resetDeploy = resetDeploy;
    vm.closeDialog = closeDialog;
    vm.copyDebugToClipboard = copyDebugToClipboard;

    function onInit() {
      // reset the deploy progress
      resetDeploy();
      if (deployService.feedbackMessageLevel) {
        loadingPromises.push(deployService.feedbackMessageLevel().then(function (data) {
          vm.feedbackMessageLevel = data.FeedbackMessageLevel;
        }));
      }

      // Hide loader when everything is completed
      $q.all(loadingPromises).then(function () {
        vm.loading = false;
      });
    };

    vm.languagesChange = function () {
      vm.disableTransferNow = !vm.contentHasVariants || getIncludedLanguages().length === 0;
    };

    vm.releaseDateChange = function (selectedDates, date) {
      if (!date) {
        return;
      }

      // The date being passed in here is the user's local date/time that they have selected.
      // We need to convert this date back to the server date on the model.
      // Note: we are following logic here used in schedule.controller.js in CMS.
      var serverTime = dateHelper.convertToServerStringTime(moment(date), Umbraco.Sys.ServerVariables.application.serverTimeOffset);
      vm.releaseDate = serverTime;
      vm.releaseDateFormatted = dateHelper.getLocalDate(vm.releaseDate, vm.currentUser.locale, "MMM Do YYYY, HH:mm");
    };

    function startInstantDeploy() {
      var node = vm.currentNode;
      node.name = vm.currentNodeName;

      var promises = [], deployItems = [];

      if (vm.contentHasVariants) {
        var includedLanguages = getIncludedLanguages();

        if (vm.languages.length == includedLanguages.length) {
          // Add all languages
          promises.push(deployHelper.getDeployItem(node, "*", vm.includeDescendants, vm.releaseDate, $routeParams, treeService, deployResource).then(function (deployItem) {
            deployItems.push(deployItem);
          }));
        } else {
          // Add all included language variants
          includedLanguages.forEach(function (language) {
            promises.push(deployHelper.getDeployItem(node, language.culture, vm.includeDescendants, vm.releaseDate, $routeParams, treeService, deployResource).then(function (deployItem) {
              deployItems.push(deployItem);
            }));
          });
        }
      } else {
        // Add invariant content
        promises.push(deployHelper.getDeployItem(node, null, vm.includeDescendants, vm.releaseDate, $routeParams, treeService, deployResource).then(function (deployItem) {
          deployItems.push(deployItem);
        }));
      }

      $q.all(promises).then(function () {
        vm.deployButtonState = 'busy';

        deployService.instantDeploy(deployItems, vm.enableWorkItemLogging).then(function () {
          vm.deploy.deployProgress = 0;
          vm.deploy.status = 'inProgress';
          vm.deploy.currentActivity = "Please wait...";
          vm.deploy.timestamp = moment().format(timestampFormat);

          vm.deployButtonState = 'init';

          if (vm.enableWorkItemLogging) {
            vm.deploy.showDebug = true;
          }

        }, function (error) {

          //Catching the 500 error from the request made to the UI/API Controller to trigger an instant deployment
          //Other errors will be caught in 'deploy:sessionUpdated' event pushed out

          //We don't have ClassName in our Exception here but ExceptionType is what we have
          //Push in the value manually into our error/exception object
          error['ClassName'] = error.ExceptionType;

          vm.deploy.status = 'failed';
          vm.deploy.error = {
            hasError: true,
            comment: error.Message,
            exception: error,
            timestamp: moment().format(timestampFormat)
          };

          vm.deployButtonState = 'init';

        });
      });
    };

    function resetDeploy() {
      vm.deploy = {
        'deployProgress': 0,
        'currentActivity': '',
        'status': '',
        'error': {},
        'trace': '',
        'showDebug': false
      };
    };

    function closeDialog() {
      navigationService.hideDialog();
    }

    $scope.$on('deploy:sessionUpdated', function (event, args) {

      // make sure the event is for us
      if (args.sessionId === deployService.sessionId) {
        angularHelper.safeApply($scope, function () {

          vm.deploy.deployProgress = args.percent;
          vm.deploy.currentActivity = args.comment;
          vm.deploy.status = deployHelper.getStatusValue(args.status);
          vm.deploy.timestamp = moment().format(timestampFormat);
          vm.deploy.serverTimestamp = moment(args.serverTimestamp).format(serverTimestampFormat);

          if (vm.deploy.status === 'failed' ||
            vm.deploy.status === 'cancelled' ||
            vm.deploy.status === 'timedOut') {

            vm.deploy.error = {
              hasError: true,
              comment: args.comment,
              log: args.log,
              exception: args.exception
            };
          }
        });
      }

    });

    // signalR heartbeat
    $scope.$on('deploy:heartbeat', function (event, args) {
      if (!deployService.isOurSession(args.sessionId)) return;

      angularHelper.safeApply($scope, function () {
        if (vm.deploy) {
          vm.deploy.timestamp = moment().format(timestampFormat);
          vm.deploy.serverTimestamp = moment(args.serverTimestamp).format(serverTimestampFormat);
        }
      });

    });

    // signalR debug heartbeat
    $scope.$on('deploy:heartbeat', function (event, args) {
      if (!deployService.isOurSession(args.sessionId)) return;
      angularHelper.safeApply($scope, function () {
        vm.deploy.trace += "❤<br />";
      });
    });

    vm.showDebug = function () {
      vm.deploy.showDebug = !vm.deploy.showDebug;
    };

    var search = window.location.search;
    vm.enableWorkItemLogging = search === '?ddebug';

    // debug

    // beware, MUST correspond to what's in WorkStatus
    var workStatus = ["Unknown", "New", "Executing", "Completed", "Failed", "Cancelled", "TimedOut"];

    function updateLog(event, sessionUpdatedArgs) {

      // make sure the event is for us
      if (deployService.isOurSession(sessionUpdatedArgs.sessionId)) {
        angularHelper.safeApply($scope, function () {
          var progress = sessionUpdatedArgs;
          vm.deploy.trace += "" + progress.sessionId.substr(0, 8) + " - " + workStatus[progress.status] + ", " + progress.percent + "%"
            + (progress.comment ? " - <em>" + progress.comment + "</em>" : "") + "<br />";
          if (progress.log)
            vm.deploy.trace += "<br />" + filterLog(progress.log) + "<br /><br />";
          //console.log("" + progress.sessionId.substr(0, 8) + " - " + workStatus[progress.status] + ", " + progress.percent + "%");
        });
      }
    }

    function filterLog(log) {
      log = log.replace(/(?:\&)/g, '&amp;');
      log = log.replace(/(?:\<)/g, '&lt;');
      log = log.replace(/(?:\>)/g, '&gt;');
      log = log.replace(/(?:\r\n|\r|\n)/g, '<br />');
      log = log.replace(/(?:\t)/g, '  ');
      log = log.replace('-- EXCEPTION ---------------------------------------------------', '<span class="umb-deploy-debug-exception">-- EXCEPTION ---------------------------------------------------');
      log = log.replace('----------------------------------------------------------------', '----------------------------------------------------------------</span>');
      return log;
    }

    function copyDebugToClipboard() {
      var trace = vm.deploy.trace;
      trace = trace.replace(/<br\s*\/?>/mg, "\n");
      trace = trace.replace(/<\/?[^>]+(>|$)/g, "");
      navigator.clipboard.writeText(trace);
      notificationsService.success("Technical details copied to clipboard.");
    }

    // note: due to deploy.service also broadcasting at beginning, the first line could be duplicated
    $scope.$on('deploy:sessionUpdated', updateLog);
    $scope.$on('restore:sessionUpdated', updateLog);

    onInit();
  }
);

angular.module('umbraco.deploy').controller('UmbracoDeploy.ExportDialogController',
  function ($scope, $q, deployService, navigationService, notificationsService, deployHelper, deployResource, $routeParams, treeService, userService, deploySignalrService, angularHelper) {

    // Depending on the user's route via the back-office, the SignalR service may not yet be initialized.
    // So we initialize it here to ensure progress is reported.
    deploySignalrService.initialize();

    // Both the "tree" and "workspace" export dialogs use the same controller, as the majority of the code is the same.
    var isTreeExport = false;
    try {
      isTreeExport = angular.element('[ui-view]').attr('ui-view') === "tree-export";
    } catch (e) {
      // Try/catch here just for unit tests, to avoid "Error: selectors not implemented in test/lib/angular.js" error from JQLite.
    }

    var vm = this;

    var timestampFormat = 'MMMM Do YYYY, HH:mm:ss';
    var serverTimestampFormat = 'YYYY-MM-DD HH:mm:ss,SSS';

    vm.exportTriggered = false;
    vm.includeDescendants = false;
    vm.includeContentDependencies = false;
    vm.includeSchemaDependencies = false;
    vm.includeContentFileDependencies = false;
    vm.includeSchemaFileDependencies = false;
    vm.currentUser = null;
    vm.allowSchemaExport = false;

    vm.export = {};
    vm.exportButtonState = "init";
    vm.downloadButtonState = "init";

    var search = window.location.search;
    vm.enableWorkItemLogging = search === '?ddebug';

    vm.lastSessionId = null;
    vm.loading = true;

    var loadingPromises = [];

    loadingPromises.push(userService.getCurrentUser().then(function (user) {
      vm.allowSchemaExport = user.allowedSections.includes("settings");
      vm.currentUser = user;
    }));

    if (isTreeExport) {
      vm.item = $scope.currentNode;

      var treeAlias = treeService.getTreeAlias(vm.item);
      if (vm.item.hasChildren) {
        var routePath = vm.item.routePath;

        loadingPromises.push(deployResource.checkForRegisteredEntityWithPermissionToExport(treeAlias, routePath).then(function (data) {
          vm.withBranch = data.CanExportDescendents;
        }));
      }
    }

    if (deployService.feedbackMessageLevel) {
      loadingPromises.push(deployService.feedbackMessageLevel().then(function (data) {
        vm.feedbackMessageLevel = data.FeedbackMessageLevel;
      }));
    }

    // Only allow explicit closing of the dialog
    navigationService.allowHideDialog(false);

    $q.all(loadingPromises).then(function () {
      vm.loading = false;
    });

    vm.startExport = function () {
      vm.exportButtonState = "busy";
      vm.exportTriggered = true;

      var exportPromise;
      if (isTreeExport) {
        exportPromise = deployHelper.getDeployItem(vm.item, null, vm.includeDescendants, null, $routeParams, treeService, deployResource)
          .then(function (deployItem) {
            return deployService.treeExport(deployItem.id, deployItem.entityType, vm.includeDescendants, vm.includeContentDependencies, vm.includeSchemaDependencies, vm.includeContentFileDependencies, vm.includeSchemaFileDependencies, treeAlias, vm.enableWorkItemLogging);
          });
      } else {
        exportPromise = deployService.export(vm.includeContentDependencies, vm.includeSchemaDependencies, vm.includeContentFileDependencies, vm.includeSchemaFileDependencies, vm.enableWorkItemLogging);
      }

      exportPromise.then(function () {
        vm.export.status = 'inProgress';
        vm.export.exportProgress = 0;
        vm.export.currentActivity = "Please wait...";
        vm.export.timestamp = moment().format(timestampFormat);

        vm.exportButtonState = "init";

        if (vm.enableWorkItemLogging) {
          vm.export.showDebug = true;
        }

        vm.lastSessionId = deployService.sessionId;
      }, function (error) {
        error['ClassName'] = error.ExceptionType;

        vm.export.status = 'failed';
        vm.export.error = {
          hasError: true,
          comment: error.Message,
          exception: error
        };

        vm.exportButtonState = "init";
      });
    };

    vm.showDebug = function () {
      vm.export.showDebug = !vm.export.showDebug;
    };

    vm.copyDebugToClipboard = function () {
      var trace = vm.export.trace;
      trace = trace.replace(/<br\s*\/?>/mg, "\n");
      trace = trace.replace(/<\/?[^>]+(>|$)/g, "");
      navigator.clipboard.writeText(trace);
      notificationsService.success("Technical details copied to clipboard.");
    }

    $scope.$on('deploy:sessionUpdated', function (event, args) {
      // make sure the event is for us
      if (args.sessionId === deployService.sessionId) {

        angularHelper.safeApply($scope, function () {

          vm.export.exportProgress = args.percent;
          vm.export.currentActivity = args.comment;
          vm.export.status = deployHelper.getStatusValue(args.status);
          vm.export.timestamp = moment().format(timestampFormat);
          vm.export.serverTimestamp = moment(args.serverTimestamp).format(serverTimestampFormat);

          if (vm.export.status === 'failed' ||
            vm.export.status === 'cancelled' ||
            vm.export.status === 'timedOut') {
            vm.export.error = {
              hasError: true,
              comment: args.comment,
              log: args.log,
              exception: args.exception
            };
          }
        });
      }
    });

    $scope.$on('deploy:sessionUpdated', updateLog);

    // signalR heartbeat
    $scope.$on('deploy:heartbeat', function (event, args) {
      if (!deployService.isOurSession(args.sessionId)) return;
      angularHelper.safeApply($scope, function () {
        if (vm.export) {
          vm.export.timestamp = moment().format(timestampFormat);
          vm.export.serverTimestamp = moment(args.serverTimestamp).format(serverTimestampFormat);
        }
      });
    });

    var workStatus = ["Unknown", "New", "Executing", "Completed", "Failed", "Cancelled", "TimedOut"];
    function updateLog(event, sessionUpdatedArgs) {

      // make sure the event is for us
      if (deployService.isOurSession(sessionUpdatedArgs.sessionId)) {
        angularHelper.safeApply($scope, function () {
          var progress = sessionUpdatedArgs;
          vm.export.trace += "" + progress.sessionId.substr(0, 8) + " - " + workStatus[progress.status] + ", " + progress.percent + "%"
            + (progress.comment ? " - <em>" + progress.comment + "</em>" : "") + "<br />";
          if (progress.log)
            vm.export.trace += "<br />" + filterLog(progress.log) + "<br /><br />";
        });
      }
    }

    function filterLog(log) {
      log = log.replace(/(?:\&)/g, '&amp;');
      log = log.replace(/(?:\<)/g, '&lt;');
      log = log.replace(/(?:\>)/g, '&gt;');
      log = log.replace(/(?:\r\n|\r|\n)/g, '<br />');
      log = log.replace(/(?:\t)/g, '  ');
      log = log.replace('-- EXCEPTION ---------------------------------------------------', '<span class="umb-deploy-debug-exception">-- EXCEPTION ---------------------------------------------------');
      log = log.replace('----------------------------------------------------------------', '----------------------------------------------------------------</span>');
      return log;
    }

    vm.downloadExport = function () {
      if (!vm.lastSessionId) {
        notificationsService.error("Export", "The exported archive of the selected items could not be downloaded (no session Id).");
        return;
      }

      vm.downloadButtonState = "busy";

      deployService.downloadExport(vm.lastSessionId).then(function () {
        vm.downloadButtonState = "success";
      }, function () {
        vm.downloadButtonState = "error";
      });
    };

    vm.deleteExport = function () {
      if (!vm.lastSessionId) {
        notificationsService.error("Export", "The exported archive of the selected items could not be deleted (no session Id).");
        return;
      }

      deployService.deleteExport(vm.lastSessionId)
        .then(function () {
          notificationsService.success("Export", "The exported archive of the selected items has been deleted.");
          vm.closeDialog();
        }, function () {
          notificationsService.error("Export", "The exported archive of the selected items could not be deleted.");
        });
    };

    vm.closeDialog = function () {
      navigationService.allowHideDialog(true);
      navigationService.hideDialog();
    };
  }
);

angular.module('umbraco.deploy').controller('UmbracoDeploy.ImportDialogController',
  function ($scope, $q, deployService, navigationService, notificationsService, Upload, deployHelper, userService, deploySignalrService, angularHelper, treeService) {

    // Depending on the user's route via the back-office, the SignalR service may not yet be initialized.
    // So we initialize it here to ensure progress is reported.
    deploySignalrService.initialize();

    var vm = this;
    vm.state = "upload";
    vm.warningsAsErrors = true;
    vm.includeContentEntityTypes = true;
    vm.includeSchemaEntityTypes = false;
    vm.includeContentFileEntityTypes = true;
    vm.includeSchemaFileEntityTypes = false;
    vm.allowSchemaImport = false;
    vm.import = {};
    vm.maxFileSizeBytes = Umbraco.Sys.ServerVariables.umbracoSettings.maxFileSize
      ? Umbraco.Sys.ServerVariables.umbracoSettings.maxFileSize * 1024
      : undefined;
    vm.maxFileSize = formatBytes(vm.maxFileSizeBytes);
    vm.enableWorkItemLogging = window.location.search === '?ddebug';
    vm.lastSessionId = null;
    vm.loading = true;
    vm.uploading = null;

    var timestampFormat = 'MMMM Do YYYY, HH:mm:ss';
    var serverTimestampFormat = 'YYYY-MM-DD HH:mm:ss,SSS';
    var loadingPromises = [];

    vm.currentUser = null;
    loadingPromises.push(userService.getCurrentUser().then(function (user) {
      vm.allowSchemaImport = user.allowedSections.includes("settings");
      if (vm.allowSchemaImport) {
        vm.includeSchemaEntityTypes = true;
        vm.includeSchemaFileEntityTypes = true;
      }

      vm.currentUser = user;
    }));

    if (deployService.feedbackMessageLevel) {
      loadingPromises.push(deployService.feedbackMessageLevel().then(function (data) {
        vm.feedbackMessageLevel = data.FeedbackMessageLevel;
      }));
    }

    // Only allow explicit closing of the dialog
    navigationService.allowHideDialog(false);

    $q.all(loadingPromises).then(function () {
      vm.loading = false;
    });

    vm.handleFiles = function (files) {
      if (files && files.length > 0) {
        vm.upload(files[0]);
      }
    };

    vm.upload = function (file) {
      var upload = Upload.upload({
        url: Umbraco.Sys.ServerVariables.umbracoUrls.deployUiBaseUrl + "UploadForImport",
        fields: {},
        file: file
      });

      // Reset error message
      vm.serverErrorMessage = undefined;

      // Store reference to Upload, so we can abort when closing dialog
      vm.uploading = {
        upload: upload,
        progress: 0,
        loaded: 0,
        total: 0
      };

      upload.then(function (resp) {
        if (resp.data.Notifications && resp.data.Notifications.length > 0) {
          // Throw message back to user with the cause of the error
          vm.serverErrorMessage = resp.data.Notifications[0].message;
        } else {
          // Set done status on file
          vm.selectedFileName = resp.data.FileName;
          vm.state = "confirm";
        }
      }, function (resp) {
        if (resp.status == -1) {
          // Ignore aborted upload
        } else if (resp.status === 400) {
          // Validation error
          vm.serverErrorMessage = resp.message;
        } else if (resp.status === 404) {
          // If file not found, server will return a 404
          vm.serverErrorMessage = "File not found";
        } else if (resp.status === 413) {
          // If file is too large, server will return a 413
          vm.serverErrorMessage = "File too large to upload.";
        } else if (resp.data) {
          // It's an unhandled error if the service returns a detailed error
          if (resp.data.InnerException) {
            vm.serverErrorMessage = resp.data.InnerException.ExceptionMessage;

            // Check if its the common "too large file" exception
            if (resp.data.InnerException.StackTrace && resp.data.InnerException.StackTrace.indexOf("ValidateRequestEntityLength") > 0) {
              vm.serverErrorMessage = "File too large to upload.";
            }
          } else if (resp.data.Message) {
            vm.serverErrorMessage = evt.Message;
          }
        }
      }, function (evt) {
        // Update progress
        vm.uploading.progress = parseInt(100.0 * evt.loaded / evt.total);
        vm.uploading.loaded = formatBytes(evt.loaded);
        if (!vm.uploading.total) {
          vm.uploading.total = formatBytes(evt.total);
        }
      });

      upload.finally(function () {
        vm.uploading = null;
      });
    };

    function formatBytes(bytes) {
      if (!+bytes) return '0 bytes';

      const k = 1024;
      const dm = 2;
      const sizes = ['bytes', 'kB', 'MB', 'GB'];
      const i = Math.min(sizes.length - 1, Math.floor(Math.log(bytes) / Math.log(k)));

      return parseFloat((bytes / Math.pow(k, i)).toFixed(dm)) + ' ' + sizes[i];
    }

    vm.startImport = function (warningsAsErrors) {
      if (warningsAsErrors !== undefined) {
        vm.warningsAsErrors = warningsAsErrors;
      }

      vm.import = {};
      vm.importButtonState = "busy";
      vm.state = "inprogress";

      deployService.import(vm.selectedFileName, vm.warningsAsErrors, vm.includeContentEntityTypes, vm.includeSchemaEntityTypes, vm.includeContentFileEntityTypes, vm.includeSchemaFileEntityTypes, vm.enableWorkItemLogging)
        .then(function (data) {
          vm.import.status = 'inProgress';
          vm.import.importProgress = 0;
          vm.import.currentActivity = "Please wait...";
          vm.import.timestamp = moment().format(timestampFormat);

          vm.importButtonState = "init";

          if (vm.enableWorkItemLogging) {
            vm.import.showDebug = true;
          }

          vm.lastSessionId = deployService.sessionId;
        },
          function (error) {
            vm.import.status = 'failed';
            vm.import.error = {
              hasError: true,
              comment: error.Message,
              exception: error,
              softFail: isSoftFail(error)
            };

            vm.importButtonState = "init";
          });
    };

    vm.showDebug = function () {
      vm.import.showDebug = !vm.import.showDebug;
    };

    vm.copyDebugToClipboard = function() {
      var trace = vm.import.trace;
      trace = trace.replace(/<br\s*\/?>/mg, "\n");
      trace = trace.replace(/<\/?[^>]+(>|$)/g, "");
      navigator.clipboard.writeText(trace);
      notificationsService.success("Technical details copied to clipboard.");
    }

    $scope.$on('deploy:sessionUpdated', function (event, args) {
      // make sure the event is for us
      if (args.sessionId === deployService.sessionId) {

        angularHelper.safeApply($scope, function () {

          vm.import.importProgress = args.percent;
          vm.import.currentActivity = args.comment;
          vm.import.status = deployHelper.getStatusValue(args.status);
          vm.import.timestamp = moment().format(timestampFormat);
          vm.import.serverTimestamp = moment(args.serverTimestamp).format(serverTimestampFormat);

          if (vm.import.status === 'failed' ||
            vm.import.status === 'cancelled' ||
            vm.import.status === 'timedOut') {
            vm.import.error = {
              hasError: true,
              comment: args.comment,
              log: args.log,
              exception: args.exception,
              softFail: isSoftFail(args.exception)
            };
          } else {
            if (vm.import.status === 'completed') {
              // Refresh the tree root.
              var treeRoot = treeService.getTreeRoot($scope.currentNode);
              treeService.loadNodeChildren({ node: treeRoot, section: $scope.currentNode.section });
            }
          }
        });
      }
    });

    function isSoftFail(exception) {
      return exception !== null &&
        typeof exception === 'object' &&
        exception.Type === 'Umbraco.Deploy.Core.Exceptions.ImportArtifactsException' &&
        exception.Mismatches.some(function (x) {
          return x.Differences.some(function (y) {
            return y.Category === 'Error';
          });
        }) === false; // Not a soft fail if we have actual errors
    }

    $scope.$on('deploy:sessionUpdated', updateLog);

    // signalR heartbeat
    $scope.$on('deploy:heartbeat', function (event, args) {
      if (!deployService.isOurSession(args.sessionId)) return;
      angularHelper.safeApply($scope, function () {
        if (vm.import) {
          vm.import.timestamp = moment().format(timestampFormat);
          vm.import.serverTimestamp = moment(args.serverTimestamp).format(serverTimestampFormat);
        }
      });
    });

    var workStatus = ["Unknown", "New", "Executing", "Completed", "Failed", "Cancelled", "TimedOut"];
    function updateLog(event, sessionUpdatedArgs) {

      // make sure the event is for us
      if (deployService.isOurSession(sessionUpdatedArgs.sessionId)) {
        angularHelper.safeApply($scope, function () {
          var progress = sessionUpdatedArgs;
          vm.import.trace += "" + progress.sessionId.substr(0, 8) + " - " + workStatus[progress.status] + ", " + progress.percent + "%"
            + (progress.comment ? " - <em>" + progress.comment + "</em>" : "") + "<br />";
          if (progress.log)
            vm.import.trace += "<br />" + filterLog(progress.log) + "<br /><br />";
        });
      }
    }

    function filterLog(log) {
      log = log.replace(/(?:\&)/g, '&amp;');
      log = log.replace(/(?:\<)/g, '&lt;');
      log = log.replace(/(?:\>)/g, '&gt;');
      log = log.replace(/(?:\r\n|\r|\n)/g, '<br />');
      log = log.replace(/(?:\t)/g, '  ');
      log = log.replace('-- EXCEPTION ---------------------------------------------------', '<span class="umb-deploy-debug-exception">-- EXCEPTION ---------------------------------------------------');
      log = log.replace('----------------------------------------------------------------', '----------------------------------------------------------------</span>');
      return log;
    }

    vm.closeDialog = function () {
      navigationService.allowHideDialog(true);
      navigationService.hideDialog();
    };

    // Make sure import file upload is aborted or deleted when closing
    $scope.$on('$destroy', function () {
      if (vm.uploading) {
        vm.uploading.upload.abort();
      } else if (vm.selectedFileName) {
        deployService.deleteImport(vm.selectedFileName);
      }
    });
  }
);

(function () {
    "use strict";

    // Note: see note in deploy.controller.js for the reason injecting deploySignalrService is necessary here, even if not used.
    function PartialRestoreDialogController($scope, deployService, deployResource, angularHelper, deployConfiguration, deployHelper, backdropService, navigationService, editorService, localizationService, treeService, editorState, deploySignalrService) {

        // Depending on the user's route via the back-office, the SignalR service may not yet be initialized.
        // So we initialize it here to ensure progress is reported.
        deploySignalrService.initialize();

        var vm = this;
        var timestampFormat = 'MMMM Do YYYY, HH:mm:ss';
        var serverTimestampFormat = 'YYYY-MM-DD HH:mm:ss,SSS';

        vm.config = deployConfiguration;
        vm.restoreWorkspace = {};
        vm.restore = {};
        vm.restoreButtonState = "init";
        vm.workspaceDropDownOpen = false;

        vm.currentNode = $scope.currentNode || editorState.current; // $scope.currentNode will be null if this dialog has been accessed via the
                                                                    // "compare" dialog, when it was launched from the editor rather than the tree.

        resetRestoreNode();
        vm.toggleIncludeDescendants = function() {
            vm.includeDescendants = !vm.includeDescendants;
        }

        vm.feedbackMessageLevel = '';

        vm.changeDestination = changeDestination;
        vm.startRestore = startRestore;
        vm.resetRestore = resetRestore;
        vm.closeDialog = closeDialog;
        vm.canPickRemoteNode = canPickRemoteNode;

        vm.copyDebugToClipboard = copyDebugToClipboard;

        vm.treeAlias = treeService.getTreeAlias(vm.currentNode);

        // Remote nodes can be picked for content and media, and any plugin registered entities that support it.
        if (isCoreEntitySupportingRemoteTree()) {
            vm.entitySupportsPickRemoteNode = true;
            vm.pickRemoteNodeLabel = "Select " + (vm.treeAlias === "media" ? "media" : "content") + " to restore";
            vm.remoteTreeAlias = vm.treeAlias === "media" ? "externalMedia" : "externalContent";
        } else {
            if (!vm.entitySupportsPickRemoteNode) {
                var routePath = "";
                deployResource.getRegisteredEntityRemoteTreeDetail(vm.treeAlias, routePath)
                    .then(function (data) {
                        if (data.SupportsRemoteMenu) {
                            vm.entitySupportsPickRemoteNode = true;
                            vm.pickRemoteNodeLabel = "Select " + data.NodeName + " to restore";
                            vm.remoteTreeAlias = data.TreeAlias;
                        }
                    });
            }
        }

        var nodeUdis = [];

        vm.pickRemoteNode = pickRemoteNode;

        vm.labels = {};

        localizationService.localizeMany([
            "dialogs_deployRestorePickFrom",
            "dialogs_deployRestoreIncludingDescendants",
            "dialogs_deployRestoreNotIncludingDescendants"
        ]).then(function (data) {
            vm.labels.deployRestorePickFrom = data[0];
            vm.labels.deployRestoreIncludingDescendants = data[1];
            vm.labels.deployRestoreNotIncludingDescendants = data[2];
        });

        function isCoreEntitySupportingRemoteTree() {
            return vm.treeAlias === "content" || vm.treeAlias === "media";
        }

        function resetRestoreNode() {
            vm.restoreNodeIsExternal = false;
            vm.restoreNode = null;

            if (vm.currentNode.id !== "-1") {
                vm.restoreNode = vm.currentNode;

            }
            vm.includeDescendants = true;
        }

        function onInit() {
            // reset restore progress
            resetRestore();

            // set the last workspace to restore from as default
            if(vm.config.RestoreWorkspaces) {
                //var lastWorkspaceIndex = vm.config.Workspaces.length - 1;
                vm.restoreWorkspace = _.last(vm.config.RestoreWorkspaces);//[lastWorkspaceIndex];
            }
            
            if(deployService.feedbackMessageLevel) {
                deployService.feedbackMessageLevel().then(function(data) {
                    vm.feedbackMessageLevel = data.FeedbackMessageLevel;
                });
            }
        }

        function freezeContextMenu() {
            backdropService.open({
                disableEventsOnClick: true,
                element: '#um-deploy-partial-restore-dialog',
                elementPreventClick : true,
            });
            backdropService.setOpacity(0);
        }

        function thawContextMenu() {
            backdropService.close();
        }
        
        function changeDestination(workspace) {
            vm.restoreWorkspace = workspace;
            resetRestoreNode();
        }

        function pickRemoteNode(workspace) {

            navigationService.allowHideDialog(false);

            var partialItemPicker = {
                section: "deploy",
                treeAlias: vm.remoteTreeAlias,
                multiPicker: false,
                title: vm.pickRemoteNodeLabel,
                customTreeParams: "workspace=" + workspace.Url,
                select: function(node) {
                    vm.restoreNodeIsExternal = true;
                    vm.restoreNode = node;
                    editorService.close();
                    navigationService.allowHideDialog(true);
                },
                close: function () {
                    editorService.close();
                    navigationService.allowHideDialog(true);
                }
            };

            editorService.treePicker(partialItemPicker);
        }
        
        function startRestore(workspace) {

            var restoreNodes = [];

            vm.restoreButtonState = "busy";
            freezeContextMenu();

            restoreNodes = [
                {
                    id: vm.restoreNode.id,
                    udi: vm.restoreNode.udi,
                    includeDescendants: vm.includeDescendants,
                    treeAlias: vm.treeAlias
                }
            ];

            deployService.partialRestore(workspace.Url, restoreNodes, vm.enableWorkItemLogging)
                .then(function(data) {

                        vm.restore.status = 'inProgress';
                        vm.restore.restoreProgress = 0;
                        vm.restore.currentActivity = "Please wait...";
                        vm.restore.timestamp = moment().format(timestampFormat);

                        vm.restoreButtonState = "init";

                        if (vm.enableWorkItemLogging) {
                            vm.restore.showDebug = true;
                        }

                    },
                    function (error) {
                        //Catching the 500 error from the request made to the UI/API Controller to trigger an instant deployment
                        //Other errors will be caught in 'restore:sessionUpdated' event pushed out

                        //We don't have ClassName in our Exception here but ExceptionType is what we have
                        //Push in the value manually into our error/exception object
                        error['ClassName'] = error.ExceptionType;

                        vm.restore.status = 'failed';
                        vm.restore.error = {
                            hasError: true,
                            comment: error.Message,
                            exception: error
                        };

                        vm.restoreButtonState = "init";

                    });

        }

        function resetRestore() {
            vm.restore = {
                'restoreProgress': 0,
                'targetName': '',
                'currentActivity': '',
                'status': '',
                'error': {},
                'trace': '',
                'showDebug': false
            };
        }

        $scope.$on('restore:sessionUpdated', function (event, args) {
            // make sure the event is for us
            if (args.sessionId === deployService.sessionId) {

                angularHelper.safeApply($scope, function () {

                    vm.restore.restoreProgress = args.percent;
                    vm.restore.currentActivity = args.comment;
                    vm.restore.status = deployHelper.getStatusValue(args.status);
                    vm.restore.timestamp = moment().format(timestampFormat);
                    vm.restore.serverTimestamp = moment(args.serverTimestamp).format(serverTimestampFormat);

                    if (vm.restore.status === 'failed' ||
                        vm.restore.status === 'cancelled' ||
                        vm.restore.status === 'timedOut') {
                        vm.restore.error = {
                            hasError: true,
                            comment: args.comment,
                            log: args.log,
                            exception: args.exception
                        };
                    } else {
                        if (vm.restore.status === 'completed') {
                            // Refresh the current node and children.
                            navigationService.syncTree({ tree: vm.treeAlias, path: vm.restoreNode.path.split(','), forceReload: true });
                            if (vm.restoreNode.hasChildren) {
                                treeService.loadNodeChildren({ node: vm.restoreNode, section: vm.restoreNode.section });
                            }
                        }
                    }
                });
            }
        });

        // signalR heartbeat
        $scope.$on('restore:heartbeat', function (event, args) {
            if (!deployService.isOurSession(args.sessionId)) return;
            angularHelper.safeApply($scope, function () {
                if(vm.restore) {
                    vm.restore.timestamp = moment().format(timestampFormat);
                    vm.restore.serverTimestamp = moment(args.serverTimestamp).format(serverTimestampFormat);
                }
            });

        });

        vm.selectNode = function (node, event) {
            var newArray = [];
            if (!node.selected) {
                node.selected = true;
                nodeUdis.push(node.Udi);
            } else {
                angular.forEach(nodeUdis, function (nodeUdi) {
                    if (nodeUdi !== node.Udi) {
                        newArray.push(nodeUdi);
                    }
                });
                node.selected = false;
                nodeUdis = newArray;
            }
            event.stopPropagation();
        };

        // signalR debug heartbeat
        $scope.$on('deploy:heartbeat', function (event, args) {
            if (!deployService.isOurSession(args.sessionId)) return;
            angularHelper.safeApply($scope, function () {
                vm.restore.trace += "❤<br />";
            });
        });

        vm.showDebug = function () {
            vm.restore.showDebug = !vm.restore.showDebug;
        };

        var search = window.location.search;
        vm.enableWorkItemLogging = search === '?ddebug';

        // debug

        // beware, MUST correspond to what's in WorkStatus
        var workStatus = ["Unknown", "New", "Executing", "Completed", "Failed", "Cancelled", "TimedOut"];

        function updateLog(event, sessionUpdatedArgs) {

            // make sure the event is for us
            if (deployService.isOurSession(sessionUpdatedArgs.sessionId)) {
                angularHelper.safeApply($scope, function () {
                    var progress = sessionUpdatedArgs;
                    vm.restore.trace += "" + progress.sessionId.substr(0, 8) + " - " + workStatus[progress.status] + ", " + progress.percent + "%"
                        + (progress.comment ? " - <em>" + progress.comment + "</em>" : "") + "<br />";
                    if (progress.log)
                        vm.restore.trace += "<br />" + filterLog(progress.log) + "<br /><br />";
                    //console.log("" + progress.sessionId.substr(0, 8) + " - " + workStatus[progress.status] + ", " + progress.percent + "%");
                });
            }
        }

        function filterLog(log) {
            log = log.replace(/(?:\&)/g, '&amp;');
            log = log.replace(/(?:\<)/g, '&lt;');
            log = log.replace(/(?:\>)/g, '&gt;');
            log = log.replace(/(?:\r\n|\r|\n)/g, '<br />');
            log = log.replace(/(?:\t)/g, '  ');
            log = log.replace('-- EXCEPTION ---------------------------------------------------', '<span class="umb-deploy-debug-exception">-- EXCEPTION ---------------------------------------------------');
            log = log.replace('----------------------------------------------------------------', '----------------------------------------------------------------</span>');
            return log;
        }

        function closeDialog() {
            thawContextMenu();
            navigationService.hideDialog();
        }

        function canPickRemoteNode() {
            return vm.restoreWorkspace && vm.restoreNode !== null && vm.restoreNodeIsExternal === false && vm.entitySupportsPickRemoteNode;
        }

        function copyDebugToClipboard() {
            var trace = vm.restore.trace;
            trace = trace.replace(/<br\s*\/?>/mg, "\n");
            trace = trace.replace(/<\/?[^>]+(>|$)/g, "");
            navigator.clipboard.writeText(trace);
            notificationsService.success("Technical details copied to clipboard.");
        }

        // note: due to deploy.service also broadcasting at beginning, the first line could be duplicated
        $scope.$on('deploy:sessionUpdated', updateLog);
        $scope.$on('restore:sessionUpdated', updateLog);

        onInit();
    }

    angular.module("umbraco.deploy").controller("UmbracoDeploy.PartialRestoreDialogController", PartialRestoreDialogController);
})();

(function () {
    "use strict";

    function RestoreDialogController($scope, $routeParams, deployService, angularHelper, deployConfiguration, deployHelper, backdropService, navigationService, localizationService, treeService, deploySignalrService) {

        // Both the "tree" and "workspace" restore dialogs use the same controller, as the majority of the code
        // is the same.  Where we need different functionality we can use the flag calculated here.
        var isTreeRestore = false;
        try {
            isTreeRestore = angular.element('[ui-view]').attr('ui-view') === "tree-restore";
        } catch (e) {
            // Try/catch here just for unit tests, to avoid "Error: selectors not implemented in test/lib/angular.js" error from JQLite.
        }

        // Depending on the user's route via the back-office, the SignalR service may not yet be initialized.
        // So we initialize it here to ensure progress is reported.
        deploySignalrService.initialize();

        var vm = this;
        var timestampFormat = 'MMMM Do YYYY, HH:mm:ss';
        var serverTimestampFormat = 'YYYY-MM-DD HH:mm:ss,SSS';

        vm.config = deployConfiguration;
        vm.restoreWorkspace = {};
        vm.restore = {};
        vm.restoreButtonState = "init";
        vm.closeDialog = closeDialog;
        vm.feedbackMessageLevel = 'not';
        vm.dropDownOpen = false;

        vm.changeDestination = changeDestination;
        vm.startRestore = startRestore;
        vm.resetRestore = resetRestore;
        vm.feedbackMessageLevel = '';

        vm.copyDebugToClipboard = copyDebugToClipboard;

        vm.labels = {};

        if (localizationService.localizeMany) {
            localizationService.localizeMany([
                "dialogs_deployFullRestoreAction"
            ]).then(function (data) {
                vm.labels.deployFullRestoreAction = data[0];
            });
        }

        function onInit() {

            // reset restore progress
            resetRestore();

            // set the last workspace to restore from as default
            if (vm.config.RestoreWorkspaces) {
                vm.restoreWorkspace = _.first(vm.config.RestoreWorkspaces);
            }

            if (deployService.feedbackMessageLevel) {
                deployService.feedbackMessageLevel().then(function(data) {
                    vm.feedbackMessageLevel = data.FeedbackMessageLevel;
                });
            }
        }
        

        function freezeContextMenu() {
            backdropService.open({
                disableEventsOnClick: true,
                element: '#um-deploy-restore-dialog',
                elementPreventClick : true,
            });
            backdropService.setOpacity(0);
        }

        function thawContextMenu() {
            backdropService.close();
        }

        function changeDestination(workspace) {
            vm.restoreWorkspace = workspace;
        }

        function startRestore(workspace) {

            vm.restoreButtonState = "busy";
            freezeContextMenu();

            var treeAlias = "";
            if (isTreeRestore) {
                treeAlias = treeService.getTreeAlias($scope.currentNode);
            }

            deployService.restore(workspace.Url, treeAlias, vm.enableWorkItemLogging)
                .then(function (data) {

                    vm.restore.status = 'inProgress';
                    vm.restore.restoreProgress = 0;
                    vm.restore.currentActivity = "Please wait...";
                    vm.restore.timestamp = moment().format(timestampFormat);

                    vm.restoreButtonState = "init";

                    if (vm.enableWorkItemLogging) {
                        vm.restore.showDebug = true;
                    }

                },
                function (error) {
                    //Catching the 500 error from the request made to the UI/API Controller to trigger an instant deployment
                    //Other errors will be caught in 'restore:sessionUpdated' event pushed out

                    //We don't have ClassName in our Exception here but ExceptionType is what we have
                    //Push in the value manually into our error/exception object
                    error['ClassName'] = error.ExceptionType;

                    vm.restore.status = 'failed';
                    vm.restore.error = {
                        hasError: true,
                        comment: error.Message,
                        exception: error
                    };

                    vm.restoreButtonState = "init";

                });
        }

        function resetRestore() {
            vm.restore = {
                'restoreProgress': 0,
                'targetName': '',
                'currentActivity': '',
                'status': '',
                'error': {},
                'trace': '',
                'showDebug': false
            };
        }

        $scope.$on('restore:sessionUpdated', function (event, args) {
            // make sure the event is for us
            if (args.sessionId === deployService.sessionId) {

                angularHelper.safeApply($scope, function () {

                    vm.restore.restoreProgress = args.percent;
                    vm.restore.currentActivity = args.comment;
                    vm.restore.status = deployHelper.getStatusValue(args.status);
                    vm.restore.timestamp = moment().format(timestampFormat);
                    vm.restore.serverTimestamp = moment(args.serverTimestamp).format(serverTimestampFormat);

                    if (vm.restore.status === 'failed' ||
                        vm.restore.status === 'cancelled' ||
                        vm.restore.status === 'timedOut') {

                        vm.restore.error = {
                            hasError: true,
                            comment: args.comment,
                            log: args.log,
                            exception: args.exception
                        };
                    } else {
                        if (vm.restore.status === 'completed') {
                            // Refresh the tree root.
                            var treeRoot = treeService.getTreeRoot($scope.currentNode);
                            treeService.loadNodeChildren({ node: treeRoot, section: $scope.currentNode.section });
                        }
                    }
                });
            }
        });

        // signalR heartbeat
        $scope.$on('restore:heartbeat', function (event, args) {
            if (!deployService.isOurSession(args.sessionId)) return;

            angularHelper.safeApply($scope, function () {
                if(vm.restore) {
                    vm.restore.timestamp = moment().format(timestampFormat);
                    vm.restore.serverTimestamp = moment(args.serverTimestamp).format(serverTimestampFormat);
                }
            });

        });

        // signalR debug heartbeat
        $scope.$on('deploy:heartbeat', function (event, args) {
            if (!deployService.isOurSession(args.sessionId)) return;
            angularHelper.safeApply($scope, function () {
                vm.restore.trace += "❤<br />";
            });
        });

        vm.showDebug = function () {
            vm.restore.showDebug = !vm.restore.showDebug;
        };

        var search = window.location.search;
        vm.enableWorkItemLogging = search === '?ddebug';

        // debug

        // beware, MUST correspond to what's in WorkStatus
        var workStatus = ["Unknown", "New", "Executing", "Completed", "Failed", "Cancelled", "TimedOut"];

        function updateLog(event, sessionUpdatedArgs) {

            // make sure the event is for us
            if (deployService.isOurSession(sessionUpdatedArgs.sessionId)) {
                angularHelper.safeApply($scope, function () {
                    var progress = sessionUpdatedArgs;
                    vm.restore.trace += "" + progress.sessionId.substr(0, 8) + " - " + workStatus[progress.status] + ", " + progress.percent + "%"
                        + (progress.comment ? " - <em>" + progress.comment + "</em>" : "") + "<br />";
                    if (progress.log)
                        vm.restore.trace += "<br />" + filterLog(progress.log) + "<br /><br />";
                    //console.log("" + progress.sessionId.substr(0, 8) + " - " + workStatus[progress.status] + ", " + progress.percent + "%");
                });
            }
        }

        function filterLog(log) {
            log = log.replace(/(?:\&)/g, '&amp;');
            log = log.replace(/(?:\<)/g, '&lt;');
            log = log.replace(/(?:\>)/g, '&gt;');
            log = log.replace(/(?:\r\n|\r|\n)/g, '<br />');
            log = log.replace(/(?:\t)/g, '  ');
            log = log.replace('-- EXCEPTION ---------------------------------------------------', '<span class="umb-deploy-debug-exception">-- EXCEPTION ---------------------------------------------------');
            log = log.replace('----------------------------------------------------------------', '----------------------------------------------------------------</span>');
            return log;
        }

        function closeDialog() {
            thawContextMenu();
            navigationService.hideDialog();
        }

        function copyDebugToClipboard() {
            var trace = vm.restore.trace;
            trace = trace.replace(/<br\s*\/?>/mg, "\n");
            trace = trace.replace(/<\/?[^>]+(>|$)/g, "");
            navigator.clipboard.writeText(trace);
            notificationsService.success("Technical details copied to clipboard.");
        }

        // note: due to deploy.service also broadcasting at beginning, the first line could be duplicated
        $scope.$on('deploy:sessionUpdated', updateLog);
        $scope.$on('restore:sessionUpdated', updateLog);

        onInit();
    }
    angular.module("umbraco.deploy").controller("UmbracoDeploy.RestoreDialogController", RestoreDialogController);
})();

angular.module('umbraco.deploy')
    .controller('UmbracoDeploy.AddWorkspaceController',
    [
        function() {
            var vm = this;

            vm.openAddEnvironment = function() {
                //window.open("https://www.s1.umbraco.io/project/" + vm.environment.alias + "?addEnvironment=true");
                alert('not implemented');
            }
        }
    ]);
angular.module('umbraco.deploy')
    .controller('UmbracoDeploy.DoneController',
    [
        'deployConfiguration', 'deployNavigation',
        function (deployConfiguration, deployNavigation) {
            var vm = this;

            vm.targetName = deployConfiguration.targetName;
            vm.targetUrl = deployConfiguration.targetUrl;

            vm.ok = function() {
                deployNavigation.navigate('queue');
            };
        }
    ]);
angular.module('umbraco.deploy')
    .controller('UmbracoDeploy.FlowController',
    [
        function () {
            var vm = this;
        }
    ]);
angular.module('umbraco.deploy')
    .controller('UmbracoDeploy.ProgressController',
    [
        '$scope', 'deployConfiguration', 'deployService', 'deployQueueService', 'deployNavigation',
        function($scope, deployConfiguration, deployService, deployQueueService, deployNavigation) {
            var vm = this;

            vm.progress = 0;

            vm.updateProgress = function(percent) {
                vm.progress = percent;
            };

            vm.deployConfiguration = deployConfiguration;

            $scope.$on('deploy:sessionUpdated', function(event, sessionUpdatedArgs) {

                // make sure the event is for us
                if (sessionUpdatedArgs.sessionId === deployService.sessionId) {

                        vm.progress = sessionUpdatedArgs.percent;
                        if (sessionUpdatedArgs.status === 3) { // Completed
                            deployNavigation.navigate('done-deploy');
                            deployQueueService.clearQueue();
                            deployService.removeSessionId();
                        } else if (sessionUpdatedArgs.status === 4) { // Failed
                            deployService.error = {
                                comment: sessionUpdatedArgs.comment,
                                log: sessionUpdatedArgs.log,
                                status: sessionUpdatedArgs.status
                            };
                            deployNavigation.navigate('error');
                        } else if (sessionUpdatedArgs.status === 5) { // Cancelled
                            deployService.error = {
                                comment: sessionUpdatedArgs.comment,
                                log: sessionUpdatedArgs.log,
                                status: sessionUpdatedArgs.status
                            };
                            deployNavigation.navigate('error');
                        } else if (sessionUpdatedArgs.status === 6) { // Timed out
                            deployService.error = {
                                comment: sessionUpdatedArgs.comment,
                                log: sessionUpdatedArgs.log,
                                status: sessionUpdatedArgs.status
                            };
                            deployNavigation.navigate('error');
                        }
                        else {
                            _.defer(function() { $scope.$apply(); });
                        }
                    }

                });

            // signalR heartbeat
            scope.$on('deploy:heartbeat', function (event, args) {
                if (!deployService.isOurSession(args.sessionId)) return;
                // fixme what shall we do?
                console.log('❤');
            });

            deployService.getStatus();
        }
    ]);
angular.module('umbraco.deploy')
    .controller('UmbracoDeploy.QueueController',
    [
        'deployConfiguration', 'deployQueueService', 'deploySignalrService', 'deployService', 'deployResource', 'deployHelper',
        function(deployConfiguration, deployQueueService, deploySignalrService, deployService, deployResource, deployHelper) {
            var vm = this;
            vm.loading = true;
            var entityTypeToNameMap = {};

            vm.deployConfiguration = deployConfiguration;

            vm.limitToItemAmount = 2;
            vm.showExpandLink = false;

            function init() {
                deployResource.getEntityTypeToNameMap()
                    .then(function (data) {
                        entityTypeToNameMap = data.Map;
                    });
                vm.loading = false;
            }

            init();

            vm.items = deployQueueService.queue;

            vm.startDeploy = function() {
                deployService.deploy(vm.items);
            };

            vm.clearQueue = function() {
                deployQueueService.clearQueue();
            };

            vm.removeFromQueue = function (item) {
                deployQueueService.removeFromQueue(item);
            };

            vm.refreshQueue = function() {
                deployQueueService.refreshQueue();
            };

            vm.restore = function() {
                deployService.restore();
            };

            vm.getEntityName = function (entityType) {
                return deployHelper.getEntityName(entityType, entityTypeToNameMap);
            };
        }
    ]);

angular.module('umbraco.deploy')
    .controller('UmbracoDeploy.WorkspaceInfoController',
        function() {
            var vm = this;
        });
angular.module('umbraco.deploy')
    .controller('UmbracoDeploy.SchemaComparisonDetailController',
    [
        '$scope', 'assetsService', 'deployManagementResource',
        function ($scope, assetsService, deployManagementResource) {

            var vm = this;

            vm.title = $scope.model.title;
            vm.subtitle = $scope.model.subtitle;
            vm.close = $scope.model.close;

            vm.loading = true;

            // Load the diff library.
            assetsService.loadJs('lib/jsdiff/diff.js', $scope).then(function () {

                // Load the comparision data.
                deployManagementResource.getSchemaComparisonForEntity($scope.model.udi).then(function (data) {

                    if (!data.artifactFromUmbraco) {
                        vm.nonComparableContentMessage = "No data exists in Umbraco for the selected item. Showing the information from the .uda file only.";
                        vm.singleArtifactDisplay = stringify(data.artifactFromFile);
                    } else if (!data.artifactFromFile) {
                        vm.nonComparableContentMessage = "No .uda file exists for the selected item. Showing the information from Umbraco only.";
                        vm.singleArtifactDisplay = stringify(data.artifactFromUmbraco);
                    } else {
                        createDiff(data.artifactFromUmbraco, data.artifactFromFile);
                        vm.comparableContentFound = true;
                    }

                    vm.loading = false;
                });
            });

            function createDiff(artifactFromUmbraco, artifactFromFile) {
                vm.artifactDiff = Diff.diffWords(stringify(artifactFromUmbraco), stringify(artifactFromFile));
            }

            function stringify(artifact) {
                return JSON.stringify(artifact, null, 4);
            }
        }
    ]);
