angular
        .module('app')
        .controller('pagescontroller', pagescontroller);

pagescontroller.$inject = [
    'pagesservice', '$sce', '$timeout'];

function pagescontroller(pagesservice, $sce, $timeout) {
    var vm = this;
    vm.pagedata;

    vm.GetPage = function () {
        pagesservice.GetPage(2).
            then(function (response) {
                vm.pageData = $sce.trustAsHtml(response.data.Content);
                }, 
                function (error) {
                vm.pageData = "Cannot get data";
            }
            );
    }

    vm.MouseOver = function(event) {
        angular.element(event.srcElement || event.target).addClass('areaHighlight');
    }

    vm.MouseLeave = function (event) {
        angular.element(event.srcElement || event.target).removeClass('areaHighlight');
    }

    return vm;

}