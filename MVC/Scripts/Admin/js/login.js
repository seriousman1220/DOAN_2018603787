angular.module('LoginApp', []).controller('LoginController', ['$scope', '$http', function ($scope, $http) {
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

        })
    }

   
}]);