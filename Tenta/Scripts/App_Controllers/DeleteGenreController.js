(function () {
    "use strict";

    angular.module("app").controller("DeleteGenreController", function ($scope, $http, $routeParams, $location) {

        var getGenre = function () {
            $http({
                url: "odata/Genres('" + $routeParams.GenreId + "')",
                method: "GET"
            })
                .success(function (data) {
                    $scope.genre = data;
                });
        }

        $scope.deleteNotGenre = function () {
            $location.path('/');
        }

        $scope.deleteGenre = function (genre) {
            $http({
                url: "odata/Genres('" + genre.GenreId + "')",
                method: "DELETE"
            })
                .success(function (data) {
                    $location.path('/');
                });
        }

        getGenre();

    });
})();