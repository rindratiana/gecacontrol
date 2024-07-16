// Please see documentation at https://learn.microsoft.com/aspnet/core/client-side/bundling-and-minification
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

$(document).ready(function () {
    $('#btnStep').click(function () {
        var controlValue = $('#control').val();
        var nbrStepValue = $('#nbrStep').val();
        if (controlValue && nbrStepValue) {
            $.ajax({
                url: '/Home/Control',
                method: 'POST',
                data:{
                    direction: controlValue,
                    step: parseInt(nbrStepValue, 10)
                },
                success: function (response) {
                    if (response.message === "booster") {
                        Swal.fire({
                            title: 'Information',
                            text: 'Do you want to increase or decrease the caterpillar\'s size? It has just eaten a booster.',
                            icon: 'warning',
                            showCancelButton: true,
                            confirmButtonText: 'Yes',
                            cancelButtonText: 'No'
                        }).then((result) => {
                            if (result.isConfirmed) {
                                $.ajax({
                                    url: '/Home/GrowCaterpillar',
                                    type: 'POST',
                                    dataType: 'json',
                                    data: { statusGrow: 1 },
                                    success: function (response) {
                                        UpdateMap(response);
                                    },
                                    error: function (xhr, status, error) {
                                        console.error('Error:', error);
                                        alert('Error occurred while processing growth request');
                                        // Handle errors
                                    }
                                });
                            } else if (result.dismiss === Swal.DismissReason.cancel) {
                                Swal.fire(
                                    'Cancelled',
                                    'Your action has been cancelled',
                                    'error'
                                );
                            }
                        });
                    }
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
    console.log(caterpillar);

    var response = JSON.parse(responseString.mapJson);

    var maxX = response.length;
    var maxY = response.reduce((max, row) => Math.max(max, row.length), 0);

    var $table = $('<table>');

    for (var y = maxY - 1; y >= 0; y--) {
        var $row = $('<tr>');

        for (var x = 0; x < maxX; x++) {
            var cell = response[x][y];
            var $cell;

            if (x == caterpillar.head.x && y == caterpillar.head.y) {
                $cell = $('<td style="background-color: green;">').text(caterpillar.head.value);
            }
            else if (x == caterpillar.tail.x && y == caterpillar.tail.y) {
                $cell = $('<td style="background-color: green;">').text(caterpillar.tail.value);
            }
            else {
                $cell = $('<td>').text(cell.Value);
            }

            if (caterpillar.intermediate.length > 0) {
                $.each(caterpillar.intermediate, function (index, item) {
                    if (x == item.x && y == item.y) {
                        $cell = $('<td style="background-color: green;">').text(item.value);
                    }
                });
            }

            $row.append($cell);
        }
        $table.append($row);
    }

    $('#matrix-display').empty().append($table);
}