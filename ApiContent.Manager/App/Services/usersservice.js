angular.module('app')
    .factory('usersservice',
        function ($http, $window) {

            var factory = {}
            factory.Login = function(userName, pwd) {
                return $http({
                        url: 'http://localhost:82/token',
                        method: 'post',
                        data: $.param({ grant_type: 'password', username: userName, password: pwd }),
                        headers: { 'Content-Type': 'application/x-www-form-urlencoded' }
                    }).
                    then(function(response) {
                        $window.sessionStorage.setItem('currentuser', response.data.access_token);
                        return response.data;
                    });
            }

            factory.Logout = function() {
                $window.sessionStorage.removeItem('currentuser');
            }

            return factory;
        }
    );