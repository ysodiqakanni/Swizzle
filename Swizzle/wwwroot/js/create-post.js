$(document).ready(function () {
    let selectedCommunityId = null;

    // Community search handling
    //$('.community-search').on('input', function () {
    //    const searchTerm = $(this).val();
    //    const resultsContainer = $('.community-results');

    //    if (searchTerm.length > 0) {
    //        resultsContainer.show();
    //        // Here you would typically make an AJAX call to search communities
    //        $.ajax({
    //            url: '/api/communities/search',
    //            data: { query: searchTerm },
    //            success: function (results) {
    //                // Populate results
    //                resultsContainer.html(results.map(community => `
    //                    <div class="community-result-item" data-community-id="${community.id}">
    //                        <img src="${community.iconUrl}" alt="${community.name}" class="community-icon">
    //                        <div class="community-info">
    //                            <div class="community-name">${community.name}</div>
    //                            <div class="community-stats">${community.memberCount} members</div>
    //                        </div>
    //                    </div>
    //                `).join(''));
    //            }
    //        });
    //    } else {
    //        resultsContainer.hide();
    //    }
    //});


    //// Community selection
    //$('.community-results').on('click', '.community-result-item', function () {
    //    const communityName = $(this).find('.community-name').text();
    //    selectedCommunityId = $(this).data('community-id');
    //    $('.community-search').val(communityName);
    //    $('.community-results').hide();
    //});

    //// Initialize rich text editor
    //if ($('#rich-text-editor').length) {
    //    // Initialize your rich text editor here
    //}


    $("#btnSubmitText").on("click", function (e) {
        console.log("submiting text only");
        e.preventDefault();
        const submitButton = $(this);
        const originalButtonText = submitButton.text();
        submitButton.prop('disabled', true).html('<span class="spinner-border spinner-border-sm me-2"></span>Posting...');

        selectedCommunityId = $("#CommunityId").val();
        // Validate required fields
        if (!selectedCommunityId) {
            showError('Please select a community');
            resetSubmitButton(submitButton, originalButtonText);
            return;
        }

        const titleInput = $("#Title1");
        if (!titleInput.val().trim()) {
            showError('Please enter a title');
            resetSubmitButton(submitButton, originalButtonText);
            return;
        }


        // Prepare form data
        const formData = new FormData();
        formData.append('CommunityId', selectedCommunityId);
        formData.append('Title1', titleInput.val().trim());

        // Add rich text editor content if exists
        const description = tinymce.get("rich-text-editor").getContent();
        formData.append('Description', description);

        // AJAX submission
        $.ajax({
            url: '/posts/create',  // '@Url.Action("Create", "Posts")',
            method: 'POST',
            data: formData,
            processData: false,
            contentType: false,
            headers: {
                'RequestVerificationToken': $('input[name="__RequestVerificationToken"]').val()
            },
            success: function (response) {
                if (response.success) {
                    // Show success message
                    showSuccess('Post created successfully!');
                    // Redirect after a brief delay
                    setTimeout(() => {
                        window.location.href = response.redirectUrl;
                    }, 2000);
                } else {
                    showError(response.message || 'Failed to create post');
                    resetSubmitButton(submitButton, originalButtonText);
                }
            },
            error: function (xhr, status, error) {
                showError('An error occurred while creating the post');
                resetSubmitButton(submitButton, originalButtonText);
                console.error('Error:', error);
            }
        });

    })

    // Form submission
    $('.post-form').on('submit', function (e) {
        e.preventDefault();

        // Show loading state
        const submitButton = $(this).find('button[type="submit"]');
        const originalButtonText = submitButton.text();
        submitButton.prop('disabled', true).html('<span class="spinner-border spinner-border-sm me-2"></span>Posting...');

        // Validate required fields
        if (!selectedCommunityId) {
            showError('Please select a community');
            resetSubmitButton();
            return;
        }

        const titleInput = $(this).find('input[asp-for="Title"]');
        if (!titleInput.val().trim()) {
            showError('Please enter a title');
            resetSubmitButton();
            return;
        }

        // Prepare form data
        const formData = new FormData(this);
        formData.append('CommunityId', selectedCommunityId);

        // Add rich text editor content if exists
        const description = tinymce.get("rich-text-editor").getContent();
        const richTextEditor = $('#Description');
        if (richTextEditor.length) {
            formData.append('Description', description);
        }

        // Handle media files
        const mediaInput = $('#mediaInput')[0];
        if (mediaInput && mediaInput.files.length > 0) {
            for (let file of mediaInput.files) {
                formData.append('MediaFiles', file);
            }
        }

        // AJAX submission
        $.ajax({
            url: $(this).attr('action'),
            method: 'POST',
            data: formData,
            processData: false,
            contentType: false,
            headers: {
                'RequestVerificationToken': $('input[name="__RequestVerificationToken"]').val()
            },
            xhr: function () {
                const xhr = new window.XMLHttpRequest();
                // Upload progress
                xhr.upload.addEventListener('progress', function (evt) {
                    if (evt.lengthComputable) {
                        const percentComplete = (evt.loaded / evt.total) * 100;
                        // Update progress bar if you have one
                        $('.upload-progress-bar').width(percentComplete + '%');
                    }
                }, false);
                return xhr;
            },
            success: function (response) {
                if (response.success) {
                    // Show success message
                    showSuccess('Post created successfully!');
                    // Redirect after a brief delay
                    setTimeout(() => {
                        window.location.href = response.redirectUrl;
                    }, 2000);
                } else {
                    showError(response.message || 'Failed to create post');
                    resetSubmitButton();
                }
            },
            error: function (xhr, status, error) {
                showError('An error occurred while creating the post');
                resetSubmitButton();
                console.error('Error:', error);
            }
        });

      
    });


    function resetSubmitButton(btn, text) {
        btn.prop('disabled', false).text(text);
    }

    // Character counter
    $('.post-title-input').on('input', function () {
        const counter = $(this).siblings('.title-counter');
        counter.text(`${this.value.length}/300`);
    });

    // Helper functions for notifications
    function showError(message) {
        // You can use any notification library here (e.g., Toastr, SweetAlert)
        $('.alert-container').html(`
            <div class="alert alert-danger alert-dismissible fade show" role="alert">
                ${message}
                <button type="button" class="btn-close" data-bs-dismiss="alert"></button>
            </div>
        `);
    }

    function showSuccess(message) {
        $('.alert-container').html(`
            <div class="alert alert-success alert-dismissible fade show" role="alert">
                ${message}
                <button type="button" class="btn-close" data-bs-dismiss="alert"></button>
            </div>
        `);
    }
});