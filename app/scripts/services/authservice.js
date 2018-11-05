'use strict';

angular
    .module('onlineClientApp')
    .factory('authService', ['$http', '$q', 'ngAuthSettings', 'localStorageService',
        function ($http, $q, ngAuthSettings, localStorageService) {

            var serviceBase = ngAuthSettings.apiServiceBaseUri;

            var authentication = {
                isAuth: false,
                email: "",
                useRefreshTokens: false
            };

            var service = {
                saveRegistration: saveRegistration,
                logIn: logIn,
                logOut: logOut,
                authentication: authentication,
                getRoles: getRoles,
                getUsers: getUsers,
                getUsersDetails: getUsersDetails,
                getPoliceStations: getPoliceStations,
                getLoggedUserEmail: getLoggedUserEmail
            };

            return service;

            function saveRegistration(registration) {
                logOut();
                return $http.post(serviceBase + 'api/users/signup', registration).then(function (response) {
                    return response;
                });
            };

            function logIn(login) {
                //Investigate error handling in Angularjs
                return $http.post(serviceBase + 'api/users/login', login).then(function (response) {

                    if (login.useRefreshTokens) {
                        localStorageService.set('authorization', { token: response.access_token, email: login.email, refreshToken: response.refresh_token, useRefreshTokens: true });
                    }
                    else {
                        localStorageService.set('authorization', { token: response.access_token, email: login.email, refreshToken: "", useRefreshTokens: false });
                    }
                    authentication.isAuth = true;
                    authentication.email = login.email;
                    authentication.useRefreshTokens = login.useRefreshTokens;

                    return response;
                });
            };

            function getLoggedUserEmail() {
                return localStorageService.authorization.email;
            }

            function getRoles() {
                return $http.get(serviceBase + 'api/users/roles').then(function (results) {
                    return results;
                });
            }

            function getPoliceStations() {
                return $http.get(serviceBase + 'api/users/roles').then(function (results) {
                    return results;
                });
            }

            function getUsers() {
                return $http.get(serviceBase + 'api/users').then(function (results) {
                    return results;
                });
            }

            function getUsersDetails(email) {
                return $http.get(serviceBase + 'api/users/user?email=' + email).then(function (results) {
                    return results;
                });
            }

            function getPoliceStations() {
                return $http.get(serviceBase + '/api/Incident/policestations').then(function (results) {
                    return results;
                });
            }

            function logOut() {
                localStorageService.remove('authorization');
                authentication.isAuth = false;
                authentication.email = "";
                authentication.useRefreshTokens = false;
            };

        }]);
