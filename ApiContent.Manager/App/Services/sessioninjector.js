angular.module('app')
    .factory('sessionInjector', 
    function ($window) {
        var sessionInjector = {
            request: function (config) {
                var token = $window.sessionStorage.getItem('currentuser');
                if (token) {
                    config.headers['Authorization'] = 'Bearer ' + token;
                }
            return config;
        }
    };
    return sessionInjector;
    });

angular.module('app').config(['$httpProvider', function ($httpProvider) {
    $httpProvider.interceptors.push('sessionInjector');
}]);