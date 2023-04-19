var app = angular.module('LoginApp', ['angular-growl', 'ngRoute']);
app.controller('LoginController', ['$scope', '$http', 'growl', '$window', function ($scope, $http, growl, $window) {
    console.log('LoginController: ready');


    $scope.CheckLogin = function () {

        var user_check = {
            user_name: $scope.username ? $scope.username : 'blank',
            pass: $scope.password ? $scope.password : 'blank',
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
                if (response.data == "OK") {
                    growl.info("Đăng kí thành công");
                    $window.location.href = '/Admin/LoginPage';

                }
                else {
                    growl.error("Thông tin đăng kí đã tồn tại!");
                }
            })

        }
        else {
            growl.error("Xác nhận mật khẩu thất bại!");
            return false;

        }
    }

    $scope.ResetPassword = function () {
        var regex_email = /^[\w-\.]+@([\w-]+\.)+[\w-]{2,4}$/;
        if ($scope.email_check.match(regex_email)) {
            var user_check = {
                email: $scope.email_check ? $scope.email_check : 'a@gmail.com',
            };
            $http({
                method: 'POST',
                url: '/API/api/Login/ResetPassword',
                data: user_check
            }).then(function (response) {
                console.log(response.data);
                if (response.data == "OK") {
                    growl.info("Cấp lại mật khẩu thành công!");


                }
                else {
                    growl.error("Email không tồn tại");
                }


            })
        }
        else {
            growl.error("Sai định dạng email");
            return false;

        }
        
    }
}]);