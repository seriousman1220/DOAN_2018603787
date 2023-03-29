var app = angular.module('myApp', []);

const pre_ct_id = 'CT';

app.controller('myCtrl', ['$scope', '$http', function ($scope, $http) {


    $scope.filter = {};
    $scope.filter.courses = '';
    $scope.filter.department = '';
    $scope.filter.rating_date = new Date();
    $scope.Add_Yn = true;
    $scope.Add_ct_yn = true;
    $scope.Filter_Yn = false;

    //Khoi tao ds
    function LoadData() {
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
        }, function (error) {
            console.log('Error: ' + error);
        });
    }
    LoadData();

    //Chon tat ca cac dong
    $scope.SelectAllRow = function () {
        for (var i = 0; i < $scope.emp_list.length; i++) {
            $scope.emp_list[i].selected = $scope.select_all;
        }
    };

    //Hien thi modal 1 (them)
    $scope.ShowAddEmpForm = function () {
        $scope.Add_Yn = true;
        $scope.ResetEmpForm();
        $scope.nemp.total = $scope.nemp.department.bonus_mark;
        $('#emp_modal').modal('show');
    };


    //Hien thi modal 2 (sua)
    $scope.ShowEditEmpForm = function () {
        let count = 0;
        for (var i = $scope.emp_list.length - 1; i >= 0; i--) {
            if ($scope.emp_list[i].selected)
                count++;
        }
        if (count == 1) {
            for (var i = $scope.emp_list.length - 1; i >= 0; i--) {
                if ($scope.emp_list[i].selected) {
                    $scope.Detail(i);
                    break;
                }
            }
        }
        else {
            alert('Vui lòng chọn một dòng để sửa!');
        }
    }


    //Hien thi modal 2 (them)
    $scope.ShowAddCtForm = function (id) {
        ct_yn = true;
        $scope.ClickCtSave = false;
        $scope.nct = {};
        var id = MaxCtID();
        if (parseInt(id.split('CT')[1]) < 9)
            id = pre_ct_id + '0' + (parseInt(id.split('CT')[1]) + 1);
        else
            id = pre_ct_id + (parseInt(id.split('CT')[1]) + 1);
        $scope.nct.id = id;
        $scope.Add_ct_yn = true;
        $('#ct_modal').modal('show');
    };


    $scope.Detail = function (index) {
        $scope.ClickSave = false;
        $scope.Add_Yn = false;
        $scope.nemp = angular.copy($scope.emp_list[index]);
        $('#emp_modal').modal('show');
    };

    $scope.DetailCt = function (index) {
        ct_yn = true;
        $scope.ClickCtSave = false;
        $scope.Add_ct_yn = false;
        $scope.nct = angular.copy($scope.nemp.ct[index]);
        $('#ct_modal').modal('show');
    };

    $scope.Remove = function (index) {
        if (confirm('Bạn có muốn xóa dòng này không?')) {
            $http({
                method: 'GET',
                url: '/API/api/myAPI/RemoveEmp/',
                params: { id_khoadt: $scope.emp_list[index].course.id, id_emp: $scope.emp_list[index].employee.id }
            }).then(function (response) {
                $scope.emp_list = response.data;
                for (var i = 0; i < $scope.emp_list.length; i++) {
                    $scope.emp_list[i].course.from_date = new Date($scope.emp_list[i].course.from_date);
                    $scope.emp_list[i].course.to_date = new Date($scope.emp_list[i].course.to_date);
                    $scope.emp_list[i].rating_date = new Date($scope.emp_list[i].rating_date);
                }
            }, function (error) {
                console.log('Error: ' + error.status);
            });
        }
    };

    $scope.RemoveCt = function (index) {
        if (confirm('Bạn có muốn xóa chỉ tiêu này không?')) {
            $scope.nemp.ct.splice(index, 1);
            $scope.TinhTongDiem();
        }
    };

    $scope.RemoveAllSelected = function () {
        var selected_emp = [];
        for (var i = $scope.emp_list.length - 1; i >= 0; i--) {
            if ($scope.emp_list[i].selected) {
                selected_emp.push($scope.emp_list[i]);
            }
        }
        if (selected_emp.length == 0) {
            alert('Vui lòng chọn dòng cần xóa!');
        }
        else {
            if (confirm('Bạn có muốn xóa những dòng này không?'))
                $http({
                    method: 'POST',
                    url: '/API/api/myAPI/RemoveSelectedEmp',
                    data: selected_emp
                }).then(function (response) {
                    $scope.emp_list = response.data;
                    for (var i = 0; i < $scope.emp_list.length; i++) {
                        $scope.emp_list[i].course.from_date = new Date($scope.emp_list[i].course.from_date);
                        $scope.emp_list[i].course.to_date = new Date($scope.emp_list[i].course.to_date);
                        $scope.emp_list[i].rating_date = new Date($scope.emp_list[i].rating_date);
                    }
                }, function (error) {
                    console.log('Error: ' + error.status);
                });
        }
    };

    $scope.ResetEmpForm = function () {
        $scope.ClickSave = false;
        $scope.nemp = {};
        $scope.nemp.course = $scope.courses[0];
        $scope.nemp.department = $scope.departments[0];
        $scope.nemp.status = $scope.status_list[0];
    };

    $scope.SaveEmp = function () {
        $scope.ClickSave = true;
        if ($scope.emp_form.$valid) {
            if ($scope.Add_Yn) {
                if (CheckDuplicatedKey()) {
                    SaveChangesEmp();
                    $('#emp_modal').modal('hide');
                }
                else $scope.ResetEmpForm();
            }
            else {
                SaveChangesEmp();
                $('#emp_modal').modal('hide');
            }
        }
    };

    $scope.SaveCopyEmp = function () {
        $scope.ClickSave = true;
        if ($scope.emp_form.$valid) {
            if (CheckDuplicatedKey()) {
                SaveChangesEmp();
                $scope.ResetEmpForm();
                $scope.ClickSave = false;
            }
            else $scope.ResetEmpForm();
        }
    };

    function SaveChangesEmp() {
        var nemp = angular.copy($scope.nemp);
        nemp.rating_date = getDDMMYYYY(nemp.rating_date);
        $http({
            method: 'POST',
            url: '/API/api/myAPI/SaveChangesEmp',
            data: nemp
        }).then(function (response) {
            $scope.emp_list = response.data;
            for (var i = 0; i < $scope.emp_list.length; i++) {
                $scope.emp_list[i].course.from_date = new Date($scope.emp_list[i].course.from_date);
                $scope.emp_list[i].course.to_date = new Date($scope.emp_list[i].course.to_date);
                $scope.emp_list[i].rating_date = new Date($scope.emp_list[i].rating_date);
            }
        }, function (error) {
            console.log('Error: ' + error);
        });
    };

    function CheckDuplicatedKey() {
        for (var i = 0; i < $scope.emp_list.length; i++) {
            if ($scope.emp_list[i].employee.id.toLowerCase() == $scope.nemp.employee.id.toLowerCase().trim()
                && $scope.emp_list[i].course.id == $scope.nemp.course.id) {
                alert('Không được thêm trùng khóa! \nVui lòng nhập lại!');
                return false;
                break;
            }
        }
        return true;
    };

    $scope.SaveCt = function () {
        $scope.ClickCtSave = true;
        if ($scope.ct_form.$valid) {
            if ($scope.nct.diem >= 0 && $scope.nct.diem <= 10) {
                if ($scope.TinhTongDiem1()) {
                    if (!$scope.Add_ct_yn) {
                        for (var i = 0; i < $scope.nemp.ct.length; i++) {
                            if ($scope.nemp.ct[i].id == $scope.nct.id) {
                                $scope.nemp.ct[i] = angular.copy($scope.nct);
                            }
                        }
                    }
                    else {
                        if (!$scope.nemp.ct) $scope.nemp.ct = [];
                        $scope.nemp.ct.push($scope.nct);
                    }
                    $('#ct_modal').modal('hide');
                    ct_yn = false;
                    $scope.TinhTongDiem();
                }
            }
            else {
                alert('Điểm phải thuộc khoảng từ 0 -> 10!');
            }
        }
    };

    $scope.SaveAddCt = function () {
        $scope.ClickCtSave = true;
        if ($scope.ct_form.$valid) {
            if ($scope.nct.diem >= 0 && $scope.nct.diem <= 10) {
                if ($scope.TinhTongDiem1()) {
                    if (!$scope.nemp.ct) $scope.nemp.ct = [];
                    var nct = angular.copy($scope.nct);
                    $scope.nemp.ct.push(nct);
                    $scope.ClickCtSave = false;
                    $scope.TinhTongDiem();
                    ct_yn = false;
                    $scope.nct = {};
                    var id = MaxCtID();
                    if (parseInt(id.split('CT')[1]) < 9)
                        id = pre_ct_id + '0' + (parseInt(id.split('CT')[1]) + 1);
                    else
                        id = pre_ct_id + (parseInt(id.split('CT')[1]) + 1);
                    $scope.nct.id = id;
                }
            }
            else {
                alert('Điểm phải thuộc khoảng từ 0 -> 10!');
            }
        }
    };

    function MaxCtID() {
        var max = 'CT0';
        if ($scope.nemp.ct) {
            max = $scope.nemp.ct[$scope.nemp.ct.length - 1].id;
            for (var i = $scope.nemp.ct.length - 2; i >= 0; i--) {
                if ($scope.nemp.ct[i].id > max) max = $scope.nemp.ct[i].id;
            }
        }

        return max;
    };

    $scope.TinhDiemQD = function () {
        var diem_qd = 0;
        if (!$scope.nct.diem || !$scope.nct.he_so) {
            diem_qd = 0;
        }
        else {
            if ($scope.nct.diem >= 0 && $scope.nct.diem <= 10)
                diem_qd = $scope.nct.diem * $scope.nct.he_so;
        }
        if (diem_qd >= 0 && diem_qd <= 10) {
            $scope.nct.diem_qd = diem_qd;
            $scope.disabled = false;
        }
        else {
            alert('Điểm quy đổi vượt quá mức cho phép!');
            $scope.disabled = true;
        }
    };

    $scope.TinhTongDiem = function () {
        var total = $scope.nemp.department.bonus_mark;
        if ($scope.nemp.ct) {
            for (var i = 0; i < $scope.nemp.ct.length; i++) {
                total += $scope.nemp.ct[i].diem_qd;
            }
        }

        if (total < 0 || total > 10) {
            alert('Tổng điểm vượt quá mức cho phép!');
        }
        else {
            $scope.nemp.total = total;
        }
    };

    $scope.TinhTongDiem1 = function () {
        var total = $scope.nemp.department.bonus_mark + $scope.nct.diem_qd;
        if ($scope.nemp.ct) {
            for (var i = 0; i < $scope.nemp.ct.length; i++) {
                total += $scope.nemp.ct[i].diem_qd;
            }
        }

        if (total < 0 || total > 10) {
            alert('Tổng điểm vượt quá mức cho phép!');
            return false;
        }

        return true;
    };

    $scope.enterSearch = function (keyEvent) {
        if (keyEvent.which === 13) {
            $scope.Search($scope.key_search);
        }
    };

    $scope.Search = function (key_search) {
        key_search = key_search ? key_search : '';
        key_search = key_search.toLowerCase().replace(/\s+/g, ' ');

        $http({
            method: 'GET',
            url: '/API/api/myAPI/Search',
            params: { key_search: key_search }
        }).then(function (response) {
            $scope.emp_list = response.data;
            for (var i = 0; i < $scope.emp_list.length; i++) {
                $scope.emp_list[i].course.from_date = new Date($scope.emp_list[i].course.from_date);
                $scope.emp_list[i].course.to_date = new Date($scope.emp_list[i].course.to_date);
                $scope.emp_list[i].rating_date = new Date($scope.emp_list[i].rating_date);
            }
        }, function (error) {
            console.log('Error: ' + error);
        });
    };

    $scope.AdvancedSearch = function () {
        $http({
            method: 'GET',
            url: '/API/api/myAPI/AdvancedSearch',
            params: {
                course: $scope.filter.courses,
                department: $scope.filter.department,
                rating_date: $scope.filter.rating_date,
                from_mark: $scope.filter.from_mark ? $scope.filter.from_mark : 0,
                to_mark: $scope.filter.to_mark ? $scope.filter.to_mark : 10
            }
        }).then(function (response) {
            $scope.emp_list = response.data;
            for (var i = 0; i < $scope.emp_list.length; i++) {
                $scope.emp_list[i].course.from_date = new Date($scope.emp_list[i].course.from_date);
                $scope.emp_list[i].course.to_date = new Date($scope.emp_list[i].course.to_date);
                $scope.emp_list[i].rating_date = new Date($scope.emp_list[i].rating_date);
            }
        }, function (error) {
            console.log('Error: ' + error);
        });
    };

    $scope.RefeshData = function () {
        LoadData();
        $scope.key_search = '';
    };

    function getDDMMYYYY(dateString) {
        var day = ("0" + dateString.getDate()).slice(-2);
        var month = ("0" + (dateString.getMonth() + 1)).slice(-2);

        var date = (month) + "/" + (day) + "/" + dateString.getFullYear();

        return date;
    };


}]);