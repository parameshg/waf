app.controller("HomeController", function ($rootScope, $scope, $api) {

    //$scope.id = 0;
    //$scope.name = "";
    //$scope.description = "";
    //$scope.scope = 0;
    //$scope.protocol = 0;
    //$scope.project = "MVSFW";
    //$scope.release = "Release";
    //$scope.assetLocation = "";
    //$scope.liveEndpoint = "";
    //$scope.virtualEndpoint = "";
    //$scope.tab = "list";
    //$scope.pattern = window.sessionStorage.getItem("console-search");

    //$scope.search = function () {
    //    window.sessionStorage.setItem("console-search", $scope.pattern);
    //    $api.getProviders(function (response) {
    //        $rootScope.providers = response.data;
    //    }, function (response) {
    //        $rootScope.error("Providers cannot be loaded");
    //    });
    //    $api.search($scope.pattern, function (response) {
    //        $rootScope.services = response.data;
    //    }, function (response) {
    //        $rootScope.error("Services cannot be loaded");
    //    });
    //}

    //$scope.create = function () {
    //    $api.createService($scope.consumerId, $scope.providerId, $scope.versionId, $scope.name, $scope.description, $scope.scope, $scope.protocol, $scope.project, $scope.release, $scope.assetLocation, $scope.liveEndpoint, $scope.virtualEndpoint, function (response) {
    //        LoadServices();
    //        $rootScope.success("Service created successfully");
    //    }, function (response) {
    //        $rootScope.error("Service cannot be created");
    //    });
    //}

    //$scope.save = function (consumerId, providerId, versionId, id, name, description, scope, protocol, project, release, assetLocation, liveEndpoint, virtualEndpoint) {
    //    name = $("#service-" + id + "-name").val();
    //    description = $("#service-" + id + "-description").val();
    //    virtualEndpoint = $("#service-" + id + "-virtualEndpoint").val();
    //    $api.updateService(consumerId, providerId, versionId, id, name, description, scope, protocol, project, release, assetLocation, liveEndpoint, virtualEndpoint, function (response) {
    //        $rootScope.success("Service updated successfully");
    //    }, function (response) {
    //        $rootScope.error("Service cannot be updated");
    //    });
    //}

    //$scope.delete = function (id) {
    //    $api.deleteService(id, function (response) {
    //        $rootScope.success("Service deleted successfully");
    //    }, function (response) {
    //        $rootScope.error("Service cannot be deleted");
    //    });
    //}
});