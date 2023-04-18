var app = angular.module('LoginApp', ['angular-growl', 'ngRoute']);
app.controller('LoginController', ['$scope', '$http', 'growl', '$window', function ($scope, $http, growl, $window) {
    console.log('LoginController: ready');


    $scope.CheckLogin = function () {

        var user_check = {
            username: $scope.username ? $scope.username : 'blank',
            password: $scope.password,
        };
        $http({
            method: 'POST',
            url: '/API/api/Login/CheckLogin',
            data: user_check
        }).then(function (response) {
            console.log(response.data);
            if (response.data == 1) {
                $window.location.href = '/Home/MainPage';
                sessionStorage.username = $scope.username;
                sessionStorage.qty_cart = 0;

            }
            else {
                growl.error("Thông tin đăng nhập chưa chính xác");
            }


        })
    }

    $scope.CreateAccount = function () {
        if ($scope.pass === $scope.retype_pass) {
            var data_save = {
                user_name: $scope.user_name,
                pass: $scope.pass,
                ten: $scope.ten,
                email: $scope.email,
            };
            $http({
                method: 'POST',
                url: '/API/api/Login/CreateAccount',
                data: data_save
            }).then(function (response) {
                console.log(response.data);
                if (response.data == 1) {
                    growl.info("Đăng kí thành công");
                    $window.location.href = '/Admin/LoginPage';
                    
                }
                else {
                    growl.error("Thông tin đăng nhập chưa chính xác");
                }


            })

        }
    }

   
}]);