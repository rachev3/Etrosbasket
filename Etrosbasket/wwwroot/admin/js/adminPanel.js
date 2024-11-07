// pdf uploading logic
$(document).ready(function () {
    // Delegate events to document to ensure they work with dynamically loaded content
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
        $('#fileInput').val(''); // Clear file input
        $('#filePreview').html('<p>No file selected.</p>'); // Reset preview
    });

    // Handle form submission with AJAX
    $(document).on('submit', '#fileUploadForm', function (e) {
        e.preventDefault();
        var formData = new FormData(this);

        $.ajax({
            url: '/Player/UploadStatistic',
            type: 'POST',
            data: formData,
            contentType: false,
            processData: false,
            success: function (response) {
                alert("File uploaded successfully!");
                $('#uploadFileModal').modal('hide'); // Hide the modal on success
                // Optionally, reload data or update UI here
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

// statistic upload modal 
$(document).on('click', '.upload-pdf-button', function (e) {
    e.preventDefault();

    $.ajax({
        url: '/Player/LoadUploadModalContent', // Controller action to load the partial view
        type: 'GET',
        success: function (response) {
            $('#uploadModalContent').html(response); // Load partial view content into modal
            $('#uploadFileModal').modal('show'); // Show the modal
        },
        error: function (xhr, status, error) {
            console.error("Error loading upload modal content:", error);
        }
    });
});






