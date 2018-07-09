angular
    .module('app')
    .controller('userscontroller',
//        userscontroller);

//userscontroller.$inject = ['usersservice', '$window', '$uibModal'];
        function userscontroller(usersservice, $window, $uibModal) {
            var vm = this;
            vm.loggedinas = '';

            vm.OpenModal = function() {
                var modalInstance = $uibModal.open({
                    templateUrl: 'App/Templates/LoginModalTemplate.html',
                    controller: 'loginmodalcontroller',
                    controllerAs: 'vm',
                    ariaLabelledBy: 'modal-title',
                    ariaDescribedBy: 'modal-body'
                });

                modalInstance.result.then(function(username) {
                    vm.loggedinas = 'Logged in as : ' + username;
                });

            }

            vm.Logout = function() {
                usersservice.Logout();
                vm.loggedinas = '';
            }

            vm.Login = function(username, password) {
            }


            //return vm;

        });