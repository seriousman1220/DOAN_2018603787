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
            $scope.qty_cart = sessionStorage.qty_cart == null ? 0 : sessionStorage.qty_cart;
            $scope.total = sessionStorage.total == null ? 0 : sessionStorage.total;

        }, function (error) {
            console.log('Error: ' + error);
        });
    }

    $scope.GetNhomSP = function () {
        //Trả về nhóm vt ở đầu web
    }

    $scope.GetListSP = function () {
        //API trả về các list sản phẩm
        //List sản phẩm bán chạy
        // 1.List sản phẩm mới
        // 2.List sản phẩm giảm giá
        // 3.List sản phẩm tươi sống 
    }

    $scope.GetGioHang = function () {
        //Trả về thông tin giỏ hàng theo session
    }

    $scope.init = function () {
        if (sessionStorage.username === undefined) {
            $scope.LoadData();
        }
        else {
            $scope.login_yn = true;
            $scope.user = sessionStorage.username;
            $scope.LoadData();

        }
    }
    $scope.init();

    $scope.Search = function () {
        console.log($scope.search_key);
        //Truyền tham số vào search_key, xử lý và chuyển hướng đến danh mục sản phẩm
    }

    $scope.SendEmailToClient = function () {
        console.log("Send Email!");
        //API xử lý Gửi email đến khách hàng muốn quan tâm đến trang web
    }


}]);