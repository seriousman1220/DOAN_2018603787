angular.module('MainPageApp', []).controller('MainPageController', ['$scope', '$http', function ($scope, $http) {
    console.log('MainPageController: ready');

   

    $scope.LoadData = function () {
        $http({
            method: 'GET',
            url: '/API/api/myAPI/LoadData/'
        }).then(function (response) {
            $scope.departments = response.data.departments;
            $scope.status_list = response.data.status_list;
            $scope.courses = response.data.courses;
            for (var i = 0; i < $scope.courses.length; i++) {
                $scope.courses[i].from_date = new Date($scope.courses[i].from_date);
                $scope.courses[i].to_date = new Date($scope.courses[i].to_date);
            }

            $scope.emp_list = response.data.dmchitieus;
            for (var i = 0; i < $scope.emp_list.length; i++) {
                $scope.emp_list[i].course.from_date = new Date($scope.emp_list[i].course.from_date);
                $scope.emp_list[i].course.to_date = new Date($scope.emp_list[i].course.to_date);
                $scope.emp_list[i].rating_date = new Date($scope.emp_list[i].rating_date);
            }
            console.log($scope.emp_list);
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
        $scope.LoadData();
    }
    $scope.init();
}]);