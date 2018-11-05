'use strict';

angular
    .module('onlineClientApp')
    .controller('DetailCtrl', ['$scope', '$routeParams', function ($scope, $routeParams) {

        var myLatlng = new google.maps.LatLng($routeParams.latitude,
            $routeParams.longitude);

            var mapOptions = {
                zoom: 14,
                center: myLatlng,
                mapTypeId: google.maps.MapTypeId.ROADMAP
            }

            $scope.map = new google.maps.Map(document.getElementById('map'), mapOptions);

            $scope.markers = [];
    
            var infoWindow = new google.maps.InfoWindow();

            (function () {
                var marker = new google.maps.Marker({
                    map: $scope.map,
                    position: myLatlng
                })

                marker.content = '<div class="infoWindowContent">' + '</div>';

                google.maps.event.addListener(marker, 'click', function () {
                    infoWindow.setContent('<h2>' + marker.title + '</h2>' + marker.content);
                    infoWindow.open($scope.map, marker);
                });

                $scope.markers.push(marker);
            });

        }]);
