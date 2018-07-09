angular.module('app')
    .factory('pagesservice',
        function ($http) {

            var factory = {}
            factory.GetPage = function (pageId) {
                return $http.get('http://localhost:82/api/pages/all?id=' + pageId + '&includeMarks=true');
            }
            return factory;
        }
    );