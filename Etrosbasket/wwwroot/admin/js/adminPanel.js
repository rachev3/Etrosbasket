// pdf uploading logic
$(document).ready(function () {

    $(document).on('click', '#selectFileButton', function () {
        $('#fileInput').click();
    });

    // Show preview when a file is selected
    $(document).on('change', '#fileInput', function () {
        var file = this.files[0];
        if (file) {
            var reader = new FileReader();
            reader.onload = function (e) {
                $('#filePreview').html(`<embed src="${e.target.result}" type="application/pdf" width="100%" height="100%">`);
            };
            reader.readAsDataURL(file);
        } else {
            $('#filePreview').html('<p>No file selected.</p>');
        }
    });

    // Discard selected file
    $(document).on('click', '#discardFileButton', function () {
        $('#fileInput').val('');
        $('#filePreview').html('<p>No file selected.</p>');
    });

    // Handle form submission with AJAX
    $(document).on('submit', '#fileUploadForm', function (e) {
        e.preventDefault();
        var formData = new FormData(this);
        var playerId = $('input[name="playerId"]').val();
        var playerName = $('input[name="playerName"]').val();

        $.ajax({

            url: `/Player/UploadStatistic?playerId=${playerId} &? playerName = ${encodeURIComponent(playerName)}`,                                                         /*   ?playerId=${ playerId } &? playerName = ${ encodeURIComponent(playerName) }*/
            type: 'POST',
            data: formData,
            contentType: false,
            processData: false,
            success: function (response) {
                if (response.success) {
                    $('#uploadFileModal').modal('hide');
                    alert(response.message);
                } else {
                    $('#uploadFileModal').modal('hide');
                    alert(response.message);
                }
            },
            error: function (xhr, status, error) {
                console.error("Error uploading file:", error);
            }
        });
    });
});


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

    var userConfirmed = confirm("Are you sure you want to delete this player?");
    if (!userConfirmed) {
        // If the user clicks "Cancel", exit the function
        return false;
    }

    // If "OK" is clicked, proceed with deletion
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


//edit player modal load
$(document).on('click', '#editPlayerButton', function (e) {
    e.preventDefault();
    var playerId = $(this).data('player-id');
    console.log(playerId)
    $.ajax({
        url: '/Player/Edit', // Adjust to your controller and action
        data: { playerId: playerId },
        type: 'GET',
        success: function (response) {
            $('#editModalContent').html(response); // Load form content into the modal
            $('#editPlayerModal').modal('show'); // Show the modal
        },
        error: function (xhr, status, error) {
            console.error("Error loading create player modal:", error);
            console.log("Status:", status);
            console.log("Response:", xhr.responseText);
        }
    });
});


$(document).on('click', '#submitPlayerForm', function (e) {
    e.preventDefault();


    var formData = $('#editPlayerForm').serialize();


    $.ajax({
        url: '/Player/Edit', // URL to the controller action
        type: 'POST',
        data: formData,
        success: function (response) {

            alert("Player updated successfully!");
            $('#editPlayerModal').modal('hide');

        },
        error: function (xhr, status, error) {
            // Handle error (e.g., show an error message)
            alert("An error occurred: " + xhr.responseText);
        }
    });
});



// statistic upload modal 
$(document).on('click', '.upload-pdf-button', function (e) {
    e.preventDefault();
    var playerId = $(this).data('player-id');
    var playerName = $(this).data('player-name');

    $.ajax({
        url: '/Player/LoadUploadModalContent',
        data: { playerId: playerId, playerName: playerName },
        type: 'GET',
        success: function (response) {
            $('#uploadModalContent').html(response);
            $('#uploadFileModal').modal('show');
        },
        error: function (xhr, status, error) {
            console.error("Error loading upload modal content:", error);
        }
    });
});






