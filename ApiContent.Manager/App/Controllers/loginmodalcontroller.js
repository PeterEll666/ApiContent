angular
    .module('app')
    .controller('loginmodalcontroller',
//    loginmodalcontroller);

//loginmodalcontroller.$inject = ['$uibModalInstance', 'usersservice'];
        function loginmodalcontroller($uibModalInstance, $q, usersservice) {
            var vm = this;
            vm.username;
            vm.password;

            vm.ok = function() {
                usersservice.Login(vm.username, vm.password).
                    then(function(response) {
                        $uibModalInstance.close(vm.username);
                        }, function(error) {
                            toastr.error("Login Failed ");
                        }
                    );
            }

            vm.cancel = function() {
                $uibModalInstance.close();
            }

            //return vm;

        }
    );
