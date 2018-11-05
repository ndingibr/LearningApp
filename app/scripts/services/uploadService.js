'use strict';

angular
    .module('onlineClientApp')
    .factory('uploadService',
    [
        '$http', '$q', 'ngAuthSettings',
        function ($http, $q, ngAuthSettings) {

            var serviceBase = ngAuthSettings.apiServiceBaseUri;

            var service = {
                upload: upload,
                getImage: getImage
            };

            return service;

            function upload(upload) {
                return $http.post(serviceBase + 'api/measures/save', upload).then(function (response) {
                    return response;
                });
            };

            function getImage(measureType, index, police_station ) {
                return $http.get(serviceBase + 'api/measures/getimage?measureType=' + measureType
                    + '&index=' + index
                    + '&police_station=' + police_station)
                    .then(function (results) {
                        return results;
                    });
            }
        }
    ]);