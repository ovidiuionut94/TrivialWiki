﻿(function(angular) {
    'use strict';

    angular.module('triviaModule')
    .service('triviaService', ['$q', '$http', function ($q, $http) {

        this.getTriviaTables = function() {
            var def = $q.defer();
            $http.get(App.url + '/triviaTables')
                .success(function(data) {
                    def.resolve(data);
                })
                .error(function(data) {
                    def.reject(data);
                });

            return def.promise;
        }

        this.addNewFriend = function(username) {
            var def = $q.defer();
            $http.post(App.url + '/addFriend/' + username)
                .success(function (data) {
                    def.resolve(data);
                })
                .error(function (data) {
                    def.reject(data);
                });

            return def.promise;
        }

        this.getTableUsers = function (tableName) {
            var def = $q.defer();
            $http.get(App.url + '/getUsersFromTable/' + tableName)
                .success(function (data) {
                    def.resolve(data);
                })
                .error(function (data) {
                    def.reject(data);
                });

            return def.promise;
        }

        this.getTableTopic = function(tableName) {
            var def = $q.defer();
            $http.get(App.url + '/getTableTopic/' + tableName)
                .success(function (data) {
                    def.resolve(data);
                })
                .error(function (data) {
                    def.reject(data);
                });

            return def.promise;
        }

        this.getActiveTopics = function() {
            var def = $q.defer();
            $http.get(App.url + '/activeTopics')
                .success(function (data) {
                    def.resolve(data);
                })
                .error(function (data) {
                    def.reject(data);
                });

            return def.promise;
        }

        this.createNewTable = function(tableName, topic) {
            var def = $q.defer();
            $http.post(App.url + '/createTable/' + tableName + '/' +topic)
                .success(function (data) {
                    def.resolve(data);
                })
                .error(function (data) {
                    def.reject(data);
                });

            return def.promise;
        }

        this.getNextQuestion = function() {
            var def = $q.defer();
            $http.get(App.url + '/getNextQuestion')
                .success(function (data) {
                    def.resolve(data);
                })
                .error(function (data) {
                    def.reject(data);
                });

            return def.promise;
        }

        this.getCurrentQuestion = function (tableName) {
            var def = $q.defer();
            $http.get(App.url + '/getCurrentQuestion/' + tableName)
                .success(function (data) {
                    def.resolve(data);
                })
                .error(function (data) {
                    def.reject(data);
                });

            return def.promise;
        }

        this.getUserFriends = function() {
            var def = $q.defer();
            $http.get(App.url + '/getFriends')
                .success(function (data) {
                    def.resolve(data);
                })
                .error(function (data) {
                    def.reject(data);
                });

            return def.promise;
        }

        

        //this.getMessages = function(skip) {
        //    var def = $q.defer();
        //    $http.get(App.url + '/getMessages/' + skip)
        //        .success(function(data) {
        //            def.resolve(data);
        //        })
        //        .error(function(data) {
        //            def.reject(data);
        //        });

        //    return def.promise;
        //};
        //this.sendMessage = function(message, sender) {
        //    var def = $q.defer();
        //    var param = {
        //        Message: message,
        //        UserName: sender
        //    }
        //    $http({
        //        url: App.url + '/addMessage',
        //        method: 'POST',
        //        params: param
        //    })
        //   .success(function (data) {
        //       def.resolve(data);
        //   })
        //   .error(function (data) {
        //       def.reject(data);
        //   });

        //    return def.promise;
        //};

        //this.getTriviaHistory = function () {
        //    var def = $q.defer();
        //    $http.get(App.url + '/getLastQuestions')
        //        .success(function (data) {
        //            def.resolve(data);
        //        })
        //        .error(function (data) {
        //            def.reject(data);
        //        });

        //    return def.promise;
        //};

        this.sendAnswer = function(message, sender) {
            var def = $q.defer();
            var param = {
                messageText: message,
                Sender: sender
            }
            $http({
                url: App.url + '/addResponse',
                method: 'POST',
                params: param
            })
           .success(function (data) {
               def.resolve(data);
           })
           .error(function (data) {
               def.reject(data);
           });

            return def.promise;
        }

        this.addNewTopic = function(topic) {
            var def = $q.defer();

            $http.post( App.url + '/topic/'+topic)
           .success(function (data) {
               def.resolve(data);
           })
           .error(function (data) {
               def.reject(data);
           });

            return def.promise;
        }

    }]);
}).call(this, this.angular);