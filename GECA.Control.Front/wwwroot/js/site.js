﻿// Please see documentation at https://learn.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.
$(document).ready(function() {    
    $.ajax({
        url: '/Home/GetMap', 
        method: 'GET',
        dataType: 'json',
        success: function (responseStr) {
            UpdateMap(responseStr);
        },
        error: function (jqXHR, textStatus, errorThrown) {
            console.log('AJAX request failed: ' + textStatus + ', ' + errorThrown);
        }
    });
});

function UpdateMap(responseString) {
    var response = JSON.parse(responseString);

    var maxX = response.length;
    var maxY = response.reduce((max, row) => Math.max(max, row.length), 0);

    var $table = $('<table>');

    for (var y = maxY - 1; y >= 0; y--) {
        var $row = $('<tr>');

        for (var x = 0; x < maxX; x++) {
            var cell = response[x][y];

            var $cell = $('<td>').text(cell.Value);
            $row.append($cell);
        }

        $table.append($row)
    }
    $('#matrix-display').append($table);
}