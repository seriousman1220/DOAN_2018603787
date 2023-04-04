angular.module('MainPageApp', []).controller('MainPageController', ['$scope', '$http', function ($scope, $http) {
    console.log('MainPageController: ready');

    //Khai báo
    $scope.user = '';
    $scope.login_yn = false;

    $scope.LoadData = function () {
        $http({
            method: 'GET',
            url: '/API/api/MainPage/LoadData/'
        }).then(function (response) {
            $scope.courses = response.data.courses;
            console.log(response.data.courses);
        }, function (error) {
            console.log('Error: ' + error);
        });
    }

    $scope.GetNhomSP = function () {
    }

    $scope.GetListSP = function () {

    }

    $scope.GetGioHang = function () {

    }

    $scope.init = function () {
        if (sessionStorage.username === undefined) {
            $scope.LoadData();
        }
        else {
            $scope.login_yn = true;
            $scope.user = sessionStorage.username;
            $scope.qty_cart = sessionStorage.qty_cart == null ? 0 : sessionStorage.qty_cart;
            $scope.LoadData();

        }
    }
    $scope.init();
}]);