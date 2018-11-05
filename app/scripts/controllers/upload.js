'use strict';

angular
    .module('onlineClientApp')
    .controller("uploadCtrl", ['$scope', '$http', 'uploadService', 'authService',
        function ($scope, $http, uploadService, authService) {

        $scope.upload = {
            id: 1,
            image: "",
            index: 0,
            measureType: "",
            police_Station: ""
        };

        $scope.measureTypes = ["PREDICTION", "KERNEL"];
 
            $scope.getImage = function () {
                if (($scope.upload.measureType != undefined)
                    && ($scope.upload.index != undefined)
                    && ($scope.upload.police_Station != undefined))
            {
                    uploadService.getImage($scope.upload.measureType
                    , $scope.upload.index, $scope.upload.police_Station)
                        .then(function (results) {
                            $scope.upload.id = results.data.id;
                            $scope.filepreview = "data:image/png;base64," + results.data.image;
                    });  
            }
        }

        $scope.getdIndexes = function () {
                if ($scope.upload.measureType == "PREDICTION")
                    $scope.indexes = [{
                        "id": 1,
                        "name": "Jan"
                    }, {
                        "id": 2,
                        "name": "Feb"
                    }, {
                        "id": 3,
                        "name": "Mar"
                    }, {
                        "id": 4,
                        "name": "May"
                    }, {
                        "id": 5,
                        "name": "Jun"
                    }, {
                        "id": 6,
                        "name": "Jul"
                    }, {
                        "id": 7,
                        "name": "Aug"
                    }, {
                        "id": 8,
                        "name": "Sep"
                    }, {
                        "id": 9,
                        "name": "Oct"
                    }, {
                        "id": 10,
                        "name": "Nov"
                    }, {
                        "id": 11,
                        "name": "Dec"
                    }];
                else
                    $scope.indexes = [{
                        "id": 1,
                        "name": "Q1"
                    }, {
                        "id": 2,
                        "name": "Q2"
                    }, {
                        "id": 3,
                        "name": "Q3"
                    }, {
                        "id": 4,
                        "name": "Q4"
                    }];
            $scope.getImage()
            }

        $scope.uploadFile = function () {

           $scope.upload.image = $scope.filepreview.replace(/data:image\/jpeg;base64,/g, '');
            uploadService.upload($scope.upload).then(function (response) {
                $scope.savedSuccessfully = true;
                $scope.message = "File uploaded successfully";
                startTimer();
            })
        };

        authService.getPoliceStations()
            .then(function (results) {
                $scope.policeStations = results.data;
            });

    }])
        
