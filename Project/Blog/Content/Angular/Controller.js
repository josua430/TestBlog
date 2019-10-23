app.controller("myCntrl", function ($scope, myService) {
  
    $scope.LoginCheck = function () {
       
        var User = {
            UserName: $scope.uName,
            Password: $scope.password
        };
        $("#divLoading").show();
        var getData = myService.UserLogin(User);
        getData.then(function (msg) {
            if (msg.data == "0") {
                $("#divLoading").hide();
                $("#alertModal").modal('show');
                $scope.msg = "¡Usuario no autorizado!";
                clearFields();
            }
            else if (msg.data == "-1") {
                $("#divLoading").hide();
                $("#alertModal").modal('show');
                $scope.msg = "¡Usuario o contraseña incorrecta!";
                $scope.password = '';
            }
            else if (msg.data == "2") {
                $("#divLoading").hide();
                $("#alertModal").modal('show');
                $scope.msg = "¡Usuario o perfil no activo!";
                $scope.password = '';
            }
            else {
                uID = msg.data;
                $("#divLoading").hide();
                window.location.href = "/Home/Index";
            }
        });
        
    }
    $scope.alertmsg = function () {
        $("#alertModal").modal('hide');
    };
   
    function clearFields() {
        $scope.uName = '';
        $scope.password = '';
    }

});

