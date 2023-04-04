angular.module('LoginApp', ['angular-growl']).controller('LoginController', ['$scope', '$http', 'growl', function ($scope, $http, growl) {
    console.log('LoginController: ready');


    $scope.CheckLogin = function () {

        var user_check = {
            user: $scope.username,
            password: $scope.password,
        };
        $http({
            method: 'POST',
            url: '/API/api/Login/CheckLogin',
            data: {
                'username': $scope.username,
                'password': $scope.password,
            }
        }).then(function (response) {
            console.log(response.data);
            growl.info(response.data);

        })
    }

   
}]);