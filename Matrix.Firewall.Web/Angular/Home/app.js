var app = angular.module("app", ["ngRoute"]);

app.config(function ($routeProvider, $locationProvider) {
    $locationProvider.hashPrefix('');
    $routeProvider
    .when("/", { templateUrl: "Home/Splash", controller: "HomeController" })
    .when("/country", { templateUrl: "Home/CountryRestrictions", controller: "CountryController" })
    .when("/address", { templateUrl: "Home/AddressRestrictions", controller: "AddressController" })
    .when("/datetime", { templateUrl: "Home/DateTimeRestrictions", controller: "DatetimeController" })
    .otherwise({ redirectTo: "/" });
});

app.run(function ($rootScope) {

    toastr.options = {
        "closeButton": true,
        "debug": false,
        "newestOnTop": true,
        "progressBar": false,
        "positionClass": "toast-bottom-right",
        "preventDuplicates": true,
        "onclick": null,
        "showDuration": "300",
        "hideDuration": "1000",
        "timeOut": "5000",
        "extendedTimeOut": "1000",
        "showEasing": "swing",
        "hideEasing": "linear",
        "showMethod": "fadeIn",
        "hideMethod": "fadeOut"
    }

    $rootScope.success = function (message) {
        toastr["success"]("", message);
    };

    $rootScope.error = function (message) {
        toastr["error"]("", message);
    }
})