$(document).ready(function () {

    // Format vote count (e.g., 4900 -> 4.9k)
    function formatVoteCount(count) {
        if (count >= 1000000) {
            return (count / 1000000).toFixed(1) + 'm';
        } else if (count >= 1000) {
            return (count / 1000).toFixed(1) + 'k';
        }
        return count.toString();
    }

    // Handle Post voting
    $('#commentDetails').on("click", ".vote-btn", (function () {
        if ($(this).hasClass('active')) {
            // duplicate voting.
            return;
        }

        upvBtn = $("#postUpvote");
        dwvBtn = $("#postDownvote");
        const isUpvote = $(this).hasClass('upvote');
        const current_count = Number($('.post-detail').data('post-votes'));
        const voteCount = $(this).siblings('.vote-count');
        const postId = $('.post-detail').data('post-id');

        // Instantly update vote count before ajax call
        const newCount = isUpvote ? current_count + 1 : current_count - 1;
        voteCount.text(formatVoteCount(newCount));
        $('.post-detail').data('post-votes', newCount);

        $.ajax({
            url: `/posts/${postId}/vote`,
            method: 'POST',
            data: { isUpvote: isUpvote },
            success: function (response) { 
                // voteCount.text(formatVoteCount(response.newVoteCount));

                // Update button styles
                $('.vote-btn.post-voting').removeClass('active');
                if (response.userVote !== 0) {
                    $(isUpvote ? '#postUpvote' : '#postDownvote').addClass('active');
                }
            }
        });
    }));

    // Handle comments voting
    // Note: this style of binding the event via the parent helps ensure new children added via the partial views still get triggered on click.
    $('#commentsThread').on("click", ".vote-btn", (function () {
        if ($(this).hasClass('active')) {
            // duplicate voting.
            return;
        } 
        const isUpvote = $(this).hasClass('upvote');
        const postId = $('.post-detail').data('post-id');
        const thisCard = $(this).closest('.comment-container');
        const commId = thisCard.data('cm-id');
        let current_count = Number(thisCard.data('cm-vc'));
        //let commId = $(this).closest('.comment-container').data('cm-id');
        //console.log("comment Id: ", commId);
        const thisBtn = $(this);
        const voteCountElm = $(this).siblings('.vote-count');
        const otherBtn = $(this).siblings('.vote-btn');

        //debugger;
        //// get the current action ( up or down.)
        //// get the 2 vote buttons 

        //alert("voting comment ID: ",commId)
        //return;

        // get the postId, commentId and the voting type (up or down.)

        // Instantly update vote count before ajax call
        //const current_count = Number($('.comment-container').data('cm-vc'));
        const newCount = isUpvote ? current_count + 1 : current_count - 1;
        voteCountElm.text(formatVoteCount(newCount));
        thisCard.data('cm-vc', newCount);
        //$('.comment-container').data('cm-vc', newCount);

        $.ajax({
            url: `/posts/${postId}/vote/${commId}`,
            method: 'POST',
            data: { isUpvote: isUpvote },
            success: function (response) { 
                // Update button styles
                otherBtn.removeClass('active');
                thisBtn.addClass('active');
            }
        });
    }));

    // Handle post comment creation
    $('.comment-input button').click(function () {
        const textarea = $(this).siblings('textarea');
        const comment = textarea.val().trim();
        if (!comment) {
            alert("Comment text is empty!");
            return;
        }

        const postId = $('.post-detail').data('post-id');

        $.ajax({
            url: `/posts/${postId}/comment`,
            method: 'POST',
            data: { content: comment },
            success: function (partialViewHtml) {
                $("#commentsThread").prepend(partialViewHtml);
                textarea.val('');
                return; 
            },
            error: function (xhr) {
                // Handle errors
                let errorMessage = "Failed to add comment. Please try again.";
                if (xhr.status === 400) {
                    errorMessage = xhr.responseText; // Show server-provided validation error
                } else if (xhr.status === 500) {
                    errorMessage = "An internal server error occurred. Please try later.";
                }
                //alert(errorMessage);
                $("#error-message").text(errorMessage).show();
            }
        });
    });

    // Load comment reply form.
    $(document).on('click', '.comment-action-btn', function () {
        if (!$(this).find('.fa-reply').length) return;

        // Todo: close any currently open reply box.

        const comment = $(this).closest('.comment-container');
        const replyForm = ` <br/>
        <div class="reply-form mb-3">
            <textarea class="form-control mb-2" rows="2" placeholder="Write a reply..."></textarea>
            <button class="btn btn-primary btn-sm submit-reply">Reply</button>
            <button class="btn btn-light btn-sm cancel-reply">Cancel</button>
        </div>
    `;

        // Only add reply form if it doesn't exist
        if (!comment.find('.reply-form').length) {
            comment.append(replyForm);
        }
    });

    // Handle reply submission
    $(document).on('click', '.submit-reply', function () {
        // could be a reply to a parent comment or to another reply.
        // need to first fetch the commentId.
            // then try to fetch the replyId if any.
        const replyForm = $(this).closest('.reply-form');
        const commentCard = $(this).closest('.comment-container');
        //const parentCommentCard = $(this).closest('.comment-replies');
        const replyCard = $(this).closest('.comment-container.reply-card');
        const reply = replyForm.find('textarea').val().trim();
        if (!reply) {
            alert("Reply is empty!");
            return;
        }

        const postId = $('.post-detail').data('post-id');
        const commentId = commentCard.data('cm-id');
        let repId = null;
        if (replyCard.length > 0) {
            repId = replyCard.data('rp-id')
        }

        $.ajax({
            url: `/posts/${postId}/reply`,
            method: 'POST',
            data: { content: reply, commentId: commentId, replyId: repId },
            success: function (partialViewHtml) {
                if (!!repId) {
                    // indent the reply in a nested format
                    commentCard.after(partialViewHtml);
                }
                else {
                    commentCard.find('.comment-replies-all').prepend(partialViewHtml);
                }
                
                //parentCommentCard.prepend(partialViewHtml);
                //commentCard.prepend(partialViewHtml);
                //commentCard.after(partialViewHtml);
                replyForm.remove();
              
            },
            error: function (xhr) {
                // Handle errors
                let errorMessage = "Failed to add a reply. Please try again.";
                if (xhr.status === 400) {
                    errorMessage = xhr.responseText; // Show server-provided validation error
                } else if (xhr.status === 500) {
                    errorMessage = "An internal server error occurred. Please try later.";
                }
                //alert(errorMessage);
                $("#error-message").text(errorMessage).show();
            }
        });
    });

    // Handle reply cancellation
    $(document).on('click', '.cancel-reply', function () {
        $(this).closest('.reply-form').remove();
    });


})