angular.module('ListProductApp', ['ngRoute']).controller('ListProductController', ['$scope', '$http', function ($scope, $http) {
    console.log('ListProductController: ready');

    //Khai báo
    $scope.user = '';
    $scope.login_yn = false;

    $scope.LoadData = function () {
        //API trả về nhóm sản phẩm, list sản phẩm (limit 15)
        //Xử lý phân trang (nếu có)
    }

    $scope.GetSPTheoNhomSP = function(){

    }

    $scope.ThemSP = function () {
        //API xử lý thêm sản phẩm vào giỏ hàng theo session
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