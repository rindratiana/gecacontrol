﻿// Please see documentation at https://learn.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.
$(document).ready(function() {    
    $.ajax({
        url: '/Home/GetMap', 
        method: 'GET',
        dataType: 'json',
        success: function (responseStr) {
            console.log(responseStr);
            UpdateMap(responseStr);
        },
        error: function (jqXHR, textStatus, errorThrown) {
            console.log('AJAX request failed: ' + textStatus + ', ' + errorThrown);
        }
    });
});

$(document).ready(function () {
    $('#btnStep').click(function () {
        var controlValue = $('#control').val();
        var nbrStepValue = $('#nbrStep').val();
        if (controlValue && nbrStepValue) {
            $.ajax({
                url: '/Home/Control',  // Replace with your endpoint URL
                method: 'POST',
                data:{
                    direction: controlValue,
                    step: parseInt(nbrStepValue, 10)
                },
                success: function (response) {
                    UpdateMap(response);
                },
                error: function (error) {
                    console.error('Error:', error);
                }
            });
        }
    });
});

function UpdateMap(responseString) {
    var caterpillar = responseString.caterpillarJson;
    var response = JSON.parse(responseString.mapJson);

    var maxX = response.length;
    var maxY = response.reduce((max, row) => Math.max(max, row.length), 0);

    var $table = $('<table>');

    for (var y = maxY - 1; y >= 0; y--) {
        var $row = $('<tr>');

        for (var x = 0; x < maxX; x++) {
            var cell = response[x][y];
            if (x == caterpillar.head.x && y == caterpillar.head.y) {
                var $cell = $('<td style="background-color: green;">').text(caterpillar.head.value);
                $row.append($cell);
            }
            else if (x == caterpillar.tail.x && y == caterpillar.tail.y) {
                var $cell = $('<td style="background-color: green;">').text(caterpillar.tail.value);
                $row.append($cell);
            }
            else {
                var $cell = $('<td>').text(cell.Value);
                $row.append($cell);
            }
        }

        $table.append($row)
    }
    $('#matrix-display').empty().append($table);
}