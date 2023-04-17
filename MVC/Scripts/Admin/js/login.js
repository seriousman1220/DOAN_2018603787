var app = angular.module('LoginApp', ['angular-growl', 'ngRoute']);
app.controller('LoginController', ['$scope', '$http', 'growl', '$window', function ($scope, $http, growl, $window) {
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
            $window.location.href = '/Home/MainPage';
            sessionStorage.username = $scope.username;
            sessionStorage.qty_cart = 10;


        })
    }

   
}]);