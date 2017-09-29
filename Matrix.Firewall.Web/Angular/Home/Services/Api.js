app.service("$api", function ($http) {

    this.getCountries = function (successCallback, errorCallback) {
        $http.get(api + "/countries")
        .then(function success(response) {
            successCallback(response);
        }, function error(response) {
            errorCallback(response);
        });
    };

    this.getPolicies = function (successCallback, errorCallback) {
        $http.get(api + "/policies")
        .then(function success(response) {
            successCallback(response);
        }, function error(response) {
            errorCallback(response);
        });
    };

    this.savePolicy = function (country, permission, successCallback, errorCallback) {
        var config = { 'Content-Type': 'application/json;' };
        var data = {
            country: country,
            permission: permission
        };
        $http.post(api + "/policy", data, config)
            .then(function success(response) {
                successCallback(response);
            }, function error(response) {
                errorCallback(response);
            });
    };

    this.purgePolicy = function (country, successCallback, errorCallback) {
        $http.delete(api + "/policy/" + country)
            .then(function success(response) {
                successCallback(response);
            }, function error(response) {
                errorCallback(response);
            });
    };
});