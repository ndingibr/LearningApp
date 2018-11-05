'use strict';

angular
    .module('onlineClientApp')
    .factory('predictService',
    [
        '$http', '$q', 'ngAuthSettings',
        function ($http, $q, ngAuthSettings) {

            var serviceBase = ngAuthSettings.apiServiceBaseUri;

            var service = {
                getAttributes: getAttributes,
                submitAttributes: submitAttributes,
                getTrainData: getTrainData,
                getIncidents: getIncidents
            };

            return service;

            function getAttributes() {
                return $http.get(serviceBase + 'api/prediction/getattributes')
                    .then(function (results) {
                        return results;
                    });
            };

            function submitAttributes(attributes) {
                return $http.post(serviceBase + 'api/prediction/submitattributes', attributes).then(function (response) {
                    return response;
                });
            };

            function getTrainData(startDate, endDate) {
                return $http.get(serviceBase + 'api/data?startDate=' + startDate + '&endDate=' + endDate)
                    .then(function (results) {
                        return results;
                    });
            };

            function getIncidents(startDate, endDate) {
                return $http.get(serviceBase + 'api/incident')
                    .then(function (results) {
                        return results;
                    });
            };

        }
    ]);
