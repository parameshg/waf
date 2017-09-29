app.controller("CountryController", function ($rootScope, $scope, $api, $timeout) {

    $scope.countries = [];

    $scope.policies = [];

    $scope.load = function () {
        $api.getCountries(function (response) {
            $api.getPolicies(function (response) {
                $scope.policies = response.data
            }, function (error) {
                $rootScope.error(error.data.Message);
            });
            $timeout(function () {
                $scope.countries = response.data;
            }, 1000);
        }, function (error) {
            $rootScope.error(error.data.Message);
        });
    }

    $scope.initToggle = function (country) {
        $timeout(function () {
            $("#toggle-" + country).bootstrapToggle();
            for (var i = 0; i < $scope.policies.length; i++) {
                if ($scope.policies[i].Country === country) {
                    if ($scope.policies[i].Permission === true) {
                        $("#toggle-" + country).bootstrapToggle('on')
                    } else {
                        $("#toggle-" + country).bootstrapToggle('off')
                    }
                }
            }
            $("#toggle-" + country).change(function () {
                var permission = $("#toggle-" + country).prop("checked");
                $api.savePolicy(country, permission, function (response) {
                    if (permission === true) {
                        $rootScope.success("location access granted");
                    }
                    else {
                        $rootScope.success("location access revoked");
                    }
                }, function (error) {
                    $rootScope.error(error.data.Message);
                });
            });
        }, 10);
    };

    $scope.load();
});