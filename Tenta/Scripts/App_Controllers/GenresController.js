(function () {
    "use strict";

    angular.module("app").controller("GenresController", function ($scope, $http, $location) {

        var getGenres = function () {
            $http({
                url: "odata/Genres",
                method: "GET"
            })
                .success(function (data) {
                    $scope.Genres = data.value;
                });
        }

        getGenres();

        $scope.addGenre = function (genre) {

            $http({
                url: "odata/Genres",
                method: "POST",
                data: $scope.genre
            })
                .success(function (data) {
                    getGenres();
                });
        }

    });
})();