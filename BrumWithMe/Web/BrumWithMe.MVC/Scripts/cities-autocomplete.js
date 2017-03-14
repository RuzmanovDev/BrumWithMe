$(document).ready(function () {
    $.getJSON("/api/city/all", function (recivedData) {

        var result = {};

        var cityNames = recivedData.data.forEach(function (element) {
            result[element] = null;
        });

        $('input.autocomplete').autocomplete({
            data: result,
            limit: 20, // The max amount of results that can be shown at once. Default: Infinity.
        });
    });
});
