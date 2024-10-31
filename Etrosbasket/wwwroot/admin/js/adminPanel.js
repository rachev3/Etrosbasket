// showing all players
$(document).ready(function () {
    $('#playersLink').on('click', function (e) {
        e.preventDefault();
        $('#mainContent').load('/Player/Players');
    });
});

//deleting player button
$(document).on('click', '.delete-player', function (e) {
    e.preventDefault();

    var playerId = $(this).data('player-id');

    $.ajax({
        url: '/Player/Delete',
        type: 'POST',
        data: { playerId: playerId },
        success: function (response) {
            // Reload the Players list
            $('#mainContent').load('/Player/Players');
        },
        error: function (xhr, status, error) {
            console.error("Error deleting player: ", error);
        }
    });
});

//player detials button
$(document).on('click', '.details-player', function (e) {
    e.preventDefault();
    var playerId = $(this).data('player-id');

    $.ajax({
        url: '/Player/Details', // Adjust to your controller and action
        type: 'GET',
        data: { playerId: playerId },
        success: function (response) {
            console.log("ALLOOOO");
            $('#modalContent').html(response);
            $('#playerDetailsModal').modal('show');
        },
        error: function (xhr, status, error) {
            console.error("Error loading player details:", error);
            console.log("Status:", status);
            console.log("Response:", xhr.responseText);
        }
    });
});

