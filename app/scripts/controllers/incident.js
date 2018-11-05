'use strict';

angular
    .module('onlineClientApp')
    .controller('IncidentCtrl', ['$scope', '$location', '$timeout', 'predictService', '$filter', 
        function ($scope, $location, $timeout, predictService, $filter) {

            $scope.startDate = "";
            $scope.endDate = "";

            $scope.data = [];

            predictService.getIncidents($scope.startDate, $scope.endDate)
                .then(function (results) {
                    $scope.data = results.data;
                });

            $scope.loadMap = function (latitude, longitude) {
                $location.path('/detail')
                    .search({
                        latitude: latitude

                    });
            };

            $scope.loadSignup = function (emailAddress) {
                $location.path('/signup').search({ email: emailAddress });
            };
            
            $scope.applyFilter = function () {

                $scope.startDate = $filter('date')($scope.startDate, 'yyyy-MM-dd HH:mm:ss')
                $scope.endDate = $filter('date')($scope.endDate, 'yyyy-MM-dd HH:mm:ss')

                predictService.getIncidents($scope.startDate, $scope.endDate).then(function (results) {
                    $scope.data = results.data;
                })
            };
            
        }]);
