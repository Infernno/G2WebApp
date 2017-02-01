$(document).ready(function () {
    $("ul.b-rating").each(function (index, item) {
        BindToElement(item, "post", "div.up", "div.down", "div.totalVotes", "articleId");
    });

    $(".comment").each(function (index, item) {
        BindToElement(item, "comment", "span.up", "span.down", "span.comment-rating", "commentId");
    });
});

function BindToElement(item, type, upVoteDiv, downVoteDiv, totalVotesDiv, idDiv) {

    var upVoteArrow = $(item).find(upVoteDiv);
    var downVoteArrow = $(item).find(downVoteDiv);
    var totalVotesElement = $(item).find(totalVotesDiv);

    var id = upVoteArrow.attr(idDiv);

    var voteValue = 0;

    upVoteArrow.click(function () {
        upVoteArrow.toggleClass("upvoted");
        downVoteArrow.removeClass("downvoted");

        voteValue = $(this).hasClass("upvoted") ? 1 : 0;
        sendRequest(id, type, voteValue, totalVotesElement);
    });

    downVoteArrow.click(function () {
        upVoteArrow.removeClass("upvoted");
        downVoteArrow.toggleClass("downvoted");

        voteValue = $(this).hasClass("downvoted") ? -1 : 0;
        sendRequest(id, type, voteValue, totalVotesElement);
    });

    /*
    var hasVoted = upVoteArrow.hasClass("upvoted") || downVoteArrow.hasClass("downvoted");

    if (hasVoted === false) {
        upVoteArrow.hover(function () {
            $(this).addClass("upvoted");
        }, function () {
            $(this).removeClass("upvoted");
        });

        downVoteArrow.hover(function () {
            $(this).addClass("downvoted");
        }, function () {
            $(this).removeClass("downvoted");
        });
    }
    */
}

function sendRequest(id, type, value, element) {
    $.getJSON("/vote/" + type + "?Id=" + id + "&Value=" + value, function (data) {
        if (data.overallRating != null) {
            element.html(data.overallRating);
        }
        else if (data.error != null) {
            showMessage(data.error, "error");
        }
    });
}