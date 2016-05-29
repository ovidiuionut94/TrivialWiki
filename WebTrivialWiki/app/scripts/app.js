(function (angular) {
    'use strict';

    App.module = angular.module('webTrivialWikiApp',
        ['adminModule', 'triviaModule', 'ngAnimate', 'ngCookies','angularFileUpload',
            'ngResource', 'ngRoute', 'ngSanitize', 'ngTouch', 'ui.bootstrap', 'angular-growl'])
        .config(function ($routeProvider) {
            $routeProvider
            .when('/', {
                templateUrl: 'views/about.html'
            })
            .when('/settings',
            {
                templateUrl: 'scripts/User/Settings/settings.html',
                controller:'settingsController'
            })
            .when('/somethingWrong', {
                templateUrl: 'views/somethingWrong.html'
            })
            .otherwise({
                redirectTo: '/'
            });
        });

    App.module.run(['$http', 'persistService','$window', function ($http, persistService, $window) {
        var authToken = persistService.readData('securityToken');
        if (authToken === undefined) {
            $window.location.href = 'http://localhost:9000/';
        }
        $http.defaults.headers.common.Authorization = authToken;
    }]);

    App.module.factory('responseErrorInterceptor', ['$q', '$location', function ($q, $location) {
            var responseErrorMarker = {
                'responseError': function (rejection) {
                    if (rejection.status === 500 || rejection.status === 400) {
                        $location.path('/somethingWrong');
                    }
                }
            };
            return responseErrorMarker;
        }])
        .config([
            '$httpProvider', function ($httpProvider) {
                $httpProvider.interceptors.push('responseErrorInterceptor');
            }
        ]);

    App.module.config(['growlProvider', function (growlProvider) {
        growlProvider.globalTimeToLive(3000);
    }]);

}).call(this, this.angular);