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

//create player modal load
$(document).on('click', '#createPlayerButton', function (e) {
    e.preventDefault();

    $.ajax({
        url: '/Player/Create', // Adjust to your controller and action
        type: 'GET',
        success: function (response) {
            $('#createModalContent').html(response); // Load form content into the modal
            $('#createPlayerModal').modal('show'); // Show the modal
        },
        error: function (xhr, status, error) {
            console.error("Error loading create player modal:", error);
            console.log("Status:", status);
            console.log("Response:", xhr.responseText);
        }
    });
});

// create player submit
$(document).on('submit', '#createPlayerForm', function (e) {
    e.preventDefault();

    $.ajax({
        url: '/Player/CreateSubmit', // Adjust to the correct action and controller
        type: 'POST',
        data: $(this).serialize(),
        success: function (response) {
            $('#createPlayerModal').modal('hide'); // Hide the modal on success
            $('.modal-backdrop').remove(); // Remove any remaining modal backdrop
            $('#mainContent').load('/Player/Players');
        },
        error: function (xhr, status, error) {
            console.error("Error creating player:", error);
        }
    });
});




