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

    // Handle voting
    $('.vote-btn').click(function () {
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
    });

    // Handle comments
    $('.comment-input button').click(function () {
        const textarea = $(this).siblings('textarea');
        const comment = textarea.val().trim();
        if (!comment) return;

        const postId = $('.post-detail').data('post-id');

        $.ajax({
            url: `/api/posts/${postId}/comments`,
            method: 'POST',
            data: { content: comment },
            success: function (response) {
                // Append new comment
                const newComment = `
                <div class="comment" data-comment-id="${response.commentId}">
                    <div class="comment-content">${response.content}</div>
                    <div class="comment-metadata">
                        <span class="comment-author">${response.author}</span>
                        <span class="comment-time">${response.timestamp}</span>
                    </div>
                    <div class="comment-actions">
                        <button class="comment-action-btn">
                            <i class="fas fa-reply"></i> Reply
                        </button>
                        <!-- Other action buttons -->
                    </div>
                </div>
            `;
                $('.comments-list').prepend(newComment);
                textarea.val('');
            }
        });
    });

    // Handle comment replies
    $(document).on('click', '.comment-action-btn', function () {
        if (!$(this).find('.fa-reply').length) return;

        const comment = $(this).closest('.comment');
        const replyForm = `
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
        const replyForm = $(this).closest('.reply-form');
        const comment = $(this).closest('.comment');
        const reply = replyForm.find('textarea').val().trim();
        if (!reply) return;

        const commentId = comment.data('comment-id');

        $.ajax({
            url: `/api/comments/${commentId}/replies`,
            method: 'POST',
            data: { content: reply },
            success: function (response) {
                const newReply = `
                <div class="comment reply" data-comment-id="${response.replyId}">
                    <div class="comment-content">${response.content}</div>
                    <div class="comment-metadata">
                        <span class="comment-author">${response.author}</span>
                        <span class="comment-time">${response.timestamp}</span>
                    </div>
                    <div class="comment-actions">
                        <button class="comment-action-btn">
                            <i class="fas fa-reply"></i> Reply
                        </button>
                        <!-- Other action buttons -->
                    </div>
                </div>
            `;
                comment.after(newReply);
                replyForm.remove();
            }
        });
    });

    // Handle reply cancellation
    $(document).on('click', '.cancel-reply', function () {
        $(this).closest('.reply-form').remove();
    });


})